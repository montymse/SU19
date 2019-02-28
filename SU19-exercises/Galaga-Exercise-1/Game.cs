using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Xml;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using  DIKUArcade.Graphics;

namespace Galaga_Exercise_1 {
    public class Game  : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;
        private Player player;
        private GameEventBus<object> eventBus;
        
        public List<Image> enemyStrides;

        public List<Enemy> enemies;
        
        public Game() {
            win = new Window("Window-name",500, 500);
            gameTimer = new GameTimer(60,60);
            
            player = new Player(this, new DynamicShape(new Vec2F(0.45f, 0.1f), 
                new Vec2F(0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images", 
                "Player.png")));
            
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window
            });
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            
            enemyStrides = ImageStride.CreateStrides(4,
            Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            AddEnemies();
        }

        //TODO: fix this shit
        private float xposition;
        public void AddEnemies() {
            for (int i = 1; i < 10; i++) {
                xposition = i * 0.09f;
                enemies.Add(new Enemy(this, new DynamicShape(new Vec2F(xposition, 0.9f),
                    new Vec2F(0.1f, 0.1f)),new ImageStride(80, enemyStrides)));
            }
        }
        public void GameLoop() {
            while(win.IsRunning()) {
                gameTimer.MeasureTime();
                eventBus.ProcessEvents();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    // Update game logic here
                }
                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    // Render gameplay entities here
                    player.RenderEntity();
                    foreach (Enemy item in enemies) {
                        item.RenderEntity();
                    }
                    win.SwapBuffers();
                }
                if (gameTimer.ShouldReset()) {
                    // 1 second has passed - display last captured ups and fps
                    win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
        }
        private void KeyPress(string key) {
            switch(key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                        "", ""));
                break;

            case "KEY_A":
                player.Direction(new Vec2F(-0.05f,0.00f));
                player.Move();
                break;
            case "KEY_D":
                player.Direction(new Vec2F(0.05f,0.00f));
                player.Move();
                break;
            }
        }
        
        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_RELEASE":
                player.Direction(new Vec2F(0.00f,0.00f));
                break;
            }
           
        }
        
        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                default:
                    break;
                }
            } else if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                }
            }
        }
    }
        
    }
