namespace Xyperico.Authentication
{
  public interface IUserAuthRelationRepository
  {
    void Add(UserAuthRelation rel);
    UserAuthRelation Get(int id);
    UserAuthRelation GetByExternalCredentials(string provider, string providerUserId);
    void Remove(int id);
  }
}
