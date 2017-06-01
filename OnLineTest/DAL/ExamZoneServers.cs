using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:ExamZoneServers
	/// </summary>
	public partial class ExamZoneServers
	{
		public ExamZoneServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ExamZoneId", "ExamZone"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ExamZoneId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ExamZone");
			strSql.Append(" where ExamZoneId=@ExamZoneId");
			SqlParameter[] parameters = {
					new SqlParameter("@ExamZoneId", SqlDbType.Int,4)
			};
			parameters[0].Value = ExamZoneId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.ExamZone model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ExamZone(");
			strSql.Append("ExamZoneName,IsVerified)");
			strSql.Append(" values (");
			strSql.Append("@ExamZoneName,@IsVerified)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ExamZoneName", SqlDbType.NVarChar,20),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1)};
			parameters[0].Value = model.ExamZoneName;
			parameters[1].Value = model.IsVerified;

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
		public bool Update(OnLineTest.Model.ExamZone model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ExamZone set ");
			strSql.Append("ExamZoneName=@ExamZoneName,");
			strSql.Append("IsVerified=@IsVerified");
			strSql.Append(" where ExamZoneId=@ExamZoneId");
			SqlParameter[] parameters = {
					new SqlParameter("@ExamZoneName", SqlDbType.NVarChar,20),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1),
					new SqlParameter("@ExamZoneId", SqlDbType.Int,4)};
			parameters[0].Value = model.ExamZoneName;
			parameters[1].Value = model.IsVerified;
			parameters[2].Value = model.ExamZoneId;

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
		public bool Delete(int ExamZoneId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ExamZone ");
			strSql.Append(" where ExamZoneId=@ExamZoneId");
			SqlParameter[] parameters = {
					new SqlParameter("@ExamZoneId", SqlDbType.Int,4)
			};
			parameters[0].Value = ExamZoneId;

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
		public bool DeleteList(string ExamZoneIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ExamZone ");
			strSql.Append(" where ExamZoneId in ("+ExamZoneIdlist + ")  ");
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
		public OnLineTest.Model.ExamZone GetModel(int ExamZoneId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ExamZoneId,ExamZoneName,IsVerified from ExamZone ");
			strSql.Append(" where ExamZoneId=@ExamZoneId");
			SqlParameter[] parameters = {
					new SqlParameter("@ExamZoneId", SqlDbType.Int,4)
			};
			parameters[0].Value = ExamZoneId;

			OnLineTest.Model.ExamZone model=new OnLineTest.Model.ExamZone();
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
		public OnLineTest.Model.ExamZone DataRowToModel(DataRow row)
		{
			OnLineTest.Model.ExamZone model=new OnLineTest.Model.ExamZone();
			if (row != null)
			{
				if(row["ExamZoneId"]!=null && row["ExamZoneId"].ToString()!="")
				{
					model.ExamZoneId=int.Parse(row["ExamZoneId"].ToString());
				}
				if(row["ExamZoneName"]!=null)
				{
					model.ExamZoneName=row["ExamZoneName"].ToString();
				}
				if(row["IsVerified"]!=null && row["IsVerified"].ToString()!="")
				{
					if((row["IsVerified"].ToString()=="1")||(row["IsVerified"].ToString().ToLower()=="true"))
					{
						model.IsVerified=true;
					}
					else
					{
						model.IsVerified=false;
					}
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
			strSql.Append("select ExamZoneId,ExamZoneName,IsVerified ");
			strSql.Append(" FROM ExamZone ");
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
			strSql.Append(" ExamZoneId,ExamZoneName,IsVerified ");
			strSql.Append(" FROM ExamZone ");
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
			strSql.Append("select count(1) FROM ExamZone ");
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
				strSql.Append("order by T.ExamZoneId desc");
			}
			strSql.Append(")AS Row, T.*  from ExamZone T ");
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
			parameters[0].Value = "ExamZone";
			parameters[1].Value = "ExamZoneId";
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

