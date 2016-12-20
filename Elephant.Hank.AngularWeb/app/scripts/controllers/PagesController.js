/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('PagesController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider', 'localStorageService','JsonHelper',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider, localStorageService,jsonHelper) {
    $scope.PagesList = [];
    $scope.Page = {};
    $scope.Website = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;

    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.Page);

    $scope.getAllPages = function () {
      $scope.loadData();
      crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.PagesList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getPageById = function () {
      crudService.getById(ngAppSettings.PagesUrl.format($stateParams.WebsiteId), $stateParams.PageId).then(function (response) {
        $scope.Page = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.deletePageById = function (pageId) {
      var deleteConfirmation = confirm("Are you sure you want to delete?");
      if (deleteConfirmation) {
        crudService.delete(ngAppSettings.PagesUrl.format($stateParams.WebsiteId), {'Id': pageId}).then(function (response) {
          $scope.PagesList = jsonHelper.deleteByProperty(angular.copy($scope.PagesList), "Id", pageId);
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.updatePage = function () {
      crudService.update(ngAppSettings.PagesUrl.format($stateParams.WebsiteId), $scope.Page).then(function (response) {
        $state.go("Website.Pages", {WebsiteId: $scope.stateParamWebsiteId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addPage = function () {
      $scope.Page.WebsiteId = $scope.Website.Id;
      crudService.add(ngAppSettings.PagesUrl.format($stateParams.WebsiteId), $scope.Page).then(function (response) {
        $state.go("Website.Pages", {WebsiteId: $scope.stateParamWebsiteId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadData = function () {
      dataProvider.currentWebSite($scope);
    };
  }]);
