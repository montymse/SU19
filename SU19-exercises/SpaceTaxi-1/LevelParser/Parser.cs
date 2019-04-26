using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace SpaceTaxi_1 {
    public class Parser {
        private string FileString;

        public Parser(string filestring) {
            this.FileString = filestring;
        }

        public List<Entity> CreateEntityList() {
            List<Entity> result = new List<Entity>();
            
            //Get positions
            List<Tuple<float,float>> positions = Placement.FindPlacement(
                Opener.CutStringLevel(Opener.FileToStringList("../../Levels/short-n-sweet.txt")));

            //Get image filenames
            string lvlString = Opener.FileToString("../../Levels/short-n-sweet.txt");
            Comparer compare = new Comparer(lvlString);

            //Add information to list
            for (int i = 0; i < positions.Count - 1; i++) {
                result.Add(new Entity(new DynamicShape(positions[i].Item1,positions[i].Item2,0.4f,0.4f), new Image("Assets/Images/" + compare.GetImageFileName(lvlString[i]))));
            }

            return result;
        }
    }
}