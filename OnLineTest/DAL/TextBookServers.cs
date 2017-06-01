using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:TextBookServers
	/// </summary>
	public partial class TextBookServers
	{
		public TextBookServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TextBookId", "TextBook"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TextBookId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TextBook");
			strSql.Append(" where TextBookId=@TextBookId");
			SqlParameter[] parameters = {
					new SqlParameter("@TextBookId", SqlDbType.Int,4)
			};
			parameters[0].Value = TextBookId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.TextBook model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TextBook(");
			strSql.Append("PaperCodeId,TextBookName,ISBN,IsVerified)");
			strSql.Append(" values (");
			strSql.Append("@PaperCodeId,@TextBookName,@ISBN,@IsVerified)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@TextBookName", SqlDbType.NVarChar,200),
					new SqlParameter("@ISBN", SqlDbType.NVarChar,50),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1)};
			parameters[0].Value = model.PaperCodeId;
			parameters[1].Value = model.TextBookName;
			parameters[2].Value = model.ISBN;
			parameters[3].Value = model.IsVerified;

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
		public bool Update(OnLineTest.Model.TextBook model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TextBook set ");
			strSql.Append("PaperCodeId=@PaperCodeId,");
			strSql.Append("TextBookName=@TextBookName,");
			strSql.Append("ISBN=@ISBN,");
			strSql.Append("IsVerified=@IsVerified");
			strSql.Append(" where TextBookId=@TextBookId");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@TextBookName", SqlDbType.NVarChar,200),
					new SqlParameter("@ISBN", SqlDbType.NVarChar,50),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1),
					new SqlParameter("@TextBookId", SqlDbType.Int,4)};
			parameters[0].Value = model.PaperCodeId;
			parameters[1].Value = model.TextBookName;
			parameters[2].Value = model.ISBN;
			parameters[3].Value = model.IsVerified;
			parameters[4].Value = model.TextBookId;

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
		public bool Delete(int TextBookId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TextBook ");
			strSql.Append(" where TextBookId=@TextBookId");
			SqlParameter[] parameters = {
					new SqlParameter("@TextBookId", SqlDbType.Int,4)
			};
			parameters[0].Value = TextBookId;

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
		public bool DeleteList(string TextBookIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TextBook ");
			strSql.Append(" where TextBookId in ("+TextBookIdlist + ")  ");
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
		public OnLineTest.Model.TextBook GetModel(int TextBookId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TextBookId,PaperCodeId,TextBookName,ISBN,IsVerified from TextBook ");
			strSql.Append(" where TextBookId=@TextBookId");
			SqlParameter[] parameters = {
					new SqlParameter("@TextBookId", SqlDbType.Int,4)
			};
			parameters[0].Value = TextBookId;

			OnLineTest.Model.TextBook model=new OnLineTest.Model.TextBook();
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
		public OnLineTest.Model.TextBook DataRowToModel(DataRow row)
		{
			OnLineTest.Model.TextBook model=new OnLineTest.Model.TextBook();
			if (row != null)
			{
				if(row["TextBookId"]!=null && row["TextBookId"].ToString()!="")
				{
					model.TextBookId=int.Parse(row["TextBookId"].ToString());
				}
				if(row["PaperCodeId"]!=null && row["PaperCodeId"].ToString()!="")
				{
					model.PaperCodeId=int.Parse(row["PaperCodeId"].ToString());
				}
				if(row["TextBookName"]!=null)
				{
					model.TextBookName=row["TextBookName"].ToString();
				}
				if(row["ISBN"]!=null)
				{
					model.ISBN=row["ISBN"].ToString();
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
			strSql.Append("select TextBookId,PaperCodeId,TextBookName,ISBN,IsVerified ");
			strSql.Append(" FROM TextBook ");
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
			strSql.Append(" TextBookId,PaperCodeId,TextBookName,ISBN,IsVerified ");
			strSql.Append(" FROM TextBook ");
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
			strSql.Append("select count(1) FROM TextBook ");
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
				strSql.Append("order by T.TextBookId desc");
			}
			strSql.Append(")AS Row, T.*  from TextBook T ");
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
			parameters[0].Value = "TextBook";
			parameters[1].Value = "TextBookId";
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

