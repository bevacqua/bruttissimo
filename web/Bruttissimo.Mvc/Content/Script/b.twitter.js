; (function ($, b, l, window) {
	b.twitter = (function () {
		var api = {
			script: {
				id: "tw-connect",
				src: "http://platform.twitter.com/anywhere.js?id={0}&v=1"
			},
			appId: void 0
		};
		
		var callback_params = {
			source: "?source=twitter",
			userId: "&userId={0}",
			returnUrl: "&returnUrl={0}",
			displayName: "&displayName={0}"
		};

		function asyncInit(appId, callback) {
			if (!!appId) {
				api.appId = appId;
			}
			if (!api.appId) {
				return;
			}
			b.load({
				url: api.script.src.format(api.appId),
				id: api.script.id,
				condition: function() {
					return window.twttr;
				},
				callback: callback
			});
		}

		function loginWithTwitter(opts) {
			asyncInit(null, function() {
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
			load: asyncInit,
			login: loginWithTwitter
        };
    })();
})(jQuery, bruttijjimo, localization, window);