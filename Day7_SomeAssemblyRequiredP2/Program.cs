using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_SomeAssemblyRequiredP2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, short> dictionary = new Dictionary<string, short>();

            string[] lines;
            int i = 0;

            lines = File.ReadAllLines(@".\Day7OperationsP2.txt");


            bool hasAddedValue;
            do
            {
                hasAddedValue = false;

                foreach (string line in lines)
                {
                   

                    string[] lineArray = line.Split(',');

                    if (lineArray.Length == 4) // Ex : lf AND lq -> ls    lf = arg1  lq = arg3 et ls = res
                    {
                        short arg1 = 0, arg3 = 0, res = 0;

                        bool isNumberArg1 = false, isNumberArg3 = false;
                        bool canAddResToDictionnary = false;


                        isNumberArg1 = short.TryParse(lineArray[0], out arg1);

                        isNumberArg3 = short.TryParse(lineArray[2], out arg3);

                        if (!isNumberArg1 && !isNumberArg3) // Ex : lf AND lq -> ls
                        {
                            if (dictionary.ContainsKey(lineArray[0]) && dictionary.ContainsKey(lineArray[2]))
                            {
                                arg1 = dictionary[lineArray[0]];
                                arg3 = dictionary[lineArray[2]];
                                canAddResToDictionnary = true;
                            }



                        }
                        else if (!isNumberArg1 && isNumberArg3) // Ex : lf AND 1 -> ls
                        {
                            if (dictionary.ContainsKey(lineArray[0]))
                            {
                                arg1 = dictionary[lineArray[0]];
                                canAddResToDictionnary = true;
                            }

                        }
                        else if (isNumberArg1 && !isNumberArg3) // Ex : 1 AND lq -> ls
                        {
                            if (dictionary.ContainsKey(lineArray[2]))
                            {
                                arg3 = dictionary[lineArray[2]];
                                canAddResToDictionnary = true;
                            }

                        }
                        else if (isNumberArg1 && isNumberArg3) // Ex : 1 AND 1 -> ls
                            canAddResToDictionnary = true;

                        // Operator
                        if (lineArray[1] == "AND")
                        {
                            res = (short)(arg1 & arg3); // Une operation bitwise renvoie des int.  
                            //obligation de convertir en short. short = 16 bits, int = 32 bits. Convertir short en int pas de soucis. 
                            //Mais inverse pas possible donc conversion implicite.

                        }
                        else if (lineArray[1] == "OR")
                        {
                            res = (short)(arg1 | arg3);

                        }
                        else if (lineArray[1] == "LSHIFT")
                        {
                            res = (short)(arg1 << arg3);
                        }
                        else if (lineArray[1] == "RSHIFT")
                        {
                            res = (short)(arg1 >> arg3);
                        }

                        if (canAddResToDictionnary && !dictionary.ContainsKey(lineArray[3]))
                        {
                            dictionary[lineArray[3]] = res;
                            hasAddedValue = true;

                        }
                            
                    }
                    else if (lineArray.Length == 3) //NOT el -> em
                    {
                        if (dictionary.ContainsKey(lineArray[1]) && !dictionary.ContainsKey(lineArray[2]))
                        {
                            dictionary[lineArray[2]] = (short)~dictionary[lineArray[1]];
                            hasAddedValue = true;

                        }
                    }
                    else if (lineArray.Length == 2) // a -> b OU 3 ->b
                    {
                        short arg1 = 0;
                        bool isNumberArg1 = false;
                        isNumberArg1 = short.TryParse(lineArray[0], out arg1);

                        if (!isNumberArg1) // si le premier arg pas un chiffre
                        {
                            if (dictionary.ContainsKey(lineArray[0]) && !dictionary.ContainsKey(lineArray[1]))
                            {
                                arg1 = dictionary[lineArray[0]];
                                dictionary[lineArray[1]] = arg1;
                                hasAddedValue = true;
                            }
                                    
                        }
                        else if (isNumberArg1) // si premier argument est un chiffre // 3 -> b
                        {
                            if (!dictionary.ContainsKey(lineArray[1])) // ajout d'une clé et sa valeur
                            {
                                dictionary[lineArray[1]] = arg1;
                                hasAddedValue = true;
                            }
                               

                        }
                    }


                }

                //if (!hasAddedValue) ;

            } while (hasAddedValue);
            

            Console.WriteLine("Value of wire a : {0}", dictionary["a"]);


        }
    }
}
