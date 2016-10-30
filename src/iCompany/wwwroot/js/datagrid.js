function datagrid(options) {
    $(options.control).datagrid({
        url: options.url,
        method: 'post',
        fit: true,
        fitColumns: true,
        pagination: true,
        pageSize: 10,
        pageList: [10, 20],
        rownumbers: true,
        idField: options.idField,
        queryParams: options.queryParams,
        sortName: options.sortName,
        sortOrder: options.sortOrder,
        toolbar: options.toolbar,
        border: 0,
        singleSelect: options.singleSelect,
        checkOnSelect: options.checkOnSelect,
        selectOnCheck: options.selectOnCheck,
        onDblClickRow: options.onDblClickRow,
        onLoadError: handleError,
        onSelect: options.onSelect,
        onLoadSuccess: options.onLoadSuccess,
        onCheck: function (index, row) {
            changeSingleSelectButton(options);
        },
        onUncheck: function (index, row) {
            changeSingleSelectButton(options);
        },
        onCheckAll: function (index, row) {
            changeSingleSelectButton(options);
        },
        onUncheckAll: function (index, row) {
            changeSingleSelectButton(options);
        }
    });
}

function changeSingleSelectButton(options) {
    var selections = $(options.control).datagrid('getChecked');
    if (options.singleSelectOnlyButton === undefined) {
        options.singleSelectOnlyButton = [];
    }
    $.each(options.singleSelectOnlyButton, function (i, n) {
        if (selections.length > 1) {
            $(n).linkbutton('disable');
        }
        else {
            $(n).linkbutton('enable');
        }
    });
}

function getSelectedId(options) {
    if (!options) {
        options = {};
    }

    if (options.datagrid == undefined) {
        options.datagrid = "#dg";
    }

    var selected = $(options.datagrid).datagrid('getSelected');
    if (!selected) {
        $.messager.alert('提示', '没有选择记录', 'info');
    }
    return selected;
}