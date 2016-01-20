/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('SuiteController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.Website = [];
    $scope.TestList = [];
    $scope.SelectAll = false;

    $scope.message = "";

    $scope.LinkSuiteTestList = [];
    $scope.SuiteList = [];
    $scope.Suite = {WebsiteId: ""};

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
        $scope.bindTest($scope.Suite);
        $scope.getSuiteTestLinks($scope.Suite.Id);
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

    $scope.getSuiteTestLinks = function (suiteId) {
      crudService.getAll(ngAppSettings.SuiteTestMapUrl.format($stateParams.WebsiteId, suiteId)).then(function (response) {
        $scope.LinkSuiteTestList = response;
        $scope.processTestData();
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getSelectedTestCase = function (suiteId) {
      var result = [];
      for (var i = 0; i < $scope.TestList.length; i++) {
        if ($scope.TestList[i].Checked) {
          result.push({TestId: $scope.TestList[i].Id, SuiteId: suiteId});
        }
      }
      return result;
    };

    $scope.bindTest = function (suiteData) {
      if (suiteData.WebsiteId == 0) {
        $scope.TestList = [];
      }
      else {
        crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format(suiteData.WebsiteId, 0)).then(function (response) {
            $scope.TestList = response;
            $scope.SelectAll = false;
            $scope.markUnMarkAll();
            $scope.processTestData();
          },
          function (response) {
            commonUi.showErrorPopup(response);
          });
      }
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
          }
        }
      }
    };

    $scope.markUnMarkAll = function () {
      for (var i = 0; i < $scope.TestList.length; i++) {
        $scope.TestList[i].Checked = $scope.SelectAll;
      }
    };

    $scope.textChecked = function (test) {
      if (!test.Checked) {
        $scope.SelectAll = false;
      }
    };
  }]);
