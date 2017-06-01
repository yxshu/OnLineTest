using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:UserGroupServers
    /// </summary>
    public partial class UserGroupServers
    {
        public UserGroupServers()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserGroupId", "UserGroup");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UserGroupName, int UserGroupId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserGroup");
            strSql.Append(" where UserGroupName=@UserGroupName and UserGroupId=@UserGroupId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserGroupName", SqlDbType.VarChar,20),
					new SqlParameter("@UserGroupId", SqlDbType.Int,4)			};
            parameters[0].Value = UserGroupName;
            parameters[1].Value = UserGroupId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OnLineTest.Model.UserGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserGroup(");
            strSql.Append("UserGroupName,UserGroupRemark)");
            strSql.Append(" values (");
            strSql.Append("@UserGroupName,@UserGroupRemark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserGroupName", SqlDbType.VarChar,20),
					new SqlParameter("@UserGroupRemark", SqlDbType.Text)};
            parameters[0].Value = model.UserGroupName;
            parameters[1].Value = model.UserGroupRemark;

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
        public bool Update(OnLineTest.Model.UserGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserGroup set ");
            strSql.Append("UserGroupRemark=@UserGroupRemark");
            strSql.Append(" where UserGroupId=@UserGroupId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserGroupRemark", SqlDbType.Text),
					new SqlParameter("@UserGroupId", SqlDbType.Int,4),
					new SqlParameter("@UserGroupName", SqlDbType.VarChar,20)};
            parameters[0].Value = model.UserGroupRemark;
            parameters[1].Value = model.UserGroupId;
            parameters[2].Value = model.UserGroupName;

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
        public bool Delete(int UserGroupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserGroup ");
            strSql.Append(" where UserGroupId=@UserGroupId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserGroupId", SqlDbType.Int,4)
			};
            parameters[0].Value = UserGroupId;

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
        public bool Delete(string UserGroupName, int UserGroupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserGroup ");
            strSql.Append(" where UserGroupName=@UserGroupName and UserGroupId=@UserGroupId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserGroupName", SqlDbType.VarChar,20),
					new SqlParameter("@UserGroupId", SqlDbType.Int,4)			};
            parameters[0].Value = UserGroupName;
            parameters[1].Value = UserGroupId;

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
        public bool DeleteList(string UserGroupIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserGroup ");
            strSql.Append(" where UserGroupId in (" + UserGroupIdlist + ")  ");
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
        public OnLineTest.Model.UserGroup GetModel(int UserGroupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserGroupId,UserGroupName,UserGroupRemark from UserGroup ");
            strSql.Append(" where UserGroupId=@UserGroupId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserGroupId", SqlDbType.Int,4)
			};
            parameters[0].Value = UserGroupId;

            OnLineTest.Model.UserGroup model = new OnLineTest.Model.UserGroup();
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
        /// 通过用户组的名称从数据库中查询一条记录并被保存为一个UserGroup对象
        /// </summary>
        /// <param name="usergroupname">用户组名称</param>
        /// <returns>用户组对象</returns>
        public OnLineTest.Model.UserGroup GetModel(string usergroupname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserGroupId,UserGroupName,UserGroupRemark from UserGroup ");
            strSql.Append(" where UserGroupName=@usergroupname");
            SqlParameter[] parameters = {
					new SqlParameter("@usergroupname",SqlDbType.VarChar)
			};
            parameters[0].Value = usergroupname;

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
        public OnLineTest.Model.UserGroup DataRowToModel(DataRow row)
        {
            OnLineTest.Model.UserGroup model = new OnLineTest.Model.UserGroup();
            if (row != null)
            {
                if (row["UserGroupId"] != null && row["UserGroupId"].ToString() != "")
                {
                    model.UserGroupId = int.Parse(row["UserGroupId"].ToString());
                }
                if (row["UserGroupName"] != null)
                {
                    model.UserGroupName = row["UserGroupName"].ToString();
                }
                if (row["UserGroupRemark"] != null)
                {
                    model.UserGroupRemark = row["UserGroupRemark"].ToString();
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
            strSql.Append("select UserGroupId,UserGroupName,UserGroupRemark ");
            strSql.Append(" FROM UserGroup ");
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
            strSql.Append(" UserGroupId,UserGroupName,UserGroupRemark ");
            strSql.Append(" FROM UserGroup ");
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
            strSql.Append("select count(1) FROM UserGroup ");
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
                strSql.Append("order by T.UserGroupId desc");
            }
            strSql.Append(")AS Row, T.*  from UserGroup T ");
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
            parameters[0].Value = "UserGroup";
            parameters[1].Value = "UserGroupId";
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

