using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDistance
{
    class DocDistance
    {
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>

        public static Dictionary<string, long> Dict_create(string data)                                 // Dict ( key , value )   key : unique word ,  value: number of occurence in the text
        {
            Dictionary<string, long> Dict_count = new Dictionary<string, long>();

            // For loop on all text in the document

            for (int i = 0; i < data.Length; i++)
            {
                string word = "";
                char currentLetter = data[i];

                // is it a white space  or punctiuation ?
                if (char.IsWhiteSpace(data[i]) || data[i] == '\'')
                {
                    continue;
                }

                // is it Start of a Letter  or Digit ?
                else if (char.IsLetterOrDigit(currentLetter))
                {
                    while (i < data.Length && char.IsLetterOrDigit(data[i]))
                    {
                        currentLetter = data[i++];
                        word += currentLetter;

                    }
                    i--;
                }

                // if the word exist in the dictionary increase count by 1
                if (Dict_count.ContainsKey(word))
                {
                    Dict_count[word] += 1;
                }
                //if not exist add the word to document and its value is 1
                else if (!word.Equals(""))
                {
                    Dict_count.Add(word, 1);
                }
            }

            return Dict_count;
        }


        //   D1.D2   ( Document 1 : D1   , Document 2 : D2 )    
        public static double dotproduct(Dictionary<string, long> dict1, Dictionary<string, long> dict2)
        {
            double sum = 0;
            foreach (string key in dict1.Keys)
            {
                if (dict2.ContainsKey(key))
                {
                    sum += dict1[key] * dict2[key];
                }
            }
            return sum;
        }

        //?   --> d(D1,D2) = angle between 2 vectors  
        //?  -->   Cos-1 (  ( D1.D2 ) / sqrt(|D1| * |D2|)
        //Note  -->  |D1| = dotprodcuct ( D1 , D1 )   
        //Note  -->  |D2| = dotprodcuct ( D2 , D2 )
        //Note  -->  D1.D2 = dotprodcuct ( D1 , D2 )
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            // TODO comment the following line THEN fill your code here
            //throw new NotImplementedException();
            string D1 = System.IO.File.ReadAllText(doc1FilePath).ToLower();
            Dictionary<string, long> Dict1 = Dict_create(D1);
            string D2 = System.IO.File.ReadAllText(doc2FilePath).ToLower();
            Dictionary<string, long> Dict2 = Dict_create(D2);
            double dotproductD1D2 = dotproduct(Dict1, Dict2);
            double dotproduct_D1 = dotproduct(Dict1, Dict1);
            double dotproduct_D2 = dotproduct(Dict2, Dict2);
            double distance = Math.Acos(dotproductD1D2 / Math.Sqrt(dotproduct_D1 * dotproduct_D2));
            distance *= 180 / Math.PI;

            return distance;

        }

    }
}
