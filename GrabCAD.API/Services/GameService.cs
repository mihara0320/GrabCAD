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
    public interface IGameService
    {
        Task<IActionResult> AddAnswerAsync(AnswerViewModel model);
        ActionResult<IEnumerable<AnswerViewModel>> GetAnswers();
        ActionResult<HashSet<string>> GetPlayers();
    }

    public class GameService: IGameService
    {
        private readonly IHubContext<GameHub, IGameHubClient> _context;
        private readonly IAnswerManager _answerManager;

        public GameService(IHubContext<GameHub, IGameHubClient> context, IAnswerManager answerManager)
        {
            _context = context;
            _answerManager = answerManager;
        }


        public async Task<IActionResult> AddAnswerAsync(AnswerViewModel model)
        {
            try
            {
                _answerManager.Add(model);
                await _context.Clients.All.RecieveAnswer(model);
                return new StatusCodeResult(202);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult<IEnumerable<AnswerViewModel>> GetAnswers()
        {
            try
            {

                var result = _answerManager.GetAll();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult<string> GetNewChallge()
        {
            try
            {
                MathChallenge mathChallenge = MathChallengeGenerator.GenerateMathChallenge();
                var result = mathChallenge.Challenge;
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
