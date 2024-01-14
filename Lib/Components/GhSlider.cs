using System.Xml;

namespace GhXMLParser.Lib.Components;

public class GhSlider: GhBaseComponent
{
    public double Min => GetMin();

    private double GetMin()
    {
        throw new NotImplementedException();
    }

    public double Max => GetMax();

    private double GetMax()
    {
        throw new NotImplementedException();
    }

    public double Step => GetStep();

    private double GetStep()
    {
        throw new NotImplementedException();
    }

    public double Value => GetValue();

    private double GetValue()
    {
        throw new NotImplementedException();
    }

    public GhSlider(XmlDocument doc) : base(doc)
    {
    }
}