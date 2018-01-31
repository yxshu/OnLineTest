<%@ WebHandler Language="C#" Class="getChapterbySubjectName" %>

using System;
using System.Web;

public class getChapterbySubjectName : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string subjectname = context.Request.Form["subjectname"].ToString();// 获得要查询的科目名称
        //select
        //s.SubjectId,s.SubjectName,p.PaperCode,p.ChineseName,t.TextBookId,t.TextBookName,c.ChapterName,c2.ChapterName
        //from Subject as s  
        //right join PaperCodes as p on s.SubjectId=p.SubjectId 
        //right join TextBook as t on p.PaperCodeId=t.PaperCodeId
        //right join Chapter as c on t.TextBookId=c.TextBookId
        //right join Chapter as c2 on c.ChapterId=c2.ChapterParentNo
        //where s.SubjectName='船舶操纵与避碰' and c.ChapterDeep=0
        //order by c.ChapterId
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}