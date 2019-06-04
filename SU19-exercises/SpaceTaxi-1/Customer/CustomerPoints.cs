using System;
using System.Diagnostics;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1.Customer {
    public class CustomerPoints {
        private int points;
        private Text Score;
        private Text Timer;
        private CustomerEntity customer;
        private Stopwatch Stopwatch;


        public CustomerPoints(CustomerEntity customer) {
            this.customer = customer;
            Stopwatch = new Stopwatch();


            points = 0;
            Score = new Text(points.ToString(), new Vec2F(0.07f, 0.7f),
                new Vec2F(0.2f, 0.2f));
            Timer = new Text(customer.timeToDrop.ToString(), new Vec2F(0.8f, 0.7f),
                new Vec2F(0.2f, 0.2f));
        }

        public void AddPoint(string point) {
            points += int.Parse(point);
        }

        public void RenderScore() {
            Score.SetText(string.Format("Score: {0}", points));
            Score.SetColor(new Vec3I(255, 0, 0));
            Score.RenderText();

            if (customer.pickedUp) {
                Stopwatch.Start();
                Timer.SetText(string.Format("Time left: {0}",
                    (int.Parse(customer.timelimit) - Stopwatch.Elapsed.Seconds)));
                Timer.SetColor(new Vec3I(0, 0, 255));
                Timer.RenderText();
            }
        }
    }
}