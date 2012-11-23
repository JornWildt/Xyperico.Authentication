using System;
using System.Collections.Generic;
using WebMatrix.WebData;
using Xyperico.Base;
using Xyperico.Base.Exceptions;
using System.Web.Security;
using CuttingEdge.Conditions;


namespace Xyperico.Authentication.Web
{
  public class SimpleMembershipProvider : ExtendedMembershipProvider
  {
    #region Dependencies

    private IUserRepository _userRepository;
    public IUserRepository UserRepository
    {
      get
      {
        if (_userRepository == null)
          _userRepository = ObjectContainer.Container.Resolve<IUserRepository>();
        return _userRepository;
      }
      set
      {
        _userRepository = value;
      }
    }


    private IUserAuthRelationRepository _userAuthRelationRepository;
    public IUserAuthRelationRepository UserAuthRelationRepository
    {
      get
      {
        if (_userAuthRelationRepository == null)
          _userAuthRelationRepository = ObjectContainer.Container.Resolve<IUserAuthRelationRepository>();
        return _userAuthRelationRepository;
      }
      set
      {
        _userAuthRelationRepository = value;
      }
    }

    #endregion


    #region Implemented

    public override bool ValidateUser(string username, string password)
    {
      try
      {
        User u = UserRepository.GetByUserName(username);
        return u.PasswordMatches(password);
      }
      catch (MissingResourceException)
      {
        return false;
      }
    }


    public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
    {
      try
      {
        Condition.Requires(values, "values").IsNotNull();
        string email = values["EMail"] as string;
        Condition.Requires(email, "values[EMail]").IsNotNullOrEmpty();
        User user = new User(userName, password, email);
        UserRepository.Add(user);
      }
      catch (DuplicateKeyException)
      {
        throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
      }
      return null; // Return what? "A token that can be sent to the user to confirm the user account."
    }


    public override int GetUserIdFromOAuth(string provider, string providerUserId)
    {
      try
      {
        UserAuthRelation rel = UserAuthRelationRepository.GetByExternalCredentials(provider, providerUserId);
        return rel.Id;
      }
      catch (MissingResourceException)
      {
        return -1;
      }
    }


    public override string GetUserNameFromId(int userId)
    {
      try
      {
        UserAuthRelation rel = UserAuthRelationRepository.Get(userId);
        User user = UserRepository.Get(rel.User_Id);
        return user.UserName;
      }
      catch (MissingResourceException)
      {
        return null;
      }
    }


    public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
    {
      try
      {
        User user = new User(userName, null, null);
        UserRepository.Add(user);

        UserAuthRelation rel = new UserAuthRelation(user.Id, provider, providerUserId);
        UserAuthRelationRepository.Add(rel);
      }
      catch (DuplicateKeyException)
      {
        throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
      }
    }

    #endregion

    public override bool ConfirmAccount(string accountConfirmationToken)
    {
      throw new NotImplementedException();
    }

    public override bool ConfirmAccount(string userName, string accountConfirmationToken)
    {
      throw new NotImplementedException();
    }

    public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
    {
      throw new NotImplementedException();
    }

    public override bool DeleteAccount(string userName)
    {
      throw new NotImplementedException();
    }

    public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
    {
      throw new NotImplementedException();
    }

    public override ICollection<WebMatrix.WebData.OAuthAccountData> GetAccountsForUser(string userName)
    {
      throw new NotImplementedException();
    }

    public override DateTime GetCreateDate(string userName)
    {
      throw new NotImplementedException();
    }

    public override DateTime GetLastPasswordFailureDate(string userName)
    {
      throw new NotImplementedException();
    }

    public override DateTime GetPasswordChangedDate(string userName)
    {
      throw new NotImplementedException();
    }

    public override int GetPasswordFailuresSinceLastSuccess(string userName)
    {
      throw new NotImplementedException();
    }

    public override int GetUserIdFromPasswordResetToken(string token)
    {
      throw new NotImplementedException();
    }

    public override bool IsConfirmed(string userName)
    {
      throw new NotImplementedException();
    }

    public override bool ResetPasswordWithToken(string token, string newPassword)
    {
      throw new NotImplementedException();
    }

    public override string ApplicationName
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      throw new NotImplementedException();
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
    {
      throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
      get { throw new NotImplementedException(); }
    }

    public override bool EnablePasswordRetrieval
    {
      get { throw new NotImplementedException(); }
    }

    public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
      throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
      throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
      get { throw new NotImplementedException(); }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
      get { throw new NotImplementedException(); }
    }

    public override int MinRequiredPasswordLength
    {
      get { throw new NotImplementedException(); }
    }

    public override int PasswordAttemptWindow
    {
      get { throw new NotImplementedException(); }
    }

    public override System.Web.Security.MembershipPasswordFormat PasswordFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string PasswordStrengthRegularExpression
    {
      get { throw new NotImplementedException(); }
    }

    public override bool RequiresQuestionAndAnswer
    {
      get { throw new NotImplementedException(); }
    }

    public override bool RequiresUniqueEmail
    {
      get { throw new NotImplementedException(); }
    }

    public override string ResetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
      throw new NotImplementedException();
    }

    public override void UpdateUser(System.Web.Security.MembershipUser user)
    {
      throw new NotImplementedException();
    }

  }
}
