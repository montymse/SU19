using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpaceTaxi_1.Collisions {
    public class CollisionPlatforms : Placement {
        public static string[] GetPlatforms(string filename) {
            string platform = "";
            string[] file = Opener.FileToStringList(filename);
            string identifier = "Platforms:";
            foreach (string elm in file) {
                if (elm.Contains(identifier)) {
                    platform = elm;
                }
            }

            platform = Regex.Replace(platform, ",", "");
            platform = Regex.Replace(platform, "Platforms:", "");

            return platform.Split();
        }

        public static List<Tuple<Tuple<float, float>, string>> GetPlatform(string textfile) {
            string[] platformNames = CollisionPlatforms.GetPlatforms(textfile);

            string[] text =
                Opener.CutStringLevel(textfile);

            Tuple<float, float> mapSize = Placement.CalculateMapSize(text);
            List<Tuple<string, string>> textures =
                ImageList.StringToImageList(Opener.FileToString(
                    textfile));
            List<Tuple<Tuple<float, float>, string>> places =
                new List<Tuple<Tuple<float, float>, string>>();

            for (int y = 0; y < text.Length - 1; y++) {
                for (int x = 0; x < text[y].Length; x++) {
                    if (platformNames.Contains(text[y][x].ToString())) {
                        foreach (Tuple<string, string> item in textures) {
                            if (item.Item1.Contains(text[y][x].ToString())) {
                                Tuple<float, float> z = Placement.Convert(x, y, mapSize);
                                places.Add(new Tuple<Tuple<float, float>,
                                    string>(Placement.Convert(x, y, mapSize), item.Item2));
                            }
                        }
                    }
                }
            }


            return places;
        }
    }
}