/**
 * Created by vyom.sharma on 18-05-2016.
 */


var ApiTestHelper = function () {

  var parser = require('xml2json');

  var JsonHelper = require('./JsonHelper.js');
  var jsonHelper = new JsonHelper();
  var thisObj = this;

  var keyIndx = 0;
  var mainIndx = 1;

  this.callApi = function (testInstance, callBackFunction) {
    var apiParameters = {
      method: testInstance.RequestType,
      url: jsonHelper.replaceVariableWithValue(testInstance.ApiUrl),
      headers: this.evaluateHeader(testInstance),
      body: jsonHelper.replaceVariableWithValue(testInstance.RequestBody)
    };

    var flow = protractor.promise.controlFlow();
    flow.wait(executeRequest(apiParameters)).then(callBackFunction);
  };

  this.evaluateHeader = function (testInstance) {
    var header = {};
    for (var i = 0; i < testInstance.Headers.length; i++) {
      var IsIgnoreHeader = false;
      for (var k = 0; k < testInstance.IgnoreHeaders.length; k++) {
        if ((testInstance.IgnoreHeaders[k].Name + "").toLowerCase() == (testInstance.Headers[i].Name+ "").toLowerCase()) {
          IsIgnoreHeader = true;
          break;
        }
      }
      if (IsIgnoreHeader) {
        continue;
      }
      header[testInstance.Headers[i].Name] = jsonHelper.replaceVariableWithValue(testInstance.Headers[i].Value);
    }
    return header;
  };

  function executeRequest(reqOption) {
    var request = require('request');

    var defer = protractor.promise.defer();

    request(reqOption, function (error, message) {
      var resultMessage = null;
      if(message == undefined || message == ""){
        message = { body: "No response returned!" }
      }
      if(jsonHelper.isJson(message.body)){
        resultMessage = JSON.parse(message.body);
      }  else if(jsonHelper.isXml(message.body)){
        resultMessage = parser.toJson(message.body, { object: true, reversible: false, sanitize: true, coerce: false });
      } else {
        resultMessage = { response: message.body }
      }

      resultMessage.responsestatusCode = message.statusCode;

      var response = jsonHelper.converToTwoDimensionalArray(resultMessage);
      defer.fulfill(JSON.stringify(response));
    });

    return defer.promise;
  }


};

module.exports = ApiTestHelper;
