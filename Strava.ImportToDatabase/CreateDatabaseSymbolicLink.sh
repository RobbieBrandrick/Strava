Root=$(pwd)/..
DatabaseTo=$Root/Strava.ImportToDatabase/bin/Debug/net5.0/
DatabaseFrom=$Root/Database/strava.db

ln -s -f "$DatabaseFrom" "$DatabaseTo"