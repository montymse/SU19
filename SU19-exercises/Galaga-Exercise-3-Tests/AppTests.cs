using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GameStateType;

namespace Galaga_Exercise_3_Tests {
    [TestFixture]
    public class Tests {
        [SetUp]
        public void BeforeEachTest() {
        }

        [Test]
        public void TransformStateToString() {
            Assert.AreEqual(StateTransformer.TransformStateToString(StateTransformer.GameStateType.MainMenu), "GAME_MAINMENU");
            Assert.AreEqual(StateTransformer.TransformStateToString(StateTransformer.GameStateType.GamePaused), "GAME_PAUSED");
            Assert.AreEqual(StateTransformer.TransformStateToString(StateTransformer.GameStateType.GameRunning), "GAME_RUNNING");
        }
        
        [Test]
        public void TransformStringToState() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_MAINMENU"),StateTransformer.GameStateType.MainMenu);
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"),StateTransformer.GameStateType.GamePaused);
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_RUNNING"),StateTransformer.GameStateType.GameRunning);
        }
    }
}