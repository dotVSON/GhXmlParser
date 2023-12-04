// See https://aka.ms/new-console-template for more information
using GhXMLParser;


namespace ConsoleTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            //Get exe path
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_gh.gh");
            
            var xmlDoc = Helper.GhToXml(path);
            var header = new GhHeader(xmlDoc);
            var xmlComponent = Helper.GetAllObjectsAsXml(xmlDoc);
            var allComponents = xmlComponent.Select(component => new GhComponent(component)).ToList();

            // Console.WriteLine(allComponents[0].XmlInputs[0].InnerXml);
        }
    }
}