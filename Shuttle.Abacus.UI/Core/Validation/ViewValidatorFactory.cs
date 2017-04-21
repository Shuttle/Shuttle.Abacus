namespace Abacus.UI
{
    public class ViewValidatorFactory : IViewValidatorFactory
    {
        private readonly IControlValidatorProvider provider;

        public ViewValidatorFactory(IControlValidatorProvider provider)
        {
            this.provider = provider;
        }

        public IViewValidator Create(IViewValidationManager manager)
        {
            return new ViewValidator(provider, manager);
        }
    }
}
