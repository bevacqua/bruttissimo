(function ($, b, l) {
    b.views.post = b.views.post || {};
    b.views.post.create = function (settings) {
        var form = $("form.post-creation");
        var linkInput = $("input.post-link");
        var messageInput = $("input.post-user-message");
        var previewContainer = $("article.post-preview");

        b.ajaxify(form);

        function commentOnPost(id, message) {
            $.ajax({
                url: settings.commentUrl.format(id),
                data: {
                    message: message
                }
            });
        }

        function validatePreview(input) {
            if (!form.valid()) { // no link was provided, or the provided link is missing its protocol.
                return false;
            }
            if (!previewContainer.is(":visible")) { // reduce overhead for small screen devices.
                previewContainer.empty();
                return false;
            }
            var last = form.data("preview-xhr") || {};
            if (last.model && last.model.input === input) { // avoid duplicates.
                return false;
            }
            if (last.xhr && last.xhr.readyState !== 4) { // abort request in progress.
                last.xhr.abort();
            }
            if (b.ajax.disabled(form)) { // sanity.
                return false;
            }
            var previews = form.data("previews") || [];
            if (input in previews) {
                b.ajax.success(previews[input], previewContainer); // preview from cache.
                return false;
            }
            return true; // valid, can perform request.
        }

        function previewSuccess(result, model) {
            if (result.faulted === "used") {
                var comment = $.trim(messageInput.val());
                var message;
                var buttons = [{ caption: l.Post.ViewPost, href: result.link}];

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

        function previewLink() { // ajax-only functionality to preview a link.
            var input = $.trim(linkInput.val());

            if (!validatePreview(input)) {
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
            ajaxOptions = b.ajax.disables(form, ajaxOptions);
            var last = {
                model: model,
                xhr: $.ajax(ajaxOptions)
            };
            form.data("preview-xhr", last);
        }

        linkInput.on("paste.preview, change.preview", function () {
            setTimeout(function () { previewLink(); }, 0); // setTimeout is required to get the correct value after paste.
        });
    };
})(jQuery, bruttijjimo, localization);