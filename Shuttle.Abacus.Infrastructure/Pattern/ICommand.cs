namespace Shuttle.Abacus.Infrastructure
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<T>
    {
        T Execute();
    }
}
