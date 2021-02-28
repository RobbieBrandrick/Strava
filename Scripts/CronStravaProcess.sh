echo "Starting import" > log

cd /usr/bin/Strava/bin/ImportToDatabase/

./Strava.ImportToDatabase

echo "Starting export to sheet" > log

cd /usr/bin/Strava/bin/ExportToGoogleSheet/

./Strava.ExportToGoogleSheet

echo "Finished" > log