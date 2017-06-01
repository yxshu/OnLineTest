using OnLineTest.Model;
using OnLineTest.BLL;
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
using System.Collections.Generic;

public partial class RightMaintenance_EditUserAuthority : System.Web.UI.Page
{
    //定义一系列的变量
    Users user = null;//用户
    List<UserRank> userranklist = null;//用户等级
    List<UserGroup> usergrouplist = null;//用户组
    //List<UserAuthority> userauthoritylist = null;//用户权限
    UserGroupManager usergroupmanager = new UserGroupManager();
    UserRankManager userrankmanager = new UserRankManager();
    UserAuthorityManager userauthoritymanager = new UserAuthorityManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            user = (Users)Session["User"]; //得到当前登录用户实例 ，因为存在母版页，所以不用进行判断
            usergrouplist = usergroupmanager.GetModelList("");
            userranklist = userrankmanager.GetModelList("");
            group.DataSource = usergrouplist;//为 组别 的下拉列表填数据
            group.DataValueField = "UserGroupId";
            group.DataTextField = "UserGroupName";
            group.DataBind();
            //group.Items.Add("<option value='-1' selected='selected'>请选择</option>");
            rank.DataSource = userranklist;//为 等级 的下拉列表填数据
            rank.DataValueField = "UserRankId";
            rank.DataTextField = "UserRankName";
            rank.DataBind();
            //rank.Items.Add("<option value='-1' selected='selected'>请选择</option>");
            rpGroup.DataSource = usergroupmanager.GetModelList("UserGroupId in (select distinct(UserGroupId) from UserAuthority)");//仅取得用户权限表中存在的群组
            rpGroup.DataBind();
        }
    }
    /// <summary>
    /// 每次用户组绑定一条数据时，启动的方法（给用户等级提供数据）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rpGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpRank = (Repeater)e.Item.FindControl("rpRank");
            //找到分类Repeater关联的数据项 
            UserGroup rowv = (UserGroup)e.Item.DataItem;
            rpRank.DataSource = userrankmanager.GetModelList("UserRankId in(select distinct(UserRankId) from UserAuthority where UserGroupId=" + rowv.UserGroupId.ToString() + ")");//仅取得对应群组下存在的用户等级
            rpRank.DataBind();
        }
    }
    /// <summary>
    /// 每次用户等级绑定一条数据时，启动的方法（为用户等级提取已经存在的权限数据）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rpRank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Repeater rpItem = (Repeater)e.Item.FindControl("rpItem");
            //找到分类Repeater关联的数据项 
            UserRank rowv = (UserRank)e.Item.DataItem;
            //提取分类ID 
            string usergroupid = ((UserGroup)(((RepeaterItem)(((Control)(sender)).Parent)).DataItem)).UserGroupId.ToString();
            string userrankid = rowv.UserRankId.ToString();
            rpItem.DataSource = userauthoritymanager.GetListLeftOuterJoinAuthority("UserGroupId='" + usergroupid + "' and UserRankId='" + userrankid + "'");
            rpItem.DataBind();
        }
    }
}
