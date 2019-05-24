using DIKUArcade.Math;

namespace SpaceTaxi_1 {
    public class Physics {
        private Vec2F Gravity;
        private Vec2F Velocity;
        private float DeltaTime;
        public  bool IsGrounded;
        
        public enum ForceDirection {
            Up,
            Down,
            Left,
            Right
        }

      /*
       * 
       * This way of handling physics was inspired by/stolen from the YouTube video
       * "Math for Game Developers - Jumping and Gravity (Time Delta, Game Loop)", by Jorge Rodriguez
       * https://youtu.be/c4b9lCfSDQM
       * 
       * 0.0015f is used as a placeholder for DeltaTime, as the deltaTime fields have been made
       * inaccessible for some reason.
       *
       * -Mikael
       * 
       */
        public Physics(float weight) {
            Gravity = new Vec2F(0,-weight);
            Velocity = new Vec2F(0,0);
            
            //This is a placeholder
            DeltaTime = 0.0015f;

            IsGrounded = false;
        }

        public Vec2F GetVelocity() {
            return Velocity * DeltaTime;
        }

        public Vec2F GetRawVelocity() {
            return Velocity;
        }

        public void UpdateVelocity() {
            Velocity += !IsGrounded ? Gravity * DeltaTime : new Vec2F(0, 0);
            Velocity.Y = Velocity.Y < 0 && IsGrounded ? 0 : Velocity.Y;
            if (IsGrounded && Velocity.Y > 0) IsGrounded = false;
        }

        public void ApplyForce(ForceDirection dir, float power) {
            switch (dir) {
                case ForceDirection.Up:
                    Velocity += new Vec2F(0,power);
                    break;
                case ForceDirection.Down:
                    Velocity += new Vec2F(0,-power);
                    break;
                case ForceDirection.Left:
                    Velocity += new Vec2F(-power,0);
                    break;
                case ForceDirection.Right:
                    Velocity += new Vec2F(power,0);
                    break;
            }
        }
    }
}