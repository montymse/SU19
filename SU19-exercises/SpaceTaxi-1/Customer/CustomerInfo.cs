using System;

namespace SpaceTaxi_1.Customer {
    public class CustomerInfo {
        
        /// <summary> Takes a string[] generated from opener and extracts the line that contains
        /// the customer info </summary>
        /// <returns> The customer information as a single string, error message if
        /// no such string is found</returns>
        public static string GetCustomerInfo(string[] file) {
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
        public static string[] SplitCustomerInfo(string info) {
            return info.Split();

        }
    }
}