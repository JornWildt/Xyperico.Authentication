using NUnit.Framework;
using Xyperico.Base.Exceptions;
using System;


namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class UserAuthRelationRepositoryTests : TestHelper
  {
    IUserAuthRelationRepository UserAuthRelationRepository = new Xyperico.Authentication.MongoDB.UserAuthRelationRepository();


    [Test]
    public void CanAddAndGetUserAuthRelation()
    {
      // Arrange
      UserAuthRelation rel1 = UserAuthRelationBuilder.BuildUserAuthRelation();

      // Act
      UserAuthRelation rel2 = UserAuthRelationRepository.Get(rel1.Id);

      // Assert
      Assert.IsNotNull(rel2);
      Assert.AreEqual(rel1.Id, rel2.Id);
      Assert.AreEqual(rel1.User_Id, rel2.User_Id);
      Assert.AreEqual(rel1.Provider, rel2.Provider);
      Assert.AreEqual(rel1.ProviderUserId, rel2.ProviderUserId);
    }


    [Test]
    public void WhenGettingUnknownRelationItThrowsMissingResource()
    {
      AssertThrows<MissingResourceException>(() => UserAuthRelationRepository.Get(92764882));
    }


    //[Test]
    //public void WhenAddingSameRelationIdTwiceItThrowsDuplicateKey()
    //{
    //  // Arrange
    //  UserAuthRelation rel1 = UserAuthRelationBuilder.BuildUserAuthRelation();
    //  UserAuthRelation rel2 = new UserAuthRelation(rel1.Id, Guid.NewGuid(), rel1.Provider+"1", rel1.ProviderUserId+"1");

    //  UserAuthRelationBuilder.RegisterInstance(rel2);

    //  // Act + Assert
    //  AssertThrows<DuplicateKeyException>(() => UserAuthRelationRepository.Add(rel2));
    //}


    [Test]
    public void WhenAddingSameExternalUserIdTwiceItThrowsDuplicateKey()
    {
      // Arrange
      UserAuthRelation rel1 = UserAuthRelationBuilder.BuildUserAuthRelation();
      UserAuthRelation rel2 = new UserAuthRelation(Guid.NewGuid(), rel1.Provider, rel1.ProviderUserId);

      UserAuthRelationBuilder.RegisterInstance(rel2);

      // Act + Assert
      AssertThrows<DuplicateKeyException>(
        () => UserAuthRelationRepository.Add(rel2),
        ex => ex.Key == "UserId");
    }


    [Test]
    public void ItCanAddMultipleExternalUserIdsForSameProvider()
    {
      // Arrange
      UserAuthRelation rel1 = UserAuthRelationBuilder.BuildUserAuthRelation();
      UserAuthRelation rel2 = new UserAuthRelation(Guid.NewGuid(), rel1.Provider, rel1.ProviderUserId+"1");

      UserAuthRelationBuilder.RegisterInstance(rel2);

      // Act
      UserAuthRelationRepository.Add(rel2);

      // Assert
      Assert.Pass();
    }


    [Test]
    public void ItCanGetByExternalCredentials()
    {
      // Arrange
      UserAuthRelation rel1 = UserAuthRelationBuilder.BuildUserAuthRelation();

      // Act
      UserAuthRelation rel2 = UserAuthRelationRepository.GetByExternalCredentials(rel1.Provider, rel1.ProviderUserId);

      // Assert
      Assert.IsNotNull(rel2);
    }


    [Test]
    public void WhenGettingByUnknownExternalCredentialsItThrowsMissingResource()
    {
      // Arrange
      UserAuthRelation rel1 = UserAuthRelationBuilder.BuildUserAuthRelation();

      // Act + Assert
      AssertThrows<MissingResourceException>(() => UserAuthRelationRepository.GetByExternalCredentials("unknown", "unknown"));
      AssertThrows<MissingResourceException>(() => UserAuthRelationRepository.GetByExternalCredentials(rel1.Provider, "unknown"));
      AssertThrows<MissingResourceException>(() => UserAuthRelationRepository.GetByExternalCredentials("unknown", rel1.ProviderUserId));
    }


    [Test]
    public void WhenAddingTwiceItGivesNewUserIdNumbers()
    {
      // Act
      UserAuthRelation rel1 = UserAuthRelationBuilder.BuildUserAuthRelation(providerUserId: "a");
      UserAuthRelation rel2 = UserAuthRelationBuilder.BuildUserAuthRelation(providerUserId: "b");

      // Assert
      Assert.Greater(rel1.Id, 0);
      Assert.AreNotEqual(rel1.Id, rel2.Id);
    }
  }
}
