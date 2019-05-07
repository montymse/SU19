using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace SpaceTaxi_1 {
    public class Player : IGameEventProcessor<object> {
        private readonly Image taxiBoosterOffImageLeft;
        private readonly Image taxiBoosterOffImageRight;
        private readonly DynamicShape shape;
        private Orientation taxiOrientation;

        public Player() {
            shape = new DynamicShape(new Vec2F(), new Vec2F());
            taxiBoosterOffImageLeft =
                new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None.png"));
            taxiBoosterOffImageRight =
                new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None_Right.png"));

            Entity = new Entity(shape, taxiBoosterOffImageLeft);
        }

        public Entity Entity { get; }

        public void SetPosition(float x, float y) {
            shape.Position.X = x;
            shape.Position.Y = y;
        }

        public void SetExtent(float width, float height) {
            shape.Extent.X = width;
            shape.Extent.Y = height;
        }

        public void RenderPlayer() {
            //TODO: Next version needs animation. Skipped for clarity.
            Entity.Image = taxiOrientation == Orientation.Left
                ? taxiBoosterOffImageLeft
                : taxiBoosterOffImageRight;
            Entity.RenderEntity();
        }


        
        public void Move() {   
            if(Entity.Shape.Position.X > 0 && Entity.Shape.Position.X < 1-Entity.Shape.Extent.X
            && Entity.Shape.Position.Y > 0 && Entity.Shape.Position.Y < 1-Entity.Shape.Extent.Y) {
                this.Entity.Shape.Move(this.Entity.Shape.AsDynamicShape().Direction);
            }
            
            else if(Entity.Shape.Position.X <= 0  && 
                    (Entity.Shape.AsDynamicShape().Direction.X > 0 || 
                     Entity.Shape.AsDynamicShape().Direction.Y!=0 && 
                     Entity.Shape.AsDynamicShape().Direction.X==0 )) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);
            }
            else if(Entity.Shape.Position.X >= 1-Entity.Shape.Extent.X &&
                    (Entity.Shape.AsDynamicShape().Direction.X < 0 || 
                     Entity.Shape.AsDynamicShape().Direction.Y!=0 &&
                     Entity.Shape.AsDynamicShape().Direction.X==0
                    )) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);
            }
            
            else if(Entity.Shape.Position.Y <= 0  && 
                    (Entity.Shape.AsDynamicShape().Direction.Y > 0 || 
                     Entity.Shape.AsDynamicShape().Direction.X!=0 && 
                     Entity.Shape.AsDynamicShape().Direction.Y==0 )) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);
            }
            else if(Entity.Shape.Position.Y >= 1-Entity.Shape.Extent.Y &&
                    (Entity.Shape.AsDynamicShape().Direction.Y < 0 || 
                     Entity.Shape.AsDynamicShape().Direction.X!=0 &&
                     Entity.Shape.AsDynamicShape().Direction.Y==0
                    )) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);
            }
            /*
            else if (Entity.Shape.Position.Y <= 0  &&
                     Entity.Shape.Position.X <= 0  && 
                     (Entity.Shape.AsDynamicShape().Direction.X > 0 || 
                      Entity.Shape.AsDynamicShape().Direction.Y > 0)) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);

            }
            else if (Entity.Shape.Position.Y >= 1-Entity.Shape.Extent.Y &&
                     Entity.Shape.Position.X >= 1-Entity.Shape.Extent.X && 
                     (Entity.Shape.AsDynamicShape().Direction.X < 0 || 
                      Entity.Shape.AsDynamicShape().Direction.Y < 0)) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);

            }
            
            else if (Entity.Shape.Position.Y <= 0 + Entity.Shape.Extent.Y &&
                     Entity.Shape.Position.X >= 1-Entity.Shape.Extent.X && 
                     (Entity.Shape.AsDynamicShape().Direction.X > 0 || 
                      Entity.Shape.AsDynamicShape().Direction.Y < 0)) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);

            }
            else if (Entity.Shape.Position.Y >= 1-Entity.Shape.Extent.Y &&
                     Entity.Shape.Position.X <= 0 + Entity.Shape.Extent.X && 
                     (Entity.Shape.AsDynamicShape().Direction.X < 0 || 
                      Entity.Shape.AsDynamicShape().Direction.Y > 0)) {
                Entity.Shape.Move(Entity.Shape.AsDynamicShape().Direction);

            }*/
            
            
            
        }
        
        private void Direction(Vec2F dir) {
            Entity.Shape.AsDynamicShape().Direction = dir;
        }


        private void Booster(GameEvent<object> gameEvent) {
            switch (gameEvent.Message) {
            case "BOOSTER_TO_LEFT":
                Direction(new Vec2F(-0.01f,0.00f));
                break;
            case "BOOSTER_TO_RIGHT":
                Direction(new Vec2F(0.01f, 0.00f));
                break;
            case "BOOSTER_UPWARDS":
                Direction(new Vec2F(0.00f, 0.01f));
                break;

            }
        }
       
        private void Release() {
            Direction(new Vec2F(0.0f,-0.001f));
            
        }
       

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "BOOSTER_TO_LEFT": case "BOOSTER_TO_RIGHT":  case "BOOSTER_UPWARDS":
                    Booster(gameEvent);
               
                    break;
                case "STOP_ACCELERATE_LEFT": case "STOP_ACCELERATE_UP" :
                case "STOP_ACCELERATE_RIGHT" : 
                    Release();
                    break;
                }
            } 
        }
    }
}