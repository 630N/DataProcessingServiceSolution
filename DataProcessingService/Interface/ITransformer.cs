namespace DataProcessingService.Interface;

public interface ITransformer<TInput, TOutput>
{
    TOutput Transform(TInput input);
}
