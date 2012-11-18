using System.ComponentModel.DataAnnotations;
using Xyperico.Base.DataAnnotations;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class LoginModel
  {
    [Required]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.Account))]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.Account))]
    public string Password { get; set; }

    [LocalizedDisplayName("Remember_me", NameResourceType=typeof(_.Account))]
    public bool RememberMe { get; set; }
  }
}