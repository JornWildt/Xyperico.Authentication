using System;


namespace Xyperico.Authentication.Contract
{
  public interface ICurrentUserService
  {
    Guid UserId { get; }
  }
}
