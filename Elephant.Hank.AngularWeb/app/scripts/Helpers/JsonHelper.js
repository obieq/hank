/**
 * Created by vyom.sharma on 20-12-2016.
 */

'use strict';
app.factory('JsonHelper', ['$stateParams',
  function ($stateParams) {
    return {
      deleteByProperty: function (array, property, compareValue) {
        for (var i = 0; i < array.length; i++) {
          if (array[i][property] == compareValue) {
            array.splice(i, 1);
            break;
          }
        }
        return array;
      }
    };
  }]);
