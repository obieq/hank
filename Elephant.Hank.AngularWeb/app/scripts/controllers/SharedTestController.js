/**
 * Created by vyom.sharma on 05-06-2015.
 */

app.controller('SharedTestController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider','authService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider,authService) {
    $scope.SharedTestList = [];
    $scope.SharedTest = {};
    $scope.Website = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    var authData = authService.getAuthData();
    $scope.IsAdmin = authData.type == 'TestAdmin' ? true : false;
    dataProvider.setAuthenticationParameters($scope,$stateParams.WebsiteId,ngAppSettings.ModuleType.SharedTestCases);
    dataProvider.currentWebSite($scope);

    $scope.getAllSharedTests = function () {
      crudService.getAll(ngAppSettings.WebsiteSharedTestCasesUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.SharedTestList = response;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.getSharedTestById = function () {
      crudService.getById(ngAppSettings.WebsiteSharedTestCasesUrl.format($stateParams.WebsiteId), $stateParams.SharedTestId).then(function (response) {
        $scope.SharedTest = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.deleteSharedTestById = function (sharedTestId) {
      var deleteConfirmation = confirm("Are you sure you want to delete?");
      if (deleteConfirmation) {
        crudService.delete(ngAppSettings.WebsiteSharedTestCasesUrl.format($stateParams.WebsiteId), {'Id': sharedTestId}).then(function (response) {
          $scope.getAllSharedTests();
        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
    };

    $scope.updateSharedTest = function () {
      crudService.update(ngAppSettings.WebsiteSharedTestCasesUrl.format($stateParams.WebsiteId), $scope.SharedTest).then(function (response) {
        $state.go("Website.SharedTest", {WebsiteId: $scope.stateParamWebsiteId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addSharedTest = function () {
      $scope.SharedTest.WebsiteId = $scope.stateParamWebsiteId;
      crudService.add(ngAppSettings.WebsiteSharedTestCasesUrl.format($stateParams.WebsiteId), $scope.SharedTest).then(function (response) {
        $state.go("Website.SharedTest", {WebsiteId: $scope.stateParamWebsiteId});
      }, function (response) {

        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadData = function () {
    };
  }]);
