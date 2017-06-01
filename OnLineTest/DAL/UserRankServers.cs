using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:UserRankServers
	/// </summary>
	public partial class UserRankServers
	{
		public UserRankServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserRankId", "UserRank"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserRankName,int UserRankId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from UserRank");
			strSql.Append(" where UserRankName=@UserRankName and UserRankId=@UserRankId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserRankName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserRankId", SqlDbType.Int,4)			};
			parameters[0].Value = UserRankName;
			parameters[1].Value = UserRankId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.UserRank model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UserRank(");
			strSql.Append("UserRankName,MinScore,MaxScore)");
			strSql.Append(" values (");
			strSql.Append("@UserRankName,@MinScore,@MaxScore)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserRankName", SqlDbType.NVarChar,20),
					new SqlParameter("@MinScore", SqlDbType.Int,4),
					new SqlParameter("@MaxScore", SqlDbType.Int,4)};
			parameters[0].Value = model.UserRankName;
			parameters[1].Value = model.MinScore;
			parameters[2].Value = model.MaxScore;

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
		public bool Update(OnLineTest.Model.UserRank model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UserRank set ");
			strSql.Append("MinScore=@MinScore,");
			strSql.Append("MaxScore=@MaxScore");
			strSql.Append(" where UserRankId=@UserRankId");
			SqlParameter[] parameters = {
					new SqlParameter("@MinScore", SqlDbType.Int,4),
					new SqlParameter("@MaxScore", SqlDbType.Int,4),
					new SqlParameter("@UserRankId", SqlDbType.Int,4),
					new SqlParameter("@UserRankName", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.MinScore;
			parameters[1].Value = model.MaxScore;
			parameters[2].Value = model.UserRankId;
			parameters[3].Value = model.UserRankName;

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
		public bool Delete(int UserRankId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserRank ");
			strSql.Append(" where UserRankId=@UserRankId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserRankId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserRankId;

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
		public bool Delete(string UserRankName,int UserRankId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserRank ");
			strSql.Append(" where UserRankName=@UserRankName and UserRankId=@UserRankId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserRankName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserRankId", SqlDbType.Int,4)			};
			parameters[0].Value = UserRankName;
			parameters[1].Value = UserRankId;

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
		public bool DeleteList(string UserRankIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserRank ");
			strSql.Append(" where UserRankId in ("+UserRankIdlist + ")  ");
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
		public OnLineTest.Model.UserRank GetModel(int UserRankId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserRankId,UserRankName,MinScore,MaxScore from UserRank ");
			strSql.Append(" where UserRankId=@UserRankId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserRankId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserRankId;

			OnLineTest.Model.UserRank model=new OnLineTest.Model.UserRank();
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
		public OnLineTest.Model.UserRank DataRowToModel(DataRow row)
		{
			OnLineTest.Model.UserRank model=new OnLineTest.Model.UserRank();
			if (row != null)
			{
				if(row["UserRankId"]!=null && row["UserRankId"].ToString()!="")
				{
					model.UserRankId=int.Parse(row["UserRankId"].ToString());
				}
				if(row["UserRankName"]!=null)
				{
					model.UserRankName=row["UserRankName"].ToString();
				}
				if(row["MinScore"]!=null && row["MinScore"].ToString()!="")
				{
					model.MinScore=int.Parse(row["MinScore"].ToString());
				}
				if(row["MaxScore"]!=null && row["MaxScore"].ToString()!="")
				{
					model.MaxScore=int.Parse(row["MaxScore"].ToString());
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
			strSql.Append("select UserRankId,UserRankName,MinScore,MaxScore ");
			strSql.Append(" FROM UserRank ");
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
			strSql.Append(" UserRankId,UserRankName,MinScore,MaxScore ");
			strSql.Append(" FROM UserRank ");
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
			strSql.Append("select count(1) FROM UserRank ");
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
				strSql.Append("order by T.UserRankId desc");
			}
			strSql.Append(")AS Row, T.*  from UserRank T ");
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
			parameters[0].Value = "UserRank";
			parameters[1].Value = "UserRankId";
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

