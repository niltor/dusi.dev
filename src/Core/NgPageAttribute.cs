namespace Core;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class NgPageAttribute : Attribute
{
    public string? Module { get; init; }
    public string? Route { get; init; }

    public NgPageAttribute(string? module, string? route)
    {
        Module = module;
        Route = route;
    }
}
