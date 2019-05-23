using System;

namespace SpaceTaxi_1 {
    public class Opener {
        
        
        /// <summary>
        /// FileToStringList opens a text file in order to read all lines of the text file
        /// into a string array
        /// </summary>
        /// <param name="file">
        /// the path of a text file as a string e.g. "../../Levels/short-n-sweet.txt"
        /// </param>
        /// <returns>
        /// a string array with all lines of the text file
        /// </returns>
        /// <remarks>
        /// FileToStringList will use the ASCII-based text file, that are given in the
        /// project hand-out
        /// </remarks>
        public static string[] FileToStringList(string file) {
            string[] text = System.IO.File.ReadAllLines(file);
            return text;
        }
        
        /// <summary>
        /// FileToString opens a text file and reads all the text in the file into a string
        /// </summary>
        /// <param name="file"></param>
        /// the path of a text file as a string e.g. "../../Levels/short-n-sweet.txt"
        /// <returns>
        /// a string with all the text from the file given as argument.
        /// </returns>
        
        public static string FileToString(string file) {
            string text = System.IO.File.ReadAllText(file);
            return text;
        }
        
        /// <summary>
        /// CutStringLevel cuts a string array, and only returns the first 23 lines
        /// of the string array. This is done because, the first 23 lines of ASCII-based
        /// text file given in the project hand-out, are the ASCII drawing of the map layout
        /// </summary>
        /// <param name="file">
        /// the path of a text file as a string e.g. "../../Levels/short-n-sweet.txt"
        /// </param>
        /// <returns>
        /// a string array with the first 23 lines of the string array that
        /// FileToStringList returns
        /// </returns>

        public static string[] CutStringLevel(string file) {
            string[] text = FileToStringList(file);
            string[] level = new string[24];
            
            for (int i = 0; i < 23; i++) {
                level[i] = text[i];   
            }

            return level;
        }

    }
}