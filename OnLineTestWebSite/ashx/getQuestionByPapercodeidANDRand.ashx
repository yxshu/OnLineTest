<%@ WebHandler Language="C#" Class="getQuestionByPapercodeidANDRand" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.SessionState;
using System.Collections.Generic;
/// <summary>
/// 根据给定的papercodeid 随机抽取一条试题
/// 要求1、已经审核通过；2、没有被软删除
/// </summary>
public class getQuestionByPapercodeidANDRand : IHttpHandler, IReadOnlySessionState
{
    Question question = new Question();
    public void ProcessRequest(HttpContext context)
    {
        int papercodeid;
        if (!Int32.TryParse(context.Request.Form["papercodeid"], out papercodeid)) return;
        QuestionManager manager = new QuestionManager();
        if (manager.GetRecordCount("PaperCodeId=" + papercodeid) < 3) return;
        Users user = context.Session["User"] == null ? null : (Users)context.Session["User"];
        if (user == null) return;
        LogPractice logpractice = new LogPractice();
        StringBuilder sb = new StringBuilder();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<object> result = new List<object>();
        result.Add(manager.getQuestionByPapercodeidANDRand(papercodeid, user.UserId, out logpractice));
        result.Add(logpractice);
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