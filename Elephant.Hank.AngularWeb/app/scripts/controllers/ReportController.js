/**
 * Created by vyom.sharma on 28-05-2015.
 */

'use strict';

app.controller('ReportController', ['$scope', '$rootScope', '$filter', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider', '$q', '$location',
  function ($scope, $rootScope, $filter, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider, $q, $location) {

    $scope.SectionOpen = true;
    $scope.ReportDetail = {};
    $scope.ReportList = [];
    $scope.Faulted = 0;
    $scope.Passed = 0;
    $scope.StateParamsWebsiteId = 0;
    $scope.CurrentDate = new Date().dateFormat($filter, true);
    $scope.DefaultOption = {Id: "0000", Value: "All"};
    $scope.SearchCriteriaData = {};
    $scope.Total = 0;
    $scope.StartNum = 0;
    $scope.EndNum = 0;
    $scope.CurrentPage = 1;
    $scope.PageSize = ngAppSettings.PageSize;
    $scope.date = {};

    $scope.PageSizes = [
      {Name: "10", Value: 10},
      {Name: "20", Value: 20},
      {Name: "40", Value: 40},
      {Name: "60", Value: 60},
      {Name: "80", Value: 80},
      {Name: "100", Value: 100}
    ];

    dataProvider.currentWebSite($scope);
    var queryParams = $location.search();
    if (($stateParams.CreatedOn == '' || $stateParams.CreatedOn == undefined) && (!queryParams.StartDate && !queryParams.EndDate && !queryParams.State)) {
      $scope.date.endDate = moment();
      $scope.date.startDate = moment();
      $scope.SearchObject = {
        'StartDate': $scope.date.startDate.format("MM-DD-YYYY"),
        'EndDate': $scope.date.endDate.format("MM-DD-YYYY")
      };
    }
    else if (!!queryParams.StartDate && !!queryParams.EndDate) {
      $scope.date.startDate = moment(queryParams.StartDate).format('MM-DD-YYYY');
      $scope.date.endDate = moment(queryParams.EndDate).format('MM-DD-YYYY');
      $scope.SearchObject = {
        'StartDate': queryParams.StartDate,
        'EndDate': queryParams.EndDate,
        'TestStatus': queryParams.State
      };
    }
    else {
      $scope.SearchObject = {'executiongroup': $stateParams.CreatedOn};
    }

    $scope.loadDataSearchData = function () {
      crudService.getById(ngAppSettings.SearchReportUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.SearchCriteriaData = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.searchReport = function () {
      if ($scope.CurrentPage != 1) {
        $scope.CurrentPage = 1;
      } else {
        $scope.getReport();
      }

    };

    $scope.getReport = function (pageNum) {
      $scope.SearchObject.ExtraData = false;
      $scope.StateParamsWebsiteId = $scope.SearchObject.WebsiteId = $stateParams.WebsiteId;
      $scope.SearchObject.PageSize = $scope.PageSize;
      $scope.SearchObject.PageNum = pageNum || 1;
      $scope.SearchObject.StartDate = !!$scope.date.startDate ? $scope.date.startDate.format('MM-DD-YYYY') : undefined;
      $scope.SearchObject.EndDate = !!$scope.date.endDate ? !!$scope.date.endDate.format('MM-DD-YYYY') : undefined;
      crudService.search(ngAppSettings.SearchReportUrl.format($stateParams.WebsiteId), $scope.SearchObject).then(function (response) {
        $scope.Total = response.Total;
        $scope.PageSize = response.PageSize;

        $scope.Passed = response.Item.CountPassed;
        $scope.Faulted = response.Item.CountFailed;

        $scope.StartNum = (($scope.SearchObject.PageNum - 1) * $scope.SearchObject.PageSize) + 1;
        $scope.EndNum = $scope.StartNum + $scope.SearchObject.PageSize - 1;
        $scope.EndNum = $scope.EndNum >= $scope.Total ? $scope.Total : $scope.EndNum;
        $scope.CurrentPage = $scope.SearchObject.PageNum;
        if (pageNum == undefined) {
          $scope.SectionOpen = false;
        }

        $scope.ReportList = response.Item.Data;
      }, function (response) {
        $scope.Total = 0;
        $scope.PageSize = ngAppSettings.PageSize;
        $scope.Faulted = 0;
        $scope.Passed = 0;
        $scope.StartNum = 0;
        $scope.EndNum = 0;
        $scope.EndNum = 0;
        $scope.ReportList = [];
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getReportDetails = function () {
      $scope.StateParamsWebsiteId = $stateParams.WebsiteId;
      crudService.getById(ngAppSettings.ReportUrl.format($stateParams.WebsiteId), $stateParams.ReportId).then(function (response) {
        $scope.ReportDetail = response.Item;
        $scope.ReportDetail.JsonValue = JSON.parse($scope.ReportDetail.Value);
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


    $scope.getCsvHeaders = function () {
      return ['Sno', 'Queue', 'Suite', 'Test Name', 'OS', 'Browser', 'Status', 'QueuedBy', 'Variable Name', 'Variable Value', 'Log Key', 'Log Value'];
    };

    $scope.getCsvData = function () {
      var deferred = $q.defer();
      $scope.SearchObject.ExtraData = true;
      $scope.SearchObject.PageSize = 0;

      crudService.search(ngAppSettings.SearchReportUrl.format($stateParams.WebsiteId), $scope.SearchObject).then(function (response) {
        var reportList = response.Item.Data;
        var result = [];
        var count = 0;
        for (var i = 0; i < reportList.length; i++) {
          var obj = getReportObject(reportList[i], ++count);
          var variableLength = reportList[i].JsonVariableStateContainer ? reportList[i].JsonVariableStateContainer.length : 0;
          var logLength = reportList[i].JsonLogContainer ? reportList[i].JsonLogContainer.length : 0;
          var maxLen = (variableLength > logLength ? variableLength : logLength);

          if (maxLen == 0) {
            result.push(obj);
            continue;
          }

          for (var j = 0; j < maxLen; j++) {
            if (j > 0) {
              obj = getReportObject(reportList[i], count);
            }
            if (reportList[i].JsonVariableStateContainer) {
              obj.VariableName = reportList[i].JsonVariableStateContainer[j] ? cleanDataForCsv(reportList[i].JsonVariableStateContainer[j].Name) : '';
              obj.VariableValue = reportList[i].JsonVariableStateContainer[j] ? cleanDataForCsv(reportList[i].JsonVariableStateContainer[j].Value) : '';
            }
            if (reportList[i].JsonLogContainer) {
              obj.LogName = reportList[i].JsonLogContainer[j] ? cleanDataForCsv(reportList[i].JsonLogContainer[j].Name) : '';
              obj.LogValue = reportList[i].JsonLogContainer[j] ? cleanDataForCsv(reportList[i].JsonLogContainer[j].Value) : '';
            }
            result.push(obj);
          }
        }
        deferred.resolve(result);
      }, function (response) {
        commonUi.showErrorPopup(response);
        deferred.reject();
      });
      return deferred.promise;
    };

    function getReportObject(reportObj, sno) {
      return {
        Sno: sno,
        TestQueueId: reportObj.TestQueueId,
        SuiteName: cleanDataForCsv(reportObj.SuiteName),
        TestName: cleanDataForCsv(reportObj.TestName),
        Os: reportObj.Os,
        BrowserName: reportObj.BrowserName,
        Status: reportObj.StatusText,
        QueuedBy: cleanDataForCsv(reportObj.QueuedBy)
      };

    }

    function cleanDataForCsv(valueToClean) {
      if (valueToClean) {
        return (valueToClean + "").replace("\n", " ");
      }
      return valueToClean;
    }
  }]);
