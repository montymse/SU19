using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

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
        
        //These two are used for animations
        private bool LeftOrRightBoosterActive;
        private bool BottomBoosterActive;
        
        private readonly DynamicShape shape;
        private Orientation taxiOrientation;
        
        private Vec2F gravityVector = new Vec2F(0.0f,-1.5f);
        private Vec2F velocityVector = new Vec2F(0.0f,0.0f);

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
           // Entity.Image = taxiOrientation == Orientation.Left
           //     ? taxiBoosterOffImageLeft
           //     : taxiBoosterOffImageRight;
           // Entity.RenderEntity();
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
                Entity.Image = taxiOrientation == Orientation.Left
                    ? taxiBoosterOffImageLeft
                    : taxiBoosterOffImageRight;
            }
            Entity.RenderEntity();
        }


        
        public void Move() {   
           /*
            * 
            * This way of handling physics was inspired by/stolen from the YouTube video
            * "Math for Game Developers - Jumping and Gravity (Time Delta, Game Loop)", by Jorge Rodriguez
            * https://youtu.be/c4b9lCfSDQM
            * 
            * I would like values to continuously be added to the velocity vector as the buttons are being held.
            * Right now the values are only being added on button press, meaning that one has to keep
            * tapping the arrow keys in order to accelerate.
            * 
            * 0.0015f is used as a placeholder for DeltaTime, as I have yet to find a way to pass this value
            * to the Move() method.
            *
            * -Mikael
            * 
            */
           
           //Entity.Shape.Position = Entity.Shape.Position + velocityVector * 0.0015f;
           Entity.Shape.AsDynamicShape().Direction = velocityVector * 0.0015f;
           Entity.Shape.AsDynamicShape().Move();
           //Entity.Shape.Position = Entity.Shape.Position + new Vec2F(0.001f, 0.0f);
           velocityVector = velocityVector + gravityVector  * 0.0015f;
           //Console.WriteLine(velocityVector);
           //Console.WriteLine(Entity.Shape.Position);


        }
        /// <summary>
        /// Basic implementation of the player movement. It determines t
        /// he direction of the entity to be the same as the vector given in the argument
        /// </summary>
        /// <param name="dir">
        /// A vector holding information about the new direction of the entity
        /// </param>
        
        private void Direction(Vec2F dir) {
            Entity.Shape.AsDynamicShape().Direction = dir;
        }

        /// <summary>
        /// Given a gameEvent it matches messages of the gameEvent using a switch statement
        /// and finds the corresponding case and changes the direction of this entity 
        /// </summary>
        /// <param name="gameEvent">
        /// Booster takes a gameEvent as argument, this is given by the processEvent method in which
        /// Booster is called. 
        /// </param>

        private void Booster(GameEvent<object> gameEvent) {
            switch (gameEvent.Message) {
            case "BOOSTER_TO_LEFT":
                //Direction(new Vec2F(-0.01f,0.00f));
                velocityVector += new Vec2F(-1f,0.0f);
                break;
            case "BOOSTER_TO_RIGHT":
                //Direction(new Vec2F(0.01f, 0.00f));
                velocityVector += new Vec2F(1f,0.0f);
                break;
            case "BOOSTER_UPWARDS":
                //Direction(new Vec2F(0.00f, 0.01f));
                velocityVector += new Vec2F(0.0f,1f);
                break;

            }
        }
        
        /// <summary>
        /// Basic implementation of the player movement. It changes the direction of
        /// the entity to go downwards.
        /// </summary>
       
        private void Release(string gameEventMsg) {
            //Direction(new Vec2F(0.0f,-0.001f));
            switch (gameEventMsg) {
                case "STOP_ACCELERATE_LEFT":
                    //velocityVector.X = 0.0f;
                    break;
                case "STOP_ACCELERATE_RIGHT":
                    //velocityVector.X = 0.0f;
                    break;
                case "STOP_ACCELERATE_UP":
                    velocityVector.Y = 0.0f;
                    break;
            }
        }
       
        /// <summary>
        /// Basic implementation of the event processor. It holds the responsibility of moving
        /// the player 
        /// </summary>
        /// <param name="eventType"></param>
        /// The gameEventType 
        /// <param name="gameEvent"></param>
        /// The gameEvent
        /// 

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "BOOSTER_TO_LEFT": case "BOOSTER_TO_RIGHT":  case "BOOSTER_UPWARDS":
                   Booster(gameEvent);
                   
                   //Handle animations
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
                   }
                   
                   break;
                case "STOP_ACCELERATE_LEFT": case "STOP_ACCELERATE_UP" :
                case "STOP_ACCELERATE_RIGHT" : 
                    LeftOrRightBoosterActive = false;
                    BottomBoosterActive = false;
                    Release(gameEvent.Message);
                    break;
                }
            } 
        }
    }
}