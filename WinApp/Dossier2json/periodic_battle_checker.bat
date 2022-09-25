@echo off

:init

pushd .
call battle_files_retriever.bat
popd

timeout /T 60 /NOBREAK

goto init