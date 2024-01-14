using System.Drawing;
using System.Xml;
using GH_IO.Serialization;

namespace GhXMLParser;

/// <summary>
/// Helper methods for parsing Grasshopper files.
/// </summary>
public static class Helper
{
    /// <summary>
    /// Retrieves a float value from a specified child node of a parent XML node.
    /// </summary>
    /// <param name="parentNode">The parent XML node.</param>
    /// <param name="childNodeName">The name of the child node.</param>
    /// <returns>The float value retrieved from the child node.</returns>
    public static float GetFloatFromNode(XmlNode parentNode, string childNodeName)
    {
        var childNode = parentNode.SelectSingleNode(childNodeName);
        if (childNode == null || !float.TryParse(childNode.InnerText, out float value))
        {
            throw new InvalidOperationException($"{childNodeName} is missing or invalid in XML.");
        }
        return value;
    }


    /// <summary>
    /// Represents a rectangle in a two-dimensional plane.
    /// </summary>
    public static Rectangle NodeToBounds(XmlNode node)
    {
        if (node == null)
        {
            throw new InvalidOperationException("Bounds is missing in XML.");
        }
        try
        {
            float x = GetFloatFromNode(node, "X");
            float y = GetFloatFromNode(node, "Y");
            float width = GetFloatFromNode(node, "W");
            float height = GetFloatFromNode(node, "H");

            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    public static bool CheckNamedComponent(string componentName, Parser.ComponentXml componentChunk)
    {
        // Check if the componentName parameter is valid
        if (string.IsNullOrWhiteSpace(componentName))
        {
            throw new ArgumentException("Component name cannot be null or whitespace.", nameof(componentName));
        }

        // Search for the component in the XML component chunk
        var node = componentChunk.SelectSingleNode($"//chunk[@name='Object']/items/item[@name='Name']");
        
        // If the component is not found, return false
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            return false;
        }

        Console.WriteLine(node.InnerText);
        // If the component is found, check if the name matches the componentName parameter
        return node.InnerText == componentName;
    }
    
    /// <summary>
    /// Converts an XML node to a PointF object.
    /// </summary>
    /// <param name="node">The XML node containing the X and Y coordinates.</param>
    /// <returns>A PointF object representing the X and Y coordinates.</returns>
    public static PointF NodeToPoint(XmlNode node)
    {
        if (node == null)
        {
            throw new InvalidOperationException("Point is missing in XML.");
        }
        try
        {
            float x = GetFloatFromNode(node, "X");
            float y = GetFloatFromNode(node, "Y");

            return new PointF(x, y);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}