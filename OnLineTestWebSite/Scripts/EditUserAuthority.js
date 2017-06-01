//申明一个对象
function userauthority() {
    this.UserAuthorityId = null,
     this.AuthorityId = null,
     this.UserGroupId = null,
     this.UserRankId = null,
     this.UserAuthoriryRemark = null
};
//---------------------------------文档载入完成以后运行---------------------------------------
$(document).ready(function () {
    var groupid, rankid;
    //自定义对象

    //给新增权限的下拉列表填充数据
    authoritySelectGetOption();

    //群组
    $(".group").mouseenter(function () {
        groupid = $(this).attr("id");
        $(this).css({ "border": " solid 1px #ff9924" });
        $("#grouplab_" + groupid).css({ "color": "#FF9924" });
    }).mouseleave(function () {
        $(this).css({ "border": " solid 1px #DDD" });
        $("#grouplab_" + groupid).css({ "color": "#999999" });
    });

    //等级
    $(".rank").mouseenter(function () {
        rankid = $(this).attr("id");
        $(this).css({ "border": " solid 1px #ff9924" });
        $(this).children("#ranklab_" + rankid).css({ "color": "#FF9924" });
    }).mouseleave(function () {
        $(this).css({ "border": "solid 1px #DDD" });
        $(this).children("#ranklab_" + rankid).css({ "color": "#999999" });
    });

    //每一个权限列
    ColumnEvent($(".column"));

    //群组下拉列表
    $("#column_groupid_rankid_columnnum_1").children("select").change(authoritySelectGetOption);

    //等级下拉列表
    $("#column_groupid_rankid_columnnum_2").children("select").change(authoritySelectGetOption);

    //新增按钮的动作
    $("#add").click(function () {
        var result;
        var newUserAuthority = new userauthority();
        var UserGroupId = $("#column_groupid_rankid_columnnum_1").children("select").val();
        var UserRankId = $("#column_groupid_rankid_columnnum_2").children("select").val();
        var AuthorityId = $("#column_groupid_rankid_columnnum_3").children("select").val();
        if (UserGroupId > 0 && UserRankId > 0 && AuthorityId > 0) {
            newUserAuthority.AuthorityId = AuthorityId;
            newUserAuthority.UserGroupId = UserGroupId;
            newUserAuthority.UserRankId = UserRankId;
            result = hanlerUserAuthority(newUserAuthority);
            if (result == 0 || result == -2) {
                alert("添加权限失败");
            } else {
                newUserAuthority.UserAuthorityId = result;
                AddFormat(newUserAuthority);
            }
        }
    });
});

//----------------------------------下面自定义函数区---------------------------------------
//为每一个用户权限添加鼠标事件
function ColumnEvent(columnDIVArray) {
    columnDIVArray.mouseenter(function () {
        var column = $(this);
        var ua = new userauthority();
        ua.UserAuthorityId = $(this).attr("id");
        var del = $("<label>删除</label>");
        del.css({
            "width": $(this).width(),
            "height": $(this).height() * 0.4,
            "background-color": "#999999",
            "cursor": "pointer",
            "color": "White",
            "line-height": "40px"
        });
        $(this).css({
            "border": "solid 2px #FF9924",
            "color": "#999999",
        });
        $(this).prepend(del);
        del.click(function () {
            var result = hanlerUserAuthority(ua);
            if (result == 0 || result == -1) {
                alert("删除失败。");
            } else {
                format(column);
            }
        });
    }).mouseleave(function () {
        $(this).css({ "border": "solid 1px #DDDDDD", "color": "Black", "line-height": "100px" });
        $(this).children("label").remove();
    }).each(function () {
        $(this).css({ "background-color": "rgb(" + Math.ceil(Math.random() * 254) + "," + Math.ceil(Math.random() * 254) + "," + Math.ceil(Math.random() * 254) + ")" });
    });
}

//为用户权限 下拉列表 提供选项数据
function authoritySelectGetOption() {
    var optiongroup = "<option value='-1' selected='selected'>请选择</option>";
    var UserGroupId = $("#column_groupid_rankid_columnnum_1").children("select").val();
    var UserRankId = $("#column_groupid_rankid_columnnum_2").children("select").val();
    var columnSelect = $("#column_groupid_rankid_columnnum_3").children("select");
    var result = getUserAuthorityModelList(UserGroupId, UserRankId);
    if (result != false) {
        var result = eval("(" + result + ")");
        for (var i = 0; i < result.length; i++) {
            optiongroup += "<option value='" + result[i].AuthorityId + "'>" + result[i].AuthorityName + "</option>";
        }
    }
    columnSelect.empty();
    columnSelect.append(optiongroup);
}

//得到相应的群组和等级没有选择的权限Authority
function getUserAuthorityModelList(UserGroupId, UserRankId) {
    var result = false;
    $.ajax({
        type: "POST",
        url: "../ashx/getUserAuthorityModelList.ashx",
        data: {
            "UserGroupId": UserGroupId,
            "UserRankId": UserRankId
        },
        dataType: "text",
        async: false,
        success: function (data) {//结果：默认为0，删除成功1，删除失败-1，添加成功2，添加失败-2
            if (data != null)
                result = data;
        },
    });
    return result;
}

//添加、删除用户权限
function hanlerUserAuthority(userauthority) {
    var result = false;
    $.ajax({
        type: "POST",
        url: "../ashx/hanlerUserAuthority.ashx",
        data: {
            "UserAuthorityId": userauthority.UserAuthorityId,
            "AuthorityId": userauthority.AuthorityId,
            "UserGroupId": userauthority.UserGroupId,
            "UserRankId": userauthority.UserRankId,
            "UserAuthoriryRemark": userauthority.UserAuthoriryRemark,
        },
        dataType: "text",
        async: false,
        success: function (data) {//结果：默认为0，删除成功1，删除失败-1，添加成功2，添加失败-2
            if (data != 0 && data != -1 && data != 1 && data != -2)
                result = data - 2;
            else result = data;
        },
    });
    return result;
}

//格式化页面
function format(column) {
    if (column.siblings(".column").length > 0) {//存在同辈元素
        column.remove();
    } else {
        if (column.parent().siblings(".rank").length > 0) {//父元素存在同辈元素
            column.parent().remove();
        } else {
            column.parent().parent().remove();
        }
    }
}

//根据ID查询Authority
function getmodelbyidforjson(id) {
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/Authority.ashx",
        data: { "id": id ,"type":"QUERY"},
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null)
                result = data;
        },
    });
    return result;
}

//根据ID查询UserGroup
function GetUserGroup(id) {
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/UserGroup.ashx",
        data: { "id": id, "type": "Query" },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null)
                result = data;
        },
    });
    return result;
}

//根据ID查询UserRank
function GetUserRank(id) {
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/UserRank.ashx",
        data: { "id": id, "type": "Query" },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null)
                result = data;
        },
    });
    return result;
}

//添加权限后，格式化页面
//新增的格式
//   <div id="UserGroupId_1" class="group">
//    <div id="grouplab_1" class="grouplab">超级管理员</div>
//    <div id="UserRankId_1" class="rank">
//        <div id="ranklab_1" class="ranklab">轮机助理</div>
//        <span title="个人中心-二级目录" id="UserAuthorityId_32" >个人中心</span>
//        <span title="设置等级-二级目录" id="UserAuthorityId_33">设置等级</span>
//    </div>
//</div>
function AddFormat(newUserAuthority) {
    var AuthorityModel = getmodelbyidforjson(newUserAuthority.AuthorityId);
    var UserGroup = $("#UserGroupId_" + newUserAuthority.UserGroupId);
    var UserRank = UserGroup.children("#UserRankId_" + newUserAuthority.UserRankId);
    var UserAuthority = $("<span title='" + AuthorityModel.AuthorityName + "' class='column' id='" + newUserAuthority.UserAuthorityId + "' >" + AuthorityModel.AuthorityName + "</span>");
    $("#column_groupid_rankid_columnnum_3").children("select").children("[value='" + newUserAuthority.AuthorityId + "']").remove();//清除select内的数据
    var parentAuthorty = getmodelbyidforjson(getmodelbyidforjson(newUserAuthority.AuthorityId).AuthorityParentId);//取得权限的父对象
    if (parentAuthorty != false)
        var PASelect = $("#column_groupid_rankid_columnnum_3").children("select").children("[value='" + parentAuthorty.AuthorityId + "']");
    if (PASelect.length > 0)
        PASelect.remove();//清除select内的父对象
    //后面处理在页面添加元素

    if (UserGroup.length <= 0) {//此组不存在,添加组对象
        var UserGroupModel = GetUserGroup(newUserAuthority.UserGroupId);
        var content = $("#content");
        UserGroup = $("<div id='UserGroupId_" + newUserAuthority.UserGroupId + "' class='group'></div>");//组
        var grouplab = $("<div id='grouplab_" + newUserAuthority.UserGroupId + "' class='grouplab'>" + UserGroupModel.UserGroupName + "</div>");//组标签
        UserGroup.append(grouplab);
        content.append(UserGroup);
    }
    if (UserRank.length <= 0) {//此等级不存在，添加等级对象
        var UserRankModel = GetUserRank(newUserAuthority.UserRankId);
        UserRank = $("<div id='UserRankId_" + newUserAuthority.UserRankId + "' class='rank'></div>");//等级
        var ranklab = $("<div id='ranklab_" + newUserAuthority.UserRankId + "' class='ranklab'>" + UserRankModel.UserRankName + "</div>");//等级标签
        UserRank.append(ranklab);
        UserGroup.append(UserRank);
    }
    UserRank.append(UserAuthority);
    ColumnEvent(UserAuthority);
    alert("添加成功");
}