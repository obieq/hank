/**
 * Created by gurpreet.singh on 04/22/2015.
 */

'use strict';
app.factory('CommonUiService', ['ngAppSettings', function (ngAppSettings) {
  return {
    showMessagePopup : function(msgObj, title) {
      this.showErrorPopup(msgObj, title);
    },
    showErrorPopup : function(msgObj, title) {
      var message = "Error: Not able to complete your request!";

      if(typeof(msgObj) == 'string')
      {
        message = msgObj;
      }
      else if(msgObj != undefined && msgObj.data && msgObj.data.IsError)
      {
        title = title == undefined ? "Error" : title;

        message = "";
        for(var i = 0; i < msgObj.data.Messages.length; i++)
        {
          var msg = msgObj.data.Messages[i];
          message += ((msg.Name + "").length > 0 ? msg.Name + ": " : "") + msg.Value + "<br />"
        }
      }
      else if(msgObj != undefined && msgObj.status == 0)
      {
        message = "Server is not reachable!";
      }
      title = title == undefined ? "Message" : title;

      document.getElementById('messageBoxBody').innerHTML = message;
      document.getElementById('messageBoxTitle').innerHTML = title;
      $('#messageBox').modal('show');
    }
  };
}]);

