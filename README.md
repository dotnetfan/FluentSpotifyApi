# Fluent Spotify API

A .NET Standard async client for Spotify Web API with fluent syntax that follows the endpoint hierarchy as described in the [Spotify Web API endpoint reference.](https://developer.spotify.com/web-api/endpoint-reference/)

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

The implementation of the ```IServiceCollection``` interface can be found in the ```Microsoft.Extensions.DependencyInjection``` NuGet package. You can find more information about Microsoft Dependency Injection framework in the [Dependency Injection with .NET Core article.](https://msdn.microsoft.com/en-us/magazine/mt707534.aspx) In case you are using a third party dependency injection framework, there should be an integration NuGet package like Autofac's [Autofac.Extensions.DependencyInjection.](https://github.com/autofac/Autofac.Extensions.DependencyInjection)  

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

If you try to run the code above, you will most likely get the ```SpotifyHttpResponseWithRegularErrorException``` with ```ErrorCode``` set to ```HttpStatusCode.Unauthorized```. The Spotify Web API [does not accept unauthenticated calls](https://developer.spotify.com/migration-guide-for-unauthenticated-web-api-calls/) so you have to use one of the authorization flows described in the [Web API Authorization Guide.](https://developer.spotify.com/web-api/authorization-guide/) The ```FluentSpotifyApi.AuthorizationFlows``` NuGet package contains app-type-neutral implementation of the authorization flows. The Client Credentials Flow does not require any other NuGet packages. On the other hand, the Authorization Code Flow requires user authorization via Spotify Accounts Service which implementation depends on type of the app. These specific implementations can be found in separate NuGet packages as described later.   

### Client Credentials Flow

This is the simplest authorization flow. It can be used when your app doesn't need to access or modify user's private data (i.e. the members that are accessible via [```IFluentSpotifyClient.Me```](src/FluentSpotifyApi/Builder/Me/IMeBuilder.cs) property). Since this flow requires your app to have access to the Client Secret, it is designed for the server-side apps only. 

Once the ```FluentSpotifyApi.AuthorizationFlows``` NuGet package is installed, you can add Client Credentials Flow to the client execution pipeline:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .Configure<ClientCredentialsFlowOptions>(
            this.Configuration.GetSection("ClientCredentialsFlowOptions"));

    services
        .AddFluentSpotifyClient(clientBuilder => clientBuilder
            .ConfigurePipeline(pipeline => pipeline.AddClientCredentialsFlow()));
}
```

In the example above, the Client Credentials Flow is configured using [.NET Core Configuration API](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration). Although the example is taken from an ASP.NET Core app, you can use the configuration API in any .NET Core app. The individual options can also be set by calling the `AddClientCredentialsFlow` method with a configuration action.

You can also check the [ASP.NET Core Client Credentials Flow sample.](samples/FluentSpotifyApi.Sample.CCF.AspNetCore)

### Authorization Code Flow

This authorization flow allows your app to access and modify user's private data (i.e. the members that are accessible via [```IFluentSpotifyClient.Me```](src/FluentSpotifyApi/Builder/Me/IMeBuilder.cs) property). 

#### ASP.NET Core 2.0

The implementation for ASP.NET Core 2.0 apps can be found in the ```FluentSpotifyApi.AuthorizationFlows.AspNetCore``` NuGet package. The user authorization via Spotify Accounts Service, exchange of the authorization code for refresh and access tokens and creation of the authentication ticket with user's claims and tokens is handled by the [Spotify OAuth Handler](src/FluentSpotifyApi.AuthorizationFlows.AspNetCore/AuthorizationCode/Handler/SpotifyHandler.cs) where most of the work is done in the base ```OAuthHandler```, which is part of the [ASP.NET Core authorization middlewares.](https://github.com/aspnet/Security) The  Authorization Code Flow added to the Spotify client execution pipeline only handles invalid refresh tokens and renewal of expired access tokens from refresh tokens. The complete configuration of the authorization should look like this:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddFluentSpotifyClient(clientBuilder => clientBuilder
            .ConfigurePipeline(pipeline => pipeline
                .AddAspNetCoreAuthorizationCodeFlow(
                    spotifyAuthenticationScheme: SpotifyAuthenticationScheme)));

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
                    o.ClientId = Configuration["ClientId"];
                    o.ClientSecret = Configuration["ClientSecret"];                            
                    o.Scope.Add("playlist-read-private");
                    o.Scope.Add("playlist-read-collaborative");
                    o.SaveTokens = true;
                });
}
```
The ```AddAspNetCoreAuthorizationCodeFlow``` method adds Authorization Code Flow to the Spotify client execution pipeline. The ```AddSpotify``` method adds Spotify OAuth handler to the ASP.NET Core authentication pipeline. The ASP.NET Core authorization middleware registers OAuth option instances under the name of the authentication scheme. In the example above, the Spotify authentication scheme name is set to `SpotifyAuthenticationScheme` constant field, which is used for registering Authorization Code Flow in Spotify client execution pipeline.

There is also a simple handler that performs automatic logout and redirects user to the login page when the `SpotifyInvalidRefreshTokenException` exception occurs:

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseSpotifyInvalidRefreshTokenExceptionHandler("/login");
}
```

In order to enable Authorization Code Flow for your ASP.NET Core app, you need to register a callback URL in the [Spotify App Registration Portal.](https://developer.spotify.com/my-applications) The default callback URL is `<AppBaseUrl>/signin-spotify`. This can be changed by setting the `CallbackPath` property in the `AddSpotify` configuration action.

You can also check the [ASP.NET Core Authorization Code Flow sample.](samples/FluentSpotifyApi.Sample.ACF.AspNetCore)

#### UWP

The implementation for UWP apps can be found in the `FluentSpotifyApi.AuthorizationFlows.UWP` NuGet package. This implementation uses the [Web Authentication Broker](https://docs.microsoft.com/en-us/windows/uwp/security/web-authentication-broker) for user authorization.
Since it is not possible to store Client Secret securely in native apps, you need to create a back-end service that will have access to your Client Secret and will handle the token exchange. The [`ITokenProxyClient`](src/FluentSpotifyApi.AuthorizationFlows/AuthorizationCode/Native/ITokenProxyClient.cs) is the client-side interface for your back-end service and it's implementation needs to be registered in `IServiceCollection`. Also user needs to be authorized in order to make Spotify Web API requests. This is achieved by calling the `RestoreSessionOrAuthorizeUserAsync` method of [```IAuthenticationManager```](src/FluentSpotifyApi.AuthorizationFlows/AuthorizationCode/Native/IAuthenticationManager.cs)
interface that is registered automatically in `IServiceCollection`. The complete configuration of the authorization should look like this:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
    	.AddSingleton<ITokenProxyClient, YourTokenProxyClient>();

    services
        .AddFluentSpotifyClient(clientBuilder => clientBuilder
            .ConfigurePipeline(pipeline => pipeline
                .AddUwpAuthorizationCodeFlow(
                    o =>
                    {
                        o.ClientId = "<ClientId>";
                        o.Scopes = new[] { Scope.PlaylistReadPrivate, Scope.PlaylistReadCollaborative };
                    })));
}
```

In order to enable Authorization Code Flow for your UWP app, you need to register a callback URL in the [Spotify App Registration Portal.](https://developer.spotify.com/my-applications) The callback URL for UWP apps is `ms-app://<PackageSID>`. You can also find the callback URL by calling `WebAuthenticationBroker.GetCurrentApplicationCallbackUri` method. 

You can also check the [UWP Authorization Code Flow sample.](samples/FluentSpotifyApi.Sample.ACF.UWP)

### Building your own authorization

Since the authorization library is split into [separate modules](src/FluentSpotifyApi.AuthorizationFlows/Core), it is also possible to create a custom authorization. 

## Exception Handling

The following exceptions can be thrown as a result of the communication error with Spotify service:

- `SpotifyServiceException`
  - `SpotifyHttpResponseWithErrorCodeException` This exception is thrown when a HTTP response with error status code is returned. In case Too Many Requests (429) error is returned, the `Headers.RetryAfter` property contains amount of time you need to wait before next attempt. The response can also contain a [JSON payload](https://developer.spotify.com/web-api/user-guide/#error-details) that is parsed into one of the following exceptions:
      - `SpotifyHttpResponseWithRegularErrorException`
      - `SpotifyHttpResponseWithAuthenticationErrorException`
  - `SpotifyHttpRequestException` The exception that shields any exception (except the `OperationCanceledException` exception when cancellation was requested from the specified cancellation token) thrown by ```HttpClient.SendAsync``` method or during processing of the response stream. The inner exception contains the original exception.
      - `SpotifyHttpRequestCanceledException` The exception that is thrown when `HttpClient` throws `OperationCanceledException` when cancellation was not requested from the specified cancellation token. This usually means that `HttpClient.Timeout` has exceeded.
  - `SpotifyAuthorizationException` The exception that is thrown when an error occurs during user authorization via Spotify Accounts Service.
      - `SpotifyUwpAuthorizationException` Contains additional ```WebAuthenticationStatus``` that is returned from the ```WebAuthenticationBroker```.
  - `SpotifyInvalidRefreshTokenException`
    The exception that is thrown when invalid refresh token is detected after [user has revoked access to your app.](https://developer.spotify.com/news-stories/2016/07/25/app-ready-token-revoke/)
  - `SpotifyDeviceUnavailableException`
    The exception that is thrown when a [Spotify device is temporarily unavailable during the Web API Connect request.](https://developer.spotify.com/web-api/working-with-connect/#202-and-retry)

### Retrying

It is possible to add a custom delegate to the client execution pipeline and use an existing library. The following example uses [Polly.](https://github.com/App-vNext/Polly)

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var retryPolicy = Policy
        .Handle<SpotifyHttpResponseWithErrorCodeException>(x => x.IsRecoverable())
        .Or<SpotifyHttpRequestException>(x => x.InnerException is HttpRequestException)
        .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(5));

    services
        .Configure<ClientCredentialsFlowOptions>(
            this.Configuration.GetSection("ClientCredentialsFlowOptions"));
            
    services
        .AddFluentSpotifyClient(clientBuilder => clientBuilder
            .ConfigurePipeline(pipeline => pipeline
                .AddDelegate((next, cancellationToken) => retryPolicy.ExecuteAsync(next, cancellationToken))
                .AddClientCredentialsFlow()));
}
```

You can find more advanced retrying policies in the [Client Credentials Integration Tests.](tests/FluentSpotifyApi.IntegrationTests.CCF)
