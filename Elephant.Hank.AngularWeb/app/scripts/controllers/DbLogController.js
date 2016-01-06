/**
 * Created by vyom.sharma on 24-08-2015.
 */


'use strict';

app.controller('DbLogController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'StringComparisionService', '$timeout',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, StringComparisionService, $timeout) {
    $scope.DbLogList = [];
    $scope.TableTypeList = [];
    $scope.TableType = 'All';
    $scope.date = {startDate: null, endDate: null};
    $scope.allDataDownloaded = false;
    $scope.ComparisionObject = [];

    $scope.getAllDbLog = function () {
      var chunk = 0;
      if ($scope.DbLogList.length > 0) {
        var log = _.min($scope.DbLogList, 'Id');
        chunk = log.Id;
      }
      var startDateToSend = $scope.date.startDate != null ? $scope.date.startDate.format("MM-DD-YYYY hh:mm a") : null;
      var endDateToSend = $scope.date.endDate != null ? $scope.date.endDate.format("MM-DD-YYYY hh:mm a") : null;
      if (!$scope.allDataDownloaded) {
        crudService.search(ngAppSettings.DbLogUrl.format(chunk, $scope.TableType),
          {
            startDate: startDateToSend,
            endDate: endDateToSend
          }).then(function (response) {

            for (var k = 0; k < response.Item.length; k++) {
              var checkDuplicate = _.where($scope.DbLogList, {'Id': response.Item[k].Id});
              if (checkDuplicate.length == 0) {
                $scope.DbLogList.push(response.Item[k]);
              }
            }
            for (var i = 0; i < $scope.DbLogList.length; i++) {
              $scope.DbLogList[i].OperationType = $scope.DbLogList[i].OperationType == 4 ? 'Add' : 'Update';
              var subStrTableType = $scope.DbLogList[i].TableType.substring(0, $scope.DbLogList[i].TableType.indexOf('_'));
              $scope.DbLogList[i].TableType = subStrTableType == '' ? $scope.DbLogList[i].TableType : subStrTableType;
              var newValue = JSON.parse($scope.DbLogList[i].NewValue);
              var oldValue = JSON.parse($scope.DbLogList[i].PreviousValue);
              if (newValue != null && newValue != undefined) {
                var keys = Object.keys(newValue);
                $scope.DbLogList[i].ComparisionObject = [];
                for (var j = 0; j < keys.length; j++) {
                  $scope.DbLogList[i].ComparisionObject[j] = {};
                  $scope.DbLogList[i].ComparisionObject[j].NewValue = newValue[keys[j]];
                  $scope.DbLogList[i].ComparisionObject[j].PreviousValue = oldValue != null ? oldValue[keys[j]] : null;
                  $scope.DbLogList[i].ComparisionObject[j].Key = keys[j];
                  $scope.DbLogList[i].ComparisionObject[j].css = oldValue != null && newValue[keys[j]] != oldValue[keys[j]] ? 'alert-danger' : '';
                }
              }
            }
            $scope.lookupData($scope.DbLogList, "TableType", "TableTypeList");
          }, function (response) {
            if (response.data.Messages[0].Value == 'Record not found!') {
              $scope.allDataDownloaded = true;
            }
            commonUi.showErrorPopup(response);
          });
      }
    };


    $scope.showComparision = function (id) {
      for (var j = 0; j < $scope.DbLogList.length; j++) {
        if ($scope.DbLogList[j].Id == id) {
          $scope.ComparisionObject = $scope.DbLogList[j].ComparisionObject;
          break;
        }
      }
      $timeout(function () { // You might need this timeout to be sure its run after DOM render.
        $scope.jsDiff();
      }, 0, false);
      //$scope.jsDiff();
    };


    $scope.jsDiff = function () {
      $('.evalComp').each(function () {
        var f = this.getAttribute('data-prev');
        var s = this.getAttribute('data-new');
        var k = this;
        var evaluated = this.getAttribute('data-setattr');
        if (evaluated == 'false') {
          this.setAttribute('data-setattr', 'true');
          var diff = JsDiff.diffChars(f, s);
          diff.forEach(function (part) {
            // green for additions, red for deletions
            // grey for common parts
            var color = part.added ? 'green' :
              part.removed ? 'red' : 'grey';
            var span = document.createElement('span');
            span.style.color = color;
            span.appendChild(document
              .createTextNode(part.value));
            k.appendChild(span);
          });
        }

      });
    };

    $scope.rollData = function (logId, toOldValue) {
      var userConfirmInput = false;
      if (toOldValue) {
        userConfirmInput = confirm("Are you sure u want to roll back?");
      }
      else {
        userConfirmInput = confirm("Are you sure u want to roll forward?");
      }
      if (userConfirmInput) {
        crudService.add(ngAppSettings.DbLogRollDataUrl, {
          'LogId': logId,
          'ToOldValue': toOldValue
        }).then(function (response) {
          commonUi.showErrorPopup("Data is rolled successfully!", 'Success');
        }, function () {
          commonUi.showErrorPopup(response);
        });
      }
    };


    $scope.lookupData = function (items, property, property1, clearArrayFirst) {
      var obj = _.uniq(items, property);
      for (var i = 0; i < obj.length; i++) {
        var value = eval("obj[i]." + property) + "";
        var check = _.where(eval("$scope." + property1), {Id: value});
        if (check.length == 0) {
          eval("$scope." + property1).push({Id: value, Value: value});
        }
      }
    };

    $scope.queryReport = function () {
      $scope.allDataDownloaded = false;
      $scope.DbLogList = [];
      $scope.getAllDbLog();
    };

    $scope.dateSelected = function () {
      $scope.DbLogList = [];
      $scope.getAllDbLog();
    }

  }]);
