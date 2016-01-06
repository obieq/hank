/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('ActionController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.ActionList = [ ];
    $scope.Action = {} ;

    $scope.getAllActions = function(){
      crudService.getAll(ngAppSettings.ActionUrl).then(function(response){
        $scope.ActionList = response;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.getActionById = function(){

      crudService.getById(ngAppSettings.ActionUrl, $stateParams.Id).then(function(response){
        $scope.Action = response.Item;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.updateAction = function(){
      crudService.update(ngAppSettings.ActionUrl, $scope.Action).then(function(response){
        $state.go("Action.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.addAction = function(){
      crudService.add(ngAppSettings.ActionUrl, $scope.Action).then(function(response){
        $state.go("Action.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };
  }]);
