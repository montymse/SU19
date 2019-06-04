using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;

namespace SpaceTaxi_1.GameStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            ActiveState = MainMenu.GetInstance();
            SpaceTaxiBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            
            SpaceTaxiBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        }

        
        /// <summary>
        /// Switches between the states
        /// </summary>
        /// <param name="stateType">
        /// Takes a gamestate as a argument and switches the activestate
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SwitchState(StateTransformer.GameStateType stateType) {
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

        /// <summary>
        /// Handles the gameevents of the GameStateEvent
        /// </summary>
        /// <param name="eventType">
        ///  The event type
        /// </param>
        /// <param name="gameEvent">
        ///  The game event
        /// </param>
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {

            if (eventType == GameEventType.GameStateEvent) {
                
                switch (gameEvent.Parameter1) {
                case "GAME_RUNNING":
                    
                    SwitchState((StateTransformer.TransformStringToState(gameEvent.Parameter1)));
                    
                    break;
                case "GAME_PAUSED": 
                    SwitchState((StateTransformer.TransformStringToState(gameEvent.Parameter1)));
                    break;

                case "GAME_MAINMENU":
                    SwitchState((StateTransformer.TransformStringToState(gameEvent.Parameter1)));
                    break;

                   
        
                }

            }
         
        }
    }
}