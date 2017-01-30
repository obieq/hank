/**
 * Created by gurpreet.singh on 06/09/2015.
 */

'use strict';

app.controller('BrowserController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.BrowserList = [ ];
    $scope.Browser = { MaxInstances: 1, ShardTestFiles: true, Properties: [{}] } ;

    $scope.IsEditMode = $stateParams.Id && $stateParams.Id > 0;

    $scope.getAllBrowsers = function(){
      crudService.getAll(ngAppSettings.BrowserUrl).then(function(response){
        $scope.BrowserList = response;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.getBrowserById = function(){
      crudService.getById(ngAppSettings.BrowserUrl, $stateParams.Id).then(function(response){
        $scope.Browser = response.Item;
        $scope.Browser.Properties = $scope.Browser.Properties || [{}];
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

    $scope.removeBrowserProperty = function(headerIndex){
      $scope.Browser.Properties = $scope.Browser.Properties || [];
      $scope.Browser.Properties.splice(headerIndex, 1);
    };

    $scope.addBrowserProperty = function(){
      $scope.Browser.Properties = $scope.Browser.Properties || [];
      $scope.Browser.Properties.push({});
    };

    if($scope.IsEditMode){
      $scope.getBrowserById();
    }
  }]);
