using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Aciderp.CQRS.Command
{
    public class CommandDispatcher : ICommandDispatcher

    {
        IServiceProvider _provider;
        public CommandDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }
        public async Task Execute<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = _provider.GetService<ICommandHandler<TCommand>>();

            if (handler == null)
                throw new CommandHandlerNotFoundException(
                    (typeof(TCommand)).FullName);

            await handler.Execute(command);
        }
    }
}
