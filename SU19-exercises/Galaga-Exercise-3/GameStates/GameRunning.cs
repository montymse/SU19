using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using GalagaGame.GalagaState;
using Galaga_Exercise_3;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3.GameStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        public static Player player;

        public List<Image> enemyStrides;


        public List<Enemy> enemies;


        private List<Image> explosionStrides;


        private AnimationContainer explosions;

        private Score score;

        private Triangle t;
        private Square s;
        private Diamond d;

        private Down down;
        private ZigZagDown zzdown;
        public static List<PlayerShot> playerShots { get; private set; }
        
        public GameRunning() {
            InitializeGameState();

        }

        private int explosionLength = 500;

        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        public void AddEnemies() {
            d.CreateEnemies(enemyStrides);
        }

        public void IterateShots() {
            foreach (var shot in playerShots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }
                
                foreach (Enemy enemy in d.enemies) {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)
                        .Collision) {
                        score.AddPoint();
                        explosions.RenderAnimations();
                        AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y,
                            enemy.Shape.Extent.X, enemy.Shape.Extent.Y);

                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                }
            }

            EntityContainer<Enemy> newEnemies = new EntityContainer<Enemy>();
            foreach (Enemy enemy in d.enemies) {
                if (!enemy.IsDeleted()) {
                    newEnemies.AddDynamicEntity(enemy);
                }
            }

            d.enemies = newEnemies;

            List<PlayerShot> newPlayerShots = new List<PlayerShot>();
            foreach (PlayerShot shot in playerShots) {
                if (!shot.IsDeleted()) {
                    newPlayerShots.Add(shot);
                }
            }

            playerShots = newPlayerShots;
        }


        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() {
            throw new System.NotImplementedException();
        }
          

        public void InitializeGameState() {
            t = new Triangle(this);
            s = new Square(this);
            d = new Diamond(this);
            down = new Down();
            zzdown = new ZigZagDown(0.0003f, 0.05f, 0.045f);
            
            player = new Player(this, new DynamicShape(new Vec2F(0.45f, 0.1f),
                new Vec2F(0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images",
                "Player.png")));
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(500);

            score = new Score(new Vec2F(0.8f, 0.7f),
                new Vec2F(0.2f, 0.2f));

            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            AddEnemies();

            playerShots = new List<PlayerShot>();
            
           
            
        }

        public void UpdateGameLogic() {
            player.Move();
            IterateShots();
            zzdown.MoveEnemies(d.enemies);        
        }

        public void RenderState() {
            
            
            d.enemies.RenderEntities();
            s.enemies.RenderEntities();
            t.enemies.RenderEntities();
            player.Entity.RenderEntity();
            score.RenderScore();
            explosions.RenderAnimations();
            foreach (Enemy item in enemies) {
                item.RenderEntity();
            }

            foreach (PlayerShot shot in playerShots) {
                shot.RenderEntity();
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            throw new System.NotImplementedException();
        }
    }
}