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
        Task<ActionResult> AddAnswerAsync(AnswerViewModel model);
        ActionResult<IEnumerable<AnswerViewModel>> GetAnswers();
    }

    public class GameService: IGameService
    {
        private readonly IHubContext<GameHub, IGameHubClient> _context;
        private readonly IAnswerManager _answerManager;
        private readonly IPlayerManager _playerManager;

        public GameService(
            IHubContext<GameHub, IGameHubClient> context, 
            IAnswerManager answerManager,
            IPlayerManager playerManager
            )
        {
            _context = context;
            _answerManager = answerManager;
            _playerManager = playerManager;
        }


        public async Task<ActionResult> AddAnswerAsync(AnswerViewModel model)
        {
            try
            {
                var resultModel = _answerManager.AnswerChallenge(model);
         
                updateScore(resultModel);
     
                await _context.Clients.All.AnswerRecieved(resultModel);
                await _context.Clients.All.ScoreUpdate(_playerManager.GetScores());

                if (resultModel.FirstCorrectAnswer)
                {
                    await _context.Clients.All.AnswerFound(resultModel);
                    await Task.Delay(5000);
                    await _context.Clients.All.ChallengeUpdate(GetNewChallge());
                }           
                return new OkObjectResult(202);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
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

        private void updateScore(AnswerViewModel model)
        {
            if (model.FirstCorrectAnswer)
            {
                _playerManager.UpdateScore(model.ConnectionId, 1);
            }

            if (!model.CorrectAnswer)
            {
                _playerManager.UpdateScore(model.ConnectionId, -1);
            }
        }

        private string GetNewChallge()
        {
            MathChallenge mathChallenge = MathChallengeGenerator.GenerateMathChallenge();
            _answerManager.SetMathChallenge(mathChallenge);
            return mathChallenge.Challenge;
        }
    }
}
