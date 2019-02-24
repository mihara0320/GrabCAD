using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
   
            services.AddSingleton<IUserManager, UserManager>();
            services.AddSingleton<IAnswerManager, AnswerManager>();
        }
    }
}
