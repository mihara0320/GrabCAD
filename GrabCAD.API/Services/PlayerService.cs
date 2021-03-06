﻿using System;
using System.Collections.Generic;
using GrabCAD.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GrabCAD.API.Services
{
    public interface IPlayerService
    {
        ActionResult<IDictionary<string, int>> GetPlayers();
        ActionResult AddPlayer(string connectionId);
        ActionResult RemovePlayer(string connectionId);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IHubContext<GameHub, IGameHubClient> _context;
        private readonly IPlayerManager _playerManager;

        public PlayerService(
            IHubContext<GameHub, IGameHubClient> context, 
            IPlayerManager playerManager
            )
        {
            _context = context;
            _playerManager = playerManager;
        }

        public ActionResult<IDictionary<string, int>> GetPlayers()
        {
            try
            {
                var result = _playerManager.GetScores();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult AddPlayer(string connectionId)
        {
            try
            {
                _playerManager.AddPlayer(connectionId);
                _context.Clients.All.PlayerUpdate(_playerManager.GetScores().Count);
                return new StatusCodeResult(202);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public ActionResult RemovePlayer(string connectionId)
        {
            try
            {
                _playerManager.RemovePlayer(connectionId);
                _context.Clients.All.PlayerUpdate(_playerManager.GetScores().Count);
                return new StatusCodeResult(202);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
