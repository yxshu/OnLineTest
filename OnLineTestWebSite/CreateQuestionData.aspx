<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-logined.master"
    AutoEventWireup="true" CodeFile="CreateQuestionData.aspx.cs" Inherits="CreateQuestionData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">

    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="txt文件位置："></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" Width="227px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="索引位置："></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" Width="227px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button2_Click" Text="将txt文件导入数据库并创建索引" />
        <br /><br /><br /><br />
        <asp:Button ID="Button2" runat="server" Text="生成试题" OnClick="createQuestion" />
    </form>


</asp:Content>
