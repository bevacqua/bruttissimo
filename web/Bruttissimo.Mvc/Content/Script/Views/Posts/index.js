; (function ($, b, localization) {
    b.views.post = b.views.post || {};
	b.views.post.index = function() {
		$(function () {
			var more = $("section.more-posts:last");
			var anchor = more.find("a.more-posts-link");
			b.ajaxify({
				element: anchor,
				viewResultContainer: more
			});
		});
	};
})(jQuery, bruttijjimo, localization);