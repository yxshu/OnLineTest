using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:SuggestionKeywordServers
    /// </summary>
    public partial class SuggestionKeywordServers
    {
        /// <summary>
        /// 获得前几行数据(降序)
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, string desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SuggestionKeywordsId,SuggestionKeywords,SuggestionKeywordsCreateTime,SuggestionKeywordsNum ");
            strSql.Append(" FROM SuggestionKeywords ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            strSql.Append("Desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据(降序)(带参数的)
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, params SqlParameter[] para)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SuggestionKeywordsId,SuggestionKeywords,SuggestionKeywordsCreateTime,SuggestionKeywordsNum ");
            strSql.Append(" FROM SuggestionKeywords ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), para);
        }

        /// <summary>
        /// 根据SuggestionKeywords判断是否存在，如果存在则返回SuggestionKeywordsId
        /// </summary>
        /// <param name="SuggestionKeywords">用户搜索的关键词</param>
        /// <param name="SuggestionKeywordsId">输出参数，如果存在，则返回id,否则返回-1</param>
        /// <returns>true则存在，false则不存在</returns>
        public bool Exists(string SuggestionKeywords, out int SuggestionKeywordsId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SuggestionKeywordsId  from SuggestionKeywords");
            strSql.Append(" where SuggestionKeywords=@SuggestionKeywords");
            SqlParameter[] parameters = {
					new SqlParameter("@SuggestionKeywords", SqlDbType.VarChar,50)
			};
            parameters[0].Value = SuggestionKeywords;
            object result = SqlHelper.ExecuteScalar(DbHelperMySQL.connectionString, CommandType.Text, strSql.ToString(), parameters);
            if (result != null)
            {
                return int.TryParse(result.ToString(), out SuggestionKeywordsId);
            }
            else
            {
                SuggestionKeywordsId = -1;
                return false;
            }

        }
    }
}

