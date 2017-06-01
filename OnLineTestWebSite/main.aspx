<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="OnLineTest.Web.main" %>


<asp:Content ID="Head" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Ajax.js" type="text/javascript"></script>
    <script src="Scripts/common.js" type="text/javascript"></script>
    <script src="Scripts/main.js" type="text/javascript"></script>

    <link href="CSS/main.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlace" runat="Server">
    <%--第一行--%>

    <%--异步加载时的画面 --%>
    <div class="loading" id="loading_user">
        <a href="#">
            <img src="Images/loading.gif" alt="练习记录" />正在载入个人信息</a>
    </div>
    <%--第二行--%>
    <div id="LastOperation" class="MainContent">
        <%--上次作业信息--%>
        <%--异步加载时的画面 --%>
        <div class="loading" id="load_LastOperation">
            <a href="#">
                <img src="Images/loading.gif" alt="练习记录" />正在载入练习记录</a>
        </div>

    </div>
    <%--第三行--%>
    <div id="PopularQuestion" class="MainContent">
        <%--最受欢迎试题信息  异步加载时的画面--%>
        <div class="loading" id="load_PopularQuestion">
            <a href="#">
                <img src="Images/loading.gif" alt="练习记录" />正在载入热题</a>
        </div>
        <div>
            <input type="hidden" class="hidden" id="LabNum" name="LabNum" value="0" />
<%--            <input type="hidden" class="hidden" id="isTrue" name="isTure" value="" />--%>
        </div>
    </div>
</asp:Content>

