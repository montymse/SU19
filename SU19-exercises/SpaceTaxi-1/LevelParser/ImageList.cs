using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpaceTaxi_1 {
    
    
    public class ImageList {
        
        
        public static List<Tuple<string,string>> StringToImageList(string filestring) {
            
            Regex rx= new Regex(".{1}\\) .*\\.png");
            Regex rxCharacter= new Regex(".{1}\\)");
            Regex rxImage= new Regex(" .*\\.png");
            Match match = (rx.Match(filestring));

            List<Tuple<string,string>> listResult = new List<Tuple<string, string>>();

            while (match.Success) {
                    listResult.Add(new Tuple<string, string>(
                        rxCharacter.Match(rx.Match(filestring).Value).Value,
                        rxImage.Match(rx.Match(filestring).Value).Value
                        ));
                    match=match.NextMatch();

            }

            return listResult;
        }
    }
}