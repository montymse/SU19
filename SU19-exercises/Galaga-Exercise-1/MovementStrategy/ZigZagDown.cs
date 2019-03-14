using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;

namespace Galaga_Exercise_1.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        private float Speed;
        private float Amplitude;
        private float Period;

        public ZigZagDown(float speed, float a, float p) {
            this.Speed = speed;
            this.Amplitude = a;
            this.Period = p;
        }
        
        public void MoveEnemy(Enemy enemy) {
            float newY = enemy.Shape.Position.Y + Speed;
            float newX = (float) (enemy.StartPosition.X + Amplitude *
                                  Math.Sin((2 * Math.PI * (enemy.StartPosition.Y - newY)) / Period));
            enemy.Shape.AsDynamicShape().Direction = new Vec2F(newX,newY);
            enemy.Shape.AsDynamicShape().Move();
           enemy.Shape.AsDynamicShape().Position = new Vec2F(newX,newY);
        }
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Entity enemy in enemies) {
                MoveEnemy(enemy as Enemy);
            }
        }
    }
}