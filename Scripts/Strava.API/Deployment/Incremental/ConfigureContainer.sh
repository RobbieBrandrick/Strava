DeployToWWWLocation=/var/www
DeployToLocation=$DeployToWWWLocation/StravaAPI

echo "Unzipping Deployment"
unzip -q -o /Deployment.zip -d $DeployToLocation/

echo 'Testing Deployment'

curl http://localhost:5000
curl stravalocal.com
