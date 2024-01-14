// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Drawing.Imaging;
using GhXMLParser;
using GhXMLParser.Lib;
using GhXMLParser.Lib.Components;


namespace ConsoleTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Get exe path (gh or ghx)
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GH_Test_Bench.ghx");
            
            //Convert the grasshopper file to xml
            var grasshopperXmlDoc = Parser.GrasshopperToXml(path);
            
            //Get the header of the grasshopper file
            var header = new GhHeader(grasshopperXmlDoc);

            //Get all the components in the grasshopper file
            var xmlComponents = Parser.GetAllComponentsAsXml(grasshopperXmlDoc);
            
            var slider = xmlComponents[0];
            var comp = Parser.ParseComponentXml(slider);
            
            Console.WriteLine(comp);

            var inner = slider.SelectSingleNode("//chunk[@name='Object']//chunk//chunk[@name='Slider']/items/item[@name='Max']");

            var impSlider = new GhSlider(slider);


            // var allComponents = xmlComponents.Select(component => new GhBaseComponent(component)).ToList();

            // foreach (var component in allComponents)
            // {
            //     Console.WriteLine(component.Name);
            //     Console.WriteLine(component.InstanceGuid);
            //     Console.WriteLine(component.Description);
            //     try
            //     {
            //         Console.WriteLine(component.Bounds);
            //     }
            //     catch (Exception e)
            //     {
            //         Console.WriteLine($"the component {component.Name} is missing the bounds =>  {e.Message}");
            //     }
            // }
        }
    }
}