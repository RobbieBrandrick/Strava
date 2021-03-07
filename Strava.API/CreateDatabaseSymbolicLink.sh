Root=$(pwd)/..
DatabaseTo=$Root/Strava.API/
DatabaseFrom=$Root/Database/strava.db

ln -s -f "$DatabaseFrom" "$DatabaseTo"