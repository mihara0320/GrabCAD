using System;
using GrabCAD.API.Helpers;
using GrabCAD.API.Models;
using Xunit;

namespace GrabCAD.UnitTests.Helpers
{
    public class MathChallengeGeneratorTests
    {
        [Fact]
        public void Generates_math_challenge()
        {
            var challenge = MathChallengeGenerator.GenerateMathChallenge();
            Assert.IsType<MathChallenge>(challenge);
        }

        [Fact]
        public void Challenge_answer_is_true()
        {
            var challege = MathChallengeGenerator.ComposeMathChallenge("1 + 1 = 2", 2, 2);
            Assert.True(challege.Answer);
        }

        [Fact]
        public void Challenge_answer_is_false()
        {
            var challege = MathChallengeGenerator.ComposeMathChallenge("1 + 1 = 3", 3, 2);
            Assert.False(challege.Answer);
        }
    }
}
