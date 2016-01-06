/**
 * Created by gurpreet.singh on 09/07/2015.
 */

function RestangularCustom() {
  return ['$state', '$q', '$http', 'Restangular', 'AppSettings', 'authService', function ($state, $q, $http, Restangular, appSettings, authService) {
    return Restangular.withConfig(function (RestangularConfigurer) {
      RestangularConfigurer.setBaseUrl(appSettings.ApiEndpoints.BASE_API_URI);

      RestangularConfigurer.addFullRequestInterceptor(function (elem, operation, path, url, headers, params, httpConfig) {
        var authData = authService.getAuthData();
        if (authData && authData.isAuth) {
          headers.Authorization = "Bearer " + authData.access_token;
        }
      });

      var refreshAccesstoken = function () {
        var deferred = $q.defer();
        authService.refreshToken().then(deferred.resolve, deferred.reject);
        return deferred.promise;
      };

      var responseData = function (dataObj) {
        var result = dataObj;
        if (dataObj && dataObj.data && dataObj.data.Item) {
          if (dataObj.data.Item instanceof Array) {
            result = dataObj.data.Item;
            result.IsError = dataObj.data.IsError;
            result.Messages = dataObj.data.Messages;
          }
          else {
            result = dataObj.data;
          }
        }

        return result;
      };

      RestangularConfigurer.setErrorInterceptor(function (response, deferred, responseHandler) {
        if (response.status === 401 || response.status == 0) {
          var authData = authService.getAuthData();

          if (authData && authData.isAuth) {
            refreshAccesstoken().then(function () {
              authData = authService.getAuthData();
              response.config.headers.Authorization = "Bearer " + authData.access_token;
              $http(response.config).then(function (dataObj) {
                deferred.resolve(responseData(dataObj));
              }, deferred.reject);
            }, function (err) {
              deferred.reject(err);
              authService.logOut();
              $state.go("LoginWithMsg", {mid: 2});
            });
          }
          else {
            authService.logOut();
            $state.go("LoginWithMsg", {mid: 2});
          }
          return false;
        }
        return true;
      });

      RestangularConfigurer.setResponseExtractor(function (response, operation) {
        if (operation === 'getList') {
          var newResponse = response.Item;
          newResponse.IsError = response.IsError;
          newResponse.Messages = response.Messages;
          return newResponse;
        }
        return response;
      });
    });
  }];
}
