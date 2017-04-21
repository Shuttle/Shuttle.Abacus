using System;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Messages;
using NServiceBus;

namespace Abacus.Server
{
    public class DecimalTableHandler :
        MessageHandler,
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

        public void Handle(CreateDecimalTableCommand message)
        {
            Transacted(uow =>
                {
                    var table = factory.Create(Guid.NewGuid(), message);

                    decimalTableRepository.Add(table);

                    table.DecimalValues.ForEach(value =>
                        {
                            decimalValueRepository.Add(table, value);

                            constraintRepository.SaveForOwner(value);
                        });
                });
        }

        public void Handle(UpdateDecimalTableCommand message)
        {
            Transacted(uow =>
                {
                    var table = factory.Create(message.DecimalTableId, message);

                    decimalValueRepository.RemoveAllForDecimalTable(message.DecimalTableId);

                    decimalTableRepository.Save(table);

                    table.DecimalValues.ForEach(value =>
                        {
                            decimalValueRepository.Add(table, value);

                            constraintRepository.SaveForOwner(value);
                        });
                });
        }
    }
}
