using System;
using NUnit.Framework;
using Galaga_Exercise_1.GameStateType;


namespace Galaga_Exercise_3_TESTS {
    [TestFixture]
    public class TransformerTests {
        
        [Test]
        public void TransformStringToStateGameRunningTest() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_RUNNING"), GameStateType.GameRunning);
        }
        
        [Test]
        public void TransformStringToStateGamePausedTest() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"), GameStateType.GamePaused);
        }
        
        [Test]
        public void TransformStringToStateMainMenuTest() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_MAINMENU"), GameStateType.MainMenu);
        }
        
        [Test]
        public void TransformStateToStringGameRunningTest() {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameRunning), "GAME_RUNNING");
        }
        
        
        [Test]
        public void TransformStateToStringGamePausedTest() {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GamePaused), "GAME_PAUSED");
        }
        
        
        [Test]
        public void TransformStateToStringMainMenuTest() {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.MainMenu), "GAME_MAINMENU");
        }
    }
}