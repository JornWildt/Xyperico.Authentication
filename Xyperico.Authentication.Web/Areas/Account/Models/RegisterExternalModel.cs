using System.ComponentModel.DataAnnotations;
using Xyperico.Base.DataAnnotations;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class RegisterExternalModel
  {
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.Account))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("EMail", NameResourceType = typeof(_.Account))]
    public string EMail { get; set; }

    public string ExternalLoginData { get; set; }

    public string ProviderName { get; set; }

    public string ProviderUserName { get; set; }

    public string ReturnUrl { get; set; }
  }
}