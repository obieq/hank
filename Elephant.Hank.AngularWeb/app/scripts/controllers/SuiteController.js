/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('SuiteController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.Suites);
    $scope.Website = [];
    $scope.TestList = [];
    $scope.TestListAdded = [];
    $scope.SelectAll = false;
    $scope.ShiftDirection = {NotAddedToToBeAdded: 1, ToBeAddedToNotAdded: 2};

    $scope.message = "";

    $scope.LinkSuiteTestList = [];
    $scope.SuiteList = [];
    $scope.Suite = {WebsiteId: ""};
    $scope.TestCatList = [];
    $scope.markUnMark = false;

    crudService.getAll(ngAppSettings.WebSiteTestCatUrl.format($stateParams.WebsiteId)).then(function (response) {
      $scope.TestCatList = response;
      $scope.TestCatList.push({"Id": 0, "Name": "--All--"})
    }, function (response) {
    });

    $scope.stateParamWebsiteId = $stateParams.WebsiteId;

    dataProvider.currentWebSite($scope);

    $scope.getAllSuites = function () {
      crudService.getAll(ngAppSettings.WebSiteSuiteUrl.format($scope.stateParamWebsiteId)).then(function (response) {
        $scope.SuiteList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getSuiteById = function () {
      crudService.getById(ngAppSettings.WebSiteSuiteUrl.format($stateParams.WebsiteId), $stateParams.SuiteId).then(function (response) {
        $scope.Suite = response.Item;
        crudService.getAll(ngAppSettings.SuiteTestMapUrl.format($stateParams.WebsiteId, $stateParams.SuiteId)).then(function (response) {
          $scope.LinkSuiteTestList = response;
          crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format($stateParams.WebsiteId, 0)).then(function (response) {
              $scope.TestList = response;
              $scope.processTestData();
            },
            function (response) {
              commonUi.showErrorPopup(response);
            });
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addSuite = function () {
      $scope.Suite.WebsiteId = $scope.Website.Id;
      crudService.add(ngAppSettings.WebSiteSuiteUrl.format($stateParams.WebsiteId), $scope.Suite).then(function (response) {
        $scope.addSuiteTestLinks(response.Item.Id, true);
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.updateSuite = function () {
      crudService.update(ngAppSettings.WebSiteSuiteUrl.format($stateParams.WebsiteId), $scope.Suite).then(function (response) {
        $scope.addSuiteTestLinks(response.Item.Id, false);
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addSuiteTestLinks = function (suiteId, doRedirect) {
      var mapData = $scope.getSelectedTestCase(suiteId);
      crudService.add(ngAppSettings.SuiteTestMapUrl.format($stateParams.WebsiteId, suiteId), mapData).then(function () {
        if (doRedirect) {
          $state.go("Website.Suite", {WebsiteId: $scope.stateParamWebsiteId});
        }
        else {
          $scope.message = "Data has been saved successfully!";
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getSelectedTestCase = function (suiteId) {
      var result = [];
      for (var i = 0; i < $scope.TestListAdded.length; i++) {
        result.push({TestId: $scope.TestListAdded[i].Id, SuiteId: suiteId});
      }
      return result;
    };

    $scope.addRemoveTest = function (test, direction) {
      $scope.shift(test, direction);
    };

    $scope.loadData = function () {
      crudService.getById(ngAppSettings.WebSiteUrl, $stateParams.WebsiteId).then(function (response) {
        $scope.Website = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.processTestData = function () {
      for (var i = 0; i < $scope.LinkSuiteTestList.length; i++) {
        for (var j = 0; j < $scope.TestList.length; j++) {
          if ($scope.TestList[j].Id == $scope.LinkSuiteTestList[i].TestId) {
            $scope.TestList[j].Checked = true;
            $scope.shift($scope.TestList[j], $scope.ShiftDirection.NotAddedToToBeAdded);
          }
        }
      }
    };

    $scope.markUnMarkAll = function () {
      if ($scope.markUnMark) {
        var tstLst = angular.copy($scope.TestList);
        for (var i = 0; i < tstLst.length; i++) {
          tstLst[i].Checked = true;
          $scope.addRemoveTest(tstLst[i], $scope.ShiftDirection.NotAddedToToBeAdded);
        }
      }
      else {
        var tstLst = angular.copy($scope.TestListAdded);
        for (var i = 0; i < tstLst.length; i++) {
          tstLst[i].Checked = false;
          $scope.addRemoveTest(tstLst[i], $scope.ShiftDirection.ToBeAddedToNotAdded);
        }
      }

    };

    $scope.textChecked = function (test) {
      if (!test.Checked) {
        $scope.SelectAll = false;
      }
    };

    $scope.onTestCategoryChange = function () {
      if ($scope.Suite.TestCatId == 0) {
        crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format($stateParams.WebsiteId, 0)).then(function (response) {
          $scope.TestList = [];
          for (var i = 0; i < response.length; i++) {
            var inAdded = _.where($scope.TestListAdded, {Id: response[i].Id})[0];
            if (!inAdded) {
              $scope.TestList.push(response[i]);
            }
          }
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
      else {
        crudService.getAll(ngAppSettings.TestCatTestScriptsUrl.format($stateParams.WebsiteId, $scope.Suite.TestCatId)).then(function (response) {
          $scope.TestList = [];
          for (var i = 0; i < response.length; i++) {
            var inAdded = _.where($scope.TestListAdded, {Id: response[i].Id})[0];
            if (!inAdded) {
              $scope.TestList.push(response[i])
            }
          }
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.shift = function (test, shiftDirection) {
      if (shiftDirection == $scope.ShiftDirection.NotAddedToToBeAdded) {
        var alreadyExist = _.where($scope.TestListAdded, {Id: test.Id})[0];
        if (!alreadyExist) {
          $scope.TestListAdded.push(test);
        }
        for (var i = 0; i < $scope.TestList.length; i++) {
          if ($scope.TestList[i].Id == test.Id) {
            var removedObject = $scope.TestList.splice(i, 1);
            removedObject = null;
            break;
          }
        }
      }
      else if (shiftDirection == $scope.ShiftDirection.ToBeAddedToNotAdded) {
        var alreadyExist = _.where($scope.TestList, {Id: test.Id})[0];
        if (!alreadyExist) {
          $scope.TestList.push(test);
        }
        for (var i = 0; i < $scope.TestListAdded.length; i++) {
          if ($scope.TestListAdded[i].Id == test.Id) {
            var removedObject = $scope.TestListAdded.splice(i, 1);
            removedObject = null;
            break;
          }
        }
      }
    }
  }]);
