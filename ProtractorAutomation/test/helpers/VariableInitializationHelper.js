/**
 * Created by vyom.sharma on 23-12-2016.
 */


var VariableInitializationHelper = function () {

    var JsonHelper = require('./JsonHelper.js');
    var jsonHelper = new JsonHelper();

    this.setVariables = function (previousTestVariableStateContainer) {
        previousTestVariableStateContainer = this.getUniqueVariable(previousTestVariableStateContainer);
        browser.params.config.variableStateContainer = config.variableStateContainer.concat(previousTestVariableStateContainer);
        browser.params.config.variableContainer = this.getUniqueVariable(browser.params.config.variableContainer.concat(previousTestVariableStateContainer));
    };

    this.getUniqueVariable = function (variables) {
        var uniqueVariableList = [];
        for (var i = 0; i < variables.length; i++) {
            var index = jsonHelper.indexOfVariableByPropertyName(uniqueVariableList, 'Name', variables[i].Name);
            if (index != undefined) {
                uniqueVariableList[index] = variables[i];
            }
            else {
                uniqueVariableList.push(variables[i])
            }
        }
        return uniqueVariableList;
    };

};
module.exports = VariableInitializationHelper;