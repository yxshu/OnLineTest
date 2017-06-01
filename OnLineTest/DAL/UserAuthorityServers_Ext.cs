
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;
using OnLineTest.Model;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:UserAuthorityServers
    /// </summary>
    public partial class UserAuthorityServers
    {
        public bool Exists(UserGroup usergroup,UserRank userrank, Authority authority)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserAuthority");
            strSql.Append(" where  AuthorityId=@AuthorityId and UserGroupId=@UserGroupId and UserRankId=@UserRankId");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4),
            new SqlParameter("@UserGroupId", SqlDbType.Int,4),
            new SqlParameter("@UserRankId", SqlDbType.Int,4)
			};
            parameters[0].Value = authority.AuthorityId;
            parameters[1].Value = usergroup.UserGroupId;
            parameters[2].Value = userrank.UserRankId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);

        }

        public bool Exists(int usergroupid,int userrankid,int authorityid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserAuthority");
            strSql.Append(" where  AuthorityId=@AuthorityId and UserGroupId=@UserGroupId and UserRankId=@UserRankId");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4),
            new SqlParameter("@UserGroupId", SqlDbType.Int,4),
            new SqlParameter("@UserRankId", SqlDbType.Int,4)
			};
            parameters[0].Value = authorityid;
            parameters[1].Value = usergroupid;
            parameters[2].Value = userrankid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 判断某项权限是否被引用
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool Exists(Authority authority)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserAuthority");
            strSql.Append(" where  AuthorityId=@AuthorityId");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4),
			};
            parameters[0].Value = authority.AuthorityId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 获得用户权限列表并向右联合查询到对应的Authority
        /// </summary>
        public DataSet GetListLeftOuterJoinAuthority(string strWhere)
        {
            //select * from UserAuthority as u   left outer join Authority as a on u.AuthorityId=a.AuthorityId where  UserGroupId='@param' and UserRankId='@param'
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM UserAuthority as u ");
            strSql.Append("left outer join Authority as a ");
            strSql.Append("on u.AuthorityId=a.AuthorityId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

