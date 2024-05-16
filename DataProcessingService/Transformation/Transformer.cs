using DataProcessingService.Helpers.Mapper;
using DataProcessingService.Interface;
using System.Reflection;

namespace DataProcessingService.Transformation;

public class Transformer<TInput, TOutput> : ITransformer<TInput, TOutput>
where TOutput : new()
{
    public TOutput Transform(TInput inputType)
    {
        var output = new TOutput();
        var inputProperties = typeof(TInput).GetProperties();

        foreach (var inputProperty in inputProperties)
        {
            var mapToAttribute = inputProperty.GetCustomAttribute<MapToAttribute>();
            var outputProperty = typeof(TOutput).GetProperty(mapToAttribute != null ? mapToAttribute.TargetPropertyName : inputProperty.Name);
            if (outputProperty == null || !outputProperty.CanWrite)
                continue;
            if (inputProperty.PropertyType.IsClass && inputProperty.PropertyType != typeof(string))
            {
                outputProperty.SetValue(output, TransformNestedObject(inputType, inputProperty, outputProperty));
                continue;
            }
            outputProperty.SetValue(output, inputProperty.GetValue(inputType));

        }
        return output;
    }

    private object? TransformNestedObject(TInput inputType, PropertyInfo? inputProperty, PropertyInfo? outputProperty)
    {
        var nestedOutput = Activator.CreateInstance(outputProperty.PropertyType);
        var nestedTransformer = Activator.CreateInstance(
            typeof(Transformer<,>).MakeGenericType(inputProperty.PropertyType, outputProperty.PropertyType));
        nestedOutput = nestedTransformer.GetType().GetMethod("Transform").Invoke(nestedTransformer, new[] { inputProperty.GetValue(inputType) });
        return nestedOutput;
    }
}
