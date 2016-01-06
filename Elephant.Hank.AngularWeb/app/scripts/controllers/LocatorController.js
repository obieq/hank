/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('LocatorController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.LocatorList = [ ];
    $scope.Locator = {} ;

    $scope.getAllLocators = function(){
       crudService.getAll(ngAppSettings.LocatorUrl).then(function(response){
         $scope.LocatorList = response;
       },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.getLocatorById = function(){
      crudService.getById(ngAppSettings.LocatorUrl, $stateParams.Id).then(function(response){
        $scope.Locator = response.Item;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.updateLocator = function(){
      crudService.update(ngAppSettings.LocatorUrl, $scope.Locator).then(function(response){
        $state.go("Locator.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.addLocator = function(){

      crudService.add(ngAppSettings.LocatorUrl, $scope.Locator).then(function(response){
        $state.go("Locator.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };
}]);
