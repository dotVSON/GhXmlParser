// namespace GhXMLParser;
//
// using System;
//
// public class Gh_ComponentsGh_Components
// {
//     public string GUID { get; set; }
//     public string Name { get; set; }
//     public Gh_Container GhContainer { get; set; }
//
//     // Constructor
//     public Gh_Components(string guid, string name, Gh_Container ghContainer)
//     {
//         GUID = guid;
//         Name = name;
//         GhContainer = ghContainer;
//     }
//
//     // Getter functions
//     public string GetGUID() => GUID;
//     public string GetName() => Name;
//     public Gh_Container GetContainer() => GhContainer;
//
//     public class Gh_Container
//     {
//         public string Description { get; set; }
//         public string InstanceGuid { get; set; }
//         public string Name { get; set; }
//         public string NickName { get; set; }
//         public bool Optional { get; set; }
//         public int SourceCount { get; set; }
//         public Attributes Attributes { get; set; }
//         public Slider Slider { get; set; }
//
//         // Constructor
//         public Gh_Container(string description, string instanceGuid, string name, string nickName, bool optional, int sourceCount, Attributes attributes, Slider slider)
//         {
//             Description = description;
//             InstanceGuid = instanceGuid;
//             Name = name;
//             NickName = nickName;
//             Optional = optional;
//             SourceCount = sourceCount;
//             Attributes = attributes;
//             Slider = slider;
//         }
//
//         // Getter functions
//         public string GetDescription() => Description;
//         public string GetInstanceGuid() => InstanceGuid;
//         public string GetName() => Name;
//         public string GetNickName() => NickName;
//         public bool GetOptional() => Optional;
//         public int GetSourceCount() => SourceCount;
//         public Attributes GetAttributes() => Attributes;
//         public Slider GetSlider() => Slider;
//     }
//
//     public class Attributes
//     {
//         public RectangleF Bounds { get; set; }
//         public PointF Pivot { get; set; }
//         public bool Selected { get; set; }
//
//         // Constructor
//         public Attributes(RectangleF bounds, PointF pivot, bool selected)
//         {
//             Bounds = bounds;
//             Pivot = pivot;
//             Selected = selected;
//         }
//
//         // Getter functions
//         public RectangleF GetBounds() => Bounds;
//         public PointF GetPivot() => Pivot;
//         public bool GetSelected() => Selected;
//     }
//
//     public class Slider
//     {
//         public int Digits { get; set; }
//         public int GripDisplay { get; set; }
//         public int Interval { get; set; }
//         public double Max { get; set; }
//         public double Min { get; set; }
//         public int SnapCount { get; set; }
//         public double Value { get; set; }
//
//         // Constructor
//         public Slider(int digits, int gripDisplay, int interval, double max, double min, int snapCount, double value)
//         {
//             Digits = digits;
//             GripDisplay = gripDisplay;
//             Interval = interval;
//             Max = max;
//             Min = min;
//             SnapCount = snapCount;
//             Value = value;
//         }
//
//         // Getter functions
//         public int GetDigits() => Digits;
//         public int GetGripDisplay() => GripDisplay;
//         public int GetInterval() => Interval;
//         public double GetMax() => Max;
//         public double GetMin() => Min;
//         public int GetSnapCount() => SnapCount;
//         public double GetValue() => Value;
//     }
//
//     // Additional classes like RectangleF and PointF should be defined or existing structures from a library can be used.
// }
//
// // // Example of a PointF class
// // public struct PointF
// // {
// //     public float X { get; set; }
// //     public float Y { get; set; }
// //
// //     public PointF(float x, float y)
// //     {
// //         X = x;
// //         Y = y;
// //     }
// // }
//
// // Example of a RectangleF class
// public struct RectangleF
// {
//     public float X { get; set; }
//     public float Y { get; set; }
//     public float Width { get; set; }
//     public float Height { get; set; }
//
//     public RectangleF(float x, float y, float width, float height)
//     {
//         X = x;
//         Y = y;
//         Width = width;
//         Height = height;
//     }
// }
