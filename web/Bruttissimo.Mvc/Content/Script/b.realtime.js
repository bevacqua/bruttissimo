; (function ($, b, window) {
	b.realtime = (function() {
		function load() {
			b.load({
				url: "/signalr/hubs"
			});
			$.connection.hub.start().done(initializeLogHub);
		}

		function initializeLogHub() {
			var logs = $.connection.logs;

			$.extend(logs, {
				testBcast: function(message) {
					console.log(message);
				}
			});
		}

		return {
			load: load
		};
	})();
})(jQuery, bruttijjimo, window);