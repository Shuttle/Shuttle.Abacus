namespace Abacus.Messages
{
    public class NotificationMessage
    {
        public NotificationMessage(IResult result)
        {
            Guard.AgainstNull(result, "result");

            Result = (Result) result;
        }

        public Result Result { get; set; }
    }
}