using GrabCAD.API.Helpers;
using GrabCAD.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GrabCAD.API.IoC
{
    public class Configuration
    {
        public static void Init(IServiceCollection services)
        {
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            services.AddSingleton<IPlayerManager, PlayerManager>();
            services.AddSingleton<IAnswerManager, AnswerManager>();
        }
    }
}
