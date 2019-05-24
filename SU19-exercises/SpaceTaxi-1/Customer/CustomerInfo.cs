using System;
using System.Xml.Linq;

namespace SpaceTaxi_1.Customer {
    public class CustomerInfo {

        /// <summary> Takes a string[] generated from opener and extracts the line that contains
        /// the customer info </summary>
        /// <returns> The customer information as a single string, error message if
        /// no such string is found</returns>
        private static string GetCustomerInfo(string filename) {
            string[] file = Opener.FileToStringList(filename);
            string identifier = "Customer:";
            string error = "Customer not found.";
            foreach (string elm in file) {
                if (elm.Contains(identifier)) {

                    return elm;
                }
            }

            return error;
        }


        /// <summary>
        /// Splits the customer information string in substrings of each word
        /// </summary>
        /// <param name="info"> the information string generated from GetCustomerInfo</param>
        /// <returns> a string[] containing the customer information</returns>
        public static string[] SplitCustomerInfo(string filename) {
            string info = CustomerInfo.GetCustomerInfo(filename);
            return info.Split();

        }


        public static Tuple<int, int> PickupPosition(string filename) {
            Tuple<int, int> x = new Tuple<int, int>(0,0);
            string[] file = Opener.CutStringLevel(filename);
            for (int i = 0; i < file.Length-1; i++) {


                for (int j = 0; j < file[i].Length; j++) {
                    {
                        
                        if (file[i][j] == (char) CustomerInfo.SplitCustomerInfo(filename)[3].ToCharArray()[0]) {
                            Console.WriteLine(file[i][j].ToString());
                            x = new Tuple<int, int>(i, j);
                        }
                    }

                }
            }

            return x;
        }
    }
}