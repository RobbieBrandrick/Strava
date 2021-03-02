ScriptFolder=/home/rob/storage/dev/Strava/Scripts/Deployment/ToLocalLXC
ProjectRoot=/home/rob/storage/dev/Strava
CrontabLocation=/usr/bin/Strava
Instance=StravaExportToGoogleSheet

$ScriptFolder/CreateDeploymentDirectory.sh $ProjectRoot

echo "Creating Container"
lxc launch images:ubuntu/20.04 $Instance

echo "Updating Container and adding required programs"
lxc exec $Instance -- apt-get -y install nano wget
lxc exec $Instance -- wget https://dot.net/v1/dotnet-install.sh
lxc exec $Instance -- bash ./dotnet-install.sh -c Current -version latest --install-dir /usr/share/dotnet  --runtime dotnet

echo 'Pushing program files'
lxc exec $Instance -- mkdir $CrontabLocation

lxc file push -r $ProjectRoot/Deployment/* $Instance/$CrontabLocation/

lxc file push $ScriptFolder/CrontabStravaProcess.sh $Instance/$CrontabLocation/

lxc file push $ScriptFolder/ConfigureCrontabLocation.sh $Instance/$CrontabLocation/
lxc exec $Instance -- bash $CrontabLocation/ConfigureCrontabLocation.sh

lxc file push $ScriptFolder/CrontabStravaEntry $Instance/$CrontabLocation/
lxc exec $Instance -- crontab $CrontabLocation/CrontabStravaEntry

echo "$Instance LXC Container has been set up"

