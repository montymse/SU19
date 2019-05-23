using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SpaceTaxi_1 {
    public class Placement {
    
    /// <summary>
    /// CalculateMapSize takes a string array as argument (intended to be the type of string[]
    /// that CutStringLevel gives), and it uses the string array to convert to x- and y-coordinates,
    /// which later will be used to give a ASCII symbol a position
    /// </summary>
    /// <param name="text">
    /// text is a string[], in which CalculateMapSize calculates the length of the array
    /// and length of the string in the array
    /// </param>
    /// <returns>
    /// Returns a tuple with a x-coordinate and a y-coordinate
    /// </returns>
        private static Tuple<float, float> CalculateMapSize(string[] text) {
            return (new Tuple<float, float>((float) 1/text[0].Length, (float) 1/(text.Length-1)));

        }
    /// <summary>
    /// Convert takes to integers, x and y, and a tuple of floats to convert it to actual
    /// positions in the actual game
    /// </summary>
    /// <param name="x"></param>
    /// x is a index in a string array 
    /// <param name="y"></param>
    /// y is a index in a string within a string array
    /// <param name="map"></param>
    /// map is a float tuple containing a x-coordinate and a y-coordinate from CalculateMapSize
    /// <returns>
    /// Position of a ASCII symbol in a ASCII drawing of the map layout
    /// </returns>

        private static Tuple<float, float> Convert(int x, int y, Tuple<float, float> map) {
            return new Tuple<float, float>( x*map.Item1, 1f-((y+1)*map.Item2));

        }
    
    /// <summary>
    /// FindPlacementAndImage finds the related image file and positions of all ASCII symbols in
    /// a ASCII drawing of the map layout in the text file
    /// </summary>
    /// <param name="textfile"></param>
    /// ASCII-based text file (as given in the project hand-out)
    /// <returns>
    ///returns a tuple containing a float tuple of positions as first element
    /// and a string with image file name as the second element in the tuple
    /// </returns>


        public static List<Tuple<Tuple<float, float>,string>> FindPlacementAndImage(string textfile) {
            string[] text = 
                Opener.CutStringLevel(textfile);
            
            Tuple<float, float> mapsize = CalculateMapSize(text);
            List<Tuple<string, string>> textures =
                ImageList.StringToImageList(Opener.FileToString(
                textfile));
            List<Tuple<Tuple<float, float>,string>> places =
                new List<Tuple<Tuple<float, float>, string>>();

           for (int y = 0; y < text.Length-1; y++) {

              for (int x = 0; x < text[y].Length; x++) {

                 if (text[y][x] != ' ') {
                     
                       foreach (Tuple<string,string> item in textures) {
                          if (item.Item1.Contains(text[y][x].ToString())) {
                              Tuple<float, float> z = Convert(x, y, mapsize);
                             places.Add(new Tuple<Tuple<float, float>,
                                 string>(Convert(x, y, mapsize),item.Item2));
                          }
                      }
                  }


               }
           }
          

            return places;
        }
        
        }
        
    }
    