/**
 * Created by gurpreet.singh on 09/02/2015.
 */

'use strict';

app.controller('TestCatController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.TestCatList = [ ];
    $scope.TestCat = {} ;
    $scope.Website = [ ];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;



    $scope.getAllTestCat = function(){
      $scope.loadData();
      crudService.getAll(ngAppSettings.WebSiteTestCatUrl.format($stateParams.WebsiteId)).then(function(response){
        $scope.TestCatList = response;
      },function(response){  });
    };

    $scope.getTestCatById = function(){
      crudService.getById(ngAppSettings.TestCatUrl.format($stateParams.WebsiteId), $stateParams.TestCatId).then(function(response){
        $scope.TestCat = response.Item;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.updateTestCat = function(){
      crudService.update(ngAppSettings.TestCatUrl.format($stateParams.WebsiteId), $scope.TestCat).then(function(response){
        $state.go("Website.TestCat", { WebsiteId: $scope.stateParamWebsiteId });
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.addTestCat = function(){
      $scope.TestCat.WebsiteId = $scope.Website.Id;
      crudService.add(ngAppSettings.TestCatUrl.format($stateParams.WebsiteId), $scope.TestCat).then(function(response){
        $state.go("Website.TestCat", { WebsiteId: $scope.stateParamWebsiteId });
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.loadData = function(){
      dataProvider.currentWebSite($scope);
    };
  }]);
