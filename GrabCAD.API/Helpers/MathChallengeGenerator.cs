using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrabCAD.API.Models;

namespace GrabCAD.API.Helpers
{
    public class MathChallengeGenerator
    {
        public static MathChallenge GenerateMathChallenge()
        {
            var random = new Random();
            var o1 = random.Next(1, 11);
            var o2 = random.Next(1, 11);
            var operation = random.Next(1, 5);
            var deviant = random.Next(-1, 2);
            string operatorString;
            int correctAnswer;

            switch (operation)
            {
                case 1:
                    correctAnswer = o1 + o2;
                    operatorString = "+";
                    break;
                case 2:
                    correctAnswer = o1 - o2;
                    operatorString = "-";
                    break;
                case 3:
                    correctAnswer = o1 * o2;
                    operatorString = "*";
                    break;
                case 4:
                    correctAnswer = o1 / o2;
                    operatorString = "/";
                    break;
                default:
                    correctAnswer = 0;
                    operatorString = "?";
                    break;
            }

            var potentialAnswer = correctAnswer + deviant;
            var mathChallegeString = $"{o1} {operatorString} {o2} = {potentialAnswer}";


            return new MathChallenge()
            {
                Challenge = mathChallegeString,
                PotentialAnswer = potentialAnswer,
                CorrectAnswer = correctAnswer,
                Answer = correctAnswer == potentialAnswer
            };


        }
    }
}
