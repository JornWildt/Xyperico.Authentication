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
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.Account))]
    [PasswordValidation(ErrorMessageResourceName = "InvalidPassword", ErrorMessageResourceType = typeof(_.Account))]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [Compare("Password", ErrorMessageResourceName = "PasswordAndConfirmationDoesNotMatch", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("Confirm_Password", NameResourceType = typeof(_.Account))]
    public string ConfirmPassword { get; set; }

    public PasswordPolicy PasswordPolicy = new PasswordPolicy
    {
      MinPasswordLength = 5,
      MinNoOfLowerCaseChars = 3,
      MinNoOfUpperCaseChars = 2,
      MaxNoOfAllowedCharacterRepetitions = 3
    };

    public PasswordStrength PasswordStrength = new PasswordStrength
    {
      StrengthCategories = "weak,medium,strong",
      StrengthColours = "red,magenta,green",
      MinPasswordLengthStrength = "5,8,11",
      MinNoOfLowerCaseCharsStrength = "3,5,7",
      MinNoOfUpperCaseCharsStrength = "2,3,4",
      MaxNoOfAllowedCharacterRepetitionsStrength = "3,2,1"
    };
  }
}