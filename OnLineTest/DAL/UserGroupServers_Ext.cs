
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
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserGroupId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserGroup");
            strSql.Append(" where UserGroupId=@UserGroupId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserGroupId", SqlDbType.Int,4)			};
            parameters[0].Value = UserGroupId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
	}
}

