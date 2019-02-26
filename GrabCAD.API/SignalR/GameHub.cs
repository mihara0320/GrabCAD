using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrabCAD.API.Helpers;
using GrabCAD.API.Models;
using GrabCAD.API.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace GrabCAD.API.Services
{
    public interface IGameHubClient
    {
        Task AnswerRecieved(AnswerViewModel model);
        Task AnswerFound(AnswerViewModel model);
        Task PlayerUpdate(int connections);
        Task ChallengeUpdate(string challege);
        Task ScoreUpdate(List<KeyValuePair<string, int>> scores);
        //Task RequestNextChallenge();
    }

    public class GameHub : Hub<IGameHubClient>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IAnswerManager _answerManager;

        public GameHub(IPlayerManager playerManager, IAnswerManager answerManager)
        {
            _playerManager = playerManager;
            _answerManager = answerManager;
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public Task RequestNextChallenge()
        {
            MathChallenge mathChallenge = MathChallengeGenerator.GenerateMathChallenge();
            _answerManager.SetMathChallenge(mathChallenge);
            return Clients.All.ChallengeUpdate(mathChallenge.Challenge);
        }
        public override Task OnConnectedAsync()
        {
            base.Clients.All.PlayerUpdate(_playerManager.GetScores().Count);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _playerManager.RemovePlayer(Context.ConnectionId);
            base.Clients.All.PlayerUpdate(_playerManager.GetScores().Count);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
