/**
 * Created by Gurpreet Singh on 12-05-2016.
 */

'use strict';

app.controller('ApiCategoriesController', ['$scope', '$rootScope', '$q', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $q, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};

    dataProvider.currentWebSite($scope);

    $scope.ApiCategories = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.ApiCategory = {};

    $scope.IsEditMode = $stateParams.ApiCategoryId && $stateParams.ApiCategoryId > 0;

    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.ApiCategories);

    $scope.onApiCategoriesListPageLoad = function () {
      crudService.getAll(ngAppSettings.ApiCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.ApiCategories = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addUpdateCat = function () {
      if($scope.IsEditMode){
        crudService.update(ngAppSettings.ApiCategoriesUrl.format($stateParams.WebsiteId), $scope.ApiCategory).then(function (response) {
          $state.go("Website.ApiCategories", {WebsiteId: $stateParams.WebsiteId});
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }else {
        $scope.ApiCategory.WebsiteId = $stateParams.WebsiteId;
        crudService.add(ngAppSettings.ApiCategoriesUrl.format($stateParams.WebsiteId), $scope.ApiCategory).then(function (response) {
          $state.go("Website.ApiCategories", {WebsiteId: $stateParams.WebsiteId});
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.getByCatId = function () {
      crudService.getById(ngAppSettings.ApiCategoriesUrl.format($stateParams.WebsiteId), $stateParams.ApiCategoryId).then(function (response) {
        $scope.ApiCategory = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    if($scope.IsEditMode){
      $scope.getByCatId();
    }

  }]);
