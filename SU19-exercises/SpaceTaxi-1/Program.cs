using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
          /*  var game = new Game();
            game.GameLoop();*/

          List<Tuple<string,string>> list = ImageList.StringToImageList(Opener.FileToString(
                  "/Users/Muse/Desktop/su19-mikaelMuseFrederik/SU19-exercises/SpaceTaxi-1/Levels/short-n-sweet.txt")
              );         
          
              foreach (var elm in list)
              {
                  Console.WriteLine("{0}",elm);
              }
              
        }
    }
}

