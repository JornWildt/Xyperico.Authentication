var UserNameChecker = {
  Initialize: function (inputSelector, statusSelector) {
    this.InputSelector = inputSelector;
    this.StatusSelector = statusSelector;
    $(inputSelector).blur(function () {
      UserNameChecker.CheckUserName();
    });
    $(inputSelector).keyup(function () {
      if (UserNameChecker.userNameTimer)
        clearTimeout(UserNameChecker.userNameTimer);
      var statusElement = $(UserNameChecker.StatusSelector);
      statusElement.html("");
      UserNameChecker.userNameTimer = setTimeout(UserNameChecker.CheckUserName, 1000);
    })
  },

  CheckUserName: function () {
    var userName = $(UserNameChecker.InputSelector).val();
    if (userName == "")
      return;
    var statusElement = $(UserNameChecker.StatusSelector);
    statusElement.html("<img src=\"/Areas/Account/Styles/spinner.gif\" /> <span>Checking ...</span>");
    $.get("/app/account/registration/checkusername", { userName: userName },
      function (data) {
        var statusElement = $(UserNameChecker.StatusSelector);
        var className = (data.Ok ? "ok" : "fail");
        var html = "<span class=\"{0}\">{1}</span>".format(className, data.Message);
        statusElement.html(html);
      });
  }
}