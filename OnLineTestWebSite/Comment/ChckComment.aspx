<%@ Page Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="ChckComment.aspx.cs" Inherits="Comment_ChckComment" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>查看评论</title>
    <script type="text/javascript" src="../Scripts/Ajax.js"></script>
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/Comment.js"></script>
    <link type="text/css" href="../CSS/Comment.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">
<input type="hidden" name="currentNum" id="currentNum" value="0" />
</asp:Content>

