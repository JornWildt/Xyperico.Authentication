using System;
using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using Xyperico.Base;
using Xyperico.Base.Crypto;


namespace Xyperico.Authentication
{
  public class User : IHaveId<Guid>
  {
    #region Persisted properties

    public Guid Id { get; protected set; }

    /// <summary>
    /// Auto incremented by persistence layer - for use with SimpleMembershipProvider.
    /// </summary>
    public int UserId { get; set; }

    public string UserName { get; protected set; }

    public string UserNameLowercase { get; protected set; }

    public string EMail { get; protected set; }

    public string EMailLowercase { get; protected set; }

    public byte[] PasswordHash { get; protected set; }

    public byte[] PasswordSalt { get; protected set; }

    public string PasswordHashAlgorithm { get; protected set; }

    // Whatch out - can be null (otherwise MongDoDB won't ignore it)
    public List<ExternalLogin> ExternalLogins { get; protected set; }

    #endregion


    public class ExternalLogin
    {
      public string Provider { get; protected set; }

      public string ProviderUserId { get; protected set; }

      public ExternalLogin(string provider, string providerUserId)
      {
        Condition.Requires(provider, "provider").IsNotNullOrEmpty();
        Condition.Requires(providerUserId, "providerUserId").IsNotNullOrEmpty();

        Provider = provider;
        ProviderUserId = providerUserId;
      }
    }



    public User()
    {
    }


    public User(string userName, string password, string email, IUserNameValidator userNameValidator, PasswordPolicy passwordPolicy)
    {
      Condition.Requires(userName, "userName").IsNotNullOrEmpty();
      Condition.Requires(userNameValidator, "userNameValidator").IsNotNull();

      if (!userNameValidator.IsValidUserName(userName))
        throw new InvalidUserNameException(userName);

      UserId = -1;
      UserName = userName;
      UserNameLowercase = userName.ToLower();

      if (email != null)
      {
        EMail = email;
        EMailLowercase = email.ToLower();
      }

      if (password != null)
      {
        ChangePassword(password, passwordPolicy);
      }
    }


    #region Password

    private void GeneratePasswordHash(string password, out byte[] salt, out byte[] hash)
    {
      salt = RandomStringGenerator.GenerateRandomBytes(20);
      hash = PasswordHasher.GeneratePasswordHash(password, salt, PasswordHashAlgorithm);
    }


    public bool PasswordMatches(string password)
    {
      if (PasswordSalt == null || PasswordHash == null)
        return false;
      byte[] hash = PasswordHasher.GeneratePasswordHash(password, PasswordSalt, PasswordHashAlgorithm);
      return hash.SequenceEqual(PasswordHash);
    }


    public void ChangePassword(string newPassword, PasswordPolicy passwordPolicy)
    {
      if (newPassword != null)
      {
        if (!passwordPolicy.IsValid(newPassword))
          throw new InvalidPasswordException(passwordPolicy.GetDescription(_.Auth.Password));

        PasswordHashAlgorithm = Configuration.Settings.PasswordHashAlgorithm;
        byte[] salt, hash;
        GeneratePasswordHash(newPassword, out salt, out hash);
        PasswordSalt = salt;
        PasswordHash = hash;
      }
      else
      {
        PasswordHash = null;
      }
    }

    #endregion


    #region External logins

    public void AddExternalLogin(string provider, string providerUserId)
    {
      if (ExternalLogins == null)
        ExternalLogins = new List<ExternalLogin>();
      if (!HasExternalLogin(provider, providerUserId))
        ExternalLogins.Add(new ExternalLogin(provider, providerUserId));
    }


    public bool HasExternalLogin(string provider, string providerUserId)
    {
      return    ExternalLogins != null
             && ExternalLogins.Any(l => l.Provider == provider && l.ProviderUserId == providerUserId);
    }


    public IEnumerable<ExternalLogin> GetExternalLogins()
    {
      return ExternalLogins ?? Enumerable.Empty<ExternalLogin>();
    }

    #endregion


    #region EMail

    public void ChangeEMail(string email)
    {
      EMail = email;
      EMailLowercase = (email != null ? email.ToLower() : null);
    }

    #endregion


    public override string ToString()
    {
      return UserName + " (" + (this.EMail ?? "unknown e-mail") + ")";
    }
  }
}
