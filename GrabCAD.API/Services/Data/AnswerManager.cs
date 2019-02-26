using GrabCAD.API.Exceptions;
using GrabCAD.API.Models;
using GrabCAD.API.ViewModels;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrabCAD.API.Helpers
{
    public interface IAnswerManager
    {
        AnswerViewModel AnswerChallenge(AnswerViewModel model);
        IEnumerable<AnswerViewModel> GetAll();
        void SetMathChallenge(MathChallenge challenge);
    }
    public class AnswerManager : IAnswerManager
    {
        private readonly Stack<AnswerViewModel> _answerStack = new Stack<AnswerViewModel>();
        private MathChallenge _mathChallenge;

        public AnswerViewModel AnswerChallenge(AnswerViewModel model)
        {
            if(_mathChallenge == null)
            {
                throw new MathChallengeNotSetException("Math challenge is not set");
            }

            var playerAnswered = _answerStack.Any(o => o.ConnectionId == model.ConnectionId );
            if (playerAnswered)
            {
                throw new PlayerHasAnswerException("Player already answered");
            }

            model.CorrectAnswer = _mathChallenge.Answer == model.Answer;
     
            if (!_mathChallenge.AnswerFound && model.CorrectAnswer)
            {
                _mathChallenge.AnswerFound = true;
                model.FirstCorrectAnswer = true;
            }
 
            _answerStack.Push(model);
            return model;
        }

        public IEnumerable<AnswerViewModel> GetAll()
        {
            return _answerStack.ToList();
        }

        public void SetMathChallenge(MathChallenge challenge)
        {
            _mathChallenge = challenge;
            _answerStack.Clear();
        }
    }
}
