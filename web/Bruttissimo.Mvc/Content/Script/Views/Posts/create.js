;
(function ($, b, l) {
	b.views.post = b.views.post || { };
	b.views.post.create = function(settings) {
		$(function() {
			var form = $("form.post-creation");
			var linkInput = $(".post-link");
			var messageInput = $(".post-user-message");
			var previewContainer = $("article.post-preview");

			b.ajaxify({
				element: form,
				success: function(result) {
					alert("success"); // TODO: ajaxify when the result comes back, we should add the post to the list 
					// TODO (in case the result is the partial or whatever..), this is true for any viewport size
				}
			});

			// ajax-only functionality to preview a link.
			linkInput.on("paste.preview, change.preview", function() {
				setTimeout(function() { // required to get input value after paste.
					var linkValue = linkInput.val();

					if (!form.valid()) { // no link was provided, or the provided link is missing its protocol.
						return;
					}
					if (!previewContainer.is(":visible")) { // reduce overhead for small screen devices.
						previewContainer.empty();
						return;
					}
					var last = form.data("preview-xhr") || { };
					if (last.url && last.url === linkValue) { // avoid duplicates.
						return;
					}
					if (last.xhr && last.xhr.readyState !== 4) { // abort request in progress.
						last.xhr.abort();
					}
					if (b.ajax.isDisabled(form)) { // sanity.
						return;
					}
					var previews = form.data("previews") || [];
					var previewSuccess = function(result) {
						if (result.faulted === "used") {
							var comment = $.trim(messageInput.val());
							var message;
							var buttons = [{ caption: l.Post.ViewPost, href: result.link }]; // TODO: ajaxify?

							if (!comment) {
								message = l.Post.DuplicateMessage;
							} else {
								message = l.Post.DuplicateViewOrComment;
								buttons.push({
									caption: l.Post.PostComment,
									click: function() {
										commentOnPost(result.id, comment);
									}
								});
							}
							buttons.push({ close: true });
							b.dialog({
								title: l.Post.DuplicateTitle,
								message: message,
								buttons: buttons,
								maxWidth: 500
							});
						} else if (result.faulted === "invalid") {
							b.notification(l.Post.InvalidTitle, l.Post.InvalidMessage);
						} else {
							previews[linkValue] = result;
							form.data("previews", previews);
							b.ajax.success(result, previewContainer);
						}
					};
					if (linkValue in previews) {
						b.ajax.success(previews[linkValue], previewContainer);
						return;
					}
					previewContainer.empty();
					var ajaxOptions = {
						url: settings.previewUrl,
						data: {
							input: linkValue
						},
						success: previewSuccess
					};
					ajaxOptions = b.ajax.disableDuringRequests(ajaxOptions, form);
					last = {
						url: linkValue,
						xhr: $.ajax(ajaxOptions)
					};
					form.data("preview-xhr", last);
				}, 0);
			});

            function commentOnPost(id, comment) {
                $.ajax({
                    url: settings.commentUrl,
                    data: {
                        comment: comment
                    },
                    success: function(result) {
                        alert("post-as-comment clicked");
                    }
                });
            }
		});
	};
})(jQuery, bruttijjimo, localization);