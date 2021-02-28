Root=./..
Deployment=$Root/Deployment
Bin=$Deployment/bin

DatabaseTo=$Root/Strava.ImportToDatabase/bin/Debug/net5.0/
DatabaseFrom=$Root/Database/strava.db

if [ -L $DatabaseTo/strava.db ]; then
    rm $DatabaseTo/strava.db
fi

ln -s $DatabaseFrom $DatabaseTo
