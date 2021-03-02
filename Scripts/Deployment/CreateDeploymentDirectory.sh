Root=$1
Deployment=$Root/Deployment
Bin=$Deployment/bin

DatabaseTo=$Bin/Database/
DatabaseFrom=$Root/Database/

ImportToDatabaseTo=$Bin/ImportToDatabase/
ImportToDatabaseFrom=$Root/Strava.ImportToDatabase/bin/Debug/net5.0/

ExportToGoogleSheetTo=$Bin/ExportToGoogleSheet/
ExportToGoogleSheetFrom=$Root/Strava.ExportToGoogleSheet/bin/Debug/net5.0/

echo "Setting Up Deployment"

dotnet build "$Root"

echo "Recreate deployment folder if needed"
if [ -d "$Deployment"/ ]; then
    rm -r "${Deployment:?}/"    
fi

mkdir "$Deployment/"

mkdir "$Bin/"

echo "Set up database"
mkdir "$DatabaseTo"
cp -r "$DatabaseFrom"* "$DatabaseTo"
    
echo "Set up ImportToDatabase"
mkdir "$ImportToDatabaseTo"
cp -r "$ImportToDatabaseFrom"* "$ImportToDatabaseTo"

echo "Set up ExportToGoogleSheet"
mkdir "$ExportToGoogleSheetTo"
cp -r "$ExportToGoogleSheetFrom"/* "$ExportToGoogleSheetTo"

echo "Deployment folder has been set up"