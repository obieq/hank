/**
 * Created by gurpreet.singh on 04/24/2015.
 */

String.prototype.format = function() {
  var formatted = this;
  for (var i = 0; i < arguments.length; i++) {
    var regexp = new RegExp('\\{'+i+'\\}', 'gi');
    formatted = formatted.replace(regexp, arguments[i]);
  }
  return formatted;
};

String.prototype.dateFormat = function($filter, onlyDate) {
  return this && this != "null" ? $filter("date")(this + "", onlyDate ? 'MM-dd-yyyy' : 'MM-dd-yyyy hh:mm a') : "";
};

String.prototype.formatTime = function(){
  var milliSeconds = this;

  if(isNaN(milliSeconds))
  {
    return "NA";
  }

  var ms = milliSeconds % 1000;
  milliSeconds = (milliSeconds - ms) / 1000;

  var secs = milliSeconds % 60;
  milliSeconds = (milliSeconds - secs) / 60;

  var mins = milliSeconds % 60;

  var hrs = (milliSeconds - mins) / 60;

  return (hrs + "").twoDigitNumber() + ':' + (mins + "").twoDigitNumber() + ':' + (secs + "").twoDigitNumber();
};

String.prototype.twoDigitNumber = function(){
  return (this < 10 ? "0" : "") + this;
};

Date.prototype.dateFormat = function($filter, onlyDate) {
  return this && this != "null" ? $filter("date")(this, onlyDate ? 'MM-dd-yyyy' : 'MM-dd-yyyy hh:mm a') : "";
};
