<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="ShowQuestion.aspx.cs" Inherits="TestAndPractice_AddQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/ShowQuestion.css" type="text/css" rel="stylesheet" />
    <link href="../CSS/CreateQuestionDiv.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Ajax.js"></script>
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/CreateQuestionDiv.js"></script>
    <script type="text/javascript" src="../Scripts/ShowQuestion.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <form action="#" runat="server">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        

    </form>
    <script type="text/javascript">
        QuestionId = <%=HiddenField1.Value%>;
    </script>
</asp:Content>

