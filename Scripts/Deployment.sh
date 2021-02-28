Root=./..
Deployment=$Root/Deployment
Bin=$Deployment/bin

DatabaseTo=$Bin/Database/
DatabaseFrom=$Root/Database/
StravaDatabase=$DatabaseTo/strava.db

ImportToDatabaseTo=$Bin/ImportToDatabase/
ImportToDatabaseFrom=$Root/Strava.ImportToDatabase/bin/Debug/net5.0/

ExportToGoogleSheetTo=$Bin/ExportToGoogleSheet/
ExportToGoogleSheetFrom=$Root/Strava.ExportToGoogleSheet/bin/Debug/net5.0/


dotnet build $Root

#Recreate deployment folder if needed
if [ -d $Deployment/ ]; then
    rm -r $Deployment/    
fi

mkdir $Deployment/

mkdir $Bin/

#Database
mkdir $DatabaseTo
cp -r $DatabaseFrom* $DatabaseTo
    
#ImportToDatabase
mkdir $ImportToDatabaseTo
cp -r $ImportToDatabaseFrom* $ImportToDatabaseTo
ln -s $ImportToDatabaseTo/Strava.ImportToDatabase $Deployment/ImportToDatabase
ln -s $StravaDatabase $ImportToDatabaseTo

#ExportToGoogleSheet
mkdir $ExportToGoogleSheetTo
cp -r $ExportToGoogleSheetFrom/* $ExportToGoogleSheetTo
ln -s $ExportToGoogleSheetTo/Strava.ExportToGoogleSheet $Deployment/ExportToGoogleSheet
ln -s $StravaDatabase $ExportToGoogleSheetTo