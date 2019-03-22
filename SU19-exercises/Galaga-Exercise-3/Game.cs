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
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using GalagaGame.GalagaState;
using Galaga_Exercise_3.GameStates;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;
        public StateMachine stateMachine;
        
        
        private GameEventBus<object> eventBus;


       
        public Game() {
            win = new Window("Window-name", 500, 500);
            gameTimer = new GameTimer(60, 60);
            stateMachine = new StateMachine();

            
            
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent, // Move the player
            });
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, this);

        }

        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    // Update game logic here
                    stateMachine.ActiveState.UpdateGameLogic();

                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    // Render gameplay entities here
                    stateMachine.ActiveState.RenderState();

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
            switch (key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                        "", ""));
                break;
            case "KEY_SPACE":
                GameRunning.player.Shoot();
                break;
            case "KEY_A": case "KEY_D": case "KEY_LEFT": case "KEY_RIGHT":
                GameRunning.player.ProcessEvent(GameEventType.PlayerEvent,GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this,
                    key == "KEY_A" || key == "KEY_LEFT" ? "LEFT" : "RIGHT",
                    "", ""));
                
                break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_A": case "KEY_D": case "KEY_LEFT": case "KEY_RIGHT":
                GameRunning.player.ProcessEvent(GameEventType.PlayerEvent,
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "RELEASE",
                        "", ""));
                break;
            case "KEY_SPACE":
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
