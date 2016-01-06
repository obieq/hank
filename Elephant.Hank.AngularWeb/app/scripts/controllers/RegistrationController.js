/**
 * Created by vyom.sharma on 29-09-2015.
 */

'use strict';

app.controller('RegistrationController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'authService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, authService) {
    $scope.User = {};

    $scope.onSubmit = function () {
      var res = authService.saveRegistration($scope.User).then(function (response) {

      });
    };

  }]);

