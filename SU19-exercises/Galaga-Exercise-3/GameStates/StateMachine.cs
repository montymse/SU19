using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GameStates;
using Galaga_Exercise_3.GameStateType;

namespace GalagaGame.GalagaState {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(StateTransformer.GameStateType stateType) {
            switch (stateType) {
                case StateTransformer.GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case StateTransformer.GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case StateTransformer.GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stateType), stateType, null);
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            /*
             *
             * I have added the ability to change states. This implementation is based on the
             * little code snippet in p. 8 of the assignment text.
             *
             * -Mikael
             * 
             */
            if (eventType == GameEventType.GameStateEvent) {
                if (gameEvent.Message == "CHANGE_STATE") {
                    switch (gameEvent.Parameter1) {
                        case "GAME_RUNNING":
                            SwitchState(StateTransformer.GameStateType.GameRunning);
                            break;
                        case "GAME_MAINMENU":
                            SwitchState(StateTransformer.GameStateType.MainMenu);
                            break;
                        case "GAME_PAUSED":
                            SwitchState(StateTransformer.GameStateType.GamePaused);
                            break;
                        default:
                            throw new ArgumentException("Invalid parameter");
                    }
                }
            }
        }
    }
}
