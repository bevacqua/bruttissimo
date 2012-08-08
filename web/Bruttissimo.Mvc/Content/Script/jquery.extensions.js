; (function ($) {
    $.fn.fadeOutAndRemove = function () {
        return this.each(function () {
            var self = $(this);
            self.fadeOut("fast", function() { 
                self.remove(); 
            });
        });
    };

    /* the element requires the following CSS in order to be properly centered using this method.
        left: 50%;
        top: 50%;
        position: absolute;
     */
    $.fn.center = function (){
        return this.each(function () {
            var self = $(this);
            self.css("marginLeft", -self.width() / 2);
            self.css("marginTop", -self.height() / 2);
        });
    };

    $.fn.enable = function(b) {
	    return this.each(function() {
		    this.disabled = !b;
	    });
    };

	$.script = function(url, opts) {
		var options = $.extend(opts || { }, {
			dataType: "script",
			url: url			
		});
		return $.ajax(options);
	};
})(jQuery);