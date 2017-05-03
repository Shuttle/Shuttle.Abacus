using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<OperationTypeDTO> OperationTypes(this IEnumerable<IOperationFactory> factories)
        {
            var result = new List<OperationTypeDTO>();

            foreach (var factory in factories)
            {
                result.Add(new OperationTypeDTO
                {
                    //TODO
                    //Name = factory.Name,
                    //Text = factory.Text
                });
            }

            result.Sort((x, y) => x.Text.CompareTo(y.Text));

            return result;
        }
    }
}