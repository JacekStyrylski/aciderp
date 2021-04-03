using Aciderp.CQRS.Command;

namespace Aciderp.DTO.Command
{
    public class AssignCustomerToTrip : ICommand
    {
        public int CustomerId { get; set; }
		public int TripId { get; set; }
    }
}
