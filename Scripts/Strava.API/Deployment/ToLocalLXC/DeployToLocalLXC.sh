ScriptFolder=/home/rob/storage/dev/Strava/Scripts/Strava.API/Deployment/ToLocalLXC
ProjectLocation=/home/rob/storage/dev/Strava/Strava.API
DeployFromLocation=$ProjectLocation/Deployment
DeploymentPackage=$ProjectLocation/Deployment.zip
DeployToWWWLocation=/var/www
DeployToLocation=$DeployToWWWLocation/StravaAPI
Instance=StravaFrontend

clear

echo "Creating Deployment folder"
if [ -d "$DeployFromLocation"/ ]; then
    rm -r "${DeployFromLocation:?}/"    
fi

mkdir $DeployFromLocation

if [ -f "$DeploymentPackage"/ ]; then
    rm "${DeploymentPackage:?}/"    
fi

dotnet publish -r linux-x64 --configuration Release -o $DeployFromLocation $ProjectLocation 

echo "Zipping deployment folder"
cd $DeployFromLocation/
zip -r -q ../Deployment.zip .

echo "Creating Container"
lxc delete $Instance --force
lxc launch images:ubuntu/20.04 $Instance

echo 'Pushing Deployment'
lxc file push -r $DeploymentPackage $Instance/
lxc file push $ScriptFolder/ConfigureContainer.sh $Instance/

echo 'Running ConfigureContainer.sh'
lxc exec $Instance -- bash /ConfigureContainer.sh

echo "$Instance LXC Container has been set up"

echo "Opening container in terminal"
lxc exec $Instance -- bash -lc "cd $DeployToLocation && bash" 

