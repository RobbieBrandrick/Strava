ProjectRoot=/home/rob/storage/dev/Strava
CrontabLocation=/usr/bin/Strava
User=rob

./CreateDeploymentDirectory.sh  $ProjectRoot
./CopyDeploymentToLocalCronFolder.sh $ProjectRoot $CrontabLocation $User

echo "========"
echo "Local Crontab requires a manual entry into the crontab file"
echo "You can access this file via the command 'sudo crontab -e"
echo "Here is an example entry"
cat ./CrontabStravaEntry