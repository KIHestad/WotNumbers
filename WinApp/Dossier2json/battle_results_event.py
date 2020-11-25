# uncompyle6 version 3.7.0
# Python bytecode 2.7 (62211)
# Decompiled from: Python 2.7.18 (v2.7.18:8d21aa21f2, Apr 20 2020, 13:19:08) [MSC v.1500 32 bit (Intel)]
# Embedded file name: scripts/common/battle_results/event.py
from battle_results_constants import BATTLE_RESULT_ENTRY_TYPE as ENTRY_TYPE
from DictPackers import ValueReplayPacker
BATTLE_RESULTS = [
 (
  'eventPoints', int, 0, None, 'sum', ENTRY_TYPE.VEHICLE_ALL),
 (
  'eventPointsLeft', int, 0, None, 'sum', ENTRY_TYPE.VEHICLE_ALL),
 (
  'eventPointsTotal', int, 0, None, 'sum', ENTRY_TYPE.VEHICLE_ALL),
 (
  'environmentID', int, 0, None, 'sum', ENTRY_TYPE.VEHICLE_ALL),
 (
  'eventLorePoints', int, 0, None, 'sum', ENTRY_TYPE.VEHICLE_ALL),
 (
  'commanderPoints', int, 0, None, 'sum', ENTRY_TYPE.VEHICLE_ALL),
 (
  'commanderLevelReached', bool, False, None, 'any', ENTRY_TYPE.VEHICLE_ALL),
 (
  'difficultyLevel', int, 0, None, 'any', ENTRY_TYPE.VEHICLE_ALL),
 (
  'eventAFKViolator', bool, False, None, 'skip', ENTRY_TYPE.VEHICLE_ALL),
 (
  'boosterApplied', bool, False, None, 'any', ENTRY_TYPE.VEHICLE_ALL),
 (
  'commanderTokenDelta', int, 0, None, 'any', ENTRY_TYPE.VEHICLE_ALL),
 (
  'commanderTokenCount', int, 0, None, 'any', ENTRY_TYPE.VEHICLE_ALL),
 (
  'commanderQuestBonusCount', int, 0, None, 'any', ENTRY_TYPE.VEHICLE_ALL),
 (
  'commanderCurrentLevel', int, 0, None, 'any', ENTRY_TYPE.VEHICLE_ALL),
 (
  'eventAFKBanned', bool, False, None, 'skip', ENTRY_TYPE.VEHICLE_ALL)]