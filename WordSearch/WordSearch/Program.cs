using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace WordSearch
{
    class Program
    {
        #region Properties 
        // Diagonal Up Right is reverse of Diagonal Down Left or vice versa and Diagonal Down Right is reverse of Diagonal Up Left or vice versa
        // So once we re-arrange data for one to facilitate search, we can use the re-arranged data to search other
        private static List<string> DiagonalDownLeft = new List<string>();
        private static List<string> DiagonalUpRight = new List<string>();
        private static List<string> DiagonalUpLeft = new List<string>();
        private static List<string> DiagonalDownRight = new List<string>();  
        #endregion 

        #region Main
        static void Main(string[] args)
        {
            try
            {                
                // WordSearch.txt and WordList.txt are embedded resources in this assembly, load these files in stream readers 
                var assembly = Assembly.GetExecutingAssembly();
                var wordSearchStreamReader = new StreamReader(assembly.GetManifestResourceStream("WordSearch.WordSearch.txt"));
                var wordListStreamReader = new StreamReader(assembly.GetManifestResourceStream("WordSearch.WordList.txt"));

                string wordSearchFileContents = wordSearchStreamReader.ReadToEnd();
                string wordListFileContents = wordListStreamReader.ReadToEnd();
                
                Console.WriteLine("Content of WordSearch.txt: " + Environment.NewLine + wordSearchFileContents);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Content of WordList.txt:" + Environment.NewLine + wordListFileContents);

                // Load the file contents into string arrays              
                var wordSearchArray = wordSearchFileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);               
                var wordListArray = wordListFileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                // Make all words in upper case(if already not, assumption: match logic does not need to be case sensitive)
                wordSearchArray = wordSearchArray.Select(s => s.ToUpper()).ToArray();

                // Make the word list into a List to easily remove the word when found(assumption is a word is only listed once) 
                // removing white space in the middle, removing leading and trailing white space and making all words to upper case         
                var wordList = wordListArray.Select(s => s.Replace(" ", string.Empty).Trim().ToUpper()).ToList();
                
                // We got the data, now find the words: 
                Console.WriteLine("WordSearch Found Words in:");               
                           
                // Find Left to right(LR)
                if (wordList.Any())
                {
                    FindLeftToRight(wordSearchArray, wordList);
                }

                // Find Right to left(RL)
                if (wordList.Any())
                {
                    FindRightToLeft(wordSearchArray, wordList);
                }

                // Find Down(D)
                if (wordList.Any())
                {
                    FindDown(wordSearchArray, wordList);
                }

                // Find Up(U)
                if (wordList.Any())
                {
                    FindUp(wordSearchArray, wordList);
                }
                  
                // Find Diagonal Down left (DDL)
                if (wordList.Any())
                {
                    FindDiagonalDownLeft(wordSearchArray, wordList);                    
                }

                // Find Diagonal Up Right (DUR)
                if (wordList.Any())
                {
                    FindDiagonalUpRight(wordSearchArray, wordList);
                }

                // Find Diagonal Up Left (DUL)
                if (wordList.Any())
                {
                    FindDiagonalUpLeft(wordSearchArray, wordList);
                }

                // Find Diagonal Down Right (DDR)
                if (wordList.Any())
                {
                    FindDiagonalDownRight(wordSearchArray, wordList);
                }

                // See if we did not find anything
                if (wordList.Any())
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("WordSearch could not found the following word(s):");                    
                                        
                    foreach(var word in wordList)
                    {
                        Console.WriteLine(word);                                             
                    }                 
                }
                else
                {
                    Console.WriteLine("WordSearch found all words listed!");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error accessing or processing content files" + ex.ToString());
            }
            Console.WriteLine(Environment.NewLine);

            // let the user exit the console app     
            Console.Write("Press any key to exit");
            Console.ReadKey();
        }
        #endregion 

        #region FindLeftToRight
        private static void FindLeftToRight(string[] wordSearchArray, List<string> wordList)
        {
            try
            {               
                // Keep track of words found in this method
                var wordsFound = new List<string>();

                // wordsearch by default is in left to right  
                foreach (var wordSearchLine in wordSearchArray)
                {
                                
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSearchLine.Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in LR");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindLeftToRight" + ex.ToString());
            }

        }
        #endregion 

        #region FindRightToLeft
        private static void FindRightToLeft(string[] wordSearchArray, List<string> wordList)
        {
            try
            {             
                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSeachLine in wordSearchArray)
                {
                    // reverse the word to get Right to Left content 
                    string wordSeachLineReversed = new string(wordSeachLine.ToCharArray().Reverse().ToArray());    
               
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSeachLineReversed.Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in RL");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindRightToLeft" + ex.ToString());
            }

        }
        #endregion

        #region FindDown
        private static void FindDown(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // re-organize the original contents to facilitate search
                var wordSearchArrayForDown = new StringBuilder[wordSearchArray.Length];

                // initialize all 
                for (int k = 0; k < wordSearchArray.Length; k++)
                {
                    wordSearchArrayForDown[k] = new StringBuilder();
                }
                
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                     var wordSearchCharArray = wordSearchArray[i].ToCharArray();
                   
                    // take each char out and build the modified array, like putting stuffs in slots
                    for (int j = 0; j < wordSearchCharArray.Length; j++)
                    {
                        wordSearchArrayForDown[j].Append(wordSearchCharArray[j].ToString());                        
                    }
                }

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in wordSearchArrayForDown)
                {                                
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSearchLine.ToString().Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in D");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindDown" + ex.ToString());
            }

        }
        #endregion 

        #region FindUp
        private static void FindUp(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // re-organize the wordSearchArray to facilitate search
                var wordSearchArrayForUp = new StringBuilder[wordSearchArray.Length];
                // initilize all 
                for (int k = 0; k < wordSearchArray.Length; k++)
                {
                    wordSearchArrayForUp[k] = new StringBuilder();
                }

                for (int i = wordSearchArray.Length - 1; i >= 0; i--)
                {
                    var wordSearchCharArray = wordSearchArray[i].ToCharArray();

                    // take the char out and build the modified array, like putting stuffs in slots
                    for (int j = 0; j < wordSearchCharArray.Length; j++)
                    {
                        wordSearchArrayForUp[j].Append(wordSearchCharArray[j].ToString());
                    }
                }

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in wordSearchArrayForUp)
                {           
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSearchLine.ToString().Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in U");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindUp" + ex.ToString());
            }

        }
        #endregion 

        #region FindDiagonalDownLeft
        private static void FindDiagonalDownLeft(string[] wordSearchArray, List<string> wordList)
        {
            try
            {                
                // First re-organize the wordSearchArray to facilitate search
                ConstructDiagonalDownLeftUpperQuadrant(wordSearchArray);
                ConstructDiagonalDownLeftLowerQuadrant(wordSearchArray);
                
                // something went wrong
                if (DiagonalDownLeft == null)
                    return;
                              
                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in DiagonalDownLeft.Distinct().ToList())
                {                                 
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSearchLine.ToString().Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in DDL");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindDiagonalDown" + ex.ToString());
            }

        }
        #endregion 
                
        #region FindDiagonalUpRight
        private static void FindDiagonalUpRight(string[] wordSearchArray, List<string> wordList)
        {
            try
            {              
                // Diagonal Up Right is reverse of Diagonal Down Left 
                ConstructDiagonalUpRight();

                // something went wrong
                if (DiagonalUpRight == null)
                    return;

                // Keep track of words found in this method
                var wordsFound = new List<string>();
                
                foreach (var wordSearchLine in DiagonalUpRight.Distinct().ToList())
                {                          
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSearchLine.ToString().Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in DUR");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindDiagonalUpRight" + ex.ToString());
            }

        }
        #endregion 

        #region FindDiagonalUpLeft
        private static void FindDiagonalUpLeft(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // First re-organize the wordSearchArray to facilitate search
                ConstructDiagonalUpLeftUpperQuadrant(wordSearchArray);
                ConstructDiagonalUpLeftLowerQuadrant(wordSearchArray);                

                // something went wrong
                if (DiagonalUpLeft == null)
                    return;

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in DiagonalUpLeft.Distinct().ToList())
                {
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSearchLine.ToString().Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in DUL");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DiagonalUpLeft" + ex.ToString());
            }

        }
        #endregion 

        #region FindDiagonalDownRight
        private static void FindDiagonalDownRight(string[] wordSearchArray, List<string> wordList)
        {
            try
            {              
                // Diagonal Up Right is reverse of Diagonal Down Right 
                ConstructDiagonalDownRight();

                // something went wrong
                if (DiagonalDownRight == null)
                    return;

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in DiagonalDownRight.Distinct().ToList())
                {                          
                    foreach (var wordToFind in wordList)
                    {
                        if (wordSearchLine.ToString().Contains(wordToFind))
                        {
                            Console.WriteLine(wordToFind + " found in DDR");
                            wordsFound.Add(wordToFind);
                        }
                    }
                }

                // remove words found from the word list
                foreach (var wordToBeRemoved in wordsFound)
                {
                    wordList.Remove(wordToBeRemoved);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindDiagonalDownRight" + ex.ToString());
            }

        }
        #endregion 
                       
        #region ConstructDiagonalDownLeftUpperQuadrant
        private static void ConstructDiagonalDownLeftUpperQuadrant(string[] wordSearchArray)
        {
            try
            {
                // re-organize the wordSearchArray to facilitate search
                var wordSearchArrayForDiagonalDownLeftUpperQuadrant = new StringBuilder[wordSearchArray.Length];

                // initialize all 
                for (int k = 0; k < wordSearchArray.Length; k++)
                {
                    wordSearchArrayForDiagonalDownLeftUpperQuadrant[k] = new StringBuilder();
                }

                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    var wordSearchCharArrayForDiagonalDownLeftUpperQuadrant = wordSearchArray[i].ToCharArray();

                    // take each char out and build the modified array, like putting stuffs in slots
                    for (int j = 0; j < wordSearchCharArrayForDiagonalDownLeftUpperQuadrant.Length; j++)
                    {
                        //  Diagonal will have incremental pick up, also need to have check so we don't get array index out of bound
                        if ((j + i) < wordSearchCharArrayForDiagonalDownLeftUpperQuadrant.Length)
                        {
                            wordSearchArrayForDiagonalDownLeftUpperQuadrant[j].Append(wordSearchCharArrayForDiagonalDownLeftUpperQuadrant[j + i].ToString());
                        }
                    }
                }

                // store the rearranged data
                if (DiagonalDownLeft != null)
                {
                    foreach (var wordSearch in wordSearchArrayForDiagonalDownLeftUpperQuadrant)
                    {
                        DiagonalDownLeft.Add(wordSearch.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ConstructDiagonalDownLeftUpperQuadrant" + ex.ToString());
            }

        }
        #endregion 

        #region ConstructDiagonalDownLeftLowerQuadrant
        private static void ConstructDiagonalDownLeftLowerQuadrant(string[] wordSearchArray)
        {
            try
            {
                // re-organize the original contents to facilitate search 
                var wordSearchArrayForDown = new StringBuilder[wordSearchArray.Length];
                var wordSearchArrayForDiagonalDownLeftLowerQuadrant = new StringBuilder[wordSearchArray.Length];

                // initialize all 
                for (int k = 0; k < wordSearchArray.Length; k++)
                {
                    wordSearchArrayForDown[k] = new StringBuilder();
                    wordSearchArrayForDiagonalDownLeftLowerQuadrant[k] = new StringBuilder();
                }

                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    var wordSearchCharArray = wordSearchArray[i].ToCharArray();

                    // take each char out and build the modified array, like putting stuffs in slots
                    for (int j = 0; j < wordSearchCharArray.Length; j++)
                    {
                        wordSearchArrayForDown[j].Append(wordSearchCharArray[j].ToString());
                    }
                }

                for (int m = 0; m < wordSearchArrayForDown.Length; m++)
                {
                    var wordSearchCharArrayForDiagonalDownLeftLowerQuadrant = wordSearchArrayForDown[m].ToString().ToCharArray();

                    //  Diagonal will have incremental pick up, also need to have check so we don't get array index out of bound  
                    for (int n = 0; n < wordSearchCharArrayForDiagonalDownLeftLowerQuadrant.Length; n++)
                    {
                        if ((n + m) < wordSearchCharArrayForDiagonalDownLeftLowerQuadrant.Length)
                        {
                            wordSearchArrayForDiagonalDownLeftLowerQuadrant[n].Append(wordSearchCharArrayForDiagonalDownLeftLowerQuadrant[n + m].ToString());
                        }
                    }
                }

                // store the rearranged data
                if (DiagonalDownLeft != null)
                {
                   foreach(var wordSearch in wordSearchArrayForDiagonalDownLeftLowerQuadrant)
                   {
                       DiagonalDownLeft.Add(wordSearch.ToString());
                   }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ConstructDiagonalDownLeftLowerQuadrant" + ex.ToString());
            }

        }
        #endregion 

        #region ConstructDiagonalUpRight
        private static void ConstructDiagonalUpRight()
        {
            try
            {
                if (DiagonalDownLeft != null)
                {
                    foreach (var wordSearch in DiagonalDownLeft)
                    {
                        // reverse the word to get Right to Left content 
                        string wordSeachLineReversed = new string(wordSearch.ToCharArray().Reverse().ToArray());
                        DiagonalUpRight.Add(wordSeachLineReversed);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ConstructDiagonalDownLeftUpperQuadrant" + ex.ToString());
            }

        }
        #endregion 
        
        #region ConstructDiagonalUpLeftUpperQuadrant
        private static void ConstructDiagonalUpLeftUpperQuadrant(string[] wordSearchArray)
        {
            try
            {
                // re-organize the wordSearchArray to facilitate Find Diagonal Down Right
                var wordSearchArrayForDiagonalUpLeftUpperQuadrant = new StringBuilder[wordSearchArray.Length];
                // initialize all 
                for (int k = 0; k < wordSearchArray.Length; k++)
                {
                    wordSearchArrayForDiagonalUpLeftUpperQuadrant[k] = new StringBuilder();
                }

                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    // reverse the word to get Right to Left content 
                    var wordSearchCharArray = wordSearchArray[i].ToCharArray().Reverse().ToArray();

                    // take each char out and build the modified array, like putting stuffs in slots
                    for (int j = wordSearchCharArray.Length - 1; j >= 0; j--)
                    {
                        //  Diagonal will have imcremental pick up, also need to have check so we don't get array index out of bound
                        if ((j + i) < wordSearchCharArray.Length)
                        {
                            wordSearchArrayForDiagonalUpLeftUpperQuadrant[j].Append(wordSearchCharArray[j + i].ToString());
                        }
                    }
                }
                // store the rearranged data
                if (DiagonalUpLeft != null)
                {
                    foreach (var wordSearch in wordSearchArrayForDiagonalUpLeftUpperQuadrant)
                    {
                        DiagonalUpLeft.Add(wordSearch.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ConstructDiagonalUpLeftUpperQuadrant" + ex.ToString());
            }

        }
        #endregion         
              
        #region ConstructDiagonalUpLeftLowerQuadrant
        private static void ConstructDiagonalUpLeftLowerQuadrant(string[] wordSearchArray)
        {
            try
            {
                // re-organize the original contents to facilitate search 
                var wordSearchArrayForUp = new StringBuilder[wordSearchArray.Length];
                var wordSearchArrayForDiagonalUpLeftLowerQuadrant = new StringBuilder[wordSearchArray.Length];

                // initialize all 
                for (int k = 0; k < wordSearchArray.Length; k++)
                {
                    wordSearchArrayForUp[k] = new StringBuilder();
                    wordSearchArrayForDiagonalUpLeftLowerQuadrant[k] = new StringBuilder();
                }

                for (int i = wordSearchArray.Length - 1; i >= 0; i--)
                {
                    var wordSearchCharArray = wordSearchArray[i].ToCharArray();

                    // take the char out and build the modified array, like putting stuffs in slots
                    for (int j = 0; j < wordSearchCharArray.Length; j++)
                    {
                        wordSearchArrayForUp[j].Append(wordSearchCharArray[j].ToString());
                    }
                }

                int s = 0;
                for (int m = wordSearchArrayForUp.Length -1 ; m >= 0; m--)
                {
                    var wordSearchCharArrayForDiagonalUpLeftLowerQuadrant = wordSearchArrayForUp[m].ToString().ToCharArray();

                    //  Diagonal will have incremental pick up, also need to have check so we don't get array index out of bound  
                    for (int n = 0; n < wordSearchCharArrayForDiagonalUpLeftLowerQuadrant.Length; n++)
                    {
                        if ((n + s) < wordSearchArrayForDiagonalUpLeftLowerQuadrant.Length)
                        {
                            wordSearchArrayForDiagonalUpLeftLowerQuadrant[n + s].Append(wordSearchCharArrayForDiagonalUpLeftLowerQuadrant[n].ToString());
                        }
                    }
                    s++;
                }

                // store the rearranged data
                if (DiagonalUpLeft != null)
                {
                    foreach (var wordSearch in wordSearchArrayForDiagonalUpLeftLowerQuadrant)
                    {
                        DiagonalUpLeft.Add(wordSearch.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ConstructDiagonalUpLeftLowerQuadrant" + ex.ToString());
            }

        }
        #endregion 

        #region ConstructDiagonalDownRight
        private static void ConstructDiagonalDownRight()
        {
            try
            {
                if (DiagonalUpLeft != null)
                {
                    foreach (var wordSearch in DiagonalUpLeft)
                    {
                        // reverse the word to get Right to Left content 
                        string wordSeachLineReversed = new string(wordSearch.ToCharArray().Reverse().ToArray());
                        DiagonalDownRight.Add(wordSeachLineReversed);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ConstructDiagonalDownRight" + ex.ToString());
            }

        }
        #endregion 
        
    }
}
