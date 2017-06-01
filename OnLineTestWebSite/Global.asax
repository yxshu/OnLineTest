<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //在应用程序启动时运行的代码
        log4net.Config.XmlConfigurator.Configure();
        log4net.ILog logger = log4net.LogManager.GetLogger("application_star");
        logger.Fatal("系统启动成功。");
    }

    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码
        log4net.ILog logger = log4net.LogManager.GetLogger("application_end");
        logger.Fatal("系统关闭。");
    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码
        try
        {

        }
        catch (Exception ex)
        {
            OnLineTest.BLL.common.ServerTransfer("error.aspx", 1005, ex, string.Empty);
            log4net.ILog logger = log4net.LogManager.GetLogger("application_error");
            logger.Fatal("系统运行错误。", ex);
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。
        
        if (Session["User"] != null)
        {
            if (Session["LogloginId"] != null)
            {
                OnLineTest.Model.LogLogin loglogin = new OnLineTest.Model.LogLogin();
                OnLineTest.BLL.LogLoginManager logloginmanager = new OnLineTest.BLL.LogLoginManager();
                if (HttpRuntime.Cache["LogloginId:" + Session["LogloginId"].ToString()] != null)
                {
                    loglogin = (OnLineTest.Model.LogLogin)HttpRuntime.Cache["LogloginId:" + Session["LogloginId"].ToString()];
                    HttpRuntime.Cache.Remove("LogloginId:" + Session["LogloginId"].ToString());
                }
                else
                {
                    loglogin = logloginmanager.GetModel(Convert.ToInt32(Session["LogloginId"]));
                }
                if (Session["Logout"] == null)
                {
                    loglogin.LogLogoutTime = DateTime.Now;
                    loglogin.Remark = "2";
                    logloginmanager.Update(loglogin);
                }
            }
            log4net.LogManager.GetLogger(typeof(OnLineTest.BLL.common)).Info(((OnLineTest.Model.Users)Session["User"]).UserName + "成功退出。");
        }
        Session.Abandon();
    }
       
</script>

