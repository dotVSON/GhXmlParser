using System.Drawing;
using System.Xml;

namespace GhXMLParser;

/// <summary>
/// Describes a Grasshopper components outputs.
/// </summary>
public class GhComponentOutput
{
     private readonly XmlDocument doc;
    public string Description => GetDescription();
    public string InstanceGuid => GetInstanceGuid();
    public string Name  => GetName();
    public string NickName => GetNickName();
    public bool IsOptional => GetOptional();
    // public string Source => GetSource();
    public int SourceCount => GetSourceCount();
    public Rectangle Bounds => GetBounds();

    public PointF Pivot => GetPivot();

    
    // Constructor
    public GhComponentOutput(XmlDocument doc)
    {
        this.doc = doc;
        // Console.WriteLine(doc.OuterXml);
    }
    
    /// <summary>
    /// Gets the description of the component.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string GetDescription()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_output']/items/item[@name='Description']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Description is missing or empty in XML.");
        }
        
        return node.InnerText;
    }

    /// <summary>
    /// Gets the instance GUID of the component.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string GetInstanceGuid()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_output']/items/item[@name='InstanceGuid']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("InstanceGuid is missing or empty in XML.");
        }

        return node.InnerText;
    }

    /// <summary>
    /// Gets the name of the component.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string GetName()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_output']/items/item[@name='Name']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Name is missing or empty in XML.");
        }

        return node.InnerText;
    }

    /// <summary>
    /// Gets the nickname of the component.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string GetNickName()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_output']/items/item[@name='NickName']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("NickName is missing or empty in XML.");
        }

        return node.InnerText;
    }

    /// <summary>
    /// Gets the optional status of the component.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool GetOptional()
    {
        var optionalStr = doc.SelectSingleNode("//chunk[@name='param_output']/items/item[@name='Optional']");
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

    // public string GetSource()
    // {

     //    /
     //
     //    var node = doc.SelectSingleNode("//chunk[@name='param_output']/items/item[@name='Source']");
     //    //     if (node == null || string.IsNullOrEmpty(node.InnerText))
     //    //     {
     //    //         throw new InvalidOperationException("Source is missing or empty in XML.");
     //    //     }
     //    //
     //    //     return node.InnerText;
     //    // }
     //
     // */
 
     /// <summary>
     /// Gets the bounds of the component.
     /// </summary>
     /// <returns></returns>
     /// <exception cref="InvalidOperationException"></exception>
     private Rectangle GetBounds()
     {
         var node = doc.SelectSingleNode("//chunk[@name='Attributes']/items/item[@name='Bounds']");
         if (node == null)
         {
             throw new InvalidOperationException("Bounds is missing in XML.");
         }

         return Helper.NodeToBounds(node);
     }
     
     /// <summary>
     /// Gets the pivot of the component.
     /// </summary>
     /// <returns></returns>
     /// <exception cref="InvalidOperationException"></exception>
     private PointF GetPivot()
     {
         var node = doc.SelectSingleNode("//chunk[@name='Attributes']/items/item[@name='Pivot']");

         if (node == null)
         {
             throw new InvalidOperationException("Pivot is missing in XML.");
         }
        
         return Helper.NodeToPoint(node);
     }
    
    /// <summary>
    /// Gets the source count of the component.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int GetSourceCount()
    {
        var sourceCountStr = doc.SelectSingleNode("//chunk[@name='param_output']/items/item[@name='SourceCount']");
        if (sourceCountStr == null || string.IsNullOrEmpty(sourceCountStr.InnerText))
        {
            throw new InvalidOperationException("SourceCount is missing or empty in XML.");
        }

        if (!int.TryParse(sourceCountStr.InnerText, out var sourceCount))
        {
            throw new InvalidOperationException("Invalid format for SourceCount in XML.");
        }

        return sourceCount;
    }
    
    /// <summary>
    /// Gets the source of the component.
    /// </summary>
    /// <param name="parentNode"></param>
    /// <param name="childNodeName"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private float GetFloatFromNode(XmlNode parentNode, string childNodeName)
    {
        var childNode = parentNode.SelectSingleNode(childNodeName);
        Console.WriteLine(childNode.InnerText);
        if (childNode == null || !float.TryParse(childNode.InnerText, out float value))
        {
            throw new InvalidOperationException($"{childNodeName} is missing or invalid in XML.");
        }
        return value;
    }
}