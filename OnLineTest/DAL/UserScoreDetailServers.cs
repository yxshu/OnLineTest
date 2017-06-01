using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:UserScoreDetailServers
	/// </summary>
	public partial class UserScoreDetailServers
	{
		public UserScoreDetailServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserScoreDetailId", "UserScoreDetail"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserScoreDetailId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from UserScoreDetail");
			strSql.Append(" where UserScoreDetailId=@UserScoreDetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserScoreDetailId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserScoreDetailId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.UserScoreDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UserScoreDetail(");
			strSql.Append("UserId,UserAuthorityId,UserScoreDetailTime)");
			strSql.Append(" values (");
			strSql.Append("@UserId,@UserAuthorityId,@UserScoreDetailTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserAuthorityId", SqlDbType.Int,4),
					new SqlParameter("@UserScoreDetailTime", SqlDbType.DateTime)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.UserAuthorityId;
			parameters[2].Value = model.UserScoreDetailTime;

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
		public bool Update(OnLineTest.Model.UserScoreDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UserScoreDetail set ");
			strSql.Append("UserId=@UserId,");
			strSql.Append("UserAuthorityId=@UserAuthorityId,");
			strSql.Append("UserScoreDetailTime=@UserScoreDetailTime");
			strSql.Append(" where UserScoreDetailId=@UserScoreDetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserAuthorityId", SqlDbType.Int,4),
					new SqlParameter("@UserScoreDetailTime", SqlDbType.DateTime),
					new SqlParameter("@UserScoreDetailId", SqlDbType.Int,4)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.UserAuthorityId;
			parameters[2].Value = model.UserScoreDetailTime;
			parameters[3].Value = model.UserScoreDetailId;

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
		public bool Delete(int UserScoreDetailId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserScoreDetail ");
			strSql.Append(" where UserScoreDetailId=@UserScoreDetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserScoreDetailId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserScoreDetailId;

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
		public bool DeleteList(string UserScoreDetailIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserScoreDetail ");
			strSql.Append(" where UserScoreDetailId in ("+UserScoreDetailIdlist + ")  ");
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
		public OnLineTest.Model.UserScoreDetail GetModel(int UserScoreDetailId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserScoreDetailId,UserId,UserAuthorityId,UserScoreDetailTime from UserScoreDetail ");
			strSql.Append(" where UserScoreDetailId=@UserScoreDetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserScoreDetailId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserScoreDetailId;

			OnLineTest.Model.UserScoreDetail model=new OnLineTest.Model.UserScoreDetail();
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
		public OnLineTest.Model.UserScoreDetail DataRowToModel(DataRow row)
		{
			OnLineTest.Model.UserScoreDetail model=new OnLineTest.Model.UserScoreDetail();
			if (row != null)
			{
				if(row["UserScoreDetailId"]!=null && row["UserScoreDetailId"].ToString()!="")
				{
					model.UserScoreDetailId=int.Parse(row["UserScoreDetailId"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["UserAuthorityId"]!=null && row["UserAuthorityId"].ToString()!="")
				{
					model.UserAuthorityId=int.Parse(row["UserAuthorityId"].ToString());
				}
				if(row["UserScoreDetailTime"]!=null && row["UserScoreDetailTime"].ToString()!="")
				{
					model.UserScoreDetailTime=DateTime.Parse(row["UserScoreDetailTime"].ToString());
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
			strSql.Append("select UserScoreDetailId,UserId,UserAuthorityId,UserScoreDetailTime ");
			strSql.Append(" FROM UserScoreDetail ");
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
			strSql.Append(" UserScoreDetailId,UserId,UserAuthorityId,UserScoreDetailTime ");
			strSql.Append(" FROM UserScoreDetail ");
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
			strSql.Append("select count(1) FROM UserScoreDetail ");
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
				strSql.Append("order by T.UserScoreDetailId desc");
			}
			strSql.Append(")AS Row, T.*  from UserScoreDetail T ");
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
			parameters[0].Value = "UserScoreDetail";
			parameters[1].Value = "UserScoreDetailId";
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

