<%@ WebHandler Language="C#" Class="HandlerRegister" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using log4net;

public class HandlerRegister : IHttpHandler, IRequiresSessionState
{
    //处理用户的注册
    ILog logger = LogManager.GetLogger(typeof(HandlerRegister));
    public void ProcessRequest(HttpContext context)
    {
        
        string txtusername = context.Request.Form["txtusername"].ToString().Trim().ToLower();
        string txtpassword = context.Request.Form["txtpassword"].ToString();
        string UserEmail = context.Request.Form["UserEmail"].ToString().Trim().ToLower();
        string UserChineseName = context.Request.Form["UserChineseName"].ToString().Trim().ToLower();
        string Tel = context.Request.Form["Tel"].ToString().Trim().ToLower();
        string ValidCode = context.Request.Form["ValidCode"].ToString().Trim().ToLower();
        if (!string.IsNullOrEmpty(ValidCode) && ValidCode.Replace('o','0') == context.Session["ValidCode"].ToString().Trim().ToLower())////验证码输入正确
        {
            Users user = new Users();
            user.UserName = txtusername;
            user.UserPassword = common.GetMD5(txtpassword);
            user.UserEmail = UserEmail;
            user.UserChineseName = UserChineseName;
            user.Tel = Tel;
            UsersManager UsersManager = new UsersManager();
            int getCount;
            if (!UsersManager.Exists(user.UserName, user.UserPassword, out getCount) && getCount == 1)
            {
                try
                {
                    int i = UsersManager.Add(user);
                    context.Session["User"] = user;
                    logger.Info("新增用户：" + user.UserName);
                    context.Response.Redirect("~/main.html", true);
                }
                catch (Exception ex)
                {
                    logger.Error("新增用户失败。", ex);
                    common.ServerTransfer("error.aspx", 1005, ex, "来自：新增用户部分——失败。",string.Empty);
                }
            }
            else
            {
                common.ServerTransfer("error.aspx",1005,"用户不存在或查询异常",string.Empty);
            }
        }
        else
        { //验证码输入错误
            common.ServerTransfer("error.aspx",1004,"难证码错误",string.Empty);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}