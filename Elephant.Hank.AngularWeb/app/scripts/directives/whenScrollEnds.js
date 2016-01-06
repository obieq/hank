/**
 * Created by vyom.sharma on 31-08-2015.
 */

'use strict';
app.directive('whenScrollEnds', function () {
  return {
    restrict: "A",
    link: function (scope, element, attrs) {
      var processingScroll = false;

      var visibleHeight = element.height();
      var threshold = 100;

      element.scroll(function () {
        var scrollableHeight = element.prop('scrollHeight');
        var hiddenContentHeight = scrollableHeight - visibleHeight;

        if (hiddenContentHeight - element.scrollTop() <= threshold) {
          // Scroll is almost at the bottom. Loading more rows
          scope.$apply(attrs.whenScrollEnds);
        }
      });
    }
  };
});
