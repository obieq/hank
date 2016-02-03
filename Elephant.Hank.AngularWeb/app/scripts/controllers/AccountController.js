/**
 * Created by vyom.sharma on 30-09-2015.
 */
'use strict';

app.controller('AccountController', ['$scope', '$state', '$stateParams', 'authService', 'CommonUiService', 'CrudService', 'ngAppSettings', 'localStorageService',
  function ($scope, $state, $stateParams, authService, commonUi, crudService, ngAppSettings, localStorageService) {
    $scope.UserProfile = {};
    $scope.BrowserList = [];
    $scope.User = {
      UserName: "",
      EmailId: "",
      Password: "",
      ConfrimPassword: ""
    };

    if ($stateParams.mid == 1) {
      commonUi.showMessagePopup("Registeration has been done successfully!", "Success!");
    }
    else if ($stateParams.mid == 2) {
      commonUi.showMessagePopup("Please login to continue!", "Logon");
    }

    $scope.loginUser = function () {
      authService.login($scope.User).then(
        function (response) {
          crudService.getAll("Account/GetModuleAuthenticatedToUser").then(function (response) {
            debugger;
            localStorageService.remove('groupData');
            localStorageService.set('groupData', response);
          });
          loginRedirect();
        },
        function (err) {
          commonUi.showErrorPopup(err);
        }
      );
    };

    $scope.registerUser = function () {
      $scope.User.ConfirmPassword = $scope.User.Password;
      authService.saveRegistration($scope.User).then(function (response) {
          commonUi.showMessagePopup("Registeration has been done successfully!", "Success!");
          $scope.User = [];
        },
        function (err) {
          commonUi.showErrorPopup(err);
        }
      );
    };

    $scope.onLoadProfilePage = function () {
      crudService.getById(ngAppSettings.UserProfileUrl).then(function (response) {
        $scope.UserProfile = response.Item;
        crudService.getAll(ngAppSettings.BrowserUrl).then(function (response) {
          $scope.BrowserList = response;
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
      crudService.update(ngAppSettings.UserProfileUrl, $scope.UserProfile).then(function (response) {
        $scope.UserProfile.Settings.Browsers = browsers;
        $scope.UserProfile = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    function loginRedirect() {
      var user = authService.getAuthData();

      if ($scope.returnToState != undefined) {
        var returnToState = $scope.returnToState;
        var returnToStateParams = $scope.returnToStateParams;
        $scope.returnToState = undefined;
        $scope.returnToStateParams = undefined;
        $state.go(returnToState, returnToStateParams);
      }
      else if (user.type == "TestAdmin") {
        $state.go("Website.List");
      }
      else if (user.type == "TestUser") {
        $state.go("Website.List");
      }
    }
  }]);

