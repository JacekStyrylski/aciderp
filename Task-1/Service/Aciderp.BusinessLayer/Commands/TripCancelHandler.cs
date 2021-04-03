using System.Threading.Tasks;
using Aciderp.CQRS.Command;
using Aciderp.Data;
using Aciderp.DTO.Command;

namespace Aciderp.BusinessLayer.Commands
{
	public class TripCancelHandler
		: ICommandHandler<DTO.Command.TripCancel>
	{
		private readonly TripRepository _tripRepository;

		public TripCancelHandler(TripRepository tripRepository)
			=> this._tripRepository = tripRepository;
		public async Task Execute(TripCancel command)
			=> await _tripRepository.CancelTrip(command).ConfigureAwait(false);
	}
}
