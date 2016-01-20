/**
 * Created by vyom.sharma on 17-12-2015.
 */

'use strict';

app.controller('DataBaseCategoriesController', ['$scope', '$rootScope', '$q', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $q, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    dataProvider.currentWebSite($scope);
    $scope.DataBaseCategories = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.DataBaseCategory = {};

    $scope.onDataBaseCategoriesListPageLoad = function () {
      crudService.getAll(ngAppSettings.WebsiteDataBaseCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.DataBaseCategories = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onDataBaseCategoriesAddPageSubmit = function () {
      $scope.DataBaseCategory.WebsiteId = $stateParams.WebsiteId;
      crudService.add(ngAppSettings.DataBaseCategoriesUrl.format($stateParams.WebsiteId), $scope.DataBaseCategory).then(function (response) {
        $state.go("Website.DataBaseCategories", {WebsiteId: $stateParams.WebsiteId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onDataBaseCategoriesUpdatePageLoad = function () {
      crudService.getById(ngAppSettings.DataBaseCategoriesUrl.format($stateParams.WebsiteId), $stateParams.DataBaseCategoryId).then(function (response) {
        $scope.DataBaseCategory = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onDataBaseCategoriesUpdatePageSubmit = function () {
      crudService.update(ngAppSettings.DataBaseCategoriesUrl.format($stateParams.WebsiteId), $scope.DataBaseCategory).then(function (response) {
        $state.go("Website.DataBaseCategories", {WebsiteId: $stateParams.WebsiteId});
      }, function () {
        commonUi.showErrorPopup(response);
      });
    };

  }]);
