using System.Drawing;
using System.Xml;
using GH_IO.Serialization;

namespace GhXMLParser;

public static class Helper
{
    public static XmlDocument GhToXml(string path)
    {
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
    /// Get all object chunks as separate XML documents.
    /// </summary>
    /// <returns>A list of XML documents, each representing an object.</returns>
    public static List<XmlDocument> GetAllObjectsAsXml(XmlDocument doc)
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
    
    public static float GetFloatFromNode(XmlNode parentNode, string childNodeName)
    {
        var childNode = parentNode.SelectSingleNode(childNodeName);
        if (childNode == null || !float.TryParse(childNode.InnerText, out float value))
        {
            throw new InvalidOperationException($"{childNodeName} is missing or invalid in XML.");
        }
        return value;
    }

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