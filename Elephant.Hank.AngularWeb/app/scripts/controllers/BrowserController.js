/**
 * Created by gurpreet.singh on 06/09/2015.
 */

'use strict';

app.controller('BrowserController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.BrowserList = [ ];
    $scope.Browser = { MaxInstances: 1, ShardTestFiles: true } ;

    $scope.getAllBrowsers = function(){
      crudService.getAll(ngAppSettings.BrowserUrl).then(function(response){
        $scope.BrowserList = response;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.getBrowserById = function(){
      crudService.getById(ngAppSettings.BrowserUrl, $stateParams.Id).then(function(response){
        $scope.Browser = response.Item;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.updateBrowser = function(){
      crudService.update(ngAppSettings.BrowserUrl, $scope.Browser).then(function(response){
        $state.go("Browser.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.addBrowser = function(){

      crudService.add(ngAppSettings.BrowserUrl, $scope.Browser).then(function(response){
        $state.go("Browser.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };
  }]);
