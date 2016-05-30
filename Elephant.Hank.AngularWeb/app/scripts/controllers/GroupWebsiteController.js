/**
 * Created by vyom.sharma on 13-01-2016.
 */

'use strict';

app.controller('GroupWebsiteController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.WebsiteList = [];
    $scope.submit=true;
    $scope.WebsiteIdList = [];
    $scope.GroupModuleList = [];
    $scope.stateparamsGroupId=$stateParams.GroupId;
    crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
      $scope.WebsiteList = response;
      crudService.getAll(ngAppSettings.GroupModuleUrl.format($stateParams.GroupId)).then(function (response) {
        debugger;
        $scope.GroupModuleList = response;
        for (var i = 0; i < $scope.WebsiteList.length; i++) {
          var checkPrmission = _.where($scope.GroupModuleList, {'WebsiteId': $scope.WebsiteList[i].Id,'CanRead':true });
          if (checkPrmission.length > 0) {
            $scope.WebsiteList[i].PermissionLink=true;
            $scope.WebsiteList[i].Permission=true;
          }
          else{
            $scope.WebsiteList[i].PermissionLink=false;
            $scope.WebsiteList[i].Permission=false;
          }
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    }, function (response) {
      commonUi.showErrorPopup(response);
    });

    $scope.onGroupWebsiteSubmit = function () {
      $scope.submit=false;
      $scope.WebsiteIdList = [];
      for (var i = 0; i < $scope.WebsiteList.length; i++) {
        if ($scope.WebsiteList[i].Permission) {
          $scope.WebsiteIdList.push($scope.WebsiteList[i].Id);
        }
      }

      crudService.add(ngAppSettings.GroupWebsiteUrl.format($stateParams.GroupId), $scope.WebsiteIdList).then(function (response) {
        window.location.reload();
        commonUi.showMessagePopup("Website had been added to group successfully", "Success");
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

  }]);
