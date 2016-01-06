/**
 * Created by vyom.sharma on 05-11-2015.
 */

var JsonHelper = require('./../helpers/JsonHelper.js');
var jsonHelper = new JsonHelper();
var fs = require('fs');
var FS = require("q-io/fs");
var path = require('path');
var RestApiHelper = require('./../helpers/RestApiHelper.js');
var restApiHelper = new RestApiHelper();

function CustomScreenShotReporter(options) {
  console.log("Inside CustomScreenShotReporter base");
}

CustomScreenShotReporter.prototype.reportSpecResults =
  function reportSpecResults(spec, descriptions, results, capabilities) {
    browser.takeScreenshot().then(function (png) {
      browser.getCapabilities().then(function (capabilities) {
        var config = browser.params.config;
        var description = jsonHelper.gatherDescriptions(spec.suite, [spec.description]);
        var curTestReportPath = jsonHelper.buildPath(config.curLocation, description, capabilities);
        var browserDetails = capabilities.caps_.platform + " " + capabilities.caps_.browserName + " " + capabilities.caps_.version;
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
        var itemLength = (spec.results().items_ != undefined ? spec.results().items_.length : 0) - 1;
        var reportData = {
          TestQueueId: config.TestQueueId,
          description: config.testDescription,
          urlTested: browser.params.config.urlToTest,
          ExecutionGroup: config.curLocation,
          screenShotFile: curTestReportPath + ".png",
          ScreenShotArray: config.screenShotArray,
          logContainer: config.logContainer,
          variableStateContainer: config.variableStateContainer,
          passed: spec.results().passed(),
          message: itemLength >= 0 ? spec.results().items_[itemLength].message : undefined,
          trace: itemLength >= 0 ? spec.results().items_[itemLength].trace + "" : undefined,
          traceFull: spec.results().items_,
          finishTime: spec.finishTime,
          finishedAt: spec.finishedAt,
          os: capabilities.caps_.platform,
          browserName: capabilities.caps_.browserName,
          browserVersion: capabilities.caps_.version,
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
          passed: spec.results().passed(),
          message: itemLength >= 0 ? spec.results().items_[itemLength].message : undefined,
          trace: itemLength >= 0 ? spec.results().items_[itemLength].trace + "" : undefined,
          traceFull: spec.results().items_,
          finishTime: spec.finishTime,
          finishedAt: spec.finishedAt,
          os: capabilities.caps_.platform,
          browserName: capabilities.caps_.browserName,
          browserVersion: capabilities.caps_.version,
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

        restApiHelper.doPost(jsonHelper.format(config.baseApiUrl + config.baseTestStateUrl, config.TestQueueId, 4), {}, function () {
          restApiHelper.doPost(jsonHelper.format(config.baseApiUrl + config.baseTestReportUrl), reportData, function () {
          });
        });
      });
    });

  };

module.exports = CustomScreenShotReporter;
