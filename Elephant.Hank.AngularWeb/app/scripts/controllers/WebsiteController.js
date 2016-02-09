/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';

app.controller('WebsiteController', ['$q', '$scope', 'CrudService', '$stateParams', '$state', 'CommonUiService', 'ngAppSettings',
  function ($q, $scope, crudService, $stateParams, $state, commonUi, ngAppSettings) {
    var baseUrl = ngAppSettings.WebSiteUrl;
    $scope.WebsiteList = [];
    $scope.Website = {WebsiteUrlList: [], IsAngular: false, Settings: {Browsers: []}};
    $scope.CheckedUrl = {};
    $scope.BrowserList = [];
    $scope.EnvironmentList = [];

    $scope.getAllWebSites = function () {
      crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
        $scope.WebsiteList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onLoadAdd = function () {
      $scope.loadDataForAdd().then(function () {
        $scope.SelectAllBrowsers = $scope.Website.WebsiteId <= 0 ? true : $scope.SelectAllBrowsers;
      }, function () {

      });
    };

    $scope.onLoadUpdate = function () {
      $scope.loadDataForUpdate().then(function(){
        for (var i = 0; i < $scope.Website.Settings.Browsers.length; i++) {
          for (var j = 0; j < $scope.BrowserList.length; j++) {
            if ($scope.Website.Settings.Browsers[i] == $scope.BrowserList[j].Id) {
              $scope.BrowserList[j].Checked = true;
              break;
            }
          }
        }

        for (var i = 0; i < $scope.Website.WebsiteUrlList.length; i++) {
          for (var j = 0; j < $scope.EnvironmentList.length; j++) {
            if ($scope.Website.WebsiteUrlList[i].Id == $scope.EnvironmentList[j].Id) {
              $scope.EnvironmentList[j].Url = $scope.Website.WebsiteUrlList[i].Url;
              break;
            }
          }
        }
      });
    };

    $scope.addWebsite = function () {
      $scope.Website.Settings.Browsers = $scope.getSelectedBrowsers();
      $scope.Website.WebsiteUrlList= $scope.getEnteredEnvironmentDetails();
      crudService.add(baseUrl, $scope.Website).then(function (response) {
        $state.go('Website.List');
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.updateWebsite = function () {
      $scope.Website.Settings.Browsers = $scope.getSelectedBrowsers();
      $scope.Website.WebsiteUrlList= $scope.getEnteredEnvironmentDetails();
      crudService.update(baseUrl, $scope.Website).then(function (response) {
        $state.go('Website.List');
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadDataForAdd = function () {
      var deferred = $q.defer();
      var promises = [];

      promises.push(crudService.getAll(ngAppSettings.BrowserUrl).then(function (response) {
        $scope.BrowserList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));

      promises.push(crudService.getAll(ngAppSettings.EnvironmentUrl).then(function (response) {
        $scope.EnvironmentList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));

      $q.all(promises).then(function () {
        deferred.resolve();
      }, function () {
        deferred.reject();
      });

      return deferred.promise;
    };

    $scope.loadDataForUpdate=function(){
      var deferred = $q.defer();
      var promises = [];

      promises.push(crudService.getAll(ngAppSettings.BrowserUrl).then(function (response) {
        $scope.BrowserList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));

      promises.push(crudService.getById(baseUrl, $stateParams.WebsiteId).then(function (response) {
        response.Item.Settings = response.Item.Settings == null ? $scope.Website.Settings : response.Item.Settings;
        $scope.Website = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));

      promises.push(crudService.getAll(ngAppSettings.EnvironmentUrl).then(function (response) {
        $scope.EnvironmentList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));

      $q.all(promises).then(function () {
        deferred.resolve();
      }, function () {
        deferred.reject();
      });

      return deferred.promise;
    };

    $scope.getEnteredEnvironmentDetails = function () {
      var result = [];
      for (var i = 0; i < $scope.EnvironmentList.length; i++) {
        result.push( {'Id': $scope.EnvironmentList[i].Id, 'Url': $scope.EnvironmentList[i].Url } );
      }
      return result;
    };

    $scope.getSelectedBrowsers = function () {
      var result = [];
      for (var i = 0; i < $scope.BrowserList.length; i++) {
        if ($scope.BrowserList[i].Checked) {
          result.push($scope.BrowserList[i].Id);
        }
      }
      return result;
    };

    $scope.markUnMarkAllBrowser = function (browserobj) {
      if (browserobj) {
        $scope.SelectAllBrowsers = $scope.SelectAllBrowsers && browserobj.Checked;
        return;
      }
      for (var i = 0; i < $scope.BrowserList.length; i++) {
        $scope.BrowserList[i].Checked = $scope.SelectAllBrowsers;
      }
    };

  }]);
