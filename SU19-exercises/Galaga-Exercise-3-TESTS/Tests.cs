using System;
using ConsoleApplication1;
using NUnit.Framework;

namespace TestProject1 {
    [TestFixture]
    
    public class TransformerTests {
    [Test]
    public void TransformStringToStateGameRunningTest() {
    Assert.AreEqual(StateTransformer.TransformStringToState("GAME_RUNNING"), StateTransformer.GameStateType.GameRunning);
    }
        
    [Test]
    public void TransformStringToStateGamePausedTest() {
    Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"), StateTransformer.GameStateType.GamePaused);
    }
        
    [Test]
    public void TransformStringToStateMainMenuTest() {
    Assert.AreEqual(StateTransformer.TransformStringToState("GAME_MAINMENU"), StateTransformer.GameStateType.MainMenu);
    }
        
    [Test]
    public void TransformStateToStringGameRunningTest() {
    Assert.AreEqual(StateTransformer.TransformStateToString(StateTransformer.GameStateType.GameRunning), "GAME_RUNNING");
    }
        
        
    [Test]
    public void TransformStateToStringGamePausedTest() {
    Assert.AreEqual(StateTransformer.TransformStateToString(StateTransformer.GameStateType.GamePaused), "GAME_PAUSED");
    }
        
        
    [Test]
    public void TransformStateToStringMainMenuTest() {
    Assert.AreEqual(StateTransformer.TransformStateToString(StateTransformer.GameStateType.MainMenu), "GAME_MAINMENU");
    }
    }
}