<%@ Page Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="Practice.aspx.cs" Inherits="TestAndPractice_Practice" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../CSS/Practice.css" />
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/Ajax.js"></script>
    <script type="text/javascript" src="../Scripts/Practice.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <%--    <div id="title"></div>
    <div id="QuestionZone">
        <div id="left" class="arrow"><</div>
        <div id="question"></div>
        <div id="right" class="arrow">></div>
    
    </div>--%>
    <div id="loading">
        <img  alt="载入中" src="../Images/loading.gif" />正在载入中</div>
   
</asp:Content>

