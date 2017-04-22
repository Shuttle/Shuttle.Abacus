namespace Shuttle.Abacus.UI.Core.Validation
{
    public interface IViewValidatorFactory
    {
        IViewValidator Create(IViewValidationManager manager);
    }
}
