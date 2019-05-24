using System;

namespace SpaceTaxi_1.GameStates {
        public class StateTransformer {
        
            public enum GameStateType {
                GameRunning,
                GameRunning2,
                GamePaused,
                MainMenu
            }
            public static GameStateType TransformStringToState(string state) {

                switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "GAME_MAINMENU":
                    return GameStateType.MainMenu;
                default:
                    throw new ArgumentException("ERROR");
                } 
            }

            public static string TransformStateToString(GameStateType state) {
                switch (state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "GAME_MAINMENU";
                default:
                    throw new ArgumentException("ERROR");
                }        
            }
        
        }
    }
