using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class TestExecutionModel
    {
        public IEnumerable<TestExecutionItemModel> Items { get; }

        public TestExecutionModel(IEnumerable<TestExecutionItemModel> items)
        {
            Guard.AgainstNull(items, "items");

            Items = items;
        }
    }
}