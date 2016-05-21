namespace Shuttle.Abacus
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput MapFrom(TInput input);
    }
}
