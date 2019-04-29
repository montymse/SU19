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
            string text = Opener.CutString(Opener.FileToString(filename));


            //Add information to list

            int z = 0;
            foreach (char elm in text) {
                if (elm != ' ') {
                            foreach (Tuple<string, string> item in textures) {
                                if (item.Item1.Contains(elm.ToString())) {
                                    result.Add(
                                        new Entity(
                                            //new DynamicShape(
                                            //new Vec2F(positions[z].Item1, positions[z].Item2),
                                            //new Vec2F(0.04f, 0.04f))
                                             new DynamicShape(positions[z].Item1,positions[z].Item2,0.025f,0.0434f),
                                            new Image("Assets/Images/" + item.Item2.Remove(0,1))));
                                    z++;
                                }
                            }

                        }
                    }
                    return result;

                    
                }
            

            
                
            }
        }

    
    

/*
 foreach (var elm in Placement.FindPlacement(Opener.CutStringLevel(Opener.FileToStringList("../../Levels/short-n-sweet.txt")))) {
               //textureList.Add(new Entity(new DynamicShape(elm.Item1.Item1,elm.Item1.Item2,0.04f,0.04f),new Image("Assets/Images/" + elm.Item2.Remove(0,1))));
               // Shape elmShape = new Shape();
               // elmShape.Position = new Vec2F(elm.Item1.Item1,elm.Item1.Item2);
               // textureList.Add(new Entity(elmShape,new Image("Assets/Images/" + elm.Item2.Remove(0,1))));
                
            }*/