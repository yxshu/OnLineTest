
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:LogLoginServers
	/// </summary>
	public partial class LogLoginServers
	{
		public LogLoginServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LogLoginId", "LogLogin"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LogLoginId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from LogLogin");
			strSql.Append(" where LogLoginId=@LogLoginId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogLoginId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogLoginId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.LogLogin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LogLogin(");
			strSql.Append("UserId,LogLoginTime,LogLogoutTime,LogLoginIp,LogLoginOperatiionSystem,LogLoginWebServerClient,Remark)");
			strSql.Append(" values (");
			strSql.Append("@UserId,@LogLoginTime,@LogLogoutTime,@LogLoginIp,@LogLoginOperatiionSystem,@LogLoginWebServerClient,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@LogLoginTime", SqlDbType.DateTime),
					new SqlParameter("@LogLogoutTime", SqlDbType.DateTime),
					new SqlParameter("@LogLoginIp", SqlDbType.VarChar,20),
					new SqlParameter("@LogLoginOperatiionSystem", SqlDbType.NVarChar,200),
					new SqlParameter("@LogLoginWebServerClient", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.LogLoginTime;
			parameters[2].Value = model.LogLogoutTime;
			parameters[3].Value = model.LogLoginIp;
			parameters[4].Value = model.LogLoginOperatiionSystem;
			parameters[5].Value = model.LogLoginWebServerClient;
			parameters[6].Value = model.Remark;

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
		public bool Update(OnLineTest.Model.LogLogin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LogLogin set ");
			strSql.Append("UserId=@UserId,");
			strSql.Append("LogLoginTime=@LogLoginTime,");
			strSql.Append("LogLogoutTime=@LogLogoutTime,");
			strSql.Append("LogLoginIp=@LogLoginIp,");
			strSql.Append("LogLoginOperatiionSystem=@LogLoginOperatiionSystem,");
			strSql.Append("LogLoginWebServerClient=@LogLoginWebServerClient,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where LogLoginId=@LogLoginId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@LogLoginTime", SqlDbType.DateTime),
					new SqlParameter("@LogLogoutTime", SqlDbType.DateTime),
					new SqlParameter("@LogLoginIp", SqlDbType.VarChar,20),
					new SqlParameter("@LogLoginOperatiionSystem", SqlDbType.NVarChar,200),
					new SqlParameter("@LogLoginWebServerClient", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@LogLoginId", SqlDbType.Int,4)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.LogLoginTime;
			parameters[2].Value = model.LogLogoutTime;
			parameters[3].Value = model.LogLoginIp;
			parameters[4].Value = model.LogLoginOperatiionSystem;
			parameters[5].Value = model.LogLoginWebServerClient;
			parameters[6].Value = model.Remark;
			parameters[7].Value = model.LogLoginId;

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
		public bool Delete(int LogLoginId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogLogin ");
			strSql.Append(" where LogLoginId=@LogLoginId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogLoginId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogLoginId;

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
		public bool DeleteList(string LogLoginIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogLogin ");
			strSql.Append(" where LogLoginId in ("+LogLoginIdlist + ")  ");
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
		public OnLineTest.Model.LogLogin GetModel(int LogLoginId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LogLoginId,UserId,LogLoginTime,LogLogoutTime,LogLoginIp,LogLoginOperatiionSystem,LogLoginWebServerClient,Remark from LogLogin ");
			strSql.Append(" where LogLoginId=@LogLoginId");
			SqlParameter[] parameters = {
					new SqlParameter("@LogLoginId", SqlDbType.Int,4)
			};
			parameters[0].Value = LogLoginId;

			OnLineTest.Model.LogLogin model=new OnLineTest.Model.LogLogin();
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
		public OnLineTest.Model.LogLogin DataRowToModel(DataRow row)
		{
			OnLineTest.Model.LogLogin model=new OnLineTest.Model.LogLogin();
			if (row != null)
			{
				if(row["LogLoginId"]!=null && row["LogLoginId"].ToString()!="")
				{
					model.LogLoginId=int.Parse(row["LogLoginId"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["LogLoginTime"]!=null && row["LogLoginTime"].ToString()!="")
				{
					model.LogLoginTime=DateTime.Parse(row["LogLoginTime"].ToString());
				}
				if(row["LogLogoutTime"]!=null && row["LogLogoutTime"].ToString()!="")
				{
					model.LogLogoutTime=DateTime.Parse(row["LogLogoutTime"].ToString());
				}
				if(row["LogLoginIp"]!=null)
				{
					model.LogLoginIp=row["LogLoginIp"].ToString();
				}
				if(row["LogLoginOperatiionSystem"]!=null)
				{
					model.LogLoginOperatiionSystem=row["LogLoginOperatiionSystem"].ToString();
				}
				if(row["LogLoginWebServerClient"]!=null)
				{
					model.LogLoginWebServerClient=row["LogLoginWebServerClient"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
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
			strSql.Append("select LogLoginId,UserId,LogLoginTime,LogLogoutTime,LogLoginIp,LogLoginOperatiionSystem,LogLoginWebServerClient,Remark ");
			strSql.Append(" FROM LogLogin ");
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
			strSql.Append(" LogLoginId,UserId,LogLoginTime,LogLogoutTime,LogLoginIp,LogLoginOperatiionSystem,LogLoginWebServerClient,Remark ");
			strSql.Append(" FROM LogLogin ");
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
			strSql.Append("select count(1) FROM LogLogin ");
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
				strSql.Append("order by T.LogLoginId desc");
			}
			strSql.Append(")AS Row, T.*  from LogLogin T ");
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
			parameters[0].Value = "LogLogin";
			parameters[1].Value = "LogLoginId";
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

