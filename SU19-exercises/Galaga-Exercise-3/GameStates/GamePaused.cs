using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga_Exercise_3.GameStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        private Entity pauseImage;
        private Text[] pauseButtons;
        private int activePauseButton;
        private int maxMenuButtons;
        public GamePaused() {
           InitializeGameState();
        }

        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        public void GameLoop() {
            throw new System.NotImplementedException();
        }

        public void InitializeGameState() {
            pauseImage = new Entity(new StationaryShape(0,0,500,500),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            pauseButtons = new[] {
                new Text("Continue", new Vec2F(0.6f, 0.6f), new Vec2F(0.2f, 0.2f)),
                new Text("Main Menu", new Vec2F(0.4f, 0.4f), new Vec2F(0.2f, 0.2f))
            };
            activePauseButton = 0;
        }

        public void UpdateGameLogic() {
        }

        public void RenderState() {
            pauseImage.RenderEntity();
            pauseButtons[0].SetText(pauseButtons[0].ToString());
            pauseButtons[0].SetColor(new Vec3I(255, 0, 0));
            pauseButtons[0].RenderText();
            pauseButtons[1].SetText(pauseButtons[0].ToString());
            pauseButtons[1].SetColor(new Vec3I(0, 0, 255));
            pauseButtons[1].RenderText();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_UP":
                if (activePauseButton == 0) {
                    activePauseButton = 1;
                } else {
                    activePauseButton = 0;
                }
                break;
            case "KEY_DOWN": 
                if (activePauseButton == 1) {
                    activePauseButton = 0;
                } else {
                    activePauseButton = 1;
                }
                break;
            
            case "KEY_ENTER":
                if (activePauseButton == 0) {
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent,
                        this,
                        "CHANGE_STATE",
                        "GAME_RUNNING", "");
                } else
                {
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent,
                        this,
                        "CHANGE_STATE",
                        "GAME_MAINMENU", "");
                }
                
                break;
            
            default:
                break;
            
            }
        }
    }
}