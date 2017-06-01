using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:ChapterServers
	/// </summary>
	public partial class ChapterServers
	{
		public ChapterServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ChapterId", "Chapter"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ChapterId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Chapter");
			strSql.Append(" where ChapterId=@ChapterId");
			SqlParameter[] parameters = {
					new SqlParameter("@ChapterId", SqlDbType.Int,4)
			};
			parameters[0].Value = ChapterId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.Chapter model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Chapter(");
			strSql.Append("TextBookId,ChapterName,ChapterParentNo,ChapterDeep,ChapterRemark,IsVerified)");
			strSql.Append(" values (");
			strSql.Append("@TextBookId,@ChapterName,@ChapterParentNo,@ChapterDeep,@ChapterRemark,@IsVerified)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TextBookId", SqlDbType.Int,4),
					new SqlParameter("@ChapterName", SqlDbType.NVarChar,200),
					new SqlParameter("@ChapterParentNo", SqlDbType.Int,4),
					new SqlParameter("@ChapterDeep", SqlDbType.Int,4),
					new SqlParameter("@ChapterRemark", SqlDbType.Text),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1)};
			parameters[0].Value = model.TextBookId;
			parameters[1].Value = model.ChapterName;
			parameters[2].Value = model.ChapterParentNo;
			parameters[3].Value = model.ChapterDeep;
			parameters[4].Value = model.ChapterRemark;
			parameters[5].Value = model.IsVerified;

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
		public bool Update(OnLineTest.Model.Chapter model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Chapter set ");
			strSql.Append("TextBookId=@TextBookId,");
			strSql.Append("ChapterName=@ChapterName,");
			strSql.Append("ChapterParentNo=@ChapterParentNo,");
			strSql.Append("ChapterDeep=@ChapterDeep,");
			strSql.Append("ChapterRemark=@ChapterRemark,");
			strSql.Append("IsVerified=@IsVerified");
			strSql.Append(" where ChapterId=@ChapterId");
			SqlParameter[] parameters = {
					new SqlParameter("@TextBookId", SqlDbType.Int,4),
					new SqlParameter("@ChapterName", SqlDbType.NVarChar,200),
					new SqlParameter("@ChapterParentNo", SqlDbType.Int,4),
					new SqlParameter("@ChapterDeep", SqlDbType.Int,4),
					new SqlParameter("@ChapterRemark", SqlDbType.Text),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1),
					new SqlParameter("@ChapterId", SqlDbType.Int,4)};
			parameters[0].Value = model.TextBookId;
			parameters[1].Value = model.ChapterName;
			parameters[2].Value = model.ChapterParentNo;
			parameters[3].Value = model.ChapterDeep;
			parameters[4].Value = model.ChapterRemark;
			parameters[5].Value = model.IsVerified;
			parameters[6].Value = model.ChapterId;

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
		public bool Delete(int ChapterId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Chapter ");
			strSql.Append(" where ChapterId=@ChapterId");
			SqlParameter[] parameters = {
					new SqlParameter("@ChapterId", SqlDbType.Int,4)
			};
			parameters[0].Value = ChapterId;

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
		public bool DeleteList(string ChapterIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Chapter ");
			strSql.Append(" where ChapterId in ("+ChapterIdlist + ")  ");
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
		public OnLineTest.Model.Chapter GetModel(int ChapterId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ChapterId,TextBookId,ChapterName,ChapterParentNo,ChapterDeep,ChapterRemark,IsVerified from Chapter ");
			strSql.Append(" where ChapterId=@ChapterId");
			SqlParameter[] parameters = {
					new SqlParameter("@ChapterId", SqlDbType.Int,4)
			};
			parameters[0].Value = ChapterId;

			OnLineTest.Model.Chapter model=new OnLineTest.Model.Chapter();
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
		public OnLineTest.Model.Chapter DataRowToModel(DataRow row)
		{
			OnLineTest.Model.Chapter model=new OnLineTest.Model.Chapter();
			if (row != null)
			{
				if(row["ChapterId"]!=null && row["ChapterId"].ToString()!="")
				{
					model.ChapterId=int.Parse(row["ChapterId"].ToString());
				}
				if(row["TextBookId"]!=null && row["TextBookId"].ToString()!="")
				{
					model.TextBookId=int.Parse(row["TextBookId"].ToString());
				}
				if(row["ChapterName"]!=null)
				{
					model.ChapterName=row["ChapterName"].ToString();
				}
				if(row["ChapterParentNo"]!=null && row["ChapterParentNo"].ToString()!="")
				{
					model.ChapterParentNo=int.Parse(row["ChapterParentNo"].ToString());
				}
				if(row["ChapterDeep"]!=null && row["ChapterDeep"].ToString()!="")
				{
					model.ChapterDeep=int.Parse(row["ChapterDeep"].ToString());
				}
				if(row["ChapterRemark"]!=null)
				{
					model.ChapterRemark=row["ChapterRemark"].ToString();
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
			strSql.Append("select ChapterId,TextBookId,ChapterName,ChapterParentNo,ChapterDeep,ChapterRemark,IsVerified ");
			strSql.Append(" FROM Chapter ");
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
			strSql.Append(" ChapterId,TextBookId,ChapterName,ChapterParentNo,ChapterDeep,ChapterRemark,IsVerified ");
			strSql.Append(" FROM Chapter ");
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
			strSql.Append("select count(1) FROM Chapter ");
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
				strSql.Append("order by T.ChapterId desc");
			}
			strSql.Append(")AS Row, T.*  from Chapter T ");
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
			parameters[0].Value = "Chapter";
			parameters[1].Value = "ChapterId";
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

