var QuestionId = null;
var content;//定义展示区域
$(document).ready(function () {
    content = $("#content");//初始化展示区
    if (QuestionId != null) {
        Uajax({ QuestionId: QuestionId }, "../ashx/getUploadQuestionByQuestionId.ashx", callback_success, callback_error, content);
    }
});

//----------------------------自定义函数---------------------------------------------------
function callback_success(data, obj) {
    var remark = $("<div id='remark' class='row'>审核中和审核通过的试题不能进行修改操作</div>");
    obj.append(remark, CreateQuestionDiv(data));
    $(".row").not("#remark").each(function () {
        $(this).on(
            {
                "mouseover": function () { $(this).children(".content").css({ "background-color": "#ff9924" }); },
                "mouseout": function () { $(this).children(".content").css({ "background-color": "#EEE" }); }
            }
            )
    });
}

function callback_error(obj) {//异步失败
    addlab(obj);
}
function addlab(obj) {
    var lab = $("<div id='nodata' class='nodata'>获取试题数据失败。</div>");
    obj.append(lab);
}
