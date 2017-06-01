<%@ WebHandler Language="C#" Class="GetCurrentUser" %>

using System;
using System.Web;
using OnLineTest.Model;
using System.Web.Script.Serialization;
using OnLineTest.BLL;
using System.Text;
using System.Collections.Generic;

public class GetCurrentUser : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    //获得MAIN页面中关于用户信息的所有内容
    public void ProcessRequest(HttpContext context)
    {
        List<object> list = new List<object>();
        StringBuilder stringbuilder = new StringBuilder();
        JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
        Users user = new Users();
        int shouru, zhichu, paiming, message_weidu, message_recerve, message_sended;
        UsersManager usersmanager = new UsersManager();
        UserScoreDetailManager userscoredetailmanager = new UserScoreDetailManager();
        UserRank userrank = new UserRank();
        UserRankManager userrankmanager = new UserRankManager();
        Message_tableManager messagemanager = new Message_tableManager();
        LogLogin loglogin = new LogLogin();
        LogLoginManager logloginmanager = new LogLoginManager();
        if (context.Session != null && context.Session["User"] != null)
        {
            user = (Users)context.Session["User"]; //得到当前登录用户实例 
        }
        else if (context.Request.Cookies["UserId"].Value != null)
        {
            user.UserId = Convert.ToInt32(context.Request.Cookies["UserId"].Value);
            user = usersmanager.GetModel(user.UserId);
        }
        else { user = null; }
        if (user != null)
        {
            shouru = userscoredetailmanager.shouru_total(user.UserId);
            zhichu = userscoredetailmanager.zhichu_total(user.UserId);
            paiming = usersmanager.paiming(user.UserId);
            userrank = userrankmanager.GetNextRank(user.UserId);
            if (userrank == null) {
                userrank = userrankmanager.GetModel(user.UserId);
            }
            message_weidu = messagemanager.NotReadMessageNum(user.UserId);
            message_recerve = messagemanager.recerveNum(user.UserId);
            message_sended = messagemanager.sendNum(user.UserId);
            loglogin = logloginmanager.GetCurrentLoglogin(user.UserId);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("shouru",shouru);
            dic.Add("zhichu", zhichu);
            dic.Add("paiming", paiming);
            dic.Add("message_recerve", message_recerve);
            dic.Add("message_sended", message_sended);
            dic.Add("message_weidu", message_weidu);
            list.Add(user);
            list.Add(dic);
            list.Add(userrank);
            list.Add(loglogin);
        }
        context.Response.Write(JavaScriptSerializer.Serialize(list));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}