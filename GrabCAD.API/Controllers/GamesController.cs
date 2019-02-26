﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using GrabCAD.API.Services;
using GrabCAD.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GrabCAD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("answers")]
        public ActionResult<IEnumerable<AnswerViewModel>> GetAnswers()
        {
            var result = _gameService.GetAnswers();
            return result;
        }

        [HttpPost("answers")]
        public async Task<ActionResult> Post([FromBody] AnswerViewModel model)
        {
            var result = await _gameService.AddAnswerAsync(model);
            return result;
        }
    }
}
