###################################################
# World of Tanks Dossier Cache to JSON            #
# Initial version by Phalynx www.vbaddict.net/wot #
#                                                 #
# Modified to run from c# using IronPhyton        #
# Edited version by BadButton -> 2015-11-20      #
###################################################

import struct, json, time, sys, os
	
def usage():
	print str(sys.argv[0]) + " dossierfilename.dat [options]"
	print 'Options:'
	print '-f Formats the JSON to be more human readable'
	print '-r Export all fields with their values and recognized names'
	print '-k Dont export Frags'
	print '-s Server Mode, disable writing of timestamp, enable logging (vBAddict mode)'

def main():
	
	import struct, json, time, sys, os, shutil, datetime, base64, cPickle

	parserversion = "1.9.0"
	wotversion = "1.18.1.0"
	
	global rawdata, tupledata, data, structures, numoffrags, working_directory
	global filename_source, filename_target
	global option_format, option_frags, option_raw
	
	filename_source = '%s\\Wot Numbers\\dossier.dat' %  os.environ['APPDATA']
	option_raw = 0 # Set to 1 and the JSON will contain all fields with their values and recognized names
	option_format = 1 # Set to 1 and the JSON will be formatted for better human readability
	option_frags = 0 # Set to 1 and the JSON will contain Kills/Frags
		
	printmessage('') 
	printmessage('###### WoTDC2J ' + parserversion + ' DOSSIER FILE CONVERT TO JSON ######')
	printmessage('Last modified for WoT version: ' + wotversion)
	printmessage('Time: ' + str(datetime.datetime.now())) 
	printmessage('Encoding: ' + str(sys.getdefaultencoding()) + ' - ' + str(sys.getfilesystemencoding()))
	working_directory = os.path.dirname(os.path.realpath(__file__))
	
	# IRONPYTHON MODIFIED: select current path as working directory
	os.chdir(working_directory)

	printmessage('Processing ' + filename_source)	

	if not os.path.exists(filename_source) or not os.path.isfile(filename_source) or not os.access(filename_source, os.R_OK):
		catch_fatal('Dossier file does not exists')
		sys.exit(1)

	if os.path.getsize(filename_source) == 0:
		catch_fatal('Dossier file size is zero')
		sys.exit(1)
		
	filename_target = os.path.splitext(filename_source)[0]
	filename_target = filename_target + '.json'

	if os.path.exists(filename_target) and os.path.isfile(filename_target) and os.access(filename_target, os.R_OK):
		try:
			os.remove(filename_target)
		except:
			catch_fatal('Cannot remove target file ' + filename_target)

	try:		
	    cachefile = open(filename_source, 'rb')
	except Exception, e:
		exitwitherror('Could not open dossier file: ' + filename_source + ' Error: ' + e.message)

	try:
		# removed safeunpicler - from SafeUnpickler import SafeUnpickler
		dossierversion, dossierCache = cPickle.load(cachefile)
	except Exception, e:
		# IRONPYTHON MODIFIED: close dossier input file
		cachefile.close()
		exitwitherror('Dossier cannot be read (pickle could not be read) ' + e.message)

	if not 'dossierCache' in locals():
		# IRONPYTHON MODIFIED: close dossier input file
		cachefile.close()
		exitwitherror('Dossier cannot be read (dossierCache does not exist)')

	printmessage("Dossier version " + str(dossierversion))
	
	tankitems = [(k, v) for k, v in dossierCache.items()]

	dossier = dict()
		
	dossierheader = dict()
	dossierheader['dossierversion'] = str(dossierversion)
	dossierheader['parser'] = 'http://wotnumbers.com'
	dossierheader['parserversion'] = parserversion
	dossierheader['tankcount'] = len(tankitems)
	dossierheader['date'] = time.mktime(time.localtime())
	
	structures = load_structures()
	
	tanks = dict()
	tanks_v2 = dict()
	
	battleCount_15 = 0
	battleCount_7 = 0
	battleCount_historical = 0
	battleCount_company = 0
	battleCount_clan = 0
	battleCount_fortBattles = 0
	battleCount_fortSorties = 0
	battleCount_rated7x7 = 0
	battleCount_globalMap = 0
	battleCount_fallout = 0

	for tankitem in tankitems:
		
		if len(tankitem) < 2:
			printmessage('Invalid tankdata')
			continue

		if len(tankitem[0]) < 2:
			printmessage('Invalid tankdata')
			continue
			
		rawdata = dict()
		
		try:
			data = tankitem[1][1]
		except Exception, e:
			printmessage('Invalid tankitem ' + str(e.message))
			continue
			
		tankstruct = str(len(data)) + 'B'
		tupledata = struct.unpack(tankstruct, data)
		tankversion = getdata("tankversion", 0, 1)
		
		#if tankversion != 87:
		#printmessage("Tankversion " + str(tankversion))
		#	continue
		
		if tankversion not in structures:
			printmessage('Unsupported tankversion ' + str(tankversion))
			continue				

		if not isinstance(tankitem[0][1], (int)):
			printmessage('Invalid tankdata')
			continue
	
		try:
			tankid = tankitem[0][1] #>> 8 & 65535
		except Exception, e:
			printmessage('cannot get tankid ' + e.message)
			continue
						
		try:
			countryid = tankitem[0][1] >> 4 & 15
		except Exception, e:
			printmessage('cannot get countryid ' + e.message)
			continue
			
		#For debugging purposes
		#if not (countryid==4 and tankid==19):
		#	continue
		
		for m in xrange(0,len(tupledata)):
			rawdata[m] = tupledata[m]
		
		if len(tupledata) == 0:
			continue

		fragslist = []
		if tankversion >= 65:
			tank_v2 = dict()
			# from tankversion 102 reduced number of structs, including only the once used by dossier parser defined in: 
			# SELECT DISTINCT [jsonSub], ''''+jsonSub +''', ' FROM json2dbMapping
			if tankversion in [107]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'uniqueAchievements',     'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon', 'fallout', 'maxFallout', 'falloutAchievements', 'ranked', 'maxRanked', 'rankedSeasons', 'a30x30', 'max30x30', 'epicBattle', 'maxEpicBattle', 'epicBattleAchievements', 'maxRankedSeason1', 'maxRankedSeason2', 'maxRankedSeason3', 'ranked_10x10' ,'maxRanked_10x10',
				'comp7Season1' ,'maxComp7Season1' )
			if tankversion in [105, 106]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'uniqueAchievements',     'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon', 'fallout', 'maxFallout', 'falloutAchievements', 'ranked', 'maxRanked', 'rankedSeasons', 'a30x30', 'max30x30', 'epicBattle', 'maxEpicBattle', 'epicBattleAchievements', 'maxRankedSeason1', 'maxRankedSeason2', 'maxRankedSeason3', 'ranked_10x10' ,'maxRanked_10x10' )
			if tankversion in [102, 103, 104]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'uniqueAchievements',     'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon', 'fallout', 'maxFallout', 'falloutAchievements', 'ranked', 'maxRanked', 'rankedSeasons', 'a30x30', 'max30x30', 'epicBattle', 'maxEpicBattle', 'epicBattleAchievements', 'maxRankedSeason1', 'maxRankedSeason2', 'maxRankedSeason3' )
			elif tankversion in [101]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon', 'fallout', 'maxFallout', 'falloutAchievements', 'ranked', 'maxRanked', 'rankedSeasons', 'a30x30', 'max30x30', 'epicBattle', 'maxEpicBattle', 'epicBattleAchievements')
			elif tankversion in [99]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon', 'fallout', 'maxFallout', 'falloutAchievements', 'ranked', 'maxRanked', 'rankedSeasons', 'a30x30', 'max30x30')
			elif tankversion in [97, 98]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon', 'fallout', 'maxFallout', 'falloutAchievements', 'ranked', 'maxRanked', 'rankedSeasons')
			elif tankversion in [94, 95, 96]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon', 'fallout', 'maxFallout', 'falloutAchievements')
			elif tankversion == 92:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7', 'globalMapCommon', 'maxGlobalMapCommon')
			elif tankversion in [88,89]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements', 'rated7x7', 'maxRated7x7')
			elif tankversion in [85, 87]:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements', 'singleAchievements', 'clanAchievements')
			elif tankversion == 81:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical', 'historicalAchievements', 'fortBattles', 'maxFortBattles', 'fortSorties', 'maxFortSorties', 'fortAchievements')
			elif tankversion == 77:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7', 'historical', 'maxHistorical')
			elif tankversion == 69:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7', 'playerInscriptions', 'playerEmblems', 'camouflages', 'compensation', 'achievements7x7')
			elif tankversion == 65:
				blocks = ('a15x15', 'a15x15_2', 'clan', 'clan2', 'company', 'company2', 'a7x7', 'achievements', 'frags', 'total', 'max15x15', 'max7x7')
			
			blockcount = len(list(blocks))+1

			newbaseoffset = (blockcount * 2)
			header = struct.unpack_from('<' + 'H' * blockcount, data)
			blocksizes = list(header[1:])
			blocknumber = 0
			numoffrags_list = 0
			numoffrags_a15x15 = 0
			numoffrags_a7x7 = 0
			numoffrags_historical = 0
			numoffrags_fortBattles = 0
			numoffrags_fortSorties = 0
			numoffrags_rated7x7 = 0
			numoffrags_globalMap = 0
			numoffrags_fallout = 0

			for blockname in blocks:

				if blocksizes[blocknumber] > 0:
					if blockname == 'frags':
						if option_frags == 1:
							fmt = '<' + 'IH' * (blocksizes[blocknumber]/6)
							fragsdata = struct.unpack_from(fmt, data, newbaseoffset)
							index = 0

							for i in xrange((blocksizes[blocknumber]/6)):
								compDescr, amount = (fragsdata[index], fragsdata[index + 1])
								numoffrags_list += amount	
								tankfrag = [compDescr, amount]
								fragslist.append(tankfrag)
								index += 2							

							for i in xrange((blocksizes[blocknumber])):
								rawdata[newbaseoffset+i] = str(tupledata[newbaseoffset+i]) + " / Frags"
								
							tank_v2['fragslist'] = fragslist
				
						newbaseoffset += blocksizes[blocknumber] 

						
					else:
						oldbaseoffset = newbaseoffset
						structureddata = getstructureddata(blockname, tankversion, newbaseoffset)
						structureddata = keepCompatibility(structureddata)
						newbaseoffset = oldbaseoffset+blocksizes[blocknumber]
						tank_v2[blockname] = structureddata 

				blocknumber +=1

			if contains_block('max15x15', tank_v2):
				if 'maxXP' in tank_v2['max15x15']:
					if tank_v2['max15x15']['maxXP']==0:
						tank_v2['max15x15']['maxXP'] = 1
						
				if 'maxFrags' in tank_v2['max15x15']:
					if tank_v2['max15x15']['maxFrags']==0:
						tank_v2['max15x15']['maxFrags'] = 1

				
			if contains_block('company', tank_v2):
				if 'battlesCount' in tank_v2['company']:
					battleCount_company += tank_v2['company']['battlesCount']
			
			if contains_block('clan', tank_v2):
				if 'battlesCount' in tank_v2['clan']:
					battleCount_company += tank_v2['clan']['battlesCount']

			if contains_block('a15x15', tank_v2):
				
				if 'battlesCount' in tank_v2['a15x15']:
					battleCount_15 += tank_v2['a15x15']['battlesCount']
					
				if 'frags' in tank_v2['a15x15']:
					numoffrags_a15x15 = int(tank_v2['a15x15']['frags'])

			if contains_block('a7x7', tank_v2):
				
				if 'battlesCount' in tank_v2['a7x7']:
					battleCount_7 += tank_v2['a7x7']['battlesCount']
				
				if 'frags' in tank_v2['a7x7']:
					numoffrags_a7x7 = int(tank_v2['a7x7']['frags'])
			
			if contains_block('historical', tank_v2):
				
				if 'battlesCount' in tank_v2['historical']:
					battleCount_historical += tank_v2['historical']['battlesCount']
				
				if 'frags' in tank_v2['historical']:
					numoffrags_historical = int(tank_v2['historical']['frags'])

			if contains_block('fortBattles', tank_v2):
				
				if 'battlesCount' in tank_v2['fortBattles']:
					battleCount_fortBattles += tank_v2['fortBattles']['battlesCount']
				
				if 'frags' in tank_v2['fortBattles']:
					numoffrags_fortBattles = int(tank_v2['fortBattles']['frags'])
					
			if contains_block('fortSorties', tank_v2):
				
				if 'battlesCount' in tank_v2['fortSorties']:
					battleCount_fortSorties += tank_v2['fortSorties']['battlesCount']
				
				if 'frags' in tank_v2['fortSorties']:
					numoffrags_fortSorties = int(tank_v2['fortSorties']['frags'])

			if contains_block('rated7x7', tank_v2):
				
				if 'battlesCount' in tank_v2['rated7x7']:
					battleCount_rated7x7 += tank_v2['rated7x7']['battlesCount']
				
				if 'frags' in tank_v2['rated7x7']:
					numoffrags_rated7x7 = int(tank_v2['rated7x7']['frags'])
			
			if contains_block('globalMapCommon', tank_v2):
				
				if 'battlesCount' in tank_v2['globalMapCommon']:
					battleCount_globalMap += tank_v2['globalMapCommon']['battlesCount']
				
				if 'frags' in tank_v2['globalMapCommon']:
					numoffrags_globalMap = int(tank_v2['globalMapCommon']['frags'])
				
			if contains_block('fallout', tank_v2):
				
				if 'battlesCount' in tank_v2['fallout']:
					battleCount_fallout += tank_v2['fallout']['battlesCount']
				
				if 'frags' in tank_v2['fallout']:
					numoffrags_fallout = int(tank_v2['fallout']['frags'])
				
			if option_frags == 1:

				try:
					if numoffrags_list <> (numoffrags_a15x15 + numoffrags_a7x7 + numoffrags_historical + numoffrags_fortBattles + numoffrags_fortSorties + numoffrags_rated7x7 + numoffrags_globalMap + numoffrags_fallout):
						pass
						#printmessage('Wrong number of frags for ' + str(tanktitle) + ', ' + str(tankversion) + ': ' + str(numoffrags_list) + ' = ' + str(numoffrags_a15x15) + ' + ' + str(numoffrags_a7x7) + ' + ' + str(numoffrags_historical) + ' + ' + str(numoffrags_fortBattles) + ' + ' + str(numoffrags_fortSorties) + ' + ' + str(numoffrags_rated7x7))
				except Exception, e:
						printmessage('Error processing frags: ' + e.message)
			
				
			tank_v2['common'] = {"countryid": countryid,
				"compactDescr": tankitem[0][1],
				"updated": tankitem[1][0],
				"updatedR": datetime.datetime.fromtimestamp(int(tankitem[1][0])).strftime('%Y-%m-%d %H:%M:%S'),
				"creationTime": tank_v2['total']['creationTime'],
				"creationTimeR": datetime.datetime.fromtimestamp(int(tank_v2['total']['creationTime'])).strftime('%Y-%m-%d %H:%M:%S'),
				"lastBattleTime": tank_v2['total']['lastBattleTime'],
				"lastBattleTimeR": datetime.datetime.fromtimestamp(int(tank_v2['total']['lastBattleTime'])).strftime('%Y-%m-%d %H:%M:%S'),
				"basedonversion": tankversion,
				"frags":  numoffrags_a15x15,
				"frags_7x7":  numoffrags_a7x7,
				"frags_historical":  numoffrags_historical,
				"frags_fortBattles":  numoffrags_fortBattles,
				"frags_fortSorties":  numoffrags_fortSorties,
				"frags_compare": numoffrags_list,
				"has_15x15": contains_block("a15x15", tank_v2),
				"has_7x7": contains_block("a7x7", tank_v2),
				"has_historical": contains_block("historical", tank_v2),
				"has_clan": contains_block("clan", tank_v2),
				"has_company": contains_block("company", tank_v2),
				"has_fort": contains_block("fortBattles", tank_v2),
				"has_sortie": contains_block("fortSorties", tank_v2)
							
			}
			
			if option_raw == 1:
				tank_v2['rawdata'] = rawdata

			tanks_v2[tankid] = tank_v2
			
	
	dossierheader['battleCount_15'] = battleCount_15	
	dossierheader['battleCount_7'] = battleCount_7
	dossierheader['battleCount_historical'] = battleCount_historical
	dossierheader['battleCount_company'] = battleCount_company
	dossierheader['battleCount_clan'] = battleCount_clan

	dossierheader['result'] = "ok"
	dossierheader['message'] = "ok"
	
	dossier['header'] = dossierheader
	dossier['tanks'] = tanks
	dossier['tanks_v2'] = tanks_v2

	dumpjson(dossier)

	# IRONPYTHON MODIFIED: close dossier input file
	cachefile.close()

	printmessage('###### Done!')

	# IRONPYTHON MODIFIED: no need for exit, throws error when calling sys.exit
	#sys.exit(0)
	
	
def get_current_working_path():
	#workaround for py2exe
	import sys, os
	
	try:
		if hasattr(sys, "frozen"):
			return os.path.dirname(unicode(sys.executable, sys.getfilesystemencoding( )))
		else:
			return sys.path[0]
	except Exception, e:
		print e.message

############################################################################################################################

def contains_block(blockname, blockdata):
	
	if blockname in blockdata:
		return 1
	return 0


def get_tank_details(compDescr):

	tankid = compDescr #>> 8 & 65535
	countryid = compDescr >> 4 & 15
	tankname = ""
	return countryid, tankid, tankname


def printmessage(message):
	print message


def exitwitherror(message):
	catch_fatal(message)
	dossier = dict()
	dossierheader = dict()
	dossierheader['result'] = "error"
	dossierheader['message'] = message
	dossier['header'] = dossierheader
	dumpjson(dossier)
	sys.exit(1)


def dumpjson(dossier):
	global option_format, filename_target
	
	try:
		
		finalfile = open(filename_target, 'w')
		
		if option_format == 1:
			finalfile.write(json.dumps(dossier, sort_keys=True, indent=4))
		else:
			finalfile.write(json.dumps(dossier))

		# IRONPYTHON MODIFIED: close dossier output file
		finalfile.close()

	except Exception, e:
		finalfile.close()
		printmessage(e)
		

def catch_fatal(message):
	import shutil
	printmessage(str(message))


def getstructureddata(category, tankversion, baseoffset=0):
	
	returndata = dict()
	
	if tankversion in structures:
		if category in structures[tankversion]:
			for item in structures[tankversion][category]:
				returndata[item['name']] = getdata(category + " " + item['name'], item['offset']+baseoffset, item['length'])
	
	return returndata


def keepCompatibility(structureddata):
	# Compatibility with older versions
	# Some names changed in WoT 0.9.0
		
	if 'directHits' in structureddata:
		structureddata['hits'] = structureddata['directHits']
		
	if 'explosionHits' in structureddata:
		structureddata['he_hits'] = structureddata['explosionHits']
		
	if 'piercings' in structureddata:
		structureddata['pierced'] = structureddata['piercings']
				
	if 'explosionHitsReceived' in structureddata:
		structureddata['heHitsReceived'] = structureddata['explosionHitsReceived']
		
	if 'directHitsReceived' in structureddata:
		structureddata['shotsReceived'] = structureddata['directHitsReceived']
		
	if 'noDamageDirectHitsReceived' in structureddata:
		structureddata['noDamageShotsReceived'] = structureddata['noDamageDirectHitsReceived']
		

	return structureddata




def get_json_data(filename):
	import json, time, sys, os
	

	# IRONPYTHON MODIFIED: removed setting path, use working directory (set in main)
	
	#os.chdir(current_working_path)
	
	if not os.path.exists(filename) or not os.path.isfile(filename) or not os.access(filename, os.R_OK):
		catch_fatal(filename + " does not exists!")
		sys.exit(1)

	file_json = open(filename, 'r')

	try:
		file_data = json.load(file_json)
	except Exception, e:
		catch_fatal(filename + " cannot be loaded as JSON: " + e.message)
		sys.exit(1)
		
		
	file_json.close()

	return file_data



def getdata(name, startoffset, offsetlength):
	global rawdata, tupledata, data

	if len(data)<startoffset+offsetlength:
		return 0
	
	structformat = 'H'

	if offsetlength==1:
		structformat = 'B'

	if offsetlength==2:
		structformat = 'H'
		
	if offsetlength==4:
		structformat = 'I'

	value = struct.unpack_from('<' + structformat, data, startoffset)[0]

	for x in range(0, offsetlength):
		rawdata[startoffset+x] = str(tupledata[startoffset+x]) + " / " + str(value) +  "; " + name

	
	return value


def load_structures():
	
	structures = dict()
	
	load_versions = [77,81,85,87,88,89,92,94,95,96,97,98,99,101,102,103,104,105,106,107];
	for version in load_versions:
		jsondata = get_json_data('structures_'+str(version)+'.json') # do not use sub folder for structures
		structures[version] = dict()
		for item in jsondata:
			category = item['category']
			if category not in structures[version]:
				structures[version][category] = list()
			structures[version][category].append(item)
	
	return structures


if __name__ == '__main__':
	main()
