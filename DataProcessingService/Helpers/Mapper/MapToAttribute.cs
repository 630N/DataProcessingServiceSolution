namespace DataProcessingService.Helpers.Mapper;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MapToAttribute : Attribute
{
    public string TargetPropertyName { get; }

    public MapToAttribute(string targetPropertyName)
    {
        TargetPropertyName = targetPropertyName;
    }
}
