/**
 * Created by vyom.sharma on 07-09-2015.
 */

'use strict';

app.controller('ScreenShotCompareController', ['$scope', '$q', '$filter', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $q, $filter, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    $scope.CompareData = {};
    $scope.schedulerList = [];
    $scope.reportListFirst = [];
    $scope.slidesFirst = [];
    $scope.slidesSecond = [];

    $scope.reportFirst = {};
    $scope.reportSecond = {};

    dataProvider.currentWebSite($scope);

    $scope.onPageLoad = function () {

      crudService.getAll(ngAppSettings.WebSiteSchedulerUrl.format($stateParams.WebsiteId)).then(function (response) {
        debugger;
        $scope.schedulerList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onSchedulerFirstChange = function () {
      crudService.getAll(ngAppSettings.SchedulerHistoryUrl.format($stateParams.WebsiteId,$scope.CompareData.SchedulerIdFirst)).then(function (response) {
        $scope.schedulerHistoryFirstList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onSchedulerSecondChange = function () {
      crudService.getAll(ngAppSettings.SchedulerHistoryUrl.format($stateParams.WebsiteId,$scope.CompareData.SchedulerIdSecond)).then(function (response) {
        $scope.schedulerHistorySecondList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onGroupNameFirstChange = function () {
        crudService.getById(ngAppSettings.ReportByExecutionGroupWhereScreenShotArrayExistUrl.format($scope.CompareData.GroupNameFirst)).then(function (response) {
        $scope.reportFirst = response.Item;
        $scope.reportFirst.JsonValue = JSON.parse($scope.reportFirst.Value);
          $scope.reportFirst.JsonValue.Os = ($scope.reportFirst.JsonValue.Os == "XP" ? "Windows" : $scope.reportFirst.JsonValue.Os).toLowerCase();
        $scope.slidesFirst = $scope.reportFirst.JsonValue.ScreenShotArray;
        for (var i = 0; i < $scope.slidesFirst.length; i++) {
          $scope.slidesFirst[i].Value = $scope.reportFirst.ImageViewUrl + $scope.slidesFirst[i].Value;
        }
          loadSliders();
      }, function (response) {
          $scope.reportFirst={};
          $scope.slidesFirst=[];
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onGroupNameSecondChange = function () {
      crudService.getById(ngAppSettings.ReportByExecutionGroupWhereScreenShotArrayExistUrl.format($scope.CompareData.GroupNameSecond)).then(function (response) {
        $scope.reportSecond = response.Item;
        $scope.reportSecond.JsonValue = JSON.parse($scope.reportSecond.Value);
        $scope.reportSecond.JsonValue.Os = ($scope.reportSecond.JsonValue.Os == "XP" ? "Windows" : $scope.reportSecond.JsonValue.Os).toLowerCase();
        $scope.slidesSecond = $scope.reportSecond.JsonValue.ScreenShotArray;
        for (var i = 0; i < $scope.slidesSecond.length; i++) {
          $scope.slidesSecond[i].Value = $scope.reportSecond.ImageViewUrl + $scope.slidesSecond[i].Value;
        }
        loadSliders();
      }, function (response) {
        $scope.reportSecond={};
        $scope.slidesSecond=[];

        commonUi.showErrorPopup(response);
      });
    };

    $scope.getPageName = function(pageUrl) {
      var startIndex = pageUrl.lastIndexOf('/') + 1;
      if (startIndex >= pageUrl.length) {
        pageUrl = pageUrl.substring(0, pageUrl.length - 1);
        startIndex = pageUrl.lastIndexOf('/');
      }

      return startIndex >= 0 ? pageUrl.substring(startIndex + 1) : pageUrl;
    };
  }]);
