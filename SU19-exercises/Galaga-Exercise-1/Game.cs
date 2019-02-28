using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
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

            case "KEY_LEFT":
                player.Direction(new Vec2F(-0.01f,0.00f));
                break;
            case "KEY_RIGHT":
                player.Direction(new Vec2F(0.01f,0.00f));
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
