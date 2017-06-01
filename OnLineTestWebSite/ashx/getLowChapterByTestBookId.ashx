<%@ WebHandler Language="C#" Class="getLowChapterByTestBookId" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.Model;
using OnLineTest.BLL;
/// <summary>
/// 根据用户提交 的Textbookid 获取所有的底层chapter
/// </summary>
public class getLowChapterByTestBookId : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Int32 TextBookId = -1;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Chapter> result = new List<Chapter>();
        if (Int32.TryParse(context.Request.Form["TextBookId"], out TextBookId) && TextBookId > 0)
        {

            ChapterManager manager = new ChapterManager();
            result = manager.getAllLowChapterByTestbookId(TextBookId);
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