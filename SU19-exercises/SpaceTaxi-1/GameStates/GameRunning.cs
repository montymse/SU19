using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;

namespace SpaceTaxi_1.GameStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private Entity backGroundImage;
        private Player player;
        private Parser parser;
        private Collision col;

        private int ActiveLevel = 0;
        private string ActiveLevelPath = "../../Levels/short-n-sweet.txt";


        public GameRunning() {
            InitializeGameState();
        }


        public static GameRunning GetInstance0() {
             GameRunning.instance=new GameRunning();
             return GameRunning.instance;
        }

        public static GameRunning GetInstance() {

            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() { }

        public void InitializeGameState() {
            // game assets
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f),
                    new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );

            // game entities
            player = new Player();
            player.SetPosition(0.45f, 0.6f);
            player.SetExtent(0.1f, 0.1f);
            player.Entity.Shape.AsDynamicShape().Direction=new Vec2F(0.0f,0.0f);

            col=new Collision();
            
           // parser=new Parser(Placement.FindPlacementAndImage(
           //     "../../Levels/short-n-sweet.txt"
           // ));
            parser=new Parser(Placement.FindPlacementAndImage(
                ActiveLevelPath
            ));
               
            //Add textures
            parser.CreateEntityList();
            
              
        }

        public void UpdateGameLogic() {
            col.Collisions(parser.textureList,player);           
            player.Move();
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            
            parser.textureList.RenderEntities();

           if (!player.Entity.IsDeleted()) {
                player.RenderPlayer();
            }

           if (player.Entity.IsDeleted()) {
               col.explosions.RenderAnimations();
               col.GameOver.RenderText();
               
              
           }

     
        }

        public void ChangeLevel() {
            if (ActiveLevel == 0) {
                ActiveLevel = 1;
                ActiveLevelPath = "../../Levels/the-beach.txt";
            } else {
                ActiveLevel = 0;
                ActiveLevelPath = "../../Levels/short-n-sweet.txt";
            }
            InitializeGameState();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
            case "KEY_PRESS":
                switch (keyAction) {
                case "KEY_ESCAPE":
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_PAUSED", ""));
                    break;
                
                case "KEY_SPACE":

                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_MAINMENU", ""));
                
                    break;
                case "KEY_Q":
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.WindowEvent,
                            this,
                            "SAVE_SCREENSHOT",
                            "", ""));

                    break;
                case "KEY_UP": case "KEY_W":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "BOOSTER_UPWARDS",
                            "", ""));
                    break;

                case "KEY_LEFT": case "KEY_A":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "BOOSTER_TO_LEFT",
                            "", ""));
                    break;
                case "KEY_RIGHT": case "KEY_D":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "BOOSTER_TO_RIGHT",
                            "", ""));
                   break;
                
                }

                break;
            case "KEY_RELEASE":
                switch (keyAction) {
                case "KEY_LEFT": case "KEY_A":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "STOP_ACCELERATE_LEFT",
                            "", ""));
                
                    break;
                case "KEY_RIGHT": case "KEY_D":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "STOP_ACCELERATE_RIGHT",
                            "", ""));
                 
                    break;
                case "KEY_UP": case "KEY_W":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "STOP_ACCELERATE_UP",
                            "", ""));
                 
                    break;

                }
                break;
            }
        }
    }
}

