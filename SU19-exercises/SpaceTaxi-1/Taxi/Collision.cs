using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

namespace SpaceTaxi_1 {
    public class Collision {
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private Text GameOver;
        private TimedEvent TimeOfCollision;


        public Collision() {
            
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(500);
            GameOver=new Text("Game's Over", new Vec2F(0.42f, 0.5f),
                new Vec2F(0.3f, 0.3f));
            GameOver.SetText("Game's Over");
            GameOver.SetColor(new Vec3I(0, 255, 0));
            TimeOfCollision = new TimedEvent(TimeSpanType.Seconds, 3, 
                "Game's over", "","");
        }
        
        
        private int explosionLength = 500;

        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }
        
        
        //Landing on platform. If 

        //Collision with an obstacle. Taxi dies. 
        public void CollisionDetect(List<Entity> Entities, Player player) {
            foreach (var elm in Entities) {
                //Console.WriteLine("elm, {0}",elm.Shape.Position);
                //Console.WriteLine("player: {0}",player.Entity.Shape.Position);
                
                if (CollisionDetection.Aabb(player.Entity.Shape.AsDynamicShape(),elm.Shape)
                    .Collision) {
                    Console.WriteLine("COLLIDEDEEED");
                    //Viser eksplosion
                    explosions.RenderAnimations();
                    AddExplosion(player.Entity.Shape.Position.X, player.Entity.Shape.Position.Y,
                        player.Entity.Shape.Extent.X, player.Entity.Shape.Extent.Y);

                    //Slette spiler
                    player.Entity.DeleteEntity();
                    
                    //Skriver Game Over på skærm
                    GameOver.RenderText();
                    //Genstarter timer
                    TimeOfCollision.ResetTimer();
                    
                    //3 sekunder efter at have skrevet Game Over så går vi over til Main Menu  
                    if (TimeOfCollision.HasExpired()) {
                        GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_MAINMENU", ""));              
                    }
                    
                }
            }
        }
        
    }
}