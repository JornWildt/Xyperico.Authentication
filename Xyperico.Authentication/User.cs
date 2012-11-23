using System;
using System.Linq;
using Xyperico.Base;
using CuttingEdge.Conditions;
using Xyperico.Base.Crypto;
using System.Security.Cryptography;


namespace Xyperico.Authentication
{
  public class User : IHaveId<Guid>
  {
    #region Public persisted properties

    public Guid Id { get; protected set; }

    public string UserName { get; protected set; }

    public string UserNameLowercase { get; protected set; }

    public string EMail { get; protected set; }

    public string EMailLowercase { get; protected set; }

    public byte[] PasswordHash { get; protected set; }

    public byte[] PasswordSalt { get; protected set; }

    public string PasswordHashAlgorithm { get; set; }

    #endregion


    public User()
    {
    }


    public User(string userName, string password, string email)
    {
      Condition.Requires(userName, "userName").IsNotNullOrEmpty();

      Id = Guid.NewGuid();
      UserName = userName;
      UserNameLowercase = userName.ToLower();

      if (email != null)
      {
        EMail = email;
        EMailLowercase = email.ToLower();
      }

      if (password != null)
      {
        PasswordHashAlgorithm = Configuration.Settings.PasswordHashAlgorithm;
        byte[] salt, hash;
        GeneratePasswordHash(password, out salt, out hash);
        PasswordSalt = salt;
        PasswordHash = hash;
      }
    }


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


    public override string ToString()
    {
      return UserName + " (" + (this.EMail ?? "unknown e-mail") + ")";
    }
  }
}
