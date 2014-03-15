using System.ComponentModel.DataAnnotations;
using Xyperico.Base.DataAnnotations;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class RegisterUnknownExternalModel : ExternalLoginBaseData
  {
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.Account))]
    public string UserName { get; set; }

    [LocalizedDisplayName("EMail", NameResourceType = typeof(_.Account))]
    public string EMail { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "p0_MustBe_p2_CharsLong", ErrorMessageResourceType = typeof(_.Account))]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.Account))]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessageResourceName = "PasswordAndConfirmationDoesNotMatch", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("Confirm_Password", NameResourceType = typeof(_.Account))]
    public string ConfirmPassword { get; set; }

    public string IsRedirect { get; set; }
  }
}