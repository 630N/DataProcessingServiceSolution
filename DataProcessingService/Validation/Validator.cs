using DataProcessingService.Helpers.Mapper;
using DataProcessingService.Interface;
using System.Reflection;

namespace DataProcessingService.Validation;

public class Validator<T> : IValidator<T>
{
    public ValidationResult Validate(T instance)
    {
        var validationResult = new ValidationResult();
        ValidateObject(instance, validationResult, "");
        return validationResult;
    }

    private void ValidateObject(object instance, ValidationResult validationResult, string basePropertyName)
    {
        var properties = instance.GetType().GetProperties();

        foreach (var property in properties)
        {
            var mapToAttributes = property.GetCustomAttributes<MapToAttribute>();
            foreach (var mapToAttribute in mapToAttributes)
            {
                // Check if the target property name exists in the destination type
                bool propertyExists = typeof(T).GetProperty(mapToAttribute.TargetPropertyName) != null;
                if (!propertyExists)
                {
                    validationResult.IsValid = false;
                    validationResult.Errors.Add($"{basePropertyName}{property.Name} is supposed to map to {mapToAttribute.TargetPropertyName}, which does not exist.");
                }
            }

            // If the property is a complex type, validate it recursively
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                var nestedObject = property.GetValue(instance);
                if (nestedObject == null)
                    continue;
                ValidateObject(nestedObject, validationResult, $"{property.Name}.");

            }
        }
    }
}
