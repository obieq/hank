/**
 * Created by vyom.sharma on 14-01-2016.
 */

'use strict';

app.controller('GroupUserController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.UserList = [];
    $scope.GroupUserList = [];


    crudService.getAll(ngAppSettings.GroupUserUrl.format($stateParams.GroupId)).then(function (response) {
      $scope.GroupUserList = response;


      crudService.getAll(ngAppSettings.UserUrl).then(function (response) {
        $scope.UserList = response;
        for (var i = 0; i < $scope.GroupUserList.length; i++) {
          for (var j = 0; j < $scope.UserList.length; j++) {
            if ($scope.UserList[j].Id == $scope.GroupUserList[i].UserId) {
              $scope.UserList.splice(j, 1);
            }
          }
        }

      }, function (response) {
        commonUi.showErrorPopup(response);
      });

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
            usertoPush = {
              UserId: $scope.UserList[i].Id,
              GroupId: $stateParams.GroupId,
              UserName: $scope.UserList[i].UserName
            };
            $scope.UserList.splice(i, 1);
            break;
          }
        }
        $scope.GroupUserList.push(usertoPush);
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onClickRemoveUserFromGroup = function (userId) {
      crudService.getById(ngAppSettings.GroupUserDeleteUrl.format($stateParams.GroupId,userId)).then(function (response) {
        var usertoPush = {};
        for (var i = 0; i < $scope.GroupUserList.length; i++) {
          if ($scope.GroupUserList[i].UserId == userId) {
            usertoPush = {
              Id: $scope.GroupUserList[i].UserId,
              UserName: $scope.GroupUserList[i].UserName
            };
            $scope.GroupUserList.splice(i, 1);
            break;
          }
        }
        $scope.UserList.push(usertoPush);
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };
  }]);
