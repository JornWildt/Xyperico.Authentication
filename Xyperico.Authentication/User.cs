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

    #endregion


    public User()
    {
    }


    public User(string userName, string password, string email)
    {
      Condition.Requires(userName, "userName").IsNotNullOrEmpty();
      //Condition.Requires(password, "password").IsNotNullOrEmpty();
      //Condition.Requires(email, "email").IsNotNull();

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
        byte[] salt, hash;
        GeneratePasswordHash(password, out salt, out hash);
        PasswordSalt = salt;
        PasswordHash = hash;
      }
    }


    public bool PasswordMatches(string password)
    {
      if (PasswordSalt == null || PasswordHash == null)
        return false;
      byte[] hash = GeneratePasswordHash(password, PasswordSalt);
      return hash.SequenceEqual(PasswordHash);
    }


    private void GeneratePasswordHash(string password, out byte[] salt, out byte[] hash)
    {
      // FIXME: refactor into separate class/interface
      salt = RandomStringGenerator.GenerateRandomBytes(20);
      hash = GeneratePasswordHash(password, salt);
    }


    private byte[] GeneratePasswordHash(string password, byte[] salt)
    {
      // FIXME: refactor into separate class/interface
      byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);

      var allBytes = new byte[salt.Length + passwordBytes.Length];

      Buffer.BlockCopy(salt, 0, allBytes, 0, salt.Length);
      Buffer.BlockCopy(passwordBytes, 0, allBytes, salt.Length, passwordBytes.Length);

      return HashAlgorithm.Create("SHA1").ComputeHash(allBytes);
    }

    
    public override string ToString()
    {
      return UserName + " (" + (this.EMail ?? "unknown e-mail") + ")";
    }
  }
}
