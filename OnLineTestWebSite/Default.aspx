<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线考试系统 登录</title>
    <link href="CSS/common.css" rel="stylesheet" type="text/css" />
    <link href="CSS/default.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Default.js" type="text/javascript"></script>

</head>
<body>
    <center>
        <form method="post" id="form1" action="ashx/Login.ashx" runat="server">
            <div id="container">
                <!--这里是头-->
                <div id="header">
                    <%--尺寸：150px*1000px  在CSS里放入一个LOGO.PNG--%>
                </div>
                <!--这个区域主要用来放背景图的，宽度100%  高度400px ，后面的小图标和登录窗口通过调整位置使其位于此背景的上面 -->
                <div id="Mbody">
                    <!--这里是背景导航的小图标-->
                    <div id="minico">
                        <ul>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                        </ul>
                    </div>
                </div>
                <!--这是登录的窗口--->
                <div id="content">
                    <!--标题--->
                    <div id="title">登录考试系统</div>
                    <!--用户名，邮箱/用户名--->
                    <div id="txtUser">
                        <img id="usernameico" class="tubiao" src="Images/username.jpg" alt="用户名" title="用户名" /><input type="text" name="txtusername" id="username" class="shurukuan" value="" />
                    </div>
                    <!--密码--->
                    <div id="txtPassword">
                        <img id="passwordico" class="tubiao" src="Images/password.jpg" alt="密码" title="密码" /><input type="password" name="txtpassword" id="password" class="shurukuan" value="" />
                    </div>
                    <!--验证码--->
                    <div id="txtValidCode">
                        <img id="validico" class="tubiao" src="Images/valid.jpg" alt="验证码" title="验证码" /><input type="text" name="txtValidCode" id="validcode" class="shurukuan" value="验证码" /><span id="validmark"></span>
                        <!--验证码图片--->
                        <%="<img id='codeimg1' alt='验证码' src='ashx/HandlerValidCode.ashx?wordnum=4&height=30&id="+DateTime.Now+"' onclick='checkcode()'/>" %>
                    </div>
                    <div id="errormessage"></div>
                    <!--登录按钮--->
                    <div id="denglu">
                        <input id="btnsubmit" type="submit" value="登  录" />
                    </div>
                    <!--底部注册--->
                    <div id="last"><a href="register.html" id="register">免费注册</a><span id="lianxigly"><a href="#">账户激活</a><a href="#">联系管理员</a></span></div>
                </div>

                <!--这是页脚部分--->
                <div id="footer">
                    <p>
                        地址:湖北省武汉市洪山区白沙洲大道6号 邮编:430065 电话:027-88756000 备案号:鄂ICP备06007470号
                    </p>
                    <br />
                    <p>
                        武汉交通职业学院 whtcc.edu.cn 2004-2016 &copy 版权所有 设计维护:航海学院 联系方式:航海学院 内容提供:航海技术教研室
                    </p>
                </div>

            </div>

        </form>
    </center>
</body>
</html>
