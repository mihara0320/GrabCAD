using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrabCAD.API.Helpers;
using GrabCAD.API.Services;
using GrabCAD.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GrabCAD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public ActionResult<HashSet<string>> GetPlayers()
        {
            var result = _playerService.GetPlayers();
            return result;
        }
    }
}
