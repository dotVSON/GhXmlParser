﻿using System.Drawing;
using System.Xml;

namespace GhXMLParser.Lib;

/// <summary>
/// Describes a Grasshopper component input.
/// </summary>
public class GhComponentInput
{
    private readonly XmlDocument doc;
    public string Description => GetDescription();
    public string InstanceGuid => GetInstanceGuid();
    public string Name  => GetName();
    public string NickName => GetNickName();
    public bool IsOptional => GetOptional();
    public string Source => GetSource();
    public int SourceCount => GetSourceCount();
    public string? Type {get; set;}

    public Rectangle Bounds => GetBounds();
    public PointF Pivot => GetPivot();
    
    // Constructor
    public GhComponentInput(XmlDocument doc)
    {
        this.doc = doc;
    }
    
    public string GetDescription()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_input']/items/item[@name='Description']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Description is missing or empty in XML.");
        }
        
        return node.InnerText;
    }

    public string GetInstanceGuid()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_input']/items/item[@name='InstanceGuid']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("InstanceGuid is missing or empty in XML.");
        }

        return node.InnerText;
    }

    public string GetName()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_input']/items/item[@name='Name']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Name is missing or empty in XML.");
        }

        return node.InnerText;
    }

    public string GetNickName()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_input']/items/item[@name='NickName']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("NickName is missing or empty in XML.");
        }

        return node.InnerText;
    }

    public bool GetOptional()
    {
        var optionalStr = doc.SelectSingleNode("//chunk[@name='param_input']/items/item[@name='Optional']");
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

    public string GetSource()
    {
        var node = doc.SelectSingleNode("//chunk[@name='param_input']/items/item[@name='Source']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Source is missing or empty in XML.");
        }

        return node.InnerText;
    }

    public int GetSourceCount()
    {
        var sourceCountStr = doc.SelectSingleNode("//chunk[@name='param_input']/items/item[@name='SourceCount']");
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
    
    private Rectangle GetBounds()
    {
        var node = doc.SelectSingleNode("//chunk[@name='Attributes']/items/item[@name='Bounds']");
        if (node == null)
        {
            throw new InvalidOperationException("Bounds is missing in XML.");
        }
        return Helper.NodeToBounds(node);
    }
     
    private PointF GetPivot()
    {
        var node = doc.SelectSingleNode("//chunk[@name='Attributes']/items/item[@name='Pivot']");

        if (node == null)
        {
            throw new InvalidOperationException("Pivot is missing in XML.");
        }
        
        return Helper.NodeToPoint(node);
    }
}