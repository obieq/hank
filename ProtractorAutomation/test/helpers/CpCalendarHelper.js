/**
 * Created by vyom.sharma on 27-12-2016.
 */

require('./DateHelper.js');

var CpCalendarHelper = function () {
    var JsonHelper = require('./JsonHelper.js');
    var jsonHelper = new JsonHelper();
    this.PreviousButtonLocator = "//span[.='«']";
    this.NextButtonLocator = "//span[.='»']";
    this.CurrentMonthLocator = "//h2[@class='calendar-md-title']/span";
    this.DayLocator = "//div[@tabindex='{0}']";
    this.MaxIteration = 6;
    this.monthNames = [
        "January", "February", "March",
        "April", "May", "June",
        "July", "August", "September",
        "October", "November", "December"
    ];
    var thisObj = this;

    this.setCalendarDate = function (date) {
        var dateToSet = new Date(date).format("mm/dd/yyyy");
        dateToSet = new Date(dateToSet);
        this.navigateMonths(dateToSet, 1, function () {
            element(by.xpath(jsonHelper.format(thisObj.DayLocator, dateToSet.getDate()))).click();
        });
    };

    this.navigateMonths = function (date, count, callBack) {
        this.getCurrentMonthAndYearFromCalendar(function (text) {
            var currYear = parseInt(text.split(' ')[1]);
            var currMonth = thisObj.monthNames.indexOf(text.split(' ')[0]);
            if (date.getMonth() == currMonth && date.getFullYear() == currYear) {
                callBack();
            }
            else {
                if (date.getFullYear() == currYear) {
                    if (date.getMonth() > currMonth) {
                        element(by.xpath(thisObj.NextButtonLocator)).click();
                    }
                    else {
                        element(by.xpath(thisObj.PreviousButtonLocator)).click();
                    }
                }
                else if (date.getFullYear() > currYear) {
                    //next
                    element(by.xpath(thisObj.NextButtonLocator)).click();
                }
                else if (date.getFullYear() < currYear) {
                    //prev
                    element(by.xpath(thisObj.PreviousButtonLocator)).click();
                }
                count++;
                if (count <= thisObj.MaxIteration) {
                    thisObj.navigateMonths(date, count, callBack);
                }
                else {
                    expect("Max Iteration Limit Reached").toEqual(" ");
                }
            }
        })
    };

    this.getCurrentMonthAndYearFromCalendar = function (callBack) {
        element(by.xpath(this.CurrentMonthLocator)).getText().then(callBack);
    };

    this.getMonthName = function (date) {
        return thisObj.monthNames[date.getMonth()];
    };

};
module.exports = CpCalendarHelper;