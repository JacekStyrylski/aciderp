namespace Aciderp.Data
{
    public abstract class Repository
    {
		protected readonly TripManagementContext _tripManagementContext;
		public Repository(TripManagementContext tripManagementContext)
		{
			this._tripManagementContext = tripManagementContext;
		}
    }
}
