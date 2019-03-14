using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga_Exercise_1.MovementStrategy {
    public class Down : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            if (enemy.Shape.Position.Y > 0.2) {
                enemy.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, -0.001f);
                enemy.Shape.AsDynamicShape().Move();
            }
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Entity enemy in enemies) {
                MoveEnemy(enemy as Enemy);
            }
        }
    }
}