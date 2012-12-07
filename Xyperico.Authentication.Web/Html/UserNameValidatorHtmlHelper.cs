using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Xyperico.Web.Mvc;


namespace Xyperico.Authentication.Web.Html
{
  public static class UserNameValidatorHtmlHelper
  {
    public static IHtmlString UserNameValidatorFor<TModel, TProperty>(
      this HtmlHelper<TModel> html,
      Expression<Func<TModel, TProperty>> expression)
    {
      ModelMetadata meta = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
      
      string inputId = meta.PropertyName;
      string spinnerImageUrl = html.ImageUrl("load-indicator-16x16.gif").ToString();
      
      string script = string.Format(@"<span id=""{0}Status""></span>
<script type=""text/javascript"">
  $(document).ready(function () {{
    UserNameChecker.Initialize(""#{0}"", ""#{0}Status"", ""{1}"");
  }});
</script>",
          inputId,
          spinnerImageUrl);

      return new MvcHtmlString(script);
    }
  }
}