using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:CommentServers
	/// </summary>
	public partial class CommentServers
	{
		public CommentServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CommentId", "Comment"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CommentId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Comment");
			strSql.Append(" where CommentId=@CommentId");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentId", SqlDbType.Int,4)
			};
			parameters[0].Value = CommentId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.Comment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Comment(");
			strSql.Append("QuestionId,UserId,CommentContent,CommentTime,QuoteCommentId,IsDeleted,DeleteUserId,DeleteCommentTime)");
			strSql.Append(" values (");
			strSql.Append("@QuestionId,@UserId,@CommentContent,@CommentTime,@QuoteCommentId,@IsDeleted,@DeleteUserId,@DeleteCommentTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@QuestionId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@CommentContent", SqlDbType.Text),
					new SqlParameter("@CommentTime", SqlDbType.DateTime),
					new SqlParameter("@QuoteCommentId", SqlDbType.Int,4),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@DeleteUserId", SqlDbType.Int,4),
					new SqlParameter("@DeleteCommentTime", SqlDbType.DateTime)};
			parameters[0].Value = model.QuestionId;
			parameters[1].Value = model.UserId;
			parameters[2].Value = model.CommentContent;
			parameters[3].Value = model.CommentTime;
			parameters[4].Value = model.QuoteCommentId;
			parameters[5].Value = model.IsDeleted;
			parameters[6].Value = model.DeleteUserId;
			parameters[7].Value = model.DeleteCommentTime;

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
		public bool Update(OnLineTest.Model.Comment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Comment set ");
			strSql.Append("QuestionId=@QuestionId,");
			strSql.Append("UserId=@UserId,");
			strSql.Append("CommentContent=@CommentContent,");
			strSql.Append("CommentTime=@CommentTime,");
			strSql.Append("QuoteCommentId=@QuoteCommentId,");
			strSql.Append("IsDeleted=@IsDeleted,");
			strSql.Append("DeleteUserId=@DeleteUserId,");
			strSql.Append("DeleteCommentTime=@DeleteCommentTime");
			strSql.Append(" where CommentId=@CommentId");
			SqlParameter[] parameters = {
					new SqlParameter("@QuestionId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@CommentContent", SqlDbType.Text),
					new SqlParameter("@CommentTime", SqlDbType.DateTime),
					new SqlParameter("@QuoteCommentId", SqlDbType.Int,4),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@DeleteUserId", SqlDbType.Int,4),
					new SqlParameter("@DeleteCommentTime", SqlDbType.DateTime),
					new SqlParameter("@CommentId", SqlDbType.Int,4)};
			parameters[0].Value = model.QuestionId;
			parameters[1].Value = model.UserId;
			parameters[2].Value = model.CommentContent;
			parameters[3].Value = model.CommentTime;
			parameters[4].Value = model.QuoteCommentId;
			parameters[5].Value = model.IsDeleted;
			parameters[6].Value = model.DeleteUserId;
			parameters[7].Value = model.DeleteCommentTime;
			parameters[8].Value = model.CommentId;

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
		public bool Delete(int CommentId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Comment ");
			strSql.Append(" where CommentId=@CommentId");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentId", SqlDbType.Int,4)
			};
			parameters[0].Value = CommentId;

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
		public bool DeleteList(string CommentIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Comment ");
			strSql.Append(" where CommentId in ("+CommentIdlist + ")  ");
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
		public OnLineTest.Model.Comment GetModel(int CommentId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CommentId,QuestionId,UserId,CommentContent,CommentTime,QuoteCommentId,IsDeleted,DeleteUserId,DeleteCommentTime from Comment ");
			strSql.Append(" where CommentId=@CommentId");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentId", SqlDbType.Int,4)
			};
			parameters[0].Value = CommentId;

			OnLineTest.Model.Comment model=new OnLineTest.Model.Comment();
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
		public OnLineTest.Model.Comment DataRowToModel(DataRow row)
		{
			OnLineTest.Model.Comment model=new OnLineTest.Model.Comment();
			if (row != null)
			{
				if(row["CommentId"]!=null && row["CommentId"].ToString()!="")
				{
					model.CommentId=int.Parse(row["CommentId"].ToString());
				}
				if(row["QuestionId"]!=null && row["QuestionId"].ToString()!="")
				{
					model.QuestionId=int.Parse(row["QuestionId"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["CommentContent"]!=null)
				{
					model.CommentContent=row["CommentContent"].ToString();
				}
				if(row["CommentTime"]!=null && row["CommentTime"].ToString()!="")
				{
					model.CommentTime=DateTime.Parse(row["CommentTime"].ToString());
				}
				if(row["QuoteCommentId"]!=null && row["QuoteCommentId"].ToString()!="")
				{
					model.QuoteCommentId=int.Parse(row["QuoteCommentId"].ToString());
				}
				if(row["IsDeleted"]!=null && row["IsDeleted"].ToString()!="")
				{
					if((row["IsDeleted"].ToString()=="1")||(row["IsDeleted"].ToString().ToLower()=="true"))
					{
						model.IsDeleted=true;
					}
					else
					{
						model.IsDeleted=false;
					}
				}
				if(row["DeleteUserId"]!=null && row["DeleteUserId"].ToString()!="")
				{
					model.DeleteUserId=int.Parse(row["DeleteUserId"].ToString());
				}
				if(row["DeleteCommentTime"]!=null && row["DeleteCommentTime"].ToString()!="")
				{
					model.DeleteCommentTime=DateTime.Parse(row["DeleteCommentTime"].ToString());
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
			strSql.Append("select CommentId,QuestionId,UserId,CommentContent,CommentTime,QuoteCommentId,IsDeleted,DeleteUserId,DeleteCommentTime ");
			strSql.Append(" FROM Comment ");
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
			strSql.Append(" CommentId,QuestionId,UserId,CommentContent,CommentTime,QuoteCommentId,IsDeleted,DeleteUserId,DeleteCommentTime ");
			strSql.Append(" FROM Comment ");
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
			strSql.Append("select count(1) FROM Comment ");
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
				strSql.Append("order by T.CommentId desc");
			}
			strSql.Append(")AS Row, T.*  from Comment T ");
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
			parameters[0].Value = "Comment";
			parameters[1].Value = "CommentId";
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

