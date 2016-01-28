/**
 * Created by vyom.sharma on 05-08-2015.
 */

'use strict';

app.controller('Test_Data_Controller', ['$scope', '$rootScope', '$q', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $rootScope, $q, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    $scope.TestData = {
      ExecutionSequence: parseInt($stateParams.ExecutionSequence),
      TestId: $stateParams.TestId,
      SharedTestDataList: []
    };

    $scope.LastSeqNumber = 1;
    $scope.InputControlDisplayStatus = {
      'ddlAction': false,
      'ddlPage': false,
      'ddlPageNonValidation': false,
      'ddlDisplayName': false,
      'txtValue': false,
      'txtAutoCompVariableName': false,
      'ddlSharedTestList': false,
      'ddlWebsiteList': false,
      'ddlTestList': false,
      'chkAssignVariableValue': false,
      'ddlDataBaseCategory': false,
      'ddlSQLAction': false,
      'txtSQLQuery': false
    };
    $scope.Test = {Id: $stateParams.TestId};
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.TestCatId = $stateParams.TestCatId;

    $scope.TestDataList = [];
    $scope.SharedTestList = [];
    $scope.ActionConstants = {};
    $scope.PagesList = [];
    $scope.DisplayNameList = [];
    $scope.ActionList = [];
    $scope.StepTypes = null;
    $scope.WebsiteList = [];
    $scope.TestList = [];
    $scope.StepTypes = ngAppSettings.StepTypes;
    $scope.VariableList = [];
    $scope.DataBaseCategories = [];

    dataProvider.currentWebSite($scope);
    dataProvider.currentTestCat($scope);

    $scope.getCsvHeaders = function(){
      return ['Sequence', 'Display Name', 'Action', 'Value', 'Variable Name', 'Description'];
    };

    $scope.getCsvData = function(){
      var result = [];
      var count = 1;
      for (var i = 0; i < $scope.TestDataList.length; i++) {
        if($scope.TestDataList[i].SharedTest && $scope.TestDataList[i].SharedTest.SharedTestDataList) {
          for(var j = 0; j < $scope.TestDataList[i].SharedTest.SharedTestDataList.length; j++){
            var testData = $scope.TestDataList[i].SharedTest.SharedTestDataList[j];
            result.push(testDataToUi(testData, count));
            count++;
          }
        }
        else{
          result.push(testDataToUi($scope.TestDataList[i], count));
          count++;
        }
      }
      console.log($scope.TestDataList)
      return result;
    };

    function testDataToUi(testData, sno){
      var obj = {};
      obj.Sequence = sno;

      if(testData) {
        if(testData.SharedStepWebsiteName) {
          obj.DisplayName = testData.SharedStepWebsiteName + " - (" + testData.SharedStepWebsiteTestName + ")";
        }
        else{
          obj.DisplayName = testData.DisplayNameValue;
        }

        obj.Action = testData.ActionValue;
        obj.Value = testData.UIValue || testData.Value;
        obj.VariableName = testData.VariableName;
        obj.Description = testData.Description;
      }

      return obj;
    };

    $scope.onLoadList = function () {
      $scope.loadDataForList().then(function () {
        for (var i = 0; i < $scope.TestDataList.length; i++) {
          if ($scope.TestDataList[i].LinkTestType == $scope.StepTypes.SharedTestStep) {
            $scope.TestDataList[i].rowStyle = 'background-color: #dcdcdc;';
            for (var j = 0; j < $scope.TestDataList[i].SharedTest.SharedTestDataList.length; j++) {
              var lnkSharedStep = _.where($scope.TestDataList[i].SharedTestSteps, {'SharedTestDataId': $scope.TestDataList[i].SharedTest.SharedTestDataList[j].Id})[0];
              $scope.TestDataList[i].SharedTest.SharedTestDataList[j].Ignore = $scope.TestDataList[i].SharedTest.SharedTestDataList[j].IsIgnored;
              $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIExecutionSequence = 0;
              if (lnkSharedStep != undefined && lnkSharedStep != null) {
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].Ignore = lnkSharedStep.IsIgnored;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIValue = lnkSharedStep.NewValue;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].UIExecutionSequence = lnkSharedStep.NewOrder;
                $scope.TestDataList[i].SharedTest.SharedTestDataList[j].LnkId = lnkSharedStep.Id;
              }
            }
          }
          else if ($scope.TestDataList[i].LinkTestType == $scope.StepTypes.WebsiteTestStep) {
            $scope.TestDataList[i].rowStyle = 'background-color: #FFFFBA;';
          }
          $scope.TestDataList[i].ShowSharedTest = false;
        }
        $scope.LastSeqNumber = $scope.TestDataList.length <= 0 ? 1 : $scope.TestDataList.length + 1;
      });
    };

    $scope.onLoadAdd = function () {
      $scope.loadDataForAdd().then(function () {
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
        }
        ];
      });
    };

    $scope.onLoadEdit = function () {
      $scope.resetAllInputControlDisplayStatus();
      $scope.loadDataForEdit().then(function () {
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
        }];
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
            if ($scope.TestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.WaitActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.LoadPartialUrlActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.AssertUrlToContainActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.HandleBrowserAlertPopupActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId) {
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.SwitchWindowActionId) {
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId) {
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId) {
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              $scope.InputControlDisplayStatus.txtValue = true;
            }
            else if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableActionId) {
              $scope.InputControlDisplayStatus.ddlPage = true;
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              $scope.InputControlDisplayStatus.ddlDisplayName = true;
            }

            else if ($scope.TestData.ActionId == $scope.ActionConstants.AssertToEqualActionId && $scope.TestData.LocatorIdentifierId == undefined) {
              $scope.InputControlDisplayStatus.ddlPageNonValidation = true;
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
              $scope.InputControlDisplayStatus.txtValue = true;
            }

            else {
              $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
              $scope.InputControlDisplayStatus.txtValue = true;
              $scope.InputControlDisplayStatus.ddlPage = true;
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
            $scope.InputControlDisplayStatus.ddlSharedTestList = true;
            $scope.TestData.SharedTestDataList = $scope.TestData.SharedTest.SharedTestDataList;

            for (var i = 0; i < $scope.TestData.SharedTestDataList.length; i++) {
              var lnkSharedStep = _.where($scope.TestData.SharedTestSteps, {'SharedTestDataId': $scope.TestData.SharedTestDataList[i].Id})[0];
              $scope.TestData.SharedTestDataList[i].Ignore = $scope.TestData.SharedTestDataList[i].IsIgnored;
              if (lnkSharedStep != undefined && lnkSharedStep != null) {
                $scope.TestData.SharedTestDataList[i].Ignore = lnkSharedStep.IsIgnored;
                $scope.TestData.SharedTestDataList[i].UIValue = lnkSharedStep.NewValue;
                $scope.TestData.SharedTestDataList[i].UIExecutionSequence = lnkSharedStep.NewOrder;
                $scope.TestData.SharedTestDataList[i].LnkId = lnkSharedStep.Id;
              }
            }
            break;
          }
          case 2:
          {
            $scope.InputControlDisplayStatus.ddlWebsiteList = true;
            $scope.InputControlDisplayStatus.ddlTestList = true;
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
              $scope.InputControlDisplayStatus.ddlDataBaseCategory = true;
              $scope.InputControlDisplayStatus.ddlSQLAction = true;
              $scope.InputControlDisplayStatus.txtSQLQuery = true;
              $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
            }, function (response) {
              commonUi.showErrorPopup(response);
            });
          }
        }
      });
    };


    $scope.onStepTypeChange = function () {
      var LinkTestType = $scope.TestData.LinkTestType;
      var exeseq = $scope.TestData.ExecutionSequence;
      $scope.TestData = {
        Id: parseInt($stateParams.TestDataId),
        ExecutionSequence: parseInt(exeseq),
        TestId: $stateParams.TestId,
        SharedTestDataList: [],
        LinkTestType: LinkTestType
      };
      switch (LinkTestType) {
        case 0:
        {
          $scope.TestList = [];
          $scope.InputControlDisplayStatus.ddlSharedTestList = false;
          $scope.InputControlDisplayStatus.ddlWebsiteList = false;
          $scope.InputControlDisplayStatus.ddlTestList = false;
          $scope.resetActionDependentInputControlDisplayStatus();

          crudService.getAll(ngAppSettings.ActionUrl).then(function (response) {
            $scope.ActionList = response;
            $scope.InputControlDisplayStatus.ddlAction = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });

          if ($scope.VariableList.length == 0) {
            crudService.getAll(ngAppSettings.TestDataGetVariableForAutoComplete.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId)).then(function (response) {
              $scope.VariableList = response;
            }, function (response) {
              //commonUi.showErrorPopup(response);
            });
          }

          break;
        }
        case 1:
        {
          $scope.InputControlDisplayStatus.ddlAction = false;
          $scope.InputControlDisplayStatus.ddlWebsiteList = false;
          $scope.InputControlDisplayStatus.ddlTestList = false;
          $scope.resetActionDependentInputControlDisplayStatus();

          crudService.getAll(ngAppSettings.WebSiteSharedTestUrl.format($stateParams.WebsiteId)).then(function (response) {
            $scope.SharedTestList = response;
            $scope.InputControlDisplayStatus.ddlSharedTestList = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });

          break;
        }
        case 2:
        {
          $scope.InputControlDisplayStatus.ddlAction = false;
          $scope.InputControlDisplayStatus.ddlSharedTestList = false;
          $scope.resetActionDependentInputControlDisplayStatus();

          crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
            $scope.WebsiteList = response;
            $scope.InputControlDisplayStatus.ddlWebsiteList = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });

          break;
        }
        case 3:
        {
          $scope.InputControlDisplayStatus.ddlAction = false;
          $scope.InputControlDisplayStatus.ddlSharedTestList = false;
          $scope.resetActionDependentInputControlDisplayStatus();
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
            $scope.InputControlDisplayStatus.ddlDataBaseCategory = true;
            $scope.InputControlDisplayStatus.ddlSQLAction = true;
            $scope.InputControlDisplayStatus.txtSQLQuery = true;
            $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
      }
    };

    $scope.onActionChange = function () {
      $scope.resetActionDependentInputControlDisplayStatus();
      if ($scope.TestData.ActionId == $scope.ActionConstants.DeclareVariableActionId) {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.TakeScreenShotActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.WaitActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.LoadNewUrlActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.LoadPartialUrlActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.SwitchWebsiteTypeActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableManuallyActionId) {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.AssertUrlToContainActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.HandleBrowserAlertPopupActionId) {
        $scope.InputControlDisplayStatus.txtValue = true;
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.SwitchWindowActionId) {
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.IgnoreLoadNeUrlActionId) {
      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.SetVariableActionId) {

        crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {

          $scope.PagesList = response;
          $scope.InputControlDisplayStatus.ddlPage = true;
          $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });

      }
      else if ($scope.TestData.ActionId == $scope.ActionConstants.AssertToEqualActionId) {

        crudService.getAll(ngAppSettings.WebSitePagesUrl.format($stateParams.WebsiteId)).then(function (response) {
          $scope.InputControlDisplayStatus.chkAssignVariableValue = true;
          $scope.PagesList = response;
          $scope.InputControlDisplayStatus.txtValue = true;
          $scope.InputControlDisplayStatus.ddlPageNonValidation = true;

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
          $scope.InputControlDisplayStatus.ddlTestList = true;
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.onSharedTestListChange = function () {
      crudService.getAll(ngAppSettings.SharedTestDataAllBySharedTestIdUrl.format($stateParams.WebsiteId, $scope.TestData.SharedTestId)).then(function (response) {
        $scope.TestData.SharedTestDataList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.onAssignVariableClick = function () {
      if ($scope.TestData.IsAssignVariableName) {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = true;
        $scope.InputControlDisplayStatus.txtValue = $scope.TestData.ActionId == $scope.ActionConstants.AssertToEqualActionId && $scope.TestData.PageId == undefined ? true : false;
      }
      else {
        $scope.InputControlDisplayStatus.txtAutoCompVariableName = false;
        $scope.InputControlDisplayStatus.txtValue = true;
        $scope.TestData.VariableName = null;
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


    $scope.addTestData = function () {
      $scope.TestData.SharedTestSteps = [];
      if ($scope.TestData.SharedTestDataList.length > 0) {
        $scope.TestData.IsSharedTest = true;
        for (var i = 0; i < $scope.TestData.SharedTestDataList.length; i++) {
          $scope.TestData.SharedTestSteps[i] = {};
          $scope.TestData.SharedTestSteps[i].SharedTestDataId = $scope.TestData.SharedTestDataList[i].Id;
          $scope.TestData.SharedTestSteps[i].IsIgnored = $scope.TestData.SharedTestDataList[i].Ignore;
          $scope.TestData.SharedTestSteps[i].NewValue = $scope.TestData.SharedTestDataList[i].UIValue;
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

    $scope.deleteTestData = function (testDataId) {
      if (confirm("Are you sure u want to delete step?")) {
        crudService.delete(ngAppSettings.TestDataAllByTestIdUrl.format($stateParams.WebsiteId, $stateParams.TestCatId, $stateParams.TestId), {Id: testDataId}).then(function (response) {
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

    $scope.loadDataForAdd = function () {
      var deferred = $q.defer();
      var promises = [];

      dataProvider.currentWebSite($scope);
      dataProvider.currentTest($scope);

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
      $scope.InputControlDisplayStatus.ddlPageNonValidation = false;
      $scope.InputControlDisplayStatus.ddlDisplayName = false;
      $scope.InputControlDisplayStatus.chkAssignVariableValue = false;
      $scope.InputControlDisplayStatus.ddlDataBaseCategory = false;
      $scope.InputControlDisplayStatus.ddlSQLAction = false;
      $scope.InputControlDisplayStatus.txtSQLQuery = false;
      $scope.InputControlDisplayStatus.txtAutoCompVariableName = false;
    };

    $scope.resetAllInputControlDisplayStatus = function () {
      $scope.InputControlDisplayStatus = {
        'ddlAction': false,
        'ddlPage': false,
        'ddlPageNonValidation': false,
        'ddlDisplayName': false,
        'txtValue': false,
        'txtAutoCompVariableName': false,
        'ddlSharedTestList': false,
        'ddlWebsiteList': false,
        'ddlTestList': false,
        'chkAssignVariableValue': false
      };
    };

    $scope.onQueueClick = function () {
      $rootScope.$broadcast('openTestQueuePopup', {'TestId': $stateParams.TestId, 'WebsiteId': $stateParams.WebsiteId});
    };


  }]);
