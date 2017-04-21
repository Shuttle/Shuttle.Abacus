namespace Shuttle.Abacus.Infrastructure
{
    public class Pipeline : IPipeline
    {
        public void Process<T>(T item)
        {
            DependencyResolver.Resolver.ResolveAssignable<IPipe<T>>().ForEach(pipe => pipe.Handle(item));
        }
    }
}
