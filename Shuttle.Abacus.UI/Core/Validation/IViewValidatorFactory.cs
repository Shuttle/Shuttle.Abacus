namespace Abacus.UI
{
    public interface IViewValidatorFactory
    {
        IViewValidator Create(IViewValidationManager manager);
    }
}
