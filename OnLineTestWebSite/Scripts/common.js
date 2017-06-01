//定义trim方法
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

//get方法
function get(id) {
    return document.getElementById(id);
}
//取得XMLHttpRequest对象是AJAX的要点
//此getXMLRequest()方法是根据不同浏览器来取得XMLHttpRequest对象
function getXMLRequest() {
    var request;
    try {
        //for火狐等浏览器
        request = new XMLHttpRequest();
    } catch (e) {
        try {
            //for IE
            request = new ActiveXObject("Microsoft.XMLHttp");
        } catch (e) {
            alert("您的浏览器不支持AJAX!!!");
            return null;
        }
    }
    return request;
}

function AjaxHandlerFunctionByGet(handlerPage) {
    var xmLrequest = getXMLRequest(); //得到XMLHttpRequest对象
    //添加参数,以求每次访问不同的url,以避免缓存问题
    xmLrequest.open("GET", handlerPage + Math.random(), true);
    xmLrequest.onreadystatechange = function () {
        if (xmLrequest.readyState == 4 && xmLrequest.status == 200) {
            //XMLrequest.responseBody返回某一格式的服务器响应数据
            //XMLrequest.responseStream以Ado Stream对象的形式返回响应信
            //XMLrequest.responseText将响应信息作为字符串返回 变量，此属性只读，将响应信息作为字符串返回。
        }
    };
    //将请求发送出去,不需要参数
    xmLrequest.send(null);
}

function AjaxHandlerFunctionByPost(handlerPage, data) {
    var xmLrequest = getXMLRequest(); //得到XMLHttpRequest对象
    //不用担心缓存问题
    xmLrequest.open("post", handlerPage, true);
    //必须设置,否则服务器端收不到参数
    xmLrequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmLrequest.onreadystatechange = function () {
        if (xmLrequest.readyState == 4 && xmLrequest.status == 200) {
            //XMLrequest.responseBody返回某一格式的服务器响应数据
            //XMLrequest.responseStream以Ado Stream对象的形式返回响应信
            //XMLrequest.responseText将响应信息作为字符串返回 变量，此属性只读，将响应信息作为字符串返回。
        }
    };
    //发送请求,要data数据
    xmLrequest.send(data);
}


//将序列化后的时间格式重新转为时间对象
function JsonToDate(json) {
    var re = /-?\d+/;
    var m = re.exec(json);
    return new Date(parseInt(m[0]));
}

// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function FreshStaytime(logtime, obj) {
    var now = new Date();
    var staytime = new Date(now - logtime).Format("hh:mm:ss");
    obj.html(staytime);
}

//将序列化后的时间格式重新转为时间对象
function JsonToDate(json) {
    var re = /-?\d+/;
    var m = re.exec(json);
    return new Date(parseInt(m[0]));
}

// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//格式化试题的参考答案
function InitQuestionAnswer(question) {
    var result = null;
    if (question.CorrectAnswer == 1 || question.CorrectAnswer == 2 || question.CorrectAnswer == 3 || question.CorrectAnswer == 4) {
        switch (question.CorrectAnswer) {
            case 1: result = "A、" + question.AnswerA; return result;
            case 2: result = "B、" + question.AnswerB; return result;
            case 3: result = "C、" + question.AnswerC; return result;
            case 4: result = "D、" + question.AnswerD; return result;
            default: result = "服务器出差了"; return result;
        }
    }
}


