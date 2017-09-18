# ASP.NET Core 2.0 Authorization Code Flow sample

This sample demonstrates usage of Authorization Code Flow in the ASP.NET Core application. In order to run the sample:

- Get a Client ID and Client Secret by creating a new app in the [Spotify App Registration Portal.](https://developer.spotify.com/my-applications)
- Store Client ID and Client Secret via Secret Manager tool: In Visual Studio, right click on the project and select "Manage User Secrets". Add the following JSON code with provided Client ID and Client Secret to the secrets.json file

```json
{
  "ClientId": "<ClientId>",
  "ClientSecret": "<ClientSecret>"
}
```
- Make sure that [SSL is enabled.](https://docs.microsoft.com/en-us/aspnet/core/security/https)
- Register app's callback URL in the [Spotify App Registration Portal.](https://developer.spotify.com/my-applications) The default callback URL is `<AppBaseUrl>/signin-spotify`.
