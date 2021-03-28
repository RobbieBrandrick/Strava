Root=$1
CronFolder=$2
User=$3
Deployment=$Root/Deployment/*

echo "Setting Up Cron Folder"

if [ -d $CronFolder ]; then
    
    rm -r $CronFolder

fi

mkdir $CronFolder

cp -r $Deployment $CronFolder
cp ./CrontabStravaProcess.sh $CronFolder/

echo "Configuration Database Symbolic Link"

StravaDatabase=$CronFolder/bin/Database/strava.db

ImportToDatabaseTo=$CronFolder/bin/ImportToDatabase
ln -s -f $StravaDatabase $ImportToDatabaseTo

ExportToGoogleSheetTo=$CronFolder/bin/ExportToGoogleSheet
ln -s -f $StravaDatabase $ExportToGoogleSheetTo

echo "Set up cron folder permissions"

chown -R $User $CronFolder
chmod -R 777 $CronFolder

echo "Cron job folder has been set up"