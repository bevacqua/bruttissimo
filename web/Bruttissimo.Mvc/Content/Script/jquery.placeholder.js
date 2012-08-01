/*
* Simple Placeholder by @marcgg under MIT License
* Report bugs or contribute on Gihub: https://github.com/marcgg/Simple-Placeholder
*/

; (function ($) {
	$.placeholder = {
		placeholderClass: null,

		hidePlaceholder: function () {
			var $this = $(this);
			if ($this.val() == $this.attr('placeholder')) {
				$this.val("").removeClass($.placeholder.placeholderClass);
			}
		},

		showPlaceholder: function () {
			var $this = $(this);
			if ($this.val() == "") {
				$this.val($this.attr('placeholder')).addClass($.placeholder.placeholderClass);
			}
		},

		preventPlaceholderSubmit: function () {
			$(this).find(".placeholder").each(function () {
				var $this = $(this);
				if ($this.val() == $this.attr('placeholder')) {
					$this.val('');
				}
			});
			return true;
		}
	};

	$.fn.placeholder = function (options) {
		if (document.createElement('input').placeholder == undefined) {
			var config = {
				placeholderClass: 'placeholding'
			};

			if (options) $.extend(config, options);
			$.placeholder.placeholderClass = config.placeholderClass;

			this.each(function () {
				var $this = $(this);
				if ($this.is(":password")) { // fallback to a fixed placeholder, it will be masked by the input anyways.
					$this.attr("placeholder", "password");
				}
				$this.focus($.placeholder.hidePlaceholder);
				$this.blur($.placeholder.showPlaceholder);
				if ($this.val() == '') {
					$this.val($this.attr("placeholder"));
					$this.addClass($.placeholder.placeholderClass);
				}
				$this.addClass("placeholder");
				$(this.form).submit($.placeholder.preventPlaceholderSubmit);
			});
		}

		return this;
	};

})(jQuery);