<%@ WebHandler Language="C#" Class="register" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using log4net;

public class register : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private static ILog logger = LogManager.GetLogger(typeof(register));
    public delegate void SendMailDelegate(string to, string subject, string body);
    public void ProcessRequest(HttpContext context)
    {
        string UserName = context.Request.Form["username"].Trim();
        string PassWord = context.Request.Form["password"].Trim();
        string Email = context.Request.Form["email"].Trim();
        string ChineseName = context.Request.Form["chinesename"].Trim();
        string Tel = context.Request.Form["tel"].Trim();
        bool resutl = false;
        //初始化用户对象
        Users user = new Users();
        user.UserName = UserName;
        user.UserPassword = common.GetMD5(PassWord);
        user.UserEmail = Email;
        user.UserChineseName = ChineseName;
        user.Tel = Tel;
        user.UserImageName = "default.jpg";
        user.IsValidate = false;
        user.UserScore = 0;
        user.UserRegisterDatetime = DateTime.Now;
        UserGroupManager UserGroupManager = new OnLineTest.BLL.UserGroupManager();
        UserGroup UserGroup = UserGroupManager.GetModel("普通用户");
        user.UserGroupId = UserGroup.UserGroupId;
        UsersManager usermanager = new UsersManager();
        int i = 0;
        //新增用户
        try
        {
            i = usermanager.Add(user);
        }
        catch (Exception ex)
        {
            logger.Error("新增用户错误" + ex.Message);
        }
        if (i > 0)
        {     //这个下面考虑采用异步的方式进行      
            SendMailDelegate SMDelegate = SendMail;
            IAsyncResult asyresult = SMDelegate.BeginInvoke(user.UserEmail, "请及时验证邮箱的正确性_来自：船员在线考试系统", "校验码: " + user.UserPassword.Substring(0, 6), null, null);
            user.UserId = i;
            resutl = true;
            context.Session.Add("User", user);
            context.Response.Cookies.Add(new HttpCookie("UserId", user.UserId.ToString()));
            SMDelegate.EndInvoke(asyresult);
        }
        context.Response.ContentType = "text/plain";
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        context.Response.Write(serializer.Serialize(resutl));
    }

    //发送邮件的方法
    public void SendMail(string To, string subject, string body)
    {
        BLL.Mail mail = new BLL.Mail();
        int i = 0; bool result = false; Exception e = null;
        do
        {
            System.Threading.Thread.Sleep(2000);
            try
            {
                result = mail.Send(To, subject, body);
            }
            catch (Exception ex)
            {
                e = ex;
            }
        } while (result == false && i < 10);//连续发送10次,如果还是失败,则放弃
        if (e != null)
            logger.Warn("邮件发送失败: " + e.Message);//记入日志
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}