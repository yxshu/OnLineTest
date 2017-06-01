using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:LogTestServers
	/// </summary>
	public partial class LogTestServers
	{
		public LogTestServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LogTestId", "LogTest"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LogTestId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from LogTest");
			strSql.Append(" where LogTestId=@LogTestId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogTestId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.LogTest model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LogTest(");
			strSql.Append("UserId,LogTestStartTime,LogTestEndTime,PaperCodeId,DifficultyId,LogTestScore)");
			strSql.Append(" values (");
			strSql.Append("@UserId,@LogTestStartTime,@LogTestEndTime,@PaperCodeId,@DifficultyId,@LogTestScore)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@LogTestStartTime", SqlDbType.DateTime),
					new SqlParameter("@LogTestEndTime", SqlDbType.DateTime),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@DifficultyId", SqlDbType.Int,4),
					new SqlParameter("@LogTestScore", SqlDbType.Int,4)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.LogTestStartTime;
			parameters[2].Value = model.LogTestEndTime;
			parameters[3].Value = model.PaperCodeId;
			parameters[4].Value = model.DifficultyId;
			parameters[5].Value = model.LogTestScore;

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
		public bool Update(OnLineTest.Model.LogTest model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LogTest set ");
			strSql.Append("UserId=@UserId,");
			strSql.Append("LogTestStartTime=@LogTestStartTime,");
			strSql.Append("LogTestEndTime=@LogTestEndTime,");
			strSql.Append("PaperCodeId=@PaperCodeId,");
			strSql.Append("DifficultyId=@DifficultyId,");
			strSql.Append("LogTestScore=@LogTestScore");
			strSql.Append(" where LogTestId=@LogTestId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@LogTestStartTime", SqlDbType.DateTime),
					new SqlParameter("@LogTestEndTime", SqlDbType.DateTime),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@DifficultyId", SqlDbType.Int,4),
					new SqlParameter("@LogTestScore", SqlDbType.Int,4),
					new SqlParameter("@LogTestId", SqlDbType.Int,4)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.LogTestStartTime;
			parameters[2].Value = model.LogTestEndTime;
			parameters[3].Value = model.PaperCodeId;
			parameters[4].Value = model.DifficultyId;
			parameters[5].Value = model.LogTestScore;
			parameters[6].Value = model.LogTestId;

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
		public bool Delete(int LogTestId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogTest ");
			strSql.Append(" where LogTestId=@LogTestId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogTestId;

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
		public bool DeleteList(string LogTestIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogTest ");
			strSql.Append(" where LogTestId in ("+LogTestIdlist + ")  ");
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
		public OnLineTest.Model.LogTest GetModel(int LogTestId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LogTestId,UserId,LogTestStartTime,LogTestEndTime,PaperCodeId,DifficultyId,LogTestScore from LogTest ");
			strSql.Append(" where LogTestId=@LogTestId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogTestId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogTestId;

			OnLineTest.Model.LogTest model=new OnLineTest.Model.LogTest();
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
		public OnLineTest.Model.LogTest DataRowToModel(DataRow row)
		{
			OnLineTest.Model.LogTest model=new OnLineTest.Model.LogTest();
			if (row != null)
			{
				if(row["LogTestId"]!=null && row["LogTestId"].ToString()!="")
				{
					model.LogTestId=int.Parse(row["LogTestId"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["LogTestStartTime"]!=null && row["LogTestStartTime"].ToString()!="")
				{
					model.LogTestStartTime=DateTime.Parse(row["LogTestStartTime"].ToString());
				}
				if(row["LogTestEndTime"]!=null && row["LogTestEndTime"].ToString()!="")
				{
					model.LogTestEndTime=DateTime.Parse(row["LogTestEndTime"].ToString());
				}
				if(row["PaperCodeId"]!=null && row["PaperCodeId"].ToString()!="")
				{
					model.PaperCodeId=int.Parse(row["PaperCodeId"].ToString());
				}
				if(row["DifficultyId"]!=null && row["DifficultyId"].ToString()!="")
				{
					model.DifficultyId=int.Parse(row["DifficultyId"].ToString());
				}
				if(row["LogTestScore"]!=null && row["LogTestScore"].ToString()!="")
				{
					model.LogTestScore=int.Parse(row["LogTestScore"].ToString());
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
			strSql.Append("select LogTestId,UserId,LogTestStartTime,LogTestEndTime,PaperCodeId,DifficultyId,LogTestScore ");
			strSql.Append(" FROM LogTest ");
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
			strSql.Append(" LogTestId,UserId,LogTestStartTime,LogTestEndTime,PaperCodeId,DifficultyId,LogTestScore ");
			strSql.Append(" FROM LogTest ");
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
			strSql.Append("select count(1) FROM LogTest ");
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
				strSql.Append("order by T.LogTestId desc");
			}
			strSql.Append(")AS Row, T.*  from LogTest T ");
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
			parameters[0].Value = "LogTest";
			parameters[1].Value = "LogTestId";
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

