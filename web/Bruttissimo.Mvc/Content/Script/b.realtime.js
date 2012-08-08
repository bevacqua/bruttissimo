﻿; (function($, b, window) {
	$(function() {
		b.realtime = (function() {
			var api = {
				url: void 0
			};
			
			function listen(url) {
				api.url = url;

				enqueue(function() {
					extendHubs();

					$.connection.hub.start();
				});
			}

			function enqueue(callback) {
				b.load({
					url: api.url,
					callback: callback
				});
			}

			function extendHubs () {
				var logs = $.connection.logs;

				$.extend(logs, {
					update: function(entry) {
						var syslogs = $("#syslogs tbody");
						var build = b.tag;
						var row = build("tr").hide();
						build("td").appendTo(row).text(entry.date);
						build("td").appendTo(row).text(entry.level);
						build("td").appendTo(row).text(entry.message);
						row.prependTo(syslogs).flash("#b1f7ed").slideDown("fast");
						$("tr", syslogs).slice(9).slideUp();
					}
				});
			}

			return {
				listen: listen
			};
		})();
	});
})(jQuery, bruttijjimo, window);