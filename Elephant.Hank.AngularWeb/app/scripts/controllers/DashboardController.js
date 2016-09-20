/**
 * Created by vyom.sharma on 16-08-2016.
 */

app.controller('DashboardController', ['$scope', 'CrudService', 'ngAppSettings', '$filter', 'CommonUiService', function ($scope, crudService, ngAppSettings, $filter, commonUi) {
  $scope.date = {};
  $scope.date.endDate = moment();
  $scope.date.startDate = moment().subtract(7, "days");


  $scope.onWebsiteChange = function (IsDefault) {
    $scope.CancelledTest = $scope.InProgressTest = $scope.InqueueTest = $scope.TotalTest = $scope.PassedTest = $scope.FailedTest = $scope.UnProcessedTest = 0;
    crudService.getAll(ngAppSettings.WebSiteUrl).then(function (response) {
      $scope.WebsiteList = response;
      IsDefault ? $scope.WebsiteId = $scope.WebsiteList[0].Id : $scope.WebsiteId;
      crudService.getAll(ngAppSettings.ReportChartUrl.format($scope.WebsiteId, $scope.date.startDate.format("YYYY-MM-DD"), $scope.date.endDate.format("YYYY-MM-DD"))).then(function (response) {
        var resultTotal = response.map(function (a) {
          return a.Total;
        });
        var resultPassed = response.map(function (a) {
          return a.CountPassed;
        });
        var resultFailed = response.map(function (a) {
          return a.CountFailed;
        });
        var resultUnProcessed = response.map(function (a) {
          return a.CountUnProcessed;
        });
        var resultCancelled = response.map(function (a) {
          return a.CountCancelled;
        });
        var resultInqueue = response.map(function (a) {
          return a.CountInqueue;
        });
        var resultInProgress = response.map(function (a) {
          return a.CountInProgress;
        });
        var labels = response.map(function (a) {
          return a.CreatedOn.split('T')[0];
        });
        $scope.data = {};
        chartData.datasets[0].data = resultPassed;
        chartData.datasets[1].data = resultFailed;
        chartData.datasets[2].data = resultUnProcessed;
        chartData.datasets[3].data = resultCancelled;
        chartData.labels = labels;
        $scope.data = chartData;
        for (var i = 0; i < resultTotal.length; i++) {
          $scope.TotalTest += resultTotal[i];
        }
        for (i = 0; i < resultPassed.length; i++) {
          $scope.PassedTest += resultPassed[i];
        }
        for (i = 0; i < resultFailed.length; i++) {
          $scope.FailedTest += resultFailed[i];
        }
        for (i = 0; i < resultUnProcessed.length; i++) {
          $scope.UnProcessedTest += resultUnProcessed[i];
        }
        for (i = 0; i < resultInqueue.length; i++) {
          $scope.InqueueTest += resultInqueue[i];
        }
        for (i = 0; i < resultInProgress.length; i++) {
          $scope.InProgressTest += resultInProgress[i];
        }
        for (i = 0; i < resultCancelled.length; i++) {
          $scope.CancelledTest += resultCancelled[i];
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
      crudService.getAll(ngAppSettings.ReportPieChartUrl.format($scope.WebsiteId, $scope.date.startDate.format("YYYY-MM-DD"), $scope.date.endDate.format("YYYY-MM-DD"), 8)).then(function (response) {
        $scope.pieDataPassed = angular.copy(pieChartData);
        for (var j = 0; j < response.length; j++) {
          for (var k = 0; k < pieChartData.labels.length; k++) {
            if (response[j].Label == pieChartData.labels[k]) {
              $scope.pieDataPassed.datasets[0].data[k] = response[j].Value;
              break;
            }
          }
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });

      crudService.getAll(ngAppSettings.ReportPieChartUrl.format($scope.WebsiteId, $scope.date.startDate.format("YYYY-MM-DD"), $scope.date.endDate.format("YYYY-MM-DD"), 9)).then(function (response) {
        $scope.pieDataFailed = angular.copy(pieChartData);
        for (var j = 0; j < response.length; j++) {
          for (var k = 0; k < pieChartData.labels.length; k++) {
            if (response[j].Label == pieChartData.labels[k]) {
              $scope.pieDataFailed.datasets[0].data[k] = response[j].Value;
              break;
            }
          }
        }
      }, function (response) {
        commonUi.showErrorPopup(response);
      });

    }, function (response) {
      commonUi.showErrorPopup(response);
    });
  };

  //$scope.chartOptions = {};

  $scope.onChartClick = function (event) {
    console.log('LineController', 'onChartClick', event);
  };


  var chartData = {
    labels: ["January", "February", "March", "April", "May", "June", "July"],
    datasets: [
      {
        label: 'Passed',
        fill: false,
        lineTension: 0.1,
        backgroundColor: 'rgba(57, 131, 44, 0.4)',
        borderColor: 'rgba(57, 131, 44, 1)',
        borderCapStyle: 'butt',
        borderDash: [],
        borderDashOffset: 0.0,
        borderJoinStyle: 'miter',
        pointBorderColor: "rgba(75,192,192,1)",
        pointBackgroundColor: '#fff',
        pointHighlightFill: '#fff',
        pointBorderWidth: 1,
        pointHoverRadius: 5,
        pointHoverBackgroundColor: 'rgba(57, 131, 44, 1)',
        pointHoverBorderColor: "rgba(220,220,220,1)",
        pointHoverBorderWidth: 2,
        pointRadius: 1,
        pointHitRadius: 10,
        spanGaps: false,
        data: []
      },
      {
        label: 'Failed',
        fill: false,
        lineTension: 0.1,
        backgroundColor: 'rgba(252, 5, 5, 0.4)',
        borderColor: 'rgba(252, 5, 5, 1)',
        borderCapStyle: 'butt',
        borderDash: [],
        borderDashOffset: 0.0,
        borderJoinStyle: 'miter',
        pointBorderColor: "rgba(252, 5, 5,1)",
        pointBackgroundColor: '#fff',
        pointHighlightFill: '#fff',
        pointBorderWidth: 1,
        pointHoverRadius: 5,
        pointHoverBackgroundColor: 'rgba(252, 5, 5, 1)',
        pointHoverBorderColor: "rgba(220,220,220,1)",
        pointHoverBorderWidth: 2,
        pointRadius: 1,
        pointHitRadius: 10,
        spanGaps: false,
        data: []
      },
      {
        label: 'Un-Processed',
        fill: false,
        lineTension: 0.1,
        backgroundColor: 'rgba(255, 208, 50, 0.4)',
        borderColor: 'rgba(255, 208, 50,1)',
        borderCapStyle: 'butt',
        borderDash: [],
        borderDashOffset: 0.0,
        borderJoinStyle: 'miter',
        pointBorderColor: "rgba(255, 208, 50,1)",
        pointBackgroundColor: '#fff',
        pointHighlightFill: '#fff',
        pointBorderWidth: 1,
        pointHoverRadius: 5,
        pointHoverBackgroundColor: 'rgba(255, 208, 50, 1)',
        pointHoverBorderColor: "rgba(220,220,220,1)",
        pointHoverBorderWidth: 2,
        pointRadius: 1,
        pointHitRadius: 10,
        spanGaps: false,
        data: []
      }
      ,
      {
        label: 'Cancelled',
        fill: false,
        lineTension: 0.1,
        backgroundColor: 'rgba(146, 74, 44, 0.4)',
        borderColor: 'rgba(146, 74, 44,1)',
        borderCapStyle: 'butt',
        borderDash: [],
        borderDashOffset: 0.0,
        borderJoinStyle: 'miter',
        pointBorderColor: "rgba(146, 74, 44,1)",
        pointBackgroundColor: '#fff',
        pointHighlightFill: '#fff',
        pointBorderWidth: 1,
        pointHoverRadius: 5,
        pointHoverBackgroundColor: 'rgba(146, 74, 44, 1)',
        pointHoverBorderColor: "rgba(220,220,220,1)",
        pointHoverBorderWidth: 2,
        pointRadius: 1,
        pointHitRadius: 10,
        spanGaps: false,
        data: []
      }
    ]
  };

  var pieChartData={
    labels: [
      "firefox",
      "chrome",
      "internet explorer"
    ],
    datasets: [
      {
        data: [0,0,0],
        backgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56"
        ],
        hoverBackgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56"
        ]
      }
    ]
  };


  /*var pieChartData = [
    {
      value: 0,
      color: '#F7464A',
      highlight: '#FF5A5E',
      label: 'firefox'
    },
    {
      value: 0,
      color: '#46BFBD',
      highlight: '#5AD3D1',
      label: 'chrome'
    },
    {
      value: 0,
      color: '#FDB45C',
      highlight: '#FFC870',
      label: 'internet explorer'
    }
  ];*/


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

 /* $scope.pieOptions = {

    // Sets the chart to be responsive
    responsive: true,

    //Boolean - Whether we should show a stroke on each segment
    segmentShowStroke: true,

    //String - The colour of each segment stroke
    segmentStrokeColor: '#fff',

    //Number - The width of each segment stroke
    segmentStrokeWidth: 2,

    //Number - The percentage of the chart that we cut out of the middle
    percentageInnerCutout: 50, // This is 0 for Pie charts

    //Number - Amount of animation steps
    animationSteps: 100,

    //String - Animation easing effect
    animationEasing: 'easeOutBounce',

    //Boolean - Whether we animate the rotation of the Doughnut
    animateRotate: true,

    //Boolean - Whether we animate scaling the Doughnut from the centre
    animateScale: false,

    //String - A legend template
    legendTemplate: '<ul class="tc-chart-js-legend"><% for (var i=0; i<segments.length; i++){%><li><span style="background-color:<%=segments[i].fillColor%>"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><br/><%}%></ul>'

  };*/

}]);
