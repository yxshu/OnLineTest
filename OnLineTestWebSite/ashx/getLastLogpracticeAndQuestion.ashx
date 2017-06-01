<%@ WebHandler Language="C#" Class="getLastLogpracticeAndQuestion" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using OnLineTest.BLL;

public class getLastLogpracticeAndQuestion : IHttpHandler, IReadOnlySessionState
{
    /// <summary>
    /// 根据用户提交的  LogPracticeId  获取logpractice并实例化其中的 question
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        int logpracticeid = -1;
        if (!Int32.TryParse(context.Request.Form["LogPracticeId"], out logpracticeid)) return;
        Users user = new Users();
        if (context.Session["User"] != null)
            user = (Users)context.Session["User"];
        LogPractice logpractice = new LogPractice();
        LogPracticeManager logpracticemanager = new LogPracticeManager();
        if (logpracticemanager.Exists(logpracticeid))
        {
            //这里是重点，这里给的参数是当前的LOGPRACTICEID，要求获取上一个ID
            logpractice = logpracticemanager.getLastModel(logpracticeid, user.UserId);//.GetModel(logpracticeid);
            if (logpractice == null || logpractice.userId != user.UserId)
                return;
        }
        else
        {
            return;
        }
        Dictionary<string, object> questionbyinstance = new Dictionary<string, object>();
        QuestionManager questionmanager = new QuestionManager();
        if (questionmanager.Exists(logpractice.QuestionId))
            questionbyinstance = questionmanager.GetQuestionAndInstantiationById(logpractice.QuestionId);
        else return;
        List<object> list = new List<object>();//用于返回的结果
        list.Add(questionbyinstance);
        list.Add(logpractice);
        context.Response.ContentType = "text/plain";
        context.Response.Write(new JavaScriptSerializer().Serialize(list));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}