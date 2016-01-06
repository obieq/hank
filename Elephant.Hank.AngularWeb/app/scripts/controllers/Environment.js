/**
 * Created by vyom.sharma on 04-08-2015.
 */

'use strict';

app.controller('EnvironmentController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.EnvironmentList = [ ];
    $scope.Environment = {} ;

    $scope.getAllEnvironment = function(){
      crudService.getAll(ngAppSettings.EnvironmentUrl).then(function(response){
        $scope.EnvironmentList = response;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.getEnvironmentById = function(){
      crudService.getById(ngAppSettings.EnvironmentUrl, $stateParams.Id).then(function(response){
        $scope.Environment = response.Item;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.updateEnvironment = function(){
      crudService.update(ngAppSettings.EnvironmentUrl, $scope.Environment).then(function(response){
        $state.go("Environment.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.addEnvironment = function(){
      crudService.add(ngAppSettings.EnvironmentUrl, $scope.Environment).then(function(response){
        $state.go("Environment.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };
  }]);
