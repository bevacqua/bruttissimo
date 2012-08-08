; (function ($, b, l, window) {
	b.fb = (function () {
		var api = {
			script: {
				id: "fb-connect",
				src: "https://connect.facebook.net/en_US/all.js"
			},
			appId: void 0,
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
			source: "?source=facebook",
			userId: "&userId={0}",
			returnUrl: "&returnUrl={0}",
			accessToken: "&accessToken={0}"
		};

		function asyncInit(appId, callback) {
			if (!!appId) {
				api.appId = appId;
			}
			if (!api.appId) {
				return;
			}
			b.load({
				url: api.script.src,
				id: api.script.id,
				condition: function() {
					var fb = window.FB;
					return fb && fb.init;
				},
				beforeLoad: function() {
					var body = $("body");
					var root = $("<div id='fb-root'></div>");
					body.append(root);
				},
				onError: function() {
					$("#fb-root").remove();
				},
				onSuccess: function(result) {
					var fb = window.FB;
					fb.init({
						appId: api.appId,
						status: false,
						cookie: false,
						xfbml: false,
						oauth: true
					});
					result.status = "initializing";
					fb.getLoginStatus(function() {
						result.defaultSuccess();
					});
				},
				callback: callback
			});
		}

		function loginWithFacebook(opts) {
			var logon = function() {
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
			};
			asyncInit(null, logon);
		}

    	return {
			load: asyncInit,
			login: loginWithFacebook
        };
    })();
})(jQuery, bruttijjimo, localization, window);