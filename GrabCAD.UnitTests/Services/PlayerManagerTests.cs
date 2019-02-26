using System;
using System.Collections.Generic;
using System.Text;
using GrabCAD.API.Exceptions;
using GrabCAD.API.Helpers;
using Xunit;

namespace GrabCAD.UnitTests.Services
{
    public class PlayerManagerTests
    {

        [Fact]
        public void Initialize_dictionary()
        {
            var service = new PlayerManager();
            var scores = service.GetScores();
            Assert.Empty(scores);
        }

        [Fact]
        public void Adds_player()
        {
            var service = new PlayerManager();
            service.AddPlayer("playerA");
            var scores = service.GetScores();
            Assert.Contains(new KeyValuePair<string,int>("playerA", 0), scores);
        }

        [Fact]
        public void Removes_player()
        {
            var service = new PlayerManager();
            service.AddPlayer("playerA");
            service.RemovePlayer("playerA");
            var scores = service.GetScores();
            Assert.Empty(scores);
        }

        [Fact]
        public void Updates_score()
        {
            var service = new PlayerManager();
            service.AddPlayer("playerA");
            service.UpdateScore("playerA", 1);
            var scores = service.GetScores();
            Assert.Contains(new KeyValuePair<string, int>("playerA", 1), scores);
        }

        [Fact]
        public void Throws_exception_when_same_player_is_added()
        {
            var service = new PlayerManager();

            service.AddPlayer("playerA");
            
            Assert.Throws<ArgumentException>(() => service.AddPlayer("playerA"));
        }
        [Fact]
        public void Throws_exception_when_player_exceeds_10()
        {
            var service = new PlayerManager();
       
            for (int i = 0; i < 10; i++)
            {
                service.AddPlayer($"player_{i}");
            }

            var ex = Assert.Throws<PlayersExceedLimitException>(() => service.AddPlayer("player_10"));
            Assert.Equal("game cannot exceed 10 players", ex.Message);
        }
    }
}
