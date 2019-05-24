using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1.Customer {
    public class CustomerPoints {
            private int score;
            private Text display;

            public CustomerPoints() {
                score = 0;
                display = new Text(score.ToString(),new Vec2F(0.8f, 0.7f),
                    new Vec2F(0.2f, 0.2f));
            }

            public void AddPoint(string points) {
                score += int.Parse(points);

            }

            public void RenderScore() {
                display.SetText(string.Format("Score: {0}", score.ToString()));
                display.SetColor(new Vec3I(255, 0, 0));
                display.RenderText();
            }
        }
    }