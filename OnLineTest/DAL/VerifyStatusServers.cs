using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:VerifyStatusServers
    /// </summary>
    public partial class VerifyStatusServers
    {
        public VerifyStatusServers()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("VerifyStatusId", "VerifyStatus");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int VerifyStatusId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from VerifyStatus");
            strSql.Append(" where VerifyStatusId=@VerifyStatusId");
            SqlParameter[] parameters = {
					new SqlParameter("@VerifyStatusId", SqlDbType.Int,4)
			};
            parameters[0].Value = VerifyStatusId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OnLineTest.Model.VerifyStatus model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into VerifyStatus(");
            strSql.Append("QuestionId,UserId,VerifyTimes,IsPass,VerifyTime)");
            strSql.Append(" values (");
            strSql.Append("@QuestionId,@UserId,@VerifyTimes,@IsPass,@VerifyTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@QuestionId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					
					new SqlParameter("@VerifyTimes", SqlDbType.Int,4),
					new SqlParameter("@IsPass", SqlDbType.Bit,1),
					new SqlParameter("@PassTime", SqlDbType.DateTime),
					new SqlParameter("@VerifyTime", SqlDbType.DateTime)
					};
            parameters[0].Value = model.QuestionId;

            parameters[1].Value = model.UserId;

            parameters[2].Value = model.VerifyTimes;
            parameters[3].Value = model.IsPass;

            parameters[4].Value = model.VerifyTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(OnLineTest.Model.VerifyStatus model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VerifyStatus set ");
            strSql.Append("QuestionId=@QuestionId,");

            strSql.Append("UserId=@UserId,");

            strSql.Append("VerifyTimes=@VerifyTimes,");
            strSql.Append("IsPass=@IsPass,");

            strSql.Append("VerifyTime=@VerifyTime,");

            strSql.Append(" where VerifyStatusId=@VerifyStatusId");
            SqlParameter[] parameters = {
                                 new SqlParameter("@QuestionId",SqlDbType.Int),
                                 new SqlParameter("@UserId",SqlDbType.Int),
                                 new SqlParameter("@VerifyTimes",SqlDbType.Int),
                                 new SqlParameter("@IsPass",SqlDbType.Bit),
                                 new SqlParameter("@VerifyTime",SqlDbType.DateTime),};
            parameters[0].Value = model.QuestionId;

            parameters[1].Value = model.UserId;

            parameters[2].Value = model.VerifyTimes;
            parameters[3].Value = model.IsPass;

            parameters[4].Value = model.VerifyTime;

            parameters[5].Value = model.VerifyStatusId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(int VerifyStatusId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from VerifyStatus ");
            strSql.Append(" where VerifyStatusId=@VerifyStatusId");
            SqlParameter[] parameters = {
					new SqlParameter("@VerifyStatusId", SqlDbType.Int,4)
			};
            parameters[0].Value = VerifyStatusId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string VerifyStatusIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from VerifyStatus ");
            strSql.Append(" where VerifyStatusId in (" + VerifyStatusIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public OnLineTest.Model.VerifyStatus GetModel(int VerifyStatusId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 VerifyStatusId,QuestionId,UserId,VerifyTimes,IsPass,VerifyTime from VerifyStatus ");
            strSql.Append(" where VerifyStatusId=@VerifyStatusId");
            SqlParameter[] parameters = {
					new SqlParameter("@VerifyStatusId", SqlDbType.Int,4)
			};
            parameters[0].Value = VerifyStatusId;

            OnLineTest.Model.VerifyStatus model = new OnLineTest.Model.VerifyStatus();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public OnLineTest.Model.VerifyStatus DataRowToModel(DataRow row)
        {
            OnLineTest.Model.VerifyStatus model = new OnLineTest.Model.VerifyStatus();
            if (row != null)
            {
                if (row["VerifyStatusId"] != null && row["VerifyStatusId"].ToString() != "")
                {
                    model.VerifyStatusId = int.Parse(row["VerifyStatusId"].ToString());
                }
                if (row["QuestionId"] != null && row["QuestionId"].ToString() != "")
                {
                    model.QuestionId = int.Parse(row["QuestionId"].ToString());
                }
                
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                
                if (row["VerifyTimes"] != null && row["VerifyTimes"].ToString() != "")
                {
                    model.VerifyTimes = int.Parse(row["VerifyTimes"].ToString());
                }
                if (row["IsPass"] != null && row["IsPass"].ToString() != "")
                {
                    if ((row["IsPass"].ToString() == "1") || (row["IsPass"].ToString().ToLower() == "true"))
                    {
                        model.IsPass = true;
                    }
                    else
                    {
                        model.IsPass = false;
                    }
                }
                
                if (row["VerifyTime"] != null && row["VerifyTime"].ToString() != "")
                {
                    model.VerifyTime = DateTime.Parse(row["VerifyTime"].ToString());
                }
                
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select VerifyStatusId,QuestionId,UserId,VerifyTimes,IsPass,VerifyTime ");
            strSql.Append(" FROM VerifyStatus ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" VerifyStatusId,QuestionId,UserId,VerifyTimes,IsPass,VerifyTime ");
            strSql.Append(" FROM VerifyStatus ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM VerifyStatus ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.VerifyStatusId desc");
            }
            strSql.Append(")AS Row, T.*  from VerifyStatus T ");
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
            parameters[0].Value = "VerifyStatus";
            parameters[1].Value = "VerifyStatusId";
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

