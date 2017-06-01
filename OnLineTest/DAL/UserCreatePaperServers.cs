using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:UserCreatePaperServers
	/// </summary>
	public partial class UserCreatePaperServers
	{
		public UserCreatePaperServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserCreatePaperId", "UserCreatePaper"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserCreatePaperId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from UserCreatePaper");
			strSql.Append(" where UserCreatePaperId=@UserCreatePaperId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserCreatePaperId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserCreatePaperId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.UserCreatePaper model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UserCreatePaper(");
			strSql.Append("UserId,PaperCodeId,DifficultyId,UserCreatePaperTime)");
			strSql.Append(" values (");
			strSql.Append("@UserId,@PaperCodeId,@DifficultyId,@UserCreatePaperTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@DifficultyId", SqlDbType.Int,4),
					new SqlParameter("@UserCreatePaperTime", SqlDbType.DateTime)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.PaperCodeId;
			parameters[2].Value = model.DifficultyId;
			parameters[3].Value = model.UserCreatePaperTime;

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
		public bool Update(OnLineTest.Model.UserCreatePaper model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UserCreatePaper set ");
			strSql.Append("UserId=@UserId,");
			strSql.Append("PaperCodeId=@PaperCodeId,");
			strSql.Append("DifficultyId=@DifficultyId,");
			strSql.Append("UserCreatePaperTime=@UserCreatePaperTime");
			strSql.Append(" where UserCreatePaperId=@UserCreatePaperId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@DifficultyId", SqlDbType.Int,4),
					new SqlParameter("@UserCreatePaperTime", SqlDbType.DateTime),
					new SqlParameter("@UserCreatePaperId", SqlDbType.Int,4)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.PaperCodeId;
			parameters[2].Value = model.DifficultyId;
			parameters[3].Value = model.UserCreatePaperTime;
			parameters[4].Value = model.UserCreatePaperId;

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
		public bool Delete(int UserCreatePaperId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserCreatePaper ");
			strSql.Append(" where UserCreatePaperId=@UserCreatePaperId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserCreatePaperId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserCreatePaperId;

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
		public bool DeleteList(string UserCreatePaperIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserCreatePaper ");
			strSql.Append(" where UserCreatePaperId in ("+UserCreatePaperIdlist + ")  ");
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
		public OnLineTest.Model.UserCreatePaper GetModel(int UserCreatePaperId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserCreatePaperId,UserId,PaperCodeId,DifficultyId,UserCreatePaperTime from UserCreatePaper ");
			strSql.Append(" where UserCreatePaperId=@UserCreatePaperId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserCreatePaperId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserCreatePaperId;

			OnLineTest.Model.UserCreatePaper model=new OnLineTest.Model.UserCreatePaper();
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
		public OnLineTest.Model.UserCreatePaper DataRowToModel(DataRow row)
		{
			OnLineTest.Model.UserCreatePaper model=new OnLineTest.Model.UserCreatePaper();
			if (row != null)
			{
				if(row["UserCreatePaperId"]!=null && row["UserCreatePaperId"].ToString()!="")
				{
					model.UserCreatePaperId=int.Parse(row["UserCreatePaperId"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["PaperCodeId"]!=null && row["PaperCodeId"].ToString()!="")
				{
					model.PaperCodeId=int.Parse(row["PaperCodeId"].ToString());
				}
				if(row["DifficultyId"]!=null && row["DifficultyId"].ToString()!="")
				{
					model.DifficultyId=int.Parse(row["DifficultyId"].ToString());
				}
				if(row["UserCreatePaperTime"]!=null && row["UserCreatePaperTime"].ToString()!="")
				{
					model.UserCreatePaperTime=DateTime.Parse(row["UserCreatePaperTime"].ToString());
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
			strSql.Append("select UserCreatePaperId,UserId,PaperCodeId,DifficultyId,UserCreatePaperTime ");
			strSql.Append(" FROM UserCreatePaper ");
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
			strSql.Append(" UserCreatePaperId,UserId,PaperCodeId,DifficultyId,UserCreatePaperTime ");
			strSql.Append(" FROM UserCreatePaper ");
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
			strSql.Append("select count(1) FROM UserCreatePaper ");
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
				strSql.Append("order by T.UserCreatePaperId desc");
			}
			strSql.Append(")AS Row, T.*  from UserCreatePaper T ");
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
			parameters[0].Value = "UserCreatePaper";
			parameters[1].Value = "UserCreatePaperId";
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

