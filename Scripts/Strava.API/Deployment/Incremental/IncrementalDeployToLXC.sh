Instance=$1
RootFolder=$2
ScriptFolder=$RootFolder/Scripts/Strava.API/Deployment/Incremental/
ProjectLocation=$RootFolder/Strava.API
DeployFromLocation=$ProjectLocation/Deployment
DeploymentPackage=$ProjectLocation/Deployment.zip
DeployToWWWLocation=/var/www
DeployToLocation=$DeployToWWWLocation/StravaAPI

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

#Remove non-pertinent files
find $DeployFromLocation -maxdepth 1  ! -regex '.*Deployment\/Strava.*' -type f -delete

echo "Zipping deployment folder"
cd $DeployFromLocation/
zip -r -q - . > ../Deployment.zip

echo 'Pushing Deployment'
lxc file push -r $DeploymentPackage $Instance/
lxc file push $ScriptFolder/ConfigureContainer.sh $Instance/

echo 'Running ConfigureContainer.sh'
lxc exec $Instance -- bash /ConfigureContainer.sh

echo "$Instance LXC Container has been set up"

echo "Opening container in terminal"
lxc exec $Instance -- bash -lc "cd $DeployToLocation && bash" 