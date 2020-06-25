using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NETCoreSSO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                
            })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    //options.SignedOutRedirectUri = "https://localhost:5001/Main";
                    options.Events.OnRemoteSignOut += context =>
                    {
                        context.Response.Redirect("/");
                        Console.WriteLine("User signed out.");
                        
                        return Task.CompletedTask;
                    };
                    options.Events.OnSignedOutCallbackRedirect += context =>
                    {
                       
                        Console.WriteLine("Signed out user");
                        return Task.CompletedTask;
                    };
                    options.ResponseType = "{Sensative Info}";
                    options.MetadataAddress = "{Sensative Info}";
                    options.ClientId = "{Sensative Info}";
                    options.RequireHttpsMetadata = "{Sensative Info}";
                    
                    options.SignedOutRedirectUri = "{Sensative Info}";
                    //options.SignedOutRedirectUri = "/Call";
                });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
