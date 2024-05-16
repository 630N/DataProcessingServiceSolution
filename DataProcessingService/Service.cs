using DataProcessingService.Exception;
using DataProcessingService.Interface;
using DataProcessingService.Validation;

namespace DataProcessingService;

public class Service<TInput, TOutput>
{
    private readonly IValidator<TInput> _validator;
    private readonly ITransformer<TInput, TOutput> _transformer;

    public Service(IValidator<TInput> validator, ITransformer<TInput, TOutput> transformer)
    {
        _validator = validator;
        _transformer = transformer;
    }

    public TOutput Transform(TInput inputData)
    {
        try
        {
            ValidationResult validationResult = _validator.Validate(inputData);
            if (!validationResult.IsValid)
                throw new TransformationException($"Validation failed: {string.Join(", ", validationResult.Errors)}");

            return _transformer.Transform(inputData);
        }
        catch
        {
            return default(TOutput);
        }
    }
}
