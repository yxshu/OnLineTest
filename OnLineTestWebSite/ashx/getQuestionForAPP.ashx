<%@ WebHandler Language="C#" Class="getQuestionForAPP" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
public class getQuestionForAPP : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        /*
        hashMap.put("paperCodeid", Integer.toString(paperCode.getPapercodeid()));
        hashMap.put("subjectid", Integer.toString(subject.getSubjectid()));
        hashMap.put("chapterid", Integer.toString(chapter.getChapterid()));
        hashMap.put("userid", Integer.toString(user.getUserid()));
        hashMap.put("pattern", pattern);
        hashMap.put("currentid", Integer.toString(currentid));
         */
        PaperCodes PaperCode = new PaperCodes();
        Subject Subject = new Subject();
        Chapter Chapter = new Chapter();
        Users User = new Users();
        string Pattern = string.Empty;
        string LastOrNext = string.Empty;
        int CurrentId = 0;
        Question question = null;
        UsersManager usermanager = new UsersManager();
        PaperCodesManager papercodemanager = new PaperCodesManager();
        QuestionManager questionmanager = new QuestionManager();
        //初始化参数
        if (!string.IsNullOrEmpty(context.Request.Form["paperCodeid"]))
        {
            PaperCode.PaperCodeId = Int32.Parse(context.Request.Form["paperCodeid"]);
        }
        if (!string.IsNullOrEmpty(context.Request.Form["subjectid"]))
        {
            Subject.SubjectId = Int32.Parse(context.Request.Form["subjectid"]);
        }
        if (!string.IsNullOrEmpty(context.Request.Form["chapterid"]))
        {
            Chapter.ChapterId = Int32.Parse(context.Request.Form["chapterid"]);
        }
        if (!string.IsNullOrEmpty(context.Request.Form["userid"]))
        {
            User.UserId = Int32.Parse(context.Request.Form["userid"]);
        }
        if (!string.IsNullOrEmpty(context.Request.Form["pattern"]))
        {
            Pattern = context.Request.Form["pattern"];//byorder 顺序练习   byrandom随机练习
        }
        if (!string.IsNullOrEmpty(context.Request.Form["lastornext"]))
        {
            LastOrNext = context.Request.Form["lastornext"];
        }
        if (!string.IsNullOrEmpty(context.Request.Form["currentid"]))
        {
            CurrentId = Int32.Parse(context.Request.Form["currentid"]);
        }
        if (usermanager.Exists(User.UserId) && papercodemanager.Exists(PaperCode.PaperCodeId))
        {
            switch (Pattern)
            {
                case "byorder"://顺序练习

                    //表示所有章节
                    if (Chapter.ChapterId == -1)
                    {
                        //上一题
                        //*select * from Question where QuestionId = (select top 1 QuestionId from Question where QuestionId < 20 and PaperCodeId=2  order by QuestionId desc )
                        if (string.Equals(LastOrNext, "last"))
                        {
                            string strWhere = "QuestionId = (select top 1 QuestionId from Question where QuestionId < " + CurrentId + " and PaperCodeId=" + PaperCode.PaperCodeId + "  order by QuestionId desc )";
                            question = questionmanager.GetModel(strWhere);
                        }
                        //下一题
                        //select * from Question where QuestionId = (select top 1 QuestionId from Question where QuestionId > 20 and PaperCodeId=2  order by QuestionId)

                        else if (string.Equals(LastOrNext, "next"))
                        {
                            string strWhere = "QuestionId = (select top 1 QuestionId from Question where QuestionId > " + CurrentId + " and PaperCodeId=" + PaperCode.PaperCodeId + "  order by QuestionId)";
                            question = questionmanager.GetModel(strWhere);
                        }
                    }
                    //固定章节
                    else if (new ChapterManager().Exists(Chapter.ChapterId))
                    {
                        //上一题
                        //select * from Question where QuestionId = (select top 1 QuestionId from Question where QuestionId < 20 and ChapterId=2  order by QuestionId desc )
                        if (string.Equals(LastOrNext, "last"))
                        {
                            string strWhere = "QuestionId = (select top 1 QuestionId from Question where QuestionId < " + CurrentId + " and ChapterId=" + Chapter.ChapterId + "  order by QuestionId desc )";
                            question = questionmanager.GetModel(strWhere);
                        }
                        //下一题
                        //select * from Question where QuestionId = (select top 1 QuestionId from Question where QuestionId > 20 and ChapterId=2  order by QuestionId)
                        else if (string.Equals(LastOrNext, "next"))
                        {
                            string strWhere = "QuestionId = (select top 1 QuestionId from Question where QuestionId >" + CurrentId + " and ChapterId=" + Chapter.ChapterId + "  order by QuestionId)";
                            question = questionmanager.GetModel(strWhere);
                        }
                    }
                    break;
                case "byrandom": //随机练习
                    //表示所有章节
                    //select TOP 1 * FROM Question where PaperCodeId=5 ORDER BY NEWID() 
                    if (Chapter.ChapterId == -1)
                    {
                        string strWhere = "PaperCodeId=" + PaperCode.PaperCodeId;
                        string orderby = "NEWID()";
                        question = questionmanager.GetModel(strWhere, orderby);
                    }
                    //固定章节
                    //select TOP 1 * FROM Question where ChapterId=2 ORDER BY NEWID()
                    else if (new ChapterManager().Exists(Chapter.ChapterId))
                    {
                        string strWhere = " ChapterId=" + Chapter.ChapterId;
                        string orderby = "NEWID()";
                        question = questionmanager.GetModel(strWhere, orderby);
                    }
                    break;
                default: break;
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(question));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}