using DIKUArcade.Entities;

namespace Galaga_Exercise_1 {
    public class Player : Entity {
        private Game game;
        
        public Player(Game game, DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.game = game;
        }
    }
}