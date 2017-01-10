/**
 * Created by vyom.sharma on 05-11-2015.
 */

var JsonHelper = require('./../helpers/JsonHelper.js');
var jsonHelper = new JsonHelper();
var Promise = require('promise');
var fs = require('fs');
var FS = require("q-io/fs");
var path = require('path');
var RestApiHelper = require('./../helpers/RestApiHelper.js');
var restApiHelper = new RestApiHelper();
var jasmineCore = require('jasmine-core');
var jasmine = jasmineCore.boot(jasmineCore);
var timer;

function CustomScreenShotReporter(options) {


  this.jasmineStarted = function () {
     timer = new jasmine.Timer();
     timer.start();
  };

    this.specDone = function (result) {
        Promise.all([
            browser.takeScreenshot(),
            browser.getCapabilities()
        ]).then(function (values) {
            var png = values[0];
            var capabilities = values[1];

            var config = browser.params.config;
            var description = jsonHelper.gatherDescriptions(result.fullName);
            var curTestReportPath = jsonHelper.buildPath(config.curLocation, description, capabilities);
            var browserDetails = capabilities.get('platform') + " " + capabilities.get('browserName') + " " + capabilities.get('version');
            var pathtoCheck = path.join("reports", config.curLocation, browserDetails);
            FS.isDirectory(pathtoCheck).then(function (IsExist) {
                if (IsExist) {
                    fs.writeFileSync('reports\\' + curTestReportPath + '.png', png, {encoding: 'base64'});
                }
                else {
                    FS.makeTree(pathtoCheck).then(function () {
                        fs.writeFileSync('reports\\' + curTestReportPath + '.png', png, {encoding: 'base64'});
                    });
                }
            });
            var specResults = result.passedExpectations.concat(result.failedExpectations);
            var itemLength = (specResults != undefined ? specResults.length : 0) - 1;

            var reportData = {
                TestQueueId: config.TestQueueId,
                description: config.testDescription,
                urlTested: browser.params.config.urlToTest,
                ExecutionGroup: config.curLocation,
                screenShotFile: curTestReportPath + ".png",
                ScreenShotArray: config.screenShotArray,
                logContainer: config.logContainer,
                variableStateContainer: config.variableStateContainer,
                status: config.isCancelled ? 5 : (result.status == 'passed' ? 8 : 9),
                passed: result.status == 'passed',
                message: itemLength >= 0 ? specResults[itemLength].message : undefined,
                trace: itemLength >= 0 ? specResults[itemLength].message + "" : undefined,
                traceFull: specResults,
                finishTime: timer.elapsed() ,
                finishedAt: new Date(),
                os: capabilities.get('platform'),
                browserName: capabilities.get('browserName'),
                browserVersion: capabilities.get('version'),
                ReportSource: "HanksTestSystem",
                LastStepExecuted: config.LastStepExecuted
            };

            var reportDataJson = {
                TestQueueId: config.TestQueueId,
                description: config.testDescription,
                urlTested: browser.params.config.urlToTest,
                ExecutionGroup: config.curLocation,
                screenShotFile: curTestReportPath + ".png",
                ScreenShotArray: config.screenShotArray,
                logContainer: config.logContainer,
                variableStateContainer: config.variableStateContainer,
                status: config.isCancelled ? 5 : (result.status == 'passed' ? 8 : 9),
                passed: result.status == 'passed',
                message: itemLength >= 0 ? specResults[itemLength].message : undefined,
                trace: itemLength >= 0 ? specResults[itemLength].message + "" : undefined,
                traceFull: specResults,
                finishTime: timer.elapsed(),
                finishedAt: new Date(),
                os: capabilities.get('platform'),
                browserName: capabilities.get('browserName'),
                browserVersion: capabilities.get('version'),
                ReportSource: "WindowService",
                LastStepExecuted: config.LastStepExecuted
            };


            var jsonPathtoCheck = path.join("reports", config.curLocation, 'JSON');
            FS.isDirectory(jsonPathtoCheck).then(function (IsExist) {
                var operatingSystem = reportDataJson.os.toLowerCase() == "xp" ? 'windows' : reportDataJson.os;
                if (IsExist) {
                    fs.writeFileSync(jsonPathtoCheck + '\\' + config.TestQueueId + '-' + operatingSystem + '-' + reportDataJson.browserName + '.json', JSON.stringify(reportDataJson));
                }
                else {
                    FS.makeTree(jsonPathtoCheck).then(function () {
                        fs.writeFileSync(jsonPathtoCheck + '\\' + config.TestQueueId + '-' + operatingSystem + '-' + reportDataJson.browserName + '.json', JSON.stringify(reportDataJson));
                    });
                }
            });

            restApiHelper.doPost(jsonHelper.format(config.baseApiUrl + config.baseTestReportUrl), reportData, function () {

            }, function () {
                console.log("Inside Reported: Stage 6");
                console.log(reportData);
            });
        });
    };

}


CustomScreenShotReporter.prototype.suiteDone = function (result) {
    console.log("Inside suiteDone");
};

CustomScreenShotReporter.prototype.jasmineDone = function (result) {
    console.log("Inside jasmineDone");
};

module.exports = CustomScreenShotReporter;
