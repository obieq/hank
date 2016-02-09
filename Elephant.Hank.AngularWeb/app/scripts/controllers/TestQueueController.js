/**
 * Created by vyom.sharma on 12-05-2015.
 */

app.controller('TestQueueController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    $scope.TestQueue = {};
    $scope.TestQueue.Settings = {};
    dataProvider.currentWebSite($scope);


    $scope.$on('openTestQueuePopup', function (event, args) {
      crudService.getById(ngAppSettings.WebSiteUrl, args.WebsiteId).then(function (response) {
          $scope.Website = response.Item;
          $scope.TestQueue.Settings.SeleniumAddress = $scope.Website.Settings.SeleniumAddress;
          $scope.TestQueue.Settings.TakeScreenShotOnUrlChanged = $scope.Website.Settings.TakeScreenShotOnUrlChanged;
          $scope.TestQueue.TestId = args.TestId;
          $scope.TestQueue.Settings.UrlObj = $scope.Website.WebsiteUrlList[0];
          $scope.TestQueue.Settings.RepeatTimes = 1;
          crudService.getAll(ngAppSettings.BrowserUrl).then(function (response) {
            crudService.getById(ngAppSettings.UserProfileUrl).then(function (profileResponse) {
              if(profileResponse.Item.Settings!=undefined ){
                if(profileResponse.Item.Settings.SeleniumAddress!=undefined && profileResponse.Item.Settings.SeleniumAddress.trim()!=''){
                  $scope.TestQueue.Settings.SeleniumAddress = profileResponse.Item.Settings.SeleniumAddress;
                }
              }
              $scope.BrowserList = response;
              if (args.Os != undefined && args.BrowserName != undefined) {
                if (args.Os.toLowerCase() == 'xp') {
                  args.Os = 'windows';
                }
                for (var j = 0; j < $scope.BrowserList.length; j++) {
                  if ($scope.BrowserList[j].ConfigName.toLowerCase() == args.BrowserName.toLowerCase() && $scope.BrowserList[j].Platform.toLowerCase() == args.Os.toLowerCase()) {
                    $scope.BrowserList[j].Checked = true;
                    break;
                  }
                }
              }
              else {
                if (profileResponse.Item.Settings != null && profileResponse.Item.Settings != undefined) {
                  for (var k = 0; k < profileResponse.Item.Settings.Browsers.length; k++) {
                    for (var j = 0; j < $scope.BrowserList.length; j++) {
                      if (profileResponse.Item.Settings.Browsers[k] == $scope.BrowserList[j].Id) {
                        $scope.BrowserList[j].Checked = true;
                        break;
                      }
                    }
                  }
                }
                else {
                  for (var k = 0; k < $scope.Website.Settings.Browsers.length; k++) {
                    for (var j = 0; j < $scope.BrowserList.length; j++) {
                      if ($scope.Website.Settings.Browsers[k] == $scope.BrowserList[j].Id) {
                        $scope.BrowserList[j].Checked = true;
                        break;
                      }
                    }
                  }
                }

              }
              $('#testQueueModal').modal('show');
            }, function (response) {
              commonUi.showErrorPopup(response);
            });
          }, function (response) {
            commonUi.showErrorPopup(response);
          });
        }
        , function (response) {
          commonUi.showErrorPopup(response);
        });

    });


    $scope.addTestToTestQueue = function () {
      $scope.TestQueue.Settings.UrlId = $scope.TestQueue.Settings.UrlObj.Id;
      $scope.TestQueue.Settings.Browsers = [];
      for (var k = 0; k < $scope.BrowserList.length; k++) {
        if ($scope.BrowserList[k].Checked) {
          $scope.TestQueue.Settings.Browsers.push($scope.BrowserList[k].Id);
        }
      }
      if ($scope.TestQueue.Settings.Browsers.length > 0) {
        crudService.add(ngAppSettings.TestQueueUrl.format($stateParams.WebsiteId), $scope.TestQueue).then(function (response) {
          $("#testQueueModal").modal('hide');
          $("#testQueueModal").bind('hidden.bs.modal', function () {
            commonUi.showMessagePopup("Test has been queued successfully!");
            $("#testQueueModal").unbind('hidden.bs.modal');
          });

        }, function (response) {
          commonUi.showErrorPopup(response);
        });
      }
      else {
        commonUi.showErrorPopup("Please select atleast one browser", 'Error');
      }
    };

    $scope.markUnMarkAllBrowser = function (browserobj) {
      if (browserobj) {
        $scope.SelectAllBrowsers = $scope.SelectAllBrowsers && browserobj.Checked;
        return;
      }
      for (var i = 0; i < $scope.BrowserList.length; i++) {
        $scope.BrowserList[i].Checked = $scope.SelectAllBrowsers;
      }
    };

  }]);

