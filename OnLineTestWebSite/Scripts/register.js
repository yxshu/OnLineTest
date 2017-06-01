var usernameresult = false;//用户名的验证结果
var passwordresult = false;//密码的验证结果
var sec_passwordresult = false;//重复密码的验证结果
var useremailresult = false;//电子邮件的验证结果
var userchinesenameresutl = true;//中文名的验证结果
var telresutl = true;//电话号码的验证结果
var codesresult = false;//验证码的验证结果
var form, btn, txtusername, txtpassword, Stronger1, Stronger2, Stronger3, sec_password, UserEmail, UserChineseName, Tel, Codes, PassWordStrong;//密码强度分为三个等级，包括:数字  字母  和大写
$(document).ready(function () {
    form = $("#form1");//表单
    btn = $("#btnsubmit");//提交按钮所在的DIV
    txtusername = $("#txtusername");//用户名 4-20个字符,不能为空,不能重复
    txtpassword = $("#txtpassword");//密码 4-20个字符,不能为空 MD5加密
    Stronger1 = $("#Stronger1");//强度1 
    Stronger2 = $("#Stronger2");//强度2
    Stronger3 = $("#Stronger3");//强度3
    sec_password = $("#password");//重复密码
    UserEmail = $("#UserEmail");//电子邮件
    UserChineseName = $("#UserChineseName");//中文名
    Tel = $("#Tel");//电话
    Codes = $("#Codes");//验证码
    var page = "ashx/register.ashx";//提交的页面
    init();
    //处理用户名的验证
    var regExp_username = new RegExp("^[a-zA-Z0-9_]{5,20}$"); //用户名5-20个字符、数字和下划线
    txtusername.focus(function () {
        usernameresult = getFocus(this, " * 5-20个字符");
    }).keyup(function () {
        usernameresult = getChange(this, regExp_username, true, "ashx/ValidUser.ashx", " * 5-20个字符");
    }).blur(function () {
        usernameresult = getBlur(this, regExp_username, true, "ashx/ValidUser.ashx", " * 5-20个字符")
    });

    //处理密码的验证
    var passwordresult = false;
    var regExp_password = new RegExp("^[a-zA-Z]\\w{5,17}$"); //以字母开头，长度在6-18之间;
    txtpassword.focus(function () {
        passwordresult = getFocus(this, " * 字母开头的6-18个字符"); PassWordStrongInit();
    }).keyup(function () {
        passwordresult = getChange(this, regExp_password, true, "notUnique", " * 字母开头的6-18个字符"); PassWordStrongSynchronous(this);
    }).blur(function () {
        passwordresult = getBlur(this, regExp_password, true, "notUnique", " * 字母开头的6-18个字符"); PassWordStrongSynchronous(this);
    });

    //处理重复密码的验证

    sec_password.focus(function () {
        sec_passwordresult = getFocus(this, " *")
    }).keyup(function () {
        sec_passwordresult = ValidSec_Password(txtpassword, $(this))
    }).blur(function () {
        sec_passwordresult = ValidSec_Password(txtpassword, $(this))
    });

    //处理电子邮件的验证

    var regExp_email = new RegExp("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{3,5})\\s*$"); //邮箱格式
    UserEmail.focus(function () {
        useremailresult = getFocus(this, " * 电子邮件用于验证")
    }).keyup(function () {
        useremailresult = getChange(this, regExp_email, true, "ashx/ValidEmail.ashx", " * 电子邮件用于验证")
    }).blur(function () {
        useremailresult = getBlur(this, regExp_email, true, "ashx/ValidEmail.ashx", "* 电子邮件用于验证")
    });


    //处理中文名的密证

    var regExp_chinesename = new RegExp("^[\u4E00-\u9FA5]{2,10}$");//中文名,要求2-10个字符
    UserChineseName.focus(function () {
        userchinesenameresutl = getFocus(this, " 请使用2-10位中文字符")
    }).keyup(function () {
        userchinesenameresutl = getChange(this, regExp_chinesename, false, "notUnique", " 请使用2-10位中文字符")
    }).blur(function () {
        userchinesenameresutl = getBlur(this, regExp_chinesename, false, "notUnique", " 请使用2-10位中文字符")
    });

    //处理电话号码的验证

    var regExp_tel = new RegExp("(^(\d{3,4}-?)?\d{7,8}$)|(^1\d{10}$)");//电话号码或手机号码
    Tel.focus(function () {
        telresutl = getFocus(this, " 请使用手机或电话号码")
    }).keyup(function () {
        //telresutl = getChange(this, regExp_tel, false, "notUnique", " 请使用手机或电话号码")
    }).blur(function () {
        telresutl = getBlur(this, regExp_tel, false, "notUnique", " 请使用手机或电话号码")
    });

    //处理验证码的验证

    var regExp_code = new RegExp("^[a-zA-Z0-9]{6}$"); //验证码(六位的字母、数字);
    Codes.focus(function () {
        $(this).parent().next().children().css({ "border": "solid 1px #FF9924" });
        codesresult = getFocus(this)
    }).keyup(function () {
        codesresult = getChange(this, regExp_code, true, "ashx/valid.ashx")
    }).blur(function () {
        $(this).parent().next().children().css({ "border": "solid 1px #CCC" });
        codesresult = getBlur(this, regExp_code, true, "ashx/valid.ashx")
    });

    //处理提交按钮事件
    btn.mouseover(function () {
        $(this).css({ "border": "solid 1px #FF9924", "background-color": "#FF9924" });
        $("#sub").css({ "background-color": "#FF9924" });
    }).mouseout(function () {
        $(this).css({ "border": "solid 1px #ccc", "background-color": "#ccc" });
        $("#sub").css({  "background-color": "#ccc" });
    }).click(function () {
        //禁止提交页面
        $("form").submit(function () {
            return false;
        });
        if (usernameresult &&
            passwordresult &&
            sec_passwordresult &&
            useremailresult &&
            userchinesenameresutl &&
            telresutl &&
            codesresult) {//如果所有的验证都通过
            //异步调用
            //方法签名
            //function Uajax(datas, page, callback_success, callback_error) 无返回值
            Uajax(getData(), page, register_success, register_error, btn);
        }
    });

});

//===================================自定义函数区===================================================================


//控件获得焦点时的方法
//参数param 获得焦点的控件
//参数 message 要显示的提示信息
function getFocus(param, message) {
    var handler = $(param);
    var prve = handler.parent().prev();
    var next = handler.parent().next();
    prve.css({ "color": "#FF9924" });
    if (message != undefined)
        next.css({ "color": "#FF9924" }).html(message);
    handler.css({ "border": "solid 1px #FF9924" }).val("");
    return false;
}

//控件数值发生改变时的方法
//参数 param 失去焦点的的控件
//参数 Exp 控件要验证的正则表达式
//参数notNull boolean值,表示是控件的值是否可以为空,可以为空false,否则 true
//参数 Page如果该控件内容要求具有唯一性,则为异步验证的地址,否则为"notUnique"
//参数 message 要显示的提示信息
function getChange(param, Exp, notNull, Page, message) {
    var handler = $(param);//控件
    var next = handler.parent().next();
    //内容为空时
    if (handler.val() == null || handler.val() == "") {
        //内容不可为空
        if (notNull == true) {
            if (message != undefined)
                next.html("内容不能为空");
            next.css({ "color": "red", "font-weight": "bolder" });
            return false;
        } else {//内容可以为空
            if (message != undefined)
                next.html(message);
            next.css({ "color": "#FF9924", "font-weight": "normal" });
            return true;
        }
        //如果内容不为空,都要求验证内容的有效性
    } else {
        return Valid(handler, Exp, Page, message);
    }
}
//控件失去焦点的方法
//参数 param 失去焦点的的控件
//参数 Exp 控件要验证的正则表达式
//参数notNull boolean值,表示是控件的值是否可以为空,可以为空false,否则 true
//参数 Page如果该控件内容要求具有唯一性,则为异步验证的地址,否则为"notUnique"
//参数 message 要显示的提示信息
function getBlur(param, Exp, notNull, Page, message) {
    var handler = $(param);
    var prve = handler.parent().prev();
    var next = handler.parent().next();
    handler.css({ "border": "solid 1px #CCC" });
    prve.css({ "color": "#777" });
    next.css({ "color": "#CCC" });
    //如果内容为空
    if (handler.val() == null || handler.val() == "") {
        //内容不可为空
        if (notNull == true) {
            if (message != undefined)
                next.html("内容不能为空");
            next.css({ "color": "red", "font-weight": "bolder" });;
            return false;
        } else {//内容可以为空
            if (message != undefined)
                next.html(message);
            next.css({ "color": "#CCC", "font-weight": "normal" });;
            return true;
        }
        //如果内容不为空,都要求验证内容的有效性
    } else {
        return Valid(handler, Exp, Page, message);
    }
}

//验证用户输入是否合法
//handler 要验证的控件
//Exp控件要求满足的要求
//返回值 成功 true;失败 false
//参数 message 显示的说明
function Valid(param, Exp, Page, message) {
    var handler = param;
    var next = handler.parent().next();
    var value = handler.val();
    var data = { name: value };
    //首先验证内容是否符合要求
    if (Exp.test(value)) {
        //根据内容是否要求具有唯一性决定是否服务器验证
        if (Page != "notUnique") {//要求数据具有唯一性
            // next.html("<img src='Images/loading.gif' alt='验证中' />");
            //其次验证内容的唯一性
            //函数的签名
            //function Uajax(datas, page, callback_success, callback_error,this) 
            Uajax(data, Page, Valid_success, Valid_error, handler);
        } else {//不要求数据具有唯一性
            next.css({ "color": "green", "font-weight": "normal" }).html("<img src='Images/true.png' alt='正确' />");
            return true;
        }
    } else {
        if (message != undefined && handler.attr("id") != "Codes")
            next.html(message);
        //if (handler.attr("id") == "Codes")
        //    next.html("<img id='ValidCode' alt='验证码' src='ashx/HandlerValidCode.ashx?wordnum=6&height=40' onclick='checkcode()' />");
        next.css({ "color": "red", "font-weight": "bolder" });
        return false;
    }
}

//初始化
function init() {
    usernameresult = false;//用户名的验证结果
    passwordresult = false;//密码的验证结果
    sec_passwordresult = false;//重复密码的验证结果
    useremailresult = false;//电子邮件的验证结果
    userchinesenameresutl = true;//中文名的验证结果
    telresutl = true;//电话号码的验证结果
    codesresult = false;//验证码的验证结果
    txtusername.val("");
    txtpassword.val("");
    sec_password.val("");
    UserEmail.val("");
    UserChineseName.val("");
    Tel.val("");
    Codes.val("");
    PassWordStrongInit();
}
/*注册异步处理成功以后执行的方法*/
function register_success(data, obj) {
    alert("注册成功！");
    window.location.href = "ashx/Login.ashx";
}
/*注册异步处理失败以后执行的方法*/
function register_error(obj) {
    alert("注册失败");
}
//验证成功以后执行的方法
//如果data=true,则不存在，可以使用，反之则存在，不能注册
function Valid_success(data, obj) {
    var next = obj.parent().next();
    if (data) {
        next.css({ "color": "green", "font-weight": "normal" }).html("<img src='Images/true.png' alt='正确' />");
        GetResultTitleByObjAndPayTrue(obj);//修改验证成功的标记
    } else if (data == false) {
        next.css({ "color": "red", "font-weight": "normal" });
        if (obj.attr("id") != "Codes")
            next.html("<img src='Images/false.png' alt='错误' />数据已经存在");
        else {
            next.html("<img id='ValidCode' alt='验证码' src='ashx/HandlerValidCode.ashx?wordnum=6&height=40' onclick='checkcode()' />");
        }
    } else {
        next.css({ "color": "red", "font-weight": "normal" });
        if (obj.attr("id") != "Codes")
            next.html("<img src='Images/false.png' alt='错误' />验证失败");
        else {
            next.html("<img id='ValidCode' alt='验证码' src='ashx/HandlerValidCode.ashx?wordnum=6&height=40' onclick='checkcode()' />");
        }
    }
}
//验证失败以后执行的方法
function Valid_error(obj) {
    var next = obj.parent().next();
    next.css({ "color": "red", "font-weight": "normal" });
    if (obj.attr("id" != "Codes"))
        next.html("<img src='Images/false.png' alt='错误' />验证失败");
    else {
        next.html("<img id='ValidCode' alt='验证码' src='ashx/HandlerValidCode.ashx?wordnum=6&height=40' onclick='checkcode()' />");
    }
}

//通过控件取得该控件成功的标志
//参数 obj 要取得成功标志的控件
//返回值 无返回值 
function GetResultTitleByObjAndPayTrue(obj) {
    var id = obj.attr("id");
    switch (id) {
        case "txtusername": usernameresult = true; break;
        case "txtpassword": passwordresult = true; break;
        case "password": sec_passwordresult = true; break;
        case "UserEmail": useremailresult = true; break;
        case "UserChineseName": userchinesenameresutl = true; break;
        case "Tel": telresutl = true; break;
        case "Codes": codesresult = true; break;
    }
}
//验证重复密码
function ValidSec_Password(password, sec_password) {
    var prve = sec_password.parent().prev();
    var next = sec_password.parent().next();
    prve.css({ "color": "#777" });
    sec_password.css({ "border": "solid 1px #CCC" });
    next.css({ "color": "#CCC" });
    if (sec_password.val() != "") {//值不为空
        if (password.val() == sec_password.val()) {//两次的密码相同
            next.css({ "color": "green", "font-weight": "normal" }).html("<img src='Images/true.png' alt='正确' />");
            return true;
        }
        else {//两次的密码不同
            next.css({ "color": "red", "font-weight": "bolder" }).html("两次输入的密码不相同");
            return false;
        }
    } else {//值为空
        next.css({ "color": "#FF9924", "font-weight": "normal" }).html(" *");
        return false;
    }
}

//获取控件的数据
function getData() {
    var requestdata = {
        "username": txtusername.val(),
        "password": txtpassword.val(),
        "email": UserEmail.val(),
        "chinesename": UserChineseName.val(),
        "tel": Tel.val()
    };//注册需要提交的数据
    return requestdata;
}
//初始化密码强度条
function PassWordStrongInit() {
    Stronger1.css({ "background-color": "#CCC" });
    Stronger2.css({ "background-color": "#CCC" });
    Stronger3.css({ "background-color": "#CCC" });
    PassWordStrong = 0;
}

//初始化密码强度的同步
function PassWordStrongSynchronous(parm) {
    PassWordStrong = 0;
    var val = $(parm).val();
    var array = new Array(new RegExp("[0-9]+"), new RegExp("[a-z]+"), new RegExp("[A-Z]+"));
    for (var i = 0; i < array.length; i++) {
        if (array[i].test(val)) {
            PassWordStrong++;
        }
    }
    switch (PassWordStrong) {
        case 0:; PassWordStrongInit(); break;
        case 1: Stronger1.css({ "background-color": "red" }); Stronger2.css({ "background-color": "#CCC" }); Stronger3.css({ "background-color": "#CCC" }); break;
        case 2: Stronger1.css({ "background-color": "yellow" }); Stronger2.css({ "background-color": "yellow" }); Stronger3.css({ "background-color": "#CCC" }); break;
        case 3: Stronger1.css({ "background-color": "green" }); Stronger2.css({ "background-color": "green" }); Stronger3.css({ "background-color": "green" }); break;
        default:; PassWordStrongInit();
    }
}