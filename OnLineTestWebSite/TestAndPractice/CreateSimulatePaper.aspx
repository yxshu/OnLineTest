<%@ Page Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="CreateSimulatePaper.aspx.cs" Inherits="TestAndPractice_CreateSimulatePaper" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/CreateSimulatePaper.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Ajax.js"></script>
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/CreateSimulatePaper.js"></script>
    <script type="text/javascript" src="../Scripts/Ajax.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <form action="../ashx/CreateSimulatePaper.ashx" target="_blank" method="post" id="form"></form>
</asp:Content>
