/**
 * Created by gurpreet.singh on 04/29/2015.
 */

var RestApiHelper = function()
{
  var authHeader = 'd2luZG93LnNlcnZpY2VAaW5zcG9waW5kaWEuY29tOmVsZXBoYW50QDEyMw==';

  this.doGet = function(url, callBackFunction)
  {
    var optionsGet =  { method: 'GET', url: '', headers: { 'content-type': 'application/json','Authorization':authHeader } };
    optionsGet.url = url;

    var flow = protractor.promise.controlFlow();
    flow.wait(executeRequest(optionsGet)).then(callBackFunction);
  };

  this.doPost = function(url, objectToPost, callBackFunction)
  {
    var optionsPost =  { method: 'POST', url: '', headers: { 'content-type': 'application/json','Authorization':authHeader }, form: {} };
    optionsPost.url = url;
    optionsPost.form = objectToPost;
    var flow = protractor.promise.controlFlow();
    flow.wait(executeRequest(optionsPost)).then(callBackFunction);
  };

  function executeRequest(reqOption) {
    var request = require('request');

    var defer = protractor.promise.defer();

    request(reqOption, function (error, message) {
      if (error || message.statusCode >= 400) {
        console.log("*****12345*****", error);
        console.log(reqOption);
        console.log(message.body);
		    console.log("*****1231112*****");
        defer.reject({ error: error, message: message });
      } else {
        defer.fulfill(message);
      }
    });

    return defer.promise;
  }
};

module.exports = RestApiHelper;
