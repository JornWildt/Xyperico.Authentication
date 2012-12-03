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

// ===================================================================================================
// Password strength checker
// From http://www.c-sharpcorner.com/uploadfile/f9935e/password-policystrength-asp-net-mvc-validator
// ===================================================================================================

function IsUserNameContained(username, password) {
  if (username == '')
    return false;
  if (password.toUpperCase().indexOf(username.toUpperCase()) > -1) {
    return true;
  } else {
    return false;
  }
}

function IsValidPolicy(pwd, val) {
  var exprPolicy = val.getAttribute('validationexpression')

  var exprMaxAllowedRepetitions = '';
  var exprRest = '';

  try {
    exprMaxAllowedRepetitions = exprPolicy.match(/(^.*?\\2.*?\$\))/)[0];

    exprMaxAllowedRepetitions = exprMaxAllowedRepetitions + '.*$';
  } catch (ex) {
  }

  try {
    exprRest = '^' + exprPolicy.match(/^(.*?\\2.*?\$\))?(.*)$/)[2];
  }
  catch (ex) {
  }

  if (pwd.match(exprRest) == null) {
    return false;
  }

  if (exprMaxAllowedRepetitions != '') {
    return (pwd.match(exprMaxAllowedRepetitions) != null);
  }

  return true;
}

function PasswordPolicyValidatorValidate(ClientID, ControlLabelID, UserNameControlClientID) {
  var val = document.getElementById(ClientID);
  var controlLabel = document.getElementById(ControlLabelID);
  var pwd = val.value;

  if (!IsValidPolicy(pwd, controlLabel)) {
    return false;
  }

  if (UserNameControlClientID != '') {
    var username = document.getElementById(UserNameControlClientID).value;
    var pwd = val.value;

    if (IsUserNameContained(username, pwd)) {
      return false;
    }
  }
  return true;
}

function GetMaxAllowedRepetitionsExpression(array2, categoryPos1) {
  var expr1 = '^(?=^((.)(?!(.*?\\2){$$MaxNoOfAllowedRepetitions$$,$$MaxNoOfAllowedRepetitions$$}))+$).*$';

  for (i = 0; i < array2.length; i++) {
    if ((array2[i][1]).indexOf('Max') >= 0) {
      expr1 = expr1.replace('$$' + array2[i][1] + '$$', array2[i][categoryPos1 + 1] != null ? array2[i][categoryPos1 + 1] : '');
      expr1 = expr1.replace('$$(?!Max)' + array2[i][1] + '$$', array2[i][categoryPos1] != null ? array2[i][categoryPos1] : '');
    }
  }

  expr1 = expr1.replace(/[$]{2,2}Max.*?[$]{2,2}/gi, '');

  return expr1;
}

function IsMaxAllowedRepetitionsSpecified(array1) {
  for (i = 0; i < array1.length; i++) {
    if ((array1[i][1]).indexOf('Max') == 0) {
      return true
    }
  }
  return false;
}

function GetExpression(arrayStr, categoryPos, Unicase, lowercase) {
  Unicase = (Unicase == '' || Unicase == null ? 'A-Z' : Unicase);
  lowercase = (lowercase == '' || lowercase == null ? 'a-z' : lowercase);

  var expr = '^(?=.{$$MinPasswordLength$$,$$MinPasswordLength$$})(?=(.*?[0-9]){$$MinNoOfNumbers$$,$$MinNoOfNumbers$$})(?=([^' + Unicase + ']*?[' + Unicase + ']){$$MinNoOfUniCaseChars$$,$$MinNoOfUpperUniChars$$})(?=([^' + lowercase + ']*?[' + lowercase + ']){$$MinNoOfLowerCaseChars$$,$$MinNoOfLowerCaseChars$$})(?=([^' + Unicase + ']*?[' + Unicase + ']){$$MinNoOfUpperCaseChars$$,$$MinNoOfUpperCaseChars$$})(?=([' + Unicase + lowercase + '0-9]*?[^' + Unicase + lowercase + '0-9]){$$MinNoOfSymbols$$,$$MinNoOfSymbols$$}).*$';

  for (i = 0; i < arrayStr.length; i++) {
    if ((arrayStr[i][1]).indexOf('Max') < 0) {
      expr = expr.replace('$$' + arrayStr[i][1] + '$$', arrayStr[i][categoryPos + 1]); expr = expr.replace('$$' + arrayStr[i][1] + '$$', arrayStr[i][categoryPos + 2] != null ? arrayStr[i][categoryPos + 2] - 1 : '');
    }
    else
      expr = expr.replace('$$' + arrayStr[i][1] + '$$', arrayStr[i][categoryPos + 2] != null ? arrayStr[i][categoryPos + 2] : ''); expr = expr.replace('$$(?!Max)' + arrayStr[i][1] + '$$', arrayStr[i][categoryPos + 1] != null ? arrayStr[i][categoryPos + 1] : '');
  }
  while (expr.match(/[$]{2,2}.*?[$]{2,2}/)) {
    expr = expr.replace(/[$]{2,2}.*?[$]{2,2}/, '0'); expr = expr.replace(/[$]{2,2}.*?[$]{2,2}/, '');
  }

  return expr;
}

function IsLowerCategory(category1, category2, arrCategories) {
  if (category1 == '' || category2 == '') {
    return arrCategories[0];
  }
  if (arrCategories.length > 0) {
    var IsCategory2Matched = false;
    for (m = 0; m < arrCategories.length; m++) {
      if (arrCategories[m] == category1) {
        return category1;
      }
      if (arrCategories[m] == category2) {
        return category2;
      }
    }
  }
}

function GetStrength(arr, arrCategories, ClientID, arrUnicode) {
  var val = document.getElementById(ClientID);

  var pwd = val.value;

  var lastMatchCategory = 'Strength did not match.';

  if (arrCategories.length > 0) {
    var lastMatchCategory = arrCategories[0];

    for (k = 0; k < arrCategories.length; k++) {
      if (pwd.match(GetExpression(arr, k + 1, arrUnicode[0], arrUnicode[1] != null ? arrUnicode[1] : '')) != null) {
        lastMatchCategory = arrCategories[k];
      }
      else
        break;
    }

    var lastMatchCategoryAllowedRepetitions = '';
    if (IsMaxAllowedRepetitionsSpecified(arr)) {
      for (k = 0; k < arrCategories.length; k++) {
        if (pwd.match(GetMaxAllowedRepetitionsExpression(arr, k + 1)) != null) {
          lastMatchCategoryAllowedRepetitions = arrCategories[k];
        }
      }
    }
    else {
      lastMatchCategoryAllowedRepetitions = 'Does not apply';
    }
  }

  return IsLowerCategory(lastMatchCategory, lastMatchCategoryAllowedRepetitions, arrCategories);
}

function ShowStrengthColour(strength, arrCategories, arrColour, labelID) {
  for (n = 0; n < arrCategories.length; n++) {
    if (arrCategories[n] == strength) {
      break;
    }
  }

  var lblMessage = $('#'+labelID);

  lblMessage.text(strength);
  lblMessage.css('color', arrColour[n]);
}



jQuery.validator.unobtrusive.adapters.add("passwordpolicy", ["policyexpr"], function (options) {
  options.rules["passwordpolicy"] = [options.params.policyexpr];
  options.messages["passwordpolicy"] = options.message;
});

jQuery.validator.addMethod('passwordpolicy', function (value, element, param) {
  var expr = new RegExp(param[0]);
  return expr.test(value);
});
