@echo off
rem params: 
rem %1 directory to process

SET BATCH_FOLDER=%~dp0
SET PROCESS_FILE=%BATCH_FOLDER%\process_battle_results_file.bat

cd %1
for %%f in (*.dat) do (
	call "%PROCESS_FILE%" %%f
)
cd ..
