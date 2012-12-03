using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Xyperico.Authentication.Web
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class PasswordValidationAttribute : Xyperico.Authentication.PasswordValidationAttribute, IClientValidatable
  {
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
      PasswordPolicy policy = Xyperico.Authentication.Configuration.Settings.GetPasswordPolicy();

      var rule = new ModelClientPasswordValidationRule();
      rule.ValidationType = "passwordpolicy";
      rule.ValidationParameters["policyexpr"] = policy.GetExpression();
      yield return rule;
    }
  }
}