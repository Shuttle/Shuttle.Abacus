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
    [RequiresPermission(SystemPermissions.Manage.Tests)]
    [Route("api/[controller]")]
    public class TestsController : Controller
    {
        private readonly IServiceBus _bus;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IDataRowMapper _dataRowMapper;
        private readonly ITestRepository _testRepository;
        private readonly ITestQuery _testQuery;
        private readonly IExecutionService _executionService;

        public TestsController(IServiceBus bus, IDatabaseContextFactory databaseContextFactory,
            IDataRowMapper dataRowMapper, ITestRepository testRepository, ITestQuery testQuery,
            IExecutionService executionService)
        {
            Guard.AgainstNull(bus, nameof(bus));
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(dataRowMapper, nameof(dataRowMapper));
            Guard.AgainstNull(testRepository, nameof(testRepository));
            Guard.AgainstNull(testQuery, nameof(testQuery));
            Guard.AgainstNull(executionService, nameof(executionService));

            _bus = bus;
            _databaseContextFactory = databaseContextFactory;
            _dataRowMapper = dataRowMapper;
            _testRepository = testRepository;
            _testQuery = testQuery;
            _executionService = executionService;
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

            if (model.FormulaId.Equals(Guid.Empty) ||
                string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Comparison) ||
                string.IsNullOrWhiteSpace(model.ExpectedResult) ||
                string.IsNullOrWhiteSpace(model.ExpectedResultDataTypeName))
            {
                return BadRequest();
            }

            _bus.Send(new RegisterTestCommand
            {
                Id = model.Id,
                Name = model.Name,
                FormulaId = model.FormulaId,
                Comparison = model.Comparison,
                ExpectedResult = model.ExpectedResult,
                ExpectedResultDataTypeName = model.ExpectedResultDataTypeName
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

        [HttpGet("{id}/arguments")]
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

        [HttpDelete("{testId}/arguments/{argumentId}")]
        public IActionResult DeleteArgument(Guid testId, Guid argumentId)
        {
            _bus.Send(new RemoveTestArgumentCommand
            {
                TestId = testId,
                ArgumentId = argumentId
            });

            return Ok();
        }

        [HttpGet("{id}/run")]
        public IActionResult Run(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                var test = _testRepository.Get(id);

                var executionContext = _executionService.Execute(test.FormulaId, test.ArgumentValues(), new ContextLogger(ContextLogLevel.Verbose));

                return Ok(new
                {
                    Result = executionContext.Result()
                });
            }
        }
    }
}