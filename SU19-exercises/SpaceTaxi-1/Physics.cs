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
        
        /// <remarks>
        /// This way of handling physics was inspired by/stolen from the YouTube video
        /// "Math for Game Developers - Jumping and Gravity (Time Delta, Game Loop)", by Jorge Rodriguez
        /// https://youtu.be/c4b9lCfSDQM
        /// 0.0015f is used as a placeholder for DeltaTime, as the deltaTime fields have been made
        /// inaccessible for some reason.
        /// </remarks>
      
        public Physics(float weight) {
            Gravity = new Vec2F(0,-weight);
            Velocity = new Vec2F(0,0);
            
            //This is a placeholder
            DeltaTime = 0.0015f;
            
            IsGrounded = false;
        }
        
        /// <summary>
        ///  Gets the velocity with the place holder deltatime
        /// </summary>
        /// <returns>
        /// the product of the velocity of the player and deltatime
        /// </returns>

        public Vec2F GetVelocity() {
            return Velocity * DeltaTime;
        }
        /// <summary>
        /// Gets the velocity
        /// </summary>
        /// <returns>
        /// Returns the velocity (without deltatime)
        /// </returns>

        public Vec2F GetRawVelocity() {
            return Velocity;
        }
        /// <summary>
        /// Updates the velocity of the player
        /// </summary>

        public void UpdateVelocity() {
            //Add gravity if the object is not grounded
            Velocity += !IsGrounded ? Gravity * DeltaTime : new Vec2F(0, 0);
            
            //If the object is falling and is grounded, stop it
            //This is to keep it from falling through the ground
            Velocity.Y = Velocity.Y < 0 && IsGrounded ? 0 : Velocity.Y;
            
            //If the object is grounded and is ascending, it's no longer grounded.
            if (IsGrounded && Velocity.Y > 0) IsGrounded = false;
        }

        
        /// <summary>
        ///  Applies forces the velocity
        /// </summary>
        /// <param name="dir">
        ///  direction
        /// </param>
        /// <param name="power">
        ///  the force/power added to the velocity in that direction
        /// </param>
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