using System.ComponentModel.DataAnnotations;

namespace GrabCAD.API.ViewModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "ConnectionId is required")]
        public string ConnectionId { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        public bool Answer { get; set; }

        public bool FirstCorrectAnswer { get; set; } = false;
        public bool CorrectAnswer { get; set; } = false;
    }
}
