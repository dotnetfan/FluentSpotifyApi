# UWP Authorization Code Flow sample

This sample demonstrates usage of the Authorization Code Flow with PKCE in a UWP app. In order to run the sample:

- Get a Client ID by creating a new app in the [Spotify App Registration Portal](https://developer.spotify.com/dashboard/applications).
- Create a new resource file `Assets\Secrets.resw` and add a resource for Client ID with Name set to *ClientId* and value set to your Client ID.
- Register app's callback URL in the [Spotify App Registration Portal](https://developer.spotify.com/dashboard/applications). You can find the callback URL by uncommenting the `WebAuthenticationBroker.GetCurrentApplicationCallbackUri()` call in [`ViewModelLocator`](ViewModelLocator.cs).
