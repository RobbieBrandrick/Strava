Root=$(pwd)/..

DatabaseTo=$Root/Strava.ExportToGoogleSheet/bin/Debug/net5.0/
DatabaseFrom=$Root/Database/strava.db

ln -s -f "$DatabaseFrom" "$DatabaseTo"