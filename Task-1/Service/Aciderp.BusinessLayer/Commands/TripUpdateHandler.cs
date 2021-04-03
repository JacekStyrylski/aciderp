using System.Threading.Tasks;
using Aciderp.CQRS.Command;
using Aciderp.Data;

namespace Aciderp.BusinessLayer.Commands
{
	public class TripUpdateHandler
		: ICommandHandler<DTO.Command.TripUpdate>
	{
		private readonly TripRepository _tripRepository;

		public TripUpdateHandler(TripRepository tripRepository)
			=> this._tripRepository = tripRepository;
		public async Task Execute(DTO.Command.TripUpdate command)
			=> await _tripRepository.Update(command).ConfigureAwait(false);
	}
}
