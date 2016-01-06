/**
 * Created by vyom.sharma on 28-05-2015.
 */

'use strict';

app.controller('ReportController', ['$scope', '$rootScope', '$filter', '$location', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $filter, $location, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.ReportDetail = {};
    $scope.MasterReportList = [];
    $scope.ReportList = [];
    $scope.faulted = 0;
    $scope.passed = 0;
    $scope.StateParamsWebsiteId = 0;
    $scope.CurrentDate = new Date().dateFormat($filter, true);
    $scope.DefaultOption = {Id: "0000", Value: "All"};
    /* $scope.searchObject = $location.search();
     var searchObjectLength = Object.keys($scope.searchObject).length;
     */
    dataProvider.currentWebSite($scope);

    if ($stateParams.CreatedOn == '' || $stateParams.CreatedOn == undefined) {
      $scope.searchObject = {'CreatedOn': $scope.CurrentDate};
    }
    else {
      $scope.searchObject = {'executiongroup': $stateParams.CreatedOn};
    }

    $scope.getReport = function () {
      debugger;
      var x = $location.search();
      $scope.StateParamsWebsiteId = $scope.searchObject.WebsiteId = $stateParams.WebsiteId;
      crudService.search(ngAppSettings.SearchReportUrl, $scope.searchObject).then(function (response) {
        for (var i = 0; i < response.Item.length; i++) {
          response.Item[i].FinishTime = (response.Item[i].FinishTime + "").formatTime();
          if (response.Item[i].Passed == false) {
            $scope.faulted++;
          }
          else if (response.Item[i].Passed) {
            $scope.passed++;
          }
        }
        $scope.ReportList = response.Item;
        $scope.MasterReportList = response.Item;
        $scope.loadData(true);
      }, function (response) {
        $scope.ReportList = [];
        $scope.MasterReportList;
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onDatePickerChange = function () {
      $scope.StateParamsWebsiteId = $scope.searchObject.WebsiteId = $stateParams.WebsiteId;
      $scope.searchObject.executiongroup = undefined;
      crudService.search(ngAppSettings.SearchReportUrl, $scope.searchObject).then(function (response) {
        for (var i = 0; i < response.Item.length; i++) {
          response.Item[i].FinishTime = (response.Item[i].FinishTime + "").formatTime();
          if (response.Item[i].Passed == false) {
            $scope.faulted++;
          }
          else if (response.Item[i].Passed) {
            $scope.passed++;
          }
        }
        $scope.ReportList = response.Item;
        $scope.MasterReportList = response.Item;
        $scope.loadData(true);
      }, function (response) {
        $scope.ReportList = [];
        $scope.MasterReportList;
        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadData = function (setDropDownToAll) {
      $scope.ExecutionGroupList = [$scope.DefaultOption];
      $scope.SuiteList = [$scope.DefaultOption];
      $scope.BrowserList = [$scope.DefaultOption];
      $scope.OperatingSystemList = [$scope.DefaultOption];
      $scope.ExecutionStatusList = [$scope.DefaultOption];
      $scope.TestList = [$scope.DefaultOption];

      $scope.lookupData($scope.ReportList, "ExecutionGroup", "ExecutionGroupList");

      if ($scope.searchObject.executiongroup == undefined) {
        $scope.ExecutionGroup = setDropDownToAll ? "All" : $scope.ExecutionGroup;
      }
      else {
        $scope.ExecutionGroup = $scope.searchObject.executiongroup;
      }

      $scope.lookupData($scope.ReportList, "SuiteName", "SuiteList");
      $scope.SuiteName = setDropDownToAll ? "All" : $scope.SuiteName;
      $scope.lookupData($scope.ReportList, "TestName", "TestList");
      $scope.TestName = setDropDownToAll ? "All" : $scope.TestName;
      $scope.lookupData($scope.ReportList, "BrowserName", "BrowserList");
      $scope.BrowserName = setDropDownToAll ? "All" : $scope.BrowserName;
      $scope.lookupData($scope.ReportList, "Os", "OperatingSystemList");
      $scope.Os = setDropDownToAll ? "All" : $scope.Os;
      $scope.lookupData($scope.ReportList, "ExecutionStatusText", "ExecutionStatusList");
      $scope.ExecutionStatusText = setDropDownToAll ? "All" : $scope.ExecutionStatusText;
    };

    $scope.lookupData = function (items, property, property1, clearArrayFirst) {
      var obj = _.uniq(items, property);
      for (var i = 0; i < obj.length; i++) {
        var value = eval("obj[i]." + property) + "";
        eval("$scope." + property1).push({Id: value, Value: value});
      }
    };

    $scope.queryReport = function () {
      var searchParam = {};
      var filters = ['SuiteName', 'ExecutionGroup', 'BrowserName', 'Os', 'ExecutionStatusText', 'TestName'];
      for (var i = 0; i < filters.length; i++) {
        if (eval('$scope.' + filters[i]) != 'All') {
          searchParam[filters[i]] = eval('$scope.' + filters[i]);
        }
      }
      $scope.ReportList = _.where($scope.MasterReportList, searchParam);
      $scope.faulted = _.where($scope.ReportList, {'Passed': false}).length;
      $scope.passed = _.where($scope.ReportList, {'Passed': true}).length;
    };

    $scope.queryReportOnSuiteSelected = function () {
      var testList = [];
      if ($scope.SuiteName == "All") {
        testList = $scope.MasterReportList;
      }
      else {
        testList = _.where($scope.MasterReportList, {'SuiteName': $scope.SuiteName});
      }
      var obj = _.uniq(testList, 'TestName');
      $scope.TestList = [$scope.DefaultOption];
      $scope.lookupData(testList, "TestName", "TestList");
      $scope.TestName = "All";

      $scope.queryReport();
    };

    $scope.getReportDetails = function () {
      $scope.StateParamsWebsiteId = $stateParams.WebsiteId;
      crudService.getById(ngAppSettings.ReportUrl, $stateParams.ReportId).then(function (response) {
        $scope.ReportDetail = response.Item;
        $scope.ReportDetail.JsonValue = JSON.parse($scope.ReportDetail.Value);
        console.log($scope.ReportDetail.JsonValue);
        $scope.ReportDetail.FinishTime = $scope.ReportDetail.FinishTime.formatTime();
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };


    $scope.onQueueClick = function (id, Os, BrowserName) {
      $rootScope.$broadcast('openTestQueuePopup', {
        'TestId': id,
        'WebsiteId': $stateParams.WebsiteId,
        'Os': Os,
        'BrowserName': BrowserName
      });
    };

  }]);
