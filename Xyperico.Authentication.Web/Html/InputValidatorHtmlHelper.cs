﻿using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Xyperico.Web.Mvc;


namespace Xyperico.Authentication.Web.Html
{
  public static class InputValidatorHtmlHelper
  {
    public static IHtmlString UserNameValidatorFor<TModel, TProperty>(
      this HtmlHelper<TModel> html,
      Expression<Func<TModel, TProperty>> expression)
    {
      ModelMetadata meta = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
      string inputId = meta.PropertyName;
      
      string actionUrl = UrlHelper.GenerateUrl("Account_default", "checkusername", "registration", new RouteValueDictionary(new { Area="Account" }), html.RouteCollection, html.ViewContext.RequestContext, false);
      return html.GenerateInputValidator(inputId, actionUrl);
    }


    public static IHtmlString EMailValidatorFor<TModel, TProperty>(
      this HtmlHelper<TModel> html,
      Expression<Func<TModel, TProperty>> expression)
    {
      ModelMetadata meta = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
      string inputId = meta.PropertyName;
      string actionUrl = UrlHelper.GenerateUrl("Account_default", "checkemail", "registration", new RouteValueDictionary(new { Area = "Account" }), html.RouteCollection, html.ViewContext.RequestContext, false);
      return html.GenerateInputValidator(inputId, actionUrl);
    }


    private static IHtmlString GenerateInputValidator(
      this HtmlHelper html,
      string inputId,
      string checkUrl)
    {
      string spinnerImageUrl = html.ImageUrl("load-indicator-16x16.gif").ToString();

      string script = string.Format(@"<span id=""{0}Status"" class=""validator-status""></span>
<script type=""text/javascript"">
  $(document).ready(function () {{
    new AjaxInputChecker('#{0}', '#{0}Status', '{1}', '{2}');
  }});
</script>",
          inputId,
          checkUrl,
          spinnerImageUrl);

      return new MvcHtmlString(script);
    }
  }
}