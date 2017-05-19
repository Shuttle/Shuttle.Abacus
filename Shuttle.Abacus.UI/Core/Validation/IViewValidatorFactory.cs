namespace Shuttle.Abacus.Shell.Core.Validation
{
    public interface IViewValidatorFactory
    {
        IViewValidator Create(IViewValidationManager manager);
    }
}
