﻿using System;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
          /*  var game = new Game();
            game.GameLoop();*/

          Console.WriteLine(
              ImageList.StringToImageList(Opener.FileToString())
              );
        }
    }
}