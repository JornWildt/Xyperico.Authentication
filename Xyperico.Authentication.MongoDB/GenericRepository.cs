using Xyperico.Base;
using Xyperico.Base.MongoDB;


namespace Xyperico.Authentication.MongoDB
{
  public abstract class GenericRepository<T, TId> : MongoDBGenericRepository<T, TId>
    where T : class, IHaveId<TId>
  {
    public override string MongoConfigEntry
    {
      get { return "Xyperico.Authentication"; }
    }
  }
}
