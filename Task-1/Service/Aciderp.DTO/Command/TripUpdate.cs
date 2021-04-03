using System;
using Aciderp.CQRS.Command;

namespace Aciderp.DTO.Command
{
    public class TripUpdate : ICommand
    {
		public int Id { get; set; }
		public string Source { get; set; }
		public string Destination { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
    }
}
