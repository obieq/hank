/**
 * Created by vyom.sharma on 18-05-2016.
 */


var ApiTestHelper = function () {

  var X2JS = require('./xml2js');
  var x2js = new X2JS();

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
        if ((testInstance.IgnoreHeaders[k].Name + "").toLowerCase() == (testInstance.Headers[i].Name + "").toLowerCase()) {
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

  this.customParseJson = function (obj) {
    if (typeof(obj) == 'object') {
      var properties = Object.keys(obj) || [];
      if (properties.length == 0) {
        return obj;
      }
      else if (properties.length == 3 && properties[0] == '__prefix' && properties[1] == '__text' && properties[2] == 'toString') {
        return obj.__text;
      }
      else {
        for (var i = 0; i < properties.length; i++) {
          if (!!obj[properties[i]]) {
            obj[properties[i]] = thisObj.customParseJson(obj[properties[i]])
          }
        }
      }
    }
    return obj;
  };

  function executeRequest(reqOption) {
    var request = require('request');
    var defer = protractor.promise.defer();
    request(reqOption, function (error, message) {
      var resultMessage = {};
      resultMessage.responsestatusCode = message.statusCode;
      if (message == undefined || message == "") {
        message = {body: "No response returned!"};
      }
      if (jsonHelper.isJson(message.body)) {
        resultMessage = JSON.parse(message.body);
        defer.fulfill(JSON.stringify(jsonHelper.converToTwoDimensionalArray(resultMessage)));
      } else if (jsonHelper.isXml(message.body)) {
        var result = x2js.xml_str2json(message.body.toString());
        var parsedResult = thisObj.customParseJson(result);
        defer.fulfill(JSON.stringify(jsonHelper.converToTwoDimensionalArray(parsedResult)));
      } else {
        resultMessage = {response: message.body};
        defer.fulfill(JSON.stringify(jsonHelper.converToTwoDimensionalArray(err)));
      }
    });

    return defer.promise;
  }


};

module.exports = ApiTestHelper;
