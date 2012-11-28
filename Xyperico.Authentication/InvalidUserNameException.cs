using System;


namespace Xyperico.Authentication
{
  public class InvalidUserNameException : Exception
  {
    public string UserName { get; protected set; }

    public InvalidUserNameException(string userName)
      : base(string.Format("Invalid user name '{0}'.", userName))
    {
      UserName = userName;
    }
  }
}
