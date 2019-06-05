using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using DIKUArcade.Entities;
using SpaceTaxi_1.Customer;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
            var game = new Game();
            game.GameLoop();
          
        }
    }
}