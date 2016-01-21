/**
 * Created by vyom.sharma on 08-12-2015.
 */

'use strict';

app.controller('UserController', ['$scope', '$rootScope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.UserList = [];
    $scope.ResetPasswordModel = {};

    $scope.onLoadUserListPage = function () {
      crudService.getAll(ngAppSettings.UserUrl).then(function (response) {
        $scope.UserList = response;
      });
    };

    $scope.onActivateDeactivateClick = function (ActivationId, enabled) {
      crudService.getAll(ngAppSettings.UserSetLockoutUrl.format(ActivationId, enabled)).then(function (response) {
        $scope.UserList = response;
      });
    };

    $scope.onResetPasswordSubmit = function () {
      if ($scope.ResetPasswordModel.NewPassword == $scope.ResetPasswordModel.ConfirmNewPassword) {
        crudService.add(ngAppSettings.ResetPasswordUrl, $scope.ResetPasswordModel).then(function (response) {
          if (response.Item  && !response.IsError) {
            commonUi.showMessagePopup("Successfully reset password", "Reset Password");
            $scope.ResetPasswordModel.NewPassword = ""
            $scope.ResetPasswordModel.ConfirmNewPassword = "";
          }
          else {
            commonUi.showErrorPopup(response, "Reset Password");
          }
        }, function (response) {
          commonUi.showErrorPopup(response, "Reset Password");
        });
      }
      else {
        commonUi.showMessagePopup("New Password and Confirm New Password doesn't match", "Reset Password");
      }
    };

    $scope.onLoadResetPasswordPage = function () {
      $scope.ResetPasswordModel.UserId = $stateParams.UserId;
    }
  }]);
