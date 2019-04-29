using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using DIKUArcade.Entities;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
              var game = new Game();
              game.GameLoop();
            

              //Console.WriteLine(
              //    Opener.CutString(Opener.FileToString("../../Levels/short-n-sweet.txt"))
              //    );

     
        }
    }
}

