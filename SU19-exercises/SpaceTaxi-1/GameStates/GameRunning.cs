using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;

namespace SpaceTaxi_1.GameStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private Entity backGroundImage;
        private Player player;
        
        
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength;
        private Parser parser;
        
        
        private Collision col;


        public GameRunning() {
            InitializeGameState();
            

        }
        

        public void IterateShots() {


            foreach (Entity elm in parser.textureList) {
                if (CollisionDetection.Aabb(player.Entity.Shape.AsDynamicShape(),
                        elm.Shape.AsDynamicShape())
                    .Collision) {
                    explosions.RenderAnimations();
                    AddExplosion(elm.Shape.Position.X, elm.Shape.Position.Y,
                        elm.Shape.Extent.X, elm.Shape.Extent.Y);

                    player.Entity.DeleteEntity();
                }
            }


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
            player.Entity.Shape.AsDynamicShape().Direction=new Vec2F(0.0f,0.0f);

            col=new Collision();
            
            parser=new Parser(Placement.FindPlacementAndImage(
                "../../Levels/short-n-sweet.txt"));
          
            
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(500);
            explosionLength = 500;
            
            
            //Add textures
            parser.CreateEntityList();
            
            
        }
        
        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

      

        public void UpdateGameLogic() {
            IterateShots();
           
            player.Move();
            
        }

        public void RenderState() {
            backGroundImage.RenderEntity();

           if (!player.Entity.IsDeleted()) {
                player.RenderPlayer();
            }
            explosions.RenderAnimations();

            parser.textureList.RenderEntities();
     
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

