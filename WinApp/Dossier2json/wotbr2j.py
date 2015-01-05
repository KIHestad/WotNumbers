################################################### 
# World of Tanks Battle Result to JSON            # 
# by Phalynx www.vbaddict.net                     # 
#                                                 #
# Modified to run from c# using IronPhyton        #
# Edited version by BadButton -> 2014-12-16       #
###################################################
# IRONPYTHON MODIFIED: added cPicler and StringIO instead of SafePicler
import struct, json, time, sys, os, cPickle, StringIO

from itertools import izip 
  
# BONUS_TYPE_NAMES = ('regular', 'training', 'company', 'tournament', 'clan', 'tutorial', 'cybersport', 'historical', 'event_battles', 'sortie') 
BONUS_TYPE_NAMES = ('regular', 'training', 'company', 'tournament', 'clan', 'tutorial', 'cybersport', 'historical', 'event_battles', 'sortie', 'fort_battle')
FINISH_REASON_NAMES = ('extermination', 'base', 'timeout', 'failure', 'technical') 

VERSIONS_LENGTH = (50, 52, 60, 62, 68, 70, 74, 81, 84, 92, 117, 90)
VERSIONS = dict(((value, index+1) for index, value in enumerate(VERSIONS_LENGTH)))

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
  
 
  
def usage(): 
	print str(sys.argv[0]) + " battleresult.dat [options]"
	print 'Options:'
	print '-f Formats the JSON to be more human readable'
	print '-s Server Mode, disable writing of timestamp, enable logging'


def main(): 
	import struct, json, time, sys, os, shutil, datetime
	global numofkills, filename_source, filename_target, option_server, option_format
	
	parserversion = "0.9.5.0"
	
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

	printmessage('###### WoTBR2J ' + parserversion + ' BATTLE FILE CONVERT TO JSON', 0) 
	printmessage('Time: ' + str(datetime.datetime.now()), 0) 
	printmessage('Encoding: ' + str(sys.getdefaultencoding()) + ' - ' + str(sys.getfilesystemencoding()), 0)
	printmessage('Processing ' + filename_source, 0) 
	if option_server == 0: 
		tanksdata = get_json_data("tanks.json") 
		mapdata = get_json_data("maps.json") 
		
	if not os.path.exists(filename_source) or not os.path.isfile(filename_source) or not os.access(filename_source, os.R_OK): 
		exitwitherror('Battle Result does not exists!') 
		
	filename_target = os.path.splitext(filename_source)[0] 
	filename_target = filename_target + '.json'
		
	cachefile = open(filename_source, 'rb') 
				  
	try: 
		# IRONPYTHON MODIFIED: no use if SafeUnpickler
		#from os.path import SafeUnpickler
		battleresultversion, battleResults = SafeUnpickler.load(cachefile) 
	except Exception, e: 
		exitwitherror('Battle Result cannot be read (pickle could not be read) ' + e.message) 
		
	if not 'battleResults' in locals(): 
		exitwitherror('Battle Result cannot be read (battleResults does not exist)') 

	if len(battleResults[1]) in VERSIONS_LENGTH:
		battleresultversion = VERSIONS[len(battleResults[1])]
	else:
		exitwitherror("Unknown Version, length: " + str(len(battleResults[1])))
		
	printmessage("Processing Version: " + str(battleresultversion), 0)
	bresult = convertToFullForm(battleResults) 
		
	if not 'personal' in bresult:
		exitwitherror('Battle Result cannot be read (personal does not exist)')
		
	tanksource = bresult['personal']['typeCompDescr'] 
	bresult['personal']['tankID'] = tanksource >> 8 & 65535
	bresult['personal']['countryID'] = tanksource >> 4 & 15
		  
	if option_server == 0: 
		bresult['personal']['tankName'] = get_tank_data(tanksdata, bresult['personal']['countryID'], bresult['personal']['tankID'], "title") 

	bresult['personal']['won'] = True if bresult['common']['winnerTeam'] == bresult['personal']['team'] else False
	bresult['personal']['achievementlist'] = list()
	import DamageEvents
		
	for key, value in bresult['vehicles'].items(): 
		
		if len(battleResults[1]) < 60: 
			bresult['vehicles'][key]['details'] = VehicleInteractionDetails_LEGACY.fromPacked(value['details']).toDict() 
			
		if len(battleResults[1]) == 60: 
			bresult['vehicles'][key]['details'] = VehicleInteractionDetails.fromPacked(value['details']).toDict() 

			for vehicleid, detail_values in bresult['vehicles'][key]['details'].items(): 
				bresult['vehicles'][key]['details'][vehicleid]['critsDestroyedTankmenList'] = getDestroyedTankmen(detail_values)
				bresult['vehicles'][key]['details'][vehicleid]['critsCriticalDevicesList'] = getCriticalDevicesList(detail_values)
				bresult['vehicles'][key]['details'][vehicleid]['critsDestroyedDevicesList'] = getDestroyedDevicesList(detail_values)
				bresult['vehicles'][key]['details'][vehicleid]['critsCount'] = len(bresult['vehicles'][key]['details'][vehicleid]['critsDestroyedTankmenList']) + len(bresult['vehicles'][key]['details'][vehicleid]['critsDestroyedTankmenList']) + len(bresult['vehicles'][key]['details'][vehicleid]['critsDestroyedTankmenList'])

		bresult['vehicles'][key]['damage_events'] = []
		if 'damage_event_list' in bresult['vehicles'][key]:
			for dmg_list in bresult['vehicles'][key]['damage_event_list']:
				dmg_vehicleid, dmg_typecomp = dmg_list
				for x in bresult['vehicles'][key]['damage_event_list'][dmg_list]:
					dmg = dict(zip(x._fields, list(x))) 
					dmg['typeComp'] = dmg_typecomp
					dmg['tankID'] = dmg_typecomp >> 8 & 65535
					dmg['countryID'] = dmg_typecomp >> 4 & 15
					dmg['vehicleid'] = dmg_vehicleid
			bresult['vehicles'][key]['damage_events'].append(dmg)
			del(bresult['vehicles'][key]['damage_event_list'])
		tanksource = bresult['vehicles'][key]['typeCompDescr'] 
			  
		if tanksource == None: 
			bresult['vehicles'][key]['tankID'] = -1
			bresult['vehicles'][key]['countryID'] = -1
			bresult['vehicles'][key]['tankName'] = 'unknown'
		else:    
			bresult['vehicles'][key]['tankID'] = tanksource >> 8 & 65535
			bresult['vehicles'][key]['countryID'] = tanksource >> 4 & 15
				  
			if option_server == 0:   
				bresult['vehicles'][key]['tankName'] = get_tank_data(tanksdata, bresult['vehicles'][key]['countryID'], bresult['vehicles'][key]['tankID'], "title") 
			else: 
				bresult['vehicles'][key]['tankName'] = "-"
		
	for key, value in bresult['players'].items(): 
		bresult['players'][key]['platoonID'] = bresult['players'][key]['prebattleID'] 
		del bresult['players'][key]['prebattleID'] 
			  
		for vkey, vvalue in bresult['vehicles'].items(): 
			if bresult['vehicles'][vkey]['accountDBID'] == key: 
				bresult['players'][key]['vehicleid'] = vkey 
				break

	bresult['common']['bonusTypeName'] = BONUS_TYPE_NAMES[bresult['common']['bonusType']-1]
		
	gameplayID = bresult['common']['arenaTypeID'] >> 16
	mapID = ((bresult['common']['arenaTypeID'] & 32767))


	bresult['common']['gameplayID'] = gameplayID 
	bresult['common']['gameplayName'] = "" 
	bresult['common']['arenaTypeID'] = mapID 
		  
	if option_server == 0: 
		bresult['common']['arenaTypeName'] = get_map_data(mapdata, mapID, "mapname") 
		bresult['common']['arenaTypeIcon'] = get_map_data(mapdata, mapID, "mapidname") 
			  
	bresult['parser'] = 'http://www.vbaddict.net'
	bresult['parserversion'] = parserversion 
	bresult['parsertime'] = time.mktime(time.localtime()) 
	bresult['common']['arenaCreateTimeH'] = datetime.datetime.fromtimestamp(int(bresult['common']['arenaCreateTime'])).strftime('%Y-%m-%d %H:%M:%S') 
	bresult['battleresultversion'] = battleresultversion 
	bresult['common']['finishReasonName'] = FINISH_REASON_NAMES[bresult['common']['finishReason']-1] 
	
	bresult['common']['result'] = 'ok'
		  
	dumpjson(bresult) 
		
	printmessage('###### Done!', 0) 
	printmessage('', 0) 

	# IRONPYTHON MODIFIED: close dossier input file
	cachefile.close()
	# IRONPYTHON MODIFIED: no need for exit, throws error when calling sys.exit
	#sys.exit(0)

def exitwitherror(message): 
	printmessage(message, 1) 
	dossierheader = dict() 
	dossierheader['common'] = dict() 
	dossierheader['common']['result'] = "error"
	dossierheader['common']['message'] = message 
	dumpjson(dossierheader) 
	sys.exit(1) 

def dumpjson(bresult): 
	global option_server, option_format, filename_target 
	  
	if option_server == 1: 
		print json.dumps(bresult)    
	else: 
		finalfile = open(filename_target, 'w') 
		if option_format == 1: 
			finalfile.write(json.dumps(bresult, sort_keys=True, indent=4)) 
		else: 
			finalfile.write(json.dumps(bresult)) 
		# IRONPYTHON MODIFIED: close dossier input file
		finalfile.close()

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




def convertToFullForm(compactForm): 
		  
	#print len(compactForm[1]) 
	
	handled = 0
	
	if len(compactForm[1])==50: 
		VEH_CELL_RESULTS = ('health', 'credits', 'xp', 'shots', 'hits', 'he_hits', 'pierced', 'damageDealt', 'damageAssisted', 'damageReceived', 'shotsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'repair', 'freeXP', 'details', 'potentialDamageDealt', 'potentialDamageReceived', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'hasTHit') 
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS))) 
		VEH_BASE_RESULTS = VEH_CELL_RESULTS[:VEH_CELL_RESULTS.index('potentialDamageDealt')] + ('accountDBID', 'team', 'typeCompDescr', 'gold', 'xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'eventIndices', 'vehLockTimeFactor') + VEH_CELL_RESULTS[VEH_CELL_RESULTS.index('potentialDamageDealt'):] 
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS))) 
		VEH_PUBLIC_RESULTS = VEH_BASE_RESULTS[:VEH_BASE_RESULTS.index('xpPenalty')] 
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS))) 
		VEH_FULL_RESULTS = VEH_BASE_RESULTS[:VEH_BASE_RESULTS.index('eventIndices')] + ('tmenXP', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'isPremium', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps') 
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS))) 
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team') 
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO))) 
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'vehLockMode') 
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1

	if len(compactForm[1])==52: 
		VEH_CELL_RESULTS = ('health', 'credits', 'xp', 'shots', 'hits', 'thits', 'he_hits', 'pierced', 'damageDealt', 'damageAssisted', 'damageReceived', 'shotsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'repair', 'freeXP', 'details', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog') 
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS))) 
		VEH_BASE_RESULTS = VEH_CELL_RESULTS[:VEH_CELL_RESULTS.index('potentialDamageDealt')] + ('accountDBID', 'team', 'typeCompDescr', 'gold', 'xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'eventIndices', 'vehLockTimeFactor') + VEH_CELL_RESULTS[VEH_CELL_RESULTS.index('potentialDamageDealt'):] 
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS))) 
		VEH_PUBLIC_RESULTS = VEH_BASE_RESULTS[:VEH_BASE_RESULTS.index('xpPenalty')] 
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS))) 
		VEH_ACCOUNT_RESULTS = ('tmenXP', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'isPremium', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps') 
		VEH_ACCOUNT_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_ACCOUNT_RESULTS))) 
		VEH_FULL_RESULTS = VEH_BASE_RESULTS[:VEH_BASE_RESULTS.index('eventIndices')] + VEH_ACCOUNT_RESULTS 
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS))) 
		VEH_ACCOUNT_RESULTS_START_INDEX = VEH_FULL_RESULTS_INDICES['tmenXP'] 
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team') 
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO))) 
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode') 
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS))) 
		handled = 1
		
	if len(compactForm[1])==60: 
		VEH_CELL_RESULTS = ('health', 'credits', 'xp', 'shots', 'hits', 'thits', 'he_hits', 'pierced', 'damageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'shotsReceived', 'noDamageShotsReceived', 'heHitsReceived', 'piercedReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'repair', 'freeXP', 'details', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects') 
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS))) 
		VEH_BASE_RESULTS = VEH_CELL_RESULTS[:VEH_CELL_RESULTS.index('potentialDamageDealt')] + ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason', 'xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'eventIndices', 'vehLockTimeFactor', 'misc') + VEH_CELL_RESULTS[VEH_CELL_RESULTS.index('potentialDamageDealt'):] 
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS))) 
		VEH_PUBLIC_RESULTS = VEH_BASE_RESULTS[:VEH_BASE_RESULTS.index('xpPenalty')] 
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS))) 
		VEH_ACCOUNT_RESULTS = ('originalCredits', 'originalXP', 'originalFreeXP', 'tmenXP', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'isPremium', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps') 
		VEH_ACCOUNT_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_ACCOUNT_RESULTS))) 
		VEH_FULL_RESULTS = VEH_BASE_RESULTS[:VEH_BASE_RESULTS.index('eventIndices')] + VEH_ACCOUNT_RESULTS 
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS))) 
		VEH_ACCOUNT_RESULTS_START_INDEX = VEH_FULL_RESULTS_INDICES['originalCredits'] 
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team') 
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO))) 
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode') 
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS))) 
		handled = 1
		
	if len(compactForm[1])==62:
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'shots', 'hits', 'thits', 'he_hits', 'pierced', 'damageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'shotsReceived', 'noDamageShotsReceived', 'heHitsReceived', 'piercedReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects', 'achievementCredits', 'achievementXP', 'achievementFreeXP')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1
		
	if len(compactForm[1])==68: 
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'hits', 'thits', 'he_hits', 'pierced', 'damageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'shotsReceived', 'noDamageShotsReceived', 'heHitsReceived', 'piercedReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1
		
	if len(compactForm[1])==70:  
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'hits', 'thits', 'he_hits', 'pierced', 'damageDealt', 'sniperDamageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'shotsReceived', 'noDamageShotsReceived', 'heHitsReceived', 'piercedReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('protoAchievements', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'creditsToDraw')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1 

	if len(compactForm[1])==74:  
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'directHits', 'directTeamHits', 'explosionHits', 'piercings', 'damageDealt', 'sniperDamageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'damageBlockedByArmor', 'directHitsReceived', 'noDamageDirectHitsReceived', 'explosionHitsReceived', 'piercingsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('protoAchievements', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'creditsToDraw')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'histAmmoCost', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID', 'marksOnGun', 'movingAvgDamage')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1 

		
	if len(compactForm[1])==81:  
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'directHits', 'directTeamHits', 'explosionHits', 'piercings', 'damageDealt', 'sniperDamageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'damageBlockedByArmor', 'directHitsReceived', 'noDamageDirectHitsReceived', 'explosionHitsReceived', 'piercingsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave', 'fortResource')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('protoAchievements', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'creditsToDraw')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'histAmmoCost', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID', 'marksOnGun', 'movingAvgDamage', 'damageRating', 'orderCredits', 'orderXP', 'orderTMenXP', 'orderFreeXP', 'orderFortResource')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS_SERVER = ('eventGoldByEventID',)
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode', 'sortieDivision')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1 
		
		
	if len(compactForm[1])==84:  
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'directHits', 'directTeamHits', 'explosionHits', 'piercings', 'damageDealt', 'sniperDamageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'damageBlockedByArmor', 'directHitsReceived', 'noDamageDirectHitsReceived', 'explosionHitsReceived', 'piercingsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tdestroyedModules', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave', 'fortResource')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('protoAchievements', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects', 'aimerSeries', 'committedSuicide')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason', 'fortBuilding')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'creditsToDraw')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas', 'clanDBID')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'histAmmoCost', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID', 'marksOnGun', 'movingAvgDamage', 'damageRating', 'orderCredits', 'orderXP', 'orderTMenXP', 'orderFreeXP', 'orderFortResource', 'fairplayViolations')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS_SERVER = ('eventGoldByEventID',)
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode', 'sortieDivision')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1 
	
	if len(compactForm[1])==92:  
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'directHits', 'directTeamHits', 'explosionHits', 'piercings', 'damageDealt', 'sniperDamageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'damageBlockedByArmor', 'directHitsReceived', 'noDamageDirectHitsReceived', 'explosionHitsReceived', 'piercingsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tdestroyedModules', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave', 'fortResource', 'damaged_while_moving', 'killed_while_moving', 'assistedDamage', 'assistedKills', 'critsByType', 'innerModuleCritCount')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('protoAchievements', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'damage_list_with_damage', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects', 'committedSuicide', 'discloseShots', 'guerrillaShots', 'critsCount', 'aimerSeries')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason', 'fortBuilding')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'creditsToDraw')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas', 'clanDBID')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'histAmmoCost', 'premiumVehicleXP', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID', 'marksOnGun', 'movingAvgDamage', 'damageRating', 'orderCredits', 'orderXP', 'orderTMenXP', 'orderFreeXP', 'orderFortResource', 'fairplayViolations', 'refSystemXPFactor10')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS_SERVER = ('eventGoldByEventID',)
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode', 'sortieDivision')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1 

	if len(compactForm[1])==117:  
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'directHits', 'directTeamHits', 'explosionHits', 'piercings', 'damageDealt', 'sniperDamageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'damageBlockedByArmor', 'directHitsReceived', 'noDamageDirectHitsReceived', 'explosionHitsReceived', 'piercingsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tdestroyedModules', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave', 'fortResource', 'critsByType', 'innerModuleCritCount', 'innerModuleDestrCount', 'isAnyOurCrittedModules', 'killsAssistedTrack', 'killsAssistedRadio', 'assistedDamage', 'assistedKills', 'killedAndDamagedByAllSquadmates', 'damagedWhileMoving', 'damagedWhileEnemyMoving', 'damagedVehicleCntAssistedTrack', 'damagedVehicleCntAssistedRadio', 'isNotSpotted', 'spottedBeforeWeBecameSpotted', 'isAnyHitReceivedWhileCapturing', 'spottedAndDamagedSPG', 'damageAssistedRadioWhileInvisible', 'damageAssistedTrackWhileInvisible', 'damage_event_list', 'multi_damage_events', 'inBattleMaxSniperSeries', 'inBattleMaxKillingSeries', 'inBattleMaxPiercingSeries', 'damageBeforeTeamWasDamaged', 'killsBeforeTeamWasDamaged', 'percentFromTotalTeamDamage', 'percentFromSecondBestDamage', 'firstBlood')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('protoAchievements', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects', 'committedSuicide', 'discloseShots', 'guerrillaShots', 'critsCount', 'aimerSeries')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason', 'fortBuilding')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'creditsToDraw')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas', 'clanDBID', 'vehsByClass')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'histAmmoCost', 'premiumVehicleXP', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'prevMarkOfMastery', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID', 'marksOnGun', 'movingAvgDamage', 'damageRating', 'orderCredits', 'orderXP', 'orderTMenXP', 'orderFreeXP', 'orderFortResource', 'fairplayViolations', 'refSystemXPFactor10', 'battleNum')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS_SERVER = ('eventGoldByEventID',)
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode', 'sortieDivision')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1 

	if len(compactForm[1])==90:  
		VEH_INTERACTION_DETAILS_NAMES = [ x[0] for x in VEH_INTERACTION_DETAILS ]
		VEH_INTERACTION_DETAILS_MAX_VALUES = dict(((x[0], x[2]) for x in VEH_INTERACTION_DETAILS))
		VEH_INTERACTION_DETAILS_INIT_VALUES = [ x[3] for x in VEH_INTERACTION_DETAILS ]
		VEH_INTERACTION_DETAILS_LAYOUT = ''.join([ x[1] for x in VEH_INTERACTION_DETAILS ])
		VEH_INTERACTION_DETAILS_INDICES = dict(((x[1][0], x[0]) for x in enumerate(VEH_INTERACTION_DETAILS)))
		_VEH_CELL_RESULTS_PUBLIC = ('health', 'credits', 'xp', 'achievementCredits', 'achievementXP', 'achievementFreeXP', 'shots', 'directHits', 'directTeamHits', 'explosionHits', 'piercings', 'damageDealt', 'sniperDamageDealt', 'damageAssistedRadio', 'damageAssistedTrack', 'damageReceived', 'damageBlockedByArmor', 'directHitsReceived', 'noDamageDirectHitsReceived', 'explosionHitsReceived', 'piercingsReceived', 'spotted', 'damaged', 'kills', 'tdamageDealt', 'tdestroyedModules', 'tkills', 'isTeamKiller', 'capturePoints', 'droppedCapturePoints', 'mileage', 'lifeTime', 'killerID', 'achievements', 'potentialDamageReceived', 'isPrematureLeave', 'fortResource', 'healedHP', 'healedHPByMe')
		_VEH_CELL_RESULTS_PRIVATE = ('repair', 'freeXP', 'details')
		_VEH_CELL_RESULTS_SERVER = ('protoAchievements', 'potentialDamageDealt', 'soloHitsAssisted', 'isEnemyBaseCaptured', 'stucks', 'autoAimedShots', 'presenceTime', 'spot_list', 'damage_list', 'kill_list', 'ammo', 'crewActivityFlags', 'series', 'tkillRating', 'tkillLog', 'destroyedObjects', 'committedSuicide', 'discloseShots', 'guerrillaShots', 'critsCount', 'aimerSeries', 'critsByType', 'innerModuleCritCount', 'innerModuleDestrCount', 'isAnyOurCrittedModules', 'killsAssistedTrack', 'killsAssistedRadio', 'assistedDamage', 'assistedKills', 'killedAndDamagedByAllSquadmates', 'damagedWhileMoving', 'damagedWhileEnemyMoving', 'damagedVehicleCntAssistedTrack', 'damagedVehicleCntAssistedRadio', 'isNotSpotted', 'spottedBeforeWeBecameSpotted', 'isAnyHitReceivedWhileCapturing', 'spottedAndDamagedSPG', 'damageAssistedRadioWhileInvisible', 'damageAssistedTrackWhileInvisible', 'damage_event_list', 'multi_damage_events', 'inBattleMaxSniperSeries', 'inBattleMaxKillingSeries', 'inBattleMaxPiercingSeries', 'damageBeforeTeamWasDamaged', 'killsBeforeTeamWasDamaged', 'percentFromTotalTeamDamage', 'percentFromSecondBestDamage', 'firstBlood')
		VEH_CELL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER
		VEH_CELL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_CELL_RESULTS)))
		_VEH_BASE_RESULTS_PUBLIC = ('accountDBID', 'team', 'typeCompDescr', 'gold', 'deathReason', 'fortBuilding')
		_VEH_BASE_RESULTS_PRIVATE = ('xpPenalty', 'creditsPenalty', 'creditsContributionIn', 'creditsContributionOut', 'creditsToDraw')
		_VEH_BASE_RESULTS_SERVER = ('eventIndices', 'vehLockTimeFactor', 'misc', 'cybersportRatingDeltas', 'clanDBID', 'vehsByClass')
		VEH_BASE_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + _VEH_CELL_RESULTS_SERVER + _VEH_BASE_RESULTS_SERVER
		VEH_BASE_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_BASE_RESULTS)))
		VEH_PUBLIC_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC
		VEH_PUBLIC_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_PUBLIC_RESULTS)))
		VEH_FULL_RESULTS_UPDATE = ('tmenXP', 'isPremium', 'eventCredits', 'eventGold', 'eventXP', 'eventFreeXP', 'eventTMenXP', 'autoRepairCost', 'autoLoadCost', 'autoEquipCost', 'histAmmoCost', 'premiumVehicleXP', 'premiumXPFactor10', 'premiumCreditsFactor10', 'dailyXPFactor10', 'igrXPFactor10', 'aogasFactor10', 'prevMarkOfMastery', 'markOfMastery', 'dossierPopUps', 'vehTypeLockTime', 'serviceProviderID', 'marksOnGun', 'movingAvgDamage', 'damageRating', 'orderCredits', 'orderXP', 'orderTMenXP', 'orderFreeXP', 'orderFortResource', 'fairplayViolations', 'refSystemXPFactor10', 'battleNum')
		VEH_FULL_RESULTS_UPDATE_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS_UPDATE)))
		_VEH_FULL_RESULTS_PRIVATE = ('originalCredits', 'originalXP', 'originalFreeXP', 'questsProgress')
		VEH_FULL_RESULTS_SERVER = ('eventGoldByEventID',)
		VEH_FULL_RESULTS = _VEH_CELL_RESULTS_PUBLIC + _VEH_BASE_RESULTS_PUBLIC + _VEH_CELL_RESULTS_PRIVATE + _VEH_BASE_RESULTS_PRIVATE + VEH_FULL_RESULTS_UPDATE + _VEH_FULL_RESULTS_PRIVATE
		VEH_FULL_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(VEH_FULL_RESULTS)))
		PLAYER_INFO = ('name', 'clanDBID', 'clanAbbrev', 'prebattleID', 'team', 'igrType')
		PLAYER_INFO_INDICES = dict(((x[1], x[0]) for x in enumerate(PLAYER_INFO)))
		COMMON_RESULTS = ('arenaTypeID', 'arenaCreateTime', 'winnerTeam', 'finishReason', 'duration', 'bonusType', 'guiType', 'vehLockMode', 'sortieDivision')
		COMMON_RESULTS_INDICES = dict(((x[1], x[0]) for x in enumerate(COMMON_RESULTS)))
		handled = 1 

	fullForm = dict()

	try:
		fullForm = {'arenaUniqueID': compactForm[0], 
		 'personal': listToDict(VEH_FULL_RESULTS, compactForm[1]), 
		 'common': {}, 
		 'players': {}, 
		 'vehicles': {}}
			
		fullForm['personal'] = keepCompatibility(fullForm['personal'])

	except Exception, e: 
		exitwitherror("Error processing Battle Result: Length: " + str(len(compactForm[1])) + ' ' + str(e.message))

	if handled == 0:
		exitwitherror("Unsupported Battle Result: Length: " + str(len(compactForm[1])))
	
	if not 'personal' in fullForm:
		return fullForm
		
	if 'details' in fullForm['personal']:

		if len(compactForm[1]) < 60: 
			fullForm['personal']['details'] = VehicleInteractionDetails_LEGACY.fromPacked(fullForm['personal']['details']).toDict() 
		else: 
			fullForm['personal']['details'] = VehicleInteractionDetails.fromPacked(fullForm['personal']['details']).toDict()             	
			if len(fullForm['personal']['details'])>0: 
				for vehicleid, detail_values in fullForm['personal']['details'].items(): 
					fullForm['personal']['details'][vehicleid]['critsDestroyedTankmenList'] = getDestroyedTankmen(detail_values)
					fullForm['personal']['details'][vehicleid]['critsCriticalDevicesList'] = getCriticalDevicesList(detail_values)
					fullForm['personal']['details'][vehicleid]['critsDestroyedDevicesList'] = getDestroyedDevicesList(detail_values)
					fullForm['personal']['details'][vehicleid]['critsCount'] = len(fullForm['personal']['details'][vehicleid]['critsDestroyedTankmenList']) + len(fullForm['personal']['details'][vehicleid]['critsDestroyedTankmenList']) + len(fullForm['personal']['details'][vehicleid]['critsDestroyedTankmenList'])
	
	# IRONPYTHON MODIFIED: added cPicler and StringIO instead of SafePicler
	#from SafeUnpickler import SafeUnpickler
	try:
		commonAsList, playersAsList, vehiclesAsList = SafeUnpickler.loads(compactForm[2]) 
	except Exception, e: 
		exitwitherror("Error processing Battle Result: Length: " + str(len(compactForm[1])) + ' ' + str(e.message))

	fullForm['common'] = listToDict(COMMON_RESULTS, commonAsList) 
	for accountDBID, playerAsList in playersAsList.iteritems(): 
		fullForm['players'][accountDBID] = listToDict(PLAYER_INFO, playerAsList) 

	for vehicleID, vehicleAsList in vehiclesAsList.iteritems(): 
		fullForm['vehicles'][vehicleID] = listToDict(VEH_PUBLIC_RESULTS, vehicleAsList) 
		fullForm['vehicles'][vehicleID] = keepCompatibility(fullForm['vehicles'][vehicleID])

	return fullForm 


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
def keepCompatibility(structureddata):

	# Compatibility with older versions
	# Some names changed in WoT 0.9.0
		
	if 'directHits' in structureddata:
		structureddata['hits'] = structureddata['directHits']
		
	if 'explosionHits' in structureddata:
		structureddata['he_hits'] = structureddata['explosionHits']
		
	if 'piercings' in structureddata:
		structureddata['pierced'] = structureddata['piercings']
				
	if 'piercingsReceived' in structureddata:
		structureddata['piercedReceived'] = structureddata['piercingsReceived']
				
	if 'explosionHitsReceived' in structureddata:
		structureddata['heHitsReceived'] = structureddata['explosionHitsReceived']
		
	if 'directHitsReceived' in structureddata:
		structureddata['shotsReceived'] = structureddata['directHitsReceived']
		
	if 'noDamageDirectHitsReceived' in structureddata:
		structureddata['noDamageShotsReceived'] = structureddata['noDamageDirectHitsReceived']
		

	return structureddata


def printmessage(logtext, to_log): 
	import datetime, os 

	global option_server, filename_source

	if option_server == 0: 
		print str(logtext)
		
	if to_log == 1:
		now = datetime.datetime.now() 
		message = str(now.strftime("%Y-%m-%d %H:%M:%S")) + " # WOTBR2J: " + str(logtext) + " # " + str(filename_source) + "\r\n"

		if option_server == 1: 
			logFile = open("/var/log/wotdc2j/wotdc2j.log", "a+b") 
			logFile.write(message) 
			logFile.close() 




def get_json_data(filename): 
	import json, time, sys, os 
	  
	os.chdir(sys.path[0]) 

	if not os.path.exists(filename) or not os.path.isfile(filename) or not os.access(filename, os.R_OK): 
		exitwitherror(filename + " does not exists!") 

	file_json = open(filename, 'r') 

	try: 
		file_data = json.load(file_json) 
	except Exception, e: 
		exitwitherror(filename + " cannot be loaded as JSON: " + e.message) 
		  
	file_json.close() 
	#print file_data 
	return file_data 



def get_tank_data(tanksdata, countryid, tankid, dataname): 
  
	for tankdata in tanksdata: 
		if tankdata['countryid'] == countryid: 
			if tankdata['tankid'] == tankid: 
				return tankdata[dataname] 

	return "unknown"

def get_map_data(mapsdata, mapid, dataname): 
  
	for mapdata in mapsdata: 
		if mapdata['mapid'] == mapid: 
				return mapdata[dataname] 

	return "unknown"


class _VehicleInteractionDetailsItem(object): 
  
	def __init__(self, values, offset): 
		self.__values = values 
		self.__offset = offset 

	def __getitem__(self, key): 
		return self.__values[self.__offset + VEH_INTERACTION_DETAILS_INDICES[key]] 

	def __setitem__(self, key, value): 
		self.__values[self.__offset + VEH_INTERACTION_DETAILS_INDICES[key]] = min(int(value), VEH_INTERACTION_DETAILS_MAX_VALUES[key]) 

	def __str__(self): 
		return str(dict(self)) 

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
	PICKLE_SAFE = {}

	@classmethod
	def find_class(cls, module, name):
		if not module in cls.PICKLE_SAFE:
			raise cPickle.UnpicklingError('Attempting to unpickle unsafe module %s' % module)
		
		__import__(module)
		mod = sys.modules[module]
			
		if not name in cls.PICKLE_SAFE[module]:
			raise cPickle.UnpicklingError('Attempting to unpickle unsafe class %s' % name)
			
		klass = getattr(mod, name)
		return klass

	@classmethod
	def loads(cls, pickle_string):
		try:
			safeUnpickler = cPickle.Unpickler(StringIO.StringIO(pickle_string))
			# IRONPYTHON MODIFIED: added cPicler and StringIO instead of SafePicler
			#safeUnpickler.find_global = cls.find_class
			return safeUnpickler.load()
		except Exception, e:
			raise cPickle.UnpicklingError('Unpickler Error')
			
	@classmethod
	def load(cls, pickle_file):
		try:
			safeUnpickler = cPickle.Unpickler(pickle_file)
			# IRONPYTHON MODIFIED: added cPicler and StringIO instead of SafePicler
			#safeUnpickler.find_global = cls.find_class
			return safeUnpickler.load()
		
		except EOFError, er:
			raise cPickle.UnpicklingError('Unpickler EOF Error')
		
		except Exception, e:
			raise cPickle.UnpicklingError('Unpickler Error')

if __name__ == '__main__': 
	main() 
