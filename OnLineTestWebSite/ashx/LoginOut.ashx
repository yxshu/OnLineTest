<%@ WebHandler Language="C#" Class="LoginOut" %>
using System;
using System.Web;
using System.Web.SessionState;
using log4net;
using OnLineTest.BLL;
using OnLineTest.Model;
public class LoginOut : IHttpHandler, IRequiresSessionState
{
    //处理用户登出
    ILog logger = LogManager.GetLogger(typeof(LoginOut));
    public void ProcessRequest(HttpContext context)
    {
        if (context.Session["User"] != null)
        {
            if (context.Session["LogloginId"] != null)
            {
                context.Session["Logout"] = "1";
                LogLogin loglogin = new LogLogin();
                LogLoginManager logloginmanager = new LogLoginManager();
                if (HttpRuntime.Cache["LogloginId:" + context.Session["LogloginId"].ToString()] != null)
                {
                    loglogin = (LogLogin)HttpRuntime.Cache["LogloginId:" + context.Session["LogloginId"].ToString()];
                    HttpRuntime.Cache.Remove("LogloginId:" + context.Session["LogloginId"].ToString());
                }
                else
                {
                    loglogin = logloginmanager.GetModel(Convert.ToInt32(context.Session["LogloginId"]));
                }
                loglogin.LogLogoutTime = DateTime.Now;
                loglogin.Remark = context.Session["Logout"].ToString();
                logloginmanager.Update(loglogin);
            }
            logger.Info(((OnLineTest.Model.Users)context.Session["User"]).UserName + "成功退出。");
        }
        context.Session.Abandon();
        context.Response.Redirect("~\\Default.aspx", false);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}