var UserNameChecker = {
  Initialize: function (inputSelector, statusSelector, spinnerImageUrl) {
    UserNameChecker.InputSelector = inputSelector;
    UserNameChecker.StatusSelector = statusSelector;
    UserNameChecker.SpinnerImageUrl = spinnerImageUrl;
    UserNameChecker.LastValue = "";

    $(inputSelector).blur(function () {
      if (UserNameChecker.LastValue != $(UserNameChecker.InputSelector).val()) {
        if (UserNameChecker.userNameTimer)
          clearTimeout(UserNameChecker.userNameTimer);
        UserNameChecker.CheckUserName();
      }
    });

    $(inputSelector).keyup(function () {
      if (UserNameChecker.userNameTimer)
        clearTimeout(UserNameChecker.userNameTimer);
      if (UserNameChecker.LastValue != $(UserNameChecker.InputSelector).val()) {
        var statusElement = $(UserNameChecker.StatusSelector);
        statusElement.html("");
        UserNameChecker.userNameTimer = setTimeout(UserNameChecker.CheckUserName, 1000);
      }
    })
  },

  CheckUserName: function () {
    var userName = $(UserNameChecker.InputSelector).val();
    if (userName == "")
      return;
    UserNameChecker.LastValue = userName;
    var statusElement = $(UserNameChecker.StatusSelector);
    statusElement.html("<img src=\"" + UserNameChecker.SpinnerImageUrl + "\" />");
    $.get("/app/account/registration/checkusername", { userName: userName },
      function (data) {
        var statusElement = $(UserNameChecker.StatusSelector);
        var className = (data.Ok ? "ok" : "fail");
        var html = "<span class=\"{0}\">{1}</span>".format(className, data.Message);
        statusElement.html(html);
      });
  }
}


jQuery.validator.unobtrusive.adapters.add("passwordpolicy", ["policyexpr"], function (options) {
  options.rules["passwordpolicy"] = [options.params.policyexpr];
  options.messages["passwordpolicy"] = options.message;
});

jQuery.validator.addMethod('passwordpolicy', function (value, element, param) {
  var expr = new RegExp(param[0]);
  return expr.test(value);
});
