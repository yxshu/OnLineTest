<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Text.RegularExpressions;
using log4net;

public class Login : IHttpHandler, IRequiresSessionState
{
    //处理用户登录
    private static ILog logger = LogManager.GetLogger(typeof(Login));
    public void ProcessRequest(HttpContext context)
    {
        Users singleUser = null;
        if (context.Session["User"] == null)//以前没有登录过
        {
            string UserName = string.Empty;
            string PassWord = string.Empty;
            string ValidCode = string.Empty;
            string SecondTransferPage = context.Request.UrlReferrer.Query.Trim();//从这里取得要二次跳转的页面地址
            //bool IsChecked = false;
            if (context.Request.Form.HasKeys())//数据正常提交过来
            {
                UserName = context.Request.Form["txtusername"].ToString().Trim().ToLower();//有可能是用户名，也有可能是电子邮件地址
                PassWord = context.Request.Form["txtpassword"].ToString().Trim();
                ValidCode = context.Request.Form["txtValidCode"].ToString().Trim().ToLower();
                //IsChecked = context.Request.Form["chkremember"] == null ? false : true;
            }
            //输入的验证码正确
            if (!string.IsNullOrEmpty(ValidCode) && Regex.IsMatch(ValidCode, "^[a-zA-Z0-9]{4}$", RegexOptions.IgnoreCase) && context.Session["ValidCode"] != null && ValidCode.Replace('o', '0') == context.Session["ValidCode"].ToString().Trim().ToLower())
            {
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PassWord))//表示用户提交了数据
                {
                    string MD5PassWord = common.GetMD5(PassWord);
                    int getCount;
                    UsersManager usersmanager = new UsersManager();
                    try
                    {
                        if (usersmanager.Exists(UserName, MD5PassWord, out getCount) && getCount == 1)//表示用户确实存在,可以是用户名也可以是电子箱邮地址
                        {
                            singleUser = usersmanager.GetModel(UserName);//从数据库中获得完整的用户实例，可以是用户名，也可以是电子邮箱地址
                            LogLogin loglogin = new LogLogin();//实例化登录记录
                            loglogin.UserId = singleUser.UserId;
                            loglogin.LogLoginIp = context.Request.UserHostAddress;
                            loglogin.LogLoginTime = DateTime.Now;
                            loglogin.LogLogoutTime = null;
                            loglogin.LogLoginOperatiionSystem = common.GetOSNameByUserAgent(context);
                            loglogin.LogLoginWebServerClient = context.Request.Browser.Browser + context.Request.Browser.Version;
                            loglogin.Remark = "0";
                            LogLoginManager logloginmanager = new LogLoginManager();
                            int i = logloginmanager.Add(loglogin);//添加登录记录，并返回记录的ID
                            loglogin.LogLoginId = i;
                            HttpRuntime.Cache.Insert("LogloginId:" + i, loglogin, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            if (i != 0)//成功添加登录记录并返回值
                            {
                                context.Session["User"] = singleUser;
                                context.Session["LogloginId"] = i;
                                logger.Info(singleUser.UserChineseName + "登录成功。");
                                HttpCookie cookie = new HttpCookie("UserName", singleUser.UserName);
                                HttpCookie cookieid = new HttpCookie("UserId", singleUser.UserId.ToString());
                                context.Response.Cookies.Add(cookie);
                                context.Response.Cookies.Add(cookieid);
                                //下面开始处理二次跳转的问题，即，如果用户从哪个页面来到这里就让他回到哪里去
                                string path = "~\\main.aspx";
                                if (!string.IsNullOrEmpty(SecondTransferPage))
                                {
                                    char[] c = new char[3] { '?', '&', '=' };
                                    string[] split = SecondTransferPage.Split(c);
                                    for (int j = 0; j < split.Length; j++)
                                    {
                                        if (split[j].ToString() == "SecondTransferPage")
                                        {
                                            path = split[j + 1];
                                            break;
                                        }
                                    }
                                }
                                context.Response.Redirect(path, false);
                            }
                            else
                            {
                                common.ServerTransfer("error.aspx", 1005, "用户登录记录写入错误", string.Empty);
                            }
                        }
                        else
                        {
                            common.ServerTransfer("error.aspx", 1000, "用户不存在", string.Empty);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("检测用户是否存在或根据用户名得到用户实例类错误,尼玛，讲这么多干嘛——用户不存在。", ex);
                        context.Session.Abandon();
                        common.ServerTransfer("error.aspx", 1005, ex, "检测用户是否存在或根据用户名得到用户实例类错误,尼玛，讲这么多干嘛——用户不存在", string.Empty);
                    }
                }
                else //用户没有提交数据或提交不全
                {
                    common.ServerTransfer("error.aspx", 1002, "用户没有提交数据或提交不够完整", string.Empty);

                }
            }
            else//没有输入验证码或验证码输入错误
            {
                common.ServerTransfer("error.aspx", 1004, "没有输入验证码或验证码输入错误", string.Empty);
            }
        }
        else//已经登录用户多次跳转到登录页
        {
            singleUser = (Users)context.Session["User"];
            context.Response.Redirect("~/main.aspx?UserName=" + singleUser.UserName.ToString() + "&UserGroupId=" + singleUser.UserGroupId.ToString(), false);
        }
    }
    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }
}