(function ($, b, l) {
    b.views.post = b.views.post || {};
    b.views.post.create = function (settings) {
        var form = $("form.post-creation");
        var linkInput = $(".post-link");
        var messageInput = $(".post-user-message");
        var previewContainer = $("article.post-preview");

        b.ajaxify({
            element: form,
            success: function (result) {
                alert("success"); // TODO: ajaxify when the result comes back, we should add the post to the list 
                // TODO (in case the result is the partial or whatever..), this is true for any viewport size
            }
        });

        function commentOnPost(id, comment) {
            $.ajax({
                url: settings.commentUrl,
                data: {
                    comment: comment
                },
                success: function (result) {
                    alert("post-as-comment clicked");
                }
            });
        }

        function previewSuccess(result, model) {
            if (result.faulted === "used") {
                var comment = $.trim(messageInput.val());
                var message;
                var buttons = [{ caption: l.Post.ViewPost, href: result.link}]; // TODO: ajaxify?

                if (!comment) {
                    message = l.Post.DuplicateMessage;
                } else {
                    message = l.Post.DuplicateViewOrComment;
                    buttons.push({
                        caption: l.Post.PostComment,
                        click: function () {
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
                var previews = form.data("previews") || [];
                previews[model.input] = result;
                form.data("previews", previews);
                b.ajax.success(result, previewContainer);
            }
        }

        function preview() {
            var input = linkInput.val();

            if (!form.valid()) { // no link was provided, or the provided link is missing its protocol.
                return;
            }
            if (!previewContainer.is(":visible")) { // reduce overhead for small screen devices.
                previewContainer.empty();
                return;
            }
            var last = form.data("preview-xhr") || {};
            if (last.model && last.model.input === input) { // avoid duplicates.
                return;
            }
            if (last.xhr && last.xhr.readyState !== 4) { // abort request in progress.
                last.xhr.abort();
            }
            if (b.ajax.isDisabled(form)) { // sanity.
                return;
            }
            var previews = form.data("previews") || [];
            if (input in previews) {
                b.ajax.success(previews[input], previewContainer);
                return;
            }
            previewContainer.empty();
            var model = {
                input: input
            };
            var ajaxOptions = {
                url: settings.previewUrl,
                data: model,
                success: function (result) {
                    previewSuccess(result, model);
                }
            };
            ajaxOptions = b.ajax.disableDuringRequests(ajaxOptions, form);
            last = {
                model: model,
                xhr: $.ajax(ajaxOptions)
            };
            form.data("preview-xhr", last);
        }

        // ajax-only functionality to preview a link.
        linkInput.on("paste.preview, change.preview", function () {
            setTimeout(function () { preview(); }, 0); // setTimeout is required to get the correct value after paste.
        });

    };
})(jQuery, bruttijjimo, localization);