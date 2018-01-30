<%@ WebHandler Language="C#" Class="getUserModelbyUsernameAndPassword" %>

using System;
using System.Web;
using OnLineTest.BLL;
using OnLineTest.Model;
using System.Web.Script.Serialization;

public class getUserModelbyUsernameAndPassword : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //string username = context.Request.QueryString["username"];
        string username = context.Request.Form["username"];
        //string password = context.Request.QueryString["password"];
        string password = context.Request.Form["password"];
        UsersManager manager = new UsersManager();
        int num=-1;
        Users user = null;
        if (!string.IsNullOrEmpty(password)&&!string.IsNullOrEmpty(username)&&manager.Exists(username, common.GetMD5(password), out num) && num == 1) {
            user = manager.GetModel(username);
        }
        context.Response.Write(serializer.Serialize(user));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}