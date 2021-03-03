CronFolder=/usr/bin/Strava

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
ln -s -f $StravaDatabase $ImportToDatabaseTo

ExportToGoogleSheetTo=$CronFolder/bin/ExportToGoogleSheet
ln -s -f $StravaDatabase $ExportToGoogleSheetTo

echo "Setting up permissions"
chmod -R 777 $CronFolder

echo "Configuration is completed"