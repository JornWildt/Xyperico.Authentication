using System.Configuration;


namespace Xyperico.Authentication.ConfigurationElements
{
  public class UserNameElement : ConfigurationElement
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
