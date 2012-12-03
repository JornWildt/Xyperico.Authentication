using System.Configuration;
using Xyperico.Authentication.ConfigurationElements;
using Xyperico.Base;


namespace Xyperico.Authentication
{
  public class Configuration : ConfigurationSettingsBase<Configuration>
  {
    public override bool IsReadOnly() { return false; }


    [ConfigurationProperty("ExternalProviders")]
    public ConfigurationElementCollection<AuthenticationProviderSection> ExternalProviders
    {
      get { return (ConfigurationElementCollection<AuthenticationProviderSection>)this["ExternalProviders"]; }
    }


    [ConfigurationProperty("PasswordHashAlgorithm")]
    public string PasswordHashAlgorithm
    {
      get { return (string)this["PasswordHashAlgorithm"]; }
      set { this["PasswordHashAlgorithm"] = value; }
    }


    [ConfigurationProperty("InvalidUserNameFile")]
    public string InvalidUserNameFile
    {
      get { return (string)this["InvalidUserNameFile"]; }
      set { this["InvalidUserNameFile"] = value; }
    }


    [ConfigurationProperty("UserName")]
    public UserNameElement UserName
    {
      get { return (UserNameElement)this["UserName"]; }
      set { this["UserName"] = value; }
    }


    [ConfigurationProperty("PasswordPolicy")]
    public PasswordPolicyElement PasswordPolicy
    {
      get { return (PasswordPolicyElement)this["PasswordPolicy"]; }
      set { this["PasswordPolicy"] = value; }
    }


    public PasswordPolicy GetPasswordPolicy()
    {
      return new PasswordPolicy
      {
        MinPasswordLength = PasswordPolicy.MinPasswordLength,
        MinNoOfLowerCaseChars = PasswordPolicy.MinNoOfLowerCaseChars,
        MinNoOfUpperCaseChars = PasswordPolicy.MinNoOfUpperCaseChars,
        MinNoOfNumbers = PasswordPolicy.MinNoOfNumbers,
        MaxNoOfAllowedCharacterRepetitions = PasswordPolicy.MaxNoOfAllowedCharacterRepetitions
      };
    }
  }
}
