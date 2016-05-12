/**
 * Created by Gurpreet Singh on 12-05-2016.
 */

'use strict';

app.controller('ApiConnectionController', ['$scope', '$rootScope', '$q', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $q, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};

    dataProvider.setAuthenticationParameters($scope,$stateParams.WebsiteId,ngAppSettings.ModuleType.ApiConnection);

    dataProvider.currentWebSite($scope);

    $scope.ApiConnections = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.ApiCategory = { };
    $scope.EnvironmentList = [];
    $scope.ApiConnection = {Headers: [{}]};

    $scope.IsEditMode = $stateParams.ApiConnectionId && $stateParams.ApiConnectionId > 0;

    crudService.getById(ngAppSettings.ApiCategoriesUrl.format($stateParams.WebsiteId), $stateParams.ApiCategoryId).then(function (response) {
      $scope.ApiCategory = response.Item;
    }, function (response) {
      commonUi.showErrorPopup(response);
    });

    $scope.getAllCons = function () {
      crudService.getAll(ngAppSettings.ApiConnectionUrl.format($stateParams.WebsiteId, $stateParams.ApiCategoryId)).then(function (response) {
        $scope.ApiConnections = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getConById = function () {
      crudService.getById(ngAppSettings.ApiConnectionUrl.format($stateParams.WebsiteId, $stateParams.ApiCategoryId), $stateParams.ApiConnectionId).then(function (response) {
        $scope.ApiConnection = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    }

    $scope.removeHeader = function(headerIndex){
      $scope.ApiConnection.Headers = $scope.ApiConnection.Headers || [];
      $scope.ApiConnection.Headers.splice(headerIndex, 1);
    };

    $scope.addBlankHeader = function(){
      $scope.ApiConnection.Headers = $scope.ApiConnection.Headers || [];
      $scope.ApiConnection.Headers.push({});
    };

    $scope.addUpdateConnection = function () {
      if($scope.IsEditMode) {
        crudService.update(ngAppSettings.ApiConnectionUrl.format($stateParams.WebsiteId, $stateParams.ApiCategoryId), $scope.ApiConnection).then(function (response) {
          $state.go("Website.ApiConnection", { WebsiteId: $stateParams.WebsiteId, ApiCategoryId: $stateParams.ApiCategoryId });
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
      else {
        $scope.ApiConnection.WebsiteId = $stateParams.WebsiteId;
        $scope.ApiConnection.CategoryId = $stateParams.ApiCategoryId;
        crudService.add(ngAppSettings.ApiConnectionUrl.format($stateParams.WebsiteId, $stateParams.ApiCategoryId), $scope.ApiConnection).then(function (response) {
          $state.go("Website.ApiConnection", { WebsiteId: $stateParams.WebsiteId, ApiCategoryId: $stateParams.ApiCategoryId });
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.loadEnvironments = function() {
      crudService.getAll(ngAppSettings.EnvironmentUrl).then(function (response) {
        $scope.EnvironmentList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    }

    if($scope.IsEditMode){
      $scope.getConById();
    }
  }
])
;
