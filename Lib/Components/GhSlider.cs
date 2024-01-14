using System.Xml;

namespace GhXMLParser.Lib.Components;

public class GhSlider : GhBaseComponent
{
    //Chunk path = //chunk[@name='Object']//chunk//chunk[@name='Slider']/items/item[@name='PARAMETER_NAME']
    public int Digits => GetDigits();
    public int GripDisplay => GetGripDisplay();
    public int Interval => GetInterval();
    public double Max => GetMax();
    public double Min => GetMin();
    public int SnapCount => GetSnapCount();
    public double Value => GetValue();
    
    public GhSlider(Parser.ComponentXml doc) : base(doc)
    {
    }
    
    #region Getters
    
    private string GetSliderProperty(string propertyName)
    {
        var node = doc.SelectSingleNode($"//chunk[@name='Object']//chunk//chunk[@name='Slider']/items/item[@name='{propertyName}']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException($"{propertyName} is missing or empty in XML.");
        }
        return node.InnerText;
    }

    private int GetDigits()
    {
        var digitsString = GetSliderProperty("Digits");
        return int.Parse(digitsString);
    }

    private int GetGripDisplay()
    {
        var gripDisplayString = GetSliderProperty("GripDisplay");
        return int.Parse(gripDisplayString);
    }

    private int GetInterval()
    {
        var intervalString = GetSliderProperty("Interval");
        return int.Parse(intervalString);
    }

    private double GetMax()
    {
        var maxString = GetSliderProperty("Max");
        return double.Parse(maxString);
    }

    private double GetMin()
    {
        var minString = GetSliderProperty("Min");
        return double.Parse(minString);
    }

    private int GetSnapCount()
    {
        var snapCountString = GetSliderProperty("SnapCount");
        return int.Parse(snapCountString);
    }

    private double GetValue()
    {
        var valueString = GetSliderProperty("Value");
        return double.Parse(valueString);
    }

    #endregion
}