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
    public class TripController : Controller
    {
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;
		public TripController(ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}
		[HttpPost]
        public async Task Create(TripCreate tc)
		{
			await _commandDispatcher.Execute(tc).ConfigureAwait(false);
		}

		[HttpPost]
		public async Task Update(TripUpdate tu)
		{
			await _commandDispatcher.Execute(tu).ConfigureAwait(false);
		}

		[HttpPost]
		public async Task Cancel(TripCancel tc)
		{
			await _commandDispatcher.Execute(tc).ConfigureAwait(false);
		}

		[HttpPost]
		public async Task AssignCustomerToTrip(AssignCustomerToTrip actt)
		{
			await _commandDispatcher.Execute(actt).ConfigureAwait(false);
		}

		[HttpGet]
		public async Task<TripGetResult> Get(int id)
		{
			return await _queryDispatcher.Execute<TripGet, TripGetResult>(new TripGet
			{
				Id = id
			}).ConfigureAwait(false);
		}
    }
}
