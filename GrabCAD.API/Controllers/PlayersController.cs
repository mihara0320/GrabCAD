using System.Collections.Generic;
using GrabCAD.API.Models;
using GrabCAD.API.Services;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IDictionary<string, int>> GetPlayers()
        {
            var result = _playerService.GetPlayers();
            return result;
        }

        [HttpPost("add")]
        public ActionResult AddPlayer(PlayerViewModel model)
        {
            var result = _playerService.AddPlayer(model.ConnectionId);
            return result;
        }

        [HttpPost("remove")]
        public ActionResult RemovePlayer(PlayerViewModel model)
        {
            var result = _playerService.RemovePlayer(model.ConnectionId);
            return result;
        }
    }
}
