using System;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Access;
using Shuttle.Access.Mvc;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;

namespace Shuttle.Abacus.WebApi.Controllers
{
    [RequiresPermission(SystemPermissions.Manage.Matrices)]
    [Route("api/[controller]")]
    [RequiresSession]
    public class MatricesController : Controller
    {
        private readonly IServiceBus _bus;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDataRowMapper _dataRowMapper;
        private readonly IMatrixQuery _matrixQuery;

        public MatricesController(IServiceBus bus, IDatabaseContextFactory databaseContextFactory,
            IDataRowMapper dataRowMapper, IMatrixQuery matrixQuery)
        {
            Guard.AgainstNull(bus, nameof(bus));
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(dataRowMapper, nameof(dataRowMapper));
            Guard.AgainstNull(matrixQuery, nameof(matrixQuery));

            _bus = bus;
            _databaseContextFactory = databaseContextFactory;
            _dataRowMapper = dataRowMapper;
            _matrixQuery = matrixQuery;
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] MatrixSearchModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<MatrixModel>(
                        _matrixQuery.Search(model.Specification()))
                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] MatrixModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            _bus.Send(new RegisterMatrixCommand
            {
                Name = model.Name,
                RowArgumentId = model.RowArgumentId,
                ColumnArgumentId = model.ColumnArgumentId,
                DataTypeName = model.DataTypeName
            });

            return Ok();
        }

        [HttpPost("{id}/constraints")]
        public IActionResult Post(Guid id, [FromBody] MatrixConstraintModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            _bus.Send(new RegisterMatrixConstraintCommand
            {
                Id = Guid.NewGuid(),
                MatrixId = id,
                Axis = model.Axis,
                Index = model.Index,
                Comparison = model.Comparison,
                Value = model.Value
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
                    Data = _dataRowMapper.MapObject<MatrixModel>(_matrixQuery.Get(id))
                });
            }
        }

        [HttpGet("{id}/constraints")]
        public IActionResult Constraint(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<MatrixConstraintModel>(_matrixQuery.Constraints(id))
                });
            }
        }
    }
}