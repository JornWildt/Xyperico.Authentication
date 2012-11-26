namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class ExternalLoginBaseData
  {
    public string ExternalLoginData { get; set; }

    public string ProviderName { get; set; }

    public string ProviderUserName { get; set; }

    public string ProviderEMail { get; set; }

    public string ReturnUrl { get; set; }
  }
}