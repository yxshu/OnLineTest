<%@ Page Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="CheckInsideMessage.aspx.cs" Inherits="InsideMessage_CheckInsideMessage" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/common.css" rel="Stylesheet" />
    <link href="../CSS/message_table.css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/Ajax.js"></script>
    <script type="text/javascript" src="../Scripts/message_table.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <div id="tabzone">
        <div class="tab tabactive">收件箱</div>
        <div class="tab">发件箱</div>
        <div class="tab">新建站内信</div>
    </div>
    <div id="contentzone">
        <div class="content contentactive">


            <div class="item itemtitle">
                <div class="Chkbox">选择</div>
                <div class="MessageFromORTo">发件人</div>
                <div class="MessageContent">内容</div>
                <div class="Sendtime">发送时间</div>
            </div>


        </div>
        <div class="content">

        </div>
        <div class="content">
            <div id="To">
                <input type="text" name="inputto" id="inputTo" value="收件人:  " />
            </div>
            <div id="lastcontact">
                <div class="lastcontact_contact" id="firstcontactitem">最近联系人:</div>
            </div>
            <div id="Content">
                <textarea name="textArea1" id="inputContent" cols="45" rows="5"></textarea>
            </div>
            <div id="submit">
                发送
            </div>
        </div>
    </div>
</asp:Content>

