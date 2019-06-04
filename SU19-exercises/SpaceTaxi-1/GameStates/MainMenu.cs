using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace SpaceTaxi_1.GameStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;


    public MainMenu() {
     InitializeGameState();     
    }
    
    public static MainMenu GetInstance() {
    return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
    }

    public void GameLoop() { }

    public void InitializeGameState() {
        backGroundImage = new Entity(
            new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
        );
        
        menuButtons = new[] {
            new Text("new game", new Vec2F(0.4f, 0.5f), new Vec2F(0.3f, 0.3f)),
            new Text("quit", new Vec2F(0.45f, 0.3f), new Vec2F(0.3f, 0.3f))
        };
        activeMenuButton = 0;
        
        menuButtons[0].SetText("New Game");
        menuButtons[0].SetColor(new Vec3I(255, 0, 0));
        
        menuButtons[1].SetText("Quit");
        menuButtons[1].SetColor(new Vec3I(0, 255, 0));
    }

    public void UpdateGameLogic() {

    }

    public void RenderState() {
        
        backGroundImage.RenderEntity();
        
        menuButtons[0].RenderText();
      
        menuButtons[1].RenderText();
        
    }

    public void HandleKeyEvent(string keyValue, string keyAction) {
        switch (keyValue) {                
                
        case "KEY_PRESS":
            switch (keyAction) {
            case "KEY_DOWN": case "KEY_S":
                if (activeMenuButton == 0) {
                    activeMenuButton = 1;
                } 
                break;
            case "KEY_UP": case "KEY_W":
                if (activeMenuButton == 1) {
                    activeMenuButton = 0;
                } 
                break;
            
            case "KEY_ENTER":
                if (activeMenuButton == 0) {
                   SpaceTaxiBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent,
                        this,
                        "CHANGE_STATE",
                        "GAME_RUNNING", ""));
                } else
                {
                    SpaceTaxiBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent,
                        this,
                        "CLOSE_WINDOW",
                        "", ""));     
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