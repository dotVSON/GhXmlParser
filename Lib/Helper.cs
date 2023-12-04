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
    
    public struct PointF
    {
        public float X { get; set; }
        public float Y { get; set; }

        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}