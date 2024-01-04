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
    /// Converts a Grasshopper file to an XmlDocument.
    /// </summary>
    /// <param name="path">The file path of the Grasshopper file to be converted.</param>
    /// <returns>An XmlDocument representing the serialized Grasshopper file content.</returns>
    /// <exception cref="IOException">Thrown when there is an issue reading the file from the provided path.</exception>
    /// <exception cref="InvalidOperationException">Thrown when serialization of the file content returns null or empty.</exception>
    /// <exception cref="XmlException">Thrown when there is an error in loading the XML content into the XmlDocument.</exception>
    /// <example>
    /// <code>
    /// string path = "path_to_grasshopper_file.gh";
    /// try
    /// {
    ///     XmlDocument xmlDoc = GrasshopperToXml(path);
    ///     // Use xmlDoc as needed.
    /// }
    /// catch (Exception ex)
    /// {
    ///     Console.WriteLine("An error occurred: " + ex.Message);
    /// }
    /// </code>
    /// </example>
    public static XmlDocument GrasshopperToXml(string path)
    { 
        //Credit: https://www.grasshopper3d.com/forum/topics/example-on-how-to-use-gh-io-dll
        var doc = new XmlDocument();
        try
        {
            var archive = new GH_Archive();

            // Attempt to read from the file
            if (!archive.ReadFromFile(path))
            {
                throw new IOException($"Failed to read from file at {path}.");
            }

            // Attempt to serialize to XML
            string xmlContent = archive.Serialize_Xml();
            if (string.IsNullOrEmpty(xmlContent))
            {
                throw new InvalidOperationException("Serialization of the archive returned null or empty content.");
            }

            // Load serialized content into XmlDocument
            doc.LoadXml(xmlContent);
            //get path to desktop
            string desktio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //save xml to desktop
        }
        catch (XmlException ex)
        {
            // Handle XML-specific errors (e.g., malformed XML content)
            Console.WriteLine($"XML error: {ex.Message}");
        }
        catch (IOException ex)
        {
            // Handle IO errors (e.g., file not found, no permission to read file, etc.)
            Console.WriteLine($"IO error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        return doc;
    }
    
    /// <summary>
    /// Get all object chunks (components) as separate XML documents.
    /// </summary>
    /// <returns>A list of XML documents, each representing an object.</returns>
    public static List<XmlDocument> GetAllComponentsAsXml(XmlDocument doc)
    {
        var objectXmlList = new List<XmlDocument>();

        // This XPath targets any 'Object' chunk within 'DefinitionObjects'
        var objectChunks = doc.SelectNodes("//chunk[@name='DefinitionObjects']//chunk[@name='Object']");

        foreach (XmlElement objectChunk in objectChunks)
        {
            var objectXml = new XmlDocument();
            var importNode = objectXml.ImportNode(objectChunk, true);
            objectXml.AppendChild(importNode);
            objectXmlList.Add(objectXml);
        }

        return objectXmlList;
    }
    
    

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
    /// <remarks>
    /// The Rectangle structure is used to define the boundaries of a rectangular area.
    /// It is commonly used in graphics programming and user interface design.
    /// </remarks>
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