Root=$(pwd)/..
DatabaseTo=$Root/Strava.API/Deployment/strava.db
DatabaseFrom=$Root/Database/strava.db

cp "$DatabaseFrom" "$DatabaseTo"