namespace GhXMLParser;

/// <summary>
/// Describes a Grasshopper group.
public class GhGroup
{
    List<Guid> Guids { get; set; }
    string Nickname { get; set; }
    Guid InstanceGuid { get; set; }
    int ItemCount { get; set; }
    public GhGroup()
    {
        throw new NotImplementedException();
    }
}