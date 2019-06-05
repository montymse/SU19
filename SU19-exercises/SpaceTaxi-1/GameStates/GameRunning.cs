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
using SpaceTaxi_1.Customer;
using SpaceTaxi_1.TaxiTour;

namespace SpaceTaxi_1.GameStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private Entity backGroundImage;
        private Player player;
        private Parser parser;
        private Collision col;
        private CustomerPoints score;
        private CustomerEntity customer;
        private PickUp taxiTour;

        private int activeLevel = 0;
        private string activeLevelPath = "../../Levels/short-n-sweet.txt";
        private string inactiveLevelPath = "../../Levels/the-beach.txt";


        public GameRunning() {
            InitializeGameState();
        }

        /// <summary>
        /// This function is used to reset the gamerunning instance. 
        /// </summary>
        /// <returns>
        /// Sets gamerunning to null and returns.
        /// </returns>
        public static GameRunning GetInstance0() {
            GameRunning.instance = null;
            return GameRunning.instance;
        }

        /// <summary>
        ///  Creates a instance of GameRunning is there is not one already
        /// </summary>
        /// <returns>
        /// return an instance of GameRunning
        /// </returns>
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
            player.SetExtent(0.06f, 0.06f);

            col = new Collision(activeLevelPath);

            parser = new Parser(Placement.FindPlacementAndImage(
                activeLevelPath
            ));

            //Add textures
            parser.CreateEntityList();

            customer = new CustomerEntity(activeLevelPath);

            score = new CustomerPoints(customer);
            
            taxiTour=new PickUp();
        }


        public void UpdateGameLogic() {
            col.Collisions(parser.textureList, player);
            taxiTour.TaxiTour(player, customer);

            if (!player.Entity.IsDeleted()) {
                player.Move();
            }
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            score.RenderScore();
            parser.textureList.RenderEntities();


            if (!player.Entity.IsDeleted()) {
                player.RenderPlayer();
            }

            if (!customer.pickedUp && customer.CountHasExpired()) {
                customer.Entity.RenderEntity();
            }


            if (player.Entity.IsDeleted()) {
                col.explosions.RenderAnimations();
                col.GameOver.RenderText();
            }
        }

        /// <summary>
        /// The function changes between the two levels
        /// </summary>
        public void ChangeLevel() {
            if (activeLevel == 0) {
                activeLevel = 1;
                activeLevelPath = "../../Levels/the-beach.txt";
                inactiveLevelPath = "../../Levels/short-n-sweet.txt";
            } else {
                activeLevel = 0;
                activeLevelPath = "../../Levels/short-n-sweet.txt";
                inactiveLevelPath = "../../Levels/the-beach.txt";

            }
            
            player = new Player();
            player.SetPosition(0.45f, 0.6f);
            player.SetExtent(0.06f, 0.06f);

            col = new Collision(activeLevelPath);

            parser = new Parser(Placement.FindPlacementAndImage(
                activeLevelPath
            ));

            //Add textures
            parser.CreateEntityList();

            customer = new CustomerEntity(activeLevelPath);
            

            
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
            case "KEY_PRESS":
                switch (keyAction) {
                case "KEY_ESCAPE":
                    SpaceTaxiBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_PAUSED", ""));
                    break;

                case "KEY_SPACE":
                    GameRunning.GetInstance0();
                    SpaceTaxiBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_MAINMENU", ""));

                    break;
                case "KEY_Q":
                    SpaceTaxiBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.WindowEvent,
                            this,
                            "SAVE_SCREENSHOT",
                            "", ""));

                    break;
                case "KEY_UP":
                case "KEY_W":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "BOOSTER_UPWARDS",
                            "", ""));
                    break;

                case "KEY_LEFT":
                case "KEY_A":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "BOOSTER_TO_LEFT",
                            "", ""));
                    break;
                case "KEY_RIGHT":
                case "KEY_D":
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
                case "KEY_A":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "STOP_ACCELERATE_LEFT",
                            "", ""));

                    break;
                case "KEY_RIGHT":
                case "KEY_D":
                    player.ProcessEvent(GameEventType.PlayerEvent,
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "STOP_ACCELERATE_RIGHT",
                            "", ""));

                    break;
                case "KEY_UP":
                case "KEY_W":
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