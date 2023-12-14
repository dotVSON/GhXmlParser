// See https://aka.ms/new-console-template for more information
using GhXMLParser;


namespace ConsoleTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Get exe path
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Orbb.gh");
            
            var xmlDoc = Helper.GhToXml(path);
            var header = new GhHeader(xmlDoc);
            var dep = header.Dependencies;
            var xmlComponent = Helper.GetAllObjectsAsXml(xmlDoc);
            var allComponents = xmlComponent.Select(component => new GhComponent(component)).ToList();

            foreach (var component in  allComponents)
            {
                Console.WriteLine(component.Name);
                Console.WriteLine($"Number of inputs {component.Inputs.Count}");
                
            }
        }
    }
}