using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xyperico.Base;
using System.Configuration;

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
    public UserNameSection UserName
    {
      get { return (UserNameSection)this["UserName"]; }
      set { this["UserName"] = value; }
    }


    public class AuthenticationProviderSection : ConfigurationSection
    {
      [ConfigurationProperty("Active")]
      public bool Active
      {
        get { return (bool)this["Active"]; }
        set { this["Active"] = value; }
      }


      [ConfigurationProperty("Name", IsKey=true)]
      public string Name
      {
        get { return (string)this["Name"]; }
        set { this["Name"] = value; }
      }


      [ConfigurationProperty("DisplayName")]
      public string DisplayName
      {
        get { return (string)this["DisplayName"]; }
        set { this["DisplayName"] = value; }
      }


      [ConfigurationProperty("Type")]
      public string Type
      {
        get { return (string)this["Type"]; }
        set { this["Type"] = value; }
      }


      [ConfigurationProperty("ClientId")]
      public string ClientId
      {
        get { return (string)this["ClientId"]; }
        set { this["ClientId"] = value; }
      }


      [ConfigurationProperty("ClientSecret")]
      public string ClientSecret
      {
        get { return (string)this["ClientSecret"]; }
        set { this["ClientSecret"] = value; }
      }


      public override string ToString()
      {
        return Name;
      }
    }


    public class UserNameSection : ConfigurationElement
    {
      public override bool IsReadOnly() { return false; }

      [ConfigurationProperty("MinLength")]
      public int MinLength
      {
        get { return (int)this["MinLength"]; }
        set { this["MinLength"] = value; }
      }


      [ConfigurationProperty("MaxLength")]
      public int MaxLength
      {
        get { return (int)this["MaxLength"]; }
        set { this["MaxLength"] = value; }
      }
    }
  }
}
