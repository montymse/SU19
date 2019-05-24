using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Activation;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using SpaceTaxi_1.GameStates;

namespace SpaceTaxi_1 {
    public class Collision {
        private List<Image> explosionStrides;
        public AnimationContainer explosions;
        public Text GameOver;
        private int explosionLength;


        public Collision() {
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(500);
            explosionLength = 500;

            GameOver = new Text("Game's Over", new Vec2F(0.42f, 0.5f),
                new Vec2F(0.3f, 0.3f));
            GameOver.SetText("Game's Over\nPress Space");
            GameOver.SetColor(new Vec3I(0, 255, 0));
        }

        private void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }


        public void Collisions(EntityContainer<Entity> Entities, Player player) {
            if (player.Entity.Shape.Position.Y >= 1f) {
                GameRunning.GetInstance().ChangeLevel();
            } else {
                CollisionDetect(Entities, player);
            }
        }

        private void CollisionDetect(EntityContainer<Entity> Entities, Player player) {
            foreach (Entity elm in Entities) {
                CollisionData col =
                    CollisionDetection.Aabb(player.Entity.Shape.AsDynamicShape(),
                        elm.Shape.AsDynamicShape());

                if (col.CollisionDir==CollisionDirection.CollisionDirLeft) {
                    Console.WriteLine("X: {0} and Y: {1}",player.Entity.Shape.AsDynamicShape().Direction.X,player.Entity.Shape.AsDynamicShape().Direction.Y);
                }

               // if (col.CollisionDir==CollisionDirection.CollisionDirRight) {
              //      Console.WriteLine("X: {0} and Y: {1}",player.Entity.Shape.AsDynamicShape().Direction.X,player.Entity.Shape.AsDynamicShape().Direction.Y);
              //  }
                if (col.Collision) {
                    //Landing
                    if (col.CollisionDir == CollisionDirection.CollisionDirDown 
                         && player.Entity.Shape.AsDynamicShape().Direction.Y >= -0.005f 
                    
                         
                         
                   ) {
                        player.physics.IsGrounded = true;
                        
                        



                        //Collision with an obstacle. Taxi dies. 
                    } else {
                        Tuple<float, float> position =
                            new Tuple<float, float>(player.Entity.Shape.Position.X,
                                player.Entity.Shape.Position.Y);
                        Tuple<float, float> extent =
                            new Tuple<float, float>(player.Entity.Shape.Extent.X,
                                player.Entity.Shape.Extent.Y);

                        AddExplosion(position.Item1, position.Item2,
                            extent.Item1, extent.Item2);

                        player.Entity.Shape.AsDynamicShape().Direction=new Vec2F(0f,0f);
                        player.Entity.DeleteEntity();
                    }
                }
            }
        }
        
    }
    
    /*
     *
     *  || col.CollisionDir == CollisionDirection.CollisionDirUnchecked  
                         && player.Entity.Shape.AsDynamicShape().Direction.Y >= 0.05f 
                         && player.Entity.Shape.AsDynamicShape().Direction.X <= 1f
     */
}