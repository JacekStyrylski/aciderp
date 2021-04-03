using System.Threading.Tasks;

namespace Aciderp.CQRS.Query
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
    {
        Task<TResult> Execute(TQuery query);
    }
}
