using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnLineTest.BLL;


public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string ms = string.Empty;
        //if (HttpRuntime.Cache["ms"]==null)
        // BLL.ErrorCodes Common.SqlDenpendencyCache.SetCache("ms", DateTime.Now.Millisecond.ToString(), BLL.SqlDenpendencyCache.initalSqlCacheDependency("Authority"));
        //ms = BLL.SqlDenpendencyCache.GetCache("ms").ToString();
        //Response.Write(ms);
    }
}
