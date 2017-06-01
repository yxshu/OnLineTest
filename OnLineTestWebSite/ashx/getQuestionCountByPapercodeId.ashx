<%@ WebHandler Language="C#" Class="getQuestionCountByPapercodeId" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using OnLineTest.BLL;
public class getQuestionCountByPapercodeId : IHttpHandler
{
    /// <summary>
    /// 根据用户提交的papercodeid随机抽取试题
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        int papercodeid;
        if (!Int32.TryParse(context.Request.Form["papercodeid"], out papercodeid)) return;
        QuestionManager manager = new QuestionManager();
        context.Response.ContentType = "text/plain";
        context.Response.Write(new JavaScriptSerializer().Serialize(manager.GetRecordCount("PaperCodeId=" + papercodeid)));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}