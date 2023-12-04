using System.Xml;
using System.Drawing;
using System.Threading.Channels;

namespace GhXMLParser;

public class GhComponent
{
    private readonly XmlDocument doc;
    public string GUID => GetGUID();
    public string Name => GetName();
    public string Description => GetDescription();
    public string InstanceGuid => GetInstanceGuid();
    public string Nickname => GetNickname();
    public bool IsOptional => GetOptional();
    public Rectangle Bounds => GetBounds();
    public PointF Pivot => GetPivot();
    public bool IsSelected => GetIsSelected();
    
    public List<GhInput> Inputs { get; } = new List<GhInput>();
    public List<XmlDocument> XmlInputs => GetAllXmlInputs();

    public GhComponent(XmlDocument doc)
    {
        this.doc = doc;
        foreach (var input in XmlInputs)
        {
            var ghInput = new GhInput(input);
            if (ghInput != null)
            {
                Inputs.Add(ghInput);
            }
        }
    }

    private string GetGUID()
    {
        var node = doc.SelectSingleNode("//chunk[@name='Object']/items/item[@name='GUID']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("GUID is missing or empty in XML.");
        }

        return node.InnerText;
    }

    private string GetName()
    {
        var node = doc.SelectSingleNode("//chunk[@name='Object']/items/item[@name='Name']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Name is missing or empty in XML.");
        }

        return node.InnerText;
    }

    private string GetDescription()
    {
        var node = doc.SelectSingleNode(
            "//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='Description']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Description is missing or empty in XML.");
        }

        return node.InnerText;
    }

    private string GetInstanceGuid()
    {
        var node = doc.SelectSingleNode(
            "//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='InstanceGuid']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("InstanceGuid is missing or empty in XML.");
        }

        return node.InnerText;
    }

    private string GetNickname()
    {
        var node = doc.SelectSingleNode(
            "//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='NickName']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("InstanceGuid is missing or empty in XML.");
        }

        return node.InnerText;
    }

    private bool GetOptional()
    {
        var optionalStr =
            doc.SelectSingleNode(
                "//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='Optional']");
        if (optionalStr == null || string.IsNullOrEmpty(optionalStr.InnerText))
        {
            throw new InvalidOperationException("Optional is missing or empty in XML.");
        }

        if (!bool.TryParse(optionalStr.InnerText, out var optional))
        {
            throw new InvalidOperationException("Invalid format for Optional in XML.");
        }

        return optional;
    }

    private Rectangle GetBounds()
    {
        var node = doc.SelectSingleNode("//chunk[@name='Object']/chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Bounds']");
        if (node == null)
        {
            throw new InvalidOperationException("Bounds is missing in XML.");
        }

        int x = GetIntFromNode(node, "X");
        int y = GetIntFromNode(node, "Y");
        int width = GetIntFromNode(node, "W");
        int height = GetIntFromNode(node, "H");

        return new Rectangle(x, y, width, height);
    }

    private PointF GetPivot()
    {
        var node = doc.SelectSingleNode(
            "//chunk[@name='Object']/chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Pivot']");

        if (node == null)
        {
            throw new InvalidOperationException("Pivot is missing in XML.");
        }
        
        float x = GetIntFromNode(node, "X");
        float y = GetIntFromNode(node, "Y");
        return new PointF(x, y);
    }
    
    private bool GetIsSelected()
    {
        var node = doc.SelectSingleNode(
            "//chunk[@name='Object']/chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Selected']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Selected is missing or empty in XML.");
        }

        if (!bool.TryParse(node.InnerText, out var selected))
        {
            throw new InvalidOperationException("Invalid format for Selected in XML.");
        }

        return selected;
    }
    
    
    private int GetIntFromNode(XmlNode parentNode, string childNodeName)
    {
        var childNode = parentNode.SelectSingleNode(childNodeName);
        if (childNode == null || !int.TryParse(childNode.InnerText, out int value))
        {
            throw new InvalidOperationException($"{childNodeName} is missing or invalid in XML.");
        }
        return value;
    }

    private List<XmlDocument> GetAllXmlInputs()
    {
        var objectXmlList = new List<XmlDocument>();
        var objectChunks =
            doc.SelectNodes("//chunk[@name='Object']/chunk[@name='Container']/chunks/chunk[@name='param_input']");
        foreach (XmlElement objectChunk in objectChunks)
        {
            var objectXml = new XmlDocument();
            var importNode = objectXml.ImportNode(objectChunk, true);
            objectXml.AppendChild(importNode);
            objectXmlList.Add(objectXml);
        }

        Console.WriteLine($"Component {Name} has {objectXmlList.Count} inputs.");
        return objectXmlList;
    }
}