using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SO53654020.Infrastructure;

namespace SO53654020
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = Constants.ApplicationScheme;
                    o.DefaultSignInScheme = Constants.SignInScheme;
                })
                .AddCookie(Constants.ApplicationScheme)
                .AddCookie(Constants.SignInScheme)
                .AddGoogle(o =>
                {
                    o.ClientId = "...";
                    o.ClientSecret = "...";
                    o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                    o.ClaimActions.Clear();
                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                    o.ClaimActions.MapJsonKey("urn:google:profile", "link");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                });

            serviceCollection.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(o => o.Conventions.AuthorizePage("/Index"));
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseDeveloperExceptionPage();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseMvcWithDefaultRoute();
        }
    }
}
