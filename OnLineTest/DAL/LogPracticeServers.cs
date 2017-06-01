using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:LogPracticeServers
	/// </summary>
	public partial class LogPracticeServers
	{
		public LogPracticeServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LogPracticeId", "LogPractice"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LogPracticeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from LogPractice");
			strSql.Append(" where LogPracticeId=@LogPracticeId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogPracticeId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogPracticeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.LogPractice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LogPractice(");
			strSql.Append("userId,QuestionId,LogPracticeTime,LogPracticeAnswer,LogPracetimeRemark)");
			strSql.Append(" values (");
			strSql.Append("@userId,@QuestionId,@LogPracticeTime,@LogPracticeAnswer,@LogPracetimeRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@QuestionId", SqlDbType.Int,4),
					new SqlParameter("@LogPracticeTime", SqlDbType.DateTime),
					new SqlParameter("@LogPracticeAnswer", SqlDbType.Int,4),
					new SqlParameter("@LogPracetimeRemark", SqlDbType.Text)};
			parameters[0].Value = model.userId;
			parameters[1].Value = model.QuestionId;
			parameters[2].Value = model.LogPracticeTime;
			parameters[3].Value = model.LogPracticeAnswer;
			parameters[4].Value = model.LogPracetimeRemark;

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
		public bool Update(OnLineTest.Model.LogPractice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LogPractice set ");
			strSql.Append("userId=@userId,");
			strSql.Append("QuestionId=@QuestionId,");
			strSql.Append("LogPracticeTime=@LogPracticeTime,");
			strSql.Append("LogPracticeAnswer=@LogPracticeAnswer,");
			strSql.Append("LogPracetimeRemark=@LogPracetimeRemark");
			strSql.Append(" where LogPracticeId=@LogPracticeId");
			SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@QuestionId", SqlDbType.Int,4),
					new SqlParameter("@LogPracticeTime", SqlDbType.DateTime),
					new SqlParameter("@LogPracticeAnswer", SqlDbType.Int,4),
					new SqlParameter("@LogPracetimeRemark", SqlDbType.Text),
					new SqlParameter("@LogPracticeId", SqlDbType.Int,4)};
			parameters[0].Value = model.userId;
			parameters[1].Value = model.QuestionId;
			parameters[2].Value = model.LogPracticeTime;
			parameters[3].Value = model.LogPracticeAnswer;
			parameters[4].Value = model.LogPracetimeRemark;
			parameters[5].Value = model.LogPracticeId;

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
		public bool Delete(int LogPracticeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogPractice ");
			strSql.Append(" where LogPracticeId=@LogPracticeId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogPracticeId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogPracticeId;

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
		public bool DeleteList(string LogPracticeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogPractice ");
			strSql.Append(" where LogPracticeId in ("+LogPracticeIdlist + ")  ");
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
		public OnLineTest.Model.LogPractice GetModel(int LogPracticeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LogPracticeId,userId,QuestionId,LogPracticeTime,LogPracticeAnswer,LogPracetimeRemark from LogPractice ");
			strSql.Append(" where LogPracticeId=@LogPracticeId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogPracticeId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogPracticeId;

			OnLineTest.Model.LogPractice model=new OnLineTest.Model.LogPractice();
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
		public OnLineTest.Model.LogPractice DataRowToModel(DataRow row)
		{
			OnLineTest.Model.LogPractice model=new OnLineTest.Model.LogPractice();
			if (row != null)
			{
				if(row["LogPracticeId"]!=null && row["LogPracticeId"].ToString()!="")
				{
					model.LogPracticeId=int.Parse(row["LogPracticeId"].ToString());
				}
				if(row["userId"]!=null && row["userId"].ToString()!="")
				{
					model.userId=int.Parse(row["userId"].ToString());
				}
				if(row["QuestionId"]!=null && row["QuestionId"].ToString()!="")
				{
					model.QuestionId=int.Parse(row["QuestionId"].ToString());
				}
				if(row["LogPracticeTime"]!=null && row["LogPracticeTime"].ToString()!="")
				{
					model.LogPracticeTime=DateTime.Parse(row["LogPracticeTime"].ToString());
				}
				if(row["LogPracticeAnswer"]!=null && row["LogPracticeAnswer"].ToString()!="")
				{
					model.LogPracticeAnswer=int.Parse(row["LogPracticeAnswer"].ToString());
				}
				if(row["LogPracetimeRemark"]!=null)
				{
					model.LogPracetimeRemark=row["LogPracetimeRemark"].ToString();
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
			strSql.Append("select LogPracticeId,userId,QuestionId,LogPracticeTime,LogPracticeAnswer,LogPracetimeRemark ");
			strSql.Append(" FROM LogPractice ");
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
			strSql.Append(" LogPracticeId,userId,QuestionId,LogPracticeTime,LogPracticeAnswer,LogPracetimeRemark ");
			strSql.Append(" FROM LogPractice ");
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
			strSql.Append("select count(1) FROM LogPractice ");
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
				strSql.Append("order by T.LogPracticeId desc");
			}
			strSql.Append(")AS Row, T.*  from LogPractice T ");
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
			parameters[0].Value = "LogPractice";
			parameters[1].Value = "LogPracticeId";
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

