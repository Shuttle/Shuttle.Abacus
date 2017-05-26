using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    public class TestExecutionPresenter : Presenter<ITestExecutionView, TestExecutionModel>, ITestExecutionPresenter
    {
        private readonly IServiceBus _serviceBus;

        public TestExecutionPresenter(ITestExecutionView executionView, IServiceBus serviceBus) : base(executionView)
        {
            Guard.AgainstNull(executionView, "view");
            Guard.AgainstNull(serviceBus, "serviceBus");

            _serviceBus = serviceBus;

            Text = "Run Test Details";

            Image = Resources.Image_Test;
        }

        public void ProcessModel(TestExecutionModel model)
        {
            AssignModel(model);

            foreach (var item in model.Items)
            {
                View.AddTest(item);
            }
        }

        public void RequestExecution(TestExecutionItemModel item)
        {
            _serviceBus.Send(new ExecuteTestCommand
            {
                Id = item.Id
            });
        }
    }
}