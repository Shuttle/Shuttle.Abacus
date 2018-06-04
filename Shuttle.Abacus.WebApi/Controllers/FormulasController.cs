using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.DataAccess;
using Shuttle.Access.Mvc;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [RequiresSession]
    public class FormulasController : Controller
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDataRowMapper _dataRowMapper;
        private readonly IFormulaQuery _formulaQuery;

        public FormulasController(IDatabaseContextFactory databaseContextFactory, IDataRowMapper dataRowMapper,
            IFormulaQuery formulaQuery)
        {
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(formulaQuery, nameof(formulaQuery));

            _databaseContextFactory = databaseContextFactory;
            _dataRowMapper = dataRowMapper;
            _formulaQuery = formulaQuery;
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] FormulaSearchModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<DataAccess.Query.Formula>(
                        _formulaQuery.Search(model.Specification()))
                });
            }
        }
    }
}