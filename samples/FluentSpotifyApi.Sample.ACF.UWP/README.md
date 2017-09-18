# UWP Authorization Code Flow sample

This sample demonstrates usage of the Authorization Code Flow in a UWP app. It uses a naive implementation of  [`ITokenProxyClient`](../../src/FluentSpotifyApi.AuthorizationFlows/AuthorizationCode/Native/ITokenProxyClient.cs) where Spotify Accounts Service is called directly from the app, and thus Client Secret needs to be embedded in the app package. This approach should not be used for publicly available apps. In order to run the sample:

- Get a Client ID and Client Secret by creating a new app in the [Spotify App Registration Portal.](https://developer.spotify.com/my-applications)
- Create a new resource file `Assets\Secrets.resw` and add a resource for Client ID with Name set to *ClientId* and value set to your Client ID and a resource for Client Secret with Name set to *ClientSecret* and value set to your Client Secret.
- Register app's callback URL in the [Spotify App Registration Portal.](https://developer.spotify.com/my-applications) You can find the callback URL by uncommenting the `WebAuthenticationBroker.GetCurrentApplicationCallbackUri()` call in [`ViewModelLocator`](ViewModelLocator.cs).
