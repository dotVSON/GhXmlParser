// See https://aka.ms/new-console-template for more information
using GhXMLParser;


namespace ConsoleTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Get exe path (gh or ghx)
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GH_Test_Bench.ghx");
            
            //Convert the grasshopper file to xml
            var grasshopperXmlDoc = Helper.GrasshopperToXml(path);
            
            //Get the header of the grasshopper file
            var header = new GhHeader(grasshopperXmlDoc);
            Console.WriteLine(header.ComponentCount);
            var dep = header.Dependencies;
            
            //Get all the components in the grasshopper file
            var xmlComponent = Helper.GetAllComponentsAsXml(grasshopperXmlDoc);
            var allComponents = xmlComponent.Select(component => new GhComponent(component)).ToList();

            foreach (var component in allComponents)
            {
                Console.WriteLine(component.Name);
                Console.WriteLine(component.InstanceGuid);
                Console.WriteLine(component.Description);
                try
                {
                    Console.WriteLine(component.Bounds);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"the component {component.Name} is missing the bounds =>  {e.Message}");
                }
            }
        }
    }
}