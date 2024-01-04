namespace GhXMLParser;

/// <summary>
/// Describes plugin dependencies of a Grasshopper file.
/// </summary>
public class GhComponentDependency
{
    public string? AssemblyFullName { get; set; }
    public string? AssemblyVersion { get; set; }
    public string? Author { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Version { get; set; }
}