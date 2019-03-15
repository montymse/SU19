using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    
    public class Enemy : Entity {
        private Game game;
        public Vec2F StartPosition { get; private set; }
        public Enemy(Game game, Shape shape, ImageStride image) : base(shape, image) {
            this.StartPosition = shape.Position;
        }
    }
}