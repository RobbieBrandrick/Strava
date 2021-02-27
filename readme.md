# Strava Project

## Strava.ImportToDatabase

This project is used to import data from strava into your own database.

To set up this project you'll need to create an appsettings.json file as follows

``` json
{
  "StravaClientSettings": {
    "Code": "",
    "ClientId": "",
    "ClientSecret": ""
  }
}
```

The Client Id and Client Secret can be obtained through set up here https://www.strava.com/settings/api

The code can be obtained by navigating through the OAuth Process which is detailed here
https://developers.strava.com/docs/authentication/. Essentially, we'll need to hit a URL in our browser to obtain the code: https://www.strava.com/oauth/authorize?client_id=[yours]&response_type=code&redirect_uri=http://localhost/exchange_token&approval_prompt=force&scope=read,activity:read. You may been to change the scope depending on the API you need to access



