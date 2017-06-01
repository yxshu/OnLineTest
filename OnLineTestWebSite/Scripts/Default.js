var validcoderesult = false;
var usernameresult = true;//发布网站的时候，一定记得把这里改成false
var passwordresult = true;//发布网站的时候，一定记得把这里改成false
var backgroundimageid = Math.ceil(Math.random() * 4);
//页面载入以后的动作
//---------------------------------文档载入完成以后运行---------------------------------------
$(function () {
    ChangeMbodyBackGroundImage();  //先设置一个背景图像
    setInterval(ChangeMbodyBackGroundImage, 10000); // 注意函数名没有引号和括弧！alert("222");
    SetIcoChangeBackground();
    var regusername = new RegExp("^[a-z]([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\\.][a-z]{2,3}([\\.][a-z]{2})?$"); //邮箱格式错误
    var regusername2 = new RegExp("^[a-zA-Z0-9_]{5,20}$"); //用户名5-20个字符、数字和下划线
    var regpassword = new RegExp("^[a-zA-Z]\\w{5,17}$"); //以字母开头，长度在6-18之间
    var regvalid = new RegExp("^[a-zA-Z0-9]{4}$"); //验证码错误(四位的字母、数字)
    var username = $("#username");
    var password = $("#password");
    var valid = $("#validcode");
    //var btnsubmit = $("#btnsubmit");
    var form = $("#form1");

    username.focus(function () {

        usernameresult = false;
        $(this).val("").css({ "color": "Black" });
        $("#txtUser").css({ "border": "solid 1px #FF9924" });
        $("#usernameico").attr("src", "Images/username-c.jpg");
    }).blur(function () {
        $("#txtUser").css({ "border": "none" });
        $("#usernameico").attr("src", "Images/username.jpg");
        if ($(this).val() == "") {
            $(this).val("邮箱/用户名").css("color", "#BBBBBB");
            showerrormessage("邮箱/用户名不能为空");
        } else if (!validata($(this).val(), regusername) && !validata($(this).val(), regusername2)) {
            //验证没有通过
            showerrormessage("邮箱格式错误 或 用户名5-20个字符、数字和下划线");
        } else {
            usernameresult = true;
        } //验证通过
    });

    password.focus(function () {
        passwordresult = false;
        $(this).val("").css("color", "Black");
        $("#txtPassword").css({ "border": "solid 1px #FF9924" });
        $("#passwordico").attr("src", "Images/password-c.jpg");
    }).blur(function () {
        $("#txtPassword").css({ "border": "none" });
        $("#passwordico").attr("src", "Images/password.jpg");
        if ($(this).val() == "") {
            showerrormessage("密码不能为空");
        } else if (!validata($(this).val(), regpassword)) {
            //验证没有通过
            showerrormessage("密码由字母开头长度为6-18位");
        } else {
            passwordresult = true;
        }; //验证通过
    });

    valid.focus(function () {
        validcoderesult = false;
        $(this).val("").css("color", "Black");
        $("#txtValidCode").css({ "border": "solid 1px #FF9924" });
        $("#validico").attr("src", "Images/valid-c.jpg");
        $("#validmark").html("");
    }).on({
        "blur": function () {
            $("#txtValidCode").css({ "border": "none" });
            $("#validico").attr("src", "Images/valid.jpg");
        },
        "keyup": function () {
            var code = $(this).val();
            if (code.length >= 4) {
                if (validata(code, regvalid)) {
                    $("#validmark").html("<img src='Images/loading.gif' />");
                    $.ajax({
                        type: "post",
                        url: "ashx/valid.ashx",
                        data: { name: code },
                        dataType: "text",
                        beforeSend: function () {
                            showerrormessage("正在处理验证码", "green");
                        },
                        success: function (data) {
                            if (data == "true") {
                                validcoderesult = true;
                                $("#validmark").html("<img src='Images/true.png' />");
                                showerrormessage("验证码正确！", "green");
                                $("#btnsubmit").focus();
                            }
                            else if (data == "false") {
                                $("#validmark").html("<img src='Images/false.png' />");
                                showerrormessage("验证码错误！", "red");
                            } else {
                                $("#validmark").html("<img src='Images/false.png' />");
                                showerrormessage("验证码过期，请刷新验证码");
                            }
                        },
                        error: function () {
                            $("#validmark").html("<img src='Images/false.png' />");
                            showerrormessage("服务器出差去火星了！", "red");
                        }
                    });
                } else {
                    validcoderesult = false;
                    $("#validmark").html("<img src='Images/false.png' />");
                    showerrormessage("验证码错误！", "red");
                }

            }
        }
    })

    //提交表单时的验证
    $("#btnsubmit").click(function () {
        if (usernameresult && passwordresult && validcoderesult) {
            this.value = "正在登录…";
            form.submit();
        }
        else {
            if (!usernameresult) {
                showerrormessage("邮箱格式错误 或 用户名5-20个字符、数字和下划线"); //用户名输入错误
            } else if (!passwordresult) {
                showerrormessage("密码由字母开头长度为6-18位"); //密码输入错误
            } else if (!validcoderesult) {
                showerrormessage("正在处理验证码，请稍候！", "green"); //验证码输入错误
            };
            return false;
        }
    });
});

//----------------------------------下面自定义函数区---------------------------------------
//验证表单
function validata(string, regexp) { //表单验证函数
    $("#errormessage").empty();
    if (string.match(regexp) != null && string.match(regexp) != "" && string != "" && string != null) {
        return true;
    } else {
        return false;
    }
};

//验证错误时显示信息
function showerrormessage(message, color)//验证错误时显示信息
{
    if (color == "" || color == null)
        color = "red";
    var error = "<p style='color:" + color + "; font-size:12px; width:100%; heigth:20px; line-height:20px;'>* " + message + " *</p>";
    $("#errormessage").empty().append(error);
}

//改变主页面的背景图片，主页面的背景放在IMAGE文件夹下的四张图，分别是DefaultBackGround1.jpg-DefaultBackGround4.jpg,宽度要求1000px及以上，高度为400PX；
function ChangeMbodyBackGroundImage() {
    var mbody = $("#Mbody");
    var ico = $("#minico li");
    var id;
    do {
        id = Math.ceil(Math.random() * 4);
    } while (backgroundimageid != null && id == backgroundimageid);
    //console.log("backgroundid=" + backgroundimageid + ";id=" + id);
    backgroundimageid = id;
    var backGroundImageAddress = "url(./Images/DefaultBackGround" + id + ".jpg)";
    for (var i = 0; i < ico.length; i++) {
        if (i + 1 == id) {
            $(ico[i]).css("background-color", "#FF9924");
        } else {
            $(ico[i]).css("background-color", "#DDDDDD");
        }
    }
    SetBackground(backGroundImageAddress, mbody);
}

//设置背景图片
function SetBackground(backgroundimage, obj) {
    obj.fadeOut(1000, function () { obj.css({ "background-image": backgroundimage, "background-position": "center" }).fadeIn(1000); });
    //mbody.slideUp(1000, function () { mbody.css({ "background-image": backGroundImageAddress, "background-position": "center" }).slideDown(1000); });
}
//设置小方块改变颜色
function SetIcoChangeBackground() {
    var ico = $("#minico li");
    for (var i = 0; i < ico.length; i++) {
        $(ico[i]).click(function () {
            for (var j = 0; j < ico.length; j++) {
                $(ico[j]).css("background-color", "#DDDDDD");
            }
            $(this).css("background-color", "#FF9924");
            var backGroundImageAddress = "url(./Images/DefaultBackGround" + ($(this).index() + 1) + ".jpg)";
            SetBackground(backGroundImageAddress, $("#Mbody"));
        });
    }
}

//点击验证码动作
function checkcode() { //刷新验证码
    $("#codeimg1").attr("src", "ashx/HandlerValidCode.ashx?wordnum=4&height=30&id=" + Math.random());
}
