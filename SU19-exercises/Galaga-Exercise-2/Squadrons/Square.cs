using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1.Squadrons {
    public class Square {
          
        //Setter added to allow for enemy deletion
        public EntityContainer<Enemy> enemies { get; set; }
        public int MaxEnemies { get; }
        private Game game;

        public Square(Game game) {
            this.game = game;
            this.MaxEnemies = 9;
            this.enemies = new EntityContainer<Enemy>(MaxEnemies);
        }
        public void CreateEnemies(List<Image> enemyStrides) {
            for (int i = 1; i < 4; i++) {
                float xposition = i * 0.09f;
                enemies.AddDynamicEntity(new Enemy(game, new DynamicShape(new Vec2F(xposition, 0.9f),
                    new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
                
                enemies.AddDynamicEntity(new Enemy(game, new DynamicShape(new Vec2F(xposition, 0.8f),
                    new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
                
                
                enemies.AddDynamicEntity(new Enemy(game, new DynamicShape(new Vec2F(xposition, 0.7f),
                    new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            }
        }
    }
}