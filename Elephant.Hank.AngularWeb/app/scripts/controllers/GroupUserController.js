/**
 * Created by vyom.sharma on 14-01-2016.
 */

'use strict';

app.controller('GroupUserController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    debugger;
    $scope.UserList = [];
    $scope.GroupUserList = [];
    crudService.getAll(ngAppSettings.UserUrl).then(function (response) {
      $scope.UserList = response;
    }, function (response) {
      commonUi.showErrorPopup(response);
    });

    crudService.getAll(ngAppSettings.GroupUserUrl.format($stateParams.GroupId)).then(function (response) {
      $scope.GroupUserList = response;
    }, function (response) {
      commonUi.showErrorPopup(response);
    });

    $scope.onClickAddUserToGroup = function (userId) {
      crudService.add(ngAppSettings.GroupUserAddUrl, {
        UserId: userId,
        GroupId: $stateParams.GroupId
      }).then(function (response) {
        var usertoPush = {};
        for (var i = 0; i < $scope.UserList.length; i++) {
          if ($scope.UserList[i].Id == userId) {
            $scope.UserList.splice(i, 1);
            usertoPush = $scope.UserList[i];
            break;
          }
        }
        $scope.GroupUserList.push(usertoPush);
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };
  }]);
