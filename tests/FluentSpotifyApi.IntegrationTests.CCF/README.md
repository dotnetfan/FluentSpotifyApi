# Client Credentials Flow integration tests

These integration tests test Web API endpoints that return publicly available data using Client Credentials Flow. In order to run the tests locally:

- Get a Client ID and Client Secret by creating a new app in the [Spotify App Registration Portal.](https://developer.spotify.com/my-applications)
- Create new `secrets.json` file in project's folder with the following JSON code using provided Client ID and Client Secret:

```json
{
  "ClientCredentialsFlowOptions": {
    "ClientId": "<ClientId>",
    "ClientSecret": "<ClientSecret>",
  }
}
```
