using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1 {
    public class Parser {
        
        /// <summary>
        /// CreateEntityList creates a list of entities using the information that
        /// FindPlacementAndImage returns. 
        /// </summary>
        /// <param name="list"></param>
        /// a tuple containing a float tuple of positions as first element
        /// and a string with image file name as the second element in the tuple
        /// <returns>
        /// A list of entities
        /// </returns>


        public static List<Entity> CreateEntityList(List<Tuple<Tuple<float, float>,string>> list) {
            List<Entity> textureList = new List<Entity>();

            foreach (var item in list) {
                textureList.Add(new Entity(
                    
                    new DynamicShape(new Vec2F(item.Item1.Item1, item.Item1.Item2),
                        new Vec2F(0.025f, 0.043f)), 
                    new Image("Assets/Images/" + item.Item2.Remove(0, 1))));
            }



            return textureList;


        }
    }

}