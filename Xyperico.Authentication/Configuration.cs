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
    public override bool IsReadOnly()
    {
      return false;
    }


    [ConfigurationProperty("ExternalProviders")]
    public ConfigurationElementCollection<AuthenticationProvider> ExternalProviders
    {
      get { return (ConfigurationElementCollection<AuthenticationProvider>)this["ExternalProviders"]; }
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


    public class AuthenticationProvider : ConfigurationSection
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
  }
}
