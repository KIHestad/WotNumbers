################################################### 
# World of Tanks Battle Result to JSON            # 
# by Phalynx www.vbaddict.net                     # 
#                                                 #
# Modified to run from c# using IronPhyton        #
# Edited version by BadButton -> 2015-11-17       #
###################################################
# IRONPYTHON MODIFIED: added cPicler and StringIO instead of SafePicler
import struct, json, time, sys, os, zlib, cPickle, StringIO

from itertools import izip 

LEGACY_VERSIONS_LENGTH = (50, 52, 60, 62, 68, 70, 74, 81, 84, 92, 117, 90, 89, 91)
LEGACY_VERSIONS = dict(((value, index+1) for index, value in enumerate(LEGACY_VERSIONS_LENGTH)))


VEHICLE_DEVICE_TYPE_NAMES = ('engine', 
 'ammoBay', 
 'fuelTank', 
 'radio', 
 'track', 
 'gun', 
 'turretRotator', 
 'surveyingDevice') 
VEHICLE_TANKMAN_TYPE_NAMES = ('commander', 
 'driver', 
 'radioman', 
 'gunner', 
 'loader') 


VEH_INTERACTION_DETAILS_LEGACY = ('spotted', 'killed', 'hits', 'he_hits', 'pierced', 'damageDealt', 'damageAssisted', 'crits', 'fire') 
VEH_INTERACTION_DETAILS_INDICES_LEGACY = dict(((x[1], x[0]) for x in enumerate(VEH_INTERACTION_DETAILS_LEGACY))) 
  
VEH_INTERACTION_DETAILS = (('spotted', 'B', 1, 0), 
 ('deathReason', 'b', 10, -1), 
 ('hits', 'H', 65535, 0), 
 ('he_hits', 'H', 65535, 0), 
 ('pierced', 'H', 65535, 0), 
 ('damageDealt', 'H', 65535, 0), 
 ('damageAssistedTrack', 'H', 65535, 0), 
 ('damageAssistedRadio', 'H', 65535, 0), 
 ('crits', 'I', 4294967295L, 0), 
 ('fire', 'H', 65535, 0)) 
VEH_INTERACTION_DETAILS_NAMES = [ x[0] for x in VEH_INTERACTION_DETAILS ] 
VEH_INTERACTION_DETAILS_MAX_VALUES = dict(((x[0], x[2]) for x in VEH_INTERACTION_DETAILS)) 
VEH_INTERACTION_DETAILS_INIT_VALUES = [ x[3] for x in VEH_INTERACTION_DETAILS ] 
VEH_INTERACTION_DETAILS_LAYOUT = ''.join([ x[1] for x in VEH_INTERACTION_DETAILS ]) 
VEH_INTERACTION_DETAILS_INDICES = dict(((x[1][0], x[0]) for x in enumerate(VEH_INTERACTION_DETAILS))) 
  
  
parser = dict()
parser['version'] = "0.9.12"
parser['name'] = 'http://www.vbaddict.net'
parser['processingTime'] = int(time.mktime(time.localtime()))
  
def usage(): 
    print str(sys.argv[0]) + " battleresult.dat [options]"
    print 'Options:'
    print '-f Formats the JSON to be more human readable'
    print '-s Server Mode, disable writing of timestamp, enable logging'
  
  
def main(): 

	import struct, json, time, sys, os, shutil, datetime 
	global filename_source, filename_target, option_server, option_format, parser, cachefile
	
	option_raw = 0
	option_format = 0
	option_server = 0
	option_frags = 1
			  
	if len(sys.argv) == 1: 
		usage() 
		sys.exit(2) 

	for argument in sys.argv: 
		if argument == "-r": 
			option_raw = 1
		elif argument == "-f": 
			option_format = 1
		elif argument == "-s": 
			option_server = 1

	filename_source = str(sys.argv[1]) 

	printmessage('', 0) 
	printmessage('###### WoTBR2J ' + parser['version'] + ' BATTLE FILE CONVERT TO JSON', 0) 
	printmessage('Time: ' + str(datetime.datetime.now()), 0) 
	printmessage('Encoding: ' + str(sys.getdefaultencoding()) + ' - ' + str(sys.getfilesystemencoding()), 0)
	printmessage('Processing ' + filename_source, 0) 
	  
	filename_target = os.path.splitext(filename_source)[0] 
	filename_target = filename_target + '.json'
	  
	if not os.path.exists(filename_source) or not os.path.isfile(filename_source) or not os.access(filename_source, os.R_OK): 
		exitwitherror('Battle Result does not exists!') 

	cachefile = open(filename_source, 'rb') 
			  
	try: 
		# IRONPYTHON MODIFIED: no use if SafeUnpickler
		#from os.path import SafeUnpickler
		legacyBattleResultVersion, battleResults = SafeUnpickler.load(cachefile) 
	except Exception, e: 
		exitwitherror('Battle Result cannot be read (pickle could not be read) ' + e.message) 

	if not 'battleResults' in locals(): 
		exitwitherror('Battle Result cannot be read (battleResults does not exist)') 

	# if len(battleResults[1]) in LEGACY_VERSIONS_LENGTH:
		# "parser['battleResultVersion'] = LEGACY_VERSIONS[len(battleResults[1])]
	#else:
		# Updates higher than v0.9.8 have to be identified using a list of new fields
	parser['battleResultVersion'] = 18
	
	while parser['battleResultVersion']>0:  
		printmessage("Processing Version: " + str(parser['battleResultVersion']), 0)
		issuccess, bresult = convertToFullForm(battleResults, parser['battleResultVersion']) 
		
		if issuccess==0:
			parser['battleResultVersion'] = parser['battleResultVersion']-1
		else:
			break
	
	if not 'personal' in bresult:
		exitwitherror('Battle Result cannot be read (personal does not exist)')

	# 0.9.8 and higher
	if len(list(bresult['personal'].keys()))<10:
		for vehTypeCompDescr, ownResults in bresult['personal'].copy().iteritems():
			if 'details' in ownResults:
				ownResults['details'] = handleDetailsCrits(ownResults['details'])
				
			for field in ('damageEventList', 'xpReplay', 'creditsReplay', 'tmenXPReplay', 'fortResourceReplay', 'goldReplay', 'freeXPReplay'):
				ownResults[field] = None
				
			bresult['personal'][vehTypeCompDescr] = ownResults
			
	# <0.9.8
	else:
		if 'details' in bresult['personal']:
		  
			if len(battleResults[1]) < 60: 
				bresult['personal']['details'] = VehicleInteractionDetails_LEGACY.fromPacked(bresult['personal']['details']).toDict() 
			else:
				bresult['personal']['details'] = VehicleInteractionDetails.fromPacked(bresult['personal']['details']).toDict()             
			
			bresult['personal']['details'] = handleDetailsCrits(bresult['personal']['details'])
	

	parser['result'] = 'ok'
	bresult['parser'] = parser
	
	dumpjson(bresult) 

	printmessage('###### Done!', 0) 
	printmessage('', 0) 

	# IRONPYTHON MODIFIED: close dossier input file
	cachefile.close()
	# IRONPYTHON MODIFIED: no need for exit, throws error when calling sys.exit
	#sys.exit(0)

def prepareForJSON(bresult):
	if 'personal' in bresult:

		if 'club' in bresult['personal']:	
			if 'clubDossierPopUps' in bresult['personal']['club']:
				oldClubDossier = bresult['personal']['club']['clubDossierPopUps'].copy()
				bresult['personal']['club']['clubDossierPopUps'] = dict()
				for achievement, amount in oldClubDossier.iteritems():
					bresult['personal']['club']['clubDossierPopUps'][str(list(achievement)[0]) + '-' + str(list(achievement)[1])] = amount
		
		if bresult['parser']['battleResultVersion'] >= 15:
			for vehTypeCompDescr, ownResults in bresult['personal'].copy().iteritems():
				if vehTypeCompDescr == 'avatar':
					if 'avatarDamageEventList' in bresult['personal'][vehTypeCompDescr]:
						del bresult['personal'][vehTypeCompDescr]['avatarDamageEventList']
				if 'club' in ownResults:	
					if 'clubDossierPopUps' in ownResults['club']:
						oldClubDossier = ownResults['club']['clubDossierPopUps'].copy()
						ownResults['club']['clubDossierPopUps'] = dict()
						for achievement, amount in oldClubDossier.iteritems():
							bresult['personal'][vehTypeCompDescr]['club']['clubDossierPopUps'][str(list(achievement)[0]) + '-' + str(list(achievement)[1])] = amount
		
			if len(bresult['personal'].copy())>1 and len(bresult['personal'].copy())<10 :
				#printmessage("Version 15 DOUBLE: " + str(bresult['arenaUniqueID']), 1)
				pass
			for vehTypeCompDescr, ownResults in bresult['personal'].copy().iteritems():
				if 'details' in ownResults:
					newdetails = detailsDictToString(ownResults['details'])
					bresult['personal'][vehTypeCompDescr]['details'] = newdetails
	
	return bresult
			
def detailsDictToString(mydict):
	mydictcopy = dict()
	for key, value in mydict.iteritems():
		value['vehicleid'] = key[0]
		value['typeCompDescr'] = key[1]
		mydictcopy[str(key[0]) + '-' + str(key[1])] = value

	return mydictcopy
	
	
	
def exitwitherror(message): 
	global parser
	printmessage(message, 1) 
	dossierheader = dict() 
	dossierheader['parser'] = dict() 
	dossierheader['parser']['result'] = "error"
	dossierheader['parser']['message'] = message 
	dumpjson(dossierheader) 
	cachefile.close()
	sys.exit(1) 

def dumpjson(bresult): 
	global option_server, option_format, filename_target
	bresult = prepareForJSON(bresult)
	
	try:
		if option_server == 1: 
			print json.dumps(bresult)    
		else: 
			finalfile = open(filename_target, 'w') 

			if option_format == 1: 
				finalfile.write(json.dumps(bresult, sort_keys=True, indent=4)) 
			else: 
				finalfile.write(json.dumps(bresult)) 

			# IRONPYTHON MODIFIED: close dossier output file
			finalfile.close()
	except Exception, e: 
		finalfile.close()
		exitwitherror("Exception: " + str(e))
		

def dictToList(indices, d): 
    l = [None] * len(indices) 
    for name, index in indices.iteritems(): 
        l[index] = d[name] 
  
    return l 
  
  
def listToDict(names, l): 
    d = {} 
    for x in enumerate(names): 
        d[x[1]] = l[x[0]] 
  
    return d 

def print_array(oarray):
	print json.dumps(oarray, sort_keys=True, indent=4)

def convertToFullForm(compactForm, battleResultVersion): 
	# from SafeUnpickler import SafeUnpickler #IRONPYTHON MODIFIED
	
	handled = 0
	import importlib
	battle_results_data = importlib.import_module('battle_results_shared_' + str(battleResultVersion).zfill(2))

	if len(battle_results_data.VEH_FULL_RESULTS)==0:
		exitwitherror("Unsupported Battle Result Version: " + str(battleResultVersion))
	else:
		if battleResultVersion >= 17:  

			arenaUniqueID, avatarResults, fullResultsList, pickled = compactForm
			fullResultsList = SafeUnpickler.loads(zlib.decompress(fullResultsList))
			avatarResults = SafeUnpickler.loads(zlib.decompress(avatarResults))
			personal = {}
			try:
				fullForm = {'arenaUniqueID': arenaUniqueID,
				'personal': personal,
				'common': {},
				'players': {},
				'vehicles': {},
				'avatars': {}}
				personal['avatar'] = avatarResults = battle_results_data.AVATAR_FULL_RESULTS.unpack(avatarResults)
				for vehTypeCompDescr, ownResults in fullResultsList.iteritems():
					vehPersonal = personal[vehTypeCompDescr] = battle_results_data.VEH_FULL_RESULTS.unpack(ownResults)
					vehPersonal['details'] = battle_results_data.VehicleInteractionDetails.fromPacked(vehPersonal['details']).toDict()
					vehPersonal['isPrematureLeave'] = avatarResults['isPrematureLeave']
					vehPersonal['fairplayViolations'] = avatarResults['fairplayViolations']

				commonAsList, playersAsList, vehiclesAsList, avatarsAsList = SafeUnpickler.loads(zlib.decompress(pickled))
				fullForm['common'] = battle_results_data.COMMON_RESULTS.unpack(commonAsList)
				for accountDBID, playerAsList in playersAsList.iteritems():
					fullForm['players'][accountDBID] = battle_results_data.PLAYER_INFO.unpack(playerAsList)

				for accountDBID, avatarAsList in avatarsAsList.iteritems():
					fullForm['avatars'][accountDBID] = battle_results_data.AVATAR_PUBLIC_RESULTS.unpack(avatarAsList)

				for vehicleID, vehiclesInfo in vehiclesAsList.iteritems():
					fullForm['vehicles'][vehicleID] = []
					for vehTypeCompDescr, vehicleInfo in vehiclesInfo.iteritems():
						fullForm['vehicles'][vehicleID].append(battle_results_data.VEH_PUBLIC_RESULTS.unpack(vehicleInfo))
			except IndexError, i:
				return 0, {}
			except KeyError, i:
				return 0, {}
			except Exception, e: 
				exitwitherror("Error occured while transforming Battle Result Version: " + str(battleResultVersion) + " Error: " + str(e))
				
		elif battleResultVersion >= 15:  

			arenaUniqueID, fullResultsList, pickled, uniqueSubUrl = compactForm
			fullResultsList = SafeUnpickler.loads(zlib.decompress(fullResultsList))
			personal = {}
			try:
				fullForm = {'arenaUniqueID': arenaUniqueID,
					'personal': personal,
					'common': {},
					'players': {},
					'vehicles': {},
					'uniqueSubUrl': uniqueSubUrl}
				for vehTypeCompDescr, ownResults in fullResultsList.iteritems():
					vehPersonal = personal[vehTypeCompDescr] = battle_results_data.VEH_FULL_RESULTS.unpack(ownResults)
					
					vehPersonal['details'] = battle_results_data.VehicleInteractionDetails.fromPacked(vehPersonal['details']).toDict()
							
				commonAsList, playersAsList, vehiclesAsList = SafeUnpickler.loads(zlib.decompress(pickled))
				fullForm['common'] = battle_results_data.COMMON_RESULTS.unpack(commonAsList)
				
				for accountDBID, playerAsList in playersAsList.iteritems():
					fullForm['players'][accountDBID] = battle_results_data.PLAYER_INFO.unpack(playerAsList)

				for vehicleID, vehiclesInfo in vehiclesAsList.iteritems():
					fullForm['vehicles'][vehicleID] = []
					for vehTypeCompDescr, vehicleInfo in vehiclesInfo.iteritems():
						fullForm['vehicles'][vehicleID].append(battle_results_data.VEH_PUBLIC_RESULTS.unpack(vehicleInfo))
						
			except IndexError, i:
				return 0, {}
			except Exception, e: 
				exitwitherror("Error occured while transforming Battle Result Version: " + str(battleResultVersion) + " Error: " + str(e))
		else:	
			fullForm = dict()
			try:
				fullForm = {'arenaUniqueID': compactForm[0], 
				 'personal': listToDict(battle_results_data.VEH_FULL_RESULTS, compactForm[1]), 
				 'common': {}, 
				 'players': {}, 
				 'vehicles': {}}

			except Exception, e: 
				exitwitherror("Error occured while transforming Battle Result Version: " + str(battleResultVersion) + " Error: " + str(e))
		
			if not 'personal' in fullForm:
				return fullForm
				  
			try:
				commonAsList, playersAsList, vehiclesAsList = SafeUnpickler.loads(compactForm[2]) 
			except Exception, e: 
				exitwitherror("Error occured while transforming Battle Result Version: " + str(battleResultVersion) + " Error: " + str(e))
			
			fullForm['common'] = listToDict(battle_results_data.COMMON_RESULTS, commonAsList) 
			
			for accountDBID, playerAsList in playersAsList.iteritems(): 
				fullForm['players'][accountDBID] = listToDict(battle_results_data.PLAYER_INFO, playerAsList) 

			for vehicleID, vehicleAsList in vehiclesAsList.iteritems(): 
				fullForm['vehicles'][vehicleID] = listToDict(battle_results_data.VEH_PUBLIC_RESULTS, vehicleAsList)

	return 1, fullForm

def handleDetailsCrits(details):

	if len(details)>0: 
		for vehicleid, detail_values in details.items(): 
			details[vehicleid]['critsDestroyedTankmenList'] = getDestroyedTankmen(detail_values)
			details[vehicleid]['critsCriticalDevicesList'] = getCriticalDevicesList(detail_values)
			details[vehicleid]['critsDestroyedDevicesList'] = getDestroyedDevicesList(detail_values)
			details[vehicleid]['critsCount'] = len(details[vehicleid]['critsDestroyedTankmenList']) + len(details[vehicleid]['critsDestroyedTankmenList']) + len(details[vehicleid]['critsDestroyedTankmenList'])
	
	return details

def getDestroyedTankmen(detail_values):
	
	destroyedTankmenList = [] 
	if detail_values['crits']>0: 
		destroyedTankmen = detail_values['crits'] >> 24 & 255
		  
		for shift in range(len(VEHICLE_TANKMAN_TYPE_NAMES)): 
			if 1 << shift & destroyedTankmen: 
				destroyedTankmenList.append(VEHICLE_TANKMAN_TYPE_NAMES[shift]) 
		
	return destroyedTankmenList  

def getCriticalDevicesList(detail_values):

	criticalDevicesList = [] 
	if detail_values['crits']>0: 
		criticalDevices = detail_values['crits'] & 4095
		  
		for shift in range(len(VEHICLE_DEVICE_TYPE_NAMES)): 
			if 1 << shift & criticalDevices: 
				criticalDevicesList.append(VEHICLE_DEVICE_TYPE_NAMES[shift]) 

	return criticalDevicesList

		
def getDestroyedDevicesList(detail_values):
	destroyedDevicesList = [] 
	if detail_values['crits']>0: 
		destroyedDevices = detail_values['crits'] >> 12 & 4095
		  
		for shift in range(len(VEHICLE_DEVICE_TYPE_NAMES)): 

			if 1 << shift & destroyedDevices: 
				destroyedDevicesList.append(VEHICLE_DEVICE_TYPE_NAMES[shift]) 
		
	return destroyedDevicesList 

############################################################################################################################ 

def printmessage(logtext, to_log): 
	import datetime, os 

	global option_server, filename_source

	#if option_server == 0: 
	#IRONPYTHON MODIFIED, PRINT RESULT ONLY TO CONSOLE
	print str(logtext)
		
	#if to_log == 1:
	#	now = datetime.datetime.now() 
	#	message = str(now.strftime("%Y-%m-%d %H:%M:%S")) + " # WOTBR2J: " + str(logtext) + " # " + str(filename_source) + "\r\n"
	#
	#	if option_server == 1: 
	#		logFile = open("/var/log/wotdc2j/wotdc2j.log", "a+b") 
	#		logFile.write(message) 
	#		logFile.close() 
		

# Pre 98
class _VehicleInteractionDetailsItem(object): 
  
    def __init__(self, values, offset): 
        self.__values = values 
        self.__offset = offset 
  
    def __getitem__(self, key): 
        return self.__values[self.__offset + VEH_INTERACTION_DETAILS_INDICES[key]] 
  
    def __setitem__(self, key, value): 
        self.__values[self.__offset + VEH_INTERACTION_DETAILS_INDICES[key]] = min(int(value), VEH_INTERACTION_DETAILS_MAX_VALUES[key]) 
  
    def __iter__(self): 
        return izip(VEH_INTERACTION_DETAILS_NAMES, self.__values[self.__offset:]) 
  
  
  
class VehicleInteractionDetails(object): 
  
    def __init__(self, vehicleIDs, values): 
        self.__vehicleIDs = vehicleIDs 
        self.__values = values 
        size = len(VEH_INTERACTION_DETAILS) 
        self.__offsets = dict(((x[1], x[0] * size) for x in enumerate(self.__vehicleIDs))) 
  
    @staticmethod
    def fromPacked(packed): 
        count = len(packed) / struct.calcsize(''.join(['<I', VEH_INTERACTION_DETAILS_LAYOUT])) 
        packedVehIDsLayout = '<%dI' % (count,) 
        packedVehIDsLen = struct.calcsize(packedVehIDsLayout) 
        vehicleIDs = struct.unpack(packedVehIDsLayout, packed[:packedVehIDsLen]) 
        values = struct.unpack('<' + VEH_INTERACTION_DETAILS_LAYOUT * count, packed[packedVehIDsLen:]) 
        return VehicleInteractionDetails(vehicleIDs, values) 
  
    def __getitem__(self, vehicleID): 
        offset = self.__offsets.get(vehicleID, None) 
        if offset is None: 
            self.__vehicleIDs.append(vehicleID) 
            offset = len(self.__values) 
            self.__values += VEH_INTERACTION_DETAILS_INIT_VALUES 
  
            self.__offsets[vehicleID] = offset 
        return _VehicleInteractionDetailsItem(self.__values, offset) 
  
    def __contains__(self, vehicleID): 
        return vehicleID in self.__offsets 
  
    def __str__(self): 
        return str(self.toDict()) 
  
    def pack(self): 
        count = len(self.__vehicleIDs) 
        packed = struct.pack(('<%dI' % count), *self.__vehicleIDs) + struct.pack(('<' + VEH_INTERACTION_DETAILS_LAYOUT * count), *self.__values) 
  
        return packed 
  
    def toDict(self): 
        return dict([ (vehID, dict(_VehicleInteractionDetailsItem(self.__values, offset))) for vehID, offset in self.__offsets.iteritems() ]) 
  
class _VehicleInteractionDetailsItem_LEGACY(object): 
  
    def __init__(self, values, offset): 
        self.__values = values 
        self.__offset = offset 
  
    def __getitem__(self, key): 
        return self.__values[self.__offset + VEH_INTERACTION_DETAILS_INDICES_LEGACY[key]] 
  
    def __setitem__(self, key, value): 
        self.__values[self.__offset + VEH_INTERACTION_DETAILS_INDICES_LEGACY[key]] = min(int(value), 65535) 
  
    def __str__(self): 
        return str(dict(self)) 
  
    def __iter__(self): 
        return izip(VEH_INTERACTION_DETAILS_LEGACY, self.__values[self.__offset:]) 
  
  
class VehicleInteractionDetails_LEGACY(object): 
  
    def __init__(self, vehicleIDs, values): 
        self.__vehicleIDs = vehicleIDs 
        self.__values = values 
        size = len(VEH_INTERACTION_DETAILS_LEGACY) 
        self.__offsets = dict(((x[1], x[0] * size) for x in enumerate(self.__vehicleIDs))) 
  
    @staticmethod
    def fromPacked(packed): 
        size = len(VEH_INTERACTION_DETAILS_LEGACY) 
        count = len(packed) / struct.calcsize('I%dH' % size) 
        unpacked = struct.unpack('%dI%dH' % (count, count * size), packed) 
        vehicleIDs = unpacked[:count] 
        values = unpacked[count:] 
        return VehicleInteractionDetails_LEGACY(vehicleIDs, values) 
  
    def __getitem__(self, vehicleID): 
        offset = self.__offsets.get(vehicleID, None) 
        if offset is None: 
            self.__vehicleIDs.append(vehicleID) 
            offset = len(self.__values) 
            size = len(VEH_INTERACTION_DETAILS_LEGACY) 
            self.__values += [0] * size 
            self.__offsets[vehicleID] = offset 
        return _VehicleInteractionDetailsItem_LEGACY(self.__values, offset) 
  
    def __contains__(self, vehicleID): 
        return vehicleID in self.__offsets 
  
    def __str__(self): 
        return str(self.toDict()) 
  
    def pack(self): 
        count = len(self.__vehicleIDs) 
        size = len(VEH_INTERACTION_DETAILS_LEGACY) 
        packed = struct.pack(('%dI' % count), *self.__vehicleIDs) + struct.pack(('%dH' % count * size), *self.__values) 
        return packed 
  
    def toDict(self): 
        return dict([ (vehID, dict(_VehicleInteractionDetailsItem_LEGACY(self.__values, offset))) for vehID, offset in self.__offsets.iteritems() ]) 
  

class SafeUnpickler(object):
	# IRONPYTHON MODIFIED: not really in use, method just performs normal cPicle.Unpickler
	
	#PICKLE_SAFE = {'DamageEvents', 'collections'}

	#@classmethod
	#def find_class(cls, module, name):
	#	if not module in cls.PICKLE_SAFE:
	#		raise cPickle.UnpicklingError('Attempting to unpickle unsafe module %s' % module)
		
	#	__import__(module)
	#	mod = sys.modules[module]
			
	#	if not name in cls.PICKLE_SAFE[module]:
	#		raise cPickle.UnpicklingError('Attempting to unpickle unsafe class %s' % name)
			
	#	klass = getattr(mod, name)
	#	return klass

	@classmethod
	def loads(cls, pickle_string):
		try:
			safeUnpickler = cPickle.Unpickler(StringIO.StringIO(pickle_string))
			# IRONPYTHON MODIFIED: added cPicler and StringIO instead of SafePicler
			#safeUnpickler.find_global = cls.find_class
			return safeUnpickler.load()
		except Exception, e:
			# IRONPYTHON MODIFIED: close dossier input file
			cachefile.close()
			raise cPickle.UnpicklingError('Unpickler Error: ' + e.message)
			
	@classmethod
	def load(cls, pickle_file):
		try:
			safeUnpickler = cPickle.Unpickler(pickle_file)
			# IRONPYTHON MODIFIED: added cPicler and StringIO instead of SafePicler
			#safeUnpickler.find_global = cls.find_class
			return safeUnpickler.load()
		
		except EOFError, er:
			# IRONPYTHON MODIFIED: close dossier input file
			cachefile.close()
			raise cPickle.UnpicklingError('Unpickler EOF Error')
		
		except Exception, e:
			# IRONPYTHON MODIFIED: close dossier input file
			cachefile.close()
			raise cPickle.UnpicklingError('Unpickler Error')

if __name__ == '__main__': 
    main() 
