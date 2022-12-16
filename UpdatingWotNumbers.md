How to update the application after a new WoT patch/update

- Most times we'll have to deal changing the battle2json files. (Remember to update the parser[version] in wotbr2j.py to the one of the game) 	
- There could be something new that need to be added to the user data base, like when a new map is added.
Add these to DbVersion.cs, increase version number in the ExpectedNumber field.

- If new tanks are added to the game, we can use the Admin tool to download and update the Admin.db database.
- Increase application version number (we need this to deploy a new binary and let the users know there is a new updated version)
Go to assembly info and update AssemblyVersion / AssemblyFileVersion.
Go to InstallerWix3 project, open WotNumbersLicense.rtf and modify version number there.

- Remember to update log files to keep track of changes which is optional but good praxis.
- Rebuild the solution in Release.
- Copy InstallerWix3/bin/Release/WotNumbersSetup.msi to LatestRelease directory
- Rename WotNumbersSetup.msi to WotNumbersSetup_VERSION_DATE.msi
if VERSION is 1.0.4 and DATE is 20-Oct-2022 the name would become WotNumbersSetup

- You can create an MD5 from this file. It's not mandatory  but good praxis.
- Update date and version number in VersionSettings.json file. (Date is in YYYY-MM-DD format)
- Upload to GitHub.
