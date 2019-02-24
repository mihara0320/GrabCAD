using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrabCAD.API.Helpers;
using GrabCAD.API.Models;
using GrabCAD.API.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace GrabCAD.API.Services
{
    public interface IGameHubClient
    {
        Task RecieveAnswer(AnswerViewModel model);
    }

    public class GameHub : Hub<IGameHubClient>
    {
        private IUserManager _userManager;

        public GameHub(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public override Task OnConnectedAsync()
        {
            _userManager.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _userManager.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
