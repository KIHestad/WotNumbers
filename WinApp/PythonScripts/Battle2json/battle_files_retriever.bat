@echo off
SET BATCH_DRIVE=%~d0
SET BATCH_FOLDER=%~dp0
SET PROCESS_FILE=%BATCH_FOLDER%\process_battle_results_directory.bat

rem SET BATTLE_RESULTS_SRC_DATA_FOLDER=D:\temp\BadButton-wot-battle-parser\battle_files\WoT_1.18.0.0
SET BATTLE_RESULTS_SRC_DATA_FOLDER=%APPDATA%\Wargaming.net\WorldOfTanks\battle_results
SET BATTLE_RESULTS_SRC_DATA_DRIVE=%BATTLE_RESULTS_SRC_DATA_FOLDER:~0,1%

SET DATFILES_BACKUP_DIR=%APPDATA%\Wot Numbers\BattleResultsDATBackup

if not exist "%DATFILES_BACKUP_DIR%" (
	mkdir "%DATFILES_BACKUP_DIR%"
)

%BATTLE_RESULTS_SRC_DATA_DRIVE%:
pushd "%BATTLE_RESULTS_SRC_DATA_FOLDER%"

for /d %%d in (*) do (
	call "%PROCESS_FILE%" "%%d"
)

popd
%BATCH_DRIVE%
