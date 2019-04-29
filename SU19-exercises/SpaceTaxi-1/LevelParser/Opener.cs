using System;

namespace SpaceTaxi_1 {
    public class Opener {
        public static string[] FileToStringList(string file) {
            string[] text = System.IO.File.ReadAllLines(file);
            return text;
        }
        
        public static string FileToString(string file) {
            string text = System.IO.File.ReadAllText(file);
            return text;
        }

        public static string[] CutStringLevel(string[] text) {
            string[] level = new string[24];
            
            for (int i = 0; i < 23; i++) {
                level[i] = text[i];   
            }

            return level;
        }

        public static string CutString(string text) {
            int a = text.IndexOf("Name", StringComparison.Ordinal);
            return text.Substring(0, a - 1);
        }

    }
}