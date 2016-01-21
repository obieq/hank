/**
 * Created by vyom.sharma on 12-10-2015.
 */

'use strict';

app.controller('ProfileController', ['$scope', '$state', '$stateParams', 'authService', 'CommonUiService', 'CrudService', 'ngAppSettings',
  function ($scope, $state, $stateParams, authService, commonUi, crudService, ngAppSettings) {
    $scope.UserProfile = {};
    $scope.BrowserList = [];
    $scope.ChangePassword = {};


    $scope.onLoadProfilePage = function () {
      crudService.getById(ngAppSettings.UserProfileUrl).then(function (response) {
        $scope.UserProfile = response.Item;
        crudService.getAll(ngAppSettings.BrowserUrl).then(function (response) {
          $scope.BrowserList = response;
          for (var i = 0; i < $scope.UserProfile.Settings.Browsers.length; i++) {
            for (var j = 0; j < $scope.BrowserList.length; j++) {
              if ($scope.UserProfile.Settings.Browsers[i] == $scope.BrowserList[j].Id) {
                $scope.BrowserList[j].Checked = true;
                break;
              }
            }
          }
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onProfileSubmit = function () {
      var browsers = [];
      for (var i = 0; i < $scope.BrowserList.length; i++) {
        if ($scope.BrowserList[i].Checked) {
          browsers.push($scope.BrowserList[i].Id);
        }
      }
      $scope.UserProfile.Settings.Browsers = browsers;
      crudService.update(ngAppSettings.UserProfileUpdateUrl.format($scope.UserProfile.UserId), $scope.UserProfile).then(function (response) {
        $scope.UserProfile = response.Item;
        commonUi.showMessagePopup("Profile saved successfully", "Profile");
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };


    $scope.onChangePasswordSubmit = function () {
      if ($scope.ChangePassword.NewPassword == $scope.ChangePassword.ConfirmNewPassword) {
        crudService.add(ngAppSettings.ChangePasswordUrl, $scope.ChangePassword).then(function (response) {
          $scope.ChangePassword.CurrentPassword = "";
          $scope.ChangePassword.NewPassword = "";
          $scope.ChangePassword.ConfirmNewPassword = "";

          commonUi.showMessagePopup("Password has been changed successfully", "Password Changed");
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
      else {
        commonUi.showMessagePopup("New Password doesn't match with Confirm New Password", "Confirm Password");
      }
    };
  }]);


