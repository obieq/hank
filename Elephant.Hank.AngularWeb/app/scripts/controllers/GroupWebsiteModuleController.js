/**
 * Created by vyom.sharma on 14-01-2016.
 */


'use strict';

app.controller('GroupWebsiteModuleController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.GroupModuleAccessList = [];
    crudService.getAll(ngAppSettings.GroupWebsiteModuleUrl.format($stateParams.GroupId, $stateParams.websiteId)).then(function (response) {
      $scope.GroupModuleAccessList = response;
    }, function (response) {
      commonUi.showErrorPopup(response);
    });

    $scope.onGroupWebsiteModuleSubmit = function () {
      crudService.add(ngAppSettings.GroupModuleAccessBulkUpdate,$scope.GroupModuleAccessList).then(function (response) {

        commonUi.showMessagePopup('Access rights has been updated successfully','Success')
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

  }]);
