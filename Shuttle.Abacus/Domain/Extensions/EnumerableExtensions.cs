using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<ConstraintTypeDTO> ConstraintTypes(this IEnumerable<IConstraintFactory> factories)
        {
            var result = new List<ConstraintTypeDTO>();

            foreach (var factory in factories)
            {
                result.Add(new ConstraintTypeDTO
                {
                    Name = factory.Name,
                    Text = factory.Text
                });
            }

            result.Sort((x, y) => string.Compare(x.Text, y.Text, StringComparison.Ordinal));

            return result;
        }

        public static IEnumerable<OperationTypeDTO> OperationTypes(this IEnumerable<IOperationFactory> factories)
        {
            var result = new List<OperationTypeDTO>();

            foreach (var factory in factories)
            {
                result.Add(new OperationTypeDTO
                {
                    Name = factory.Name,
                    Text = factory.Text
                });
            }

            result.Sort((x, y) => x.Text.CompareTo(y.Text));

            return result;
        }
    }
}