; (function ($, l) {
    $(function() {
        function convertValidationMessagesToTooltips(form, isRenderTimeValidation) {
            var fields = $("fieldset .field-validation-valid, fieldset .field-validation-error", form);
            fields.each(function() {
                var self = $(this);

                if (!!isRenderTimeValidation) {
                    self.addClass("model-validation");
                } else {
                    self.removeClass("model-validation");
                }

                self.addClass("tooltip-icon");
                self.attr("rel", "tooltip");
                if (self.hasClass("field-validation-error")) {
                    self.attr("title", self.text());
                } else {
                    self.attr("title", l.Common.Valid);
                }
                var span = self.find("span");
                if (span.length) {
                    span.text("");
                } else {
                    self.text("");
                }
                self.tooltip();
            });
        }

        $("form").each(function() {
            var form = $(this);
            var settings = form.data("validator").settings;

            // update error message:
            var old_errorPlacement = settings.errorPlacement;
            var new_errorPlacement = function() {
                (old_errorPlacement || $.noop).apply(this, arguments);
                convertValidationMessagesToTooltips(form);
            };
            settings.errorPlacement = new_errorPlacement;

            // update success message:
            var old_success = settings.success;
            var new_success = function() {
                (old_success || $.noop).apply(this, arguments);
                convertValidationMessagesToTooltips(form);
            };
            settings.success = new_success;

            convertValidationMessagesToTooltips(form, true); // initialize in case of model-drawn validation messages at page render time.
        });
    });
})(jQuery, localization);