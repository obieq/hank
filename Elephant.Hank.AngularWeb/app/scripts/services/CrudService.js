/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';
app.factory('CrudService', ['$http','$q', 'RestangularCustom', function ($http,$q, Restangular) {
  return{
    getById : function(baseUrl, id){
      return Restangular.one(baseUrl, id).get();
    },
    getAll : function(baseUrl){
      return Restangular.all(baseUrl).getList();
    },
    add : function(baseUrl, objToAdd){
      return Restangular.all(baseUrl).post(objToAdd);
    },
    update:function(baseUrl, objToUpdate){
      return Restangular.one(baseUrl, objToUpdate.Id).customPUT(objToUpdate);
    },
    delete:function(baseUrl, objToUpdate){
      return Restangular.one(baseUrl, objToUpdate.Id).remove();
    },
    search : function(baseUrl, objToSearch) {
      return Restangular.all(baseUrl).post(objToSearch);
    }
  };
}]);
