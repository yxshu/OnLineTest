using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:QuestionServers
	/// </summary>
	public partial class QuestionServers
	{
		public QuestionServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("QuestionId", "Question"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int QuestionId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Question");
			strSql.Append(" where QuestionId=@QuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@QuestionId", SqlDbType.Int,4)
			};
			parameters[0].Value = QuestionId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.Question model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Question(");
			strSql.Append("QuestionTitle,AnswerA,AnswerB,AnswerC,AnswerD,CorrectAnswer,ImageAddress,DifficultyId,UserId,UpLoadTime,VerifyTimes,IsVerified,IsDelte,IsSupported,IsDeSupported,PaperCodeId,TextBookId,ChapterId,PastExamPaperId,PastExamQuestionId)");
			strSql.Append(" values (");
			strSql.Append("@QuestionTitle,@AnswerA,@AnswerB,@AnswerC,@AnswerD,@CorrectAnswer,@ImageAddress,@DifficultyId,@UserId,@UpLoadTime,@VerifyTimes,@IsVerified,@IsDelte,@IsSupported,@IsDeSupported,@PaperCodeId,@TextBookId,@ChapterId,@PastExamPaperId,@PastExamQuestionId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@QuestionTitle", SqlDbType.Text),
					new SqlParameter("@AnswerA", SqlDbType.Text),
					new SqlParameter("@AnswerB", SqlDbType.Text),
					new SqlParameter("@AnswerC", SqlDbType.Text),
					new SqlParameter("@AnswerD", SqlDbType.Text),
					new SqlParameter("@CorrectAnswer", SqlDbType.Int,4),
					new SqlParameter("@ImageAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@DifficultyId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UpLoadTime", SqlDbType.DateTime),
					new SqlParameter("@VerifyTimes", SqlDbType.Int,4),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1),
					new SqlParameter("@IsDelte", SqlDbType.Bit,1),
					new SqlParameter("@IsSupported", SqlDbType.Int,4),
					new SqlParameter("@IsDeSupported", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@TextBookId", SqlDbType.Int,4),
					new SqlParameter("@ChapterId", SqlDbType.Int,4),
					new SqlParameter("@PastExamPaperId", SqlDbType.Int,4),
					new SqlParameter("@PastExamQuestionId", SqlDbType.Int,4)};
			parameters[0].Value = model.QuestionTitle;
			parameters[1].Value = model.AnswerA;
			parameters[2].Value = model.AnswerB;
			parameters[3].Value = model.AnswerC;
			parameters[4].Value = model.AnswerD;
			parameters[5].Value = model.CorrectAnswer;
			parameters[6].Value = model.ImageAddress;
			parameters[7].Value = model.DifficultyId;
			parameters[8].Value = model.UserId;
			parameters[9].Value = model.UpLoadTime;
			parameters[10].Value = model.VerifyTimes;
			parameters[11].Value = model.IsVerified;
			parameters[12].Value = model.IsDelte;
			parameters[13].Value = model.IsSupported;
			parameters[14].Value = model.IsDeSupported;
			parameters[15].Value = model.PaperCodeId;
			parameters[16].Value = model.TextBookId;
			parameters[17].Value = model.ChapterId;
			parameters[18].Value = model.PastExamPaperId;
			parameters[19].Value = model.PastExamQuestionId;

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
		public bool Update(OnLineTest.Model.Question model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Question set ");
			strSql.Append("QuestionTitle=@QuestionTitle,");
			strSql.Append("AnswerA=@AnswerA,");
			strSql.Append("AnswerB=@AnswerB,");
			strSql.Append("AnswerC=@AnswerC,");
			strSql.Append("AnswerD=@AnswerD,");
			strSql.Append("CorrectAnswer=@CorrectAnswer,");
			strSql.Append("ImageAddress=@ImageAddress,");
			strSql.Append("DifficultyId=@DifficultyId,");
			strSql.Append("UserId=@UserId,");
			strSql.Append("UpLoadTime=@UpLoadTime,");
			strSql.Append("VerifyTimes=@VerifyTimes,");
			strSql.Append("IsVerified=@IsVerified,");
			strSql.Append("IsDelte=@IsDelte,");
			strSql.Append("IsSupported=@IsSupported,");
			strSql.Append("IsDeSupported=@IsDeSupported,");
			strSql.Append("PaperCodeId=@PaperCodeId,");
			strSql.Append("TextBookId=@TextBookId,");
			strSql.Append("ChapterId=@ChapterId,");
			strSql.Append("PastExamPaperId=@PastExamPaperId,");
			strSql.Append("PastExamQuestionId=@PastExamQuestionId");
			strSql.Append(" where QuestionId=@QuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@QuestionTitle", SqlDbType.Text),
					new SqlParameter("@AnswerA", SqlDbType.Text),
					new SqlParameter("@AnswerB", SqlDbType.Text),
					new SqlParameter("@AnswerC", SqlDbType.Text),
					new SqlParameter("@AnswerD", SqlDbType.Text),
					new SqlParameter("@CorrectAnswer", SqlDbType.Int,4),
					new SqlParameter("@ImageAddress", SqlDbType.NVarChar,100),
					new SqlParameter("@DifficultyId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UpLoadTime", SqlDbType.DateTime),
					new SqlParameter("@VerifyTimes", SqlDbType.Int,4),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1),
					new SqlParameter("@IsDelte", SqlDbType.Bit,1),
					new SqlParameter("@IsSupported", SqlDbType.Int,4),
					new SqlParameter("@IsDeSupported", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@TextBookId", SqlDbType.Int,4),
					new SqlParameter("@ChapterId", SqlDbType.Int,4),
					new SqlParameter("@PastExamPaperId", SqlDbType.Int,4),
					new SqlParameter("@PastExamQuestionId", SqlDbType.Int,4),
					new SqlParameter("@QuestionId", SqlDbType.Int,4)};
			parameters[0].Value = model.QuestionTitle;
			parameters[1].Value = model.AnswerA;
			parameters[2].Value = model.AnswerB;
			parameters[3].Value = model.AnswerC;
			parameters[4].Value = model.AnswerD;
			parameters[5].Value = model.CorrectAnswer;
			parameters[6].Value = model.ImageAddress;
			parameters[7].Value = model.DifficultyId;
			parameters[8].Value = model.UserId;
			parameters[9].Value = model.UpLoadTime;
			parameters[10].Value = model.VerifyTimes;
			parameters[11].Value = model.IsVerified;
			parameters[12].Value = model.IsDelte;
			parameters[13].Value = model.IsSupported;
			parameters[14].Value = model.IsDeSupported;
			parameters[15].Value = model.PaperCodeId;
			parameters[16].Value = model.TextBookId;
			parameters[17].Value = model.ChapterId;
			parameters[18].Value = model.PastExamPaperId;
			parameters[19].Value = model.PastExamQuestionId;
			parameters[20].Value = model.QuestionId;

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
		public bool Delete(int QuestionId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Question ");
			strSql.Append(" where QuestionId=@QuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@QuestionId", SqlDbType.Int,4)
			};
			parameters[0].Value = QuestionId;

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
		public bool DeleteList(string QuestionIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Question ");
			strSql.Append(" where QuestionId in ("+QuestionIdlist + ")  ");
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
		public OnLineTest.Model.Question GetModel(int QuestionId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 QuestionId,QuestionTitle,AnswerA,AnswerB,AnswerC,AnswerD,CorrectAnswer,ImageAddress,DifficultyId,UserId,UpLoadTime,VerifyTimes,IsVerified,IsDelte,IsSupported,IsDeSupported,PaperCodeId,TextBookId,ChapterId,PastExamPaperId,PastExamQuestionId from Question ");
			strSql.Append(" where QuestionId=@QuestionId");
			SqlParameter[] parameters = {
					new SqlParameter("@QuestionId", SqlDbType.Int,4)
			};
			parameters[0].Value = QuestionId;

			OnLineTest.Model.Question model=new OnLineTest.Model.Question();
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
		public OnLineTest.Model.Question DataRowToModel(DataRow row)
		{
			OnLineTest.Model.Question model=new OnLineTest.Model.Question();
			if (row != null)
			{
				if(row["QuestionId"]!=null && row["QuestionId"].ToString()!="")
				{
					model.QuestionId=int.Parse(row["QuestionId"].ToString());
				}
				if(row["QuestionTitle"]!=null)
				{
					model.QuestionTitle=row["QuestionTitle"].ToString();
				}
				if(row["AnswerA"]!=null)
				{
					model.AnswerA=row["AnswerA"].ToString();
				}
				if(row["AnswerB"]!=null)
				{
					model.AnswerB=row["AnswerB"].ToString();
				}
				if(row["AnswerC"]!=null)
				{
					model.AnswerC=row["AnswerC"].ToString();
				}
				if(row["AnswerD"]!=null)
				{
					model.AnswerD=row["AnswerD"].ToString();
				}
				if(row["CorrectAnswer"]!=null && row["CorrectAnswer"].ToString()!="")
				{
					model.CorrectAnswer=int.Parse(row["CorrectAnswer"].ToString());
				}
				if(row["ImageAddress"]!=null)
				{
					model.ImageAddress=row["ImageAddress"].ToString();
				}
				if(row["DifficultyId"]!=null && row["DifficultyId"].ToString()!="")
				{
					model.DifficultyId=int.Parse(row["DifficultyId"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["UpLoadTime"]!=null && row["UpLoadTime"].ToString()!="")
				{
					model.UpLoadTime=DateTime.Parse(row["UpLoadTime"].ToString());
				}
				if(row["VerifyTimes"]!=null && row["VerifyTimes"].ToString()!="")
				{
					model.VerifyTimes=int.Parse(row["VerifyTimes"].ToString());
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
				if(row["IsDelte"]!=null && row["IsDelte"].ToString()!="")
				{
					if((row["IsDelte"].ToString()=="1")||(row["IsDelte"].ToString().ToLower()=="true"))
					{
						model.IsDelte=true;
					}
					else
					{
						model.IsDelte=false;
					}
				}
				if(row["IsSupported"]!=null && row["IsSupported"].ToString()!="")
				{
					model.IsSupported=int.Parse(row["IsSupported"].ToString());
				}
				if(row["IsDeSupported"]!=null && row["IsDeSupported"].ToString()!="")
				{
					model.IsDeSupported=int.Parse(row["IsDeSupported"].ToString());
				}
				if(row["PaperCodeId"]!=null && row["PaperCodeId"].ToString()!="")
				{
					model.PaperCodeId=int.Parse(row["PaperCodeId"].ToString());
				}
				if(row["TextBookId"]!=null && row["TextBookId"].ToString()!="")
				{
					model.TextBookId=int.Parse(row["TextBookId"].ToString());
				}
				if(row["ChapterId"]!=null && row["ChapterId"].ToString()!="")
				{
					model.ChapterId=int.Parse(row["ChapterId"].ToString());
				}
				if(row["PastExamPaperId"]!=null && row["PastExamPaperId"].ToString()!="")
				{
					model.PastExamPaperId=int.Parse(row["PastExamPaperId"].ToString());
				}
				if(row["PastExamQuestionId"]!=null && row["PastExamQuestionId"].ToString()!="")
				{
					model.PastExamQuestionId=int.Parse(row["PastExamQuestionId"].ToString());
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
			strSql.Append("select QuestionId,QuestionTitle,AnswerA,AnswerB,AnswerC,AnswerD,CorrectAnswer,ImageAddress,DifficultyId,UserId,UpLoadTime,VerifyTimes,IsVerified,IsDelte,IsSupported,IsDeSupported,PaperCodeId,TextBookId,ChapterId,PastExamPaperId,PastExamQuestionId ");
			strSql.Append(" FROM Question ");
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
			strSql.Append(" QuestionId,QuestionTitle,AnswerA,AnswerB,AnswerC,AnswerD,CorrectAnswer,ImageAddress,DifficultyId,UserId,UpLoadTime,VerifyTimes,IsVerified,IsDelte,IsSupported,IsDeSupported,PaperCodeId,TextBookId,ChapterId,PastExamPaperId,PastExamQuestionId ");
			strSql.Append(" FROM Question ");
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
			strSql.Append("select count(1) FROM Question ");
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
				strSql.Append("order by T.QuestionId desc");
			}
			strSql.Append(")AS Row, T.*  from Question T ");
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
			parameters[0].Value = "Question";
			parameters[1].Value = "QuestionId";
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

