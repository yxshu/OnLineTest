using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
	/// <summary>
	/// 数据访问类:PaperCodesServers
	/// </summary>
	public partial class PaperCodesServers
	{
		public PaperCodesServers()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("PaperCode", "PaperCodes"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int PaperCode,int PaperCodeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PaperCodes");
			strSql.Append(" where PaperCode=@PaperCode and PaperCodeId=@PaperCodeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperCode", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4)			};
			parameters[0].Value = PaperCode;
			parameters[1].Value = PaperCodeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(OnLineTest.Model.PaperCodes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PaperCodes(");
			strSql.Append("SubjectId,PaperCode,ChineseName,PaperCodePassScore,PaperCodeTotalScore,TimeRange,PaperCodeRemark,IsVerified)");
			strSql.Append(" values (");
			strSql.Append("@SubjectId,@PaperCode,@ChineseName,@PaperCodePassScore,@PaperCodeTotalScore,@TimeRange,@PaperCodeRemark,@IsVerified)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SubjectId", SqlDbType.Int,4),
					new SqlParameter("@PaperCode", SqlDbType.Int,4),
					new SqlParameter("@ChineseName", SqlDbType.NVarChar,100),
					new SqlParameter("@PaperCodePassScore", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeTotalScore", SqlDbType.Int,4),
					new SqlParameter("@TimeRange", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeRemark", SqlDbType.Text),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1)};
			parameters[0].Value = model.SubjectId;
			parameters[1].Value = model.PaperCode;
			parameters[2].Value = model.ChineseName;
			parameters[3].Value = model.PaperCodePassScore;
			parameters[4].Value = model.PaperCodeTotalScore;
			parameters[5].Value = model.TimeRange;
			parameters[6].Value = model.PaperCodeRemark;
			parameters[7].Value = model.IsVerified;

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
		public bool Update(OnLineTest.Model.PaperCodes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PaperCodes set ");
			strSql.Append("SubjectId=@SubjectId,");
			strSql.Append("ChineseName=@ChineseName,");
			strSql.Append("PaperCodePassScore=@PaperCodePassScore,");
			strSql.Append("PaperCodeTotalScore=@PaperCodeTotalScore,");
			strSql.Append("TimeRange=@TimeRange,");
			strSql.Append("PaperCodeRemark=@PaperCodeRemark,");
			strSql.Append("IsVerified=@IsVerified");
			strSql.Append(" where PaperCodeId=@PaperCodeId");
			SqlParameter[] parameters = {
					new SqlParameter("@SubjectId", SqlDbType.Int,4),
					new SqlParameter("@ChineseName", SqlDbType.NVarChar,100),
					new SqlParameter("@PaperCodePassScore", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeTotalScore", SqlDbType.Int,4),
					new SqlParameter("@TimeRange", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeRemark", SqlDbType.Text),
					new SqlParameter("@IsVerified", SqlDbType.Bit,1),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
					new SqlParameter("@PaperCode", SqlDbType.Int,4)};
			parameters[0].Value = model.SubjectId;
			parameters[1].Value = model.ChineseName;
			parameters[2].Value = model.PaperCodePassScore;
			parameters[3].Value = model.PaperCodeTotalScore;
			parameters[4].Value = model.TimeRange;
			parameters[5].Value = model.PaperCodeRemark;
			parameters[6].Value = model.IsVerified;
			parameters[7].Value = model.PaperCodeId;
			parameters[8].Value = model.PaperCode;

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
		public bool Delete(int PaperCodeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PaperCodes ");
			strSql.Append(" where PaperCodeId=@PaperCodeId");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4)
			};
			parameters[0].Value = PaperCodeId;

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
		public bool Delete(int PaperCode,int PaperCodeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PaperCodes ");
			strSql.Append(" where PaperCode=@PaperCode and PaperCodeId=@PaperCodeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperCode", SqlDbType.Int,4),
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4)			};
			parameters[0].Value = PaperCode;
			parameters[1].Value = PaperCodeId;

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
		public bool DeleteList(string PaperCodeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PaperCodes ");
			strSql.Append(" where PaperCodeId in ("+PaperCodeIdlist + ")  ");
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
		public OnLineTest.Model.PaperCodes GetModel(int PaperCodeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 PaperCodeId,SubjectId,PaperCode,ChineseName,PaperCodePassScore,PaperCodeTotalScore,TimeRange,PaperCodeRemark,IsVerified from PaperCodes ");
			strSql.Append(" where PaperCodeId=@PaperCodeId");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4)
			};
			parameters[0].Value = PaperCodeId;

			OnLineTest.Model.PaperCodes model=new OnLineTest.Model.PaperCodes();
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
		public OnLineTest.Model.PaperCodes DataRowToModel(DataRow row)
		{
			OnLineTest.Model.PaperCodes model=new OnLineTest.Model.PaperCodes();
			if (row != null)
			{
				if(row["PaperCodeId"]!=null && row["PaperCodeId"].ToString()!="")
				{
					model.PaperCodeId=int.Parse(row["PaperCodeId"].ToString());
				}
				if(row["SubjectId"]!=null && row["SubjectId"].ToString()!="")
				{
					model.SubjectId=int.Parse(row["SubjectId"].ToString());
				}
				if(row["PaperCode"]!=null && row["PaperCode"].ToString()!="")
				{
					model.PaperCode=int.Parse(row["PaperCode"].ToString());
				}
				if(row["ChineseName"]!=null)
				{
					model.ChineseName=row["ChineseName"].ToString();
				}
				if(row["PaperCodePassScore"]!=null && row["PaperCodePassScore"].ToString()!="")
				{
					model.PaperCodePassScore=int.Parse(row["PaperCodePassScore"].ToString());
				}
				if(row["PaperCodeTotalScore"]!=null && row["PaperCodeTotalScore"].ToString()!="")
				{
					model.PaperCodeTotalScore=int.Parse(row["PaperCodeTotalScore"].ToString());
				}
				if(row["TimeRange"]!=null && row["TimeRange"].ToString()!="")
				{
					model.TimeRange=int.Parse(row["TimeRange"].ToString());
				}
				if(row["PaperCodeRemark"]!=null)
				{
					model.PaperCodeRemark=row["PaperCodeRemark"].ToString();
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
			strSql.Append("select PaperCodeId,SubjectId,PaperCode,ChineseName,PaperCodePassScore,PaperCodeTotalScore,TimeRange,PaperCodeRemark,IsVerified ");
			strSql.Append(" FROM PaperCodes ");
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
			strSql.Append(" PaperCodeId,SubjectId,PaperCode,ChineseName,PaperCodePassScore,PaperCodeTotalScore,TimeRange,PaperCodeRemark,IsVerified ");
			strSql.Append(" FROM PaperCodes ");
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
			strSql.Append("select count(1) FROM PaperCodes ");
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
				strSql.Append("order by T.PaperCodeId desc");
			}
			strSql.Append(")AS Row, T.*  from PaperCodes T ");
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
			parameters[0].Value = "PaperCodes";
			parameters[1].Value = "PaperCodeId";
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

