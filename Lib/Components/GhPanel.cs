using System.Drawing;
using System.Xml;

namespace GhXMLParser.Lib.Components;

public class GhPanel : GhBaseComponent
{
    public string UserText => GetUserText();
    
    //Panel Properties
    public Color  Colour { get => GetColour(); }
    public bool DrawIndices { get => GetDrawIndices(); }
    public bool DrawPaths { get => GetDrawPaths(); }
    public bool Multiline { get => GetMultiline(); }
    public bool SpecialCodes { get => GetSpecialCodes(); }
    public bool Stream { get => GetStream(); }
    public bool Wrap { get => GetWrap(); }
    
    public GhPanel(Parser.ComponentXml doc) : base(doc)
    {
    }

    #region Getters
    
    private bool GetPanelPropertyAsBool(string propertyName)
    {
        var value = GetPanelProperty(propertyName);
        return bool.Parse(value);
    }
    
    private Color GetColour()
    {
        string argbString = GetPanelProperty("Colour");
        return ParseArgbToColor(argbString);
    }

    private bool GetDrawIndices()
    {
        return GetPanelPropertyAsBool("DrawIndices");
    }

    private bool GetDrawPaths()
    {
        return GetPanelPropertyAsBool("DrawPaths");
    }

    private bool GetMultiline()
    {
        return GetPanelPropertyAsBool("Multiline");
    }

    private bool GetSpecialCodes()
    {
        return GetPanelPropertyAsBool("SpecialCodes");
    }

    private bool GetStream()
    {
        return GetPanelPropertyAsBool("Stream");
    }

    private bool GetWrap()
    {
        return GetPanelPropertyAsBool("Wrap");
    }

    #endregion

    #region Helpers
    
    private string GetUserText()
    {
        var node = doc.SelectSingleNode("//chunk[@name='Object']/items/item[@name='UserText']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("UserText is missing or empty in XML.");
        }

        return node.InnerText;
    }
    
    private string GetPanelProperty(string propertyName)
    {
        var node = doc.SelectSingleNode($"//chunk[@name='Object']//chunk//chunk[@name='PanelProperties']/items/item[@name='{propertyName}']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException($"{propertyName} is missing or empty in XML.");
        }
        return node.InnerText;
    }

    
    private Color ParseArgbToColor(string argb)
    {
        var parts = argb.Split(';');
        if (parts.Length != 4)
        {
            throw new FormatException("ARGB string is not in the correct format.");
        }

        return Color.FromArgb(
            int.Parse(parts[0]), 
            int.Parse(parts[1]), 
            int.Parse(parts[2]), 
            int.Parse(parts[3])
        );
    }

    #endregion


}