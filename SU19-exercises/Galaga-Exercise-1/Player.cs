using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : Entity {
        private Game game;
        
        public Player(Game game, DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.game = game;
        }

        public void Direction(Vec2F dir) {
            this.Shape.AsDynamicShape().Direction = dir;
        }

        public void Move() {
            //our player will only move left and right, so we only check X values
            if(this.Shape.Position.X > 0.01 && this.Shape.Position.X < 1-this.Shape.Extent.X) {
                this.Shape.Move(this.Shape.AsDynamicShape().Direction);
            }
            else if(this.Shape.Position.X <= 0 + this.Shape.Extent.X && this.Shape.AsDynamicShape().Direction.X > 0) {
                this.Shape.Move(this.Shape.AsDynamicShape().Direction);
            }
            else if(this.Shape.Position.X >= 1-this.Shape.Extent.X && this.Shape.AsDynamicShape().Direction.X < 0) {
                this.Shape.Move(this.Shape.AsDynamicShape().Direction);
            }
        }
    }
}