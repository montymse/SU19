using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1.Squadrons {
    public class Diamond {
             
        public EntityContainer<Enemy> enemies { get; }
        public int MaxEnemies { get; }
        private Game game;

        public Diamond(Game game) {
            this.game = game;
            this.MaxEnemies = 11;
            this.enemies = new EntityContainer<Enemy>();
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.4f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.6f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.5f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.5f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.5f, 0.7f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
        }
    }
}