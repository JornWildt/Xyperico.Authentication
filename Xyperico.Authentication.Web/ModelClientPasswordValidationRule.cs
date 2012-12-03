using System.Web.Mvc;


namespace Xyperico.Authentication.Web
{
  public class ModelClientPasswordValidationRule : ModelClientValidationRule
  {
    public ModelClientPasswordValidationRule()
    {
      ErrorMessage = "Client invalid password";
      this.ValidationParameters.Add("secret", "111");
      this.ValidationParameters.Add("length", "5");
      ValidationType = "passwordx";
    }
  }
}