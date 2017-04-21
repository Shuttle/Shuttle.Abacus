namespace Shuttle.Abacus.Domain
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput MapFrom(TInput input);
    }
}
