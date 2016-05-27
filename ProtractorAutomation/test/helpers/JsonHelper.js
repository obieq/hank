/**
 * Created by vyom.sharma on 10-02-2015.
 */

var JsonHelper = function () {

  var path = require('path');
  var FS = require("q-io/fs");
  var RestApiHelper = require('./RestApiHelper.js');
  var restApiHelper = new RestApiHelper();

  var thisObj = this;

  this.format = function () {
    var formatted = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
      var regexp = new RegExp('\\{' + (i - 1) + '\\}', 'gi');
      formatted = formatted.replace(regexp, arguments[i]);
    }
    return formatted;
  };

  this.customTrim = function (str) {
    if (typeof (str) == 'object') {
      return str[0].trim().replace(/(\r\n|\n|\r)/gmi, " ").replace(/\s\s+/gi, " ")
    }
    else {
      return str.trim().replace(/(\r\n|\n|\r)/gmi, " ").replace(/\s\s+/gi, " ")
    }
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

  this.GetIndexesFromVariable = function (varName, aryData) {
    varName = varName || "";

    varName = this.checkVariableInVariableAndGetValue(varName);

    var indexes = varName.match(/\[(.*?)\]/g) || [];

    for (var i = 0; i < indexes.length; i++) {
      indexes[i] = indexes[i].replace("[", "").replace("]", "");
    }

    if (aryData == undefined || aryData[0] == undefined) {
      return undefined;
    }

    var idxRow = indexes[0] || "0";
    var idxCol = indexes.length > 1 ? indexes[1] : undefined;

    var idxRowVal = idxRow;
    var idxColVal = idxCol;

    if (isNaN(idxRow) && indexes.length == 1) {
      var rowIndex = -1;
      var tmpColIndex = -1;
      var searchData = idxRow.split(";");
      var colNameValuePair = [];

      idxCol = "idxCol";
      varName += "[idxCol]";

      for (var i = 0; i < searchData.length; i++) {
        var tmpData = searchData[i].split('~');
        colNameValuePair.push({ColIndex: this.inArray(aryData[0], tmpData[0], true), Value: tmpData[1]});
      }

      var isMatchFound = false;
      for (var i = 0; i < aryData.length; i++) {
        isMatchFound = true;
        for (var j = 0; j < colNameValuePair.length; j++) {
          if (colNameValuePair[j].Value && colNameValuePair[j].Value.toLowerCase() != (aryData[i][colNameValuePair[j].ColIndex] + "").toLowerCase()) {
            isMatchFound = false;
            break;
          }
        }

        if (isMatchFound) {
          rowIndex = colNameValuePair.length == 1 && colNameValuePair[0].Value == undefined ? 1 : i;
          tmpColIndex = colNameValuePair[colNameValuePair.length - 1].ColIndex;
          break;
        }
      }

      idxRowVal = rowIndex;
      idxColVal = tmpColIndex;

      if (!isNaN(searchData[searchData.length - 1])) {
        idxColVal = searchData[searchData.length - 1];
      }

      if (rowIndex == -1) {
        idxRowVal = 0;
        idxColVal = -1;
      }
    }

    if (isNaN(idxRow) && isNaN(idxRowVal) && idxCol && isNaN(idxCol)) {
      idxColVal = this.inArray(aryData[0], idxRow, true);

      for (var i = 0; i < aryData.length; i++) {
        if (aryData[i][idxColVal].toLowerCase() == idxCol.toLowerCase()) {
          idxRowVal = i;
          break;
        }
      }

      idxColVal = isNaN(idxRowVal) ? "-1" : idxColVal;
      idxRowVal = isNaN(idxRowVal) ? "0" : idxRowVal;
    }

    if (isNaN(idxRow) && isNaN(idxRowVal)) {
      idxRowVal = this.inArray(aryData[0], idxRow, true);
    }

    if (idxCol && isNaN(idxCol) && isNaN(idxColVal) && aryData[idxRowVal] != undefined) {
      idxColVal = this.inArray(aryData[0], idxCol, true);
    }

    idxColVal = isNaN(idxColVal) ? "-1" : idxColVal;
    idxRowVal = isNaN(idxRowVal) ? "0" : idxRowVal;

    varName = varName.replace(idxCol, idxColVal);
    varName = varName.replace(idxRow, idxRowVal);

    return {varName: varName, idxCol: idxCol, idxColVal: idxColVal, idxRow: idxRow, idxRowVal: idxRowVal};
  };

  this.GetIndexedVariableValueFromVariableContainer = function (varName) {
    varName = varName || "";

    var indexes = varName.match(/\[(.*?)\]/g) || [];

    for (var i = 0; i < indexes.length; i++) {
      indexes[i] = indexes[i].replace("[", "").replace("]", "");
    }

    if (indexes.length > 0) {
      var res = varName.substring(0, varName.indexOf('['));
      for (var m = 0; m < browser.params.config.variableContainer.length; m++) {
        if (browser.params.config.variableContainer[m].Name == res) {
          browser.params.config.variableContainer[m].JsonValue = JSON.parse(browser.params.config.variableContainer[m].Value);

          var aryData = browser.params.config.variableContainer[m].JsonValue;

          var indexs = this.GetIndexesFromVariable(varName, aryData);
          console.log("Final variable", indexs.varName);
          return eval("aryData" + indexs.varName.substring(indexs.varName.indexOf('[')));
        }
      }
    }
    else {
      var res = varName;
      for (var m = 0; m < browser.params.config.variableContainer.length; m++) {
        if (browser.params.config.variableContainer[m].Name == res) {
          return browser.params.config.variableContainer[m].Value;
        }
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
        var keyIndx = 0;
        var mainIndx = 1;
        a[0] = [];
        for (var i = 0; i < resultMessage.Item.length; i++) {
          a[mainIndx] = [];
          var keys = Object.keys(resultMessage.Item[i]);
          for (var j = 0; j < keys.length; j++) {
            a[mainIndx][j] = resultMessage.Item[i][keys[j]];
            if (thisObj.inArray(a[0], keys[j], true) == -1) {
              a[0][keyIndx] = keys[j];
              keyIndx++;
            }
          }
          mainIndx++;
        }
      }
      defer.fulfill(a);
    });

    return defer.promise;
  };

  this.executeAutoIncrement = function (testInstance) {
    var defer = protractor.promise.defer();
    restApiHelper.doPost(browser.params.config.baseApiUrl + browser.params.config.autoincrementUrl, testInstance, function (msg) {
      var resultMessage = JSON.parse(msg.body);
      defer.fulfill(resultMessage.Item);
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
      if (jarry[j] && ((jarry[j] == objectToFind) || (ignoreCase && (jarry[j] + "").toLowerCase() == (objectToFind + "").toLowerCase()))) {
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

    var lastPart = (descriptions.join('-') + timeCode).replace(/[^a-z\d\s-]+/gi, "");

    curTestReportPath = path.join(reportPath, browserDetails, lastPart);
    return curTestReportPath;
  };

  this.gatherDescriptions = function (suite, soFar) {
    soFar.push(suite.description);
    if (suite.parentSuite) {
      return this.gatherDescriptions(suite.parentSuite, soFar);
    } else {
      return soFar;
    }
  };

  this.checkVariableInVariableAndGetValue = function (varName) {
    var variables = varName.match(/\{(.*?)\}/g) || [];
    for (var i = 0; i < variables.length; i++) {
      var processedVariable = variables[i].replace("{", "").replace("}", "");
      var variableValue = this.GetIndexedVariableValueFromVariableContainer(processedVariable);
      varName = varName.replace(variables[i], variableValue);
    }
    return varName;
  };

  this.ExtractVariableValue = function (variableName) {
    var result = undefined;
    var variableContainer = browser.params.config.variableContainer;
    for (var k = 0; k < variableContainer.length; k++) {
      if (variableName.toLowerCase() == variableContainer[k].Name.toLowerCase()) {
        result = variableContainer[k].Value;
        break;
      }
    }
    return result == undefined ? variableName : result;
  };

  this.getIndexInArray = function (arr, indexNameToSearch) {
    var result = undefined;
    for (var i = 0; i < arr.length; i++) {
      if (arr[i] == indexNameToSearch) {
        result = i;
        break;
      }
    }
    return result;
  };

  var keyIndx=0;
  var mainIndx=1;

  this.converToTwoDimensionalArray = function (resultMessage, prepend, a) {
    if (a == undefined) {
      a = [[], []];
      keyIndx=0;
      mainIndx=1;
    }
    var keys = Object.keys(resultMessage);
    for (var j = 0; j < keys.length; j++) {
      var propertName = prepend == undefined ? keys[j] : prepend + keys[j];
      if (typeof(resultMessage[keys[j]]) == 'object' && resultMessage[keys[j]] != null) {
        this.converToTwoDimensionalArray(resultMessage[keys[j]], propertName + ".", a)
      }
      else {
        if (this.inArray(a[0], propertName, true) == -1) {
          a[0][keyIndx] = propertName;
          keyIndx++;
        }
        a[mainIndx][this.getIndexInArray(a[0], propertName)] = resultMessage[keys[j]];
      }
    }
    return a;
  };


  this.replaceVariableWithValue = function (strVariable) {
    if(strVariable == undefined || strVariable == null){
      return strVariable;
    }

    var variables = strVariable.match(/\{(.*?)\}/g) || [];
    if (variables.length > 0) {
      for (var j = 0; j < variables.length; j++) {
        var result = this.GetIndexedVariableValueFromVariableContainer(variables[j].replace('{', '').replace('}', ''));
        strVariable = strVariable.replace(variables[j], result);
      }
    }
    return strVariable;
  };

};
module.exports = JsonHelper;
