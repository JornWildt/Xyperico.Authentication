using System;


namespace Xyperico.Authentication
{
  public class InvalidPasswordException : Exception
  {
    public InvalidPasswordException(string msg)
      : base(msg)
    {
    }
  }
}
