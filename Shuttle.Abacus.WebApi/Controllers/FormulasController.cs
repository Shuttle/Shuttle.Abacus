using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Access.Mvc;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;

namespace Shuttle.Abacus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [RequiresSession]
    public class FormulasController : Controller
    {
        private readonly IServiceBus _bus;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDataRowMapper _dataRowMapper;
        private readonly IFormulaQuery _formulaQuery;

        public FormulasController(IServiceBus bus, IDatabaseContextFactory databaseContextFactory,
            IDataRowMapper dataRowMapper, IFormulaQuery formulaQuery)
        {
            Guard.AgainstNull(bus, nameof(bus));
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(dataRowMapper, nameof(dataRowMapper));
            Guard.AgainstNull(formulaQuery, nameof(formulaQuery));

            _bus = bus;
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

        [RequiresPermission(SystemPermissions.Manage.Formulas)]
        [HttpPost]
        public IActionResult Post([FromBody] FormulaModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            _bus.Send(new RegisterFormulaCommand
            {
                Name = model.Name
            });

            return Ok();
        }

    }
}