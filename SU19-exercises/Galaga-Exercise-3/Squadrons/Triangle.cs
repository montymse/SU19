using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;


namespace Galaga_Exercise_3.Squadrons {
    public class Triangle : ISquadrons {
        
        //Setter added to allow for enemy deletion
        public EntityContainer<Enemy> enemies { get; set; }
        public int MaxEnemies { get; }
        private IGameState game;
        public List<Enemy> listenemy;


        public Triangle(IGameState game) {
            this.game = game;
            this.MaxEnemies = 3;
            this.enemies = new EntityContainer<Enemy>(MaxEnemies);
            listenemy=new List<Enemy>();

        }
        public void CreateEnemies(List<Image> enemyStrides) {
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.6f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.4f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.5f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(game,new DynamicShape(new Vec2F(0.5f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
        }
    }
}
