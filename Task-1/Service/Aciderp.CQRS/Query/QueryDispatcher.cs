using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Aciderp.CQRS.Query
{
    public class QueryDispatcher : IQueryDispatcher

    {
        IServiceProvider _provider;
        public QueryDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }
        public async Task<TResult> Execute<TQuery, TResult>(TQuery command)
            where TQuery : IQuery
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = _provider.GetService<IQueryHandler<TQuery, TResult>>();

            if (handler == null)
                throw new QueryHandlerNotFoundException(
                    (typeof(TQuery)).FullName);

            return await handler.Execute(command);
        }
    }
}
