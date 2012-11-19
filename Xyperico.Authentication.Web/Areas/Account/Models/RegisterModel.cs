using System.ComponentModel.DataAnnotations;
using Xyperico.Base.DataAnnotations;
using System.Web.Mvc;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class RegisterModel
  {
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.Account))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("EMail", NameResourceType = typeof(_.Account))]
    public string EMail { get; set; }

    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "p0_MustBe_p2_CharsLong", ErrorMessageResourceType = typeof(_.Account))]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.Account))]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [Compare("Password", ErrorMessageResourceName = "PasswordAndConfirmationDoesNotMatch", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("Confirm_Password", NameResourceType = typeof(_.Account))]
    public string ConfirmPassword { get; set; }
  }
}