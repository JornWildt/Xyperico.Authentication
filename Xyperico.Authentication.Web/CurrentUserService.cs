using System;
using System.Web;
using Xyperico.Authentication.Contract;


namespace Xyperico.Authentication.Web
{
  public class CurrentUserService : ICurrentUserService
  {
    #region Dependencies

    public IUserRepository UserRepository { get; set; }

    #endregion


    #region ICurrentUserService Members

    public Guid UserId
    {
      get 
      {
        if (HttpContext.Current == null || HttpContext.Current.User == null || HttpContext.Current.User.Identity == null)
          throw new InvalidOperationException("No current user available");
        User u = UserRepository.GetByUserName(HttpContext.Current.User.Identity.Name);
        return u.Id;
      }
    }

    #endregion
  }
}