using System.Drawing;
using System.Xml;

namespace GhXMLParser.Lib.Components;

public class GhBaseComponent
{
    public readonly XmlDocument doc;
    public string GUID => GetGUID();
    public string Name => GetName();
    public string Description => GetDescription();
    public string InstanceGuid => GetInstanceGuid();
    public string Nickname => GetNickname();
    public bool? IsOptional => GetOptional();
    public Rectangle Bounds => GetBounds();
    public PointF Pivot => GetPivot();
    public bool IsSelected => GetIsSelected();
    public List<GhComponentInput> Inputs { get; } = new List<GhComponentInput>();
    public List<GhComponentOutput> Outputs { get; } = new List<GhComponentOutput>();
    public List<XmlDocument> XmlInputs => GetAllXmlInputs();
    public List<XmlDocument> XmlOutputs => GetAllXmlOutputs();

    public GhBaseComponent(XmlDocument doc)
    {
        this.doc = doc;
        foreach (var input in XmlInputs)
        {
            var ghInput = new GhComponentInput(input);
            if (ghInput != null)
            {
                Inputs.Add(ghInput);
            }
        }

        foreach (var output in XmlOutputs)
        {
            var ghOutput = new GhComponentOutput(output);
            if (ghOutput != null)
            {
                Outputs.Add(ghOutput);
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

    private bool? GetOptional()
    {
        var optionalStr =
            doc.SelectSingleNode(
                "//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='Optional']");

        if (optionalStr == null || string.IsNullOrEmpty(optionalStr.InnerText))
        {
            return null;
        }

        if (!bool.TryParse(optionalStr.InnerText, out var optional))
        {
            throw new InvalidOperationException("Invalid format for Optional in XML.");
        }

        return optional;
    }

    private Rectangle GetBounds()
    {
        //TODO : check if the bounds are missing or component is actually a group
        
        var node = doc.SelectSingleNode("//chunk[@name='Object']/chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Bounds']");
        if (node == null)
        {
            throw new InvalidOperationException("Bounds is missing in XML.");
        }

        float x = Helper.GetFloatFromNode(node, "X");
        float y = Helper.GetFloatFromNode(node, "Y");
        float width = Helper.GetFloatFromNode(node, "W");
        float height = Helper.GetFloatFromNode(node, "H");

        return new Rectangle((int)x, (int)y, (int)width, (int)height);
    }

    private PointF GetPivot()
    {
        var node = doc.SelectSingleNode(
            "//chunk[@name='Object']/chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Pivot']");

        if (node == null)
        {
            throw new InvalidOperationException("Pivot is missing in XML.");
        }
        
        float x = Helper.GetFloatFromNode(node, "X");
        float y = Helper.GetFloatFromNode(node, "Y");
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

    private List<XmlDocument> GetAllXmlInputs()
    {
        var paramInputXmlList = new List<XmlDocument>();

        var paramInputChunks = doc.SelectNodes("//chunk[@name='param_input']");

        if (paramInputChunks == null)
        {
            throw new InvalidOperationException("param_input is missing in XML.");
        }

        foreach (XmlElement paramInputChunk in paramInputChunks)
        {
            var paramInputXml = new XmlDocument();
            var importNode = paramInputXml.ImportNode(paramInputChunk, true);
            paramInputXml.AppendChild(importNode);
            paramInputXmlList.Add(paramInputXml);
        }
        return paramInputXmlList;
    }
    
    private List<XmlDocument> GetAllXmlOutputs()
    {
        var paramInputXmlList = new List<XmlDocument>();

        var paramInputChunks = doc.SelectNodes("//chunk[@name='param_output']");

        if (paramInputChunks == null)
        {
            throw new InvalidOperationException("param_output is missing in XML.");
        }

        foreach (XmlElement paramInputChunk in paramInputChunks)
        {
            var paramInputXml = new XmlDocument();
            var importNode = paramInputXml.ImportNode(paramInputChunk, true);
            paramInputXml.AppendChild(importNode);
            paramInputXmlList.Add(paramInputXml);
        }
        return paramInputXmlList;
    }
}