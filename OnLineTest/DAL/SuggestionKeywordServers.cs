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
        public SuggestionKeywordServers()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SuggestionKeywordsId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SuggestionKeywords");
            strSql.Append(" where SuggestionKeywordsId=@SuggestionKeywordsId");
            SqlParameter[] parameters = {
					new SqlParameter("@SuggestionKeywordsId", SqlDbType.Int,4)
			};
            parameters[0].Value = SuggestionKeywordsId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OnLineTest.Model.SuggestionKeyword model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SuggestionKeywords(");
            strSql.Append("SuggestionKeywords,SuggestionKeywordsCreateTime,SuggestionKeywordsNum)");
            strSql.Append(" values (");
            strSql.Append("@SuggestionKeywords,@SuggestionKeywordsCreateTime,@SuggestionKeywordsNum)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SuggestionKeywords", SqlDbType.VarChar,50),
					new SqlParameter("@SuggestionKeywordsCreateTime", SqlDbType.DateTime),
					new SqlParameter("@SuggestionKeywordsNum", SqlDbType.Int,4)};
            parameters[0].Value = model.SuggestionKeywords;
            parameters[1].Value = model.SuggestionKeywordsCreateTime;
            parameters[2].Value = model.SuggestionKeywordsNum;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(OnLineTest.Model.SuggestionKeyword model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SuggestionKeywords set ");
            strSql.Append("SuggestionKeywords=@SuggestionKeywords,");
            strSql.Append("SuggestionKeywordsCreateTime=@SuggestionKeywordsCreateTime,");
            strSql.Append("SuggestionKeywordsNum=@SuggestionKeywordsNum");
            strSql.Append(" where SuggestionKeywordsId=@SuggestionKeywordsId");
            SqlParameter[] parameters = {
					new SqlParameter("@SuggestionKeywords", SqlDbType.VarChar,50),
					new SqlParameter("@SuggestionKeywordsCreateTime", SqlDbType.DateTime),
					new SqlParameter("@SuggestionKeywordsNum", SqlDbType.Int,4),
					new SqlParameter("@SuggestionKeywordsId", SqlDbType.Int,4)};
            parameters[0].Value = model.SuggestionKeywords;
            parameters[1].Value = model.SuggestionKeywordsCreateTime;
            parameters[2].Value = model.SuggestionKeywordsNum;
            parameters[3].Value = model.SuggestionKeywordsId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int SuggestionKeywordsId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SuggestionKeywords ");
            strSql.Append(" where SuggestionKeywordsId=@SuggestionKeywordsId");
            SqlParameter[] parameters = {
					new SqlParameter("@SuggestionKeywordsId", SqlDbType.Int,4)
			};
            parameters[0].Value = SuggestionKeywordsId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string SuggestionKeywordsIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SuggestionKeywords ");
            strSql.Append(" where SuggestionKeywordsId in (" + SuggestionKeywordsIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OnLineTest.Model.SuggestionKeyword GetModel(int SuggestionKeywordsId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SuggestionKeywordsId,SuggestionKeywords,SuggestionKeywordsCreateTime,SuggestionKeywordsNum from SuggestionKeywords ");
            strSql.Append(" where SuggestionKeywordsId=@SuggestionKeywordsId");
            SqlParameter[] parameters = {
					new SqlParameter("@SuggestionKeywordsId", SqlDbType.Int,4)
			};
            parameters[0].Value = SuggestionKeywordsId;

            OnLineTest.Model.SuggestionKeyword model = new OnLineTest.Model.SuggestionKeyword();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OnLineTest.Model.SuggestionKeyword DataRowToModel(DataRow row)
        {
            OnLineTest.Model.SuggestionKeyword model = new OnLineTest.Model.SuggestionKeyword();
            if (row != null)
            {
                if (row["SuggestionKeywordsId"] != null && row["SuggestionKeywordsId"].ToString() != "")
                {
                    model.SuggestionKeywordsId = int.Parse(row["SuggestionKeywordsId"].ToString());
                }
                if (row["SuggestionKeywords"] != null)
                {
                    model.SuggestionKeywords = row["SuggestionKeywords"].ToString();
                }
                if (row["SuggestionKeywordsCreateTime"] != null && row["SuggestionKeywordsCreateTime"].ToString() != "")
                {
                    model.SuggestionKeywordsCreateTime = DateTime.Parse(row["SuggestionKeywordsCreateTime"].ToString());
                }
                if (row["SuggestionKeywordsNum"] != null && row["SuggestionKeywordsNum"].ToString() != "")
                {
                    model.SuggestionKeywordsNum = int.Parse(row["SuggestionKeywordsNum"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SuggestionKeywordsId,SuggestionKeywords,SuggestionKeywordsCreateTime,SuggestionKeywordsNum ");
            strSql.Append(" FROM SuggestionKeywords ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据(升序)
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
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
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SuggestionKeywords ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.SuggestionKeywordsId desc");
            }
            strSql.Append(")AS Row, T.*  from SuggestionKeywords T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "SuggestionKeywords";
            parameters[1].Value = "SuggestionKeywordsId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

