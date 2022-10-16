@echo off
SET WOTBR2J_FOLDER=%~dp0
SET DATFILES_BACKUP_DIR=%APPDATA%\Wot Numbers\BattleResultsDATBackup
SET BATTLE_RESULTS_DST_DATA_FOLDER=%APPDATA%\Wot Numbers\BattleResult

if not exist "%DATFILES_BACKUP_DIR%" (
	goto :end
)

cd "%DATFILES_BACKUP_DIR%"
for %%f in (*.dat) do (
	copy /Y "%%f" "%BATTLE_RESULTS_DST_DATA_FOLDER%"
)

:end