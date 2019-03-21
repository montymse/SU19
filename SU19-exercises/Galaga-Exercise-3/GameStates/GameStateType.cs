
namespace Galaga_Exercise_1.GameStateType {
    using System;

    
    public class StateTransformer {
        public enum GameStateType {
            GameRunning,
            GamePaused,
            MainMenu
        }

        public static GameStateType TransformStringToState(string state) {

            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                    break;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                    break;
                case "GAME_MAINMENU":
                    return GameStateType.MainMenu;
                    break;
                default:
                    throw new ArgumentException("ERROR");
                    break;  
            } 
        }

        public static string TransformStateToString(GameStateType state) {
            switch (state) {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
                break;

            case GameStateType.GamePaused:
                return "GAME_PAUSED";
                break;
            case GameStateType.MainMenu:
                return "GAME_MAINMENU";
                break;
            default:
                throw new ArgumentException("ERROR");
                break;
            }        
        }
        
    }
}