using System;
using System.Collections.Generic;

namespace SpaceTaxi_1 {
    public class Comparer {
        private List<Tuple<string, string>> imgList;

        /// <summary>
        /// Used to extract filenames of textures.
        /// </summary>
        /// <param name="levelString">Level ASCII layout</param>
        public Comparer(string levelString) {
            this.imgList = ImageList.StringToImageList(levelString);
        }
        
        /// <summary>
        /// Searches for a character in the list of textures.
        /// </summary>
        /// <param name="character">Character to compare</param>
        /// <returns>Character's correpsonding texture</returns>
        /// <exception cref="Exception">Thrown if character is not present in texture list</exception>
        public string GetImageFileName(char character) {
            //Find a match in the list. Return image filename when match is found
            foreach (Tuple<string,string> item in imgList) {
                if (item.Item1.Contains(character.ToString())) {
                    return item.Item2;
                }
            }
            
            //Throw an exception if item is not found
            throw new Exception("Character not found in image list.");
        }
    }
}