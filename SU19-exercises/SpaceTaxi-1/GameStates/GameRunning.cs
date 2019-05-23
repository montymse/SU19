using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace SpaceTaxi_1.GameStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private Entity backGroundImage;
        private Player player;
        private List<Entity> textureList;
        private Collision col;


        public GameRunning() {
            InitializeGameState();
            col=new Collision();

        }

        public static GameRunning GetInstance0() {

            return GameRunning.instance = new GameRunning();
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
            backGroundImage.RenderEntity();

            // game entities
            player = new Player();
            player.SetPosition(0.45f, 0.6f);
            player.SetExtent(0.1f, 0.1f);


            //Add textures
            textureList = Parser.CreateEntityList(Placement.FindPlacementAndImage(
                "../../Levels/short-n-sweet.txt"));
        }

        public void UpdateGameLogic() {
            player.Move();
            col.CollisionDetect(textureList,player);
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            player.RenderPlayer();

            foreach (Entity elm in textureList) {
                elm.RenderEntity();
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

