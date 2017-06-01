var tabs, contents;//tab标签和内容项
var AsynchronousFinished;//异步操作是否结束的标记
var showtabid;//当前显示项的标记
var to, subimt, inputContent, inputTo;//联系人框、发送按钮、发送内容框和联系人输入input
var model;
$(document).ready(function () {
    tabs = $(".tab");//标签组
    contents = $(".content");//内容项组
    to = $("#To");//收件人框
    inputTo = $("#inputTo");//收件人输入框
    inputContent = $("#inputContent");//发送内容框
    subimt = $("#submit");//发送按钮
    AsynchronousFinished = true;//异步执行是否完成的标记
    tabs.each(function (index) { $(this).on("mouseover", { index: index }, TabMouseover) });//tab标签绑定事件
    lastcontact();//初始化最近联系人
    subimt.on("click", { messageto: inputTo, messagecontent: inputContent }, createmessage);//发送站内信按钮绑定事件
    TabMouseover(0);//初始化一个tab标签被点击事件
    showtabid = 0;//定义当前显示项  
});

//====================================自定义函数区============================================
//获取最近联系人
function lastcontact() {
    Uajax({}, "../ashx/getLastContact.ashx", getlastcontact_success, getlastcontact_error, $("#lastcontact"))
}

//获取最近联系人成功
function getlastcontact_success(data, obj) {
    obj.empty();
    for (var i = 0; i < data.length; i++) {
        obj.append(Div_contact(data[i]));
    }
}

//生成一个联系人的DIV
function Div_contact(contact) {
    var div = $("<div class='lastcontact_contact' id='lastcontact_id_" + contact.id + "'>" + contact.name + "</div>");
    div.on("click", { handler: $(this) }, lastcontact_click);
    return div;
}

//最近联系人点击事件
function lastcontact_click(handler) {
    var target = $(handler.target);
    $("#inputTo").val($("#inputTo").val() + target.html() + ";  ");
    target.remove();
}

//获取最近联系人失败
function getlastcontact_error(data, obj) {
}

//点击发送按钮时的方法
function createmessage(e) {
    var param = e.data;
    var messageto = param.messageto.val();
    var messagecontent = param.messagecontent.val();
    if (model == "create" && AsynchronousFinished == true && messageto != null && messagecontent != null && messagecontent != "") {
        AsynchronousFinished = false;
        Uajax({ model: "create", messageto: messageto, messagecontent: messagecontent }, "../ashx/getMessage_table.ashx", createmessage_success, createmessage_error, contents[2])
    } else if (AsynchronousFinished == false) {
        alert("服务器正从火星回地球路上……");
    } else if (messagecontent == "") {
        alert("内容不能为空。");
    } else {
        alert("服务器看你不爽，不想为你服务。");
    }
}
//异步新建站内信成功
function createmessage_success(data, obj) {
    if (data.length > 0) {
        alert("站内信发送成功。");
    } else {
        alert("没有发送给任何人。");
    }
    AsynchronousFinished = true;
    lastcontact();
    inputTo.val("发件人： ");
    inputContent.val("");
}
//异步新建站内信失败
function createmessage_error(data, obj) {
    console.log(data);
    AsynchronousFinished = true;
    alert("站内信发送失败。");
    lastcontact();
    inputTo.val("发件人： ");
    inputContent.val("");
}
//标签被点击时的方法,其人发件箱和收件箱直接初始化
function TabMouseover(index) {
    var i = typeof (index.data) == "undefined" ? index : index.data.index;//定义标签序号
    if (i != showtabid) {//如果点击的就是当前显示项，则不执行操作
        showtabid = i;
        tabs.removeClass("tabactive");
        $(tabs[i]).addClass("tabactive");
        contents.removeClass("contentactive");
        $(contents[i]).addClass("contentactive");
        pagenum = 1;//页码值,注意从第1页开始，后台处理了第1页，即 第0-10条记录
        //定义model的方式，用于后台识别
        switch (i) {
            case 0: model = "sended"; break;
            case 1: model = "received"; break;
            case 2: model = "create"; break;
            default: model = null; break;
        }
        //用于异步调用
        if ((model == "sended" || model == "received") && AsynchronousFinished == true) {
            AsynchronousFinished = false;
            Uajax({ model: model, pagenum: pagenum }, "../ashx/getMessage_table.ashx", getMessage_table_success, getMessage_table_error, contents[i])
        }
    }
}
//异步取数据成功
function getMessage_table_success(data, obj) {
    if (data.length > 0) {//有多条记录
        for (var i = 0; i < data.length; i++) {
            if (($("#message" + data[i].MessageId)).length <= 0) {
                $(obj).append(CreateMessageItem(data[i]));
            }
        }
    } else {//没有记录
        alert("没有记录");
    }

    AsynchronousFinished = true;

}
//异步取数据失败
function getMessage_table_error(data, obj) {
    AsynchronousFinished = true;
}
//生成一条Message的DIV
function CreateMessageItem(message) {
    var item;
    if (message.MessageIsRead == false) {
        item = $("<div id='message" + message.MessageId + "'  class='item'></div>");
    } else {
        item = $("<div id='message" + message.MessageId + "'  class='item isRead'></div>");
    }
    var chkbox = "<div class='Chkbox'> <input type='checkbox' /></div>";
    var messageFromORTo = "<div class='MessageFromORTo'>" + message.UserName + "</div>";
    var content = "<div class='MessageContent'>" + message.MessageContent.substring(0, 35) + "</div>";
    var sendtime = "<div class='Sendtime'>" + JsonToDate(message.MessageSendTime).Format("yyyy-MM-dd") + "</div>";
    item.append(chkbox, messageFromORTo, content, sendtime);
    item.click(function () { alert(message.MessageContent) });
    item.mouseover(function () { $(this).css({ "background-color": "#FF9924", "color": "white" }) }).mouseout(function () { $(this).css({ "background-color": "white", "color": "black" }) });
    return item;
}

