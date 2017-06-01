var Content;//定义显示区域
$(document).ready(function () {
    Content = $("#content");//显示区域
    getQuestionToVeriry(Content);
});

/// 随机产生用于审核的试题
/// 要求：
///     1、试题的IsVerity==false&&VerityTimes<3&&IsDeleted==false;
///     2、产生的试题不是当前用户提交的；
///     3、每个试题每个用户最多只能审核一次；
///     4、试题要求进行实例化
function getQuestionToVeriry(content) {
    content.empty();
    Uajax({}, "../ashx/getQuestionToVerify.ashx", getQuestionToVerify_success, getQuestionToVerify_error, content);
}
function getQuestionToVerify_success(data, obj) {
    var remark = $("<div class='row' id='remark'>船员在线考试系统感谢你 认真审核每一道试题。</div>");//添加标题行
    if (data != null && $(data).length > 0) {
        var btn = $("<div id='subrow' class='row'><div id='pass' class='btnsub'>通过</div><div id='nopass' class='btnsub'>不通过</div><div id='go' class='btnsub'>跳过</div></div>");//添加按钮行
        $(obj).append(remark, CreateQuestionDiv(data), btn);
        //通过 按钮绑定事件
        $("#pass").on({
            "click": UpdataVerify,
            "mouseover": mouseoverEvent,
            "mouseout": mouseoutEvent
        });
        //不通过 按钮绑定事件
        $("#nopass").on({
            "click": UpdataVerify,
            "mouseover": mouseoverEvent,
            "mouseout": mouseoutEvent
        });

    } else {
        var nodata = $("<div id='nodata'>对不起，没有符合要求的数据，请重试。</div>");
        var btn = $("<div id='go'>重试</div>");
        obj.append(remark, nodata, btn);
    }
    //路过 按钮绑定事件
    $("#go").on({
        "click": function () { getQuestionToVeriry(Content) },
        "mouseover": mouseoverEvent,
        "mouseout": mouseoutEvent
    });

}
function getQuestionToVerify_error(obj) {
    alert("获取审核试题失败。");
}
//提交用户审核的结果
function UpdataVerify(Event) {
    var judge = false;//点击的标记，默认为不通过
    var QuestionId = $("#id").text();
    var conf = "确定不通过审核？";
    if (Event.currentTarget.id == "pass") {
        judge = true;
        conf = "确定通过审核？"
    };
    if (confirm(conf)) {
        Uajax({ judge: judge, QuestionId: QuestionId }, "../ashx/UpdataVerify.ashx", UpdataVerify_success, UpdataVerify_error, Content);
    }
}
//提交用户审核结果成功
function UpdataVerify_success(data, obj) {
    if (data == true) {
        alert("提交审核成功。");
    } else {
        alert("提交审核结果失败.....");
    }
    getQuestionToVeriry(Content);
}
//提交用户审核结果失败
function UpdataVerify_error(obj) {
    alert("提交审核结果失败!!!!!!!。");
    getQuestionToVeriry(Content);
}
//鼠标进行事件
function mouseoverEvent() {
    $(this).css({
        "cursor": "pointer",
        "background-color": "#FF9924"
    });
}
//鼠标移出事件
function mouseoutEvent() {
    $(this).css({
        "background-color": "#DDD",
    });
}