(function ($, b, window) {
	b.twitter = function() {
		var api = {
			src: "http://platform.twitter.com/anywhere.js?id={0}&v=1",
			id: void 0
		};

		var callback_params = {
			source: "?source=twitter",
			userId: "&userId={0}",
			returnUrl: "&returnUrl={0}",
			displayName: "&displayName={0}"
		};

		function load(appId) {
			api.id = appId;
			enqueue();
		}

		function enqueue(callback) {
			b.load({
				url: api.src.format(api.id),
				callback: callback
			});
		}

		function loginWithTwitter(opts) {
			enqueue(function() {
				var twitter = window.twttr;
				twitter.anywhere(function(t) {
					t.bind("authComplete", function(e, user) {
						var callback = opts.callbackUrl + callback_params.source;
						callback += callback_params.userId.format(encodeURI(user.id));
						callback += callback_params.displayName.format(encodeURI(user.name));
						if (opts.returnUrl) {
							callback += callback_params.returnUrl.format(encodeURI(opts.returnUrl));
						}
						window.location = callback;
					});
					t.signIn();
				});
			});
		}

		return {
			load: load,
			login: loginWithTwitter
		};
	};
})(jQuery, bruttijjimo, window);