using System.Xml;
using GH_IO.Serialization;

namespace GhXMLParser;

public static class Parser
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
}