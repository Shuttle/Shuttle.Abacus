namespace Shuttle.Abacus.Infrastructure
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput MapFrom(TInput input);
    }
}
