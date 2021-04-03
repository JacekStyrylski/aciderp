using System.Threading.Tasks;
using Aciderp.CQRS.Query;
using Aciderp.Data;
using Aciderp.DTO.Query;

namespace Aciderp.BusinessLayer.Query
{
	public class TripGetHandler
		: IQueryHandler<DTO.Query.TripGet, DTO.Query.TripGetResult>
	{
		private readonly TripRepository _tripRepository;
		public TripGetHandler(TripRepository tripRepository)
			=> this._tripRepository = tripRepository;
		public async Task<TripGetResult> Execute(TripGet query)
			=> await _tripRepository.Get(query).ConfigureAwait(false);
	}
}
