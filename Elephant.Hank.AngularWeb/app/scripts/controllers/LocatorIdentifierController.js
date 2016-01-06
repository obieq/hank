/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('LocatorIdentifierController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.LocatorIdentifierList = [];
    $scope.LocatorIdentifier = {};
    $scope.LocatorList = [];
    $scope.stateParamPageId = $stateParams.PageId;
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;

    $scope.getAllLocatorIdentifiers = function () {
      $scope.loadData(1);

      crudService.getAll(ngAppSettings.PagesLocatorIdentifierUrl.format($stateParams.PageId)).then(function (response) {
        $scope.LocatorIdentifierList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getLocatorIdentifierById = function () {
      crudService.getById(ngAppSettings.LocatorIdentifierUrl, $stateParams.LocatorIdentifierId).then(function (response) {
        $scope.LocatorIdentifier = response.Item;
        $scope.loadData(2);
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.updateLocatorIdentifier = function () {
      crudService.update(ngAppSettings.LocatorIdentifierUrl, $scope.LocatorIdentifier).then(function (response) {
        $state.go("Website.PagesLocatorIdentifier", {
          WebsiteId: $scope.stateParamWebsiteId,
          PageId: $stateParams.PageId
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onRegularExpressionInputKeyUp=function(){
      try{
        var regexcomputaionresult = new RegExp($scope.LocatorIdentifier.Value).exec($scope.LocatorIdentifier.Exp);
        if(regexcomputaionresult.length==0){
          $("#txtRegularExp").setAttribute('style', 'border-color: red;');
        }
        else{
          $("#txtRegularExp").attr('style', 'border-color: green;');
        }
      }
      catch(e){
        $("#txtRegularExp").attr('style', 'border-color: red;');
      }
    };

    $scope.addLocatorIdentifier = function () {
      $scope.LocatorIdentifier.WebsiteId = $stateParams.WebsiteId;
      $scope.LocatorIdentifier.PageId = $stateParams.PageId;
      if ($scope.LocatorIdentifier.LocatorId == 15) {
        try{
          var regexcomputaionresult = new RegExp($scope.LocatorIdentifier.Value).exec($scope.LocatorIdentifier.Exp);
          if(regexcomputaionresult.length==0){
            commonUi.showMessagePopup("Regular expression is not correct in respect to 'To be applied'","Regex Error");
          }
          else{
            crudService.add(ngAppSettings.LocatorIdentifierUrl, $scope.LocatorIdentifier).then(function (response) {
              $state.go("Website.PagesLocatorIdentifier", {
                WebsiteId: $scope.stateParamWebsiteId,
                PageId: $stateParams.PageId
              });
            }, function (response) {
              commonUi.showErrorPopup(response);
            });
          }
        }
        catch(e){
          commonUi.showMessagePopup("Regular expression is not correct in respect to 'To be applied'","Regex Error");
        }
      }
      else{
        crudService.add(ngAppSettings.LocatorIdentifierUrl, $scope.LocatorIdentifier).then(function (response) {
          $state.go("Website.PagesLocatorIdentifier", {
            WebsiteId: $scope.stateParamWebsiteId,
            PageId: $stateParams.PageId
          });
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.loadData = function (loadType) {
      if (loadType == undefined || loadType == 2) // Add Edit
      {
        crudService.getAll(ngAppSettings.LocatorUrl).then(function (response) {
          $scope.LocatorList = response;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }

      dataProvider.currentWebSite($scope);
      dataProvider.currentPage($scope);
    };
  }]);
