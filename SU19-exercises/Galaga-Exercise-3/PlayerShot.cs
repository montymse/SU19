using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga_Exercise_3 {
    public class PlayerShot : Entity {
        private IGameState game;
        
        public PlayerShot(IGameState game, DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.game = game;
            
            shape.Direction = new Vec2F(0.0f,0.01f);
        }
    }
}