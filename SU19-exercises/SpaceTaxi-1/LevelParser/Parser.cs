using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1 {
    public class Parser {

                    
            public static List<Entity> CreateEntityList(string filename) {
            //This list will contain the results 
            List<Entity> result = new List<Entity>();

            //Get positionslist
            List<Tuple<float, float>> positions =
                Placement.FindPlacement(Opener.CutStringLevel(Opener.FileToStringList(filename)));

            //Get image filenameslist
            List<Tuple<string, string>> textures =
                ImageList.StringToImageList(Opener.FileToString(filename));

            //Get image StringList
            string[] text = Opener.CutStringLevel(Opener.FileToStringList(filename));


            //Add information to list

            int z = 0;
            for (int y = 0; y < text.Length - 1; y++) {
                for (int x = 0; x < text[y].Length; x++) {
                    if (text[y][x] != ' ') {
                        foreach (Tuple<string, string> item in textures) {
                            if (item.Item1.Contains(text[y][x].ToString())) {
                                result.Add(
                                    new Entity(
                                        new DynamicShape(
                                            new Vec2F(positions[z].Item1, positions[z].Item2),
                                            new Vec2F(0.04f, 0.04f)),
                                        // new DynamicShape(positions[z].Item1,positions[z].Item2,0.4f,0.4f)
                                        new Image("Assets/Images/" + item.Item2.Remove(0,1))));
                                z++;
                            }
                        }

                    }
                }
            }
            return result;
        }

    }
    }
