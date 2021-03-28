RemoteDatabase=/usr/bin/Strava/bin/Database/strava.db
LocalDatabaseLocation=/home/rob/storage/dev/Strava/Database
Instance=do:StravaExportToGoogleSheet

mv $LocalDatabaseLocation/strava.db $LocalDatabaseLocation/$(date -d "today" +"%Y%m%d%H%M").strava.db

/snap/bin/lxc file pull -r $Instance/$RemoteDatabase $LocalDatabaseLocation