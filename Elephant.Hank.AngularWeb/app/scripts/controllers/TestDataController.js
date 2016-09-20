/**
 * Created by vyom.sharma on 16-05-2016.
 */


app.controller('TestDataController', ['$scope', '$rootScope', '$q', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $q, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.TestScripts);

    $scope.TestData = {
      ExecutionSequence: parseInt($stateParams.ExecutionSequence),
      TestId: $stateParams.TestId,
      SharedTestDataList: [],
      ApiTestData: {}
    };
    $scope.LastSeqNumber = 1;
    $scope.InputControlDisplayStatus = {
      'ddlAction': false,
      'ddlPage': false,
      'ddlPageNonValidation': false,
      'ddlDisplayName': false,
      'txtValue': false,
      'txtAutoCompVariableName': false,
      'chkAssignVariableValue': false
    };

    crudService.getById(ngAppSettings.TestCatUrl.format($stateParams.WebsiteId), $stateParams.TestCatId).then(function (response) {
      $scope.TestCat = response.Item;
    }, function (response) {
      $scope.TestCat = {};
      $scope.TestCat.Name = "View All";
    });

    $scope.Test = {Id: $stateParams.TestId};
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.stateParamTestDataId = $stateParams.TestDataId;
    $scope.TestCatId = $stateParams.TestCatId;
    $scope.TestDataList = [];
    $scope.SharedTestList = [];
    $scope.ActionConstants = {};
    $scope.PagesList = [];
    $scope.DisplayNameList = [];
    $scope.ActionList = [];
    dataProvider.currentWebSite($scope);
    dataProvider.currentTest($scope);
    $scope.StepTypes = [{
      'Id': 0,
      'Type': 'Test Step'
    }, {
      'Id': 1,
      'Type': 'Shared Test Step'
    }, {
      'Id': 2,
      'Type': 'Website Test Step'
    }, {
      'Id': 3,
      'Type': 'SQL Test Step'
    }, {
      'Id': 4,
      'Type': 'Api Test Step'
    }];

    $scope.StepTypeCodes = ngAppSettings.StepTypes;
    $scope.WebsiteList = [];
    $scope.TestList = [];
    $scope.VariableList = [];
    $scope.DataBaseCategories = [];
    $scope.ApiCategories = [];
    $scope.RequestTypeList = [];


    $scope.addUpdateTestData = function () {
      if ($scope.stateParamTestDataId) {
        $scope.updateTestData();
      }
      else {
        $scope.addTestData();
      }
    };

    //#Start Section For Add#//

    $scope.onLoadAdd = function () {
      $scope.loadDataForAdd();
    };

    $scope.addTestData = function () {
      $scope.TestData.SharedTestSteps = [];
      if ($scope.TestData.SharedTestDataList.length > 0) {
        $scope.TestData.IsSharedTest = true;
        for (var i = 0; i < $scope.TestData.SharedTestDataList.length; i++) {
          $scope.TestData.SharedTestSteps[i] = {};
          $scope.TestData.SharedTestSteps[i].SharedTestDataId = $scope.TestData.SharedTestDataList[i].Id;
          $scope.TestData.SharedTestSteps[i].IsIgnored = $scope.TestData.SharedTestDataList[i].Ignore;
          $scope.TestData.SharedTestSteps[i].NewValue = $scope.TestData.SharedTestDataList[i].UIValue;
          $scope.TestData.SharedTestSteps[i].NewVariable = $scope.TestData.SharedTestDataList[i].UIVariableName;
          $scope.TestData.SharedTestSteps[i].NewOrder = $scope.TestData.SharedTestDataList[i].UIExecutionSequence;
        }
      }
      crudService.add(ngAppSettings.TestDataAllByTestIdUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId), $scope.TestData).then(function (response) {
        $state.go("Website.TestData", {
          WebsiteId: $scope.stateParamWebsiteId,
          TestCatId: $scope.TestCatId,
          TestId: $scope.Test.Id
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadDataForAdd = function () {
      var deferred = $q.defer();
      var promises = [];

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

    //#End Section For Add#//

    //#Start Section For Update#//
    $scope.updateTestData = function () {

      if ($stateParams.ExecutionSequence < $scope.TestData.ExecutionSequence) {
        $scope.TestData.ExecutionSequence = $stateParams.ExecutionSequence;
      }
      $scope.TestData.SharedTestSteps = [];
      if ($scope.TestData.SharedTestDataList && $scope.TestData.SharedTestDataList.length > 0) {
        $scope.TestData.IsSharedTest = true;
        for (var i = 0; i < $scope.TestData.SharedTestDataList.length; i++) {
          $scope.TestData.SharedTestSteps[i] = {};
          $scope.TestData.SharedTestSteps[i].Id = $scope.TestData.SharedTestDataList[i].LnkId;
          $scope.TestData.SharedTestSteps[i].SharedTestDataId = $scope.TestData.SharedTestDataList[i].Id;
          $scope.TestData.SharedTestSteps[i].IsIgnored = $scope.TestData.SharedTestDataList[i].Ignore;
          $scope.TestData.SharedTestSteps[i].NewValue = $scope.TestData.SharedTestDataList[i].UIValue;
          $scope.TestData.SharedTestSteps[i].NewVariable = $scope.TestData.SharedTestDataList[i].UIVariableName;
          var check1 = $scope.TestData.SharedTestDataList[i].UIVariableName;
          var check2 = $scope.TestData.SharedTestSteps[i].NewVariable;
          $scope.TestData.SharedTestSteps[i].NewOrder = $scope.TestData.SharedTestDataList[i].UIExecutionSequence;
        }
      }

      crudService.update(ngAppSettings.TestDataAllByTestIdUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId), $scope.TestData).then(function (response) {
        $state.go("Website.TestData", {
          WebsiteId: $scope.stateParamWebsiteId,
          TestCatId: $scope.TestCatId,
          TestId: $scope.Test.Id
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onLoadEdit = function () {
      $scope.resetAllInputControlDisplayStatus();
      $scope.resetModel();
      $scope.loadDataForEdit().then(function () {
        switch ($scope.TestData.LinkTestType) {
          case 0:
          {
            if ($scope.VariableList.length == 0) {
              crudService.getAll(ngAppSettings.TestDataGetVariableForAutoComplete.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
                $scope.VariableList = response;
              }, function (response) {
                //commonUi.showErrorPopup(response);
              });
            }
            $scope.InputControlDisplayStatus.ddlAction = true;

            if ($scope.TestData.ActionId == $scope.ActionConstants.WaitActionId || $scope.TestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId || $scope.TestData.ActionId == $scope.ActionConstants.LoadPartialUrlActionId || $scope.TestData.ActionId == $scope.ActionConstants.AssertUrlToContainActionId || $scope.TestData.ActionId == $scope.ActionConstants.HandleBrowserAlertPopupActionId || $scope.TestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId || $scope.TestData.ActionId == $scope.ActionConstants.SwitchWindowActionId || $scope.TestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId || $scope.TestData.ActionId == $scope.ActionConstants.TerminateTestActionId) {
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId || $scope.TestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableActionId) {
              $scope.InputControlDisplayStatus.ddlPage = true;
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              $scope.InputControlDisplayStatus.ddlDisplayName = true;
            }
            else if (($scope.TestData.ActionId == $scope.ActionConstants.AssertToEqualActionId || $scope.TestData.ActionId == $scope.ActionConstants.AssertToContainActionId) && $scope.TestData.LocatorIdentifierId == undefined) {
              $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
              if ($scope.TestData.VariableName != null && !!$scope.TestData.VariableName.trim()) {
                $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
                $scope.TestData.IsAssignVariableName = true;
                $scope.InputControlDisplayStatus.txtValue = true;
                $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              }
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.SendKeyActionId && $scope.TestData.LocatorIdentifierId == undefined) {
              $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else {
              $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
              $scope.InputControlDisplayStatus.txtValue = true;
              $scope.InputControlDisplayStatus.ddlPage = true;
              $scope.InputControlDisplayStatus.ddlPageNonValidation = false;
              $scope.InputControlDisplayStatus.ddlDisplayName = true;
              if (!!$scope.TestData.VariableName.trim()) {
                $scope.TestData.IsAssignVariableName = true;
                $scope.InputControlDisplayStatus.txtValue = false;
                $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              }
            }
            break;
          }
          case 1:
          {
            $scope.TestData.SharedTestDataList = $scope.TestData.SharedTest.SharedTestDataList;
            for (var i = 0; i < $scope.TestData.SharedTestDataList.length; i++) {
              var lnkSharedStep = _.where($scope.TestData.SharedTestSteps, {'SharedTestDataId': $scope.TestData.SharedTestDataList[i].Id})[0];
              $scope.TestData.SharedTestDataList[i].Ignore = $scope.TestData.SharedTestDataList[i].IsIgnored;
              if (lnkSharedStep != undefined && lnkSharedStep != null) {
                $scope.TestData.SharedTestDataList[i].Ignore = lnkSharedStep.IsIgnored;
                $scope.TestData.SharedTestDataList[i].UIValue = lnkSharedStep.NewValue;
                $scope.TestData.SharedTestDataList[i].UIVariableName = lnkSharedStep.NewVariable;
                $scope.TestData.SharedTestDataList[i].UIExecutionSequence = lnkSharedStep.NewOrder;
                $scope.TestData.SharedTestDataList[i].LnkId = lnkSharedStep.Id;
              }
            }
            break;
          }
          case 3:
          {
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
            }, function (response) {
              commonUi.showErrorPopup(response);
            });
          }
        }
        $scope.onRequestTypeChange();
      });
    };

    $scope.loadDataForEdit = function () {
      var deferred = $q.defer();
      var promises = [];

      dataProvider.currentWebSite($scope);
      dataProvider.currentTest($scope);

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

      promises.push(crudService.getById(ngAppSettings.TestDataAllByTestIdUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId), $stateParams.TestDataId).then(function (response) {
        $scope.TestData = response.Item;
        switch ($scope.TestData.LinkTestType) {
          case 0:
          {
            if ($scope.TestData.ActionId != $scope.ActionConstants.DeclareVariableActionId &&
              $scope.TestData.ActionId != $scope.ActionConstants.TakeScreenShotActionId &&
              $scope.TestData.ActionId != $scope.ActionConstants.SetVariableManuallyActionId) {
              promises.push(crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
                $scope.PagesList = response;
              }, function (response) {
                commonUi.showErrorPopup(response);
              }));

              promises.push(crudService.getAll(ngAppSettings.LocatorIdentifierUrl.format($stateParams.WebsiteId, $scope.TestData.PageId)).then(function (response) {
                $scope.DisplayNameList = response;
              }, function (response) {
                commonUi.showErrorPopup(response);
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

            promises.push(crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format($scope.TestData.SharedStepWebsiteId, $stateParams.TestCatId)).then(function (response) {
              $scope.TestList = response;
            }, function (response) {
              commonUi.showErrorPopup(response);
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

    //#End Section For Update#//

    $scope.getCsvHeaders = function () {
      return ['Sequence', 'Display Name', 'Action', 'Value', 'Variable Name', 'Description'];
    };

    $scope.getCsvData = function () {
      var result = [];
      var count = 1;
      for (var i = 0; i < $scope.TestDataList.length; i++) {
        if ($scope.TestDataList[i].SharedTest && $scope.TestDataList[i].SharedTest.SharedTestDataList) {
          for (var j = 0; j < $scope.TestDataList[i].SharedTest.SharedTestDataList.length; j++) {
            var testData = $scope.TestDataList[i].SharedTest.SharedTestDataList[j];
            result.push(testDataToUi(testData, count));
            count++;
          }
        }
        else {
          result.push(testDataToUi($scope.TestDataList[i], count));
          count++;
        }
      }
      console.log($scope.TestDataList)
      return result;
    };

    function testDataToUi(testData, sno) {
      var obj = {};
      obj.Sequence = sno;

      if (testData) {
        if (testData.SharedStepWebsiteName) {
          obj.DisplayName = testData.SharedStepWebsiteName + " - (" + testData.SharedStepWebsiteTestName + ")";
        }
        else {
          obj.DisplayName = testData.DisplayNameValue;
        }

        obj.Action = testData.ActionValue;
        obj.Value = testData.UIValue || testData.Value;
        obj.VariableName = testData.VariableName;
        obj.Description = testData.Description;
      }

      return obj;
    };

    //#Start Section Load List#//

    $scope.onLoadList = function () {
      $scope.loadDataForList().then(function () {
        for (var i = 0; i < $scope.TestDataList.length; i++) {
          if ($scope.TestDataList[i].LinkTestType == ngAppSettings.StepTypes.SharedTestStep) {
            $scope.TestDataList[i].rowStyle = 'background-color: #dcdcdc;';
            for (var j = 0; j < $scope.TestDataList[i].SharedTest.SharedTestDataList.length; j++) {
              var lnkSharedStep = _.where($scope.TestDataList[i].SharedTestSteps, {'SharedTestDataId': $scope.TestDataList[i].SharedTest.SharedTestDataList[j].Id})[0];
              $scope.TestDataList[i].SharedTest.SharedTestDataList[j].Ignore = $scope.TestDataList[i].SharedTest.SharedTestDataList[j].IsIgnored;
              $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIExecutionSequence = 0;
              if (lnkSharedStep != undefined && lnkSharedStep != null) {
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].Ignore = lnkSharedStep.IsIgnored;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIValue = lnkSharedStep.NewValue;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIVariableName = lnkSharedStep.NewVariable;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIExecutionSequence = lnkSharedStep.NewOrder;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIModifiedByUserName = lnkSharedStep.ModifiedByUserName;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].LnkId = lnkSharedStep.Id;
              }
            }
          }
          else if ($scope.TestDataList[i].LinkTestType == ngAppSettings.StepTypes.WebsiteTestStep) {
            $scope.TestDataList[i].rowStyle = 'background-color: #FFFFBA;';
          }
          $scope.TestDataList[i].ShowSharedTest = false;
        }
        $scope.LastSeqNumber = $scope.TestDataList.length <= 0 ? 1 : $scope.TestDataList.length + 1;

      });
    };


    $scope.loadDataForList = function () {
      var deferred = $q.defer();
      var promises = [];

      dataProvider.currentWebSite($scope);
      dataProvider.currentTest($scope);

      promises.push(crudService.getAll(ngAppSettings.TestDataAllByTestIdUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
        $scope.TestDataList = response;
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

    //#End Section For Load List#//


    $scope.deleteTestData = function (testDataId) {
      if (confirm("Are you sure u want to delete step?")) {
        crudService.delete(ngAppSettings.TestDataAllByTestIdUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId), {Id: testDataId}).then(function (response) {
          $scope.onLoadList();
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.onStepTypeChange = function () {
      var LinkTestType = $scope.TestData.LinkTestType;
      $scope.resetModel();
      $scope.TestData.LinkTestType = LinkTestType;
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
          if ($scope.VariableList.length == 0) {
            crudService.getAll(ngAppSettings.TestDataGetVariableForAutoComplete.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
              $scope.VariableList = response;
            });
          }
          break;
        }
        case 1:
        {
          $scope.resetAllInputControlDisplayStatus();
          crudService.getAll(ngAppSettings.WebSiteSharedTestUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.SharedTestList = response;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
          break;
        }
        case 2:
        {
          $scope.resetAllInputControlDisplayStatus();
          crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
            $scope.WebsiteList = response;
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
            if ($scope.VariableList.length == 0) {
              crudService.getAll(ngAppSettings.TestDataGetVariableForAutoComplete.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
                $scope.VariableList = response;
              });
            }
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
            $scope.TestData.ApiTestData.Headers = [{}];
            $scope.TestData.ApiTestData.IgnoreHeaders = [{}];
            if ($scope.VariableList.length == 0) {
              crudService.getAll(ngAppSettings.TestDataGetVariableForAutoComplete.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
                $scope.VariableList = response;
              });
              crudService.getAll(ngAppSettings.RequestTypeUrl).then(function (response) {
                $scope.RequestTypeList = response;
              });
            }
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
          break;
        }
      }
    };

    $scope.onActionChange = function () {
      var actionId = $scope.TestData.ActionId;
      var executionSequence = $scope.TestData.ExecutionSequence;
      $scope.resetModel('onActionChange');
      $scope.TestData.ActionId = actionId;
      $scope.TestData.ExecutionSequence = executionSequence;
      if ($scope.TestData.LinkTestType == 0) {
        $scope.resetAllInputControlDisplayStatus();
        $scope.InputControlDisplayStatus.ddlAction = true;
        $scope.InputControlDisplayStatus.txtValue = true;
        if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId || $scope.TestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
          $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
          $scope.InputControlDisplayStatus.txtValue = $scope.TestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId ? true : false;
        }
        else if ($scope.TestData.ActionId == $scope.ActionConstants.WaitActionId || $scope.TestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId || $scope.TestData.ActionId == $scope.ActionConstants.AssertUrlToContainActionId || $scope.TestData.ActionId == $scope.ActionConstants.HandleBrowserAlertPopupActionId || $scope.TestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId || $scope.TestData.ActionId == $scope.ActionConstants.LoadPartialUrlActionId) {
          $scope.InputControlDisplayStatus.txtValue = true;
        }
        else if ($scope.TestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId || $scope.TestData.ActionId == $scope.ActionConstants.SwitchWindowActionId || $scope.TestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId || $scope.TestData.ActionId == $scope.ActionConstants.TerminateTestActionId) {
          $scope.InputControlDisplayStatus.txtValue = false;
        }
        else if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableActionId) {
          crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.PagesList = response;
            $scope.InputControlDisplayStatus.ddlPage = true;
            $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
            $scope.InputControlDisplayStatus.txtValue = false;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
        else if ($scope.TestData.ActionId == $scope.ActionConstants.AssertToEqualActionId || $scope.TestData.ActionId == $scope.ActionConstants.AssertToContainActionId) {
          crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
            $scope.PagesList = response;
            $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
        else if ($scope.TestData.ActionId == $scope.ActionConstants.SendKeyActionId) {
          crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.PagesList = response;
            $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
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
      crudService.getAll(ngAppSettings.LocatorIdentifierUrl.format($stateParams.WebsiteId, $scope.TestData.PageId)).then(function (response) {
        $scope.DisplayNameList = response;
        $scope.InputControlDisplayStatus.ddlDisplayName = true;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onWebsiteChange = function () {
      if ($scope.TestData.SharedStepWebsiteId > 0) {
        crudService.getAll(ngAppSettings.WebSiteTestCasesUrl.format($scope.TestData.SharedStepWebsiteId, 0)).then(function (response) {
          $scope.TestList = response;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.removeHeader = function (headerIndex, type) {
      if (type == 1) {
        $scope.TestData.ApiTestData.Headers = $scope.TestData.ApiTestData.Headers || [];
        $scope.TestData.ApiTestData.Headers.splice(headerIndex, 1);
      }
      else if (type == 2) {
        $scope.TestData.ApiTestData.IgnoreHeaders = $scope.TestData.ApiTestData.IgnoreHeaders || [];
        $scope.TestData.ApiTestData.IgnoreHeaders.splice(headerIndex, 1);
      }
    };

    $scope.addBlankHeader = function (type) {
      if (type == 1) {
        $scope.TestData.ApiTestData.Headers = $scope.TestData.ApiTestData.Headers || [];
        $scope.TestData.ApiTestData.Headers.push({});
      }
      else if (type == 2) {
        $scope.TestData.ApiTestData.IgnoreHeaders = $scope.TestData.ApiTestData.IgnoreHeaders || [];
        $scope.TestData.ApiTestData.IgnoreHeaders.push({});
      }
    };

    $scope.onSharedTestListChange = function () {
      crudService.getAll(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId, $scope.TestData.SharedTestId)).then(function (response) {
        $scope.TestData.SharedTestDataList = response;
        for (var i = 0; i < $scope.TestData.SharedTestDataList.length; i++) {
          $scope.TestData.SharedTestDataList[i].Ignore = $scope.TestData.SharedTestDataList[i].IsIgnored;
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onAssignVariableClick = function () {
      if ($scope.TestData.IsAssignVariableName) {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        $scope.InputControlDisplayStatus.txtValue = ($scope.TestData.ActionId == $scope.ActionConstants.AssertToEqualActionId || $scope.TestData.ActionId == $scope.ActionConstants.AssertToContainActionId) && $scope.TestData.PageId == undefined ? true : false;
      }
      else {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = false;
        $scope.InputControlDisplayStatus.txtValue = true;
        $scope.TestData.VariableName = null;
      }
    };

    $scope.resetAllInputControlDisplayStatus = function () {
      $scope.InputControlDisplayStatus = {
        'ddlAction': false,
        'ddlPage': false,
        'ddlPageNonValidation': false,
        'ddlDisplayName': false,
        'txtValue': false,
        'txtAutoCompVariableName': false,
        'chkAssignVariableValue': false
      };
    };

    $scope.resetModel = function (event) {
      var linkTestType = $scope.TestData.LinkTestType;
      $scope.TestData = {};
      $scope.TestData = {
        Id: $stateParams.TestDataId,
        ExecutionSequence: parseInt($stateParams.ExecutionSequence),
        TestId: $stateParams.TestId,
        SharedTestDataList: [],
        ApiTestData: {}
      };
      $scope.TestData.LinkTestType = event == 'onActionChange' ? linkTestType : undefined;
    };

    $scope.onSharedTestLinkClick = function (id) {
      for (var i = 0; i < $scope.TestDataList.length; i++) {
        if ($scope.TestDataList[i].Id == id) {
          $scope.TestDataList[i].ShowSharedTest = $scope.TestDataList[i].ShowSharedTest ? false : true;
          break;
        }
      }
    };

    $scope.onRequestTypeChange = function () {
      var request = _.where($scope.RequestTypeList, {'Id': $scope.TestData.ApiTestData.RequestTypeId});
      $scope.IsRequestBodyAllowed = request && request[0] ? request[0].IsRequestBodyAllowed : false;
    };

    $scope.onQueueClick = function () {
      $rootScope.$broadcast('openTestQueuePopup', {'TestId': $stateParams.TestId, 'WebsiteId': $stateParams.WebsiteId});
    };

    $scope.onActionChangeOnEdit = function () {

    };

  }]);
