/**
 * Created by gurpreet.singh on 09/17/2015.
 */

'use strict';

app.controller('NavCtrl', ['$scope', '$state', 'authService', '$stateParams', '$rootScope',
  function ($scope, $state, authService, $stateParams, $rootScope) {
    $scope.DefaultState = "Login";

    $scope.RoleDefaultStateMapping = [
      {Role: "TestUser", StateName: "Website.List"},
      {Role: "TestAdmin", StateName: "Website.List"}
    ];

    $scope.isWebsiteSelected = $stateParams.WebsiteId > 0;
    $scope.CurrentUrl = $state.href($state.current.name, $state.params, {absolute: true});
    $scope.IsLoggedIn = false;
    $scope.UserType = false;
    $scope.IsAdmin = false;
    $scope.UserName = '';
    $scope.Designation = '';
    $scope.WebsiteId = $stateParams.WebsiteId;

    $scope.isActive = function (matchIndex, stateName) {
      matchIndex = matchIndex - 1;

      var states = $state.current.name.split('.');
      var state = states[matchIndex];
      var subState = states[matchIndex + 1];
      var targetState = stateName.split('.');

      if (matchIndex == 0 && subState != "Add" && subState != "Update" && subState != "List") {
        return "";
      }
      if (state == undefined) {
        return "";
      }
      return state.indexOf(targetState[matchIndex]) == 0 ? "active" : "";
    };

    /* $scope.$on('$stateChangeStart', function (ev, to, toParams, from, fromParams) {
     var permissionData = to.permissionData;
     if (permissionData && aryValIndex(permissionData.Roles, 'All') == -1) {
     var authData = authService.getAuthData();
     }
     });*/

    $scope.$on('$stateChangeStart', function (ev, to, toParams, from, fromParams) {
      var permissionData = to.permissionData;
      var isNotAuth = false;
      if (permissionData && aryValIndex(permissionData.Roles, 'All') == -1) {
        var authData = authService.getAuthData();
        if (authData.type) {
          var roleIdx = aryValIndex(permissionData.Roles, authData.type);
          isNotAuth = roleIdx == -1;
          if ($rootScope.returnToState != undefined) {
            var returnToState = $rootScope.returnToState;
            var returnToStateParams = $rootScope.returnToStateParams;
            $rootScope.returnToState = undefined;
            $rootScope.returnToStateParams = undefined;
            ev.preventDefault();
            $state.go(returnToState, returnToStateParams);
          }
        }
        else if (permissionData.Roles && permissionData.Roles.length > 0) {
          isNotAuth = true;
        }

        if (isNotAuth || (permissionData.NotAllowdedIfLoggedIn && authData.type)) {
          var redirectSate = $scope.DefaultState;
          if (to.name != '' || to.name != undefined) {
            $rootScope.returnToState = to.name;
            $rootScope.returnToStateParams = toParams;
          }
          var mappedObj = aryfindObjIndex($scope.RoleDefaultStateMapping, 'Role', authData.type);
          if (mappedObj != -1) {
            redirectSate = $scope.RoleDefaultStateMapping[mappedObj].StateName;
          }
          if (redirectSate != to.name) {
            ev.preventDefault();
            $state.go(redirectSate);
          }
        }
      }
      else {
        if (!isNotAuth || permissionData.NotAllowdedIfLoggedIn) {
          var authData = authService.getAuthData();
          for (var t = 0; t <= $scope.RoleDefaultStateMapping.length; t++) {
            if (authData.type == $scope.RoleDefaultStateMapping[t].Role) {
              ev.preventDefault();
              $state.go($scope.RoleDefaultStateMapping[t].StateName);
              break;
            }
          }

        }
      }
    });

    $scope.$on('$stateChangeSuccess', function (e) {
      $scope.CurrentUrl = $state.href($state.current.name, $state.params, {absolute: true});
      $scope.WebsiteId = $stateParams.WebsiteId;
      $scope.isWebsiteSelected = $stateParams.WebsiteId > 0;
      var authData = authService.getAuthData();
      $scope.IsLoggedIn = authData.isAuth;
      $scope.UserType = authData.type;
      if (authData.type == 'TestAdmin') {
        $scope.IsAdmin = true;
      }
      else {
        $scope.IsAdmin = false;
      }
      $scope.UserName = authData.userName;
      $scope.Designation = authData.designation;
      $scope.WebsiteId = $stateParams.WebsiteId;
      $scope.membersince = authData.membersince;
      $scope.FirstName = authData.FirstName;
      $scope.LastName = authData.LastName;
    });

    $scope.logout = function () {
      authService.logOut();
      $state.go($scope.DefaultState);
    };

    function aryValIndex(ary, value) {
      if (value && ary && ary.length > 0) {
        value = value.toLowerCase();

        for (var i = 0; i < ary.length; i++) {
          if (ary[i].toLowerCase() == value) {
            return i;
          }
        }
      }
      return -1;
    };

    function aryfindObjIndex(ary, prop, value) {
      if (value && prop && ary && ary.length > 0) {
        value = value.toLowerCase();

        for (var i = 0; i < ary.length; i++) {
          var curVal = eval("ary[i]." + prop) + "";

          if (curVal.toLowerCase() == value) {
            return i;
          }
        }
      }
      return -1;
    };
  }]);
