using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using SpaceTaxi_1.GameStates;
using SpaceTaxi_1.Customer;

namespace SpaceTaxi_1 {
    public class Game : IGameEventProcessor<object> {
        private GameTimer gameTimer;
        private Window win;
        public StateMachine stateMachine;

        
        private List<Entity> textureList;

        public Game() {
            // window
            win = new Window("Space Taxi Game v0.1", 500, AspectRatio.R1X1);
            win.RegisterEventBus(GalagaBus.GetBus());

            //statemachine
            stateMachine = new StateMachine();
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>()
            {
                GameEventType.WindowEvent,
                GameEventType.InputEvent,
                GameEventType.GameStateEvent,
                GameEventType.PlayerEvent

            });
        

            GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);


                      
            // game timer
            gameTimer = new GameTimer(60, 60); // 60 UPS, 60 FPS limit


        }

        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();

                while (gameTimer.ShouldUpdate()) {

                    win.PollEvents();
                    
                    //TODO: Rename galagabus
                    GalagaBus.GetBus().ProcessEvents();
                    stateMachine.ActiveState.UpdateGameLogic();
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();  
                    stateMachine.ActiveState.RenderState();

                    win.SwapBuffers();
                    
                }

                if (gameTimer.ShouldReset()) {
                    // 1 second has passed - display last captured ups and fps from the timer
                    win.Title = "Space Taxi | UPS: " + gameTimer.CapturedUpdates + ", FPS: " +
                                 gameTimer.CapturedFrames;
                }
            }
        }

       

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                case "SAVE_SCREENSHOT":
                    Console.WriteLine("Saving screenshot");
                    win.SaveScreenShot();
                    break;
                }
              
            }

            if (eventType == GameEventType.GameStateEvent) {
                stateMachine.ProcessEvent(eventType,gameEvent);

              }
            
            if (eventType == GameEventType.PlayerEvent) {
            }
            
            else if (eventType == GameEventType.InputEvent) {

                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    stateMachine.ActiveState.HandleKeyEvent(gameEvent.Parameter1,
                        gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    stateMachine.ActiveState.HandleKeyEvent(gameEvent.Parameter1,
                        gameEvent.Message);
                    break;
                }
            }
            
        }
       
            }
        }
    