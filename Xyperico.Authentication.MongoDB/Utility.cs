using MongoDB.Bson.Serialization;
using Xyperico.Base;


namespace Xyperico.Authentication.MongoDB
{
  public static class Utility
  {
    public static void Initialize(IObjectContainer container)
    {
      Xyperico.Base.MongoDB.Utility.Initialize();

      BsonClassMap.RegisterClassMap<User>(cm =>
      {
        cm.AutoMap();
        cm.GetMemberMap("EMailLowercase").SetIgnoreIfNull(true);
        cm.GetMemberMap("ExternalLogins").SetIgnoreIfNull(true);
      });

      ConfigureDependencies(container);
    }


    private static void ConfigureDependencies(IObjectContainer container)
    {
      container.AddComponent<IUserRepository, UserRepository>();
    }
  }
}
