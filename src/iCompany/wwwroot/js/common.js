function showLoading() {
    $("#loadingMask").css('display', 'block');
    $("#loadingMaskMsg").css('display', 'block');
}

function hideLoading() {
    $("#loadingMask").css('display', 'none');
    $("#loadingMaskMsg").css('display', 'none');
}

function handleError(data) {
    hideLoading();
    $.messager.show({
        showType: null,
        title: '提示',
        msg: data.responseText,
        width: 600,
        height: 400,
        style: {
            right: '',
            bottom: ''
        },
        timeout: 0
    });
}


var beforeAndAfterValidating = false;
var beforeAndAfterTimeValidating = false;
var regNumber = /[0-9]/;
var regNumberOnly = /^[0-9]*$/;
var regWordSmall = /[a-z]/;
var regWordBig = /[A-Z]/;
var regSymbol = /[!@#$%^]/;
var regLength8 = /.{8,}/;
var regLength10 = /.{10,}/;

$.extend($.fn.validatebox.defaults.rules, {
    equalTo: {
        validator: function (value, param) {
            return $(param[0]).val() == value;
        },
        message: '字段不匹配'
    },
    beforeAndAfter: {
        validator: function (value, param) {
            if (!beforeAndAfterValidating) {
                var smallOne = $(param[0]).datetimebox('getValue');
                var greatOne = $(param[1]).datetimebox('getValue');
                if (smallOne == '' || greatOne == '') {
                    return true;
                }
                beforeAndAfterValidating = true;
                var smaller = parseDatetime(smallOne);
                var greater = parseDatetime(greatOne);
                $(param[0]).datetimebox('validate');
                $(param[1]).datetimebox('validate');
                beforeAndAfterValidating = false;
                return greater >= smaller;
            }
            else {
                return true;
            }
        },
        message: '结束时间应比开始时间晚'
    },
    beforeAndAfterTimespin: {
        validator: function (value, param) {
            if (!beforeAndAfterTimeValidating) {
                var smallOne = $(param[0]).timespinner('getValue');
                var greatOne = $(param[1]).timespinner('getValue');
                if (smallOne == '' || greatOne == '') {
                    return true;
                }
                beforeAndAfterTimeValidating = true;
                $(param[0]).timespinner('validate');
                $(param[1]).timespinner('validate');
                beforeAndAfterTimeValidating = false;
                return greatOne >= smallOne;
            }
            else {
                return true;
            }
        },
        message: '结束时间应比开始时间晚'
    },
    number: {
        validator: function (value) {
            return regNumberOnly.test(value);
        },
        message: '请输入正确的数字'
    },
    passwordWeak: {
        validator: function (value) {
            return true;
        },
        message: '请输入正确的密码'
    },
    passwordMiddle: {
        validator: function (value) {
            var count = 0;
            if (regNumber.test(value)) count++;
            if (regWordSmall.test(value)) count++;
            if (regWordBig.test(value)) count++;
            if (regSymbol.test(value)) count++;
            return count > 1 && regLength8.test(value);
        },
        message: '密码需含大写字母、小写字母、数字和符号任意两种，长度大于8'
    },
    passwordStrong: {
        validator: function (value) {
            var count = 0;
            if (regNumber.test(value)) count++;
            if (regWordSmall.test(value)) count++;
            if (regWordBig.test(value)) count++;
            if (regSymbol.test(value)) count++;
            return count > 2 && regLength10.test(value);
        },
        message: '密码需含大写字母、小写字母、数字和符号任意三种，长度大于10'
    }
})