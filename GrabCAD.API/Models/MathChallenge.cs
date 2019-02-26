namespace GrabCAD.API.Models
{
    public class MathChallenge
    {
        public string Challenge { get; set; }

        public int PotentialAnswer { get; set; }

        public int CorrectAnswer { get; set; }

        public bool Answer { get; set; } // Answer is YES or NO

        public bool AnswerFound { get; set; } = false;
    }
}
