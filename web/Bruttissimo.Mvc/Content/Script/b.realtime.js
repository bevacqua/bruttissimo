; (function ($, b, window) {
	b.realtime = (function() {
		var posts = $.connection.posts;

		$.extend(posts, {
			postCreated: function(cid, post) {
				if ($.connection.hub.id === cid) {
					return;
				}
				console.log(post);
			}
		});
		
		function load() {
			$.connection.hub.start().done(initializePostHub);
		}

		function initializePostHub() {
		}

		return {
			load: load
		};
	})();
})(jQuery, bruttijjimo, window);