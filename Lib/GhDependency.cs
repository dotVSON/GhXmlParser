namespace GhXMLParser;

public class GhDependency
{
    public string? AssemblyFullName { get; set; }
    public string AssemblyVersion { get; set; }
    public string Author { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }
}