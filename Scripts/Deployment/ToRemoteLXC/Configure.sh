CronFolder=/usr/bin/Strava/Deployment

echo "Beginning Configuration"

mkdir $CronFolder

echo "Setting up Programs"

apt-get -y install nano wget zip
wget https://dot.net/v1/dotnet-install.sh
bash ./dotnet-install.sh -c Current -version latest --install-dir /usr/share/dotnet  --runtime dotnet

echo "unzipping deployment files"
unzip -q /Deployment.zip -d $CronFolder/

echo "Setting up crontab"

crontab $CronFolder/CrontabStravaEntry

echo "Setting up databases"

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

echo "Setting up permissions"
#chown -R rob $CronFolder
chmod -R 777 $CronFolder

echo "Configuration is completed"