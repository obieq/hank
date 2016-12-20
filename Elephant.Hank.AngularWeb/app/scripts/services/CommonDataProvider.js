/**
 * Created by gurpreet.singh on 05/05/2015.
 */

'use strict';
app.factory('CommonDataProvider', ['$localStorage', '$stateParams', 'ngAppSettings', 'CrudService', 'localStorageService', 'authService', 'CommonUiService',
  function ($localStorage, $stateParams, ngAppSettings, crudService, localStorageService, authService, commonUi) {
    return {
      currentWebSite: function ($scope) {
        var storage = $localStorage.$default({CurrentWebSite: {Id: 0}});

        if (storage.CurrentWebSite.timestamp && new Date().getTime() > (storage.CurrentWebSite.timestamp + storage.CurrentWebSite.expireTimeInMilliseconds)) {
          storage.CurrentWebSite.Id = 0;
        }

        if (storage.CurrentWebSite.Id == 0 || storage.CurrentWebSite.Id != $stateParams.WebsiteId) {
          crudService.getById(ngAppSettings.WebSiteUrl, $stateParams.WebsiteId).then(function (response) {
              storage.CurrentWebSite = response.Item;
              storage.CurrentWebSite.timestamp = new Date().getTime();
              storage.CurrentWebSite.expireTimeInMilliseconds = ngAppSettings.StorageTimeOut;

              $scope.Website = storage.CurrentWebSite;
            }
            , function (response) {
              commonUi.showErrorPopup(response);
            });
        }

        $scope.Website = storage.CurrentWebSite;
      },
      currentTestCat: function ($scope) {
        $scope.TestCat = [];
        var storage = $localStorage.$default({CurrentTestCat: {Id: 0}});

        if (storage.CurrentTestCat.timestamp && new Date().getTime() > (storage.CurrentTestCat.timestamp + storage.CurrentTestCat.expireTimeInMilliseconds)) {
          storage.CurrentTestCat.Id = 0;
        }

        if ((storage.CurrentTestCat.Id == 0 || storage.CurrentTestCat.Id != $stateParams.TestCatId)) {
          crudService.getById(ngAppSettings.TestCatUrl.format($stateParams.WebsiteId), $stateParams.TestCatId).then(function (response) {
              storage.CurrentTestCat = response.Item;
              storage.CurrentTestCat.timestamp = new Date().getTime();
              storage.CurrentTestCat.expireTimeInMilliseconds = ngAppSettings.StorageTimeOut;

              $scope.TestCat = storage.CurrentTestCat;
            }
            , function (response) {
              storage.CurrentTestCat.Id = 0;
              $scope.TestCat.Name = "View All";
            });
        }

        $scope.TestCat = storage.CurrentTestCat;

        if ($scope.TestCat.Id == 0) {
          $scope.TestCat.Name = "View All";
        }
      },
      currentPage: function ($scope) {
        var storage = $localStorage.$default({CurrentPage: {Id: 0}});

        if (storage.CurrentPage.timestamp && new Date().getTime() > (storage.CurrentPage.timestamp + storage.CurrentPage.expireTimeInMilliseconds)) {
          storage.CurrentPage.Id = 0;
        }

        if (storage.CurrentPage.Id == 0 || storage.CurrentPage.Id != $stateParams.PageId) {
          crudService.getById(ngAppSettings.PagesUrl.format($stateParams.WebsiteId), $stateParams.PageId).then(function (response) {
              storage.CurrentPage = response.Item;
              storage.CurrentPage.timestamp = new Date().getTime();
              storage.CurrentPage.expireTimeInMilliseconds = ngAppSettings.StorageTimeOut;
              $scope.Page = storage.CurrentPage;
            }
            , function (response) {
              commonUi.showErrorPopup(response);
            });
        }

        $scope.Page = storage.CurrentPage;
      },
      currentTest: function ($scope) {
        var storage = $localStorage.$default({CurrentTest: {Id: 0}});

        /*  if(storage.CurrentTest.timestamp && new Date().getTime() > (storage.CurrentTest.timestamp + storage.CurrentTest.expireTimeInMilliseconds))
         {
         storage.CurrentTest.Id = 0;
         }*/

        /* if($stateParams.TestCatId && $stateParams.TestCatId != 0)
         {*/
        crudService.getById(ngAppSettings.TestUrl.format($stateParams.WebsiteId, $stateParams.TestCatId), $stateParams.TestId).then(function (response) {
            storage.CurrentTest = response.Item;
            storage.CurrentTest.timestamp = new Date().getTime();
            storage.CurrentTest.expireTimeInMilliseconds = ngAppSettings.StorageTimeOut;
            $scope.Test = storage.CurrentTest;
          }
          , function (response) {
            //commonUi.showErrorPopup(response);
          });
        //}
      },

      currentSharedTest: function ($scope) {
        crudService.getById(ngAppSettings.WebsiteSharedTestCasesUrl.format($stateParams.WebsiteId), $stateParams.SharedTestId).then(function (response) {
            $scope.SharedTest = response.Item;
          }
          , function (response) {
            commonUi.showErrorPopup(response);
          });
      },

      setAuthenticationParameters: function (scope, websiteId, moduleId, operation) {
        var authData = authService.getAuthData();
        scope.LoggeddInUserName = authData.FullName;
        if (authData.type == "TestUser") {
          var groupData = authService.getGroupAuthData();
          var check = _.where(groupData.Item, {WebsiteId: websiteId, ModuleId: moduleId});

          if (check != null && check != undefined && check.length > 0) {
            scope.Authentication.CanWrite = check[0].CanWrite;
            scope.Authentication.CanDelete = check[0].CanDelete;
            scope.Authentication.CanExecute = check[0].CanExecute;
          }
          else {
            scope.Authentication.CanWrite = false;
            scope.Authentication.CanDelete = false;
            scope.Authentication.CanExecute = false;
          }
        }
        else {
          if (authData.type == "TestAdmin") {
            scope.IsAdmin = true;
          }
          scope.Authentication.CanWrite = true;
          scope.Authentication.CanDelete = true;
          scope.Authentication.CanExecute = true;
        }
      }
    };
  }]);
