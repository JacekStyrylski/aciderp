using System.Threading.Tasks;

namespace Aciderp.CQRS.Query
{
    public interface IQueryDispatcher
    {
        Task<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;
    }
}
