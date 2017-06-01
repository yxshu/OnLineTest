<%@ WebHandler Language="C#" Class="getUploadQuestionByQuestionId" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using OnLineTest.BLL;
public class getUploadQuestionByQuestionId : IHttpHandler, IReadOnlySessionState
{
    /// <summary>
    /// 根据QuestionId查询Question
    /// 其中要求 被查询的Question是当前用户上传的，才能被查询
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        Dictionary<string, object> result = null;//结果集
        JavaScriptSerializer serializer = new JavaScriptSerializer();//序列化工具
        if (context.Session["User"] != null)//判断用户是否登录
        {
            Users user = (Users)context.Session["User"];
            Question question = new Question();
            int id;
            if (Int32.TryParse(context.Request.Form["QuestionId"].ToString(), out id))
            {
                question.QuestionId = id;
            }
            else
            {
                question.QuestionId = -1;
            }
            if (question.QuestionId > 0)//获取正常的QuestinId
            {
                QuestionManager manager = new QuestionManager();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                try
                {
                    dic = manager.GetQuestionAndInstantiationById(question.QuestionId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if ((Int32)dic["UserId"] == user.UserId)
                {
                    result = dic;
                }
            }
        }
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