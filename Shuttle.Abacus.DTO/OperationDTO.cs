namespace Shuttle.Abacus.DTO
{
    public class OperationDTO
    {
        public OperationTypeDTO OperationType { get; set; }
        public ValueSourceTypeDTO ValueSourceType { get; set; }
        public string ValueSelection { get; set; }
        public string Text { get; set; }
    }
}
