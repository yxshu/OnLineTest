<%@ WebHandler Language="C#" Class="getChapterbyPaperCodeId" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.Model;
using OnLineTest.BLL;

public class getChapterbyPaperCodeId : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int PaperCodeId = -1;
        List<Chapter> list = null;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        if (Int32.TryParse(context.Request.Form["PaperCodeId"], out PaperCodeId) && PaperCodeId > 0)
        {
            list = new List<Chapter>();
            TextBookManager textbookmanager = new TextBookManager();
            ChapterManager chaptermanager = new ChapterManager();
            List<TextBook> listTB = textbookmanager.GetModelList("PaperCodeId=" + PaperCodeId);
            foreach (TextBook t in listTB)
            {
                List<Chapter> l = chaptermanager.GetModelList("TextBookId=" + t.TextBookId);
                if (l == null) continue;
                foreach (Chapter c in l)
                {
                    list.Add(c);
                }
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(list));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}