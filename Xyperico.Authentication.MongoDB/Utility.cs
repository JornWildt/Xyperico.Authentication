using MongoDB.Bson.Serialization;
using Xyperico.Base;


namespace Xyperico.Authentication.MongoDB
{
  public static class Utility
  {
    public static void Initialize(IObjectContainer container)
    {
      Xyperico.Base.MongoDB.Utility.Initialize();

      BsonClassMap.RegisterClassMap<User>();

      ConfigureDependencies(container);
    }


    private static void ConfigureDependencies(IObjectContainer container)
    {
      container.AddComponent<IUserRepository, UserRepository>();
      container.AddComponent<IUserAuthRelationRepository, UserAuthRelationRepository>();
    }
  }
}
