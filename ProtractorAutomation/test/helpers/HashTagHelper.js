/**
 * Created by vyom.sharma on 15-03-2016.
 */

var HashTagHelper = function () {

  var currentCycleDate;
  var thisobj = this;

  this.computeHashTags = function (hashTagText) {
    var defer = protractor.promise.defer();
    console.log("**********Inside compute hash tags*********** splittedHashTagArray=");
    var splittedHashTagArray = hashTagText.split('#');
    console.log(splittedHashTagArray[1].toLowerCase());
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
      console.log("*********Inside compute hash tags VARIABLE SECTION ***********");
      browser.getCurrentUrl().then(function (curUrl) {
        console.log("****************Getting current url******************");
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
    return defer.promise;
  };

  this.computeDate = function (hashTagText, correspondingValue) {
    console.log("hash tag helper inside computedata hashTagText=" + hashTagText + " correspondingValue=" + correspondingValue);
    switch (hashTagText) {
      case 'now':
      {
        currentCycleDate = new Date();
        break;
      }
      case 'variable':
      {
        var variableContainer = browser.params.config.variableContainer;
        console.log("********variableContainer************");
        console.log(variableContainer);
        for (var k = 0; k < variableContainer.length; k++) {
          if (correspondingValue == variableContainer[k].Name.toLowerCase()) {
            currentCycleDate = new Date(variableContainer[k].Value);
            console.log("Inside variable currentCycleDate=" + currentCycleDate);
            break;
          }
        }
        break;
      }
      case 'addyears':
      {
        console.log("Year*******");
        console.log(currentCycleDate);
        currentCycleDate = new Date(currentCycleDate.setFullYear(currentCycleDate.getFullYear() + correspondingValue));
        console.log("Year After*******");
        console.log(currentCycleDate);
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
        switch (correspondingValue) {
          case 'dd-mm-yyyy':
          {
            currentCycleDate = currentCycleDate.getDate() + '-' + currentCycleDate.getMonth() + 1 + '-' + currentCycleDate.getFullYear();
            break;
          }
          case 'dd/mm/yyyy':
          {
            currentCycleDate = currentCycleDate.getDate() + 1 + '/' + currentCycleDate.getMonth() + 1 + '/' + currentCycleDate.getFullYear();
            break;
          }
          case 'mm-dd-yyyy':
          {
            currentCycleDate = currentCycleDate.getMonth() + 1 + '-' + currentCycleDate.getDate() + '-' + currentCycleDate.getFullYear();
            break;
          }
          case 'mm/dd/yyyy':
          {
            console.log("current month:- " + currentCycleDate.getMonth() + " currcycdate=" + currentCycleDate);
            currentCycleDate = currentCycleDate.getMonth() + 1 + '/' + currentCycleDate.getDate() + '/' + currentCycleDate.getFullYear();
            break;
          }
          case 'dd-mm-yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getDate() + '-' + currentCycleDate.getMonth() + 1 + '-' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();
            break;
          }
          case 'dd/mm/yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getDate() + '/' + currentCycleDate.getMonth() + 1 + '/' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();
            break;
          }
          case 'mm-dd-yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getMonth() + 1 + '-' + currentCycleDate.getDate() + '-' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();
            break;
          }
          case 'mm/dd/yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getMonth() + 1 + '/' + currentCycleDate.getDate() + '/' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();

            break;
          }
        }
        break;
      }

    }

  };

};
module.exports = HashTagHelper;
