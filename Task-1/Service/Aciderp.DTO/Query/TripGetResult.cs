using System;

namespace Aciderp.DTO.Query
{
    public class TripGetResult
    {
		public int Id { get; set; }
		public string Source { get; set; }
		public string Destination { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public bool IsCanceled { get; set; }
    }
}
