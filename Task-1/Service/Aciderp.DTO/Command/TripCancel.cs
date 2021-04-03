using Aciderp.CQRS.Command;

namespace Aciderp.DTO.Command
{
    public class TripCancel : ICommand
    {
        public int Id { get; set; }
    }
}
