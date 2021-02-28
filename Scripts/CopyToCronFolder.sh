dotnet build

#cp -r ./Database/ /usr/bin/Strava/bin
#chown rob -R /usr/bin/Strava/bin/Database
ln -l /usr/bin/Strava/bin/Database/strava.db ./Database/

if [ ! -d /usr/bin/Strava/bin/ImportToDatabase ]; then
    mkdir /usr/bin/Strava/bin/ImportToDatabase
fi

cp -r ./Strava.ImportToDatabase/bin/Debug/net5.0/* /usr/bin/Strava/bin/ImportToDatabase/

if [ ! -f /usr/bin/Strava/ImportToDatabase ]; then
    ln -s /usr/bin/Strava/bin/ImportToDatabase/Strava.ImportToDatabase /usr/bin/Strava/ImportToDatabase
fi

if [ ! -d /usr/bin/Strava/bin/ExportToGoogleSheet ]; then
    mkdir /usr/bin/Strava/bin/ExportToGoogleSheet
fi

cp -r ./Strava.ExportToGoogleSheet/bin/Debug/net5.0/* /usr/bin/Strava/bin/ExportToGoogleSheet/

if [ ! -f /usr/bin/Strava/ExportToGoogleSheet ]; then
    ln -s /usr/bin/Strava/bin/ExportToGoogleSheet/Strava.ExportToGoogleSheet /usr/bin/Strava/ExportToGoogleSheet
fi
