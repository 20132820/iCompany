var menuUrl;
var currentPath;

function showMenuTop() {
    $('#menuTop').tabs({
        border: false,
        width: 'auto',
        onSelect: function (title) {
            var tab = $('#menuTop').tabs('getSelected');
            showMenuLeft(tab.panel('options').id);
        }
    });

    $.ajax({
        type: 'POST',
        url: menuUrl,
        data: {
            deep: 2,
            currentPath: currentPath
        },
        success: function (menu) {
            $.each(menu, function (i, item) {
                $('#menuTop').tabs('add', {
                    id: item.url,
                    title: item.title,
                    closable: false
                });
            });
        },
        error: handleError
    });
}

function showMenuLeft(startPath) {
    $.ajax({
        type: 'POST',
        url: menuUrl,
        data: {
            startPath: startPath,
            currentPath: currentPath
        },
        success: function (menu) {
            var top = $('#menuAccordion');
            $.each(menu, function (i, item) {
                var first = $("<div></div>");
                first.attr('title', item.title);
                if (item.selected) {
                    first.attr('data-options', 'selected:true');
                }
                $.each(item.childNodes, function (j, itemChild) {
                    var second = $("<a />");
                    if (itemChild.selected) {
                        second.attr('class', 'bodySelected');
                    }
                    else {
                        second.attr('class', 'body');
                    }
                    second.attr('onclick', "showContentPage('" + itemChild.url + "')");
                    second.attr('title', itemChild.description);
                    second.text(itemChild.title);
                    first.append(second);
                });
                top.append(first);
            });
            top.accordion({ fit: true });
        },
        error: handleError
    });
}

function showContentPage(url) {
    showLoading();
    $.ajax({
        type: 'GET',
        url: url,
        success: function (content) {
            $('#contentPage').html(content);
            $.parser.parse();
            hideLoading();
        },
        error: handleError
    });
}