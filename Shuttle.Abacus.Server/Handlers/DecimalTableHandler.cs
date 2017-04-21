using System;
using Shuttle.Abacus.Domain;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class DecimalTableHandler :
        IMessageHandler<CreateDecimalTableCommand>,
        IMessageHandler<UpdateDecimalTableCommand>
    {
        private readonly IConstraintRepository constraintRepository;
        private readonly IDecimalTableFactory factory;
        private readonly IDecimalValueRepository decimalValueRepository;
        private readonly IDecimalTableRepository decimalTableRepository;

        public DecimalTableHandler(IDecimalTableFactory factory, IDecimalTableRepository decimalTableRepository, IDecimalValueRepository decimalValueRepository, IConstraintRepository constraintRepository)
        {
            this.factory = factory;
            this.decimalTableRepository = decimalTableRepository;
            this.decimalValueRepository = decimalValueRepository;
            this.constraintRepository = constraintRepository;
        }

        public void ProcessMessage(IHandlerContext<CreateDecimalTableCommand> context)
        {
            var message = context.Message;

            var table = factory.Create(Guid.NewGuid(), message);

            decimalTableRepository.Add(table);

            foreach (var value in table.DecimalValues)
            {
                decimalValueRepository.Add(table, value);

                constraintRepository.SaveForOwner(value);
            }
        }

        public void ProcessMessage(IHandlerContext<UpdateDecimalTableCommand> context)
        {
            var message = context.Message;

            var table = factory.Create(message.DecimalTableId, message);

            decimalValueRepository.RemoveAllForDecimalTable(message.DecimalTableId);

            decimalTableRepository.Save(table);

            foreach (var value in table.DecimalValues)
            {
                decimalValueRepository.Add(table, value);

                constraintRepository.SaveForOwner(value);
            }
        }
    }
}
