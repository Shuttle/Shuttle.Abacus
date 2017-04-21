using System;
using System.Collections.Generic;
using System.Data;
using Abacus.Domain;

namespace Abacus.Data
{
    public class MethodTestDataTableMapper : IDataTableMapper<MethodTest>
    {
        public IEnumerable<MethodTest> MapFrom(DataTable input)
        {
            var result = new List<MethodTest>();

            var test = new MethodTest(Guid.Empty);

            foreach (DataRow row in input.Rows)
            {
                if (!test.Id.Equals(MethodTestColumns.Id.MapFrom(row)))
                {
                    if (!test.Id.Equals(Guid.Empty))
                    {
                        result.Add(test);
                    }

                    test = new MethodTest(MethodTestColumns.Id.MapFrom(row))
                           {
                               Description = MethodTestColumns.Description.MapFrom(row),
                               ExpectedResult = MethodTestColumns.ExpectedResult.MapFrom(row),
                               MethodId = MethodTestColumns.MethodId.MapFrom(row)
                           };
                }

                var argumentName = MethodTestColumns.ArgumentAnswerColumns.ArgumentName.MapFrom(row);
                var answerType = MethodTestColumns.ArgumentAnswerColumns.AnswerType.MapFrom(row);
                var answer = MethodTestColumns.ArgumentAnswerColumns.Answer.MapFrom(row);

                if (string.IsNullOrEmpty(argumentName)
                    ||
                    string.IsNullOrEmpty(answer)
                    ||
                    string.IsNullOrEmpty(answerType))
                {
                    continue;
                }

                test.AddArgumentAnswer(new MethodTestArgumentAnswer(
                                         MethodTestColumns.ArgumentAnswerColumns.ArgumentId.MapFrom(row), argumentName,
                                         answerType, answer));
            }

            if (!test.Id.Equals(Guid.Empty))
            {
                result.Add(test);
            }

            return result;
        }
    }
}
