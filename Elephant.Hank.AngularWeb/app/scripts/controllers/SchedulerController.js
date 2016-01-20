/**
 * Created by vyom.sharma on 20-05-2015.
 */

'use strict';

app.controller('SchedulerController', ['$scope', '$q', '$filter', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $q, $filter, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.SchedulerList = [];
    $scope.Scheduler = {
      Id: 0,
      RecurEvery: 0,
      StopIfLongerThan: 0,
      RepeatTaskFrequency: 0,
      RepeatTaskDuration: 0,
      Settings: {Browsers: []}
    };
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.FrequencyList = ['Onetime', 'Daily', 'Weekly', 'Monthly'];
    $scope.SelectAll = false;
    $scope.SuiteList = [];
    $scope.LinkScheduleSuiteList = [];
    $scope.SchedulerHistory = [];
    $scope.StateParamsWebsiteId = $stateParams.WebsiteId;
    $scope.TestUnderSuite = [];

    dataProvider.currentWebSite($scope);

    $scope.forceExecute = function (id) {
      crudService.search(ngAppSettings.SchedulerForceExecuteUrl.format($stateParams.WebsiteId, id)).then(function (response) {
        commonUi.showMessagePopup("Scheduler has been queued successfully!");
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getAllScheduler = function () {
      crudService.getAll(ngAppSettings.WebSiteSchedulerUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.SchedulerList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getSchedulerHistory = function () {
      crudService.getById(ngAppSettings.SchedulerUrl.format($stateParams.WebsiteId), $stateParams.Id).then(function (response) {
        $scope.Scheduler = response.Item;
        crudService.getAll(ngAppSettings.SchedulerHistoryUrl.format($stateParams.WebsiteId, $stateParams.Id)).then(function (response) {
          $scope.SchedulerHistory = response;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addScheduler = function () {
      $scope.Scheduler.WebsiteId = $stateParams.WebsiteId;
      $scope.Scheduler.Settings.Browsers = $scope.getSelectedBrowsers();
      var x = $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedForTest;
      if (x != null && x != undefined) {
        $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedTestId = x.TestId;
        $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedSuiteId = x.SuiteId;
      }
      crudService.add(ngAppSettings.SchedulerUrl.format($stateParams.WebsiteId), $scope.Scheduler).then(function (response) {
        $scope.addSchedulerSuiteLinks(response.Item.Id, true);
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getSchedulerById = function () {
      $scope.loadData();
      crudService.getById(ngAppSettings.SchedulerUrl.format($stateParams.WebsiteId), $stateParams.Id).then(function (response) {
        $scope.Scheduler = response.Item;
        response.Item.Settings = response.Item.Settings == null ? $scope.Scheduler.Settings : response.Item.Settings;
        $scope.Scheduler.StartDateTime = $scope.Scheduler.StartDateTime.dateFormat($filter);
        $scope.Scheduler.ExpirationDateTime = ($scope.Scheduler.ExpirationDateTime + "").dateFormat($filter);
        $scope.loadBrowsers().then(function () {
          $scope.bindSuite($scope.Scheduler).then(function () {
            $scope.getSchedulerSuiteLinks($scope.Scheduler.Id);
          });
        });

      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.updateScheduler = function () {
      $scope.Scheduler.Settings.Browsers = $scope.getSelectedBrowsers();
      var x = $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedForTest;
      if (x != null && x != undefined) {
        $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedTestId = x.TestId;
        $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedSuiteId = x.SuiteId;
      }
      crudService.update(ngAppSettings.SchedulerUrl.format($stateParams.WebsiteId), $scope.Scheduler).then(function (response) {
        $scope.addSchedulerSuiteLinks(response.Item.Id, true);
        //$state.go("Website.Scheduler",{ WebsiteId: $scope.stateParamWebsiteId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });

    };

    $scope.loadData = function () {
      dataProvider.currentWebSite($scope);
      $scope.getWebsiteAllSuites();
    };

    $scope.onLoadAdd = function () {
      crudService.getById(ngAppSettings.WebSiteUrl, $scope.stateParamWebsiteId).then(function (response) {
        $scope.Website = response.Item;
        $scope.Scheduler.Settings.SeleniumAddress = $scope.Website.Settings.SeleniumAddress;
        crudService.getAll(ngAppSettings.BrowserUrl).then(function (response) {
          $scope.BrowserList = response;
          for (var i = 0; i < $scope.BrowserList.length; i++) {
            for (var j = 0; j < $scope.Website.Settings.Browsers.length; j++) {
              if ($scope.Website.Settings.Browsers[j] == $scope.BrowserList[i].Id) {
                $scope.BrowserList[i].Checked = true;
              }
            }
          }
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
        $scope.getWebsiteAllSuites();
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getWebsiteAllSuites = function () {
      crudService.getAll(ngAppSettings.WebSiteSuiteUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.SuiteList = response;

      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };


    $scope.addSchedulerSuiteLinks = function (schedulerId, doRedirect) {
      var mapData = $scope.getSelectedSuite(schedulerId);

      crudService.add(ngAppSettings.SchedulerSuiteUrl.format($stateParams.WebsiteId, schedulerId), mapData).then(function () {
        if (doRedirect) {
          $state.go("Website.Scheduler", {WebsiteId: $scope.stateParamWebsiteId});
        }
        else {
          $scope.message = "Data has been saved successfully!";
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.markUnMarkAll = function () {
      var suiteIdListToSend = '';
      for (var r = 0; r < $scope.SuiteList.length; r++) {
        $scope.SuiteList[r].Checked = $scope.SelectAll;
        if ($scope.SuiteList[r].Checked) {
          suiteIdListToSend = suiteIdListToSend.split(',').length == 0 ? $scope.SuiteList[r].Id.toString() : suiteIdListToSend + ',' + $scope.SuiteList[r].Id;
        }
      }
      if (suiteIdListToSend != '') {
        crudService.getAll(ngAppSettings.SuiteTestMapBySuiteIdListUrl.format($stateParams.WebsiteId, suiteIdListToSend)).then(function (response) {
          for (var k = 0; k < response.length; k++) {
            var checkEntryAddToDDL = _.where($scope.TestUnderSuite, {
              'SuiteId': response[k].SuiteId,
              'TestId': response[k].TestId
            });
            if (checkEntryAddToDDL.length == 0) {
              $scope.TestUnderSuite.push(response[k]);
            }
          }
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }

    };

    $scope.textChecked = function (suite) {

      if (!suite.Checked) {
        $scope.SelectAll = false;
        _.remove($scope.TestUnderSuite, {
          SuiteId: suite.Id
        });
      }
      else {
        crudService.getAll(ngAppSettings.SuiteTestMapUrl.format($stateParams.WebsiteId, suite.Id)).then(function (response) {

          for (var k = 0; k < response.length; k++) {
            $scope.TestUnderSuite.push(response[k]);
          }
          console.log($scope.TestUnderSuite);
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.getSelectedSuite = function (schedulerId) {
      var result = [];
      for (var i = 0; i < $scope.SuiteList.length; i++) {
        if ($scope.SuiteList[i].Checked) {
          result.push({SuiteId: $scope.SuiteList[i].Id, SchedulerId: schedulerId});
        }
      }
      return result;
    };

    $scope.bindSuite = function (scheduleData) {
      var deferred = $q.defer();
      if (scheduleData.WebsiteId == 0) {
        $scope.SuiteList = [];
        deferred.resolve();
      }
      else {
        if ($scope.SuiteList.length == 0) {
          crudService.getAll(ngAppSettings.WebSiteSuiteUrl.format(scheduleData.WebsiteId)).then(function (response) {
              $scope.SuiteList = response;
              $scope.SelectAll = false;
              $scope.markUnMarkAll();
              deferred.resolve();
            },
            function (response) {
              commonUi.showErrorPopup(response);
              deferred.reject();
            });
        }
        else {
          $scope.SelectAll = false;
          $scope.markUnMarkAll();
          deferred.resolve();
          //$scope.processTestData();
        }
      }
      return deferred.promise;
    };

    $scope.processTestData = function () {
      for (var i = 0; i < $scope.LinkScheduleSuiteList.length; i++) {
        for (var j = 0; j < $scope.SuiteList.length; j++) {
          if ($scope.SuiteList[j].Id == $scope.LinkScheduleSuiteList[i].SuiteId) {
            $scope.SuiteList[j].Checked = true;
          }
        }
      }
      $scope.bindScreenShotForTestDDL();
    };

    $scope.getSchedulerSuiteLinks = function (schedulerId) {
      crudService.getAll(ngAppSettings.SchedulerSuiteUrl.format($stateParams.WebsiteId, schedulerId)).then(function (response) {
        $scope.LinkScheduleSuiteList = response;
        $scope.processTestData();
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.markUnMarkAllBrowser = function (browserobj) {
      if (browserobj) {
        $scope.SelectAllBrowsers = $scope.SelectAllBrowsers && browserobj.Checked;
        return;
      }
      for (var i = 0; i < $scope.BrowserList.length; i++) {
        $scope.BrowserList[i].Checked = $scope.SelectAllBrowsers;
      }
    };

    $scope.getSelectedBrowsers = function () {
      var result = [];
      for (var i = 0; i < $scope.BrowserList.length; i++) {
        if ($scope.BrowserList[i].Checked) {
          result.push($scope.BrowserList[i].Id);
        }
      }
      return result;
    };

    $scope.loadBrowsers = function () {
      var deferred = $q.defer();
      crudService.getAll(ngAppSettings.BrowserUrl).then(function (response) {
        $scope.BrowserList = response;
        $scope.SelectAllBrowsers = $scope.Scheduler.Id <= 0 ? true : $scope.SelectAllBrowsers;
        if ($scope.SelectAllBrowsers) {
          $scope.markUnMarkAllBrowser();
        }
        for (var i = 0; i < $scope.Scheduler.Settings.Browsers.length; i++) {
          for (var j = 0; j < $scope.BrowserList.length; j++) {
            if ($scope.Scheduler.Settings.Browsers[i] == $scope.BrowserList[j].Id) {
              $scope.BrowserList[j].Checked = true;
              break;
            }
          }
        }
        deferred.resolve();
      }, function (response) {
        commonUi.showErrorPopup(response);
        deferred.reject();
      });
      return deferred.promise;
    };


    $scope.getAll = function () {
      var deferred = $q.defer();
      for (var l = 0; l < $scope.SuiteList.length; l++) {
        if ($scope.SuiteList[l].Checked) {
          crudService.getAll(ngAppSettings.SuiteTestMapUrl.format($stateParams.WebsiteId, suite.Id)).then(function (response) {
            for (var k = 0; k < response.length; k++) {
              var testAlreadyExist = _.where($scope.TestUnderSuite, {TestId: response[k].TestId})[0];
              if (testAlreadyExist == undefined || testAlreadyExist == null) {
                $scope.TestUnderSuite.push(response[k]);
              }
            }
            console.log($scope.TestUnderSuite);
          }, function (response) {
            commonUi.showErrorPopup(response);
            deferred.reject();
          });
        }
      }

      return deferred.promise;
    };

    $scope.bindScreenShotForTestDDL = function () {
      console.log("inside bindScreenShotForTestDDL");
      var deferred = $q.defer();
      var promises = [];
      var suiteIdListToSend = '';
      for (var r = 0; r < $scope.SuiteList.length; r++) {
        if ($scope.SuiteList[r].Checked) {
          suiteIdListToSend = suiteIdListToSend.split(',').length == 0 ? $scope.SuiteList[r].Id.toString() : suiteIdListToSend + ',' + $scope.SuiteList[r].Id;
        }
      }

      if (suiteIdListToSend != '') {
        promises.push(crudService.getAll(ngAppSettings.SuiteTestMapBySuiteIdListUrl.format($stateParams.WebsiteId,suiteIdListToSend)).then(function (response) {
          for (var k = 0; k < response.length; k++) {
            var checkEntryAddToDDL = _.where($scope.TestUnderSuite, {
              'SuiteId': response[k].SuiteId,
              'TestId': response[k].TestId
            });
            if (checkEntryAddToDDL.length == 0) {
              $scope.TestUnderSuite.push(response[k]);
            }
          }
        }, function (response) {
          commonUi.showErrorPopup(response);
          deferred.reject();
        }));
      }
      $q.all(promises).then(function (data) {

        var e = $scope.TestUnderSuite;
        $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedForTest = _.where($scope.TestUnderSuite, {
          'TestId': $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedTestId,
          'SuiteId': $scope.Scheduler.Settings.TakeScreenShotOnUrlChangedSuiteId
        })[0];
        deferred.resolve();
      });
      return deferred.promise;
    };

  }
])
;
