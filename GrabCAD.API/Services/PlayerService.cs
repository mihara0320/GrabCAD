using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using GrabCAD.API.Helpers;
using GrabCAD.API.Models;
using GrabCAD.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace GrabCAD.API.Services
{
    public interface IPlayerService
    {
        ActionResult<HashSet<string>> GetPlayers();
    }

    public class PlayerService : IPlayerService
    {
        private readonly IUserManager _userManager;

        public PlayerService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult<HashSet<string>> GetPlayers()
        {
            try
            {
                var result = _userManager.GetAll();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
