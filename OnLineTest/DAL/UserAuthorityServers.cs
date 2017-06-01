using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:UserAuthorityServers
	/// </summary>
	public partial class UserAuthorityServers
	{
		public UserAuthorityServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserAuthorityId", "UserAuthority"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserAuthorityId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from UserAuthority");
			strSql.Append(" where UserAuthorityId=@UserAuthorityId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserAuthorityId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserAuthorityId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.UserAuthority model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UserAuthority(");
			strSql.Append("AuthorityId,UserGroupId,UserRankId,UserAuthoriryRemark)");
			strSql.Append(" values (");
			strSql.Append("@AuthorityId,@UserGroupId,@UserRankId,@UserAuthoriryRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4),
					new SqlParameter("@UserGroupId", SqlDbType.Int,4),
					new SqlParameter("@UserRankId", SqlDbType.Int,4),
					new SqlParameter("@UserAuthoriryRemark", SqlDbType.Text)};
			parameters[0].Value = model.AuthorityId;
			parameters[1].Value = model.UserGroupId;
			parameters[2].Value = model.UserRankId;
			parameters[3].Value = model.UserAuthoriryRemark;

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
		public bool Update(OnLineTest.Model.UserAuthority model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UserAuthority set ");
			strSql.Append("AuthorityId=@AuthorityId,");
			strSql.Append("UserGroupId=@UserGroupId,");
			strSql.Append("UserRankId=@UserRankId,");
			strSql.Append("UserAuthoriryRemark=@UserAuthoriryRemark");
			strSql.Append(" where UserAuthorityId=@UserAuthorityId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4),
					new SqlParameter("@UserGroupId", SqlDbType.Int,4),
					new SqlParameter("@UserRankId", SqlDbType.Int,4),
					new SqlParameter("@UserAuthoriryRemark", SqlDbType.Text),
					new SqlParameter("@UserAuthorityId", SqlDbType.Int,4)};
			parameters[0].Value = model.AuthorityId;
			parameters[1].Value = model.UserGroupId;
			parameters[2].Value = model.UserRankId;
			parameters[3].Value = model.UserAuthoriryRemark;
			parameters[4].Value = model.UserAuthorityId;

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
		public bool Delete(int UserAuthorityId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserAuthority ");
			strSql.Append(" where UserAuthorityId=@UserAuthorityId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserAuthorityId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserAuthorityId;

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
		public bool DeleteList(string UserAuthorityIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserAuthority ");
			strSql.Append(" where UserAuthorityId in ("+UserAuthorityIdlist + ")  ");
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
		public OnLineTest.Model.UserAuthority GetModel(int UserAuthorityId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserAuthorityId,AuthorityId,UserGroupId,UserRankId,UserAuthoriryRemark from UserAuthority ");
			strSql.Append(" where UserAuthorityId=@UserAuthorityId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserAuthorityId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserAuthorityId;

			OnLineTest.Model.UserAuthority model=new OnLineTest.Model.UserAuthority();
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
		public OnLineTest.Model.UserAuthority DataRowToModel(DataRow row)
		{
			OnLineTest.Model.UserAuthority model=new OnLineTest.Model.UserAuthority();
			if (row != null)
			{
				if(row["UserAuthorityId"]!=null && row["UserAuthorityId"].ToString()!="")
				{
					model.UserAuthorityId=int.Parse(row["UserAuthorityId"].ToString());
				}
				if(row["AuthorityId"]!=null && row["AuthorityId"].ToString()!="")
				{
					model.AuthorityId=int.Parse(row["AuthorityId"].ToString());
				}
				if(row["UserGroupId"]!=null && row["UserGroupId"].ToString()!="")
				{
					model.UserGroupId=int.Parse(row["UserGroupId"].ToString());
				}
				if(row["UserRankId"]!=null && row["UserRankId"].ToString()!="")
				{
					model.UserRankId=int.Parse(row["UserRankId"].ToString());
				}
				if(row["UserAuthoriryRemark"]!=null)
				{
					model.UserAuthoriryRemark=row["UserAuthoriryRemark"].ToString();
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
			strSql.Append("select UserAuthorityId,AuthorityId,UserGroupId,UserRankId,UserAuthoriryRemark ");
			strSql.Append(" FROM UserAuthority ");
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
			strSql.Append(" UserAuthorityId,AuthorityId,UserGroupId,UserRankId,UserAuthoriryRemark ");
			strSql.Append(" FROM UserAuthority ");
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
			strSql.Append("select count(1) FROM UserAuthority ");
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
				strSql.Append("order by T.UserAuthorityId desc");
			}
			strSql.Append(")AS Row, T.*  from UserAuthority T ");
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
			parameters[0].Value = "UserAuthority";
			parameters[1].Value = "UserAuthorityId";
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

