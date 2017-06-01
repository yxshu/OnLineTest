<%@ Page Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="EditQuestion.aspx.cs" Inherits="TestAndPractice_EditQuestion" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/common.js" type="text/javascript"></script>
    <script src="../Scripts/Ajax.js" type="text/javascript"></script>
    <script src="../Scripts/EditQuestion.js" type="text/javascript"></script>
    <link href="../CSS/EditQuestion.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <%--用于保存用户当前获取上传试题页码值--%>
    <div>
        <input type="hidden" class="hidden" id="PageNum" name="PageNum" value="0" />
    </div>
    <%--新增按钮--%>

    <div id="AddNewQuestion">+ 新增</div>

    <%--展示上传试题区域--%>
    <div id="myQuestion"></div>
</asp:Content>
