using System;
using System.Collections.Generic;
using System.Linq;
using GrabCAD.API.Exceptions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace GrabCAD.API.Helpers
{
    public interface IPlayerManager
    {
        void AddPlayer(string id);
        void RemovePlayer(string id);
        void UpdateScore(string id, int score);
        List<KeyValuePair<string, int>> GetScores();
    }

    public class PlayerManager : IPlayerManager
    {
        private readonly IDictionary<string, int> PlayerScores = new Dictionary<string, int>();

        public void AddPlayer(string id)
        {
            if (PlayerScores.Count < 10)
            {
                PlayerScores.Add(id, 0);
            }
            else
            {
                throw new PlayersExceedLimitException("game cannot exceed 10 players");
            }
 
        }

        public void RemovePlayer(string id)
        {
            try
            {
                PlayerScores.Remove(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateScore(string id, int score)
        {
            var newScore = PlayerScores[id] + score;
            PlayerScores[id] = newScore > 0 ? newScore : 0;
        }

        public List<KeyValuePair<string, int>> GetScores()
        {
            return PlayerScores.ToList();
        }
    }
}
