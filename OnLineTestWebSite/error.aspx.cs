using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnLineTest.BLL;
using System.Text;

public partial class error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        int errorcode = 1005;
        string errormessage = string.Empty;
        string pagetitle = string.Empty;
        string SecondTransferPage = string.Empty;
        //错误码
        if (Request.QueryString.AllKeys.Contains("code"))
        {
            errorcode = Convert.ToInt32(Request.QueryString["code"]);
        }
        //产生的异常
        if (!string.IsNullOrEmpty(Request.QueryString["exception"]))
        {
            errormessage = Request.QueryString["exception"] + "<br />";
            errormessage += "<p>请联系管理员：<h2 style='color:Blue'>yxshu@qq.com</h2></p>";
        }
        //产生错误的页面
        if (Request.QueryString.AllKeys.Contains("page"))
        {
            pagetitle = Request.QueryString["page"] + "<br />";
        }
        //错误的提示信息
        if (Request.QueryString.AllKeys.Contains("errormessage"))
        {
            errormessage += "<br />";
            errormessage += "自定义错误消息：" + Request.QueryString["errormessage"] + "<br />";

        }
        //第二次跳转的页面
        if (Request.QueryString.AllKeys.Contains("SecondTransferPage"))
        {
            SecondTransferPage = Request.QueryString["SecondTransferPage"];
        }
        //显示给用户的定义错误信息
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<h1>Internet Explorer 无法显示该网页</h1>");
        sb.AppendLine("对不起，访问错误！<br />");
        sb.AppendLine("错误码：" + errorcode.ToString());
        sb.AppendLine("<hr />");
        sb.AppendLine("<h3>详细信息：</h3>");
        sb.AppendLine(errorcode.ToString() + "：" + Enum.GetName(typeof(BLL.ErrorCodes), errorcode) + "<br />");
        sb.AppendLine(errormessage);
        if (!string.IsNullOrEmpty(pagetitle))
        {
            sb.AppendLine("错误项来源：" + pagetitle);
        }
        //第一次跳转的路径
        string path = "Default.aspx";
        if (!string.IsNullOrEmpty(SecondTransferPage))
        {
            path += "?SecondTransferPage=" + SecondTransferPage;
            sb.AppendLine(common.RootPath + SecondTransferPage + "<br />");
        }
        sb.AppendLine("<a title='正在准备跳转' href='" + path + "' id='SecondTransferPage'><span id='SecondTransferPageTime'>5</span>秒钟以后进行跳转，如果你的浏览器不支持，请点击这里。</a>");
        Lberrormessage.Text = sb.ToString();

        //定义自动跳转的js代码
        StringBuilder JQuerySB = new StringBuilder();
        JQuerySB.AppendLine("$(document).ready(function () {");
        JQuerySB.AppendLine(" var SecondTransferPageTime = $('#SecondTransferPageTime');");
        JQuerySB.AppendLine("var ChangeTime = setInterval(function () {");
        JQuerySB.AppendLine("SecondTransferPageTime.text(SecondTransferPageTime.text() - 1);");
        JQuerySB.AppendLine("if (SecondTransferPageTime.text() <= 0) {");
        JQuerySB.AppendLine("clearInterval(ChangeTime);");
        JQuerySB.AppendLine("window.location.href = '"+path+"';");
        JQuerySB.AppendLine("}");
        JQuerySB.AppendLine(" }, 1000);");
        JQuerySB.AppendLine("});");
        common.InsertJQueryCodeByRegisterClientScriptBlock(this.Page, "jqb", JQuerySB.ToString());












    }
}
