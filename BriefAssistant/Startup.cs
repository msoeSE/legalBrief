using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
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
using Npgsql;
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
                    var conn = new NpgsqlConnection(connectionString)
                    {
                        ProvideClientCertificatesCallback = certificates =>
                        {
                            var absPath = Path.GetFullPath("rds-combined-ca-bundle.pfx");
                            Console.WriteLine("Trying to import certs from: " + absPath);
                            var rdsCertBundle = new X509Certificate2Collection();
                            rdsCertBundle.Import("rds-combined-ca-bundle.pfx");
                            certificates.AddRange(rdsCertBundle);
                        }
                    };
                    options.UseNpgsql(conn);
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
                options.Password.RequireNonAlphanumeric = false;
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

            SetupDatabase(app).GetAwaiter().GetResult();

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
        }

        private async Task SetupDatabase(IApplicationBuilder app, CancellationToken cts = default(CancellationToken))
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (Environment.IsDevelopment())
                {
                    await context.Database.EnsureCreatedAsync(cts);
                }
                else
                {
                    await context.Database.MigrateAsync(cts);
                }

                var openIddictManager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication<Guid>>>();
                await RegisterOdicClients(openIddictManager, cts);
            }
        }

        private async Task RegisterOdicClients(OpenIddictApplicationManager<OpenIddictApplication<Guid>> manager,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Note: when using a custom entity or a custom key type, replace OpenIddictApplication by the appropriate type.
            var hostUrl = Configuration["HostUrl"];

            const string clientId = "angular-client";
            if (await manager.FindByClientIdAsync(clientId, cancellationToken) == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = clientId,
                    DisplayName = "Angular Client",
                    PostLogoutRedirectUris = {new Uri($"{hostUrl}signout-odic")},
                    RedirectUris = {new Uri(hostUrl)},
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
