using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    
    public class Enemy : Entity {
        private Game game;
        
        public List<Image> enemyStrides;

        public List<Enemy> enemies;
        public Enemy(Game game, Shape shape, IBaseImage image) : base(shape, image) {
        
        enemyStrides = ImageStride.CreateStrides(4,
        Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemies = new List<Enemy>();
    }

        ImageStride strides = new ImageStride(int 80, enemyStrides);

        public void AddEnemies() {
            Enemy Enemy1 = new Enemy(game, new DynamicShape(new Vec2F(0.20f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            Enemy Enemy2 = new Enemy(game, new DynamicShape(new Vec2F(0.30f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            Enemy Enemy3 = new Enemy(game, new DynamicShape(new Vec2F(0.40f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            Enemy Enemy4 = new Enemy(game, new DynamicShape(new Vec2F(0.50f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            Enemy Enemy5 = new Enemy(game, new DynamicShape(new Vec2F(0.60f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            Enemy Enemy6 = new Enemy(game, new DynamicShape(new Vec2F(0.70f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            Enemy Enemy7 = new Enemy(game, new DynamicShape(new Vec2F(0.80f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            Enemy Enemy8 = new Enemy(game, new DynamicShape(new Vec2F(0.90f, 0.9f), 
                new Vec2F(0.1f, 0.1f)),);
            
            enemies.Add(Enemy1);
            enemies.Add(Enemy2);
            enemies.Add(Enemy3);
            enemies.Add(Enemy4);
            enemies.Add(Enemy5);
            enemies.Add(Enemy6);
            enemies.Add(Enemy7);
            enemies.Add(Enemy8);
            
        }
        
    }
}