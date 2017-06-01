<%@ WebHandler Language="C#" Class="ValidEmail" %>

using System;
using System.Web;
using System.Text.RegularExpressions;
using OnLineTest.BLL;
using OnLineTest.Model;

public class ValidEmail : IHttpHandler
{
    //根据邮箱地址，判断邮箱是否已经注册
    //如果 存在则返回false,不存在则返回true;
    //提交的数据为 {name: username}
    public void ProcessRequest(HttpContext context)
    {
        bool result = false;
        string mail = context.Request.Form["name"].Trim();
        Users User = new Users();
        Regex regExp = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{3,5})\\s*$", RegexOptions.IgnoreCase);
        if (!String.IsNullOrEmpty(mail) && regExp.IsMatch(mail))
        {
            UsersManager UserManage = new UsersManager();
            User=UserManage.GetModel(mail);
        }
        if (User == null)
        {
            result = true;
        }
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(result));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}