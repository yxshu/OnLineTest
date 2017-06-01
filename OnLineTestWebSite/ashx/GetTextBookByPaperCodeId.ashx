<%@ WebHandler Language="C#" Class="GetTextBookByPaperCodeId" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using OnLineTest.BLL;
/// <summary>
/// 参考教材：根据选择的Papercodeid异步加载textbook,
/// </summary>
public class GetTextBookByPaperCodeId : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Int32 PaperCodeId = -1;
        List<TextBook> result = new List<TextBook>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        if (Int32.TryParse(context.Request.Form["PaperCodeId"], out PaperCodeId) && PaperCodeId > 0)
        {
            TextBookManager manager = new TextBookManager();
            result = manager.GetModelList("PaperCodeId=" + PaperCodeId);
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