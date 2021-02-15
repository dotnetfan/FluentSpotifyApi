using System;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Utils;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Exceptions;
using FluentSpotifyApi.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FluentSpotifyApi.Sample.ACF.AspNetCore
{
    public class Startup
    {
        private const string SpotifyAuthenticationScheme = SpotifyDefaults.AuthenticationScheme;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

                        // Validate claims and tokens that are required when using FluentSpotifyClient.
                        o.Events.OnSigningIn = c =>
                        {
                            if (!SpotifyRequiredClaimsUtils.Validate(c.Principal))
                            {
                                throw new InvalidOperationException("User ID was not provided during login. Ensure that NameIdentifier claim is added to SpotifyOptions.");
                            }

                            if (!SpotifyRequiredTokensUtils.Validate(c.Properties))
                            {
                                throw new InvalidOperationException("Tokens were not provided during login. Ensure that SaveTokens is set to true in SpotifyOptions.");
                            }

                            return Task.CompletedTask;
                        };
                        o.Events.OnValidatePrincipal = async c =>
                        {
                            if (!SpotifyRequiredClaimsUtils.Validate(c.Principal) || !SpotifyRequiredTokensUtils.Validate(c.Properties))
                            {
                                c.RejectPrincipal();
                                await c.HttpContext.SignOutAsync();
                                c.HttpContext.Response.Redirect("/login");
                            }
                        };
                    })
                .AddSpotify(
                    SpotifyAuthenticationScheme,
                    o =>
                    {
                        var spotifyAuthSection = this.Configuration.GetSection("Authentication:Spotify");

                        o.ClientId = spotifyAuthSection["ClientId"];
                        o.ClientSecret = spotifyAuthSection["ClientSecret"];

                        // Uncomment to add email claim. Requires user-read-email scope.
                        ////o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                        ////o.Scope.Add(SpotifyScopes.UserReadEmail);

                        // Uncomment to add custom profile picture claim.
                        ////o.ClaimActions.MapCustomJson(
                        ////    "urn:spotify:profilepicture",
                        ////    e => e.TryGetProperty("images", out var images) && images.GetArrayLength() > 0 && images[0].TryGetProperty("url", out var url) ? url.GetString() : null);

                        o.Scope.Add(SpotifyScopes.PlaylistReadPrivate);
                        o.Scope.Add(SpotifyScopes.PlaylistReadCollaborative);

                        o.SaveTokens = true;
                    });

            services
                .AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (SpotifyInvalidRefreshTokenException)
                {
                    await context.SignOutAsync();
                    context.Response.Redirect("/login");
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app
                .UseAuthentication()
                .UseAuthorization();

            app.Map(
                "/login",
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            // Return a challenge to invoke the Spotify authentication scheme
                            await context.ChallengeAsync(SpotifyAuthenticationScheme, properties: new AuthenticationProperties() { RedirectUri = "/", IsPersistent = true });
                        });
                });

            // Listen for requests on the /logout path, and sign the user out
            app.Map(
                "/logout",
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            // Sign the user out of the authentication middleware (i.e. it will clear the Auth cookie)
                            await context.SignOutAsync();

                            // Redirect the user to the home page after signing out
                            context.Response.Redirect("/");
                        });
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
