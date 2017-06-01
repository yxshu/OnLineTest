
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:AuthorityServers
	/// </summary>
	public partial class AuthorityServers
	{
		public AuthorityServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AuthorityId", "Authority"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string AuthorityHandlerPage,int AuthorityId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Authority");
			strSql.Append(" where AuthorityHandlerPage=@AuthorityHandlerPage and AuthorityId=@AuthorityId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityHandlerPage", SqlDbType.VarChar,50),
					new SqlParameter("@AuthorityId", SqlDbType.Int,4)			};
			parameters[0].Value = AuthorityHandlerPage;
			parameters[1].Value = AuthorityId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.Authority model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Authority(");
			strSql.Append("AuthorityName,AuthorityDeep,AuthorityParentId,AuthorityScore,AuthorityHandlerPage,AuthorityOrderNum,AuthorityRemark)");
			strSql.Append(" values (");
			strSql.Append("@AuthorityName,@AuthorityDeep,@AuthorityParentId,@AuthorityScore,@AuthorityHandlerPage,@AuthorityOrderNum,@AuthorityRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityName", SqlDbType.NVarChar,20),
					new SqlParameter("@AuthorityDeep", SqlDbType.Int,4),
					new SqlParameter("@AuthorityParentId", SqlDbType.Int,4),
					new SqlParameter("@AuthorityScore", SqlDbType.Int,4),
					new SqlParameter("@AuthorityHandlerPage", SqlDbType.VarChar,50),
					new SqlParameter("@AuthorityOrderNum", SqlDbType.Int,4),
					new SqlParameter("@AuthorityRemark", SqlDbType.Text)};
			parameters[0].Value = model.AuthorityName;
			parameters[1].Value = model.AuthorityDeep;
			parameters[2].Value = model.AuthorityParentId;
			parameters[3].Value = model.AuthorityScore;
			parameters[4].Value = model.AuthorityHandlerPage;
			parameters[5].Value = model.AuthorityOrderNum;
			parameters[6].Value = model.AuthorityRemark;

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
		public bool Update(OnLineTest.Model.Authority model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Authority set ");
			strSql.Append("AuthorityName=@AuthorityName,");
			strSql.Append("AuthorityDeep=@AuthorityDeep,");
			strSql.Append("AuthorityParentId=@AuthorityParentId,");
			strSql.Append("AuthorityScore=@AuthorityScore,");
			strSql.Append("AuthorityOrderNum=@AuthorityOrderNum,");
			strSql.Append("AuthorityRemark=@AuthorityRemark");
			strSql.Append(" where AuthorityId=@AuthorityId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityName", SqlDbType.NVarChar,20),
					new SqlParameter("@AuthorityDeep", SqlDbType.Int,4),
					new SqlParameter("@AuthorityParentId", SqlDbType.Int,4),
					new SqlParameter("@AuthorityScore", SqlDbType.Int,4),
					new SqlParameter("@AuthorityOrderNum", SqlDbType.Int,4),
					new SqlParameter("@AuthorityRemark", SqlDbType.Text),
					new SqlParameter("@AuthorityId", SqlDbType.Int,4),
					new SqlParameter("@AuthorityHandlerPage", SqlDbType.VarChar,50)};
			parameters[0].Value = model.AuthorityName;
			parameters[1].Value = model.AuthorityDeep;
			parameters[2].Value = model.AuthorityParentId;
			parameters[3].Value = model.AuthorityScore;
			parameters[4].Value = model.AuthorityOrderNum;
			parameters[5].Value = model.AuthorityRemark;
			parameters[6].Value = model.AuthorityId;
			parameters[7].Value = model.AuthorityHandlerPage;

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
		public bool Delete(int AuthorityId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Authority ");
			strSql.Append(" where AuthorityId=@AuthorityId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4)
			};
			parameters[0].Value = AuthorityId;

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
		public bool Delete(string AuthorityHandlerPage,int AuthorityId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Authority ");
			strSql.Append(" where AuthorityHandlerPage=@AuthorityHandlerPage and AuthorityId=@AuthorityId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityHandlerPage", SqlDbType.VarChar,50),
					new SqlParameter("@AuthorityId", SqlDbType.Int,4)			};
			parameters[0].Value = AuthorityHandlerPage;
			parameters[1].Value = AuthorityId;

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
		public bool DeleteList(string AuthorityIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Authority ");
			strSql.Append(" where AuthorityId in ("+AuthorityIdlist + ")  ");
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
		public OnLineTest.Model.Authority GetModel(int AuthorityId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AuthorityId,AuthorityName,AuthorityDeep,AuthorityParentId,AuthorityScore,AuthorityHandlerPage,AuthorityOrderNum,AuthorityRemark from Authority ");
			strSql.Append(" where AuthorityId=@AuthorityId");
			SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4)
			};
			parameters[0].Value = AuthorityId;

			OnLineTest.Model.Authority model=new OnLineTest.Model.Authority();
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
		public OnLineTest.Model.Authority DataRowToModel(DataRow row)
		{
			OnLineTest.Model.Authority model=new OnLineTest.Model.Authority();
			if (row != null)
			{
				if(row["AuthorityId"]!=null && row["AuthorityId"].ToString()!="")
				{
					model.AuthorityId=int.Parse(row["AuthorityId"].ToString());
				}
				if(row["AuthorityName"]!=null)
				{
					model.AuthorityName=row["AuthorityName"].ToString();
				}
				if(row["AuthorityDeep"]!=null && row["AuthorityDeep"].ToString()!="")
				{
					model.AuthorityDeep=int.Parse(row["AuthorityDeep"].ToString());
				}
				if(row["AuthorityParentId"]!=null && row["AuthorityParentId"].ToString()!="")
				{
					model.AuthorityParentId=int.Parse(row["AuthorityParentId"].ToString());
				}
				if(row["AuthorityScore"]!=null && row["AuthorityScore"].ToString()!="")
				{
					model.AuthorityScore=int.Parse(row["AuthorityScore"].ToString());
				}
				if(row["AuthorityHandlerPage"]!=null)
				{
					model.AuthorityHandlerPage=row["AuthorityHandlerPage"].ToString();
				}
				if(row["AuthorityOrderNum"]!=null && row["AuthorityOrderNum"].ToString()!="")
				{
					model.AuthorityOrderNum=int.Parse(row["AuthorityOrderNum"].ToString());
				}
				if(row["AuthorityRemark"]!=null)
				{
					model.AuthorityRemark=row["AuthorityRemark"].ToString();
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
			strSql.Append("select AuthorityId,AuthorityName,AuthorityDeep,AuthorityParentId,AuthorityScore,AuthorityHandlerPage,AuthorityOrderNum,AuthorityRemark ");
			strSql.Append(" FROM Authority ");
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
			strSql.Append(" AuthorityId,AuthorityName,AuthorityDeep,AuthorityParentId,AuthorityScore,AuthorityHandlerPage,AuthorityOrderNum,AuthorityRemark ");
			strSql.Append(" FROM Authority ");
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
			strSql.Append("select count(1) FROM Authority ");
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
				strSql.Append("order by T.AuthorityId desc");
			}
			strSql.Append(")AS Row, T.*  from Authority T ");
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
			parameters[0].Value = "Authority";
			parameters[1].Value = "AuthorityId";
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

