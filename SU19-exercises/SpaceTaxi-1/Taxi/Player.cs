using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1 {
    public class Player : IGameEventProcessor<object> {
        private readonly Image taxiBoosterOffImageLeft;
        private readonly Image taxiBoosterOffImageRight;
        private ImageStride taxiBoosterOnRight;
        private ImageStride taxiBoosterOnLeft;
        private ImageStride taxiBoosterOnBottomBackRight;
        private ImageStride taxiBoosterOnBottomBackLeft;
        private ImageStride taxiBoosterOnBottom;
        private ImageStride taxiBoosterOnBottomRight;
        private ImageStride taxiBoosterOnBottomLeft;
        
        //Booster flags
        private bool LeftOrRightBoosterActive;
        private bool BottomBoosterActive;
        
        private float BoostPower = 0.2f;
        
        private readonly DynamicShape shape;
        private Orientation taxiOrientation;
        
        //public Vec2F gravityVector = new Vec2F(0.0f,-40f);
        //public Vec2F velocityVector = new Vec2F(0.0f,0.0f);
        public Physics physics;
        

        public Player() {
            shape = new DynamicShape(new Vec2F(), new Vec2F());
            
            taxiBoosterOffImageLeft =
                new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None.png"));
            taxiBoosterOffImageRight =
                new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None_Right.png"));
            
            taxiBoosterOnLeft = new ImageStride(10, ImageStride.CreateStrides(
                2,Path.Combine("Assets","Images","Taxi_Thrust_Back.png")));
            taxiBoosterOnRight = new ImageStride(10, ImageStride.CreateStrides(
                2,Path.Combine("Assets","Images","Taxi_Thrust_Back_Right.png")));
            taxiBoosterOnBottomBackLeft = new ImageStride(10, ImageStride.CreateStrides(
                2,Path.Combine("Assets","Images","Taxi_Thrust_Bottom_Back.png")));
            taxiBoosterOnBottomBackRight = new ImageStride(10, ImageStride.CreateStrides(
                2,Path.Combine("Assets","Images","Taxi_Thrust_Bottom_Back_Right.png")));
            taxiBoosterOnBottomLeft = new ImageStride(10, ImageStride.CreateStrides(
                2,Path.Combine("Assets","Images","Taxi_Thrust_Bottom.png")));
            taxiBoosterOnBottomRight = new ImageStride(10, ImageStride.CreateStrides(
                2,Path.Combine("Assets","Images","Taxi_Thrust_Bottom_Right.png")));
    
            BottomBoosterActive = false;
            LeftOrRightBoosterActive = false;
            
            physics = new Physics(40);

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
        
        /// <summary>
        /// Renders the player with correct orientation
        /// </summary>

        public void RenderPlayer() {
          
            if (BottomBoosterActive && LeftOrRightBoosterActive) {
                Entity.Image = taxiOrientation == Orientation.Left
                    ? taxiBoosterOnBottomBackLeft
                    : taxiBoosterOnBottomBackRight;
            }
            else if (BottomBoosterActive) {
                Entity.Image = taxiOrientation == Orientation.Left
                    ? taxiBoosterOnBottomLeft
                    : taxiBoosterOnBottomRight;
            }
            else if (LeftOrRightBoosterActive) {
                Entity.Image = taxiOrientation == Orientation.Left
                    ? taxiBoosterOnLeft
                    : taxiBoosterOnRight;
            } else {
                //No booster is active
                Entity.Image = taxiOrientation == Orientation.Left
                    ? taxiBoosterOffImageLeft
                    : taxiBoosterOffImageRight;
            }
            Entity.RenderEntity();
        }

        /// <summary>
        /// Moves the player around in with the given velocity and direction
        /// </summary>
        
        public void Move() {   
            
           //Engage the thrusters
           Booster();
           
          Entity.Shape.AsDynamicShape().Direction = physics.GetVelocity();
          Entity.Shape.AsDynamicShape().Move();
          physics.UpdateVelocity();
        }
        
        /// <summary>
        /// Activates the booster
        /// </summary>
        private void Booster() {
            if (LeftOrRightBoosterActive) {
               switch (taxiOrientation) {
                   case Orientation.Left:
                       physics.ApplyForce(Physics.ForceDirection.Left,BoostPower);
                       break;
                   case Orientation.Right:
                       physics.ApplyForce(Physics.ForceDirection.Right,BoostPower);
                       break;
               }
            }
            if (BottomBoosterActive) {
                //velocityVector += new Vec2F(0.0f, BoostPower);
               physics.ApplyForce(Physics.ForceDirection.Up,BoostPower);
            }
        }
        
       
        /// <summary>
        /// Event processor. It changes the booster flags and taxi orientation based on user input 
        /// </summary>
        /// <param name="eventType">
        /// The gameEventType
        /// </param>
        /// <param name="gameEvent">
        /// The gameEvent
        /// </param>

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "BOOSTER_TO_LEFT":
                        taxiOrientation = Orientation.Left;
                        LeftOrRightBoosterActive = true;
                        break;
                    case "BOOSTER_TO_RIGHT":
                        taxiOrientation = Orientation.Right;
                        LeftOrRightBoosterActive = true;
                        break;
                    case "BOOSTER_UPWARDS":
                        BottomBoosterActive = true;
                        break;
                    case "STOP_ACCELERATE_LEFT": case "STOP_ACCELERATE_RIGHT" : 
                        LeftOrRightBoosterActive = false;
                        break;
                    case "STOP_ACCELERATE_UP" :
                        BottomBoosterActive = false;
                        break;
                }
            } 
        }
    }
}
