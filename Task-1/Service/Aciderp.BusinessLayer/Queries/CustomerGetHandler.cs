using System.Threading.Tasks;
using Aciderp.CQRS.Query;
using Aciderp.Data;
using Aciderp.DTO.Query;

namespace Aciderp.BusinessLayer.Query
{
	public class CustomerGetHandler
		: IQueryHandler<DTO.Query.CustomerGet, DTO.Query.CustomerGetResult>
	{
		private readonly CustomerRepository _customerRepository;
		public CustomerGetHandler(CustomerRepository customerRepository)
			=> this._customerRepository = customerRepository;
		public async Task<CustomerGetResult> Execute(CustomerGet query)
			=> await _customerRepository.Get(query).ConfigureAwait(false);
	}
}
