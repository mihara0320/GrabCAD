using GrabCAD.API.Authentication;
using GrabCAD.API.Services;
using GrabCAD.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace GrabCAD.API
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
            var authenticationSettings = Configuration.GetSection(nameof(AuthenticationSettings)).Get<AuthenticationSettings>();
            services.Configure<AuthenticationSettings>(Configuration.GetSection(nameof(AuthenticationSettings)));
            ConfigureAuthentication(services, authenticationSettings);
  
            services.AddSignalR();
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200");
            }));

            IoC.Configuration.Init(services);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseWebSockets();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseSignalR((options) => {
                options.MapHub<GameHub>("/hubs/game");
            });
            app.UseHttpsRedirection();
            app.UseMvc();     
        }

        private void ConfigureAuthentication(IServiceCollection services, AuthenticationSettings authenticationSettings)
        {
            var signingKey = new SigningSymmetricKey(authenticationSettings.SigningSecurityKey
                                                     ?? throw new InvalidOperationException("Empty signin security key"));
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),
                        ValidateIssuer = true,
                        ValidIssuer = authenticationSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authenticationSettings.Audience,
                        ValidateLifetime = true
                    };

                    jwtBearerOptions.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken) &&
                                (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
