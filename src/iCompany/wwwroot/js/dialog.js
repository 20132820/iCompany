function openDialog(options) {
    showLoading();

    if (!options) {
        options = {};
    }

    if (options.dialog == undefined) {
        options.dialog = "#dlg";
    }

    $.ajax({
        type: 'POST',
        url: options.url,
        data: options.data,
        success: function (html) {
            hideLoading();
            var div = $(options.dialog);
            var parent = div.parent();
            div.html(html);
            div.dialog({
                title: options.title,
                width: options.width,
                height: options.height,
                onClose: function () {
                    div.dialog('destroy');
                    parent.append('<div id="' + options.dialog.replace('#', '') + '"></div>');
                    options.onClose;
                },
                modal: true,
                closable: options.closable

            });
            if (options.success) {
                options.success();
            }
        },
        error: handleError
    });
}

function closeDialog(options) {
    hideLoading();
    if (!options) {
        options = {};
    }

    if (options.dialog == undefined) {
        options.dialog = "#dlg";
    }

    $(options.dialog).dialog("close");
}

function closeAndReload(options) {
    closeDialog(options);
    reload(options);
}

function reload(options) {
    hideLoading();

    if (!options) {
        options = {};
    }

    if (options.datagrid == undefined) {
        options.datagrid = "#dg";
    }

    var dg = $(options.datagrid);
    dg.datagrid("clearChecked");
    dg.datagrid("reload");
}
