using FluentSpotifyApi.AuthorizationFlows.ClientCredentials;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials.Extensions;
using FluentSpotifyApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.Sample.CCF.AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<ClientCredentialsFlowOptions>(
                    this.Configuration.GetSection("ClientCredentialsFlowOptions"));

            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .ConfigurePipeline(pipeline => pipeline.AddClientCredentialsFlow()));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
