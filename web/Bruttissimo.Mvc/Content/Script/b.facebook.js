; (function ($, b, l, window) {
	b.fb = (function () {
		var api = {
			script: {
				id: "fb-connect",
				src: "https://connect.facebook.net/en_US/all.js"
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
			source: "?source=facebook",
			userId: "&userId={0}",
			returnUrl: "&returnUrl={0}",
			accessToken: "&accessToken={0}"
		};

		function asyncInit(appId) {
			if(api.status === "disabled" || api.status === "error") {
				if (!$("#fb-root").length) {
					api.status = "loading";
					api.loading = new Date();

					var body = $("body");
					var root = $("<div id='fb-root'></div>");
					var script = $("<script id='{0}' src='{1}'><\/script>".format(api.script.id, api.script.src));
					body.append(root);
					body.append(script);
				}
				var interval = setInterval(function() {
					var fb = window.FB;
					if (fb && fb.init) {
						clearInterval(interval);
						fb.init({
							appId: appId,
							status: false,
							cookie: false,
							xfbml: false,
							oauth: true
						});
						api.status = "initializing";
						fb.getLoginStatus(function(){
							api.status = "loaded";
						});
					} else if(hasTimedOut()) {
						clearInterval(interval);
						$("#fb-root, #{0}".format(api.script.id)).remove();
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

		function afterApiLoad(method, id) { // method used to avoid invoking 
			if(!id) {
				id = "api_afterLoad_{0}".format(api.afterLoad.length); // identifiers in order to prevent unintended spamming of the same method.
			}
			api.afterLoad[id] = method;
			if(!api.waiting) { // avoid multiple loops waiting on the same thing.
				api.waiting = true;
				var interval = setInterval(function() {
					var loaded = api.status === "loaded";
					var timedOut = hasTimedOut();
					if (loaded || timedOut) {
						clearInterval(interval);
						api.waiting = false;
						if (loaded) {
							var i;
							for(i in api.afterLoad) {
								(api.afterLoad[i] || $.noop)();
							}
						} else if (timedOut) {
							b.notification(l.Facebook.ApiFailureTitle, l.Facebook.ApiFailureDescription);
						}
					}
				}, 50);
			}
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
			afterApiLoad(logon, "logon");
		}

    	return {
			load: asyncInit,
			login: loginWithFacebook
        };
    })();
})(jQuery, bruttijjimo, localization, window);