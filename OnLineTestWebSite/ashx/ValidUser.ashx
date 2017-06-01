<%@ WebHandler Language="C#" Class="ValidUser" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using OnLineTest.BLL;
using OnLineTest.Model;

public class ValidUser : IHttpHandler
{
    //根据用户名，判断用户的是否存在
    //如果 存在则返回false,不存在则返回true;
    //提交的数据为 {name: username}
    public void ProcessRequest(HttpContext context)
    {
        bool result = false;
        string username = context.Request.Form["name"].Trim();
        Users User = new Users();
        Regex regExp = new Regex("^[a-zA-Z0-9_]{5,20}$", RegexOptions.IgnoreCase);
        if (!String.IsNullOrEmpty(username) && regExp.IsMatch(username))
        {
            UsersManager UserManage = new UsersManager();
            User = UserManage.GetModel(username);
        }
        if (User == null)
        {
            result = true;
        }
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new JavaScriptSerializer();
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