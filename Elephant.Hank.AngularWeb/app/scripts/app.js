'use strict';

/**
 * @ngdoc overview
 * @name elephantprotractorangularWebApp
 * @description
 * # elephantprotractorangularWebApp
 *
 * Main module of the application.
 */
var app = angular
  .module('elephantprotractorangularWebApp', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch',
    'angular-loading-bar',
    'restangular',
    'ui.router',
    'ngStorage',
    'ngCsv',
    'ui.bootstrap',
    'ncy-angular-breadcrumb',
    'daterangepicker',
    'LocalStorageModule'
  ])
  .factory('RestangularCustom', RestangularCustom())
  .constant('AppSettings', AppSettings())
  .config(function ($stateProvider, $urlRouterProvider, RestangularProvider, $breadcrumbProvider) {

    $breadcrumbProvider.setOptions({
      templateUrl: 'views/breadcrum.html'
    });

    $urlRouterProvider.otherwise("Login");

    $stateProvider
      .state('Environment', {
        abstract: true,
        url: "/Environment",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'}
        }
      })
      .state('Environment.List', {
        url: "",
        templateUrl: "views/Environment/environment.html",
        controller: 'EnvironmentController',
        ncyBreadcrumb: {label: 'Environment', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Environment.Update', {
        url: "/{Id:int}",
        templateUrl: 'views/Environment/environment-update.html',
        controller: 'EnvironmentController',
        ncyBreadcrumb: {label: '{{Environment.Name}}', parent: "Environment.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Environment.Add', {
        url: "/Add",
        templateUrl: 'views/Environment/environment-add.html',
        controller: 'EnvironmentController',
        ncyBreadcrumb: {label: 'New Environment', parent: "Environment.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('DbLog', {
        abstract: true,
        url: "/DbLog",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'}
        },
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('DbLog.List', {
        url: "",
        templateUrl: "views/DataBaseLogger/DbLog.html",
        controller: 'DbLogController',
        ncyBreadcrumb: {label: 'DbLog', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('DbLog.Details', {
        url: "/{Id:int}",
        templateUrl: "views/DataBaseLogger/DbLogDetail.html",
        controller: 'DbLogController',
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('Locator', {
        abstract: true,
        url: "/Locator",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'}
        },
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Locator.List', {
        url: "",
        templateUrl: "views/locator.html",
        controller: 'LocatorController',
        ncyBreadcrumb: {label: 'Locator', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Locator.Update', {
        url: "/{Id:int}",
        templateUrl: 'views/locator-update.html',
        controller: 'LocatorController',
        ncyBreadcrumb: {label: '{{Locator.Value}}', parent: "Locator.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Locator.Add', {
        url: "/Add",
        templateUrl: 'views/locator-add.html',
        controller: 'LocatorController',
        ncyBreadcrumb: {label: 'New Locator', parent: "Locator.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('Action', {
        abstract: true,
        url: "/Action",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'}
        }
      })
      .state('Action.List', {
        url: "",
        templateUrl: "views/action.html",
        controller: 'ActionController',
        ncyBreadcrumb: {label: 'Action', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Action.Update', {
        url: "/{Id:int}",
        templateUrl: 'views/action-update.html',
        controller: 'ActionController',
        ncyBreadcrumb: {label: '{{Action.Value}}', parent: "Action.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Action.Add', {
        url: "/Add",
        templateUrl: 'views/action-add.html',
        controller: 'ActionController',
        ncyBreadcrumb: {label: 'New Action', parent: "Action.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('Login', {
        url: "/Login",
        ncyBreadcrumb: {label: 'Login'},
        views: {
          "MainView": {templateUrl: "views/Account/login.html", controller: 'AccountController'}
        },
        permissionData: { NotAllowdedIfLoggedIn: true}
      })
      .state('LoginWithMsg', {
        url: "/Login/:mid?",
        ncyBreadcrumb: {label: 'Login'},
        views: {
          "MainView": {templateUrl: "views/Account/login.html", controller: 'AccountController'}
        },
        permissionData: { NotAllowdedIfLoggedIn: true}
      })
      .state('SignUp', {
        url: "/SignUp",
        ncyBreadcrumb: {label: 'Register'},
        views: {
          "MainView": {templateUrl: "views/Account/signup.html", controller: 'AccountController'}
        },
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('User', {
        abstract: true,
        url: "/Profile",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'}
        }
      })
      .state('User.Profile', {
        url: "",
        templateUrl: "views/Account/profile.html",
        controller: 'ProfileController',
        ncyBreadcrumb: {label: 'Profile', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('User.ChangePassword', {
        url: "/change-password",
        templateUrl: "views/Account/change-password.html",
        controller: 'ProfileController',
        ncyBreadcrumb: {label: 'Change Password', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('UserManager', {
        abstract: true,
        url: "/users",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'}
        }
      })
      .state('UserManager.List', {
        url: "",
        templateUrl: "views/UserManagement/user-list.html",
        controller: 'UserController',
        ncyBreadcrumb: {label: 'Users', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('UserManager.ResetPassword', {
        url: "/{UserId:int}/reset-password",
        templateUrl: "views/UserManagement/reset-password.html",
        controller: 'UserController',
        ncyBreadcrumb: {label: 'Users', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('Browser', {
        abstract: true,
        url: "/Browsers",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'}
        }
      })
      .state('Browser.List', {
        url: "",
        templateUrl: "views/Browser/browser.html",
        controller: 'BrowserController',
        ncyBreadcrumb: {label: 'Browser', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Browser.Update', {
        url: "/{Id:int}",
        templateUrl: 'views/Browser/browser-update.html',
        controller: 'BrowserController',
        ncyBreadcrumb: {label: '{{Browser.DisplayName}}', parent: "Browser.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Browser.Add', {
        url: "/Add",
        templateUrl: 'views/Browser/browser-add.html',
        controller: 'BrowserController',
        ncyBreadcrumb: {label: 'New Browser', parent: "Browser.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })

      .state('Website', {
        abstract: true,
        url: "/Website",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<ui-view />'},
          "TestQueueView": {templateUrl: "views/QueueTest/queuetest.html", controller: 'TestQueueController'}
        },
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.List', {
        url: "",
        templateUrl: 'views/website.html',
        controller: 'WebsiteController',
        ncyBreadcrumb: {label: 'Home'},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.Add', {
        url: "/Add",
        templateUrl: 'views/website-add.html',
        controller: 'WebsiteController',
        ncyBreadcrumb: {label: 'New Website', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.Update', {
        url: "/{WebsiteId:int}",
        templateUrl: 'views/website-update.html',
        controller: 'WebsiteController',
        ncyBreadcrumb: {label: '{{Website.Name}}', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.DataBaseCategories', {
        url: "/{WebsiteId:int}/data-base-categories",
        templateUrl: 'views/DataBaseCategories/list.html',
        controller: 'DataBaseCategoriesController',
        ncyBreadcrumb: {label: 'Data Base Categories', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.DataBaseCategoriesAdd', {
        url: "/{WebsiteId:int}/data-base-categories/add",
        templateUrl: 'views/DataBaseCategories/add.html',
        controller: 'DataBaseCategoriesController',
        ncyBreadcrumb: {label: 'New Data Base Categories', parent: "Website.DataBaseCategories"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.DataBaseCategoriesUpdate', {
        url: "/{WebsiteId:int}/data-base-categories/{DataBaseCategoryId:int}",
        templateUrl: 'views/DataBaseCategories/update.html',
        controller: 'DataBaseCategoriesController',
        ncyBreadcrumb: {label: '{{ DataBaseCategory.Name }}', parent: "Website.DataBaseCategories"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.DataBaseConnection', {
        url: "/{WebsiteId:int}/data-base-categories/{DataBaseCategoryId:int}/data-base-connection",
        templateUrl: 'views/DataBaseConnection/list.html',
        controller: 'DataBaseConnectionController',
        ncyBreadcrumb: {label: 'Data Base Connections', parent: "Website.DataBaseCategoriesUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.DataBaseConnectionAdd', {
        url: "/{WebsiteId:int}/data-base-categories/{DataBaseCategoryId:int}/data-base-connection/add",
        templateUrl: 'views/DataBaseConnection/add.html',
        controller: 'DataBaseConnectionController',
        ncyBreadcrumb: {label: 'New Data Base Connection', parent: "Website.DataBaseCategoriesUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.DataBaseConnectionUpdate', {
        url: "/{WebsiteId:int}/data-base-categories/{DataBaseCategoryId:int}/data-base-connection/{DataBaseConnectionId:int}",
        templateUrl: 'views/DataBaseConnection/update.html',
        controller: 'DataBaseConnectionController',
        ncyBreadcrumb: {label: 'New Data Base Connection', parent: "Website.DataBaseCategoriesUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.TestCat', {
        url: "/{WebsiteId:int}/test-category",
        templateUrl: 'views/TestCategory/list.html',
        controller: 'TestCatController',
        ncyBreadcrumb: {label: 'Test Categories', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.TestCatAdd', {
        url: "/{WebsiteId:int}/test-category/Add",
        templateUrl: 'views/TestCategory/add-new.html',
        controller: 'TestCatController',
        ncyBreadcrumb: {label: 'New test category', parent: "Website.TestCat"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.TestCatUpdate', {
        url: "/{WebsiteId:int}/test-category/{TestCatId:int}",
        templateUrl: 'views/TestCategory/update.html',
        controller: 'TestCatController',
        ncyBreadcrumb: {label: '{{ TestCat.Name }}', parent: "Website.TestCat"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.TestCatTest', {
        url: "/{WebsiteId:int}/test-category/{TestCatId:int}/Test",
        templateUrl: "views/TestScripts/test.html",
        controller: 'TestController',
        ncyBreadcrumb: {label: 'Test Scripts', parent: "Website.TestCatUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.TestCatTestAdd', {
        url: "/{WebsiteId:int}/test-category/{TestCatId:int}/Test/Add",
        templateUrl: "views/TestScripts/test-add.html",
        controller: 'TestController',
        ncyBreadcrumb: {label: 'New Test Script', parent: "Website.TestCatTest"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.TestCatTestUpdate', {
        url: "/{WebsiteId:int}/test-category/{TestCatId:int}/Test/{TestId:int}",
        templateUrl: 'views/TestScripts/test-update.html',
        controller: 'TestController',
        ncyBreadcrumb: {label: '{{Test.TestName}}', parent: "Website.TestCatTest"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.TestData', {
        url: "/{WebsiteId:int}/test-category/{TestCatId:int}/Test/{TestId:int}/test-data",
        templateUrl: 'views/TestData/test-data.html',
        controller: 'Test_Data_Controller',
        ncyBreadcrumb: {label: 'Test Data', parent: "Website.TestCatTestUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.TestDataAdd', {
        url: "/{WebsiteId:int}/test-category/{TestCatId:int}/Test/{TestId:int}/test-data/Add/{ExecutionSequence:int}",
        templateUrl: 'views/TestData/test-data-add.html',
        controller: 'Test_Data_Controller',
        ncyBreadcrumb: {label: 'New Test Data', parent: "Website.TestData"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.TestDataUpdate', {
        url: "/{WebsiteId:int}/test-category/{TestCatId:int}/Test/{TestId:int}/test-data/{TestDataId:int}",
        templateUrl: 'views/TestData/test-data-update.html',
        controller: 'Test_Data_Controller',
        ncyBreadcrumb: {label: 'Edit', parent: "Website.TestData"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.Pages', {
        url: "/{WebsiteId:int}/pages",
        templateUrl: 'views/pages.html',
        controller: 'PagesController',
        ncyBreadcrumb: {label: 'Pages', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.PagesAdd', {
        url: "/{WebsiteId:int}/pages/Add",
        templateUrl: 'views/pages-add.html',
        controller: 'PagesController',
        ncyBreadcrumb: {label: 'New Page', parent: "Website.Pages"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.PagesUpdate', {
        url: "/{WebsiteId:int}/pages/{PageId:int}",
        templateUrl: 'views/pages-update.html',
        controller: 'PagesController',
        ncyBreadcrumb: {label: '{{Page.Value}}', parent: "Website.Pages"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.PagesLocatorIdentifier', {
        url: "/{WebsiteId:int}/pages/{PageId:int}/locator-identifier",
        templateUrl: 'views/LocatorIdentifier.html',
        controller: 'LocatorIdentifierController',
        ncyBreadcrumb: {label: 'Locator Identifier', parent: "Website.PagesUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.PagesLocatorIdentifierAdd', {
        url: "/{WebsiteId:int}/pages/{PageId:int}/locator-identifier/Add",
        templateUrl: 'views/LocatorIdentifier-add.html',
        controller: 'LocatorIdentifierController',
        ncyBreadcrumb: {label: 'New Locator Identifier', parent: "Website.PagesLocatorIdentifier"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.PagesLocatorIdentifierUpdate', {
        url: "/{WebsiteId:int}/pages/{PageId:int}/locator-identifier/{LocatorIdentifierId:int}",
        templateUrl: 'views/LocatorIdentifier-update.html',
        controller: 'LocatorIdentifierController',
        ncyBreadcrumb: {label: '{{LocatorIdentifier.DisplayName}}', parent: "Website.PagesLocatorIdentifier"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.Suite', {
        url: "/{WebsiteId:int}/suite",
        templateUrl: 'views/suite/suite.html',
        controller: 'SuiteController',
        ncyBreadcrumb: {label: 'Suite', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SuiteAdd', {
        url: "/{WebsiteId:int}/suite/Add",
        templateUrl: 'views/suite/suite-add.html',
        controller: 'SuiteController',
        ncyBreadcrumb: {label: 'New Suite', parent: "Website.Suite"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SuiteUpdate', {
        url: "/{WebsiteId:int}/suite/{SuiteId:int}",
        templateUrl: 'views/suite/suite-update.html',
        controller: 'SuiteController',
        ncyBreadcrumb: {label: '{{Suite.Name}}', parent: "Website.Suite"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.Scheduler', {
        url: "/{WebsiteId:int}/Scheduler",
        templateUrl: "views/Scheduler/scheduler.html",
        controller: 'SchedulerController',
        ncyBreadcrumb: {label: 'Scheduler', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SchedulerAdd', {
        url: "/{WebsiteId:int}/Scheduler/Add",
        templateUrl: "views/Scheduler/scheduler-add.html",
        controller: 'SchedulerController',
        ncyBreadcrumb: {label: 'New Scheduler', parent: "Website.Scheduler"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SchedulerUpdate', {
        url: "/{WebsiteId:int}/Scheduler/{Id:int}",
        templateUrl: 'views/Scheduler/scheduler-update.html',
        controller: 'SchedulerController',
        ncyBreadcrumb: {label: '{{Scheduler.Name}}', parent: "Website.Scheduler"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SchedulerHistory', {
        url: "/{WebsiteId:int}/Scheduler/{Id:int}/history",
        templateUrl: 'views/Scheduler/scheduler-history.html',
        controller: 'SchedulerController',
        ncyBreadcrumb: {label: 'Scheduler History', parent: "Website.SchedulerUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.Report', {
        url: "/{WebsiteId:int}/Report",
        templateUrl: "views/Report/ReportList.html",
        controller: 'ReportController',
        ncyBreadcrumb: {label: 'Report', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.ReportByExecution', {
        url: "/{WebsiteId:int}/Report/{CreatedOn}",
        templateUrl: "views/Report/ReportList.html",
        controller: 'ReportController',
        ncyBreadcrumb: {label: 'Report', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.ReportDetail', {
        url: "/{WebsiteId:int}/Report-Detail/{ReportId:int}",
        templateUrl: "views/Report/ReportDetail.html",
        controller: 'ReportController',
        ncyBreadcrumb: {label: 'Details', parent: "Website.Report"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.SharedTest', {
        url: "/{WebsiteId:int}/SharedTest",
        templateUrl: "views/SharedTest/test.html",
        controller: 'SharedTestController',
        ncyBreadcrumb: {label: 'Shared Test', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SharedTestAdd', {
        url: "/{WebsiteId:int}/SharedTest/Add",
        templateUrl: "views/SharedTest/test-add.html",
        controller: 'SharedTestController',
        ncyBreadcrumb: {label: 'New Shared Test', parent: "Website.SharedTest"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SharedTestUpdate', {
        url: "/{WebsiteId:int}/SharedTest/{SharedTestId:int}",
        templateUrl: 'views/SharedTest/test-update.html',
        controller: 'SharedTestController',
        ncyBreadcrumb: {label: '{{SharedTest.TestName}}', parent: "Website.SharedTest"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })

      .state('Website.SharedTestData', {
        url: "/{WebsiteId:int}/SharedTest/{SharedTestId:int}/test-data",
        templateUrl: 'views/SharedTestData/test-data.html',
        controller: 'Shared_Test_Data_Controller',
        ncyBreadcrumb: {label: 'Shared Test Data', parent: "Website.SharedTestUpdate"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SharedTestDataAdd', {
        url: "/{WebsiteId:int}/SharedTest/{SharedTestId:int}/shared-test-data/Add/{ExecutionSequence:int}",
        templateUrl: 'views/SharedTestData/test-data-add.html',
        controller: 'Shared_Test_Data_Controller',
        ncyBreadcrumb: {label: 'New Shared Test Data', parent: "Website.SharedTestData"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.SharedTestDataUpdate', {
        url: "/{WebsiteId:int}/SharedTest/{SharedTestId:int}/shared-test-data/{TestDataId:int}",
        templateUrl: 'views/SharedTestData/test-data-Update.html',
        controller: 'Shared_Test_Data_Controller',
        ncyBreadcrumb: {label: 'Edit', parent: "Website.SharedTestData"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Website.ScreenShotCompare', {
        url: "/{WebsiteId:int}/screen-shot-compare",
        templateUrl: 'views/screen-shot-compare.html',
        controller: 'ScreenShotCompareController',
        ncyBreadcrumb: {label: 'Screen Shot Compare', parent: "Website.Update"},
        permissionData: {Roles: ["TestAdmin", "TestUser"]}
      })
      .state('Group', {
        abstract: true,
        url: "/Group",
        views: {
          "NavView": {templateUrl: "views/navigation.html", controller: 'NavCtrl'},
          "MainView": {template: '<div ui-view="GroupView"></div>'}
        }
      })
      .state('Group.List', {
        url: "",
        views: {
          "GroupView": {
            templateUrl: 'views/Group/list.html',
            controller: 'GroupController'
          }
        },
        ncyBreadcrumb: {label: 'Group', parent: "Website.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Group.Update', {
        url: "/{GroupId:int}",
        views: {
          "GroupView": {
            templateUrl: 'views/Group/update.html',
            controller: 'GroupController'
          }
        },
        ncyBreadcrumb: {label: '{{Group.Value}}', parent: "Group.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Group.Add', {
        url: "/Add",
        views: {
          "GroupView": {
            templateUrl: 'views/Group/add.html',
            controller: 'GroupController'
          }
        },
        ncyBreadcrumb: {label: 'New Group', parent: "Group.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Group.Manage', {
        abstract: true,
        url: "/{GroupId:int}/manage",
        views: {
          "GroupView": {templateUrl: 'views/GroupManager/Tabs.html'},
          "TabsView@Group.Manage": {template: '<div ui-view="TabContent"></div>'}
        },
        ncyBreadcrumb: {label: 'New Group', parent: "Group.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Group.Manage.Website', {
        url: "/website",
        views: {
          "TabContent": {
            templateUrl: 'views/GroupManager/GroupWebsites.html',
            controller: 'GroupWebsiteController'
          }
        },
        ncyBreadcrumb: {label: 'New Group', parent: "Group.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Group.Manage.Module', {
        url: "/website/{WebsiteId}",
        views: {
          "TabContent": {
            templateUrl: 'views/GroupManager/GroupWebsiteModule.html',
            controller:'GroupWebsiteModuleController'
          }
        },
        ncyBreadcrumb: {label: 'New Group', parent: "Group.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })
      .state('Group.Manage.User', {
        url: "/users",
        views: {
          "TabContent": {templateUrl: 'views/GroupManager/GroupUsers.html',controller:'GroupUserController'}

        },
        ncyBreadcrumb: {label: 'New Group', parent: "Group.List"},
        permissionData: {Roles: ["TestAdmin"]}
      })

  })
  .constant('ngAppSettings', {
    StorageTimeOut: 300000,

    StepTypes: {
      TestStep: 0,
      SharedTestStep: 1,
      WebsiteTestStep: 2,
      SqlTestStep: 3
    },

    WebSiteUrl: "website",
    WebSitePagesUrl: "website/{0}/pages",
    WebSiteTestCatUrl: "website/{0}/test-cat",
    WebSiteSuiteUrl: "website/{0}/suite",
    WebSiteTestCasesUrl: "website/{0}/test-cat/{1}/test",
    WebSiteSchedulerUrl: "website/{0}/scheduler",
    WebSiteSharedTestUrl: "website/{0}/shared-test",
    WebsiteSharedTestCasesUrl: "website/{0}/shared-test",
    WebsiteGetVariableForAutoComplete: 'website/{0}/variable-for-autocomplete',
    WebsiteDataBaseCategoriesUrl: "website/{0}/data-base-categories",

    TestCatUrl: "website/{0}/test-cat",
    TestCatTestScriptsUrl: "website/{0}/test-cat/{1}/test",

    SchedulerUrl: "website/{0}/scheduler",
    SchedulerSuiteUrl: "website/{0}/scheduler/{1}/scheduler-suite-map",
    SchedulerHistoryUrl: "website/{0}/scheduler/{1}/scheduler-history",
    SchedulerForceExecuteUrl: "website/{0}/scheduler/{1}/force-execute",

    UserProfileUrl: "user/user-profile",
    UserProfileUpdateUrl: "profile",
    ChangePasswordUrl: "account/change-password",

    LocatorUrl: "locator",
    ActionUrl: "action",
    ActionForSqlTestStep: "action/action-for-sql-test-step",

    ReportUrl: "Report",
    SearchReportUrl: "Report/SearchReport",
    ReportByExecutionGroupUrl: "report/execution-group/{0}",
    ReportByExecutionGroupWhereScreenShotArrayExistUrl: "report/execution-group/{0}/screen-shot-array",
    PagesUrl: "website/{0}/pages",
    LocatorIdentifierUrl: "website/{0}/pages/{1}/locator-identifier",

    TestUrl: "website/{0}/test-cat/{1}/test",
    TestQueueUrl: "website/{0}/test-queue",
    TestDataAllByTestIdUrl: "website/{0}/test-cat/{1}/test/{2}/test-data",
    TestDataGetVariableForAutoComplete: 'website/{0}/test-cat/{1}/test/{2}/variable-for-autocomplete',
    TestDataListAddUrl: "website/{0}/test-cat/{1}/test/{2}/test-data/copy-test-data",
    SuiteTestMapUrl: "website/{0}/suite/{1}/suite-test-map",
    SuiteTestMapBySuiteIdListUrl: "website/{0}/suite/suite-test-map/{1}",
    SharedTestDataAllBySharedTestIdUrl: "website/{0}/shared-test/{1}/shared-test-data",
    SharedTestDataGetVariableTestSteps: "website/{0}/shared-test/{1}/variable-test-steps",

    BrowserUrl: "browser",
    EnvironmentUrl: "environment",

    DbLogUrl: "dblog/{0}/{1}",
    DbLogRollDataUrl: "dblog/roll-data",
    DbLogDateTimeRangeUrl: "dblog/get-data-range",
    ActionConstantUrl: "action/action-constants",
    UserUrl: "user",
    UserSetLockoutUrl: "user/{0}/set-lockout-enabled/{1}",
    ResetPasswordUrl: "Account/reset-password",
    DataBaseCategoriesUrl: "website/{0}/data-base-categories",
    DataBaseCategoriesConnectionUrl: "website/{0}/data-base-categories/{1}/data-base-connection",
    DataBaseConnectionUrl: "website/{0}/data-base-categories/{1}/data-base-connection",
    DataBaseConnectionGetDataBaseListUrl: "website/{0}/data-base-categories/{1}/data-base-connection/get-database-list",
    GroupUrl: "group",
    GroupUserAddUrl:'group-user',
    GroupUserUrl: "group/{0}/user",
    GroupUserDeleteUrl:"group/{0}/user/{1}/remove-from-group",
    GroupWebsiteUrl: "group/{0}/add-website-to-group",
    GroupModuleUrl: "group/{0}/group-module-access",
    GroupWebsiteModuleUrl: "group/{0}/website/{1}",
    GroupModuleAccessBulkUpdate:"group-module-access/update-access-bulk"
  });
