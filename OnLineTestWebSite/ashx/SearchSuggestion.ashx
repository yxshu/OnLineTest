<%@ WebHandler Language="C#" Class="SearchSuggestion" %>

using System;
using System.Web;
using OnLineTest.BLL;
using OnLineTest.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
public class SearchSuggestion : IHttpHandler
{
    //处理搜索建议
    public void ProcessRequest(HttpContext context)
    {
        string[] keywords=null;
        string term = context.Request["term"].Trim();
        List<SuggestionKeyword> list = new List<SuggestionKeyword>();
        SuggestionKeywordManager skm = new SuggestionKeywordManager();
        SqlParameter[] parameters = {
                    new SqlParameter("@term", System.Data.SqlDbType.VarChar, 100),//注意varchar100,只能存50个字 符，nvarchar100,可以存100个字符
                    };
        parameters[0].Value = term.Length > 50 ? "%" + term.Substring(0, 50) + "%" : "%" + term + "%";
        DataTable dt = skm.GetList(10, "SuggestionKeywords like @term", "SuggestionKeywordsNum desc", parameters).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            list = skm.DataTableToList(dt);
            keywords = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                keywords[i] = list[i].SuggestionKeywords;
            }
        }
        context.Response.ContentType = "text/plain";
        if (keywords != null)
        {
            context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(keywords));
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}