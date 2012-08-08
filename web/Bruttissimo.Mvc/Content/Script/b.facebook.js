; (function($, b, l, window) {
	b.fb = (function() {
		var api = {
			src: "https://connect.facebook.net/en_US/all.js",
			id: void 0,
			params: {
				scope: "email"
			}
		};

		var callback_params = {
			source: "?source=facebook",
			userId: "&userId={0}",
			returnUrl: "&returnUrl={0}",
			accessToken: "&accessToken={0}"
		};

		function load(appId) {
			api.id = appId;
			enqueue();
		}

		function enqueue(callback) {
			b.load({
				url: api.src,
				before: function() {
					var body = $("body");
					var root = $("<div id='fb-root'></div>");
					body.append(root);
				},
				success: function(result) {
					var fb = window.FB;
					fb.init({
						appId: api.id,
						status: false,
						cookie: false,
						xfbml: false,
						oauth: true
					});
					fb.getLoginStatus(function() {
						result.complete(result); // mark the result as completed, running any pending callbacks.
					});
				},
				error: function() {
					$("#fb-root").remove();
				},
				callback: callback
			});
		}

		function loginWithFacebook(opts) {
			enqueue(function() {
				window.FB.login(function(response) {
					if (response.authResponse && response.status === "connected") {
						var userId = encodeURI(response.authResponse.userID);
						var accessToken = encodeURI(response.authResponse.accessToken);
						var callback = opts.callbackUrl;
						callback += callback_params.source;
						callback += callback_params.userId.format(userId);
						callback += callback_params.accessToken.format(accessToken);
						if (opts.returnUrl) {
							callback += callback_params.returnUrl.format(encodeURI(opts.returnUrl));
						}
						window.location = callback;
					}
				}, api.params);
			});
		}

		return {
			load: load,
			login: loginWithFacebook
		};
	})();
})(jQuery, bruttijjimo, localization, window);