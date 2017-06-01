using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using OnLineTest.BLL;

public partial class RightMaintenance_EditAuthority : System.Web.UI.Page
{
    protected AuthorityManager authoritymanager = new AuthorityManager();
    protected void Page_Load(object sender, EventArgs e)
    {    //先邦定栏目
        if (!IsPostBack)
        {
            showdata(rpParent);
        }
    }

    public void showdata(Repeater rpParent)
    {
        DataSet ds = authoritymanager.GetList("AuthorityParentId=0 order by AuthorityOrderNum");
        rpParent.DataSource = ds;
        rpParent.DataBind();
    }

    public void rpParent_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpColumnNews = (Repeater)e.Item.FindControl("rpChild");
            //找到分类Repeater关联的数据项 
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            //提取分类ID 
            string strClassID = Convert.ToString(rowv["AuthorityId"]);
            //里面的Repeater
            DataSet ds = authoritymanager.GetList("AuthorityParentId=" + strClassID + "order by AuthorityOrderNum");
            rpColumnNews.DataSource = ds;
            rpColumnNews.DataBind();
        }

    }
}





