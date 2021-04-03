using System.Threading.Tasks;
using Aciderp.CQRS.Command;
using Aciderp.Data;
using Aciderp.DTO.Command;

namespace Aciderp.BusinessLayer.Commands
{
	public class CustomerUpdateHandler
		: ICommandHandler<DTO.Command.CustomerUpdate>
	{
		private readonly CustomerRepository _customerRepository;
		public CustomerUpdateHandler(CustomerRepository customerRepository)
			=> this._customerRepository = customerRepository;
		public async Task Execute(CustomerUpdate command)
			=> await _customerRepository.Update(command).ConfigureAwait(false);
	}
}
