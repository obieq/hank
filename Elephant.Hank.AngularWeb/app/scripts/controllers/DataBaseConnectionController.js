/**
 * Created by vyom.sharma on 17-12-2015.
 */


'use strict';

app.controller('DataBaseConnectionController', ['$scope', '$rootScope', '$q', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $q, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    dataProvider.currentWebSite($scope);
    $scope.DataBaseConnections = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.DataBaseCategory = {};
    $scope.EnvironmentList = [];
    $scope.DataBaseConnection = {};
    $scope.DataBaseList = [];
    $scope.AuthenticationList = [{Id: 1, Name: 'Windows Authentication'}, {Id: 2, Name: 'SQL Server Authentication'}];

    crudService.getById(ngAppSettings.DataBaseCategoriesUrl, $stateParams.DataBaseCategoryId).then(function (response) {
      $scope.DataBaseCategory = response.Item;
    }, function (response) {
      commonUi.showErrorPopup(response);
    });

    $scope.onDataBaseConnectionAddPageLoad = function () {
      crudService.getAll(ngAppSettings.EnvironmentUrl).then(function (response) {
        $scope.EnvironmentList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onClickLoadDataBaseList = function () {
      crudService.add(ngAppSettings.DataBaseConnectionGetDataBaseListUrl, $scope.DataBaseConnection).then(function (response) {
        $scope.DataBaseList = response;
      },function(response){
        commonUi.showMessagePopup("The server details u provide is incorrect","Invalid Credentials")
      });
    };

    $scope.onDataBaseConnectionListPageLoad = function () {
      crudService.getAll(ngAppSettings.DataBaseCategoriesConnectionUrl.format($stateParams.DataBaseCategoryId)).then(function (response) {
        $scope.DataBaseConnections = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onDataBaseConnectionAddPageSubmit = function () {
      $scope.DataBaseConnection.WebsiteId = $stateParams.WebsiteId;
      $scope.DataBaseConnection.CategoryId = $stateParams.DataBaseCategoryId;
      crudService.add(ngAppSettings.DataBaseConnectionUrl, $scope.DataBaseConnection).then(function (response) {
        $state.go("Website.DataBaseConnection", {
          WebsiteId: $stateParams.WebsiteId,
          DataBaseCategoryId: $stateParams.DataBaseCategoryId
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onDataBaseConnectionUpdatePageLoad = function () {
      crudService.getAll(ngAppSettings.EnvironmentUrl).then(function (response) {
        $scope.EnvironmentList = response;
        crudService.getById(ngAppSettings.DataBaseConnectionUrl, $stateParams.DataBaseConnectionId).then(function (response) {
          $scope.DataBaseConnection = response.Item;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });

    };

    $scope.onAuthenticatonDropdownChange = function () {
      $scope.DataBaseList = [];
      if ($scope.DataBaseConnection.Authentication == 1) {
        $scope.DataBaseConnection.UserName = "";
        $scope.DataBaseConnection.Password = "";
      }
    };

    $scope.onDataBaseConnectionUpdatePageSubmit = function () {
      crudService.update(ngAppSettings.DataBaseConnectionUrl, $scope.DataBaseConnection).then(function (response) {
        $state.go("Website.DataBaseConnection", {
          WebsiteId: $stateParams.WebsiteId,
          DataBaseCategoryId: $stateParams.DataBaseCategoryId
        });
      }, function () {
        commonUi.showErrorPopup(response);
      });
    };

  }
])
;
