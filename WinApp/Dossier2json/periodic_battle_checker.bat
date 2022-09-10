@echo off

:init
timeout /T 60 /NOBREAK

pushd .
call battle_files_retriever.bat
popd

goto init