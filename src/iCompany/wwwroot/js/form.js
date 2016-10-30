function validateForm(options) {
    if (!options) {
        options = {};
    }

    if (options.controlId == undefined) {
        options.controlId = "#form";
    }

    return $(options.controlId).form("validate");
}
