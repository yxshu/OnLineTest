<%@ Page Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="EditUserAuthority.aspx.cs" Inherits="RightMaintenance_EditUserAuthority" Title="编辑用户权限" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/EditUserAuthority.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/EditUserAuthority.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <%--第一行新增部分--%>
    <div class="group" id="groupid">
        <%--组别标签--%>
        <div class="grouplab" id="grouplab_groupid">新增</div>
        <%--等级--%>
        <div class="rank" id="rankid">
            <%--等级标签--%>
            <div class="ranklab" id="ranklab_rankid">新增</div>
            <%--第一列--%>
            <span id="column_groupid_rankid_columnnum_1">
                <label for="group" title="请选择组别">组别</label>
                <select clientidmode="Static" id="group" runat="server">
                    <option value="-1" selected="selected">请选择</option>
                </select>
            </span>
            <%--第二列--%>
            <span id="column_groupid_rankid_columnnum_2">
                <label for="rank" title="请选择等级">等级</label>
                <select clientidmode="Static" id="rank" runat="server">
                    <option value="-1" selected="selected">请选择</option>
                </select>
            </span>
            <%--第三列--%>
            <span id="column_groupid_rankid_columnnum_3">
                <label for="authority" title="请选择权限">权限</label>
                <select clientidmode="Static" id="authority" runat="server">
                    <option value="-1" selected="selected">请选择</option>
                </select>
            </span>
            <%--第四列--%>
            <span id="column_groupid_rankid_columnnum_4">
                <label id="add">增加</label>
            </span>
        </div>
    </div>

    <%--页面载入中，将数据库中已经存在的用户权限载入--%>
    <%--    
    <div id="UserGroupId_1" class="group">
        <div id="grouplab_1" class="grouplab">超级管理员</div>
        <div id="UserRankId_1" class="rank">
            <div id="ranklab_1" class="ranklab">轮机助理</div>
            <span title="个人中心-二级目录" id="UserAuthorityId_32" >个人中心</span>
            <span title="设置等级-二级目录" id="UserAuthorityId_33">设置等级</span>
        </div>
    </div>
    --%>




    <asp:Repeater ID="rpGroup" OnItemDataBound="rpGroup_ItemDataBound" runat="server">
        <ItemTemplate>
            <div class="group" id="UserGroupId_<%#Eval("UserGroupId") %>">
                <%--组别标签--%>
                <div class="grouplab" id="grouplab_<%#Eval("UserGroupId") %>"><%#Eval("UserGroupName") %></div>
                <asp:Repeater ID="rpRank" OnItemDataBound="rpRank_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="rank" id="UserRankId_<%#Eval("UserRankId") %>">
                            <%--等级标签--%>
                            <div class="ranklab" id="ranklab_<%#Eval("UserRankId") %>"><%#Eval("UserRankName") %></div>
                            <asp:Repeater ID="rpItem" runat="server">
                                <ItemTemplate>
                                    <span class="column" id="<%#Eval("UserAuthorityId")%>" title="<%#Eval("AuthorityRemark") %>">
                                        <%#Eval("AuthorityName") %>
                                    </span>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>


