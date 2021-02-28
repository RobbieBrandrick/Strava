Root=./..
CronFolder=/usr/bin/Strava
Deployment=$Root/Deployment/*

echo "Setting Up Cron Folder"

if [ -d $CronFolder ]; then
    
    rm -r $CronFolder

fi

mkdir $CronFolder

cp -r $Deployment $CronFolder

echo "Configuration Database Symbolic Link"

StravaDatabase=$CronFolder/bin/Database/strava.db
ImportToDatabaseTo=$CronFolder/bin/ImportToDatabase

if [ -L $ImportToDatabaseTo/strava.db ]; then
    rm $ImportToDatabaseTo/strava.db
fi

ln -s $StravaDatabase $ImportToDatabaseTo

ExportToGoogleSheetTo=$CronFolder/bin/ExportToGoogleSheet

if [ -L $ExportToGoogleSheetTo/strava.db ]; then
    rm $ExportToGoogleSheetTo/strava.db
fi

ln -s $StravaDatabase $ExportToGoogleSheetTo

cp ./CronStravaProcess.sh $CronFolder/

chown -R rob $CronFolder
chmod -R 777 $CronFolder

echo "Cron job folder has been set up"