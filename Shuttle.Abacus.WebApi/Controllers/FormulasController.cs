using System;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Access.Mvc;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;

namespace Shuttle.Abacus.WebApi.Controllers
{
    [RequiresPermission(SystemPermissions.Manage.Formulas)]
    [Route("api/[controller]")]
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
                    Data = _dataRowMapper.MapObjects<FormulaModel>(
                        _formulaQuery.Search(model.Specification()))
                });
            }
        }

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

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObject<FormulaModel>(_formulaQuery.Get(id))
                });
            }
        }

        [HttpGet("{id}/operations")]
        public IActionResult Operations(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<FormulaOperationModel>(_formulaQuery.Operations(id))
                });
            }
        }


        [HttpPost("{id}/operations")]
        public IActionResult Post(Guid id, [FromBody] FormulaOperationModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            _bus.Send(new RegisterFormulaOperationCommand
            {
                Id = Guid.NewGuid(),
                FormulaId = id,
                Operation = model.Operation,
                ValueProviderName = model.ValueProviderName,
                InputParameter = model.InputParameter
            });

            return Ok();
        }

        [HttpDelete("{formulaId}/operations/{operationId}")]
        public IActionResult DeleteOperation(Guid formulaId, Guid operationId)
        {
            _bus.Send(new RemoveFormulaOperationCommand
            {
                FormulaId = formulaId,
                OperationId = operationId
            });

            return Ok();
        }

        [HttpGet("{id}/constraints")]
        public IActionResult Constraints(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<FormulaConstraintModel>(_formulaQuery.Constraints(id))
                });
            }
        }

        [HttpPost("{id}/constraints")]
        public IActionResult Post(Guid id, [FromBody] FormulaConstraintModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            _bus.Send(new RegisterFormulaConstraintCommand
            {
                Id = Guid.NewGuid(),
                FormulaId = id,
                ArgumentId = model.ArgumentId,
                Comparison = model.Comparison,
                Value = model.Value
            });

            return Ok();
        }

        [HttpDelete("{formulaId}/constraints/{constraintId}")]
        public IActionResult DeleteConstraint(Guid formulaId, Guid constraintId)
        {
            _bus.Send(new RemoveFormulaConstraintCommand
            {
                FormulaId = formulaId,
                ConstraintId = constraintId
            });

            return Ok();
        }
    }
}