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
        static void Main(string[] args)
        {
            try
            {
                // WordSearch.txt and WordList.txt are embedded resources in this assembly, load these files in stream readers 
                var assembly = Assembly.GetExecutingAssembly();
                var wordSearchStreamReader = new StreamReader(assembly.GetManifestResourceStream("WordSearch.WordSearch.txt"));
                var wordListStreamReader = new StreamReader(assembly.GetManifestResourceStream("WordSearch.WordList.txt"));


                Console.WriteLine(wordSearchStreamReader.ReadToEnd());
                Console.WriteLine(wordListStreamReader.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error accessing resources" + ex.ToString());
            }

            // exit the console app     
            Console.Write("Press any key to exit");
            Console.ReadKey();
        }
    }
}
