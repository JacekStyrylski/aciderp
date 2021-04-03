using System.Threading.Tasks;
using Aciderp.CQRS.Command;
using Aciderp.Data;
using Aciderp.DTO.Command;

namespace Aciderp.BusinessLayer.Commands
{
	public class AssignCustomerToTripHandler
		: ICommandHandler<DTO.Command.AssignCustomerToTrip>
	{
		private readonly CustomerRepository _customerRepository;
		private readonly TripRepository _tripRepository;

		public AssignCustomerToTripHandler(
			Data.CustomerRepository customerRepository,
			Data.TripRepository tripRepository)
		{
			this._customerRepository = customerRepository;
			this._tripRepository = tripRepository;
		}
		public async Task Execute(AssignCustomerToTrip command)
			=> await _tripRepository.AssignCustomer(command).ConfigureAwait(false);
	}
}
