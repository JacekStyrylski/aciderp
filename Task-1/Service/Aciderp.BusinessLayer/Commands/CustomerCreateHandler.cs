using System.Threading.Tasks;
using Aciderp.CQRS.Command;
using Aciderp.Data;

namespace Aciderp.BusinessLayer.Commands
{
	public class CustomerCreateHandler
		: ICommandHandler<DTO.Command.CustomerCreate>
	{
		private readonly CustomerRepository _customerRepository;
		public CustomerCreateHandler(CustomerRepository customerRepository)
		{
			this._customerRepository = customerRepository;

		}
		public async Task Execute(DTO.Command.CustomerCreate command)
			=> await _customerRepository.Create(command).ConfigureAwait(false);
	}
}
