; (function ($, b) {
	b.container = "#layout-content";

	$.ajaxSetup({
		type: "POST",
		success: function(result) { // default ajax success, if it's overriden, b.ajax.success should still be invoked.
			b.ajax.success(result); // prevent jQuery from passing an invalid viewResultContainer.
		}
	});

	$("#menu-icon").click(function(e) {
		b.scrollTo($("#menu"));
        e.preventDefault();
        return false;
	});

	$("li.nav-top a").click(function(e) {
		b.scrollTo($("#top"));
        e.preventDefault();
        return false;
	});
})(jQuery, bruttijjimo);