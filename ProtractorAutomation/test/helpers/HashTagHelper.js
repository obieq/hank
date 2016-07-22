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
        result = thisobj.ProcessMathematicOperation([jsonHelper.ExtractVariableValue(splittedHashTagArray[1].split('~')[1].replace('{', '').replace('}', '')), jsonHelper.ExtractVariableValue(splittedHashTagArray[1].split('~')[2].replace('{', '').replace('}', ''))], hashTagAction);
        defer.fulfill(result);
      });
    }
    else if (splittedHashTagArray[1].split('~')[0].toLowerCase() == 'assign') {
      browser.getCurrentUrl().then(function (curUrl) {
        var variables = hashTagText.match(/\{(.*?)\}/g) || [];
        if (variables.length > 0) {
          result = jsonHelper.GetIndexedVariableValueFromVariableContainer(splittedHashTagArray[1].substring(splittedHashTagArray[1].indexOf('{') + 1, splittedHashTagArray[1].lastIndexOf('}')));
        }
        else {
          result = splittedHashTagArray[1].split('~')[1];
        }
        defer.fulfill(result);
      });
    }
    else if (splittedHashTagArray[1].split('~')[0].toLowerCase() == 'substring') {
      browser.getCurrentUrl().then(function (curUrl) {
        result = jsonHelper.GetIndexedVariableValueFromVariableContainer(splittedHashTagArray[1].substring(splittedHashTagArray[1].indexOf('{') + 1, splittedHashTagArray[1].lastIndexOf('}')));
        var substrIndex = splittedHashTagArray[1].split('~')[2];
        if (splittedHashTagArray[1].split('~').length == 4) {
          var indx = parseInt(splittedHashTagArray[1].split('~')[3]);
          console.log("Indx= " + indx);
          if (isNaN(indx)) {
            var delimeter = splittedHashTagArray[1].split('~')[3].replace("'", "").replace("'", "");
            console.log("delimeter= " + delimeter);
            var delimeterIndx = result.indexOf(delimeter) + 1;
            console.log("delimeterIndx= " + delimeterIndx);
            result = eval("result.substr(delimeterIndx).substr(" + substrIndex + ")");
          }
          else {
            result = eval("result.substr(" + substrIndex + ")");
          }
        }
        else {
          result = eval("result.substr(" + substrIndex + ")");
        }
        defer.fulfill(result);
      });
    }
    else if (splittedHashTagArray[1].split('~')[0].toLowerCase() == 'split') {
      browser.getCurrentUrl().then(function (curUrl) {
        result = jsonHelper.GetIndexedVariableValueFromVariableContainer(splittedHashTagArray[1].substring(splittedHashTagArray[1].indexOf('{') + 1, splittedHashTagArray[1].lastIndexOf('}')));
        var splitDelimeter = splittedHashTagArray[1].split('~')[2];
        var defaultArray = [["Output"]];
        var splittedArray = result.split(splitDelimeter);
        for (var k = 0; k < splittedArray.length; k++) {
          defaultArray.push([splittedArray[k]]);
        }
        defer.fulfill(JSON.stringify(defaultArray));
      });
    }
    else if (splittedHashTagArray[1].split('~')[0].toLowerCase() == 'newguid') {
      var newGuid = jsonHelper.createGuid();
      defer.fulfill(newGuid);
    }
    return defer.promise;
  };

  this.ProcessMathematicOperation = function (valueArray, hashTagAction) {
    var valueAfterOperaton;
    var valueToPrepend;
    var operationValueArray = [];
    for (var k = valueArray.length - 1; k > -1; k--) {
      valueToPrepend = '';
      var checkForDigit = true;
      var strReverseArray = valueArray[k].split('').reverse();
      for (var i = 0; i < strReverseArray.length; i++) {
        if ((!isNaN(strReverseArray[i]) || strReverseArray[i] == '.' ) && checkForDigit) {
          operationValueArray[k] = strReverseArray[i] + operationValueArray[k];
        }
        else {
          checkForDigit = false;
          valueToPrepend = strReverseArray[i] + valueToPrepend;
        }
      }
    }
    valueAfterOperaton = valueToPrepend;
    valueAfterOperaton += hashTagAction == 'subtract' ? (parseFloat(operationValueArray[0]) - parseFloat(operationValueArray[1])).toFixed(2) : (parseFloat(operationValueArray[0]) + parseFloat(operationValueArray[1])).toFixed(2);
    return valueAfterOperaton;
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
