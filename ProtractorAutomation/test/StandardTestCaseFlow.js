/**
 * Created by vyom.sharma on 10-02-2015.
 */
'use strict';

var StandardTestCaseFlow =
  function () {
    var RestApiHelper = require('./helpers/RestApiHelper.js');
    var website = null;
    var suite = null;
    var testCase = null;
    var testDataList = null;
    var TakeScreenShot=false;
    var TakeScreenShotBrowser=null;

    var InputHelper = require('./helpers/InputHelper.js');
    var inputHelper = new InputHelper();
    var JsonHelper = require('./helpers/JsonHelper.js');
    var jsonHelper = new JsonHelper();

    var params = browser.params;

    this.ProcessCustomConfig = function (customConfig) {
      customConfig.urlToTest = params.config.urlToTest != '' ? params.config.urlToTest : customConfig.urlToTest;
      customConfig.testToExecute = jsonHelper.parseJsonArrays(params.config.testToExecute);
      customConfig.dotNetApiDomainUrl = params.config.dotNetApiDomainUrl != '' ? params.config.dotNetApiDomainUrl : customConfig.dotNetApiDomainUrl;
    };

    this.ExecuteTest = function (customConfig) {
      this.ProcessCustomConfig(customConfig);
      if (customConfig.testToExecute.length != 0 && jsonHelper.inArray(customConfig.testToExecute, customConfig.testName, true) == -1) {
        return;
      }
      params.config.TestQueueId = customConfig.TestQueueId;

      describe('Elephant.com', function () {
          var restApiHelper = new RestApiHelper();

          beforeEach(function () {
            var testDataUrl = jsonHelper.format(params.config.baseApiUrl + params.config.baseTestDataUrl, params.config.TestQueueId);

            restApiHelper.doGet(testDataUrl, function (msg) {
              console.log("console1");
              console.log(msg);
              var resultMessage = JSON.parse(msg.body);
              expect(resultMessage.IsError).toEqual(false);
              params.config.urlToTest = resultMessage.Item.UrlToTest;
              website = resultMessage.Item.Website;
              suite = resultMessage.Item.Suite != null ? resultMessage.Item.Suite : {Name: 'Anonymous'};
              testCase = resultMessage.Item.TestCase;
              testDataList = resultMessage.Item.TestData;
              params.config.TestSuiteId = resultMessage.Item.Suite != null ? resultMessage.Item.Suite.Id : 0;
              params.config.TestCaseId = resultMessage.Item.TestCase.Id;
              TakeScreenShot=resultMessage.Item.TakeScreenShot;
              TakeScreenShotBrowser=resultMessage.Item.TakeScreenShotBrowser;
            });
          });

          it("Start test " + params.config.TestQueueId, function () {
              params.config.testDescription = website.Name + "-" + testCase.TestName;
              params.config.descriptionArray.push("Start test " + params.config.TestQueueId);
              params.config.descriptionArray.push('Elephant.com');
              params.config.descriptionArray.push(testCase.TestName);
              try {
                if (website.IsAngular == false) {
                  browser.ignoreSynchronization = true;
                }

                restApiHelper.doPost(jsonHelper.format(params.config.baseApiUrl + params.config.baseTestStateUrl, params.config.TestQueueId, 3), {}, function () {
                });

                browser.get(params.config.urlToTest);

                for (var i = 0; i < testDataList.length; i++) {
                  var key = inputHelper.setLocator(testDataList[i], testCase.TestName,TakeScreenShot,TakeScreenShotBrowser);
                }
              }
              catch (err) {
                expect("ErroMessage:").toEqual(err.message)
              }
            }
          );
        }
      );
    };
  };

module.exports = StandardTestCaseFlow;
