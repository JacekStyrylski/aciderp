using System;
using System.Linq;
using System.Threading.Tasks;
using Aciderp.DTO.Command;
using Aciderp.DTO.Query;
using Microsoft.EntityFrameworkCore;

namespace Aciderp.Data
{
	public class CustomerRepository : Repository
	{
		public CustomerRepository(TripManagementContext tripManagementContext)
			: base(tripManagementContext) { }
		public async Task<int> Create(DTO.Command.CustomerCreate cc)
		{
			Model.Customer customer = new ();
			customer.Name = cc.Name;
			customer.Phone = cc.Phone;
			customer.Surname = cc.Surname;

			Model.Address address = new();
			address.BuildingNumber = cc.BuildingNumber;
			address.City = cc.City;
			address.FlatNumber = cc.FlatNumber;
			address.Postal = cc.Postal;
			address.StreetName = cc.StreetName;

			customer.Address = address;

			_tripManagementContext.Customers.Add(customer);

			await _tripManagementContext.SaveChangesAsync().ConfigureAwait(false);

			return customer.Id;
		}

		public async Task Update(DTO.Command.CustomerUpdate uc)
		{
			var customer = await _tripManagementContext.Customers.Include("Address").
				FirstOrDefaultAsync(c => c.Id == uc.Id).ConfigureAwait(false);

			if(customer == null)
				throw new Exception("Customer does not exist.");

			customer.Name = uc.Name;
			customer.Phone = uc.Phone;
			customer.Surname = uc.Surname;

			customer.Address.BuildingNumber = uc.BuildingNumber;
			customer.Address.City = uc.City;
			customer.Address.FlatNumber = uc.FlatNumber;
			customer.Address.Postal = uc.Postal;
			customer.Address.StreetName = uc.StreetName;

			await _tripManagementContext.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task<DTO.Query.CustomerGetResult> Get(DTO.Query.CustomerGet query)
		{
			var customer = await _tripManagementContext.Customers.Include("Address").FirstOrDefaultAsync(c
				=> c.Id == query.Id).ConfigureAwait(false);

			if(customer == null)
				throw new Exception("Customer does not exist.");

			DTO.Query.CustomerGetResult cgr = new();

			cgr.BuildingNumber = customer.Address.BuildingNumber;
			cgr.City = customer.Address.City;
			cgr.FlatNumber = customer.Address.FlatNumber;
			cgr.Id = customer.Id;
			cgr.Name = customer.Name;
			cgr.Phone = customer.Phone;
			cgr.Postal = customer.Address.Postal;
			cgr.StreetName = customer.Address.StreetName;
			cgr.Surname = customer.Surname;

			return cgr;
		}
	}
}
