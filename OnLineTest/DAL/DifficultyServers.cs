using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:DifficultyServers
	/// </summary>
	public partial class DifficultyServers
	{
		public DifficultyServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DifficultyId", "Difficulty"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DifficultyId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Difficulty");
			strSql.Append(" where DifficultyId=@DifficultyId");
			SqlParameter[] parameters = {
					new SqlParameter("@DifficultyId", SqlDbType.Int,4)
			};
			parameters[0].Value = DifficultyId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.Difficulty model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Difficulty(");
			strSql.Append("DifficultyRatio,DifficultyDescrip,DifficultyRemark)");
			strSql.Append(" values (");
			strSql.Append("@DifficultyRatio,@DifficultyDescrip,@DifficultyRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@DifficultyRatio", SqlDbType.Int,4),
					new SqlParameter("@DifficultyDescrip", SqlDbType.NVarChar,20),
					new SqlParameter("@DifficultyRemark", SqlDbType.Text)};
			parameters[0].Value = model.DifficultyRatio;
			parameters[1].Value = model.DifficultyDescrip;
			parameters[2].Value = model.DifficultyRemark;

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
		public bool Update(OnLineTest.Model.Difficulty model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Difficulty set ");
			strSql.Append("DifficultyRatio=@DifficultyRatio,");
			strSql.Append("DifficultyDescrip=@DifficultyDescrip,");
			strSql.Append("DifficultyRemark=@DifficultyRemark");
			strSql.Append(" where DifficultyId=@DifficultyId");
			SqlParameter[] parameters = {
					new SqlParameter("@DifficultyRatio", SqlDbType.Int,4),
					new SqlParameter("@DifficultyDescrip", SqlDbType.NVarChar,20),
					new SqlParameter("@DifficultyRemark", SqlDbType.Text),
					new SqlParameter("@DifficultyId", SqlDbType.Int,4)};
			parameters[0].Value = model.DifficultyRatio;
			parameters[1].Value = model.DifficultyDescrip;
			parameters[2].Value = model.DifficultyRemark;
			parameters[3].Value = model.DifficultyId;

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
		public bool Delete(int DifficultyId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Difficulty ");
			strSql.Append(" where DifficultyId=@DifficultyId");
			SqlParameter[] parameters = {
					new SqlParameter("@DifficultyId", SqlDbType.Int,4)
			};
			parameters[0].Value = DifficultyId;

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
		public bool DeleteList(string DifficultyIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Difficulty ");
			strSql.Append(" where DifficultyId in ("+DifficultyIdlist + ")  ");
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
		public OnLineTest.Model.Difficulty GetModel(int DifficultyId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 DifficultyId,DifficultyRatio,DifficultyDescrip,DifficultyRemark from Difficulty ");
			strSql.Append(" where DifficultyId=@DifficultyId");
			SqlParameter[] parameters = {
					new SqlParameter("@DifficultyId", SqlDbType.Int,4)
			};
			parameters[0].Value = DifficultyId;

			OnLineTest.Model.Difficulty model=new OnLineTest.Model.Difficulty();
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
		public OnLineTest.Model.Difficulty DataRowToModel(DataRow row)
		{
			OnLineTest.Model.Difficulty model=new OnLineTest.Model.Difficulty();
			if (row != null)
			{
				if(row["DifficultyId"]!=null && row["DifficultyId"].ToString()!="")
				{
					model.DifficultyId=int.Parse(row["DifficultyId"].ToString());
				}
				if(row["DifficultyRatio"]!=null && row["DifficultyRatio"].ToString()!="")
				{
					model.DifficultyRatio=int.Parse(row["DifficultyRatio"].ToString());
				}
				if(row["DifficultyDescrip"]!=null)
				{
					model.DifficultyDescrip=row["DifficultyDescrip"].ToString();
				}
				if(row["DifficultyRemark"]!=null)
				{
					model.DifficultyRemark=row["DifficultyRemark"].ToString();
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
			strSql.Append("select DifficultyId,DifficultyRatio,DifficultyDescrip,DifficultyRemark ");
			strSql.Append(" FROM Difficulty ");
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
			strSql.Append(" DifficultyId,DifficultyRatio,DifficultyDescrip,DifficultyRemark ");
			strSql.Append(" FROM Difficulty ");
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
			strSql.Append("select count(1) FROM Difficulty ");
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
				strSql.Append("order by T.DifficultyId desc");
			}
			strSql.Append(")AS Row, T.*  from Difficulty T ");
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
			parameters[0].Value = "Difficulty";
			parameters[1].Value = "DifficultyId";
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

