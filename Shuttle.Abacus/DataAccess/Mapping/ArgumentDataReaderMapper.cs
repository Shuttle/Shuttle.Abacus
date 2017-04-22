using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentDataReaderMapper : IDataReaderMapper<Argument>
    {
        public IEnumerable<Argument> MapFrom(IDataReader input)
        {
            var root = new Argument
                       {
                           Name = string.Empty
                       };

            var result = new List<Argument>();

            while (input.Read())
            {
                var name = ArgumentColumns.Name.MapFrom(input);
                var type = ArgumentColumns.AnswerType.MapFrom(input);

                if (!name.Equals(root.Name))
                {
                    if (!string.IsNullOrEmpty(root.Name))
                    {
                        result.Add(root);
                    }

                    var id = ArgumentColumns.Id.MapFrom(input);

                    root = new Argument(id)
                           {
                               Name = name,
                               AnswerType = type,
                               IsSystemData = ArgumentColumns.IsSystemData.MapFrom(input),
                           };
                }

                var answer = ArgumentColumns.RestrictedAnswerColumns.Answer.MapFrom(input);

                if(string.IsNullOrEmpty(answer))
                {
                    continue;
                }

                root.AddArgumentAnswer(answer);
            }

            if (!string.IsNullOrEmpty(root.Name))
            {
                result.Add(root);
            }

            return result;
        }
    }
}
