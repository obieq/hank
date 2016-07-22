/**
 * Created by vyom.sharma on 22-07-2016.
 */

var CustomAssertHelper = function () {

  var JsonHelper = require('./JsonHelper.js');
  var jsonHelper = new JsonHelper();

  this.arrayComparisionAssert = function (var1, var2) {
    var varName1 = var1.split('[')[0];
    var varName2 = var2.split('[')[0];
    console.log("varName1= " + varName1 + " varName2" + varName2);
    var var1Value = jsonHelper.ExtractVariableValue(varName1);
    var var2Value = jsonHelper.ExtractVariableValue(varName2);
    console.log("var1Value= " + var1Value + " var2Value" + var2Value);
    var colIndexes1 = this.getColIndex(var1, var1Value);
    var colIndexes2 = this.getColIndex(var2, var2Value);
    var var1JsonValue = JSON.parse(var1Value);
    var var2JsonValue = JSON.parse(var2Value);
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
    if (var1JsonValue.length != var2JsonValue.length || colIndexes1.length != colIndexes2.length) {
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
      for (var k = 0; k < colIndexes1.length; k++) {
        for (var l = 1; l < var1JsonValue.length; l++) {
          console.log("colIndexes2[k].indx= " + colIndexes2[k].indx + " var1JsonValue[l][colIndexes1[k].indx]= " + var1JsonValue[l][colIndexes1[k].indx])
          if (!this.checkValueExist(var2JsonValue, colIndexes2[k].indx, var1JsonValue[l][colIndexes1[k].indx])) {
            expect("variable " + varName1 + " columns " + colIndexes1[k].name).toEqual("variable " + varName2 + " columns " + colIndexes2[k].name);
            return false;
          }
        }
        for (var m = 1; m < var2JsonValue.length; m++) {
          console.log("colIndexes1[k].indx= " + colIndexes1[k].indx + " var2JsonValue[m][colIndexes2[k].indx]= " + var2JsonValue[m][colIndexes2[k].indx])
          if (!this.checkValueExist(var1JsonValue, colIndexes1[k].indx, var2JsonValue[m][colIndexes2[k].indx])) {
            //expectation failed
            expect("variable " + varName2 + " columns " + colIndexes2[k].name).toEqual("variable " + varName1 + " columns " + colIndexes1[k].name);
            return false;
          }
        }
      }
    }
    return true;
  };

  this.checkValueExist = function (arr, indx, value) {
    console.log("Inside checkValue");
    var isValueExist = false;
    for (var k = 1; k < arr.length; k++) {
      if (arr[k][indx] == value) {
        isValueExist = true;
        break;
      }
    }
    return isValueExist;
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
    var varJsonValue = JSON.parse(varValue);
    for (var k = 0; k < indexes.length; k++) {
      for (var i = 0; i < varJsonValue[0].length; i++) {
        if (varJsonValue[0][i] == indexes[k]) {
          colIndexes.push({indx: i, name: indexes[k]});
          break;
        }
      }
    }
    return colIndexes;
  };

};
module.exports = CustomAssertHelper;
