# Strava Project

## Strava.ImportToDatabase

This project is used to import data from strava into your own database.

To set up this project you'll need to create an appsettings.json file as follows

``` json
{
  "ConnectionStrings": {
    "StravaDb": "Data Source=strava.db"
  },
  "StravaClientSettings": {
    "Code": "",
    "ClientId": "",
    "ClientSecret": ""
  }
}
```

The Client Id and Client Secret can be obtained through set up here https://www.strava.com/settings/api

The code can be obtained by navigating through the OAuth Process which is detailed here
https://developers.strava.com/docs/authentication/. Essentially, we'll need to hit a URL in our browser to obtain the code: https://www.strava.com/oauth/authorize?client_id=[yours]&response_type=code&redirect_uri=http://localhost/exchange_token&approval_prompt=force&scope=read,activity:read. You may been to change the scope depending on the API you need to access. We are then redirected, so I copy the code from the url and paste into the appsettings.json. 

## Strava.ExportToGoogleSheet

This console application uses the cached data that is retrieved from Strava.ImportToDatabase and writes it to a google spreadsheet. 

You'll need to enroll the application in the Google Developer Console and use OAuth to generate a credentials file for this process. Once the credential file is generate then it can be reused so you won't need to go through the account permission screens again.

Here is an example of the required fields within in the appsetting.json file

``` json
{
    "ConnectionStrings": {
        "StravaDb": "Data Source=./strava.db"
    },
    "GoogleSheetService": {
        "ClientId": "",
        "ClientSecret": "",
        "ApplicationName": "",
        "CredentialLocation": "./GoogleSheetCredentials.json"
    }
}
```

I set this process up as a cron job to automatically import my data into my spreadsheet. Alternatively, I can deploy this to a Linux Container and push it to a remote server to run (e.g., Digital Ocean)

## Strava.API + Dashboard

This project uses the database populated by Strava.ImportToDatabase to expose the data via a Web API to a hosted frontend application that contains charts and graphs of your Strava activities.

Check it out: https://strava.robbiebrandrick.com

### Setup in LXD container

The scripts can be found here: /Scripts/Strava.API/Deployment

