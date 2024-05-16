using DataProcessingService.Validation;

namespace DataProcessingService.Interface;

public interface IValidator<T>
{
    ValidationResult Validate(T instance);
}
