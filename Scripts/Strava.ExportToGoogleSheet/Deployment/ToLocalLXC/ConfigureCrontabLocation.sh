CronFolder=/usr/bin/Strava

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

#chown -R rob $CronFolder
chmod -R 777 $CronFolder