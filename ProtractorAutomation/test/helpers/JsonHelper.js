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

  this.GetIndexesFromVariable = function (varName, aryData) {
    varName = varName || "";

    var indexes = varName.match(/\[(.*?)\]/g) || [];

    console.log("**********Inside GetIndexesFromVariable Indexes:-******************");
    console.log(indexes);

    for(var i = 0; i < indexes.length; i++){
      indexes[i] = indexes[i].replace("[", "").replace("]", "");
    }

    if(aryData == undefined || aryData[0] == undefined){ return undefined; }

    var idxRow = indexes[0] || "0";
    var idxCol = indexes.length > 1 ? indexes[1] : undefined;

    var idxRowVal = idxRow;
    var idxColVal = idxCol;

    if(isNaN(idxRow) && indexes.length == 1){
      var rowIndex = -1;
      var tmpColIndex = -1;
      var searchData = idxRow.split(";");
      var colNameValuePair = [];

      idxCol = "idxCol";
      varName += "[idxCol]";

      for(var i = 0; i < searchData.length; i++){
        var tmpData = searchData[i].split('~');
        colNameValuePair.push({ ColIndex: this.inArray(aryData[0], tmpData[0], true), Value: tmpData[1] });
      }

      var isMatchFound = false;
      for(var i = 0; i < aryData.length; i++){
        isMatchFound = true;
        for(var j = 0; j < colNameValuePair.length; j++){
          if(colNameValuePair[j].Value && colNameValuePair[j].Value.toLowerCase() != aryData[i][colNameValuePair[j].ColIndex].toLowerCase()){
            isMatchFound = false;
            break;
          }
        }

        if(isMatchFound){
          rowIndex = i;
          tmpColIndex = colNameValuePair[colNameValuePair.length - 1].ColIndex;
          break;
        }
      }

      idxRowVal = rowIndex;
      idxColVal = tmpColIndex;

      if(!isNaN(searchData[searchData.length-1])){
        idxColVal = searchData[searchData.length-1];
      }

      if(rowIndex == -1){
        idxRowVal = 0;
        idxColVal = -1;
      }
    }

    if(isNaN(idxRow) && isNaN(idxRowVal) && idxCol && isNaN(idxCol)) {
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

    if(isNaN(idxRow) && isNaN(idxRowVal)) {
      idxRowVal = this.inArray(aryData[0], idxRow, true);
    }

    if(idxCol && isNaN(idxCol) && isNaN(idxColVal) && aryData[idxRowVal] != undefined) {
      idxColVal = this.inArray(aryData[0], idxCol, true);
    }

    idxColVal = isNaN(idxColVal) ? "-1" : idxColVal;
    idxRowVal = isNaN(idxRowVal) ? "0" : idxRowVal;

    varName = varName.replace(idxCol, idxColVal);
    varName = varName.replace(idxRow, idxRowVal);

    return {varName: varName, idxCol: idxCol, idxColVal: idxColVal, idxRow: idxRow, idxRowVal:idxRowVal};
  };

  this.GetIndexedVariableValueFromVariableContainer = function (varName) {
    varName = varName || "";

    var indexes = varName.match(/\[(.*?)\]/g) || [];

    for(var i = 0; i < indexes.length; i++){
      indexes[i] = indexes[i].replace("[", "").replace("]", "");
    }

    if(indexes.length > 0){
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

  this.executeAutoIncrement=function(testInstance){
    var defer = protractor.promise.defer();
    restApiHelper.doPost(browser.params.config.baseApiUrl + browser.params.config.autoincrementUrl , testInstance, function (msg) {
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

    var lastPart = (descriptions.join('-') + timeCode).replace(/[^a-z\d\s-]+/gi, "");

    curTestReportPath = path.join(reportPath, browserDetails, lastPart);
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
