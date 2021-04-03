using System.Threading.Tasks;

namespace Aciderp.CQRS.Command
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }

}
