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

                // Make all words in upper case(if already not, assumption match logic does not need to be case sensitive)
                wordSearchArray = wordSearchArray.Select(s => s.ToUpper()).ToArray();

                // Make the word list into a List to easily remove the word when found(assumption is a word is only listed once) 
                // removeing white space in the middle, removing leading and trailing white space and making all words to upper case         
                var wordList = wordListArray.Select(s => s.Replace(" ", string.Empty).Trim().ToUpper()).ToList();
                                
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

                // Find Up(D)
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
                    FindDiagonalDownLeftUpperQuadrant(wordSearchArray, wordList);                    
                }

                 // Find Diagonal Down left (DDL)
                if (wordList.Any())
                {
                    FindDiagonalDownLeftLowerQuadrant(wordSearchArray, wordList);                    
                }                

                // Find Diagonal Up Right (DUR)
                if (wordList.Any())
                {
                    FindDiagonalUpRightUpperQuadrant(wordSearchArray, wordList);
                }

                 // Find Diagonal Up Right (DUR)
                if (wordList.Any())
                {
                    FindDiagonalUpRightLowerQuadrant(wordSearchArray, wordList);
                }                

                // Find Diagonal down right (DDR)
                 if (wordList.Any())
                 {
                     FindDiagonalDownRight(wordSearchArray, wordList);
                 }

                 // Find Diagonal up left (DUL)
                 if (wordList.Any())
                 {
                     FindDiagonalUpLeft(wordSearchArray, wordList);
                 }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error accessing or processing content files" + ex.ToString());
            }

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
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    wordSearchArrayForDown[i] = new StringBuilder();
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
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    wordSearchArrayForUp[i] = new StringBuilder();
                }

                for (int i = wordSearchArray.Length - 1; i > 0; i--)
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

        #region FindDiagonalDownLeftUpperQuadrant
        private static void FindDiagonalDownLeftUpperQuadrant(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // re-organize the wordSearchArray to facilitate search
                var wordSearchArrayForDiagonalDownLeftUpperQuadrant = new StringBuilder[wordSearchArray.Length];

                // initialize all 
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    wordSearchArrayForDiagonalDownLeftUpperQuadrant[i] = new StringBuilder();
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

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in wordSearchArrayForDiagonalDownLeftUpperQuadrant)
                {
                    // wordsearch by default is in left to right so don't have to do anything here               
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
                Console.WriteLine("Error in FindDiagonalDownLeftUpperQuadrant" + ex.ToString());
            }

        }
        #endregion 
             
        #region FindDiagonalDownLeftLowerQuadrant
        private static void FindDiagonalDownLeftLowerQuadrant(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // re-organize the original contents to facilitate search 
                var wordSearchArrayForDown = new StringBuilder[wordSearchArray.Length];
                var wordSearchArrayForDiagonalDownLeftLowerQuadrant = new StringBuilder[wordSearchArray.Length];

                // initialize all 
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    wordSearchArrayForDown[i] = new StringBuilder();
                    wordSearchArrayForDiagonalDownLeftLowerQuadrant[i] = new StringBuilder();
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

                for (int i = 0; i < wordSearchArrayForDown.Length; i++)
                {
                    var wordSearchCharArrayForDiagonalDownLeftLowerQuadrant = wordSearchArrayForDown[i].ToString().ToCharArray();

                    //  Diagonal will have incremental pick up, also need to have check so we don't get array index out of bound  
                    for (int j = 0; j < wordSearchCharArrayForDiagonalDownLeftLowerQuadrant.Length; j++)
                    {
                        if ((j + i) < wordSearchCharArrayForDiagonalDownLeftLowerQuadrant.Length)
                        {
                            wordSearchArrayForDiagonalDownLeftLowerQuadrant[j].Append(wordSearchCharArrayForDiagonalDownLeftLowerQuadrant[j + i].ToString());
                        }
                    }
                }
                
                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in wordSearchArrayForDiagonalDownLeftLowerQuadrant)
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
                Console.WriteLine("Error in FindDiagonalDownLeftLowerQuadrant" + ex.ToString());
            }

        }
        #endregion 

        #region FindDiagonalUpRightUpperQuadrant
        private static void FindDiagonalUpRightUpperQuadrant(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // re-organize the wordSearchArray to facilitate search
                var wordSearchArrayForDiagonalUpRightUpperQuadrant = new StringBuilder[wordSearchArray.Length];
                // initialize all 
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    wordSearchArrayForDiagonalUpRightUpperQuadrant[i] = new StringBuilder();
                }

                for (int i = wordSearchArray.Length - 1; i >= 0; i--)
                {
                    var wordSearchCharArray = wordSearchArray[i].ToCharArray();

                    // take each char out and build the modified array, like putting stuffs in slots
                    for (int j = 0; j < wordSearchCharArray.Length; j++)
                    {
                        //  Diagonal will have imcremental pick up, also need to have check so we don't get array index out of bound
                        if ((j + i) < wordSearchCharArray.Length)
                        {
                            wordSearchArrayForDiagonalUpRightUpperQuadrant[j].Append(wordSearchCharArray[j + i].ToString());
                        }
                    }
                }

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in wordSearchArrayForDiagonalUpRightUpperQuadrant)
                {
                    // wordsearch by default is in left to right so don't have to do anything here               
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
                Console.WriteLine("Error in FindDiagonalUpRightUpperQuadrant" + ex.ToString());
            }

        }
        #endregion 
                
        #region FindDiagonalDownRight
        private static void FindDiagonalDownRight(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // re-organize the wordSearchArray to facilitate Find Diagonal Down Right
                var wordSearchArrayForDiagonalDownRight = new StringBuilder[wordSearchArray.Length];
                // initialize all 
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    wordSearchArrayForDiagonalDownRight[i] = new StringBuilder();
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
                            wordSearchArrayForDiagonalDownRight[j].Append(wordSearchCharArray[j + i].ToString());
                        }
                    }
                }

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in wordSearchArrayForDiagonalDownRight)
                {
                    // wordsearch by default is in left to right so don't have to do anything here               
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

        #region FindDiagonalUpLeft
        private static void FindDiagonalUpLeft(string[] wordSearchArray, List<string> wordList)
        {
            try
            {
                // re-organize the wordSearchArray to facilitate Find Diagonal Up Left
                var wordSearchArrayForDiagonalUpLeft = new StringBuilder[wordSearchArray.Length];
                // initialize all 
                for (int i = 0; i < wordSearchArray.Length; i++)
                {
                    wordSearchArrayForDiagonalUpLeft[i] = new StringBuilder();
                }

                for (int i = wordSearchArray.Length -1 ; i >= 0; i--)
                {
                    var wordSearchCharArray = wordSearchArray[i].ToCharArray();

                    // take each char out and build the modified array, like putting stuffs in slots
                    for (int j = 0; j < wordSearchCharArray.Length; j++)
                    {
                        //  Diagonal will have imcremental pick up, also need to have check so we don't get array index out of bound
                        if ((j + i) < wordSearchCharArray.Length)
                        {
                            wordSearchArrayForDiagonalUpLeft[j].Append(wordSearchCharArray[j + i].ToString());
                        }
                    }
                }

                // Keep track of words found in this method
                var wordsFound = new List<string>();

                foreach (var wordSearchLine in wordSearchArrayForDiagonalUpLeft)
                {
                    // wordsearch by default is in left to right so don't have to do anything here               
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
                Console.WriteLine("Error in FindDiagonalUpLeft" + ex.ToString());
            }

        }
        #endregion         
    }
}
