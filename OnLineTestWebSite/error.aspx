<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-unlogined.master" AutoEventWireup="true"
    CodeFile="error.aspx.cs" Inherits="error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <form id="form1" action="#" method="post" runat="server">
        <asp:Label ID="Lberrormessage" runat="server"></asp:Label>
    </form>
</asp:Content>
