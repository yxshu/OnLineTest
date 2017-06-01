using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestAndPractice_AddQuestion : System.Web.UI.Page
{
    public int QuestionId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!int.TryParse(Context.Request.QueryString["questionid"].ToString(), out QuestionId))
        {
            QuestionId = -1;
        }

    }
}