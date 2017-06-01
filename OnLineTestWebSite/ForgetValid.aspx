<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-unlogined.master" AutoEventWireup="true" CodeFile="ForgetValid.aspx.cs" Inherits="ForgetValid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/ForgetValid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <div id="main">
        <div id="left">
            <div id="lTop">
                <div>
                    <img alt="确认码" src="Images/ViledEmailCode.png" /></div>
                <div>请输入您邮箱中的确认码，填写正确的确认码后可以设置新的登录密码。<br />
                    如果您三分钟未收到验证码，请点击<input type="button" value="重新发送邮箱确认码" /></div>
            </div>
            <div id="rDown">
                <div id="name">用户名：<input type="text" id="UserName" /></div>
                <div id="EmailVaildCode">邮箱确认码：<input type="text" id="OkCode" /></div>
                <div id="NewPsw">请输入新密码：<input type="password" id="NewPassword" /></div>
                <div id="VaildNewPsw">再次输入新密码：<input type="text" id="OkPassword" /></div>
                <div id="SubNewPasswork">
                    <input type="submit" id="BtnPswReset" value="密码重置"/></div>
            </div>
        </div>
        <div style="float: right;" id="right">
            <a href="register.aspx">注册新账号</a>
        </div>
    </div>
</asp:Content>

