using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace SpaceTaxi_1.GameStates {
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

        public void GameLoop() {}

        public void InitializeGameState() {
    

            pauseImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );
          
            pauseButtons = new[] {
                new Text("Continue", new Vec2F(0.42f, 0.5f), new Vec2F(0.3f, 0.3f)),
                new Text("Main Menu", new Vec2F(0.4f, 0.3f), new Vec2F(0.3f, 0.3f)),
            };
            activePauseButton = 0;
            
        
            pauseButtons[0].SetText("Continue");
            pauseButtons[0].SetColor(new Vec3I(255, 0, 0));
            
            pauseButtons[1].SetText("Main Menu");
            pauseButtons[1].SetColor(new Vec3I(0, 255, 0));
        }

        public void UpdateGameLogic() {
        }

        public void RenderState() {

            pauseImage.RenderEntity();
            
            pauseButtons[0].RenderText();
          
            pauseButtons[1].RenderText();
            
            
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
            case "KEY_PRESS":
                switch (keyAction) {
                case "KEY_UP": case "KEY_W":
                    if (activePauseButton == 1) {
                        activePauseButton = 0;
                    } 
                    break;
                case "KEY_DOWN": case "KEY_S":
                    if (activePauseButton == 0) {
                        activePauseButton = 1;
                    } 
                    break;
            
                case "KEY_ENTER":
                    if (activePauseButton == 0) {
                        SpaceTaxiBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_RUNNING", ""));
                    } else {
                        GameRunning.GetInstance0();
                        SpaceTaxiBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_MAINMENU", ""));
                    }                
                    break;
            
           
                }
                    break;
            
            case "KEY_RELEASE":
                break;   
            }
           
                     
        }
        
    }
}