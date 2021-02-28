Root=$(pwd)/..
Deployment=$Root/Deployment
Bin=$Deployment/bin

DatabaseTo=$Bin/Database/
DatabaseFrom=$Root/Database/
StravaDatabase=$DatabaseTo/strava.db

ImportToDatabaseTo=$Bin/ImportToDatabase/
ImportToDatabaseFrom=$Root/Strava.ImportToDatabase/bin/Debug/net5.0/

ExportToGoogleSheetTo=$Bin/ExportToGoogleSheet/
ExportToGoogleSheetFrom=$Root/Strava.ExportToGoogleSheet/bin/Debug/net5.0/

echo "Setting Up Deployment"

dotnet build $Root

echo "Recreate deployment folder if needed"
if [ -d $Deployment/ ]; then
    rm -r $Deployment/    
fi

mkdir $Deployment/

mkdir $Bin/

echo "Set up database"
mkdir $DatabaseTo
cp -r $DatabaseFrom* $DatabaseTo
    
echo "Set up ImportToDatabase"
mkdir $ImportToDatabaseTo
cp -r $ImportToDatabaseFrom* $ImportToDatabaseTo
ln -s $ImportToDatabaseTo/Strava.ImportToDatabase $Deployment/ImportToDatabase

echo "Set up ExportToGoogleSheet"
mkdir $ExportToGoogleSheetTo
cp -r $ExportToGoogleSheetFrom/* $ExportToGoogleSheetTo
ln -s $ExportToGoogleSheetTo/Strava.ExportToGoogleSheet $Deployment/ExportToGoogleSheet

if [ -L $ExportToGoogleSheetTo/strava.db ]; then
    rm $ExportToGoogleSheetTo/strava.db
fi

ln -s $StravaDatabase $ExportToGoogleSheetTo

echo "Deployment folder has been set up"