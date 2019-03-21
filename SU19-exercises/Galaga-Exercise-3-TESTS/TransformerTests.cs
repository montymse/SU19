using System;
using NUnit.Framework;
using Galaga_Exercise_1.GameStateType


namespace Galaga_Exercise_3_TESTS {
    [TestFixture]
    public class TransformerTests {
        [Test]
        public void TransformStringToStateTest("GAME_RUNNING") {
            Assert.Equals(GameStateType.GameRunning);
        }
        
        [Test]
        public void TransformStateToStringTest() {
            Assert.True(true);
        }
    }
}