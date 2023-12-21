# GhXMLParser

This project is a C# library for parsing Grasshopper files (`.gh`,`.ghx`) in XML format. It provides a set of classes and helper methods to extract data from Grasshopper files.

## Features

- Convert Grasshopper files to XML documents.
- Extract various information from the XML documents such as GUID, Name, Description, InstanceGuid, Nickname, Optional, Bounds, Pivot, Selected, Inputs, and Outputs.
- Handle Grasshopper components and their dependencies.
- Handle Grasshopper component inputs and outputs.

## Classes

- `Helper`: Provides helper methods for parsing Grasshopper files.
- `GhComponent`: Represents a Grasshopper component with various properties and methods to extract data.
- `GhHeader`: TODO
- `GhComponentDependency`: TODO
- `GhComponentInput`: TODO
- `GhComponentOutput`: TODO

## Usage

To use this library, you need to have a Grasshopper file. You can convert this file to an XML document using the `GrasshopperToXml` method from the `Helper` class

## Example

```csharp
string path = "path to gh or ghx file";
//Convert the grasshopper file to xml
var grasshopperXmlDoc = Helper.GrasshopperToXml(path);

//Get the header of the grasshopper file
var header = new GhHeader(grasshopperXmlDoc);

//Get all the components in the grasshopper file
var xmlComponents = Helper.GetAllComponentsAsXml(grasshopperXmlDoc);
var allComponents = xmlComponents.Select(component => new GhComponent(component)).ToList();
