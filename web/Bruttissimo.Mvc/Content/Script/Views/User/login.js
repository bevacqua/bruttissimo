; (function ($, b, l, window) {
    b.openid = (function () {
    	var providers = {
    		google: {
				source: "openid",
				css: "google-icon",
    			name: "Google",
    			url: "https://www.google.com/accounts/o8/id"
    		},
    		facebook: {
				source: "facebook",
				css: "fb-icon",
    			name: "Facebook"
    		},
    		twitter: {
				source: "twitter",
				css: "twitter-icon",
    			name: "Twitter"
    		},
    		yahoo: {
				source: "openid",
				css: "yahoo-icon",
    			name: "Yahoo",
    			url: "http://yahoo.com/"
    		}
    	};

    	var opts = {
			facebookAppId: void 0,
			twitterAppId: void 0,
			callbackUrl: void 0,
			returnUrl: void 0
		};

		var image_title = l.Authentication.LogonTitle;
		var button_class = "logon-provider-button";
		var selected_class = "logon-provider-selected";

		var preferenceKey = "openid";

		var hidden = {
			source: $(".provider-source"),
			openid: $(".provider-openIdProvider"),
			returnUrl: $(".provider-returnUrl")
		};

    	var container = $('.provider-buttons');
		var form = $(".provider-logon");

		function init(options) {
			$.extend(opts, options);

			b.fb.load(opts.facebookAppId);
			b.twitter.load(opts.twitterAppId);

			form.removeClass("hidden"); // initially hidden for noscript accessibility.
			
			var provider;
			var button;
			for (provider in providers) { // add buttons for each provider to the DOM
				button = getButton(provider, providers[provider]);
				container.append(button);
			}
			var id = b.getPreference(preferenceKey);
			if (id) {
				login(id, true);
			}
        }

		function getButton (id, provider) {
			var button = b.tag("a", button_class)
				.attr("title", image_title.format(provider.name))
				.addClass("provider-icon")
				.addClass(provider.css)
				.data("provider", id)
				.click(function() {
					login(id, false);
				});
			return button;
		}

		function login(id, isInitialization) {
			var provider = providers[id];
			if (!provider) {
				return;
			}
			highlight(id);
			setProviderInfo(provider);
			if (!isInitialization) {
				if (id === "facebook") {
					b.fb.login({
						callbackUrl: opts.callbackUrl,
						returnUrl: opts.returnUrl
					});
				} else if (id === "twitter") {
					b.twitter.login({
						callbackUrl: opts.callbackUrl,
						returnUrl: opts.returnUrl
					});
				} else {
					b.setPreference(preferenceKey, id);
					form.submit();
				}
			}
		}
		
		function setProviderInfo(provider) {
			hidden.source.val(provider.source);
			hidden.openid.val(provider.url);
			hidden.returnUrl.val(opts.returnUrl);
		}

		function highlight(id) {
			var buttons = $(".{0}".format(button_class));
			buttons.removeClass(selected_class);
			buttons.filter(function() {
				return $(this).data("provider") == id;
			}).addClass(selected_class);
		}

    	return {
            init: init
        };
    })();
})(jQuery, bruttijjimo, localization, window);