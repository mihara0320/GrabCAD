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
            Random random = new Random();
            var o1 = random.Next(1, 11);
            var o2 = random.Next(1, 11);
            var operation = random.Next(1, 5);
            string operatorString = "+";
            int answer;

            switch (operation)
            {
                case 1:
                    answer = o1 + o2;
                    operatorString = "+";
                    break;
                case 2:
                    answer = o1 - o2;
                    operatorString = "-";
                    break;
                case 3:
                    answer = o1 * o2;
                    operatorString = "*";
                    break;
                case 4:
                    answer = o1 / o2;
                    operatorString = "/";
                    break;
                default:
                    answer = 0;
                    operatorString = "?";
                    break;
            }

            string mathChallegeString = $"{o1} {operatorString} {o2}";
            return new MathChallenge(){ Challenge = mathChallegeString, Answer = answer};
        }
    }
}
