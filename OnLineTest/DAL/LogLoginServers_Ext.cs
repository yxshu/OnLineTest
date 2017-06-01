
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
using OnLineTest.Model;
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:LogLoginServers
    /// </summary>
    public partial class LogLoginServers
    {
        /// <summary>
        /// 得到用户最后一次的登录信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>loglogin</returns>
        public LogLogin GetCurrentLoglogin(int userid)
        {
            DataSet ds = new DataSet();
            ds = DbHelperSQL.Query("select top 1 * from loglogin where userid=" + userid + " order by loglogintime desc");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
    }
}

