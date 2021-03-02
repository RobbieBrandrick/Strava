ScriptFolder=/home/rob/storage/dev/Strava/Scripts/Deployment/ToRemoteLXC
ProjectRoot=/home/rob/storage/dev/Strava
CrontabLocation=/usr/bin/Strava
Instance=do:StravaExportToGoogleSheet

echo "Creating Container"
lxc delete $Instance --force
lxc launch images:ubuntu/20.04 $Instance

$ScriptFolder/CreateDeploymentDirectory.sh $ProjectRoot
cp $ScriptFolder/CrontabStravaProcess.sh $ProjectRoot/Deployment/ 
cp $ScriptFolder/CrontabStravaEntry $ProjectRoot/Deployment/ 
zip -r -q  $ProjectRoot/Deployment.zip $ProjectRoot/Deployment/*

echo 'Pushing deployment folder'

lxc file push -r $ProjectRoot/Deployment.zip $Instance/
lxc file push -r $ScriptFolder/Configure.sh $Instance/

lxc exec $Instance -- bash /Configure.sh

echo "$Instance LXC Container has been set up"

