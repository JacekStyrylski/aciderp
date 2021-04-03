using System.Threading.Tasks;
using Aciderp.CQRS.Command;
using Aciderp.Data;

namespace Aciderp.BusinessLayer.Commands
{
	public class TripCreateHandler
		: ICommandHandler<DTO.Command.TripCreate>
	{
		private readonly TripRepository _tripRepository;

		public TripCreateHandler(TripRepository tripRepository)
			=> this._tripRepository = tripRepository;
		public async Task Execute(DTO.Command.TripCreate command)
			=> await _tripRepository.Create(command).ConfigureAwait(false);
	}
}
