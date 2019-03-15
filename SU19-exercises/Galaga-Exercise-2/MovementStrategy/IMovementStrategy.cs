using DIKUArcade.Entities;

namespace Galaga_Exercise_1.MovementStrategy {
        public interface IMovementStrategy {
            void MoveEnemy(Enemy enemy);
            void MoveEnemies(EntityContainer<Enemy> enemies);
        }
    }

