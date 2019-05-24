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

       /*  foreach (var elm in CustomerInfo.SplitCustomerInfo("../../Levels/the-beach.txt")) {
             Console.WriteLine(elm);
  
         }
  
            Console.WriteLine(CustomerInfo.SplitCustomerInfo("../../Levels/the-beach.txt")[3].ToCharArray()[0]);
  
         Console.WriteLine(       CustomerInfo.PickupPosition("../../Levels/the-beach.txt"));
  
  
  
  
                foreach (var elm in ImageList.StringToImageList( Opener.FileToString("../../Levels/the-beach.txt")))
                 {
                    Console.WriteLine(elm);
  
                }
                  
                
                foreach (var elm in Opener.CutStringLevel( Opener.FileToStringList("../../Levels/the-beach.txt")))
                {
                    Console.WriteLine(elm);
  
                }
         
                foreach (var elm in Placement.FindPlacement(("../../Levels/short-n-sweet.txt")))
                 {
                    Console.WriteLine(elm);
  
                }
             */


        }
    }
}

