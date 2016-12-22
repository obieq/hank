/**
 * Created by vyom.sharma on 17-05-2016.
 */

'use strict';

app.controller('SharedTestDataController', ['$scope', '$q', '$stateParams', '$state', '$filter', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider', '$route',
  function ($scope, $q, $stateParams, $state, $filter, crudService, ngAppSettings, commonUi, dataProvider, $route) {

    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.SharedTestCases);
    $scope.SharedTestData = {
      ExecutionSequence: parseInt($stateParams.ExecutionSequence),
      SharedTestId: $stateParams.SharedTestId,
      ApiTestData: {}
    };

    $scope.LastSeqNumber = 1;
    $scope.InputControlDisplayStatus = {
      'ddlAction': false,
      'ddlPage': false,
      'ddlDisplayName': false,
      'txtValue': false,
      'txtAutoCompVariableName': false,
      'chkAssignVariableValue': false,
      'chkSkipByDefault': false,
      'chkOptional': false
    };

    $scope.StepTypes = [{
      'Id': 0,
      'Type': 'Test Step'
    }, {
      'Id': 3,
      'Type': 'SQL Test Step'
    }, {
      'Id': 4,
      'Type': 'Api Test Step'
    }];

    $scope.DataBaseCategories = [];
    $scope.SharedTest = {Id: $stateParams.SharedTestId};
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.SharedTestDataList = [];
    $scope.ActionConstants = {};
    $scope.PagesList = [];
    $scope.DisplayNameList = [];
    $scope.ActionList = [];
    $scope.VariableList = [];
    $scope.stateParamSharedTestDataId = $stateParams.TestDataId;

    dataProvider.currentWebSite($scope);
    //dataProvider.currentTest($scope);
    dataProvider.currentSharedTest($scope);

    $scope.onLoadList = function () {
      $scope.loadDataForList().then(function () {
        $scope.LastSeqNumber = $scope.SharedTestDataList.length <= 0 ? 1 : $scope.SharedTestDataList.length + 1;
      });
    };


    $scope.loadDataForList = function () {
      var deferred = $q.defer();
      var promises = [];
      promises.push(crudService.getAll(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId, $stateParams.SharedTestId)).then(function (response) {
        $scope.SharedTestDataList = response;
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


    $scope.addUpdateSharedTestData = function () {
      if ($scope.stateParamSharedTestDataId) {
        $scope.updateSharedTestData();
      }
      else {
        $scope.addSharedTestData();
      }
    };

    $scope.onLoadAdd = function () {
      $scope.loadDataForAdd().then(function () {
        //code goes here...
      });
    };


    $scope.loadDataForAdd = function () {
      var deferred = $q.defer();
      var promises = [];
      /* promises.push(crudService.getAll(ngAppSettings.WebsiteGetVariableForAutoComplete.format($stateParams.WebsiteId)).then(function (response) {
       $scope.VariableList = response;
       }, function (response) {
       }));*/
      /* promises.push(crudService.getAll(ngAppSettings.ActionUrl).then(function (response) {
       $scope.ActionList = response;
       $scope.InputControlDisplayStatus.ddlAction = true;
       }, function (response) {
       commonUi.showErrorPopup(response);
       }));*/
      promises.push(crudService.getById(ngAppSettings.ActionConstantUrl).then(function (response) {
        $scope.ActionConstants = response.Item;
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

    $scope.addSharedTestData = function () {
      $scope.SharedTestData.DayTillPastByDateCbx = !!$scope.SharedTestData.DayTillPastByDateCbx;
      crudService.add(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId, $stateParams.SharedTestId), $scope.SharedTestData).then(function (response) {
        $state.go("Website.SharedTestData", {
          WebsiteId: $scope.stateParamWebsiteId,
          SharedTestId: $scope.SharedTest.Id
        });
      });
    };


    $scope.onLoadEdit = function () {
      $scope.resetAllInputControlDisplayStatus();
      $scope.resetModel();
      $scope.loadDataForEdit().then(function () {
        $scope.InputControlDisplayStatus.chkSkipByDefault = true;
        $scope.InputControlDisplayStatus.chkOptional = true;
        switch ($scope.SharedTestData.StepType) {
          case 0:
          {
            $scope.InputControlDisplayStatus.ddlAction = true;
            if ($scope.SharedTestData.ActionId == $scope.ActionConstants.WaitActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.LoadPartialUrlActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.AssertUrlToContainActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.HandleBrowserAlertPopupActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.LoadReportDataActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.MarkLoadDataFromReportActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWindowActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.TerminateTestActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchToDefaultContentActionId) {
            }
            else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableActionId) {
              $scope.InputControlDisplayStatus.ddlPage = true;
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              $scope.InputControlDisplayStatus.ddlDisplayName = true;
            }
            else if (($scope.SharedTestData.ActionId == $scope.ActionConstants.AssertToEqualActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.AssertToContainActionId) && $scope.SharedTestData.LocatorIdentifierId == undefined) {
              $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
              if ($scope.SharedTestData.VariableName != null && !!$scope.SharedTestData.VariableName.trim()) {
                $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
                $scope.SharedTestData.IsAssignVariableName = true;
                $scope.InputControlDisplayStatus.txtValue = true;
                $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              }
            }
            else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SendKeyActionId && $scope.SharedTestData.LocatorIdentifierId == undefined) {
              $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else {
              $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
              $scope.InputControlDisplayStatus.txtValue = true;
              $scope.InputControlDisplayStatus.ddlPage = true;
              $scope.InputControlDisplayStatus.ddlPageNonValidation = false;
              $scope.InputControlDisplayStatus.ddlDisplayName = true;
              if (!!$scope.SharedTestData.VariableName.trim()) {
                $scope.SharedTestData.IsAssignVariableName = true;
                $scope.InputControlDisplayStatus.txtValue = false;
                $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              }
            }
            break;
          }
          case 3:
          {
            crudService.getAll(ngAppSettings.WebsiteDataBaseCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
              $scope.DataBaseCategories = response;
              crudService.getAll(ngAppSettings.ActionForSqlTestStep).then(function (response) {
                $scope.ActionList = response;
              });
            }, function (response) {
              commonUi.showErrorPopup(response);
            });
          }
        }
      });
    };

    $scope.onWebsiteChange = function () {
      if ($scope.SharedTestData.ActionId == $scope.ActionConstants.LoadReportDataActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.MarkLoadDataFromReportActionId) {
        crudService.getAll(ngAppSettings.WebSiteTestCatUrl.format($scope.SharedTestData.ReportDataWebsiteId)).then(function (response) {
          $scope.TestCategoryList = response;
          $scope.TestCategoryList.splice(0, 0, {"Id": "0", "Name": "--View All--"});
        }, function (response) {
        });
      }
    };

    $scope.onTestCategoryChange = function () {
      crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format($scope.SharedTestData.ReportDataWebsiteId, $scope.SharedTestData.ReportDataCategoryId)).then(function (response) {
        $scope.TestList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadDataForEdit = function () {
      var deferred = $q.defer();
      var promises = [];

      dataProvider.currentWebSite($scope);
      //dataProvider.currentTest($scope);

      promises.push(crudService.getById(ngAppSettings.ActionConstantUrl).then(function (response) {
        $scope.ActionConstants = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));

      promises.push(crudService.getAll(ngAppSettings.ActionUrl).then(function (response) {
        $scope.ActionList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));
      promises.push(crudService.getById(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId, $stateParams.SharedTestId), $stateParams.TestDataId).then(function (response) {
        $scope.SharedTestData = response.Item;
        $scope.SharedTestData.DayTillPastByDate = !!$scope.SharedTestData.DayTillPastByDate ? $scope.SharedTestData.DayTillPastByDate.dateFormat($filter) : '';
        switch ($scope.SharedTestData.StepType) {
          case 0:
          {
            if ($scope.SharedTestData.ActionId != $scope.ActionConstants.DeclareVariableActionId &&
              $scope.SharedTestData.ActionId != $scope.ActionConstants.TakeScreenShotActionId &&
              $scope.SharedTestData.ActionId != $scope.ActionConstants.SetVariableManuallyActionId &&
              $scope.SharedTestData.ActionId != $scope.ActionConstants.LoadReportDataActionId &&
              $scope.SharedTestData.ActionId != $scope.ActionConstants.MarkLoadDataFromReportActionId) {
              promises.push(crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
                $scope.PagesList = response;
              }, function (response) {
                commonUi.showErrorPopup(response);
              }));

              promises.push(crudService.getAll(ngAppSettings.LocatorIdentifierUrl.format($stateParams.WebsiteId, $scope.SharedTestData.PageId)).then(function (response) {
                $scope.DisplayNameList = response;
              }, function (response) {
                commonUi.showErrorPopup(response);
              }));
            }
            else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.LoadReportDataActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.MarkLoadDataFromReportActionId) {
              promises.push(crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
                $scope.WebsiteList = response;
              }, function (response) {
                commonUi.showErrorPopup(response);
              }));
              promises.push(crudService.getAll(ngAppSettings.WebSiteTestCatUrl.format($scope.SharedTestData.ReportDataWebsiteId)).then(function (response) {
                $scope.TestCategoryList = response;
              }, function (response) {
              }));
              promises.push(crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format($scope.SharedTestData.ReportDataWebsiteId, $scope.SharedTestData.ReportDataCategoryId)).then(function (response) {
                $scope.TestList = response;
              }, function (response) {
              }));
            }
            break;
          }
          case 1:
          {
            promises.push(crudService.getAll(ngAppSettings.WebSiteSharedTestUrl.format($stateParams.WebsiteId)).then(function (response) {
              $scope.SharedTestList = response;
            }, function (response) {
              commonUi.showErrorPopup(response);
            }));
            break;
          }
          case 2:
          {
            promises.push(crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
              $scope.WebsiteList = response;
            }, function (response) {
              commonUi.showErrorPopup(response);
            }));

            promises.push(crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format($scope.SharedTestData.ReportDataWebsiteId, 0)).then(function (response) {
              $scope.TestList = response;
            }, function (response) {
              //commonUi.showErrorPopup(response);
            }));
            break;
          }
          case 4:
          {
            promises.push(crudService.getAll(ngAppSettings.WebsiteApiCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
              $scope.ApiCategories = response;
            }, function (response) {
              commonUi.showErrorPopup(response);
            }));

            promises.push(crudService.getAll(ngAppSettings.RequestTypeUrl).then(function (response) {
              $scope.RequestTypeList = response;
              $scope.onRequestTypeChange();
            }, function (response) {
              commonUi.showErrorPopup(response);
            }));
          }
        }
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

    $scope.updateSharedTestData = function () {
      if ($stateParams.ExecutionSequence < $scope.SharedTestData.ExecutionSequence) {
        $scope.SharedTestData.ExecutionSequence = $stateParams.ExecutionSequence;
      }

      $scope.SharedTestData.PageId = $scope.InputControlDisplayStatus.ddlPage ? $scope.SharedTestData.PageId : 0;
      $scope.SharedTestData.LocatorIdentifierId = $scope.InputControlDisplayStatus.ddlDisplayName ? $scope.SharedTestData.LocatorIdentifierId : null;

      crudService.update(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId, $stateParams.SharedTestId), $scope.SharedTestData).then(function (response) {
        $state.go("Website.SharedTestData", {
          WebsiteId: $scope.stateParamWebsiteId,
          SharedTestId: $scope.SharedTest.Id
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.deleteSharedTestData = function (sharedTestDataId) {
      if (confirm("Are you sure u want to delete step?")) {
        crudService.delete(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId, $stateParams.SharedTestId), {'Id': sharedTestDataId}).then(function (response) {
          $scope.onLoadList();
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };


    $scope.onStepTypeChange = function () {
      var LinkTestType = $scope.SharedTestData.StepType;
      $scope.resetModel();
      $scope.SharedTestData.StepType = LinkTestType;
      switch (LinkTestType) {
        case 0:
        {
          $scope.resetAllInputControlDisplayStatus();
          crudService.getAll(ngAppSettings.ActionUrl).then(function (response) {
            $scope.ActionList = response;
            $scope.InputControlDisplayStatus.ddlAction = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
          break;
        }
        case 3:
        {
          $scope.resetAllInputControlDisplayStatus();
          crudService.getAll(ngAppSettings.WebsiteDataBaseCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.DataBaseCategories = response;
            crudService.getAll(ngAppSettings.ActionForSqlTestStep).then(function (response) {
              $scope.ActionList = response;
            });
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
          break;
        }
        case 4:
        {
          $scope.resetAllInputControlDisplayStatus();
          crudService.getAll(ngAppSettings.WebsiteApiCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.ApiCategories = response;
            $scope.SharedTestData.ApiTestData.Headers = [{}];
            $scope.SharedTestData.ApiTestData.IgnoreHeaders = [{}];
            crudService.getAll(ngAppSettings.RequestTypeUrl).then(function (response) {
              $scope.RequestTypeList = response;
            });
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
          break;
        }
      }
    };

    $scope.onActionChange = function () {
      var actionId = $scope.SharedTestData.ActionId;
      var executionSequence = $scope.SharedTestData.ExecutionSequence;
      $scope.resetModel('onActionChange');
      $scope.SharedTestData.ExecutionSequence = executionSequence;
      $scope.SharedTestData.ActionId = actionId;
      if ($scope.SharedTestData.StepType == 0) {
        $scope.resetAllInputControlDisplayStatus();
        $scope.InputControlDisplayStatus.chkOptional = true;
        $scope.InputControlDisplayStatus.chkSkipByDefault = true;
        $scope.InputControlDisplayStatus.ddlAction = true;
        $scope.InputControlDisplayStatus.txtValue = true;
        if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
          $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
          $scope.InputControlDisplayStatus.txtValue = $scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId ? true : false;
        }
        else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.WaitActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.AssertUrlToContainActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.HandleBrowserAlertPopupActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.LoadPartialUrlActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchFrameActionId) {
          $scope.InputControlDisplayStatus.txtValue = true;
        }
        else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWindowActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.TerminateTestActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.AssertUrlToContainActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchToDefaultContentActionId) {
          $scope.InputControlDisplayStatus.txtValue = false;
        }
        else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableActionId) {
          crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.PagesList = response;
            $scope.InputControlDisplayStatus.ddlPage = true;
            $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
            $scope.InputControlDisplayStatus.txtValue = false;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
        else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.AssertToEqualActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.AssertToContainActionId) {
          crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
            $scope.PagesList = response;
            $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
        else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SendKeyActionId) {
          crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.PagesList = response;
            $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
        else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.LoadReportDataActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.MarkLoadDataFromReportActionId) {
          crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
            $scope.WebsiteList = response;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }

        else {
          crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
            $scope.PagesList = response;
            $scope.InputControlDisplayStatus.ddlPage = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
      }
    };

    $scope.onPageChange = function () {
      crudService.getAll(ngAppSettings.LocatorIdentifierUrl.format($stateParams.WebsiteId, $scope.SharedTestData.PageId)).then(function (response) {
        $scope.DisplayNameList = response;
        $scope.InputControlDisplayStatus.ddlDisplayName = true;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onAssignVariableClick = function () {
      if ($scope.SharedTestData.IsAssignVariableName) {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        $scope.InputControlDisplayStatus.txtValue = ($scope.SharedTestData.ActionId == $scope.ActionConstants.AssertToEqualActionId || $scope.SharedTestData.ActionId == $scope.ActionConstants.AssertToContainActionId ) && $scope.SharedTestData.PageId == undefined ? true : false;

      }
      else {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = false;
        $scope.InputControlDisplayStatus.txtValue = true;
        $scope.SharedTestData.VariableName = null;
      }
    };

    $scope.resetAllInputControlDisplayStatus = function () {
      $scope.InputControlDisplayStatus = {
        'ddlAction': false,
        'ddlPage': false,
        'ddlDisplayName': false,
        'txtValue': false,
        'txtAutoCompVariableName': false,
        'chkAssignVariableValue': false,
        'chkSkipByDefault': false,
        'chkOptional': false
      };
    };

    $scope.resetModel = function (event) {
      var linkTestType = $scope.SharedTestData.StepType;
      $scope.SharedTestData = {};
      $scope.SharedTestData = {
        Id: $stateParams.TestDataId,
        ExecutionSequence: parseInt($stateParams.ExecutionSequence),
        SharedTestId: $stateParams.SharedTestId,
        ApiTestData: {}
      };
      $scope.SharedTestData.StepType = event == 'onActionChange' ? linkTestType : undefined;
    };

    $scope.removeHeader = function (headerIndex, type) {
      if (type == 1) {
        $scope.SharedTestData.ApiTestData.Headers = $scope.SharedTestData.ApiTestData.Headers || [];
        $scope.SharedTestData.ApiTestData.Headers.splice(headerIndex, 1);
      }
      else if (type == 2) {
        $scope.SharedTestData.ApiTestData.IgnoreHeaders = $scope.SharedTestData.ApiTestData.IgnoreHeaders || [];
        $scope.SharedTestData.ApiTestData.IgnoreHeaders.splice(headerIndex, 1);
      }
    };

    $scope.onRequestTypeChange = function () {
      var request = _.where($scope.RequestTypeList, {'Id': $scope.SharedTestData.ApiTestData.RequestTypeId});
      $scope.IsRequestBodyAllowed = request && request[0] ? request[0].IsRequestBodyAllowed : false;
    };

    $scope.addBlankHeader = function (type) {
      if (type == 1) {
        $scope.SharedTestData.ApiTestData.Headers = $scope.SharedTestData.ApiTestData.Headers || [];
        $scope.SharedTestData.ApiTestData.Headers.push({});
      }
      else if (type == 2) {
        $scope.SharedTestData.ApiTestData.IgnoreHeaders = $scope.SharedTestData.ApiTestData.IgnoreHeaders || [];
        $scope.SharedTestData.ApiTestData.IgnoreHeaders.push({});
      }
    };

    $scope.onDayTillPastByDateCbxClick = function () {

      $('.form_datetime').datetimepicker({
        endDate: '+0d',
        format: 'mm-dd-yyyy',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
      });


    };


  }
])
;
