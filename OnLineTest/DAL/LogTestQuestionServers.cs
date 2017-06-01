using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:LogTestQuestionServers
	/// </summary>
	public partial class LogTestQuestionServers
	{
		public LogTestQuestionServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LogTestQuestionId", "LogTestQuestion"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LogTestQuestionId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from LogTestQuestion");
			strSql.Append(" where LogTestQuestionId=@LogTestQuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestQuestionId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogTestQuestionId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.LogTestQuestion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LogTestQuestion(");
			strSql.Append("LogTestId,QuestionId,LogTestQuestionAnswer,LogTestQuestionRemark)");
			strSql.Append(" values (");
			strSql.Append("@LogTestId,@QuestionId,@LogTestQuestionAnswer,@LogTestQuestionRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestId", SqlDbType.Int,4),
					new SqlParameter("@QuestionId", SqlDbType.Int,4),
					new SqlParameter("@LogTestQuestionAnswer", SqlDbType.Int,4),
					new SqlParameter("@LogTestQuestionRemark", SqlDbType.Text)};
			parameters[0].Value = model.LogTestId;
			parameters[1].Value = model.QuestionId;
			parameters[2].Value = model.LogTestQuestionAnswer;
			parameters[3].Value = model.LogTestQuestionRemark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(OnLineTest.Model.LogTestQuestion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LogTestQuestion set ");
			strSql.Append("LogTestId=@LogTestId,");
			strSql.Append("QuestionId=@QuestionId,");
			strSql.Append("LogTestQuestionAnswer=@LogTestQuestionAnswer,");
			strSql.Append("LogTestQuestionRemark=@LogTestQuestionRemark");
			strSql.Append(" where LogTestQuestionId=@LogTestQuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestId", SqlDbType.Int,4),
					new SqlParameter("@QuestionId", SqlDbType.Int,4),
					new SqlParameter("@LogTestQuestionAnswer", SqlDbType.Int,4),
					new SqlParameter("@LogTestQuestionRemark", SqlDbType.Text),
					new SqlParameter("@LogTestQuestionId", SqlDbType.Int,4)};
			parameters[0].Value = model.LogTestId;
			parameters[1].Value = model.QuestionId;
			parameters[2].Value = model.LogTestQuestionAnswer;
			parameters[3].Value = model.LogTestQuestionRemark;
			parameters[4].Value = model.LogTestQuestionId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int LogTestQuestionId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogTestQuestion ");
			strSql.Append(" where LogTestQuestionId=@LogTestQuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestQuestionId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogTestQuestionId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string LogTestQuestionIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogTestQuestion ");
			strSql.Append(" where LogTestQuestionId in ("+LogTestQuestionIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public OnLineTest.Model.LogTestQuestion GetModel(int LogTestQuestionId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LogTestQuestionId,LogTestId,QuestionId,LogTestQuestionAnswer,LogTestQuestionRemark from LogTestQuestion ");
			strSql.Append(" where LogTestQuestionId=@LogTestQuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestQuestionId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogTestQuestionId;

			OnLineTest.Model.LogTestQuestion model=new OnLineTest.Model.LogTestQuestion();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public OnLineTest.Model.LogTestQuestion DataRowToModel(DataRow row)
		{
			OnLineTest.Model.LogTestQuestion model=new OnLineTest.Model.LogTestQuestion();
			if (row != null)
			{
				if(row["LogTestQuestionId"]!=null && row["LogTestQuestionId"].ToString()!="")
				{
					model.LogTestQuestionId=int.Parse(row["LogTestQuestionId"].ToString());
				}
				if(row["LogTestId"]!=null && row["LogTestId"].ToString()!="")
				{
					model.LogTestId=int.Parse(row["LogTestId"].ToString());
				}
				if(row["QuestionId"]!=null && row["QuestionId"].ToString()!="")
				{
					model.QuestionId=int.Parse(row["QuestionId"].ToString());
				}
				if(row["LogTestQuestionAnswer"]!=null && row["LogTestQuestionAnswer"].ToString()!="")
				{
					model.LogTestQuestionAnswer=int.Parse(row["LogTestQuestionAnswer"].ToString());
				}
				if(row["LogTestQuestionRemark"]!=null)
				{
					model.LogTestQuestionRemark=row["LogTestQuestionRemark"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LogTestQuestionId,LogTestId,QuestionId,LogTestQuestionAnswer,LogTestQuestionRemark ");
			strSql.Append(" FROM LogTestQuestion ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" LogTestQuestionId,LogTestId,QuestionId,LogTestQuestionAnswer,LogTestQuestionRemark ");
			strSql.Append(" FROM LogTestQuestion ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM LogTestQuestion ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.LogTestQuestionId desc");
			}
			strSql.Append(")AS Row, T.*  from LogTestQuestion T ");
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
			parameters[0].Value = "LogTestQuestion";
			parameters[1].Value = "LogTestQuestionId";
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

