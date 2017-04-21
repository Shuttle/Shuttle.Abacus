namespace Abacus.UI
{
    public interface IReportCoordinator :
        ICoordinator,
        IMessageHandler<DecimalTableReportMessage>,
        IMessageHandler<MethodTestPrintMessage>
    {

    }
}
