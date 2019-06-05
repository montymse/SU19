using System.Collections.Generic;
using DIKUArcade.EventBus;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.GameStates;

namespace TestSpaceTaxi.TestGameStates {
    [TestFixture]

    public class StateMachineTest {
        private StateMachine stateMachine;

        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();

            SpaceTaxiBus.GetBus().InitializeEventBus(new List<GameEventType>() {
                GameEventType.WindowEvent,
                GameEventType.InputEvent,
                GameEventType.GameStateEvent,
                GameEventType.PlayerEvent

            });

            SpaceTaxiBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);


        }

        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestEventGamePaused() {
            SpaceTaxiBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this,
                    "CHANGE_STATE",
                    "GAME_PAUSED", ""));

            SpaceTaxiBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());

        }
    }
}
    