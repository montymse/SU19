using System;

namespace SpaceTaxi_1.GameStates {
        public class StateTransformer {
        
            public enum GameStateType {
                GameRunning,
                GameRunning2,
                GamePaused,
                MainMenu
            }
            
            
            /// <summary>
            /// Transforms a string to a state
            /// </summary>
            /// <param name="state">
            /// Takes a case of string 
            /// </param>
            /// <returns>
            /// Returns the corresponding gamestate
            /// </returns>
            /// <exception cref="ArgumentException"></exception>
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

            /// <summary>
            /// Transforms a state to a string
            /// </summary>
            /// <param name="state">
            /// Takes a case of string 
            /// </param>
            /// <returns>
            /// Returns the corresponding string
            /// </returns>
            /// <exception cref="ArgumentException"></exception>
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
