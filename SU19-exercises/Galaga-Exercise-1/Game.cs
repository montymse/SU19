using System;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;

namespace Galaga_Exercise_1 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;

        public Game() {
            win = new Window("Galaga",500,500);
            gameTimer = new GameTimer(60,60);
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                
                while(gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    //game logic is updated here
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    //render shit here
                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    win.Title = String.Format("Galaga | UPS: {0} | FPS: {1}",gameTimer.CapturedUpdates, gameTimer.CapturedFrames);
                }
            }
        }
        
        public void KeyPress(string key) {
            throw new NotImplementedException();
        }
        
        public void KeyRelease(string key) {
            throw new NotImplementedException();
        }
        
        public void ProcessEvent(GameEventType eventType,GameEvent<object> gameEvent) {
            throw new NotImplementedException();
        }
    }
}