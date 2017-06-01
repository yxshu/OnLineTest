<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-logined.master"
    AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/search.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Search.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">

    <div id="UserUI">
        <!--试题和评论的选择标签-->
        <div id="SearchTitle">
            <span id="Question">试题</span>
            <span id="Comment">评论</span>
        </div>
        <!--搜索框区域-->
       
        <div id="UserData">
            <input type="text" name="keywords" id="keywords" />
            <input type="hidden" name="subject" id="Subject" value="Question" />
            <input type="hidden" name="currentNum" id="currentNum" value="0" />
            <input type="button" id="btnSubmit" value="搜  索" />
        </div>

        <!--搜索的结果显示区-->
        <div id="Result">
        </div>
    </div>
</asp:Content>
