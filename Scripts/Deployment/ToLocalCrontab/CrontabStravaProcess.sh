root=/usr/bin/Strava
log=$root/log

echo "Starting import" >> $log

cd $root/bin/ImportToDatabase/ || exit

./Strava.ImportToDatabase >> $log 2>&1

echo "Starting export to sheet" >> $log

cd $root/bin/ExportToGoogleSheet/ || exit

./Strava.ExportToGoogleSheet >> $log 2>&1

echo "Finished" >> $log