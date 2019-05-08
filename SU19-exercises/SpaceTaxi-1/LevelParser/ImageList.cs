using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpaceTaxi_1 {
    
    
    public class ImageList {
        
        /// <summary>
        /// StringToImageList uses regex expressions to match ASCII characters to a image file
        /// </summary>
        /// <param name="filestring"></param>
        /// ASCII-based text file (as given in the project hand-out)
        /// <returns>
        /// A tuple list with the identifier character as the first element
        /// and a image text file name as the second element
        /// </returns>

        
        public static List<Tuple<string,string>> StringToImageList(string filestring) {
            
            Regex rx= new Regex(".{1}\\) .*\\.png");
            Regex rxCharacter= new Regex(".{1}\\)");
            Regex rxImage= new Regex(" .*\\.png");
            Match match = (rx.Match(filestring));

            List<Tuple<string,string>> listResult = new List<Tuple<string, string>>();

            while (match.Success) {
                    listResult.Add(new Tuple<string, string>(
                        rxCharacter.Match(match.Value).Value,
                        rxImage.Match(match.Value).Value
                        ));
                    match=match.NextMatch();

            }

            return listResult;
        }
    }
}