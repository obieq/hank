/**
 * Created by vyom.sharma on 14-01-2016.
 */


'use strict';

app.controller('GroupTabController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {

    $scope.HeadingText = '';
    $scope.GroupId = $stateParams.GroupId;

    if ($stateParams.GroupId != undefined && $stateParams.GroupId > 0) {
      crudService.getById(ngAppSettings.GroupUrl, $stateParams.GroupId).then(function (response) {
        $scope.HeadingText = response.Item.GroupName;
        if ($stateParams.WebsiteId != undefined && $stateParams.WebsiteId > 0) {
          crudService.getById(ngAppSettings.WebSiteUrl, $stateParams.WebsiteId).then(function (response) {
            $scope.HeadingText = $scope.HeadingText + " - " + response.Item.Name;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });

        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    } ;

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

  }]);
