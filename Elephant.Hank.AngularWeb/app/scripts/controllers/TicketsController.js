/**
 * Created by Mohammad iliyash on 22-06-2016.
 */

'use strict';

app.controller('TicketsController',['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi) {
    $scope.Tickets = [ ];
    $scope.Ticket = {} ;
    $scope.TicketData = {} ;
    
    $scope.AssignedTo = [ ];
    $scope.Status = [ ];
    $scope.Type = [ ];
    $scope.Priority = [ ];
    
    if($scope.AssignedTo.length == 0 || $scope.AssignedTo.Status ||  $scope.AssignedTo.Type || $scope.AssignedTo.Priority){
    crudService.getById(ngAppSettings.TicketsDataUrl).then(function(responseData){
        $scope.TicketData = responseData.Item;
        $scope.AssignedTo =$scope.TicketData.AssignedTo;
        $scope.Status =$scope.TicketData.Status;
        $scope.Type =$scope.TicketData.Type;
        $scope.Priority =$scope.TicketData.Priority;
         
        
      },function(responseData){ commonUi.showErrorPopup(responseData); });
    }
    $scope.getAllTickets = function(){
      crudService.getAll(ngAppSettings.TicketsUrl).then(function(response){
        $scope.Tickets = response;        
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.getTicketById = function(){
      crudService.getById(ngAppSettings.TicketsUrl, $stateParams.Id).then(function(response){
        $scope.Ticket = response.Item;
        $scope.TicketHistory = response.Item.TicketHistory;
        
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.updateTicket = function(){
      crudService.update(ngAppSettings.TicketsUrl,$scope.Ticket).then(function(response){
        $state.go("Tickets.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };

    $scope.addTicket = function(){
      crudService.add(ngAppSettings.TicketsUrl, $scope.Ticket).then(function(response){
        $state.go("Tickets.List");
      },function(response){ commonUi.showErrorPopup(response); });
    };
    
    $scope.formatTicketData = function (item) {
     var user =  $.grep($scope.AssignedTo, function(e){ return e.Value == item; });
     if(user.length > 0)
     {
     return user[0].Name;  
     }
     else
     return "User Not Found"
     
    }
    
    $scope.setClickedRow = function(index)
    {
      alert(index);
    }
    
    $scope.sortType     = 'name'; // set the default sort type
    $scope.sortReverse  = true;  // set the default sort order
  
  
    
  }]);
