using System.Linq;
using Aciderp.Data.Model;
using Bogus;

namespace Aciderp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TripManagementContext context)
        {
            // Look for any students.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }
			var customerIds = 0;

            var testCustomers = new Faker<Customer>()
				.RuleFor(o => o.Name, f => f.Name.FirstName())
				.RuleFor(o => o.Surname, f => f.Name.LastName())
				.RuleFor(o => o.Phone, f => f.Phone.PhoneNumber("###-###-###"))
				.RuleFor(o => o.Address, f => new Address{
					City = f.Address.City(),
					BuildingNumber = f.Address.BuildingNumber(),
					FlatNumber = f.Address.BuildingNumber(),
					Postal = f.Address.ZipCode(),
					StreetName = f.Address.StreetName()
				})
				;

			var customers = testCustomers.Generate(10);
			context.Customers.AddRange(customers);

            context.SaveChanges();

			var tripIds = 0;

			var testTrips = new Faker<Trip>()
				.RuleFor(o => o.Destination, f => f.Address.City())
				.RuleFor(o => o.Source, f => f.Address.City())
				.RuleFor(o => o.StartTime, f => f.Date.Future())
				.RuleFor(o => o.EndTime, f => f.Date.Future())
				.RuleFor(o => o.Customers, f => customers.ToList())
				;

			var trips = testTrips.Generate(5);

			context.Trips.AddRange(trips);
			context.SaveChanges();
        }
    }
}
