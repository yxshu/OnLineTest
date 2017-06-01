using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.IO;
using OnLineTest.Model;
using OnLineTest.BLL;

namespace OnLineTest.Web
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Users user = (Users)Session["User"]; //得到当前登录用户实例 
            }

        }
    }
}
