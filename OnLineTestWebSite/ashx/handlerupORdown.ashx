<%@ WebHandler Language="C#" Class="handlerupORdown" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using OnLineTest.BLL;

/// <summary>
/// 处理用户提交过来的顶踩事件
/// 传递的参数包括：{ "questionid": questionid, "upORdown": UOD }，UOD=1表示顶 uod=-1表示踩
/// update Question set IsSupported+=1 where QuestionId=1
/// 要求，同一个用户要10分钟内只能顶踩一次，用session来记录
/// </summary>
public class handlerupORdown : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool result = false;
        int questionid, upORdown;
        if (!Int32.TryParse(context.Request.Form["questionid"], out questionid)) return;//取用户提交的questionid
        if (!Int32.TryParse(context.Request.Form["upORdown"], out upORdown)) return;//取用户提交的值
        if (questionid > 0 && upORdown == 1 || upORdown == -1)
        {
            QuestionManager manager = new QuestionManager();
            if (manager.Exists(questionid))
            {
                int offminutes;//计算当前与上次修改的时候分钟差
                if (context.Session["upORdownTIME"] != null)
                {
                    offminutes = (DateTime.Now - (DateTime)context.Session["upORdownTIME"]).Minutes;
                    if (offminutes > 10)
                    {
                        context.Session["upORdownTIME"] = DateTime.Now;
                    }
                }
                else
                {
                    offminutes = int.MaxValue;
                    context.Session.Add("upORdownTIME", DateTime.Now);
                }
                if (offminutes > 10)
                {
                    result = manager.handlerupORdown(questionid, upORdown);
                }
            }
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