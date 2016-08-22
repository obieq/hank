/**
 * Created by vyom.sharma on 16-08-2016.
 */

app.controller('DashboardController', ['$scope', 'CrudService', 'ngAppSettings', '$filter', function ($scope, crudService, ngAppSettings, $filter) {

  $scope.EndDate = new Date().dateFormat($filter, true);
  $scope.StartDate = new Date();
  var numberOfDaysToAdd = 5;
  $scope.StartDate.setDate($scope.StartDate.getDate() - numberOfDaysToAdd);
  $scope.StartDate = $scope.StartDate.dateFormat($filter, true);

  $scope.onWebsiteChange = function (IsDefault) {
    crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
      $scope.WebsiteList = response;
      IsDefault ? $scope.WebsiteId = $scope.WebsiteList[0].Id : $scope.WebsiteId;
      crudService.getAll(ngAppSettings.ReportChartUrl.format($scope.WebsiteId, $scope.StartDate, $scope.EndDate)).then(function (response) {
        var resultPassed = response.map(function (a) {
          return a.CountPassed;
        });
        var resultFailed = response.map(function (a) {
          return a.CountFailed;
        });
        var labels = response.map(function (a) {
          return a.CreatedOn.split('T')[0];
        });
        $scope.data = {};
        chartData.datasets[0].data = resultPassed;
        chartData.datasets[1].data = resultFailed;
        chartData.labels = labels;
        $scope.data = chartData;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    }, function (response) {
      commonUi.showErrorPopup(response);
    });
  };

  var chartData = {
    labels: [],
    datasets: [
      {
        label: 'Passed',
        fillColor: 'rgba(220,220,220,0.2)',
        strokeColor: 'rgba(220,220,220,1)',
        pointColor: 'rgba(220,220,220,1)',
        pointStrokeColor: '#fff',
        pointHighlightFill: '#fff',
        pointHighlightStroke: 'rgba(220,220,220,1)',
        data: []
      },
      {
        label: 'Failed',
        fillColor: 'rgba(151,187,205,0.2)',
        strokeColor: 'rgba(151,187,205,1)',
        pointColor: 'rgba(151,187,205,1)',
        pointStrokeColor: '#fff',
        pointHighlightFill: '#fff',
        pointHighlightStroke: 'rgba(151,187,205,1)',
        data: []
      }
    ]
  };

  $scope.options = {

    // Sets the chart to be responsive
    responsive: true,

    ///Boolean - Whether grid lines are shown across the chart
    scaleShowGridLines: true,

    //String - Colour of the grid lines
    scaleGridLineColor: 'rgba(0,0,0,.05)',

    //Number - Width of the grid lines
    scaleGridLineWidth: 1,

    //Boolean - Whether the line is curved between points
    bezierCurve: true,

    //Number - Tension of the bezier curve between points
    bezierCurveTension: 0.4,

    //Boolean - Whether to show a dot for each point
    pointDot: true,

    //Number - Radius of each point dot in pixels
    pointDotRadius: 4,

    //Number - Pixel width of point dot stroke
    pointDotStrokeWidth: 1,

    //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    pointHitDetectionRadius: 20,

    //Boolean - Whether to show a stroke for datasets
    datasetStroke: true,

    //Number - Pixel width of dataset stroke
    datasetStrokeWidth: 2,

    //Boolean - Whether to fill the dataset with a colour
    datasetFill: true,

    // Function - on animation progress
    onAnimationProgress: function () {
    },

    maintainAspectRatio: true,

    onAnimationComplete: function () {
    },

    legendTemplate: '<ul class="tc-chart-js-legend"><% for (var i=0; i<datasets.length; i++){%><li><span style="background-color:<%=datasets[i].strokeColor%>"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><br/><%}%></ul>'
  };


}]);
