/**
 * Created by vyom.sharma on 12-05-2016.
 */

'use strict';

app.controller('HashTagController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.HashTagDescription = {};

    crudService.getById(ngAppSettings.HashTagDescriptionUrl, 1).then(function(response){
      setTimeout(function () {
        $scope.HashTagDescription = response.Item;
      }, 200);
    },function(response){ commonUi.showErrorPopup(response); });

    $scope.addUpdate = function () {
      $scope.HashTagDescription.Id=1;
      crudService.update(ngAppSettings.HashTagDescriptionUrl, $scope.HashTagDescription).then(function (response) {
        $state.go("HashTagDescription.Description");
      }, function (response) {
        commonUi.showErrorPopup(response);
      });

    };

  }]);
