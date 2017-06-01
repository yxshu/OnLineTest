<%@ WebHandler Language="C#" Class="GetChapterByTextBookId" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.Model;
using OnLineTest.BLL;
/// <summary>
/// //根据选择的textbook 异步加载 chapter
/// </summary>
public class GetChapterByTextBookId : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Int32 TextBookId = -1;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Chapter> result = new List<Chapter>();
        if (Int32.TryParse(context.Request.Form["TextBookId"], out TextBookId) && TextBookId > 0)
        {
            ChapterManager manager = new ChapterManager();
            try
            {
                result = manager.getStructionChapterModel_by_TextBookId(TextBookId);
            }
            catch (Exception ex)
            {
                throw ex;
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