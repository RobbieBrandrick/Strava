
1. Execute Remote Process
  ```
  sudo lxc exec do:StravaExportToGoogleSheet -- /usr/bin/Strava/CrontabStravaProcess.sh
  ```

2. Review Remote Log
  ```
  sudo lxc exec do:StravaExportToGoogleSheet -- cat /usr/bin/Strava/log
  ```

3. Pull Push Database
  ```
  Local=.
  From=do:StravaExportToGoogleSheet/usr/bin/Strava/bin/Database/strava.db
  To=do:StravaAPI/var/www/StravaAPI/
  mv $Local/strava.db $Local/$(date -d "today" +"%Y%m%d%H%M").strava.db
  sudo lxc file pull $From $Local/
  sudo lxc file push $Local/strava.db $To
  ```