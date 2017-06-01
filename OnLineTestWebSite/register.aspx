<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-unlogined.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/register.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Ajax.js" type="text/javascript"></script>
    <script src="Scripts/register.js" type="text/javascript"></script>
    <script type="text/javascript">
        //此checkcode()方法是更换验证码图片的要点
        function checkcode() {
            document.getElementById("ValidCode").src = "ashx/HandlerValidCode.ashx?wordnum=6&height=40&id=" + Math.random();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <form id="form1" runat="server" action="#" method="post">
        <div id="reglogo"><span>
            <img src="Images/logo.png" alt="" /></span><span id="regist">&nbsp账号注册</span></div>
        <center>
            <div id="regmaincontent">
                <div id="navigator"><span class="navmsg" id="regnow">1、填写账户信息</span><span class="navlab" id="regnowlab">》</span><span class="navmsg">2、验证账户信息</span><span class="navlab">》</span><span class="navmsg">3、注册成功</span></div>
                <div id="regcontent">
                    <div>
                        <div class="txtLab">会员名：</div>
                        <div class="txt">
                            <input type="text" name="UserName" id="txtusername" /></div>
                        <div class="descrip">*&nbsp5-20个字符</div>
                    </div>
                    <div>
                        <div class="txtLab">密码：</div>
                        <div class="txt">
                            <input type="password" name="PassWord" id="txtpassword" /></div>
                        <div class="descrip">*&nbsp字母开头的6-18个字符</div>
                    </div>
                    <%--<div id="KeyStrong"><div class="txtLab"></div><div class="txt"><span class="Stronger">弱</span><span class="Stronger">中</span><span class="Stronger">强</span></div><div class="descrip"></div></div>--%>
                    <div id="KeyStrong">
                        <div id="txtLabspecial"></div>
                        <div id="txtspecial">
                            <div id="Stronger1">弱</div>
                            <div id="Stronger2">中</div>
                            <div id="Stronger3">强</div>
                        </div>
                        <div id="descripspecial"></div>
                    </div>
                    <div>
                        <div class="txtLab">重复密码：</div>
                        <div class="txt">
                            <input type="password" id="password" /></div>
                        <div class="descrip">*</div>
                    </div>
                    <div>
                        <div class="txtLab">电子邮件：</div>
                        <div class="txt">
                            <input type="text" name="Email" id="UserEmail" /></div>
                        <div class="descrip">*&nbsp电子邮件用于验证</div>
                    </div>
                    <div>
                        <div class="txtLab">中文名：</div>
                        <div class="txt">
                            <input type="text" name="ChineseName" id="UserChineseName" /></div>
                        <div class="descrip">请使用一个动听的文字吧</div>
                    </div>
                    <div>
                        <div class="txtLab">电话：</div>
                        <div class="txt">
                            <input type="text" name="Tel" id="Tel" /></div>
                        <div class="descrip">接收本站的通知哦</div>
                    </div>
                    <div>
                        <div class="txtLab">验证码：</div>
                        <div class="txt">
                            <input type="text" name="Code" id="Codes" /></div>
                        <div class="descrip" id="Code">
                            <img id="ValidCode" alt="验证码" src="ashx/HandlerValidCode.ashx?wordnum=6&height=40" onclick="checkcode()" /></div>
                    </div>
                </div>
                <div id="picture">
                    <img src="Images/小贝.jpg" alt="" /></div>
            </div>
            <div id="btnsubmit">
                <input type="submit" value="同意协议并注册" id="sub" /></div>
            <div id="agreement"><a href="agreement.htm" target="_blank">《在线考试系统服务协议》</a></div>
        </center>
    </form>
</asp:Content>

