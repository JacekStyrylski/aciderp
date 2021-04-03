using System;
using System.Threading.Tasks;
using Aciderp.CQRS.Command;
using Aciderp.CQRS.Query;
using Aciderp.DTO.Command;
using Aciderp.DTO.Query;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Aciderp.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	[EnableCors]
    public class CustomerController : Controller
    {
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;

		public CustomerController(ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		[HttpPost]
		public async Task Create(CustomerCreate cc)
		{
			await _commandDispatcher.Execute(cc).ConfigureAwait(false);
		}

		[HttpPost]
		public async Task Update(CustomerUpdate cu)
		{
			await _commandDispatcher.Execute(cu).ConfigureAwait(false);
		}

		[HttpGet]
		public async Task<CustomerGetResult> Get(int id)
		{
			return await _queryDispatcher.Execute<CustomerGet, CustomerGetResult>(new CustomerGet {
				Id = id
			}).ConfigureAwait(false);
		}
    }
}
