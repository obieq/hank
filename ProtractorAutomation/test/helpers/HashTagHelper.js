/**
 * Created by vyom.sharma on 15-03-2016.
 */

require('./DateHelper.js');

var HashTagHelper = function () {
  var JsonHelper = require('./JsonHelper.js');
  var jsonHelper = new JsonHelper();
  var currentCycleDate;
  var thisobj = this;
  var result;

  this.computeHashTags = function (hashTagText) {
    var defer = protractor.promise.defer();
    var splittedHashTagArray = hashTagText.split('#');
    if (splittedHashTagArray[1].split('~')[0].toLowerCase() == 'now') {
      for (var i = 1; i < splittedHashTagArray.length; i++) {
        var splitedTextValue = splittedHashTagArray[i].split('~');
        if (splitedTextValue.length > 1) {
          if (parseInt(splitedTextValue[1])) {
            this.computeDate(splitedTextValue[0].toLowerCase(), parseInt(splitedTextValue[1]));
          }
          else {
            this.computeDate(splitedTextValue[0].toLowerCase(), splitedTextValue[1].toLowerCase());
          }
        }
        else {
          this.computeDate(splittedHashTagArray[i].toLowerCase());
        }
      }
      defer.fulfill(currentCycleDate);
    }
    else if (splittedHashTagArray[1].split('~')[0].toLowerCase() == 'variable') {
      browser.getCurrentUrl().then(function (curUrl) {
        for (var i = 1; i < splittedHashTagArray.length; i++) {
          var splitedTextValue = splittedHashTagArray[i].split('~');
          if (splitedTextValue.length > 1) {
            if (parseInt(splitedTextValue[1])) {
              thisobj.computeDate(splitedTextValue[0].toLowerCase(), parseInt(splitedTextValue[1]));
            }
            else {
              thisobj.computeDate(splitedTextValue[0].toLowerCase(), splitedTextValue[1].toLowerCase());
            }
          }
          else {
            thisobj.computeDate(splittedHashTagArray[i].toLowerCase());
          }
        }
        defer.fulfill(currentCycleDate);
      });
    }
    else if (splittedHashTagArray[1].split('~')[0].toLowerCase() == 'add' || splittedHashTagArray[1].split('~')[0].toLowerCase() == 'subtract') {
      browser.getCurrentUrl().then(function (curUrl) {
        var hashTagAction = splittedHashTagArray[1].split('~')[0].toLowerCase();
        var firstValue = '';
        var secondValue = '';
        var variablesNameList = hashTagText.match(/\{(.*?)\}/g) || [];
        if (variablesNameList.length == 1) {
          firstValue = jsonHelper.ExtractVariableValue(variablesNameList[0].replace('{', '').replace('}', ''));
          result = thisobj.ProcessAdd(firstValue, undefined, splittedHashTagArray[1].split('~')[2], hashTagAction);
        }
        if (variablesNameList.length == 2) {
          firstValue = jsonHelper.ExtractVariableValue(variablesNameList[0].replace('{', '').replace('}', ''));
          secondValue = jsonHelper.ExtractVariableValue(variablesNameList[1].replace('{', '').replace('}', ''));
          result = thisobj.ProcessAdd(firstValue, secondValue, undefined, hashTagAction);
        }
        defer.fulfill(result);
      });
    }
    return defer.promise;
  };

  this.ProcessAdd = function (firstValue, secondValue, value, hashTagAction) {
    var incrementedValue;
    var checkForDigit = true;
    var strValueToIncrementFirst = '';
    var valueToPrependFirst = '';
    var splittedStringFirst = firstValue.split('').reverse();
    for (var i = 0; i < splittedStringFirst.length; i++) {
      if (!isNaN(splittedStringFirst[i]) && checkForDigit) {
        strValueToIncrementFirst = splittedStringFirst[i] + strValueToIncrementFirst;
      }
      else {
        checkForDigit = false;
        valueToPrependFirst = splittedStringFirst[i] + valueToPrependFirst;
      }
    }
    checkForDigit = true;
    if (secondValue != undefined) {
      var strValueToIncrementSecond = '';
      var valueToPrependSecond = '';
      var splittedStringSecond = secondValue.split('').reverse();
      for (var i = 0; i < splittedStringSecond.length; i++) {
        if (!isNaN(splittedStringSecond[i]) && checkForDigit) {
          strValueToIncrementSecond = splittedStringSecond[i] + strValueToIncrementSecond;
        }
        else {
          checkForDigit = false;
          valueToPrependSecond = splittedStringSecond[i] + valueToPrependSecond;
        }
      }
    }
    if (value == undefined) {
      if (hashTagAction.toLowerCase() == 'subtract') {
        incrementedValue = parseInt(strValueToIncrementFirst) - parseInt(strValueToIncrementSecond);
      }
      else if (hashTagAction.toLowerCase() == 'add') {
        incrementedValue = parseInt(strValueToIncrementFirst) + parseInt(strValueToIncrementSecond);
      }
    }
    else {
      if (hashTagAction.toLowerCase() == 'subtract') {
        incrementedValue = parseInt(strValueToIncrementFirst) - parseInt(value);
      }
      else if (hashTagAction.toLowerCase() == 'add') {
        incrementedValue = parseInt(strValueToIncrementFirst) + parseInt(value);
      }
    }
    console.log("Inside ProcessAdd result= " + valueToPrependFirst + incrementedValue);
    return valueToPrependFirst + incrementedValue;
  };

  this.computeDate = function (hashTagText, correspondingValue) {

    switch (hashTagText) {
      case 'now':
      {
        currentCycleDate = new Date();
        break;
      }
      case 'variable':
      {
        var variableContainer = browser.params.config.variableContainer;

        for (var k = 0; k < variableContainer.length; k++) {
          if (correspondingValue == variableContainer[k].Name.toLowerCase()) {
            currentCycleDate = new Date(variableContainer[k].Value);
            break;
          }
        }
        break;
      }
      case 'addyears':
      {
        currentCycleDate = new Date(currentCycleDate.setFullYear(currentCycleDate.getFullYear() + correspondingValue));
        break;
      }
      case 'addmonths':
      {
        currentCycleDate = new Date(currentCycleDate.setMonth(currentCycleDate.getMonth() + correspondingValue));
        break;
      }
      case 'adddays':
      {
        currentCycleDate = new Date(currentCycleDate.setDate(currentCycleDate.getDate() + correspondingValue));
        break;
      }
      case 'addhours':
      {
        currentCycleDate = new Date(currentCycleDate.setHours(currentCycleDate.getHours() + correspondingValue));
        break;
      }
      case 'addminutes':
      {
        currentCycleDate = new Date(currentCycleDate.setMinutes(currentCycleDate.getMinutes() + correspondingValue));
        break;
      }
      case 'format':
      {
        currentCycleDate = currentCycleDate.format(correspondingValue);
        break;
      }

    }

  };

};
module.exports = HashTagHelper;
