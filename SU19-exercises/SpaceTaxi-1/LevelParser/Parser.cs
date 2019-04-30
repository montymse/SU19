using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1 {
    public class Parser {


        public static List<Entity> CreateEntityList(List<Tuple<Tuple<float, float>,string>> list) {
            List<Entity> textureList = new List<Entity>();

            foreach (var item in list) {
                textureList.Add(new Entity(
                    new DynamicShape(item.Item1.Item1, item.Item1.Item2, 0.025f, 0.043f),
                    new Image("Assets/Images/" + item.Item2.Remove(0, 1))));


            }



            return textureList;


        }
    }

}