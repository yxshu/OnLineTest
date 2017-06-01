<%@ WebHandler Language="C#" Class="UpdataVerify" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;

/// <summary>
/// 处理用户提交审核情况
/// 用户提交的参数有两个QuestionId=1, judge=true/false,如果judge=true审核通过，否则不通过
/// 采取的行动有两条：1、在question表中修改审核次数VerifyTime++
///                   2、在VerifyStatus表中添加一条记录
/// </summary>
public class UpdataVerify : IHttpHandler, IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        bool result = false;//最终返回的结果
        int QuestionId = -1;//接收数据QuestionId
        bool IsPass = false;//接收数据judge
        Users user = new Users();
        if (int.TryParse(context.Request.Form["QuestionId"], out QuestionId) && bool.TryParse(context.Request.Form["judge"], out IsPass) && context.Session["User"] != null)//取值成功
        {
            user = (Users)context.Session["User"];
            Question Question = new OnLineTest.Model.Question();
            QuestionManager Qmanager = new QuestionManager();
            if (QuestionId > 0 && Qmanager.Exists(QuestionId))
            {
                Question = Qmanager.GetModel(QuestionId);
                VerifyStatus verifystatus = new VerifyStatus();
                verifystatus.QuestionId = Question.QuestionId;
                verifystatus.UserId = user.UserId;
                verifystatus.VerifyTimes = Question.VerifyTimes + 1;
                verifystatus.IsPass = IsPass;
                //verifystatus.VerifyTime = DateTime.Now;//使用默认值
                VerifyStatusManager Vmanager = new VerifyStatusManager();
                if (Vmanager.Add(verifystatus, Question))
                {
                    result = true;
                }
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(result));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}