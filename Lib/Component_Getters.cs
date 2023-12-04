// using System.Xml;
//
// namespace GhXMLParser;
//
// public class Component_Getters
// {
//     
//     private XmlDocument doc;
//     
//     public string GetGUID()
//     {
//         var node = doc.SelectSingleNode("//chunk[@name='Object']/items/item[@name='GUID']");
//         if (node == null || string.IsNullOrEmpty(node.InnerText))
//         {
//             throw new InvalidOperationException("GUID is missing or empty in XML.");
//         }
//
//         return node.InnerText;
//     }
//
//     public string GetName()
//     {
//         var node = doc.SelectSingleNode("//chunk[@name='Object']/items/item[@name='Name']");
//         if (node == null || string.IsNullOrEmpty(node.InnerText))
//         {
//             throw new InvalidOperationException("Name is missing or empty in XML.");
//         }
//
//         return node.InnerText;
//     }
//
//     public string GetDescription()
//     {
//         var node = doc.SelectSingleNode("//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='Description']");
//         if (node == null || string.IsNullOrEmpty(node.InnerText))
//         {
//             throw new InvalidOperationException("Description is missing or empty in XML.");
//         }
//
//         return node.InnerText;
//     }
//     
//     public string GetInstanceGuid()
//     {
//         var node = doc.SelectSingleNode("//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='InstanceGuid']");
//         if (node == null || string.IsNullOrEmpty(node.InnerText))
//         {
//             throw new InvalidOperationException("InstanceGuid is missing or empty in XML.");
//         }
//
//         return node.InnerText;
//     }
//
//     public bool GetOptional()
//     {
//         var optionalStr = GetItemInnerText("//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='Optional']");
//         if (!bool.TryParse(optionalStr, out bool optional))
//         {
//             throw new InvalidOperationException("Invalid format for Optional in XML.");
//         }
//         return optional;
//     }
//
//     public int GetSourceCount()
//     {
//         var sourceCountStr = GetItemInnerText("//chunk[@name='Object']/chunks/chunk[@name='Container']/items/item[@name='SourceCount']");
//         if (!int.TryParse(sourceCountStr, out int sourceCount))
//         {
//             throw new InvalidOperationException("Invalid format for Source Count in XML.");
//         }
//         return sourceCount;
//     }
//     
//     public RectangleF GetBounds()
//     {
//         var node = doc.SelectSingleNode("//chunk[@name='Attributes']/items/item[@name='Bounds']");
//         if (node == null)
//         {
//             throw new InvalidOperationException("Bounds data is missing in XML.");
//         }
//
//         float x = GetFloatValue(node, "X");
//         float y = GetFloatValue(node, "Y");
//         float width = GetFloatValue(node, "W");
//         float height = GetFloatValue(node, "H");
//
//         return new RectangleF(x, y, width, height);
//     }
//     
//     
//     /// <summary>
//     /// Helper function to get the inner text of an item.
//     /// </summary>
//     /// <param name="xpath"></param>
//     /// <returns></returns>
//     /// <exception cref="InvalidOperationException"></exception>
//     
//     private string GetItemInnerText(string xpath)
//     {
//         var node = doc.SelectSingleNode(xpath);
//         if (node == null || string.IsNullOrEmpty(node.InnerText))
//         {
//             throw new InvalidOperationException($"Item not found or empty for XPath: {xpath}");
//         }
//         return node.InnerText;
//     }
//     
//     private string GetStringValue(string xpath)
//     {
//         var node = doc.SelectSingleNode(xpath);
//         if (node == null || string.IsNullOrEmpty(node.InnerText))
//         {
//             throw new InvalidOperationException($"String value not found or empty for XPath: {xpath}");
//         }
//         return node.InnerText;
//     }
//
//     private int GetIntValue(string xpath)
//     {
//         var stringValue = GetStringValue(xpath);
//         if (!int.TryParse(stringValue, out int intValue))
//         {
//             throw new InvalidOperationException($"Invalid integer format for XPath: {xpath}");
//         }
//         return intValue;
//     }
//     
//     private float GetFloatValue(XmlNode parentNode, string childNodeName)
//     {
//         var childNode = parentNode.SelectSingleNode(childNodeName);
//         if (childNode == null || !float.TryParse(childNode.InnerText, out float value))
//         {
//             throw new InvalidOperationException($"Invalid or missing {childNodeName} value in XML.");
//         }
//         return value;
//     }
//
//     private bool GetBoolValue(string xpath)
//     {
//         var stringValue = GetStringValue(xpath);
//         if (!bool.TryParse(stringValue, out bool boolValue))
//         {
//             throw new InvalidOperationException($"Invalid boolean format for XPath: {xpath}");
//         }
//         return boolValue;
//     }
// }