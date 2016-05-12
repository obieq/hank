// ---------------------------------------------------------------------------------------------------
// <copyright file="protractor.conf.js" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-10</date>
// <summary>
//     The protractor.conf template
// </summary>
// ---------------------------------------------------------------------------------------------------

var reportPath = "";
var curTestReportPath = "";
var urlToTest = "";
var CustomScreenShotReporter = require('./../../Reporter/CustomScreenShotReporter.js');
var path = require('path');
var RestApiHelper = require('./../../helpers/RestApiHelper.js');
var restApiHelper = new RestApiHelper();
var JsonHelper = require('./../../helpers/JsonHelper.js');
var jsonHelper = new JsonHelper();
var Constant = require('./../../constants/constant.js');
var constant = new Constant();


exports.config =
{
    seleniumAddress: 'http://10.140.2.163:4444/wd/hub',

    params:
    {
        config:
        {
            curLocation: '27-04-2016-16-01-25-27',
            baseApiUrl: 'http://localhost:26264/',
            baseTestDataUrl: 'api/website/0/test-queue/{0}/exe-test-data',
            baseTestStateUrl: 'api/website/0/test-queue/{0}/test-state/{1}',
            baseSchedulerHistoryStatusUrl: 'api/website/0/scheduler/0/scheduler-history/status/{0}/{1}',
            baseTestReportUrl: 'api/website/0/report',
            executeSqlUrl:'api/execute-sql/{0}',
            autoincrementUrl: "api/website/0/test-queue/auto-increment",
            logContainer: [],
            variableContainer:[],
            variableStateContainer: [],
            descriptionArray:[],
            screenShotArray:[]
        }
    },

    multiCapabilities: [


{
    platform: "WINDOWS",
        browserName: "internet explorer",
    version: '9',
    shardTestFiles: true,
    maxInstances: 3 // Use number of instances you want
}

],

specs:
    [
        'spec/va/*-1-*.js'
    ],

        jasmineNodeOpts: {
    onComplete: null,
        isVerbose: true,
        showColors: true,
        includeStackTrace: true,
        defaultTimeoutInterval: 1200000
},

allScriptsTimeout: 4600000,

    onPrepare: function () {
    require('./../../helpers/WaitReady.js');
    browser.driver.manage().window().maximize();
    reportPath = browser.params.config.curLocation;
    urlToTest = browser.params.config.urlToTest;
    jasmine.getEnv().addReporter(new CustomScreenShotReporter({

    }));
}
};

this.GetNumberToString = function (number) {
    var value = number.toString();
    return (value.length > 1 ? "" : "0") + value;
};