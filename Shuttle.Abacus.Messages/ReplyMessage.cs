namespace Abacus.Messages
{
    public class ReplyMessage
    {
        public ReplyMessage(IResult result)
        {
            Guard.AgainstNull(result, "result");

            Result = (Result) result;
        }

        public Result Result { get; set; }
    }
}