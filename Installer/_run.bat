echo BIN
"C:\Program Files (x86)\WiX Toolset v3.8\bin\heat.exe" dir ..\WinApp\Bin\Debug -gg -srd -dr INSTALLFOLDER_Bin -var var.BinSource -cg ProductComponents_Bin -o _Bin.wxs
echo LIB
"C:\Program Files (x86)\WiX Toolset v3.8\bin\heat.exe" dir ..\WinApp\Lib -gg -srd -dr INSTALLFOLDER_Lib -var var.LibSource -cg ProductComponents_Lib -o _Lib.wxs
echo D2J
"C:\Program Files (x86)\WiX Toolset v3.8\bin\heat.exe" dir ..\WinApp\Bin\Debug\Dossier2json -gg -srd -dr INSTALLFOLDER_Dossier2json -var var.D2jSource -cg ProductComponents_Dossier2json -o _D2j.wxs