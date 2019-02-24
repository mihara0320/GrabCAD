using GrabCAD.API.ViewModels;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrabCAD.API.Helpers
{
    public interface IAnswerManager
    {
        void Add(AnswerViewModel model);
        IEnumerable<AnswerViewModel> GetAll();
    }
    public class AnswerManager : IAnswerManager
    {
        private Stack<AnswerViewModel> AnswerStack = new Stack<AnswerViewModel>();

        public void Add(AnswerViewModel model)
        {
            AnswerStack.Push(model);
        }

        public IEnumerable<AnswerViewModel> GetAll()
        {
            return AnswerStack.ToList();
        }
    }
}
