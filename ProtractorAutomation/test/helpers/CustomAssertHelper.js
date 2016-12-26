/**
 * Created by vyom.sharma on 22-07-2016.
 */

require('./DateHelper.js');

var CustomAssertHelper = function () {

  var JsonHelper = require('./JsonHelper.js');
  var jsonHelper = new JsonHelper();

  this.arrayContainsAssert = function (value, variable) {
    var varName = variable.split('[')[0];
    var varValue = jsonHelper.ExtractVariableValue(varName);
    var varJsonValue = JSON.parse(varValue);
    var colIndexes = this.getColIndex(variable, varJsonValue);
    for (var i = 0; i < colIndexes.length; i++) {
      for (var l = 1; l < varJsonValue.length; l++) {
        if (varJsonValue[l][colIndexes[i].indx] == value) {
          expect(value).toEqual(varJsonValue[l][colIndexes[i].indx]);
          return;
        }
      }
    }
    return;
  };

  this.arrayComparisionAssert = function (var1, var2, isContainOperation) {
    var varName1 = var1.split('[')[0];
    var varName2 = var2.split('[')[0];
    console.log("var1= " + var1 + " var2= " + var2);
    console.log("varName1= " + varName1 + " varName2= " + varName2);
    var var1Value = jsonHelper.ExtractVariableValue(varName1);
    var var2Value = jsonHelper.ExtractVariableValue(varName2);
    console.log("var1Value= " + var1Value + " var2Value" + var2Value);
    var var1JsonValue = JSON.parse(var1Value);
    var var2JsonValue = JSON.parse(var2Value);

    var1JsonValue = this.formatColumn(var1, var1JsonValue);
    var2JsonValue = this.formatColumn(var2, var2JsonValue);

    var colIndexes1 = this.getColIndex(var1, var1JsonValue);
    var colIndexes2 = this.getColIndex(var2, var2JsonValue);

    if (colIndexes1.length > 0 && colIndexes2.length == 0) {
      colIndexes2 = colIndexes1;
    }
    else if (colIndexes2.length > 0 && colIndexes1.length == 0) {
      colIndexes1 = colIndexes2;
    }
    else if (colIndexes2.length == 0 && colIndexes1.length == 0) {
      for (var n = 0; n < var1JsonValue[0].length; n++) {
        colIndexes1.push({indx: n, name: var1JsonValue[0][n]});
      }
      for (n = 0; n < var2JsonValue[0].length; n++) {
        colIndexes2.push({indx: n, name: var2JsonValue[0][n]});
      }
    }
    console.log("colIndexes1= " + colIndexes1 + " colIndexes2" + colIndexes2);

    if ((var1JsonValue.length != var2JsonValue.length || colIndexes1.length != colIndexes2.length) && !isContainOperation) {
      console.log("inside not equal lenght   section");
      expect("variable1 length").toEqual("variable1 length");
      return false;
    }
    else if (colIndexes1.length != colIndexes2.length) {
      console.log("inside not equal  colum section");
      expect("variable1 comparisions column").toEqual("variable2 comparisions column");
      return false;
    }
    else {
      var forwardTrace = this.checkValueExist(var1JsonValue, var2JsonValue, colIndexes1, colIndexes2);
      var backwardTrace = forwardTrace && !isContainOperation ? this.checkValueExist(var2JsonValue, var1JsonValue, colIndexes2, colIndexes1) : false;
      if (forwardTrace && backwardTrace) {
        return true;
      }
      else if (forwardTrace && isContainOperation) {
        return true;
      }
      else if (!forwardTrace) {
        expect("First Variable").toEqual("Second Variable");
        return false
      }
      else if (!backwardTrace) {
        expect("Second Variable").toEqual("First Variable");
        return false
      }
    }
    return true;
  };

  this.checkValueExist = function (array1, array2, array1ColIndex, array2ColIndex) {
    console.log("Inside checkValue");
    for (var i = 1; i < array1.length; i++) {
      var isRowMatched = false;
      for (var j = 1; j < array2.length; j++) {
        var isValueExist = false;
        for (var k = 0; k < array1ColIndex.length; k++) {
          if (array1[i][array1ColIndex[k].indx] == array2[j][array2ColIndex[k].indx]) {
            isValueExist = true;
          }
          else {
            isValueExist = false;
            break;
          }
        }
        if (isValueExist) {
          console.log("Inside is value exist");
          isRowMatched = true;
          break;
        }
      }
      if (!isRowMatched) {
        console.log("Inside is row matched");
        return false;
      }
    }
    return true;
  };

  this.getIndexes = function (varName) {
    var indexes = varName.match(/\[(.*?)\]/g) || [];
    for (var i = 0; i < indexes.length; i++) {
      indexes[i] = indexes[i].replace("[", "").replace("]", "");
    }
    return indexes;
  };

  this.getColIndex = function (varName, varValue) {
    var colIndexes = [];
    var indexes = this.getIndexes(varName);
    var varJsonValue = varValue;
    for (var k = 0; k < indexes.length; k++) {
      var format = indexes[k].split('|');
      if (!!format[1]) {
        indexes[k] = format[0];
      }
      for (var i = 0; i < varJsonValue[0].length; i++) {
        if (varJsonValue[0][i] == indexes[k]) {
          colIndexes.push({indx: i, name: indexes[k]});
          break;
        }
      }
    }
    return colIndexes;
  };

  this.formatColumn = function (varName, value) {
    console.log("Inside format column previous value:- ");
    console.log(value);
    var indexes = this.getIndexes(varName);
    console.log("Inside format column varName= " + varName);
    console.log("Inside format column indexes:= ");
    console.log(indexes);
    for (var k = 0; k < indexes.length; k++) {
      var format = indexes[k].split('|');
      console.log("Inside format column format[1]= " + format[1]);
      if (!!format[1]) {
        if (format[1].startsWith('format')) {
          console.log("Inside format column inside starts with");
          for (var i = 0; i < value[0].length; i++) {
            if (value[0][i] == format[0]) {
              console.log("goes inside it");
              for (var l = 1; l < value.length; l++) {
                console.log("goes in it value[" + l + "]" + "[" + i + "]= " + value[l][i]);
                var dateToConvert = new Date(value[l][i]);
                console.log("goes in it dateToConvert= " + dateToConvert);
                value[l][i] = dateToConvert.format(format[1].split('~')[1]);
              }
              break;
            }
          }
        }
      }
    }
    console.log("Inside format column value after format function:- ");
    console.log(value);
    return value;
  };

};
module.exports = CustomAssertHelper;
