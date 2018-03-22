using System;
using System.Threading;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using BriefAssistant.Authorization;
using BriefAssistant.Data;
using BriefAssistant.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.SecurityHeaders;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using OpenIddict.Core;
using OpenIddict.Models;

namespace BriefAssistant
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            TelemetryConfiguration.Active.DisableTelemetry = true;

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (Environment.IsDevelopment())
                {
                    options.UseInMemoryDatabase("testdb");
                }
                else
                {
                    options.UseNpgsql(connectionString);
                }

                options.UseOpenIddict<Guid>();
            });


            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Sign In settings
                options.SignIn.RequireConfirmedEmail = true;

                // Configure Identity to use the same JWT claims as OpenIddict instead
                // of the legacy WS-Federation claims it uses by default (ClaimTypes),
                // which saves you from doing the mapping in your authorization controller.
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddOpenIddict<Guid>(options =>
            {
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
                options.AddMvcBinders();
                // Enable the token endpoint.
                // Form password flow (used in username/password login requests)
                options.EnableTokenEndpoint("/connect/token");

                // Enable the password and the refresh token flows.
                options.AllowPasswordFlow()
                    .AllowRefreshTokenFlow();

                if (Environment.IsDevelopment())
                {
                    options.DisableHttpsRequirement();
                }
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddOAuthValidation();

            services.AddAuthorization();
            services.AddSingleton<IAuthorizationHandler, BriefAuthorizationCrudHandler>();

            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc(options =>
            {
                if (!Environment.IsDevelopment())
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            });

            services.AddAutoMapper();
            services.AddSingleton<IHostingEnvironment>(Environment);


            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                var forwardOptions = new ForwardedHeadersOptions();
                forwardOptions.ForwardedHeaders = ForwardedHeaders.All;
                forwardOptions.KnownProxies.Clear();
                forwardOptions.KnownNetworks.Clear();
                app.UseForwardedHeaders(forwardOptions);

                app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());
            }

            RegisterOdicClients(app).GetAwaiter().GetResult();

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            var headerPolicies = new HeaderPolicyCollection()
                .AddFrameOptionsDeny()
                .AddXssProtectionBlock()
                .AddContentTypeOptionsNoSniff()
                .AddReferrerPolicyStrictOriginWhenCrossOrigin()
                .RemoveServerHeader()
                .AddContentSecurityPolicy(builder =>
                {
                    builder.AddDefaultSrc().Self();
                    builder.AddConnectSrc().Self().Data();
                    builder.AddObjectSrc().None();
                    builder.AddFormAction().Self();
                    builder.AddImgSrc().Self();
                    builder.AddScriptSrc().Self();
                    builder.AddStyleSrc().Self();
                    builder.AddMediaSrc().Self();
                    builder.AddFrameAncestors().None();
                    builder.AddFrameSource().None();
                });
            app.UseSecurityHeaders(headerPolicies);
        }

        private async Task RegisterOdicClients(IApplicationBuilder app, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync(cancellationToken);

                // Note: when using a custom entity or a custom key type, replace OpenIddictApplication by the appropriate type.
                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication<Guid>>>();

                var hostUrl = Configuration["HostUrl"];

                const string clientId = "angular-client";
                if (await manager.FindByClientIdAsync(clientId, cancellationToken) == null)
                {
                    
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = clientId,
                        DisplayName = "Angular Client",
                        PostLogoutRedirectUris = { new Uri($"{hostUrl}signout-odic")},
                        RedirectUris = {new Uri(hostUrl) },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.Password,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                        }
                    };

                    await manager.CreateAsync(descriptor, cancellationToken);
                }
            }
        }
    }
}
