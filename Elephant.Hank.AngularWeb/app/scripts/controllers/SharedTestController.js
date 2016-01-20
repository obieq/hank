/**
 * Created by vyom.sharma on 05-06-2015.
 */

app.controller('SharedTestController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.SharedTestList = [];
    $scope.SharedTest = {};
    $scope.Website = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;

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
