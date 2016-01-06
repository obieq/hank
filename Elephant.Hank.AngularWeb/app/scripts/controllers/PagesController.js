/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('PagesController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.PagesList = [ ];
    $scope.Page = {} ;
    $scope.Website = [ ];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;

    $scope.getAllPages = function(){
      $scope.loadData();
      crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function(response){
        $scope.PagesList = response;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.getPageById = function(){
      crudService.getById(ngAppSettings.PagesUrl, $stateParams.PageId).then(function(response){
        $scope.Page = response.Item;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.updatePage = function(){
      crudService.update(ngAppSettings.PagesUrl, $scope.Page).then(function(response){
        $state.go("Website.Pages", { WebsiteId: $scope.stateParamWebsiteId });
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.addPage = function(){
      $scope.Page.WebsiteId = $scope.Website.Id;
      crudService.add(ngAppSettings.PagesUrl, $scope.Page).then(function(response){
        $state.go("Website.Pages", { WebsiteId: $scope.stateParamWebsiteId });
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.loadData = function(){
      dataProvider.currentWebSite($scope);
    };
  }]);
