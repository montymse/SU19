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
    public class GameRunning2 : IGameState {
        private static GameRunning2 instance = null;

        private Entity backGroundImage;
        private Player player;
        private Parser parser;
        private Collision col;


        public GameRunning2() {
            InitializeGameState();
        }

        public static GameRunning2 GetInstance() {

            return GameRunning2.instance ?? (GameRunning2.instance = new GameRunning2());
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
            
            parser=new Parser(Placement.FindPlacementAndImage(
                "../../Levels/the-beach.txt"
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

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
            case "KEY_PRESS":
                switch (keyAction) {
                case "KEY_ESCAPE":
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.WindowEvent,
                            this,
                            "CLOSE_WINDOW",
                            "", ""));
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
                case "KEY_UP":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "BOOSTER_UPWARDS",
                            "", ""));
                    break;

                case "KEY_LEFT":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "BOOSTER_TO_LEFT",
                            "", ""));
                    break;
                case "KEY_RIGHT":
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
                case "KEY_LEFT":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "STOP_ACCELERATE_LEFT",
                            "", ""));
                
                    break;
                case "KEY_RIGHT":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "STOP_ACCELERATE_RIGHT",
                            "", ""));
                 
                    break;
                case "KEY_UP":
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

