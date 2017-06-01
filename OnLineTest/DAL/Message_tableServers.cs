using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:Message_tableServers
	/// </summary>
	public partial class Message_tableServers
	{
		public Message_tableServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("MessageId", "Message_table"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MessageId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Message_table");
			strSql.Append(" where MessageId=@MessageId");
			SqlParameter[] parameters = {
					new SqlParameter("@MessageId", SqlDbType.Int,4)
			};
			parameters[0].Value = MessageId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.Message_table model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Message_table(");
			strSql.Append("MessageParentId,MessageTO,MessageFrom,MessageContent,MessageSendTime,MessageIsRead,MessageReadTime)");
			strSql.Append(" values (");
			strSql.Append("@MessageParentId,@MessageTO,@MessageFrom,@MessageContent,@MessageSendTime,@MessageIsRead,@MessageReadTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@MessageParentId", SqlDbType.Int,4),
					new SqlParameter("@MessageTO", SqlDbType.Int,4),
					new SqlParameter("@MessageFrom", SqlDbType.Int,4),
					new SqlParameter("@MessageContent", SqlDbType.Text),
					new SqlParameter("@MessageSendTime", SqlDbType.DateTime),
					new SqlParameter("@MessageIsRead", SqlDbType.Bit,1),
					new SqlParameter("@MessageReadTime", SqlDbType.DateTime)};
			parameters[0].Value = model.MessageParentId;
			parameters[1].Value = model.MessageTO;
			parameters[2].Value = model.MessageFrom;
			parameters[3].Value = model.MessageContent;
			parameters[4].Value = model.MessageSendTime;
			parameters[5].Value = model.MessageIsRead;
			parameters[6].Value = model.MessageReadTime;

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
		public bool Update(OnLineTest.Model.Message_table model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Message_table set ");
			strSql.Append("MessageParentId=@MessageParentId,");
			strSql.Append("MessageTO=@MessageTO,");
			strSql.Append("MessageFrom=@MessageFrom,");
			strSql.Append("MessageContent=@MessageContent,");
			strSql.Append("MessageSendTime=@MessageSendTime,");
			strSql.Append("MessageIsRead=@MessageIsRead,");
			strSql.Append("MessageReadTime=@MessageReadTime");
			strSql.Append(" where MessageId=@MessageId");
			SqlParameter[] parameters = {
					new SqlParameter("@MessageParentId", SqlDbType.Int,4),
					new SqlParameter("@MessageTO", SqlDbType.Int,4),
					new SqlParameter("@MessageFrom", SqlDbType.Int,4),
					new SqlParameter("@MessageContent", SqlDbType.Text),
					new SqlParameter("@MessageSendTime", SqlDbType.DateTime),
					new SqlParameter("@MessageIsRead", SqlDbType.Bit,1),
					new SqlParameter("@MessageReadTime", SqlDbType.DateTime),
					new SqlParameter("@MessageId", SqlDbType.Int,4)};
			parameters[0].Value = model.MessageParentId;
			parameters[1].Value = model.MessageTO;
			parameters[2].Value = model.MessageFrom;
			parameters[3].Value = model.MessageContent;
			parameters[4].Value = model.MessageSendTime;
			parameters[5].Value = model.MessageIsRead;
			parameters[6].Value = model.MessageReadTime;
			parameters[7].Value = model.MessageId;

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
		public bool Delete(int MessageId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Message_table ");
			strSql.Append(" where MessageId=@MessageId");
			SqlParameter[] parameters = {
					new SqlParameter("@MessageId", SqlDbType.Int,4)
			};
			parameters[0].Value = MessageId;

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
		public bool DeleteList(string MessageIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Message_table ");
			strSql.Append(" where MessageId in ("+MessageIdlist + ")  ");
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
		public OnLineTest.Model.Message_table GetModel(int MessageId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MessageId,MessageParentId,MessageTO,MessageFrom,MessageContent,MessageSendTime,MessageIsRead,MessageReadTime from Message_table ");
			strSql.Append(" where MessageId=@MessageId");
			SqlParameter[] parameters = {
					new SqlParameter("@MessageId", SqlDbType.Int,4)
			};
			parameters[0].Value = MessageId;

			OnLineTest.Model.Message_table model=new OnLineTest.Model.Message_table();
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
		public OnLineTest.Model.Message_table DataRowToModel(DataRow row)
		{
			OnLineTest.Model.Message_table model=new OnLineTest.Model.Message_table();
			if (row != null)
			{
				if(row["MessageId"]!=null && row["MessageId"].ToString()!="")
				{
					model.MessageId=int.Parse(row["MessageId"].ToString());
				}
				if(row["MessageParentId"]!=null && row["MessageParentId"].ToString()!="")
				{
					model.MessageParentId=int.Parse(row["MessageParentId"].ToString());
				}
				if(row["MessageTO"]!=null && row["MessageTO"].ToString()!="")
				{
					model.MessageTO=int.Parse(row["MessageTO"].ToString());
				}
				if(row["MessageFrom"]!=null && row["MessageFrom"].ToString()!="")
				{
					model.MessageFrom=int.Parse(row["MessageFrom"].ToString());
				}
				if(row["MessageContent"]!=null)
				{
					model.MessageContent=row["MessageContent"].ToString();
				}
				if(row["MessageSendTime"]!=null && row["MessageSendTime"].ToString()!="")
				{
					model.MessageSendTime=DateTime.Parse(row["MessageSendTime"].ToString());
				}
				if(row["MessageIsRead"]!=null && row["MessageIsRead"].ToString()!="")
				{
					if((row["MessageIsRead"].ToString()=="1")||(row["MessageIsRead"].ToString().ToLower()=="true"))
					{
						model.MessageIsRead=true;
					}
					else
					{
						model.MessageIsRead=false;
					}
				}
				if(row["MessageReadTime"]!=null && row["MessageReadTime"].ToString()!="")
				{
					model.MessageReadTime=DateTime.Parse(row["MessageReadTime"].ToString());
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
			strSql.Append("select MessageId,MessageParentId,MessageTO,MessageFrom,MessageContent,MessageSendTime,MessageIsRead,MessageReadTime ");
			strSql.Append(" FROM Message_table ");
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
			strSql.Append(" MessageId,MessageParentId,MessageTO,MessageFrom,MessageContent,MessageSendTime,MessageIsRead,MessageReadTime ");
			strSql.Append(" FROM Message_table ");
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
			strSql.Append("select count(1) FROM Message_table ");
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
				strSql.Append("order by T.MessageId desc");
			}
			strSql.Append(")AS Row, T.*  from Message_table T ");
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
			parameters[0].Value = "Message_table";
			parameters[1].Value = "MessageId";
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

