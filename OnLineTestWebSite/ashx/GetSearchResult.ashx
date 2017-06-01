<%@ WebHandler Language="C#" Class="GetSearchResult" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Lucene.Net.Documents;
using OnLineTest.BLL;
using OnLineTest.Model;

public class GetSearchResult : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string Keyword, Subject;
        List<object> ResultList = new List<object>();
        Int32 StarNum = 0, TotalNum = 10;
        Type type = null;
        if (!string.IsNullOrEmpty(context.Request.Form["Keyword"].Trim()))
        {
            Keyword = context.Request.Form["Keyword"].Trim();
            Subject = !string.IsNullOrEmpty(context.Request.Form["Subject"].Trim()) ? context.Request.Form["Subject"].Trim() : "Question";
            Int32.TryParse(context.Request.Form["StarNum"].Trim(), out StarNum);
            Int32.TryParse(context.Request.Form["TotalNum"].Trim(), out TotalNum);
            DirectoryInfo directoryinfo = new DirectoryInfo(ConfigurationManager.AppSettings["CreateIndexDirectionPath"]); //得到索引存放的路径
            List<Document> list = new List<Document>(); //用于存储查询结果的     
            TimeSpan costtime = TimeSpan.Zero;//搜索所花费的时间
            int countnum = Int32.MinValue;//搜索结果中所包含的数据量 
            bool isSuccess = true; //是否查询成功的标志

            if (HttpRuntime.Cache["search-" + Subject + "-" + Keyword] != null)//先从缓存里面取
            {
                object[] obj = (object[])HttpRuntime.Cache["search-" + Subject + "-" + Keyword];
                list = (List<Document>)obj[0];
                costtime = (TimeSpan)obj[1];
                countnum = (int)obj[2];
                type = (Type)obj[3];
            }
            else  //如果缓存中没有，则执行查询
            {
                type = (Subject == "Question") ? typeof(Question) : typeof(Comment);
                list.AddRange(common.Query(type, Keyword, directoryinfo, out costtime, out  countnum, out isSuccess));
                HttpRuntime.Cache.Insert("search-" + Subject + "-" + Keyword, new object[] { list, costtime, countnum ,type}, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                if (!string.IsNullOrEmpty(Keyword.Trim()))
                {
                    common.UpdataOrAddSuggestionKeyword(Keyword);//尝试更新或者新增用户搜索关键词
                }
            }
            //查询数据成功，开始处理生成的数据
            if (isSuccess && list != null && list.Count > 0)
            {
                ResultList.Add(costtime);
                ResultList.Add(countnum);
                //查询的是试题
                if (type == typeof(Question))
                {
                    ResultList.Add(typeof(Question).Name);
                    QuestionManager questionmanager = new QuestionManager();
                    for (int i = StarNum; i < ((StarNum + TotalNum > list.Count) ? list.Count : StarNum + TotalNum); i++)
                    {
                        int QuestionId;
                        Int32.TryParse(((Document)list[i]).Get("QuestionId"), out QuestionId);//尝试得到试题的ID
                        Question q = (Question)common.HighLight(questionmanager.GetModel(QuestionId), Keyword);
                        q.QuestionId = i + 1;  //此处有意改变id，以方便前台显示时按序编号
                        if (q != null)
                        {
                            ResultList.Add(q);
                        }
                    }
                }

                else
                {  //查询的是评论
                    ResultList.Add(typeof(Comment).Name);
                    CommentManager commentmanager = new CommentManager();
                    for (int i = StarNum; i < ((StarNum + TotalNum > list.Count) ? list.Count : StarNum + TotalNum); i++)
                    {
                        int CommentId;
                        Int32.TryParse(((Document)list[i]).Get("CommentId"), out CommentId);
                        Comment c = commentmanager.GetModel(CommentId);
                        c.CommentId = i + 1;
                        if (c != null)
                        {
                            ResultList.Add(c);
                        }
                    }
                }
            }
            //查询数据失败
            else
            {
                ResultList = null;
            }
        }
        else
        {
            //没有正确提交数据
            ResultList = null;

        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ResultList));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}