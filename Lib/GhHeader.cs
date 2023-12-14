using System.Xml;
using GH_IO.Serialization;
using System.Linq;

namespace GhXMLParser
{
    public class GhHeader
    {
        private readonly XmlDocument doc;
        
        public string ArchiveVersion => GetArchiveVersion();
        public string GrasshopperVersion => GetGrasshopperVersion();
        public string DocumentId => GetDocumentId();
        public string DocumentDate => GetDocumentDate();
        public string DocumentDescription => GetDocumentDescription();
        public string DocumentName => GetDocumentName();
        public (int targetX, int targetY, float zoom) ProjectionDetails => GetProjectionDetails();
        public string AuthorEmail => GetAuthorEmail();
        public string AuthorName => GetAuthorName();
        public string AuthorCopyRight => GetAuthorCopyright();
        public int ComponentCount => GetComponentCount();
        public List<GhDependency> Dependencies => GetDependency();

        public GhHeader(XmlDocument doc)
        {
            this.doc = doc;
        }

        #region Definition properties
        
        /// <summary>
        /// Get the plugin version from the GH file.
        /// </summary>
        /// <returns>The plugin version in 'Major.Minor.Revision' format.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetArchiveVersion()
        {
            var versionNode = doc.SelectSingleNode("//item[@name='ArchiveVersion']");
            if (versionNode == null)
            {
                throw new InvalidOperationException("Plugin version node is missing in XML.");
            }

            var majorNode = versionNode.SelectSingleNode("Major");
            var minorNode = versionNode.SelectSingleNode("Minor");
            var revisionNode = versionNode.SelectSingleNode("Revision");

            if (majorNode == null || minorNode == null || revisionNode == null)
            {
                throw new InvalidOperationException("One or more version components (Major, Minor, Revision) are missing in XML.");
            }

            string major = majorNode.InnerText;
            string minor = minorNode.InnerText;
            string revision = revisionNode.InnerText;

            return $"{major}.{minor}.{revision}";
        }

        /// <summary>
        /// Get the plugin version from the GH file.
        /// </summary>
        /// <returns>The plugin version in 'Major.Minor.Revision' format.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetGrasshopperVersion()
        {
            var versionNode = doc.SelectSingleNode("//item[@name='plugin_version']");
            if (versionNode == null)
            {
                throw new InvalidOperationException("Plugin version node is missing in XML.");
            }

            var majorNode = versionNode.SelectSingleNode("Major");
            var minorNode = versionNode.SelectSingleNode("Minor");
            var revisionNode = versionNode.SelectSingleNode("Revision");

            if (majorNode == null || minorNode == null || revisionNode == null)
            {
                throw new InvalidOperationException("One or more version components (Major, Minor, Revision) are missing in XML.");
            }

            string major = majorNode.InnerText;
            string minor = minorNode.InnerText;
            string revision = revisionNode.InnerText;

            return $"{major}.{minor}.{revision}";
        }
        
        #region DocumentHeader

        /// <summary>
        /// Get the document ID from the GH file.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetDocumentId()
        {
            var node = doc.SelectSingleNode("//chunk[@name='DocumentHeader']/items/item[@name='DocumentID']");
            if (node == null || string.IsNullOrEmpty(node.InnerText))
            {
                throw new InvalidOperationException("Document ID is missing or empty in XML.");
            }

            return node.InnerText;
        }
        
        //TODO Preview/PreviewMeshType/PreviewNormal/PreviewSelected

        #endregion

        #region DefinitionProperties
        /// <summary>
        /// Get the document date from the GH file.
        /// </summary>
        /// <returns>The document date as a string.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetDocumentDate()
        {
            var node = doc.SelectSingleNode("//chunk[@name='DefinitionProperties']/items/item[@name='Date']");
            if (node == null || string.IsNullOrEmpty(node.InnerText))
            {
                throw new InvalidOperationException("Document date is missing or empty in XML.");
            }

            return node.InnerText; // Convert this to a date format if necessary
        }
        
        /// <summary>
        /// Get the document description from the GH file.
        /// </summary>
        /// <returns>The document description.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetDocumentDescription()
        {
            var node = doc.SelectSingleNode("//chunk[@name='DefinitionProperties']/items/item[@name='Description']");
            if (node == null)
            {
                throw new InvalidOperationException("Document description node is missing in XML.");
            }

            return node.InnerText; // This could be empty but present
        }
        
        /// <summary>
        /// Get the document name from the GH file.
        /// </summary>
        /// <returns>The document name.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetDocumentName()
        {
            var node = doc.SelectSingleNode("//chunk[@name='DefinitionProperties']/items/item[@name='Name']");
            if (node == null || string.IsNullOrEmpty(node.InnerText))
            {
                throw new InvalidOperationException("Document name is missing or empty in XML.");
            }

            return node.InnerText;
        }
        
        //TODO KeepOpen? Revisions
        
        /// <summary>
        /// Get the projection details (Target X, Target Y, and Zoom) from the GH file.
        /// </summary>
        /// <returns>A tuple containing Target X, Target Y, and Zoom.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public (int targetX, int targetY, float zoom) GetProjectionDetails()
        {
            var targetNode = doc.SelectSingleNode("//chunk[@name='Projection']/items/item[@name='Target']");
            var zoomNode = doc.SelectSingleNode("//chunk[@name='Projection']/items/item[@name='Zoom']");

            if (targetNode == null)
            {
                throw new InvalidOperationException("Projection Target node is missing in XML.");
            }

            var xNode = targetNode.SelectSingleNode("X");
            var yNode = targetNode.SelectSingleNode("Y");

            if (xNode == null || yNode == null)
            {
                throw new InvalidOperationException("Projection Target X or Y node is missing in XML.");
            }

            if (!int.TryParse(xNode.InnerText, out int targetX))
            {
                throw new InvalidOperationException("Invalid format for Projection Target X in XML.");
            }

            if (!int.TryParse(yNode.InnerText, out int targetY))
            {
                throw new InvalidOperationException("Invalid format for Projection Target Y in XML.");
            }

            if (zoomNode == null || !float.TryParse(zoomNode.InnerText, out float zoom))
            {
                throw new InvalidOperationException("Zoom information is missing or in invalid format in XML.");
            }

            return (targetX, targetY, zoom);
        }
        
        //TODO Views/RcpLayout
       
        #endregion

        #region  Autor infos
        
        /// <summary>
        /// Get the author's company from the GH file.
        /// </summary>
        /// <returns>The author's company.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetAuthorEmail()
        {
            var node = doc.SelectSingleNode("//chunk[@name='Author']/items/item[@name='EMail']");

            if (node == null)
            {
                throw new InvalidOperationException("Author's company is missing");
            }

            return node.InnerText;
        }

        /// <summary>
        /// Get the author's copyright from the GH file.
        /// </summary>
        /// <returns>The author's copyright.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetAuthorCopyright()
        {
            var node = doc.SelectSingleNode("//chunk[@name='Author']/items/item[@name='Copyright']");
            if (node == null)
            {
                throw new InvalidOperationException("Author's copyright is missing");
            }

            return node.InnerText;
        }

        /// <summary>
        /// Get the author's name from the GH file.
        /// </summary>
        /// <returns>The author's name.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private string GetAuthorName()
        {
            var node = doc.SelectSingleNode("//chunk[@name='Author']/items/item[@name='Name']");
            if (node == null)
            {
                throw new InvalidOperationException("Author's name is missing.");
            }

            return node.InnerText;
        }
        #endregion

        public List<GhDependency> GetDependency()
        {
            if (doc == null)
            {
                throw new InvalidOperationException("XML document is not initialized.");
            }

            List<GhDependency> libraries = new List<GhDependency>();

            var libraryNodes = doc.SelectNodes("//chunk[@name='GHALibraries']/chunks/chunk[@name='Library']");
            if (libraryNodes == null)
            {
                return libraries; // Return empty list if no library nodes found
            }

            foreach (XmlNode libraryNode in libraryNodes)
            {
                var libraryInfo = new GhDependency();

                var assemblyFullNameNode = libraryNode.SelectSingleNode("items/item[@name='AssemblyFullName']");
                var assemblyVersionNode = libraryNode.SelectSingleNode("items/item[@name='AssemblyVersion']");
                var authorNode = libraryNode.SelectSingleNode("items/item[@name='Author']");
                var idNode = libraryNode.SelectSingleNode("items/item[@name='Id']");
                var nameNode = libraryNode.SelectSingleNode("items/item[@name='Name']");

                libraryInfo.AssemblyFullName = assemblyFullNameNode?.InnerText;
                libraryInfo.AssemblyVersion = assemblyVersionNode?.InnerText;
                libraryInfo.Author = authorNode?.InnerText;

                if (Guid.TryParse(idNode?.InnerText, out Guid parsedGuid))
                {
                    libraryInfo.Id = parsedGuid;
                }

                libraryInfo.Name = nameNode?.InnerText;

                libraries.Add(libraryInfo);
            }

            return libraries;
        }
        
        #endregion
        
        
    

    #region DefinitionObjects

    //Getting all the objects in the definition
    /// <summary>
    /// Get the object count from the GH file.
    /// </summary>
    /// <returns>The object count as an integer.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int GetComponentCount()
    {
        var node = doc.SelectSingleNode("//item[@name='ObjectCount']");
        if (node == null || string.IsNullOrEmpty(node.InnerText))
        {
            throw new InvalidOperationException("Object count is missing or empty in XML.");
        }

        if (!int.TryParse(node.InnerText, out int objectCount))
        {
            throw new InvalidOperationException("Invalid format for Object Count in XML.");
        }

        return objectCount;
    }
    


    #region Component Inofs

    public string GetObjectGuid(XmlNode objectNode)
    {
        return objectNode.SelectSingleNode("items/item[@name='GUID']").InnerText;
    }
    
    public string GetObjectName(XmlNode objectNode)
    {
        return objectNode.SelectSingleNode("items/item[@name='Name']").InnerText;
    }
    
    public string GetContainerDescription(XmlNode objectNode)
    {
        return objectNode.SelectSingleNode("chunks/chunk[@name='Container']/items/item[@name='Description']").InnerText;
    }
    
    public string GetInstanceGuid(XmlNode objectNode)
    {
        return objectNode.SelectSingleNode("chunks/chunk[@name='Container']/items/item[@name='InstanceGuid']").InnerText;
    }

    public string GetContainerName(XmlNode objectNode)
    {
        return objectNode.SelectSingleNode("chunks/chunk[@name='Container']/items/item[@name='Name']").InnerText;
    }
    
    public string GetNickName(XmlNode objectNode)
    {
        return objectNode.SelectSingleNode("chunks/chunk[@name='Container']/items/item[@name='NickName']").InnerText;
    }
    
    public bool GetOptional(XmlNode objectNode)
    {
        return bool.Parse(objectNode.SelectSingleNode("chunks/chunk[@name='Container']/items/item[@name='Optional']").InnerText);
    }
    
    public int GetSourceCount(XmlNode objectNode)
    {
        return int.Parse(objectNode.SelectSingleNode("chunks/chunk[@name='Container']/items/item[@name='SourceCount']").InnerText);
    }
    
    public (int X, int Y, int W, int H) GetBounds(XmlNode objectNode)
    {
        XmlNode boundsNode = objectNode.SelectSingleNode("chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Bounds']");
        int x = int.Parse(boundsNode.SelectSingleNode("X").InnerText);
        int y = int.Parse(boundsNode.SelectSingleNode("Y").InnerText);
        int w = int.Parse(boundsNode.SelectSingleNode("W").InnerText);
        int h = int.Parse(boundsNode.SelectSingleNode("H").InnerText);
        return (x, y, w, h);
    }
    
    public (float X, float Y) GetPivot(XmlNode objectNode)
    {
        XmlNode pivotNode = objectNode.SelectSingleNode("chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Pivot']");
        float x = float.Parse(pivotNode.SelectSingleNode("X").InnerText);
        float y = float.Parse(pivotNode.SelectSingleNode("Y").InnerText);
        return (x, y);
    }
    
    public bool GetSelected(XmlNode objectNode)
    {
        return bool.Parse(objectNode.SelectSingleNode("chunks/chunk[@name='Container']/chunks/chunk[@name='Attributes']/items/item[@name='Selected']").InnerText);
    }











    #endregion

    #endregion
    
    }

}