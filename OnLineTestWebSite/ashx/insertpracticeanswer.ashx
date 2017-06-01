<%@ WebHandler Language="C#" Class="insertpracticeanswer" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
/// <summary>
/// 根据用户提交的两个数据{ "LogPracticeId": currentlogpracticeid, "curranswer": curranswer } 插入用户的答案
/// </summary>
public class insertpracticeanswer : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool result=false ;
        int logpracticeid = -1;
        int answer = -1; Users user = null;
        if (context.Session["User"] != null)
        {

            user = (Users)context.Session["User"];
        }
        if (!Int32.TryParse(context.Request.Form["LogPracticeId"], out logpracticeid)) result = false;
        if (!Int32.TryParse(context.Request.Form["curranswer"], out answer)) result = false;
        LogPracticeManager manager = new LogPracticeManager();
        if (manager.Exists(logpracticeid) && answer == 1 || answer == 2 || answer == 3 || answer == 4)
        {
            LogPractice log = manager.GetModel(logpracticeid);
            if (log.userId == user.UserId)
            {
                log.LogPracticeAnswer = answer;
                result = manager.Update(log);
            }
        }
        else
        {
            result = false;
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(new JavaScriptSerializer().Serialize(result));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}