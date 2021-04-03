using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aciderp.DTO.Command;
using Microsoft.EntityFrameworkCore;

namespace Aciderp.Data
{
	public class TripRepository : Repository
	{
		public TripRepository(TripManagementContext tripManagementContext)
			: base(tripManagementContext) { }
		public async Task<int> Create(TripCreate tc)
		{
			Model.Trip trip = new();

			trip.Source = tc.Source;
			trip.Destination = tc.Destination;
			trip.StartTime = tc.StartTime;
			trip.EndTime = tc.EndTime;

			await _tripManagementContext.Trips.AddAsync(trip).ConfigureAwait(false);

			return trip.Id;
		}

		public async Task Update(TripUpdate tu)
		{
			var trip = await _tripManagementContext.Trips.FindAsync(tu.Id).ConfigureAwait(false);

			if(trip == null)
				throw new Exception("Trip does not exits.");

			trip.Source = tu.Source;
			trip.Destination = tu.Destination;
			trip.StartTime = tu.StartTime;
			trip.EndTime = tu.EndTime;

			await _tripManagementContext.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task CancelTrip(DTO.Command.TripCancel tripCancel)
		{
			var trip = await _tripManagementContext.Trips.FindAsync(tripCancel.Id).ConfigureAwait(false);

			if(trip == null)
				throw new Exception("Trip does not exits.");

			var sevenDaysTimeSpan = new TimeSpan(7,0,0);

			if(trip.StartTime.Subtract(sevenDaysTimeSpan).CompareTo(DateTime.Now) <= 0)
				throw new Exception("A customer cannot cancel a trip within 7 business days before its start.");

			trip.IsCanceled = true;

			await _tripManagementContext.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task<DTO.Query.TripGetResult> Get(DTO.Query.TripGet query)
		{
			var trip = await _tripManagementContext.Trips.FindAsync(query.Id).ConfigureAwait(false);

			if(trip == null)
				throw new Exception("Trip does not exits.");

			DTO.Query.TripGetResult tr = new();

			tr.Destination = trip.Destination;
			tr.EndTime = trip.EndTime;
			tr.Id = trip.Id;
			tr.Source = trip.Source;
			tr.StartTime = trip.StartTime;
			tr.IsCanceled = trip.IsCanceled;

			return tr;
		}

		public async Task AssignCustomer(AssignCustomerToTrip actt)
		{
			await ValidateIfCustomerCanBeAssignedToTrip(actt);

			var customer = await _tripManagementContext.Customers.FindAsync(actt.CustomerId).ConfigureAwait(false);
			var trip = await _tripManagementContext.Trips.Include("Customers").
				FirstOrDefaultAsync(t => t.Id == actt.TripId).ConfigureAwait(false);

			trip.Customers.Add(customer);
			await _tripManagementContext.SaveChangesAsync();
		}

		private async Task ValidateIfCustomerCanBeAssignedToTrip(AssignCustomerToTrip actt)
		{
			var trip = await _tripManagementContext.Trips.FindAsync(actt.TripId).ConfigureAwait(false);
			var customerTrips = await GetCustomerRegisteredTrips(actt.CustomerId);

			if(trip == null)
				throw new Exception("Trip does not exits.");

			if(customerTrips.Any(t => DatesOverlap(t.StartTime, t.EndTime, trip.StartTime, trip.EndTime)))
				throw new Exception("Customer cannot have two overlapping trips.");
		}

		//TODO: Write unit test
		private bool DatesOverlap(
			DateTime date1Start,
			DateTime date1End,
			DateTime date2Start,
			DateTime date2End)
			=> date1Start < date2End && date2Start < date1End;

		private async Task<List<DTO.Query.TripGetResult>> GetCustomerRegisteredTrips(int customerId)
		{
			return await _tripManagementContext.Trips.Where(t => t.Customers.Any(c => c.Id == customerId))
				.Select(t => new DTO.Query.TripGetResult {
					Destination = t.Destination,
					EndTime = t.EndTime,
					Id = t.Id,
					Source = t.Source,
					StartTime = t.StartTime,
					IsCanceled = t.IsCanceled
				})
				.ToListAsync().ConfigureAwait(false);
		}
	}
}
