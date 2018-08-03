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

            if (string.IsNullOrEmpty(model.Axis) ||
                !(model.Axis.Equals("Row", StringComparison.InvariantCultureIgnoreCase) ||
                  model.Axis.Equals("Column", StringComparison.InvariantCultureIgnoreCase)) ||
                model.Index < 1 ||
                string.IsNullOrEmpty(model.Comparison) ||
                string.IsNullOrEmpty(model.Value))
            {
                return BadRequest();
            }

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

        [HttpPost("{id}/elements")]
        public IActionResult Post(Guid id, [FromBody] MatrixElementModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            if (model.Row < 1 ||
                model.Column< 1 ||
                string.IsNullOrEmpty(model.Value))
            {
                return BadRequest();
            }

            _bus.Send(new RegisterMatrixElementCommand
            {
                Id = Guid.NewGuid(),
                MatrixId = id,
                Row = model.Row,
                Column = model.Column,
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
                    Data = _dataRowMapper.MapObject<MatrixModel>(_matrixQuery.Find(new MatrixSearchSpecification().WithId(id)))
                });
            }
        }

        [HttpGet("{id}/constraints")]
        public IActionResult Constraints(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<MatrixConstraintModel>(_matrixQuery.Constraints(id))
                });
            }
        }

        [HttpGet("{id}/elements")]
        public IActionResult elements(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<MatrixElementModel>(_matrixQuery.Elements(id))
                });
            }
        }
    }
}