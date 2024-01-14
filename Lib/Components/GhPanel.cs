using System.Xml;

namespace GhXMLParser.Lib.Components;

public class GhPanel : GhBaseComponent
{
    public string Text => GetText();
    public double ScrollRatio => GetScrollRatio();

    private double GetScrollRatio()
    {
        throw new NotImplementedException();
    }

    public int SourceCount => GetSourceCount();

    private int GetSourceCount()
    {
        throw new NotImplementedException();
    }

    public GhPanel(XmlDocument doc) : base(doc)
    {
    }
    
    private string GetText()
    {
        throw new NotImplementedException();
    }
}