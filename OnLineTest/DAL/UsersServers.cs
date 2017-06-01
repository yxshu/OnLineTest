using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:UsersServers
	/// </summary>
	public partial class UsersServers
	{
		public UsersServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserId", "Users"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserName,int UserId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Users");
			strSql.Append(" where UserName=@UserName and UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@UserId", SqlDbType.Int,4)			};
			parameters[0].Value = UserName;
			parameters[1].Value = UserId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.Users model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Users(");
			strSql.Append("UserName,UserPassword,UserChineseName,UserImageName,UserEmail,IsValidate,Tel,UserScore,UserRegisterDatetime,UserGroupId)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@UserPassword,@UserChineseName,@UserImageName,@UserEmail,@IsValidate,@Tel,@UserScore,@UserRegisterDatetime,@UserGroupId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@UserPassword", SqlDbType.VarChar,200),
					new SqlParameter("@UserChineseName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserImageName", SqlDbType.NVarChar,100),
					new SqlParameter("@UserEmail", SqlDbType.VarChar,50),
					new SqlParameter("@IsValidate", SqlDbType.Bit,1),
					new SqlParameter("@Tel", SqlDbType.VarChar,20),
					new SqlParameter("@UserScore", SqlDbType.Int,4),
					new SqlParameter("@UserRegisterDatetime", SqlDbType.DateTime),
					new SqlParameter("@UserGroupId", SqlDbType.Int,4)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.UserPassword;
			parameters[2].Value = model.UserChineseName;
			parameters[3].Value = model.UserImageName;
			parameters[4].Value = model.UserEmail;
			parameters[5].Value = model.IsValidate;
			parameters[6].Value = model.Tel;
			parameters[7].Value = model.UserScore;
			parameters[8].Value = model.UserRegisterDatetime;
			parameters[9].Value = model.UserGroupId;

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
		public bool Update(OnLineTest.Model.Users model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Users set ");
			strSql.Append("UserPassword=@UserPassword,");
			strSql.Append("UserChineseName=@UserChineseName,");
			strSql.Append("UserImageName=@UserImageName,");
			strSql.Append("UserEmail=@UserEmail,");
			strSql.Append("IsValidate=@IsValidate,");
			strSql.Append("Tel=@Tel,");
			strSql.Append("UserScore=@UserScore,");
			strSql.Append("UserRegisterDatetime=@UserRegisterDatetime,");
			strSql.Append("UserGroupId=@UserGroupId");
			strSql.Append(" where UserId=@UserId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserPassword", SqlDbType.VarChar,200),
					new SqlParameter("@UserChineseName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserImageName", SqlDbType.NVarChar,100),
					new SqlParameter("@UserEmail", SqlDbType.VarChar,50),
					new SqlParameter("@IsValidate", SqlDbType.Bit,1),
					new SqlParameter("@Tel", SqlDbType.VarChar,20),
					new SqlParameter("@UserScore", SqlDbType.Int,4),
					new SqlParameter("@UserRegisterDatetime", SqlDbType.DateTime),
					new SqlParameter("@UserGroupId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.VarChar,20)};
			parameters[0].Value = model.UserPassword;
			parameters[1].Value = model.UserChineseName;
			parameters[2].Value = model.UserImageName;
			parameters[3].Value = model.UserEmail;
			parameters[4].Value = model.IsValidate;
			parameters[5].Value = model.Tel;
			parameters[6].Value = model.UserScore;
			parameters[7].Value = model.UserRegisterDatetime;
			parameters[8].Value = model.UserGroupId;
			parameters[9].Value = model.UserId;
			parameters[10].Value = model.UserName;

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
		public bool Delete(int UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Users ");
			strSql.Append(" where UserId=@UserId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserId;

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
		public bool Delete(string UserName,int UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Users ");
			strSql.Append(" where UserName=@UserName and UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@UserId", SqlDbType.Int,4)			};
			parameters[0].Value = UserName;
			parameters[1].Value = UserId;

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
		public bool DeleteList(string UserIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Users ");
			strSql.Append(" where UserId in ("+UserIdlist + ")  ");
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
		public OnLineTest.Model.Users GetModel(int UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserId,UserName,UserPassword,UserChineseName,UserImageName,UserEmail,IsValidate,Tel,UserScore,UserRegisterDatetime,UserGroupId from Users ");
			strSql.Append(" where UserId=@UserId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserId;

			OnLineTest.Model.Users model=new OnLineTest.Model.Users();
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
		public OnLineTest.Model.Users DataRowToModel(DataRow row)
		{
			OnLineTest.Model.Users model=new OnLineTest.Model.Users();
			if (row != null)
			{
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["UserPassword"]!=null)
				{
					model.UserPassword=row["UserPassword"].ToString();
				}
				if(row["UserChineseName"]!=null)
				{
					model.UserChineseName=row["UserChineseName"].ToString();
				}
				if(row["UserImageName"]!=null)
				{
					model.UserImageName=row["UserImageName"].ToString();
				}
				if(row["UserEmail"]!=null)
				{
					model.UserEmail=row["UserEmail"].ToString();
				}
				if(row["IsValidate"]!=null && row["IsValidate"].ToString()!="")
				{
					if((row["IsValidate"].ToString()=="1")||(row["IsValidate"].ToString().ToLower()=="true"))
					{
						model.IsValidate=true;
					}
					else
					{
						model.IsValidate=false;
					}
				}
				if(row["Tel"]!=null)
				{
					model.Tel=row["Tel"].ToString();
				}
				if(row["UserScore"]!=null && row["UserScore"].ToString()!="")
				{
					model.UserScore=int.Parse(row["UserScore"].ToString());
				}
				if(row["UserRegisterDatetime"]!=null && row["UserRegisterDatetime"].ToString()!="")
				{
					model.UserRegisterDatetime=DateTime.Parse(row["UserRegisterDatetime"].ToString());
				}
				if(row["UserGroupId"]!=null && row["UserGroupId"].ToString()!="")
				{
					model.UserGroupId=int.Parse(row["UserGroupId"].ToString());
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
			strSql.Append("select UserId,UserName,UserPassword,UserChineseName,UserImageName,UserEmail,IsValidate,Tel,UserScore,UserRegisterDatetime,UserGroupId ");
			strSql.Append(" FROM Users ");
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
			strSql.Append(" UserId,UserName,UserPassword,UserChineseName,UserImageName,UserEmail,IsValidate,Tel,UserScore,UserRegisterDatetime,UserGroupId ");
			strSql.Append(" FROM Users ");
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
			strSql.Append("select count(1) FROM Users ");
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
				strSql.Append("order by T.UserId desc");
			}
			strSql.Append(")AS Row, T.*  from Users T ");
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
			parameters[0].Value = "Users";
			parameters[1].Value = "UserId";
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

