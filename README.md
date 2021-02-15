# Fluent Spotify API

A .NET 5 async client for [Spotify Web API](https://developer.spotify.com/documentation/web-api/reference/) with fluent syntax.

## Installation And Configuration

Use the following command to install the client NuGet package:

```
Install-Package FluentSpotifyApi
```

Once installed, you can register the client in the composition root alongside your other services:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<NewReleasesService>();

    services.AddFluentSpotifyClient();
}
```

Finally you can start using the client by adding the parameter of type ```IFlunetSpotifyClient``` into the service's constructor:

```csharp
public class NewReleasesService
{
    private readonly IFluentSpotifyClient fluentSpotifyClient;

    public NewReleasesService(IFluentSpotifyClient fluentSpotifyClient)
    {
        this.fluentSpotifyClient = fluentSpotifyClient;
    }

    public Task<NewReleases> GetAsync()
    {
        return this.fluentSpotifyClient.Browse.NewReleases.GetAsync(limit: 50);
    }
}
```

## Authorization

If you try to run the code above, you will get the ```SpotifyRegularErrorException``` with ```ErrorCode``` set to ```HttpStatusCode.Unauthorized```. The Spotify Web API [does not accept unauthenticated calls](https://developer.spotify.com/community/news/2017/01/27/removing-unauthenticated-calls-to-the-web-api/) so you have to use one of the authorization flows described in the [Web API Authorization Guide](https://developer.spotify.com/documentation/general/guides/authorization-guide/). The ```FluentSpotifyApi.AuthorizationFlows``` NuGet package contains app-type-neutral implementation of the authorization flows. The Client Credentials Flow does not require any other NuGet packages. On the other hand, the Authorization Code Flow requires user authorization via Spotify Accounts Service which implementation depends on type of the app. These specific implementations can be found in separate NuGet packages as described later.   

### Client Credentials Flow

This is the simplest authorization flow. It can be used when your app doesn't need to access or modify user's private data. Since this flow requires your app to have access to the Client Secret, it is designed for the server-side apps only. 

Once the ```FluentSpotifyApi.AuthorizationFlows``` NuGet package is installed, you can add Client Credentials Flow to the client HTTP pipeline:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .Configure<SpotifyClientCredentialsFlowOptions>(this.Configuration.GetSection("SpotifyClientCredentialsFlow"));

    services
        .AddFluentSpotifyClient()
        .ConfigureHttpClientBuilder(b => b.AddSpotifyClientCredentialsFlow());
}
```

In the example above, the Client Credentials Flow is configured using [.NET Core Configuration API](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration). The individual options can also be set by calling the `AddSpotifyClientCredentialsFlow` method with a configuration action.

You can also check the [ASP.NET Core Client Credentials Flow sample](samples/FluentSpotifyApi.Sample.CCF.AspNetCore).

### Authorization Code Flow

This authorization flow allows your app to access and modify user's private data. 

#### ASP.NET Core 5.0

The implementation for ASP.NET Core 5.0 apps can be found in the ```FluentSpotifyApi.AuthorizationFlows.AspNetCore``` NuGet package. The user authorization via Spotify Accounts Service, exchange of the authorization code for refresh and access tokens and creation of the authentication ticket with user's claims and tokens is handled by the [Spotify OAuth Handler](src/FluentSpotifyApi.AuthorizationFlows.AspNetCore/AuthorizationCode/Handler/SpotifyHandler.cs) where most of the work is done in the base ```OAuthHandler```, which is part of the [ASP.NET Core authorization middlewares](https://github.com/dotnet/aspnetcore/tree/main/src/Security). The Authorization Code Flow added to the Spotify client HTTP pipeline only handles invalid refresh tokens and renewal of expired access tokens from refresh tokens. The complete configuration of the authorization should look like this:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddFluentSpotifyClient()
        .ConfigureHttpClientBuilder(b => b.AddSpotifyAuthorizationCodeFlow(spotifyAuthenticationScheme: SpotifyAuthenticationScheme));

    services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(
            o =>
            {
                o.LoginPath = new PathString("/login");
                o.LogoutPath = new PathString("/logout");
            })
        .AddSpotify(
            SpotifyAuthenticationScheme,
            o =>
            {
                var spotifyAuthSection = this.Configuration.GetSection("Authentication:Spotify");

                o.ClientId = spotifyAuthSection["ClientId"];
                o.ClientSecret = spotifyAuthSection["ClientSecret"];

                o.Scope.Add(SpotifyScopes.PlaylistReadPrivate);
                o.Scope.Add(SpotifyScopes.PlaylistReadCollaborative);

                o.SaveTokens = true;
            });
}
```
The ```AddSpotifyAuthorizationCodeFlow``` method adds Authorization Code Flow to the Spotify client HTTP pipeline. The ```AddSpotify``` method adds Spotify OAuth handler to the ASP.NET Core authentication pipeline. The ASP.NET Core authorization middleware registers OAuth option instances under the name of the authentication scheme. In the example above, the Spotify authentication scheme name is set to `SpotifyAuthenticationScheme` constant field, which is used for registering Authorization Code Flow in Spotify client HTTP pipeline.

In order to enable Authorization Code Flow for your ASP.NET Core app, you need to register a callback URL in the [Spotify App Registration Portal](https://developer.spotify.com/dashboard/applications). The default callback URL is `<AppBaseUrl>/signin-spotify`. This can be changed by setting the `CallbackPath` property in the `AddSpotify` configuration action.

You can also check the [ASP.NET Core Authorization Code Flow sample](samples/FluentSpotifyApi.Sample.ACF.AspNetCore).

#### UWP

The implementation for UWP apps can be found in the `FluentSpotifyApi.AuthorizationFlows.UWP` NuGet package. This implementation uses the [Web Authentication Broker](https://docs.microsoft.com/en-us/windows/uwp/security/web-authentication-broker) for user authorization.
Since it is not possible to store Client Secret securely in native apps, the implementation uses [Auhtorization Code Flow with PKCE](https://developer.spotify.com/documentation/general/guides/authorization-guide/#authorization-code-flow-with-proof-key-for-code-exchange-pkce). User needs to be authorized in order to make Spotify Web API requests. This is achieved by calling the `RestoreSessionOrAuthorizeUserAsync` method of [```IAuthenticationManager```](src/FluentSpotifyApi.AuthorizationFlows.Native/AuthorizationCode/IAuthenticationManager.cs)
interface that is registered automatically in `IServiceCollection`. The complete configuration of the authorization should look like this:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddFluentSpotifyClient()
        .ConfigureHttpClientBuilder(b => b.AddSpotifyAuthorizationCodeFlow(
            o =>
            {
                o.ClientId = "<ClientId>";

                o.Scopes.Add(SpotifyScopes.PlaylistReadPrivate);
                o.Scopes.Add(SpotifyScopes.PlaylistReadCollaborative);
            }));
}
```

In order to enable Authorization Code Flow for your UWP app, you need to register a callback URL in the [Spotify App Registration Portal](https://developer.spotify.com/dashboard/applications). The callback URL for UWP apps is `ms-app://<PackageSID>`. You can also find the callback URL by calling `WebAuthenticationBroker.GetCurrentApplicationCallbackUri` method. 

You can also check the [UWP Authorization Code Flow sample](samples/FluentSpotifyApi.Sample.ACF.UWP).

## Exception Handling

The following exceptions can be thrown as a result of the communication error with Spotify service:

- `SpotifyCommunicationException`
  - `SpotifyHttpRequestException` The exception that shields `HttpRequestException` thrown by `HttpClient`.
  - `SpotifyHttpClientTimeoutException` The exception that shields `OperationCanceledException` caused by `HttpClient` internal cancellation token representing timeout. 
  - `SpotifyHttpResponseException` This exception is thrown when a HTTP response with error status code is returned. The response can also contain a [JSON payload](https://developer.spotify.com/documentation/web-api/#response-schema) that is parsed into one of the following exceptions:
      - `SpotifyRegularErrorException`
      - `SpotifyAuthenticationErrorException`  
          - `SpotifyInvalidRefreshTokenException` The exception that is thrown when invalid refresh token is detected after [user has revoked access to your app](https://developer.spotify.com/community/news/2016/07/25/app-ready-token-revoke/).
  - `SpotifyAuthorizationException` The exception that is thrown when an error occurs during user authorization via Spotify Accounts Service.
      - `SpotifyWebAuthenticationBrokerException` Contains additional ```WebAuthenticationStatus``` that is returned from the ```WebAuthenticationBroker```.
  
### Retrying

Since the registration API exposes `IHttpClientBuilder`, it is possible to use `Microsoft.Extensions.Http.Polly` library to implement retrying:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var retryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(x => x.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: (retryCount, response, context) =>
            {
                var retryAfter = (response?.Result?.Headers?.RetryAfter?.Delta).GetValueOrDefault();
                var min = TimeSpan.FromSeconds(1);
                return retryAfter > min ? retryAfter : min;
            },
            onRetryAsync: (response, timespan, retryCount, context) => Task.CompletedTask);

    services
        .Configure<SpotifyClientCredentialsFlowOptions>(config.GetSection("SpotifyClientCredentialsFlow"));

    services
        .AddFluentSpotifyClient()
        .ConfigureHttpClientBuilder(b => b.AddPolicyHandler(retryPolicy))
        .ConfigureHttpClientBuilder(b => b
            .AddSpotifyClientCredentialsFlow()
            .ConfigureTokenHttpClientBuilder(b => b.AddPolicyHandler(retryPolicy)));
}
```

You can find the complete example in the [Client Credentials Integration Tests](tests/FluentSpotifyApi.IntegrationTests.CCF).
