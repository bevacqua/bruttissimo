; (function($, b, window) {
	b.realtime = (function() {
		var api = {
			hubs: void 0
		};

		function listen(hubs) {
			api.hubs = hubs;

			enqueue(function() {
				$.connection.hub.start().done(initializeLogHub);
			});
		}

		function enqueue(callback) {
			b.load({
				url: api.hubs,
				callback: callback
			});
		}

		function initializeLogHub() {
			var logs = $.connection.logs;

			$.extend(logs, {
				testBcast: function(cid, message) {
					// why U no come back??
					console.log(message);
					alert(cid);
				}
			});

			logs.testMessage("blobloblop!");
		}

		return {
			listen: listen
		};
	})();
})(jQuery, bruttijjimo, window);