/**
 * Created by vyom.sharma on 10-08-2015.
 */

'use strict';

app.controller('Shared_Test_Data_Controller', ['$scope', '$q', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider', '$route',
  function ($scope, $q, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider, $route) {

    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope,$stateParams.WebsiteId,ngAppSettings.ModuleType.SharedTestCases);
    $scope.SharedTestData = {
      ExecutionSequence: parseInt($stateParams.ExecutionSequence),
      SharedTestId: $stateParams.SharedTestId
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

    $scope.DataBaseCategories = [];
    $scope.SharedTest = {Id: $stateParams.SharedTestId};
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.SharedTestDataList = [];
    $scope.ActionConstants = {};
    $scope.PagesList = [];
    $scope.DisplayNameList = [];
    $scope.ActionList = [];
    $scope.VariableList = [];

    $scope.onLoadList = function () {
      $scope.loadDataForList().then(function () {
        $scope.LastSeqNumber = $scope.SharedTestDataList.length <= 0 ? 1 : $scope.SharedTestDataList.length + 1;
      });
    };

    $scope.onLoadAdd = function () {
      $scope.loadDataForAdd().then(function () {
        //code goes here...
      });
    };

    $scope.onLoadEdit = function () {
      $scope.resetAllInputControlDisplayStatus();
      $scope.loadDataForEdit().then(function () {
        if ($scope.SharedTestData.StepType == 3) {
          $scope.IsSqlTestStep = true;
          crudService.getAll(ngAppSettings.WebsiteDataBaseCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.DataBaseCategories = response;
            if ($scope.VariableList.length == 0) {
              crudService.getAll(ngAppSettings.TestDataGetVariableForAutoComplete.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
                $scope.VariableList = response;
              }, function (response) {
              });
            }
            crudService.getAll(ngAppSettings.ActionForSqlTestStep).then(function (response) {
              $scope.ActionList = response;
            });
            $scope.InputControlDisplayStatus.chkSkipByDefault = true;
            $scope.InputControlDisplayStatus.chkOptional = true;
            $scope.InputControlDisplayStatus.ddlDataBaseCategory = true;
            $scope.InputControlDisplayStatus.ddlSQLAction = true;
            $scope.InputControlDisplayStatus.ddlAction = false;
            $scope.InputControlDisplayStatus.txtSQLQuery = true;
            $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
        else {
          $scope.InputControlDisplayStatus.chkOptional = true;
          $scope.InputControlDisplayStatus.chkSkipByDefault = true;
          $scope.InputControlDisplayStatus.ddlAction = true;
          if ($scope.SharedTestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
            $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId) {
            $scope.InputControlDisplayStatus.txtValue = true;
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.WaitActionId) {
            $scope.InputControlDisplayStatus.txtValue = true;
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId) {
            $scope.InputControlDisplayStatus.txtValue = true;
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId) {
            $scope.InputControlDisplayStatus.txtValue = true;
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId) {
            $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
            $scope.InputControlDisplayStatus.txtValue = true;
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWindowActionId) {
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId) {
          }
          else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableActionId) {
            $scope.InputControlDisplayStatus.ddlPage = true;
            $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
            $scope.InputControlDisplayStatus.ddlDisplayName = true;
          }
          else {
            $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
            $scope.InputControlDisplayStatus.txtValue = true;
            $scope.InputControlDisplayStatus.ddlPage = true;
            $scope.InputControlDisplayStatus.ddlDisplayName = true;
            if ($scope.SharedTestData.VariableName != null && !!$scope.SharedTestData.VariableName.trim()) {
              $scope.SharedTestData.IsAssignVariableName = true;
              $scope.InputControlDisplayStatus.txtValue = false;
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
            }
          }
        }
      });
    };


    $scope.onActionChange = function () {
      $scope.resetActionDependentInputControlDisplayStatus();
      $scope.InputControlDisplayStatus.chkOptional = true;
      $scope.InputControlDisplayStatus.chkSkipByDefault = true;
      if ($scope.SharedTestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.WaitActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId) {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SwitchWindowActionId) {
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId) {
      }
      else if ($scope.SharedTestData.ActionId == $scope.ActionConstants.SetVariableActionId) {

        crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {

          $scope.PagesList = response;
          $scope.InputControlDisplayStatus.ddlPage = true;
          $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
      else {
        crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
          $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
          $scope.PagesList = response;
          $scope.InputControlDisplayStatus.txtValue = true;
          $scope.InputControlDisplayStatus.ddlPage = true;

        }, function (response) {
          commonUi.showErrorPopup(response);
        });

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
        $scope.InputControlDisplayStatus.txtValue = false;
      }
      else {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = false;
        $scope.InputControlDisplayStatus.txtValue = true;
        $scope.SharedTestData.VariableName = null;
      }
    };

    $scope.onSharedTestLinkClick = function (id) {
      for (var i = 0; i < $scope.TestDataList.length; i++) {
        if ($scope.TestDataList[i].Id == id) {
          $scope.TestDataList[i].ShowSharedTest = $scope.TestDataList[i].ShowSharedTest ? false : true;
          break;
        }
      }
    };


    $scope.addSharedTestData = function () {
      crudService.add(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId,$stateParams.SharedTestId), $scope.SharedTestData).then(function (response) {
        $state.go("Website.SharedTestData", {
          WebsiteId: $scope.stateParamWebsiteId,
          SharedTestId: $scope.SharedTest.Id
        });
      });
    };

    $scope.updateSharedTestData = function () {
      if ($stateParams.ExecutionSequence < $scope.SharedTestData.ExecutionSequence) {
        $scope.SharedTestData.ExecutionSequence = $stateParams.ExecutionSequence;
      }
      crudService.update(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId,$stateParams.SharedTestId), $scope.SharedTestData).then(function (response) {
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
        crudService.delete(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId,$stateParams.SharedTestId), {'Id': sharedTestDataId}).then(function (response) {
          $scope.onLoadList();
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };


    $scope.loadDataForList = function () {
      var deferred = $q.defer();
      var promises = [];

      dataProvider.currentWebSite($scope);
      dataProvider.currentTest($scope);
      dataProvider.currentSharedTest($scope);

      promises.push(crudService.getAll(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId,$stateParams.SharedTestId)).then(function (response) {
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

    $scope.loadDataForAdd = function () {
      var deferred = $q.defer();
      var promises = [];

      dataProvider.currentWebSite($scope);
      dataProvider.currentTest($scope);
      dataProvider.currentSharedTest($scope);

      crudService.getAll(ngAppSettings.WebsiteGetVariableForAutoComplete.format($stateParams.WebsiteId)).then(function (response) {
        $scope.VariableList = response;
      }, function (response) {

      });

      promises.push(crudService.getAll(ngAppSettings.ActionUrl).then(function (response) {
        $scope.ActionList = response;
        $scope.InputControlDisplayStatus.ddlAction = true;
      }, function (response) {
        commonUi.showErrorPopup(response);
      }));

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

    $scope.loadDataForEdit = function () {
      var deferred = $q.defer();
      var promises = [];

      dataProvider.currentWebSite($scope);
      dataProvider.currentTest($scope);
      dataProvider.currentSharedTest($scope);

      crudService.getAll(ngAppSettings.WebsiteGetVariableForAutoComplete.format($stateParams.WebsiteId)).then(function (response) {
        $scope.VariableList = response;
      }, function (response) {

      });

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

      promises.push(crudService.getById(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId,$stateParams.SharedTestId), $stateParams.TestDataId).then(function (response) {

        $scope.SharedTestData = response.Item;
        if ($scope.SharedTestData.ActionId != $scope.ActionConstants.DeclareVariableActionId &&
          $scope.SharedTestData.ActionId != $scope.ActionConstants.TakeScreenShotActionId &&
          $scope.SharedTestData.ActionId != $scope.ActionConstants.SetVariableManuallyActionId) {
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


    $scope.resetActionDependentInputControlDisplayStatus = function () {
      $scope.InputControlDisplayStatus.txtValue = false;
      $scope.InputControlDisplayStatus.txtAutoCompVariableName = false;
      $scope.InputControlDisplayStatus.ddlPage = false;
      $scope.InputControlDisplayStatus.ddlDisplayName = false;
      $scope.InputControlDisplayStatus.chkAssignVariableValue = false;
    };

    $scope.resetAllInputControlDisplayStatus = function () {
      $scope.InputControlDisplayStatus = {
        'ddlAction': false,
        'ddlPage': false,
        'ddlDisplayName': false,
        'txtValue': false,
        'txtAutoCompVariableName': false,
        'ddlSharedTestList': false,
        'ddlWebsiteList': false,
        'ddlTestList': false,
        'chkAssignVariableValue': false
      };
    };

    $scope.onIsSqlTestStepClick = function () {
      if ($scope.IsSqlTestStep) {
        $scope.resetAllInputControlDisplayStatus();
        crudService.getAll(ngAppSettings.WebsiteDataBaseCategoriesUrl.format($stateParams.WebsiteId)).then(function (response) {
          $scope.DataBaseCategories = response;
          if ($scope.VariableList.length == 0) {
            crudService.getAll(ngAppSettings.TestDataGetVariableForAutoComplete.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
              $scope.VariableList = response;
            }, function (response) {
            });
          }
          crudService.getAll(ngAppSettings.ActionForSqlTestStep).then(function (response) {
            $scope.ActionList = response;
          });
          $scope.InputControlDisplayStatus.chkSkipByDefault = true;
          $scope.InputControlDisplayStatus.chkOptional = true;
          $scope.InputControlDisplayStatus.ddlDataBaseCategory = true;
          $scope.InputControlDisplayStatus.ddlSQLAction = true;
          $scope.InputControlDisplayStatus.ddlAction = false;
          $scope.InputControlDisplayStatus.txtSQLQuery = true;
          $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
      else {
        window.location.reload();
      }

    };

    $scope.onIsSqlTestStepClickOnEdit = function () {
      $scope.IsSqlTestStep = true;
      commonUi.showMessagePopup("You Can't change the step type here to change it please delete and Re-Add the step", "Not allowed")
    }

  }]);
