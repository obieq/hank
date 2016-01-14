/**
 * Created by vyom.sharma on 12-01-2016.
 */


'use strict';

app.controller('GroupController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.GroupList = [ ];
    $scope.Group = {} ;

    $scope.onLoadList = function(){
      crudService.getAll(ngAppSettings.GroupUrl).then(function(response){
        $scope.GroupList = response;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.onLoadEdit = function(){
      crudService.getById(ngAppSettings.GroupUrl, $stateParams.GroupId).then(function(response){
        $scope.Group = response.Item;
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.onUpdateSubmit = function(){
      crudService.update(ngAppSettings.GroupUrl, $scope.Group).then(function(response){
        $state.go("Group.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.onAddSubmit = function(){
      crudService.add(ngAppSettings.GroupUrl, $scope.Group).then(function(response){
        $state.go("Group.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };
  }]);
