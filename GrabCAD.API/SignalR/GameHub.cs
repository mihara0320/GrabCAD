using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrabCAD.API.Exceptions;
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
        //Task RequestNextChallenge();
    }

    public class GameHub : Hub<IGameHubClient>
    {
        private readonly IPlayerManager _playerManager;

        public GameHub(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public int GetConnectionCount()
        {
            return _playerManager.GetAll().Count;
        }
        
        public override Task OnConnectedAsync()
        {
            base.Clients.All.PlayerUpdate(_playerManager.GetAll().Count);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _playerManager.RemovePlayer(Context.ConnectionId);
            base.Clients.All.PlayerUpdate(_playerManager.GetAll().Count);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
