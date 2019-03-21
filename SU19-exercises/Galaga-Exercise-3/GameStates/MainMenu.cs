using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga_Exercise_3;

namespace GalagaGame.GalagaState {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

    public MainMenu() {
       backGroundImage=new Entity(new StationaryShape(0,0,500,500),new Image(Path.Combine("Assets", "Images",
           "TitleImage.png")));
       menuButtons = new[] {
           new Text("new game", new Vec2F(0.6f, 0.6f), new Vec2F(0.2f, 0.2f)),
           new Text("quit", new Vec2F(0.4f, 0.4f), new Vec2F(0.2f, 0.2f))
       };
       activeMenuButton = 0;
    }
    
    public static MainMenu GetInstance() {
    return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
    }

    public void GameLoop() {
        throw new System.NotImplementedException();
    }

    public void InitializeGameState() {
        throw new System.NotImplementedException();
    }

    public void UpdateGameLogic() {
        throw new System.NotImplementedException();
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        menuButtons[0].SetText(menuButtons[0].ToString());
        menuButtons[0].SetColor(new Vec3I(255, 0, 0));
        menuButtons[0].RenderText();
        menuButtons[1].SetText(menuButtons[0].ToString());
        menuButtons[1].SetColor(new Vec3I(0, 0, 255));
        menuButtons[1].RenderText();
    }

    public void HandleKeyEvent(string keyValue, string keyAction) {
        switch (keyAction) {
            case "KEY_UP":
                if (activeMenuButton == 0) {
                    activeMenuButton = 1;
                } else {
                    activeMenuButton = 0;
                }
                break;
            case "KEY_DOWN": 
                if (activeMenuButton == 1) {
                    activeMenuButton = 0;
                } else {
                    activeMenuButton = 1;
                }
                break;
            
            case "KEY_ENTER":
                if (activeMenuButton == 0) {
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent,
                        this,
                        "CHANGE_STATE",
                        "GAME_RUNNING", "");
                } else
                    {
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.WindowEvent,
                            this,
                            "CLOSE_WINDOW",
                            "", "");
                    }
                
                break;
            
            default:
                    break;
            
        }
    }
         }
}