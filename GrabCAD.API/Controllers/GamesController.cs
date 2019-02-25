﻿using System;
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
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("challenge")]
        public ActionResult<string> GetChallenge()
        {
            var result = _gameService.GetNewChallge();
            return result;
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
