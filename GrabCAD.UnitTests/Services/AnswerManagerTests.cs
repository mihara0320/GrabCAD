using System;
using System.Collections.Generic;
using System.Linq;
using GrabCAD.API.Exceptions;
using GrabCAD.API.Helpers;
using GrabCAD.API.Models;
using GrabCAD.API.ViewModels;
using Xunit;

namespace GrabCAD.UnitTests.Services
{
    public class AnswerManagerTests
    {
        readonly AnswerViewModel _answer = new AnswerViewModel()
        {
            ConnectionId = "playerA",
            Answer = false
        };

        readonly MathChallenge _challenge = MathChallengeGenerator.GenerateMathChallenge();

        [Fact]
        public void Initialize_stack()
        {
            var service = new AnswerManager();
            var answers = service.GetAll();
            Assert.Empty(answers);
        }

        [Fact]
        public void Sets_math_challenge()
        {
            var service = new AnswerManager();
            service.SetMathChallenge(_challenge);
            Assert.Equal(_challenge, service.GetMathChallenge());
        }

        [Fact]
        public void Adds_answer()
        {
            var service = new AnswerManager();

            service.SetMathChallenge(_challenge);
            service.AnswerChallenge(_answer);
            var answers = service.GetAll();
            Assert.Contains(_answer, answers);
        }

        [Fact]
        public void Checks_answer_is_correct()
        {
            var service = new AnswerManager();
            service.SetMathChallenge(_challenge);
            var answer = new AnswerViewModel()
            {
                ConnectionId = "playerA",
                Answer = _challenge.Answer
            };
            service.AnswerChallenge(answer);
            var answers = service.GetAll();
            var obtainedAnswer = answers.FirstOrDefault(p => p.ConnectionId == answer.ConnectionId);
            Assert.True(obtainedAnswer.CorrectAnswer);
        }

        [Fact]
        public void Checks_answer_is_incorrect()
        {
            var service = new AnswerManager();
            service.SetMathChallenge(_challenge);
            var answer = new AnswerViewModel()
            {
                ConnectionId = "playerA",
                Answer = !_challenge.Answer
            };
            service.AnswerChallenge(answer);
            var answers = service.GetAll();
            var obtainedAnswer = answers.FirstOrDefault(p => p.ConnectionId == answer.ConnectionId);
            Assert.False(obtainedAnswer.CorrectAnswer);
        }

        [Fact]
        public void Throws_MathChallengeNotSetException_on_GetMathChallenge()
        {
            var service = new AnswerManager();
            var ex = Assert.Throws<MathChallengeNotSetException>(() => service.GetMathChallenge());
            Assert.Equal("Math challenge is not set", ex.Message);
        }

        [Fact]
        public void Throws_MathChallengeNotSetException_on_AnswerChallenge()
        {
            var service = new AnswerManager();
            var ex = Assert.Throws<MathChallengeNotSetException> (() => service.AnswerChallenge(_answer));
            Assert.Equal("Math challenge is not set", ex.Message);
        }

        [Fact]
        public void Throws_PlayerHasAnswerException_on_AnswerChallenge()
        {
            var service = new AnswerManager();
            var challenge = MathChallengeGenerator.GenerateMathChallenge();
            service.SetMathChallenge(challenge);
            service.AnswerChallenge(_answer);
            var ex = Assert.Throws<PlayerHasAnswerException>(() => service.AnswerChallenge(_answer));
            Assert.Equal("Player already answered", ex.Message);
        }
    }
}
