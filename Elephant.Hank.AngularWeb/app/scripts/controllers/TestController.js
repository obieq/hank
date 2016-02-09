/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('TestController', ['$scope', '$rootScope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider','authService',
  function ($scope, $rootScope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider,authService) {

    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.TestScripts);
    var authData = authService.getAuthData();

    $scope.LoggeddInUserName=authData.userName;

    $scope.testAccessStatusList = [{
      Id: 1,
      Name: 'Public'
    },{
        Id: 2,
        Name: 'Read Only'
      }, {
        Id: 3,
        Name: 'Private'
      }
    ];
    $scope.TestList = [];
    $scope.CopyTestData = {};
    $scope.CopyTestData.HasTestData = false;
    $scope.CopyTestData.DataToSend = {};
    $scope.CopyTestData.DataToSend.TestDataIdList = [];
    $scope.Test = {};
    $scope.TestQueue = {};
    $scope.Website = [];
    $scope.TestCat = [];

    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.TestCatId = $stateParams.TestCatId;
    $scope.TestCatList = [];

    if ($scope.TestCatId > 0) {
      $scope.Test.CategoryId = $scope.TestCatId;
    }

    $scope.BrowserList = [];
    $scope.Step_Types = ngAppSettings.StepTypes;

    $scope.getAllTests = function () {
      crudService.getAll(ngAppSettings.TestCatTestScriptsUrl.format($stateParams.WebsiteId, $scope.TestCatId)).then(function (response) {
        $scope.TestList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getAllTestsIgnoreCat = function () {
      crudService.getAll(ngAppSettings.TestCatTestScriptsUrl.format($stateParams.WebsiteId, 0)).then(function (response) {
        $scope.TestList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getTestById = function () {
      crudService.getById(ngAppSettings.TestUrl.format($stateParams.WebsiteId, $stateParams.TestCatId), $stateParams.TestId).then(function (response) {
        $scope.Test = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.updateTest = function () {
      crudService.update(ngAppSettings.TestUrl.format($stateParams.WebsiteId, $stateParams.TestCatId), $scope.Test).then(function (response) {
        $state.go("Website.TestCatTest", {WebsiteId: $scope.stateParamWebsiteId, TestCatId: $scope.TestCatId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addTest = function () {

      $scope.Test.WebsiteId = $scope.stateParamWebsiteId;
      crudService.add(ngAppSettings.TestUrl.format($stateParams.WebsiteId, $stateParams.TestCatId), $scope.Test).then(function (response) {

        var j = 1;
        if ($scope.CopyTestData.HasTestData && $scope.CopyTestData.IsCopy && $scope.CopyTestData.Test != null) {
          $scope.CopyTestData.DataToSend.ToTestId = response.Item.Id;
          $scope.CopyTestData.DataToSend.FromTestId = $scope.CopyTestData.Test.Id;
          $scope.CopyTestData.DataToSend.CopyAll = $scope.CopyTestData.AllTestDataChecked;
          for (var i = 0; i < $scope.CopyTestData.TestDataList.length; i++) {
            if ($scope.CopyTestData.TestDataList[i].Checked == true) {
              $scope.CopyTestData.DataToSend.TestDataIdList.push($scope.CopyTestData.TestDataList[i].Id);
            }
          }
          crudService.add(ngAppSettings.TestDataListAddUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId), $scope.CopyTestData.DataToSend).then(function (response) {
            $state.go("Website.TestCatTest", {WebsiteId: $scope.stateParamWebsiteId, TestCatId: $scope.TestCatId});
          }, function (response) {

            commonUi.showErrorPopup(response);
          });
        }
        else {
          $state.go("Website.TestCatTest", {WebsiteId: $scope.stateParamWebsiteId, TestCatId: $scope.TestCatId});
        }
      }, function (response) {

        commonUi.showErrorPopup(response);
      });
    };

    $scope.onQueueClick = function (id) {
      $rootScope.$broadcast('openTestQueuePopup', {'TestId': id, 'WebsiteId': $stateParams.WebsiteId});
    };


    $scope.loadData = function (ignoreCatListLoad) {
      ignoreCatListLoad = ignoreCatListLoad == undefined ? false : true;

      dataProvider.currentWebSite($scope);
      dataProvider.currentTestCat($scope);

      if (ignoreCatListLoad == false) {
        crudService.getAll(ngAppSettings.WebSiteTestCatUrl.format($stateParams.WebsiteId)).then(function (response) {
          $scope.TestCatList = response;
        }, function (response) {
        });
      }
    };

    $scope.loadTestData = function () {

      crudService.getAll(ngAppSettings.TestDataAllByTestIdUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $scope.CopyTestData.Test.Id)).then(function (response) {

        $scope.CopyTestData.HasTestData = true;
        $scope.CopyTestData.TestDataList = response;

        for (var i = 0; i < $scope.CopyTestData.TestDataList.length; i++) {
          if ($scope.CopyTestData.TestDataList[i].LinkTestType == $scope.Step_Types.SharedTestStep) {
            $scope.CopyTestData.TestDataList[i].rowStyle = 'background-color: #dcdcdc;';
          }
          else if ($scope.CopyTestData.TestDataList[i].LinkTestType == $scope.Step_Types.WebsiteTestStep) {
            $scope.CopyTestData.TestDataList[i].rowStyle = 'background-color: #FFFFBA;';
          }
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.selectAllTestData = function () {
      for (var i = 0; i < $scope.CopyTestData.TestDataList.length; i++) {
        $scope.CopyTestData.TestDataList[i].Checked = $scope.CopyTestData.AllTestDataChecked;
      }
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
  }]);
