/**
 * Created by vyom.sharma on 15-03-2016.
 */

var HashTagHelper = function () {

  var currentCycleDate;

  this.computeHashTags = function (hashTagText) {
    var splittedHashTagArray = hashTagText.split('#');
    if (splittedHashTagArray[1].toLowerCase() == 'now') {
      for (var i = 1; i < splittedHashTagArray.length; i++) {
        var splitedTextValue = splittedHashTagArray[i].split('`');
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
      return currentCycleDate;
    }
  };

  this.computeDate = function (hashTagText, correspondingValue) {
    console.log("hash tag helper inside computedata hashTagText=" + hashTagText + " correspondingValue=" + correspondingValue);
    switch (hashTagText) {
      case 'now':
      {
        currentCycleDate = new Date();
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
        switch (correspondingValue) {
          case 'dd-mm-yyyy':
          {
            currentCycleDate = currentCycleDate.getDate() + '-' + currentCycleDate.getMonth() + '-' + currentCycleDate.getFullYear();
            break;
          }
          case 'dd/mm/yyyy':
          {
            currentCycleDate = currentCycleDate.getDate() + '/' + currentCycleDate.getMonth() + '/' + currentCycleDate.getFullYear();
            break;
          }
          case 'mm-dd-yyyy':
          {
            currentCycleDate = currentCycleDate.getMonth() + '-' + currentCycleDate.getDate() + '-' + currentCycleDate.getFullYear();
            break;
          }
          case 'mm/dd/yyyy':
          {
            currentCycleDate = currentCycleDate.getMonth() + '/' + currentCycleDate.getDate() + '/' + currentCycleDate.getFullYear();
            break;
          }
          case 'dd-mm-yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getDate() + '-' + currentCycleDate.getMonth() + '-' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();
            break;
          }
          case 'dd/mm/yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getDate() + '/' + currentCycleDate.getMonth() + '/' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();
            break;
          }
          case 'mm-dd-yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getMonth() + '-' + currentCycleDate.getDate() + '-' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();
            break;
          }
          case 'mm/dd/yyyy hh:mm:ss':
          {
            currentCycleDate = currentCycleDate.getMonth() + '/' + currentCycleDate.getDate() + '/' + currentCycleDate.getFullYear() + ' ' + currentCycleDate.getHours() + ':' + currentCycleDate.getMinutes() + ':' + currentCycleDate.getSeconds();

            break;
          }
        }
        break;
      }

    }

  };

};
module.exports = HashTagHelper;
