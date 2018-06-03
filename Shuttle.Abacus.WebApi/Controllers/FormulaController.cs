using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.WebApi.Controllers
{
    public class FormulasController : Controller
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IFormulaQuery _formulaQuery;

        public FormulasController(IDatabaseContextFactory databaseContextFactory, IDatabaseGateway databaseGateway, IFormulaQuery formulaQuery)
        {
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(databaseGateway, nameof(databaseGateway));
            Guard.AgainstNull(formulaQuery, nameof(formulaQuery));

            _databaseContextFactory = databaseContextFactory;
            _databaseGateway = databaseGateway;
            _formulaQuery = formulaQuery;
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] FormulaSearchModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            using (_databaseContextFactory.Create())
            {
                foreach (var row in _formulaQuery.Search(model.Specification()))
                {
                    result.Add(new
                    {
                        Id = AccountColumns.Id.MapFrom(row),
                        Name = NameColumns.Name.MapFrom(row),
                        Number = QueryColumns.AccountNumber.MapFrom(row),
                        Status = QueryColumns.StatusDesc.MapFrom(row),
                        Description = QueryColumns.AccountDesc.MapFrom(row)
                    });
                }

                return Ok(new
                {
                    Data = result
                });
            }
        }
    }
}