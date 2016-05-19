/**
 * Created by vyom.sharma on 18-05-2016.
 */


var ApiTestHelper = function () {

  var JsonHelper = require('./JsonHelper.js');
  var jsonHelper = new JsonHelper();
  var thisObj = this;

  var keyIndx = 0;
  var mainIndx = 1;

  this.callApi = function (testInstance, callBackFunction) {
    var apiParameters = {
      method: testInstance.RequestType,
      url: this.evaluateApiUrl(testInstance),
      headers: this.evaluateHeader(testInstance),
      form: testInstance.RequestBody
    };

    console.log("Api Parameters:-");
    console.log(apiParameters);

    var flow = protractor.promise.controlFlow();
    flow.wait(executeRequest(apiParameters)).then(callBackFunction);
  };

  this.evaluateApiUrl = function (testInstance) {
    var evaluatedApiUrl = testInstance.ApiUrl;
    var variables = evaluatedApiUrl.match(/\{(.*?)\}/g) || [];
    if (variables.length > 0) {
      for (var j = 0; j < variables.length; j++) {
        var result = jsonHelper.GetIndexedVariableValueFromVariableContainer(variables[j].replace('{', '').replace('}', ''));
        evaluatedApiUrl = evaluatedApiUrl.replace(variables[j], result);
      }
    }
    return evaluatedApiUrl;
  };

  this.evaluateRequestBody = function (testInstance) {
    var evaluatedRequestBody = testInstance.RequestBody;
    var variables = evaluatedRequestBody.match(/\{(.*?)\}/g) || [];
    if (variables.length > 0) {
      for (var j = 0; j < variables.length; j++) {
        var result = jsonHelper.GetIndexedVariableValueFromVariableContainer(variables[j].replace('{', '').replace('}', ''));
        evaluatedRequestBody = evaluatedRequestBody.replace(variables[j], result);
      }
    }
    return evaluatedRequestBody;
  };

  this.evaluateHeader = function (testInstance) {
    var header = {};
    for (var i = 0; i < testInstance.Headers.length; i++) {
      var IsIgnoreHeader = false;
      for (var k = 0; k < testInstance.IgnoreHeaders.length; k++) {
        if (testInstance.IgnoreHeaders[k].Name == testInstance.Headers[i].Name) {
          IsIgnoreHeader = true;
          break;
        }
      }
      if (IsIgnoreHeader) {
        continue;
      }
      var variables = testInstance.Headers[i].Value.match(/\{(.*?)\}/g) || [];
      if (variables.length > 0) {
        for (var j = 0; j < variables.length; j++) {
          var result = jsonHelper.GetIndexedVariableValueFromVariableContainer(variables[j].replace('{', '').replace('}', ''));
          testInstance.Headers[i].Value = testInstance.Headers[i].Value.replace(variables[j], result);
        }
      }
      header[testInstance.Headers[i].Name] = testInstance.Headers[i].Value;
    }
    return header;
  };

  function executeRequest(reqOption) {
    var request = require('request');

    var defer = protractor.promise.defer();

    request(reqOption, function (error, message) {
      if (error || message.statusCode >= 400) {
        console.log("*****ApiTestHelper*****", error);
        console.log(reqOption);
        console.log(message.body);
        console.log("****ApiTestHelper*****");
        defer.reject({error: error, message: message});
      } else {
        console.log("****ApiTestHelper Fulfilled*****");
        resultMessage = JSON.parse(message.body);
        var response = jsonHelper.converToTwoDimensionalArray(resultMessage);
        console.log("response on Api helper conversion");
        console.log(response);
        defer.fulfill(JSON.stringify(response));
      }
    });

    return defer.promise;
  }


};

module.exports = ApiTestHelper;
