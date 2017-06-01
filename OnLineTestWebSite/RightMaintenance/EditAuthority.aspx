<%@ Page Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true"
    CodeFile="EditAuthority.aspx.cs" Inherits="RightMaintenance_EditAuthority" Title="编辑根权限" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/EditAuthority.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="../Scripts/EditAuthority.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
<div id="AuthorityContent">
    <!---用户根权限--->
    <asp:Repeater ID="rpParent" runat="server" OnItemDataBound="rpParent_ItemDataBound">
        <HeaderTemplate>
            <div id="rpheader">
                <span class="id">序 号</span><span class="name">名 称 </span><span class="handlerpage" id="rpheaderrowpage">
                    处理页面 </span><span class="score">分 值 </span><span class="remark">备 注 </span>
                <span class="ordernum" id="rpheaderrowordernum">顺序号 </span><span class="edit" id="rpheaderrowedit">编 辑 </span>
                <span class="delete" id="rpheaderrowdelete">删 除 </span>
            </div>
        </HeaderTemplate>
        <ItemTemplate>
            <!---每组权限-->
            <div class="rpitem">
                <!---每组的父权限-->
                <div class="parentrow"  id="<%#Eval("AuthorityId") %>">
                    <span class="id">
                        <%# Container.ItemIndex+1%>
                        组 </span><span class="name">
                            <%# DataBinder.Eval(Container.DataItem,"AuthorityName") %>
                        </span><span class="handlerpage">
                            <%# DataBinder.Eval(Container.DataItem,"AuthorityHandlerPage") %>
                        </span><span class="score">
                            <%# DataBinder.Eval(Container.DataItem,"AuthorityScore") %>
                        </span><span class="remark">
                            <%# DataBinder.Eval(Container.DataItem,"AuthorityRemark")%>
                        </span><span class="ordernum">
                            <%# DataBinder.Eval(Container.DataItem,"AuthorityOrderNum") %>
                        </span><span class="edit"><label title='<%#Eval("AuthorityId") %>'>编辑</label>
                            </span><span class="delete"><label title='<%#Eval("AuthorityId") %>'>删除</label>
                        </span>
                </div>
                <!---每组的子权限--->
                <div class="Child">
                    <asp:Repeater ID="rpChild" runat="server">
                        <ItemTemplate>
                            <div class="childrow" id="<%#Eval("AuthorityId") %>">
                                <span class="id">
                                    <%# Container.ItemIndex+1%>
                                </span><span class="name">
                                    <%# DataBinder.Eval(Container.DataItem,"AuthorityName") %>
                                </span><span class="handlerpage">
                                    <%# DataBinder.Eval(Container.DataItem,"AuthorityHandlerPage") %>
                                </span><span class="score">
                                    <%# DataBinder.Eval(Container.DataItem,"AuthorityScore") %>
                                </span><span class="remark">
                                    <%# DataBinder.Eval(Container.DataItem,"AuthorityRemark")%>
                                </span><span class="ordernum">
                                    <%# DataBinder.Eval(Container.DataItem,"AuthorityOrderNum") %>
                                </span><span class="edit"><label title='<%#Eval("AuthorityId") %>'>编辑</label>
                            </span><span class="delete"><label title='<%#Eval("AuthorityId") %>'>删除</label>
                        </span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                
            </div>
        </ItemTemplate>
        <FooterTemplate>
            <div id="rpfooter" title="add">
            新&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;增
            </div>
        </FooterTemplate>
    </asp:Repeater>
    </div>
    <div id="FloatDiv"></div>
</asp:Content>
