<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-unlogined.master" AutoEventWireup="true" CodeFile="ForgetPsw.aspx.cs" Inherits="ForgetPsw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/ForgetPsw.css" rel="stylesheet" />

    <style type="text/css">
        #SubEmail {
            width: 78px;
            height: 40px;
        }

        #EmailValid {
            height: 31px;
            width: 154px;
        }

        #Codes {
            height: 31px;
            width: 154px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <form action="ForgetValid.aspx">
        <div id="main">
            <div id="Top_Text">请输入您需要找回密码的邮箱</div>
            <div id="center_Text">
                <div align="center" id="Email"><span id="EmailLab">邮箱地址：</span><input type="text" id="EmailValid" /></div>
                <div align="center" id="CodeValid">
                    <div align="center" class="txtLab" id="txtLab">验证码：<input type="text" name="Code" id="Codes" /><div>
                        <img id="ValidCode" alt="验证码" src="ashx/HandlerValidCode.ashx?wordnum=6&height=35" onclick="checkcode()" /></div>
                    </div>
                    <div id="sub" align="center">
                        <input id="SubEmail" size="60" type="submit" value="提交" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

