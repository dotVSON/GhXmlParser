// See https://aka.ms/new-console-template for more information
using GhXMLParser.Lib;


namespace ConsoleTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            //Get exe path
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_gh.gh");
            var gh = new GhXMLParser.Lib.XmlGetters(path);

            Console.WriteLine(gh.GetAuthorName());
            

        }
    }
}