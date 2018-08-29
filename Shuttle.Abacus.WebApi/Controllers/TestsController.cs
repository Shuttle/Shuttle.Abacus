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
    [RequiresPermission(SystemPermissions.Manage.Tests)]
    [Route("api/[controller]")]
    public class TestsController : Controller
    {
        private readonly IServiceBus _bus;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDataRowMapper _dataRowMapper;
        private readonly ITestQuery _testQuery;

        public TestsController(IServiceBus bus, IDatabaseContextFactory databaseContextFactory,
            IDataRowMapper dataRowMapper, ITestQuery testQuery)
        {
            Guard.AgainstNull(bus, nameof(bus));
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(dataRowMapper, nameof(dataRowMapper));
            Guard.AgainstNull(testQuery, nameof(testQuery));

            _bus = bus;
            _databaseContextFactory = databaseContextFactory;
            _dataRowMapper = dataRowMapper;
            _testQuery = testQuery;
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] TestSearchModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<TestModel>(
                        _testQuery.Search(model.Specification()))
                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TestModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            _bus.Send(new RegisterTestCommand
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
                    Data = _dataRowMapper.MapObject<TestModel>(_testQuery.Get(id))
                });
            }
        }

        [HttpGet("{id}/argument")]
        public IActionResult Arguments(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _dataRowMapper.MapObjects<TestArgumentModel>(_testQuery.Arguments(id))
                });
            }
        }


        [HttpPost("{id}/arguments")]
        public IActionResult Post(Guid id, [FromBody] TestArgumentModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            _bus.Send(new RegisterTestArgumentCommand
            {
                TestId = id,
                ArgumentId = model.ArgumentId,
                Value = model.Value
            });

            return Ok();
        }

        [HttpDelete("{formulaId}/arguments/{argumentId}")]
        public IActionResult DeleteArgument(Guid formulaId, Guid argumentId)
        {
            _bus.Send(new RemoveTestArgumentCommand
            {
                TestId = formulaId,
                ArgumentId = argumentId
            });

            return Ok();
        }
    }
}