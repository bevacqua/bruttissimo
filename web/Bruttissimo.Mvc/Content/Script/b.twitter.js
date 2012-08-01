; (function ($, b, l, window) {
	b.twitter = (function () {
		var api = {
			script: {
				id: "tw-connect",
				src: "http://platform.twitter.com/anywhere.js?id={0}&v=1"
			},
			status: "disabled",
			loading: null,
			timeout: 10000,
			params: {
				scope: "email"
			},
			waiting: false,
			afterLoad: []
		};
		
		var callback_params = {
			source: "?source=twitter",
			userId: "&userId={0}",
			returnUrl: "&returnUrl={0}",
			displayName: "&displayName={0}"
		};

		function asyncInit(appId) {
			if(api.status === "disabled" || api.status === "error") {
				api.status = "loading";
				api.loading = new Date();
			
				var body = $("body");
				var script = $("<script id='{0}' src='{1}'><\/script>".format(api.script.id, api.script.src.format(appId)));
				body.append(script);

				var interval = setInterval(function() {
					var tw = window.twttr;
					if (tw) {
						clearInterval(interval);
						api.status = "loaded";
					} else if(hasTimedOut()) {
						clearInterval(interval);
						$("#{0}".format(api.script.id)).remove();
						api.status = "error";
						api.loading = null;
					}
				}, 50);
			}
		}
		
		function hasTimedOut() {
			var now = new Date();
			if(api.loading + api.timeout < now) {
				return true;
			}
			return false;
		}

		function loginWithTwitter(opts) {
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
		}

		return {
			load: asyncInit,
			login: loginWithTwitter
        };
    })();
})(jQuery, bruttijjimo, localization, window);