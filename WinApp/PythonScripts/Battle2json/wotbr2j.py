#################################################### 
# World of Tanks Battle Results to JSON            # 
# by BadButton at wotnumbers.com                   # 
# originally by Phalynx www.vbaddict.net (retired) # 
#################################################### 
import struct, json, time, sys, os, zlib, traceback, datetime
import cPickle, StringIO
from itertools import izip 
from collections import OrderedDict
from DictPackers import Meta
from battle_results_constants import BATTLE_RESULT_ENTRY_TYPE as ENTRY_TYPE
import battle_results_common, battle_results_random

parser = dict()
parser['version'] = "1.19.0.0"

parser['name'] = 'http://wotnumbers.com'
parser['processingTime'] = int(time.mktime(time.localtime()))

# Main procedure
def main(): 
    # main variables
    global filename_source, filename_target, option_format, parser, option_logging, log_file
    option_format = False
    option_logging = False
    # the file to be parsed
    global cachefile 
    cachefile = None
    # structs for unpacking data generated based from battle_results_xxx files
    global BR_COMMON, BR_ACCOUNT, BR_ACCOUNT_ALL, BR_VEHICLE, BR_VEHICLE_ALL, BR_PLAYER_INFO, BR_SERVER
    BR_COMMON = Meta()
    BR_ACCOUNT = Meta()
    BR_ACCOUNT_ALL = Meta()
    BR_VEHICLE = Meta()
    BR_VEHICLE_ALL = Meta()
    BR_PLAYER_INFO = Meta()
    BR_SERVER = Meta()

    # Check if at least one command line arg exists
    if len(sys.argv) == 1: 
        helpMessage() 
        sys.exit(2) 

    # Get command line ags
    script_dir = os.path.dirname(__file__) # Absolute path to script location
    for argument in sys.argv:
        if argument == "-f": 
            option_format = True
        if argument == "-l": 
            option_logging = True
            log_file = os.path.join(script_dir, "wotbr2j_log.txt")

    # Get battle dat-file, must be first arg
    filename_source = str(sys.argv[1])

    # Display startup message
    printmessage('', True)
    printmessage('### WoT Battle Result to Json - version ' + parser['version'] + ' ###', True) 
    printmessage('Time: ' + str(datetime.datetime.now()), True)
    printmessage('Encoding: ' + str(sys.getdefaultencoding()) + ' - ' + str(sys.getfilesystemencoding()), True)
    if option_logging:
        printmessage('Logging to file: ' + log_file, True) 
    printmessage('Starting to process file: ' + filename_source, True) 

    # Prepare json result file 
    filename_target = os.path.splitext(filename_source)[0] 
    filename_target = filename_target + '.json' 
    if not os.path.exists(filename_source) or not os.path.isfile(filename_source):
        exitwitherror('Battle Result file not found or no access to file: ' + filename_source)
    if not os.access(filename_source, os.R_OK):
        exitwitherror('Cannot read Battle result, read-access was denied for file: ' + filename_source)

    # Read binary battle file
    cachefile = open(filename_source, 'rb') 
              
    # Read file content
    try: 
        # Read file, first element is to be ignored, second is battle result (br) all data in raw format, needs furher unpickling 
        legacyBattleResultVersion, brAllDataRaw = cPickle.load(cachefile)
    except Exception, e: 
        exitwitherror('Error occured reading battle file:' + filename_source + ' - Error Message:' + e.message) 

    printmessage('Battle file binary content read, found legacy battle result version: ' + str(legacyBattleResultVersion), False)

    if not 'brAllDataRaw' in locals(): 
        exitwitherror('Battle Result file read, but main data object is missing.')

    # Read raw binary data from battle file
    arenaUniqueID, brAccount, brVehicleRaw, brOtherDataRaw = brAllDataRaw
    
    # Unpickle to get readable data
    brAccount = Unpickler.loads(zlib.decompress(brAccount))
    brVehicle = Unpickler.loads(zlib.decompress(brVehicleRaw))
    brCommon, brPlayersInfo, brPlayersVehicle, brPlayersResult = Unpickler.loads(zlib.decompress(brOtherDataRaw))

    # Drill down in to battle result vehicle and get spesific data
    brVehicle = brVehicle.items()[0]
    tankId = brVehicle[0]
    brVehicle = brVehicle[1]
    
    try:
        # Prepare result as json, add some initial values
        jsonCommon = {}
        jsonVehicle = {}
        jsonAccount = {}
        jsonPlayers = {}

        # Split BATTLE_RESULT struct into separate structs per ENTRY_TYPE
        setBattleResult(battle_results_common.BATTLE_RESULTS)

        # Validate and unpack common data
        validate('common', BR_COMMON, brCommon)
        jsonCommon = BR_COMMON.unpack(brCommon)
        # remove accountCompDescr since this is not needed in this result set, parsed separately
        if 'accountCompDescr' in jsonCommon:
            del jsonCommon['accountCompDescr']

        # Inspect result to find game mode
        bonusType = -1
        if 'bonusType' in jsonCommon:
            bonusType = jsonCommon['bonusType']
        
        # Extend BATTLE_RESULT struct based on game mode, only support for random mode for now
        if (bonusType == 1):
            setBattleResult(battle_results_random.BATTLE_RESULTS)
            printmessage('Detected battle mode: Random', True)
        elif (bonusType == -1):
            exitwitherror('Could not classify battle mode (common.bonusType not foud)')
        else:
            exitwitherror('Detected unsupported battle mode (bonusType = {})'.format(bonusType))

        # Validate and unpack personal.avatars data
        validate('private.account', BR_ACCOUNT, brAccount)
        jsonAccount = brAccount = BR_ACCOUNT.unpack(brAccount)
        # TODO: Can be removed later, testing new method: Remove avatarDatageEventList, not Json compatible value
        #if 'avatarDamageEventList' in jsonAccount:
        #    del jsonAccount['avatarDamageEventList']

        # Validate and unpack vehicle data
        validate('private.vehicle', BR_VEHICLE, brVehicle)
        jsonVehicle = brVehicle = BR_VEHICLE.unpack(brVehicle)
        # Remove personal.vehicle.details, not Json compatible value
        if 'details' in jsonVehicle:
            del jsonVehicle['details']
        
        # Validate and unpack players info, this is a list of players only validate first item 
        validatedPlayer = False
        for accountDBID, player in brPlayersInfo.iteritems():
            if not validatedPlayer:
                validate('players', BR_PLAYER_INFO, player)
                validatedPlayer = True
            jsonPlayers[accountDBID] = BR_PLAYER_INFO.unpack(player)

        # Validate and unpack players account data = result, this is a list of all players participated in battle, validate first item 
        validatedPlayer = False
        for accountDBID, player in brPlayersResult.iteritems():
            if not validatedPlayer:
                validate('players.result', BR_ACCOUNT_ALL, player)
                validatedPlayer = True
            jsonPlayers[accountDBID]['result'] = BR_ACCOUNT_ALL.unpack(player)

        # Validate and unpack vehicles data, this is a list of all vechicles participated in battle, validate first item 
        validatedVehicle = False
        for vehicleId, vehicles in brPlayersVehicle.iteritems():
            for vehTypeCompDescr, vehicle in vehicles.iteritems():
                if not validatedVehicle:
                    validate('players.vehicle', BR_VEHICLE_ALL, vehicle)
                    validatedVehicle = True
                playerVehicle = BR_VEHICLE_ALL.unpack(vehicle)
                if 'accountDBID' in playerVehicle:
                    accountDBID = playerVehicle['accountDBID']
                    jsonPlayers[accountDBID]['vehicle'] = playerVehicle
                    jsonPlayers[accountDBID]['vehicle']['vehicleId'] = vehicleId
                else:
                    printmessage('Warning: Found vehicle data but could not map to player. TankId: {}'.format(vehTypeCompDescr), True)
        
        # Prepare result 
        printmessage("All data read, preparing result", True)
        parser['result'] = 'ok'
        br2jResult = OrderedDict()
        br2jResult['parser'] = parser
        br2jResult['arenaUniqueID'] = arenaUniqueID
        br2jResult['tankId'] = tankId
        br2jResult['common'] = jsonCommon
        br2jResult['private'] = {}
        br2jResult['private']['account'] = jsonAccount
        br2jResult['private']['vehicle'] = jsonVehicle
        br2jResult['players'] = jsonPlayers

        # write json file now
        dumpjson(br2jResult) 

        printmessage('### Done ###', True) 
        printmessage('', False) 
        cachefile.close()

    except IndexError, e:
        printmessage('Index error during data unpacking: ' + e.message, True)
        printmessage(traceback.format_exc(e), False)
        exitwitherror('Battle result cannot be read')
    except KeyError, e:
        printmessage('Key error during data unpacking: ' + e.message, True)
        printmessage(traceback.format_exc(e), False)
        exitwitherror('Battle result cannot be read')
    except Exception, e:
        printmessage('Exception error during data unpacking: ' + e.message, True)
        printmessage(traceback.format_exc(e), False)
        exitwitherror('Battle result cannot be read')

    sys.exit(0) 


# CLASS **************************************************************************************************************************

# Extract binary data to readable data
class Unpickler(object): 
    @classmethod
    def loads(cls, pickle_string):
        try:
            unpickler = cPickle.Unpickler(StringIO.StringIO(pickle_string))
            return unpickler.load()
        except Exception, e:
            cachefile.close()
            raise cPickle.UnpicklingError('Unpickler Error: ' + e.message)
            
    @classmethod
    def load(cls, pickle_file):
        try:
            unpickler = cPickle.Unpickler(pickle_file)
            return unpickler.load()
        
        except EOFError, er:
            cachefile.close()
            raise cPickle.UnpicklingError('Unpickler EOF Error: ' + er.message)
        
        except Exception, e:
            cachefile.close()
            raise cPickle.UnpicklingError('Unpickler Error: ' + e.message)

# HELPERS **************************************************************************************************************************

# Display help message explaining args to be used
def helpMessage(): 
    print str(sys.argv[0]) + 'battleresult-filename.dat [options]'
    print 'Options:'
    print '-f Formats output result to JSON pretty print (includes line breaks and indents)'
    print '-l Logging to file enabled, output to file: wotbr2j_log.txt'

# Validation procedure
def validate(objectName, struct, data):
    structLen = str(len(struct) + 1) 
    dataLen = str(len(data))
    if (structLen <> dataLen):
        errMsg = 'Wrong number of data items for object: {} (found: {}, expecting {})'.format(objectName, dataLen, structLen)
        exitwitherror(errMsg, None, True)
    else:
        printmessage('> Successfully validated object: {} (found {} data items)'.format(objectName, dataLen), True)
        return

# Split battle result into into separate structs for mapping to data
def setBattleResult(battleResult):
    global BR_COMMON, BR_ACCOUNT, BR_ACCOUNT_ALL, BR_VEHICLE, BR_VEHICLE_ALL, BR_PLAYER_INFO, BR_SERVER
    for item in battleResult:
        itemData = (item[0],item[1],item[2],item[3],item[4])
        itemType = item[5]
        if itemType == ENTRY_TYPE.COMMON:
            BR_COMMON += Meta(itemData)
        elif itemType == ENTRY_TYPE.ACCOUNT_ALL:
            BR_ACCOUNT += Meta(itemData)
            BR_ACCOUNT_ALL += Meta(itemData)
        elif itemType == ENTRY_TYPE.ACCOUNT_SELF:
            BR_ACCOUNT += Meta(itemData) 
        elif itemType == ENTRY_TYPE.VEHICLE_ALL:
            BR_VEHICLE += Meta(itemData)
            BR_VEHICLE_ALL += Meta(itemData)
        elif itemType == ENTRY_TYPE.VEHICLE_SELF:
            BR_VEHICLE += Meta(itemData)
        elif itemType == ENTRY_TYPE.PLAYER_INFO:
            BR_PLAYER_INFO += Meta(itemData)
        elif itemType == ENTRY_TYPE.SERVER:
            BR_SERVER += Meta(itemData)
    return

# Show message in console and/or log to file
def printmessage(logtext, to_console): 
    if to_console:
        print str(logtext)
        
    if option_logging:
        now = datetime.datetime.now() 
        message = str(now.strftime("%Y-%m-%d %H:%M:%S")) + " - " + str(logtext) + "\r\n"
        logFile = open(log_file, "a+b") 
        logFile.write(message) 
        logFile.close() 

# Error handling
def exitwitherror(message, e=None, abort=False):
    global parser, cachefile
    if e is None:
        printmessage(message, True)
    else:
        printmessage(message + e.message, True)
        printmessage(traceback.format_exc(e), True)
    if not abort:
        dossierheader = dict() 
        dossierheader['parser'] = dict() 
        dossierheader['parser']['result'] = "error"
        dossierheader['parser']['message'] = message 
        dumpjson(dossierheader) 
    if cachefile is not None:
        cachefile.close() 
    sys.exit(1) 

# Save data as json file
def dumpjson(bresult): 
    global option_logging, option_format, filename_target
    try:
        printmessage('Creating output file:' + filename_target, True)
        finalfile = open(filename_target, 'w') 
        if option_format: 
            finalfile.write(json.dumps(bresult, ensure_ascii=False, skipkeys=True, indent=4, cls=SetEncoder)) 
        else: 
            finalfile.write(json.dumps(bresult, ensure_ascii=False, skipkeys=True, cls=SetEncoder))        
        finalfile.close()
    except Exception, e:
        if finalfile is not None: 
            finalfile.close() 
        exitwitherror("Exception creating file: ", e, abort=True)


class SetEncoder(json.JSONEncoder):
    def default(self, obj):
        if isinstance(obj, set):
            return list(obj)
        return json.JSONEncoder.default(self, obj)

# Run main() on startup
if __name__ == '__main__': 
    main() 
