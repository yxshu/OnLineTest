using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnLineTest.BLL;

public partial class TestAndPractice_AddQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string questionid = Context.Request.QueryString["questionid"].ToString();
        HiddenField1.Value = questionid;
    }
}