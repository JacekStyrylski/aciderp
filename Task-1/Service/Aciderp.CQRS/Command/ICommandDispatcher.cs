using System.Threading.Tasks;

namespace Aciderp.CQRS.Command
{
    public interface ICommandDispatcher
    {
        Task Execute<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
