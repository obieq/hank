/**
 * Created by vyom.sharma on 29-09-2015.
 */
'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'AppSettings', function ($http, $q, localStorageService, appSettings) {

  var serviceBase = appSettings.ApiEndpoints.BASE_API_URI;

  var authServiceFactory = {};

  var _authentication = {
    isAuth: false,
    userName: ""
  };

  var _saveRegistration = function (registration) {
    return $http.post(serviceBase + '/account/register', registration);
  };

  var _login = function (loginData) {

    var data = "grant_type=password&client_id=" + appSettings.ClientId + "&client_secret=" + appSettings.ClientSecret + "&username=" + loginData.UserName + "&password=" + loginData.Password;

    var deferred = $q.defer();

    $http.post(serviceBase + '/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
      localStorageService.set('authorizationData', response);
      _authentication = response;
      _authentication.isAuth = true;
      deferred.resolve(response);
    }).error(function (err, status) {
      _logOut();
      deferred.reject(err);
    });

    return deferred.promise;
  };

  var _logOut = function () {
    localStorageService.remove('authorizationData');
    _authentication = {};
    _authentication.isAuth = false;
  };

  var _getAuthData = function () {
    _fillAuthData();
    return _authentication;
  };

  var _fillAuthData = function () {

    var authData = localStorageService.get('authorizationData');
    if (authData) {
      _authentication = authData;
      _authentication.isAuth = true;
    }
  };

  var _refreshToken = function () {
    var deferred = $q.defer();
    var authData = _getAuthData();

    if (authData) {
      var data = "grant_type=refresh_token&refresh_token=" + authData.refresh_token + "&client_id=" + appSettings.ClientId;

      localStorageService.remove('authorizationData');

      $http.post(serviceBase + '/token', data, {headers: {'Content-Type': 'application/x-www-form-urlencoded'}}).success(function (response) {
        localStorageService.set('authorizationData', response);
        deferred.resolve(response);
      }).error(function (err, status) {
        _logOut();
        deferred.reject(err);
      });
    }

    return deferred.promise;
  };

  var _setLastState = function(stateName){
    localStorageService.set('lastStateName', stateName)
  };

  var _getLastState = function(){
    return localStorageService.get('lastStateName')
  };

  authServiceFactory.saveRegistration = _saveRegistration;
  authServiceFactory.login = _login;

  authServiceFactory.setLastState = _setLastState;
  authServiceFactory.getLastState = _getLastState;

  authServiceFactory.logOut = _logOut;
  authServiceFactory.fillAuthData = _fillAuthData;
  authServiceFactory.getAuthData = _getAuthData;
  authServiceFactory.refreshToken = _refreshToken;

  return authServiceFactory;
}]);
