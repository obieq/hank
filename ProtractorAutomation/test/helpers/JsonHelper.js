/**
 * Created by vyom.sharma on 10-02-2015.
 */

var JsonHelper = function () {

  var path = require('path');
  var FS = require("q-io/fs");
  var RestApiHelper = require('./RestApiHelper.js');
  var restApiHelper = new RestApiHelper();

  this.format = function () {
    var formatted = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
      var regexp = new RegExp('\\{' + (i - 1) + '\\}', 'gi');
      formatted = formatted.replace(regexp, arguments[i]);
    }
    return formatted;
  };

  this.customTrim = function (str) {
    return str.trim().replace(/(\r\n|\n|\r)/gmi, " ").replace(/\s\s+/gi, " ")
  };

  this.log = function (txt) {
    FS.isFile("Log.txt").then(
      function (value) {
        if (value) {
          FS.append("Log.txt", "\n " + txt + " \n");
        }
        else {
          FS.write("Log.txt", "\n " + txt + " \n");
        }
      }
    );
  };

  this.GetIndexedVariableValueFromVariableContainer = function (varName) {
    var res = varName.substring(0, varName.indexOf('['));
    for (var m = 0; m < browser.params.config.variableContainer.length; m++) {
      if (browser.params.config.variableContainer[m].Name == res) {
        browser.params.config.variableContainer[m].JsonValue = JSON.parse(browser.params.config.variableContainer[m].Value);
        return eval("browser.params.config.variableContainer[m].JsonValue" + varName.substring(varName.indexOf('[')));
      }
    }
  };

  this.parseJsonToExecuteSql = function (testInstance) {
    var defer = protractor.promise.defer();
    console.log("Inside set variable step type 3");
    var params = browser.params;

    var urleTohit = this.format(params.config.baseApiUrl + params.config.executeSqlUrl, params.config.TestQueueId);
    console.log("urleTohit= " + urleTohit);
    var rex = /\{([^}]+)\}/g;
    var matches = testInstance.Value.match(rex);
    console.log("matches= " + matches);
    if (matches != undefined || matches != null) {
      testInstance.VariablesUsedInQuery = [];
      for (var l = 0; l < matches.length; l++) {
        testInstance.VariablesUsedInQuery[l] = {};
        testInstance.VariablesUsedInQuery[l].Name = matches[l].substring(1, matches[l].length - 1);
        if (testInstance.VariablesUsedInQuery[l].Name.indexOf('[') == -1) {
          for (var m = 0; m < browser.params.config.variableContainer.length; m++) {
            if (browser.params.config.variableContainer[m].Name == testInstance.VariablesUsedInQuery[l].Name) {
              testInstance.VariablesUsedInQuery[l].Value = browser.params.config.variableContainer[m].Value;
              break;
            }
          }
        }
        else {
          testInstance.VariablesUsedInQuery[l].Value = this.GetIndexedVariableValueFromVariableContainer(testInstance.VariablesUsedInQuery[l].Name);
        }
      }
    }
    var a = [];
    restApiHelper.doPost(urleTohit, testInstance, function (msg) {
      var resultMessage = JSON.parse(msg.body);
      console.log("Length= " + resultMessage.Item.length);
      if (resultMessage.Item != "null") {
        for (var i = 0; i < resultMessage.Item.length; i++) {
          a[i] = [];
          var keys = Object.keys(resultMessage.Item[i]);
          for (var j = 0; j < keys.length; j++) {
            a[i][j] = resultMessage.Item[i][keys[j]];
          }
        }
      }
      defer.fulfill(a);
    });

    return defer.promise;
  };

  this.parseJsonArrays = function (stringToParse) {
    var retArray = [];
    try {
      retArray = JSON.parse(stringToParse);
    }
    catch (ex) {
      retArray = [];
    }
    return retArray;
  };

  this.inArray = function (jarry, objectToFind, ignoreCase) {
    var retIndex = -1;

    ignoreCase = ignoreCase == undefined ? false : ignoreCase;

    for (var j = 0; j < jarry.length; j++) {
      if ((jarry[j] == objectToFind) || (ignoreCase && jarry[j].toLowerCase() == objectToFind.toLowerCase())) {
        retIndex = j;
        break;
      }
    }
    return retIndex;
  };


  this.buildPath = function (reportPath, descriptions, capabilities) {
    var date = new Date();
    var month = (date.getMonth() + 1).toString();
    month = (month.length > 1 ? "" : "0") + month;
    var day = date.getDate().toString();
    day = (day.length > 1 ? "" : "0") + day;
    if (reportPath == "" || reportPath == undefined) {
      reportPath = day + '-' + month + '-' + date.getFullYear() + '-' + date.getHours();
    }
    var browserDetails = capabilities.caps_.platform + " " + capabilities.caps_.browserName + " " + capabilities.caps_.version;
    var timeCode = date.getHours() + " " + date.getMinutes() + " " + date.getSeconds();
    curTestReportPath = path.join(reportPath, browserDetails, descriptions.join('-') + timeCode);
    return curTestReportPath;
  }

  this.gatherDescriptions = function (suite, soFar) {
    soFar.push(suite.description);
    if (suite.parentSuite) {
      return this.gatherDescriptions(suite.parentSuite, soFar);
    } else {
      return soFar;
    }
  }

};
module.exports = JsonHelper;
