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
        var TakeScreenShot = false;
        var TakeScreenShotBrowser = null;

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
                        restApiHelper.doPost(jsonHelper.format(params.config.baseApiUrl + params.config.baseTestStateUrl, params.config.TestQueueId, 2), {}, function () {
                        });
                        var testDataUrl = jsonHelper.format(params.config.baseApiUrl + params.config.baseTestDataUrl, params.config.TestQueueId);

                        if(params.config.isMobile == 'false') {
                            browser.driver.manage().window().maximize();
                        }

                        restApiHelper.doGet(testDataUrl, function (msg) {
                            var resultMessage = JSON.parse(msg.body);
                            expect(resultMessage.IsError).toEqual(false);
                            params.config.urlToTest = resultMessage.Item.UrlToTest;
                            website = resultMessage.Item.Website;
                            suite = resultMessage.Item.Suite != null ? resultMessage.Item.Suite : {Name: 'Anonymous'};
                            testCase = resultMessage.Item.TestCase;
                            testDataList = resultMessage.Item.TestData;

                            params.config.isCancelled = resultMessage.Item.IsCancelled;
                            params.config.TestSuiteId = resultMessage.Item.Suite != null ? resultMessage.Item.Suite.Id : 0;
                            params.config.TestCaseId = resultMessage.Item.TestCase.Id;
                            TakeScreenShot = resultMessage.Item.TakeScreenShot;
                            TakeScreenShotBrowser = resultMessage.Item.TakeScreenShotBrowser;
                        });
                    });

                    afterAll(function(done){
                        process.nextTick(done);
                    });

                    it("Start test " + params.config.TestQueueId, function () {
                            params.config.testDescription = website.Name + "-" + testCase.TestName;
                            params.config.descriptionArray.push("Start test " + params.config.TestQueueId);
                            params.config.descriptionArray.push('Elephant.com');
                            params.config.descriptionArray.push(testCase.TestName);

                            params.config.urlStack = [];

                            try {
                                browser.ignoreSynchronization = website.IsAngular == false;

                                var nextStatus = 3;
                                if (params.config.isCancelled) {
                                    nextStatus = 5;
                                }
                                restApiHelper.doPost(jsonHelper.format(params.config.baseApiUrl + params.config.baseTestStateUrl, params.config.TestQueueId, nextStatus), {}, function () {
                                });

                                if (params.config.isCancelled) {
                                    expect("ErroMessage:").toEqual("Test execution has been cancelled by the user!")
                                } else {
                                    browser.get(params.config.urlToTest);

                                    for (var i = 0; i < testDataList.length; i++) {
                                        var key = inputHelper.setLocator(testDataList[i], testCase.TestName, TakeScreenShot, TakeScreenShotBrowser, i);
                                    }
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
