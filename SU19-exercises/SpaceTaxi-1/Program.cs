using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
            /*  var game = new Game();
              game.GameLoop();*/

            List<Tuple<string, string>> list = ImageList.StringToImageList(Opener.FileToString(
                "/Users/Muse/Desktop/su19-mikaelMuseFrederik/SU19-exercises/SpaceTaxi-1/Levels/short-n-sweet.txt")
            );

            string[] s = Opener.CutStringLevel(
                    Opener.FileToStringList(
                        "/Users/Muse/Desktop/su19-mikaelMuseFrederik/SU19-exercises/SpaceTaxi-1/Levels/short-n-sweet.txt"))
                ;
            int z = 0;
            foreach (var elm in Placement.FindPlacement(s)) {
                Console.WriteLine(elm);
                z++;
            }
            Console.WriteLine(z);


            /*
            for (int y = 0; y < s.Length-1; y++) {
                for (int x = 0; x < s[y].Length-1; x++) {
                    if (s[y][x].ToString().Equals(" ")) {
                        
                    }
                }
            }
        
        */
        }
    }
}

