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
            throw new System.NotImplementedException();
        }
    }
}
