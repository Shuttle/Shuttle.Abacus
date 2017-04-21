using System.Collections.Generic;
using System.Text;

namespace Shuttle.Abacus.DTO
{
    public class MethodContextDTO
    {
        public MethodContextDTO()
        {
            GraphNodes = new List<GraphNodeDTO>();
        }

        public decimal Total { get; set; }

        public List<string> ErrorMessages { get; set; }
        public List<string> WarningMessages { get; set; }
        public List<string> InformationMessages { get; set; }

        public List<GraphNodeDTO> GraphNodes { get; set; }

        public byte[] LogEncoded { get; set; }

        public void SetLog(string value)
        {
            LogEncoded = Encoding.UTF8.GetBytes(value);
        }

        public string GetLog()
        {
            return Encoding.UTF8.GetString(LogEncoded);
        }
    }
}
