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
        console.log('hashTagText: ' + hashTagText);
        var defer = protractor.promise.defer();
        var splittedHashTagArray = hashTagText.split('#');

        if(splittedHashTagArray.length < 2) {
            defer.reject({error: "Hashtag not found!"});
        }
        else {
            var tagNameLowerCase = splittedHashTagArray[1].split('~')[0].toLowerCase();
            var variables = splittedHashTagArray[1].match(/\{(.*?)\}/g) || [];
            var variableName = splittedHashTagArray[1].substring(splittedHashTagArray[1].indexOf('{') + 1, splittedHashTagArray[1].lastIndexOf('}'));
            console.log('variableName= ' + variableName);

            if (tagNameLowerCase == 'now' || tagNameLowerCase == 'variable') {
                for (var i = 1; i < splittedHashTagArray.length; i++) {
                    var splitedTextValue = splittedHashTagArray[i].split('~');
                    if (splitedTextValue.length > 1) {
                        this.computeDate(splitedTextValue[0].toLowerCase(), (splitedTextValue[1] + "").toLowerCase());
                    }
                    else {
                        this.computeDate(splittedHashTagArray[i].toLowerCase());
                    }
                }
                defer.fulfill(currentCycleDate);
            }
            else if (tagNameLowerCase == 'add' || tagNameLowerCase == 'subtract') {
                result = thisobj.ProcessMathematicOperation([jsonHelper.ExtractVariableValue(splittedHashTagArray[1].split('~')[1].replace('{', '').replace('}', '')), jsonHelper.ExtractVariableValue(splittedHashTagArray[1].split('~')[2].replace('{', '').replace('}', ''))], tagNameLowerCase);
                defer.fulfill(result);
            }
            else if (tagNameLowerCase == 'assign') {
                if (variables.length > 0) {
                    result = jsonHelper.GetIndexedVariableValueFromVariableContainer(variableName);
                }
                else {
                    result = splittedHashTagArray[1].split('~')[1];
                }
                defer.fulfill(result);
            }
            else if (tagNameLowerCase == 'substring') {
                result = jsonHelper.GetIndexedVariableValueFromVariableContainer(variableName);
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
            }
            else if (tagNameLowerCase == 'split') {
                result = jsonHelper.GetIndexedVariableValueFromVariableContainer(variableName);
                var splitDelimeter = splittedHashTagArray[1].split('~')[2];
                var defaultArray = [["Output"]];
                var splittedArray = result.split(splitDelimeter);
                for (var k = 0; k < splittedArray.length; k++) {
                    defaultArray.push([splittedArray[k]]);
                }
                defer.fulfill(JSON.stringify(defaultArray));
            }
            else if (tagNameLowerCase == 'concat') {
                var concatenatedValues = '';
                for (var varCount = 0; varCount < variables.length; varCount++) {
                    concatenatedValues += jsonHelper.GetIndexedVariableValueFromVariableContainer(variables[varCount].substring(variables[varCount].indexOf('{') + 1, variables[varCount].lastIndexOf('}')));
                }
                defer.fulfill(concatenatedValues);
            }
            else if (tagNameLowerCase == 'newguid') {
                var newGuid = jsonHelper.createGuid();
                defer.fulfill(newGuid);
            }
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
        if(!isNaN(correspondingValue)) {
            correspondingValue = parseInt(correspondingValue);
        }

        switch (hashTagText) {
            case 'now':
            {
                currentCycleDate = new Date();
                break;
            }
            case 'variable':
            {
                correspondingValue = correspondingValue + "";
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
