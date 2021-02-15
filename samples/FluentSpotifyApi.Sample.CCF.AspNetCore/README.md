# ASP.NET Core 5.0 Client Credentials Flow sample

This sample demonstrates usage of Client Credentials Flow in the ASP.NET Core application. In order to run the sample:

- Get a Client ID and Client Secret by creating a new app in the [Spotify App Registration Portal](https://developer.spotify.com/dashboard/applications).
- Store Client ID and Client Secret via Secret Manager tool: In Visual Studio, right click on the project and select "Manage User Secrets". Add the following JSON code with provided Client ID and Client Secret to the secrets.json file

```json
{
  "SpotifyClientCredentialsFlow": {
    "ClientId": "<ClientId>",
    "ClientSecret": "<ClientSecret>"
  }
}
```
