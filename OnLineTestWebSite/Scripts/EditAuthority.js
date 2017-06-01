//---------------------------------文档载入完成以后运行---------------------------------------
$(document).ready(function () {

    var ordernum;
    //设置排序按钮的属性和事件
    $(".ordernum:not(#rpheaderrowordernum)").mouseenter(function () {
        ordernum = $(this).text();
        $(this).empty();
        $(this).append("<img src='../Images/up.gif' title='up' alt='up' />&nbsp;<img src='../Images/down.gif' title='down' alt='down' />");
        var up = $(this).children().first();
        var down = $(this).children().last();
        var currentrow = $(this).parent(); //按钮所在的行
        up.click(function () { //向上的按钮
            upclick(currentrow);
        });
        down.click(function () { //向下的按钮
            downclick(currentrow);
        });
    }).mouseleave(function () {
        $(this).empty();
        $(this).append(ordernum);
    });

    //设置编辑按钮的属性和事件$("selector1:not(selector2)")
    $(".edit:not(#rpheaderrowedit)").click(function () {
        editclick($(this).children("label:first-child").attr("title")); //参数是ID
    }).mouseenter(function () {
        $(this).css({ "position": "relative", "top": 2, "left": 2, "cursor": "pointer" });
    }).mouseleave(function () {
        $(this).css({ "position": "inherit", "top": 0, "left": 0, "cursor": "none" });
    });


    //设置删除按钮的属性和事件
    $(".delete:not(#rpheaderrowdelete)").click(function () {
        deleteclick($(this).children("label:first-child").attr("title")); //参数是ID
    }).mouseenter(function () {
        $(this).css({ "position": "relative", "top": 2, "left": 2, "cursor": "pointer" });
    }).mouseleave(function () {
        $(this).css({ "position": "inherit", "top": 0, "left": 0, "cursor": "none" });
    });




    //设置添加按钮的属性和事件
    $("#rpfooter").click(function () {
        addclick($(this).attr("title")); //title属性值为add
    }).mouseenter(function () {
        $(this).css({ "background": "green", "cursor": "pointer" });
    }).mouseleave(function () {
        $(this).css({ "background": "#FF9924" });
    });


    //设置每一组权限的效果
    $(".rpitem").mouseenter(function () {
        $(this).css({ "border": "solid 1px #ff9924" });
    }).mouseleave(function () {
        $(this).css({ "border": "solid 1px #DDDDDD" });
    });


    //设置每一行的效果
    $(".childrow").mouseenter(function () {
        $(this).css({ "background-color": "#EEEEEE" });
    }).mouseleave(function () {
        $(this).css({ "background-color": "inherit" });
    });


    //初始化时，隐藏遮照层
    $("#FloatDiv").hide();


    //滚动务移动时重新调整浮动层的位置
    $(window).scroll(function () {
        ShowFloatDiv($("#FloatDiv"));
    });
});

//----------------------------------下面自定义函数区---------------------------------------
var UpdataedAuthority = null; //在编辑的时候用于装新编辑后的实例

//排序中向上的按钮点击事件
function upclick(currentrow) {//参数：父元素，即所在的行,取他的id=AuthorityId和class=childrow/parentrow属性
    var id = currentrow.attr("id");
    var authority = getmodelbyidforjson(id);
    if (authority != false && ModifyAuthority(authority, "orderup") != false) {
        refresh(authority, "orderup");
    } else {
        alert("调整顺序失败。注：如果是第一位，则不能调整");
    }
}

//排序中向下的按钮点击事件
function downclick(currentrow) { //参数：父元素，即所在的行,取他的id=AuthorityId和class=childrow/parentrow属性
    var id = currentrow.attr("id");
    var authority = getmodelbyidforjson(id);
    if (authority != false && ModifyAuthority(authority, "orderdown") != false) {
        refresh(authority, "orderdown");
    } else {
        alert("调整顺序失败。注：如果是末位，则不能调整");
    }
}

//处理编辑按钮的点击动作
function editclick(id) {
    var authority = getmodelbyidforjson(id);
    //得到正确的实例
    if (authority != false) {
        CreateDivAndAppendtoPosition("编辑权限", authority, $("#FloatDiv"), "update"); //生成DIV层,并将其after到页面预留的“id=FloatDiv”区域后面第一个元素位置
        ShowFloatDiv($("#FloatDiv")); //新建一个浮动层，并执行保存和取消的动作，如果执行成功refresh主页面
    } else {//没有得到正确的数据
        alert("获取数据失败，请重试");
    }
}

//处理删除按钮的点击动作
function deleteclick(id) {
    var authority = getmodelbyidforjson(id);
    if (authority != false) {
        if (confirm("序号:" + authority.AuthorityId + "\n名称:" + authority.AuthorityName + "\n\n确定删除？")) {
            if (ModifyAuthority(authority, "delete") != false) { //删除成功，修改前台页面,注：如果本条记录存在子记录，则不能删除
                refresh(authority, "delete");
            } else {
                alert("删除失败。注：如果存在子目录，则不能删除。");
            }
        }
    } else {
        alert("获取数据失败。");
    }
}

//添加按钮的点击事件
function addclick(type) {//参数：为添加行的title属性为add，为了后面的showdiv和refresh作标识
    var maxid = -1;
    maxid = getmaxid("Authority"); //取得表中的最大ID
    if (maxid > 0) {
        var authority = {
            AuthorityId: maxid * 1 + 1,
            AuthorityName: "请添加名称",
            AuthorityDeep: -1,
            AuthorityParentId: -1,
            AuthorityScore: 0,
            AuthorityHandlerPage: "请添加处理页面",
            AuthorityOrderNum: -1,
            AuthorityRemark: "清添加备注"
        };
        CreateDivAndAppendtoPosition("新增权限", authority, $("#FloatDiv"), type); //生成DIV层,并将其after到页面预留的“id=FloatDiv”区域后面第一个元素位置
        ShowFloatDiv($("#FloatDiv"));
    } else {
        alert("获取数据错误");
    }
}

//根据Authority生成一个DIV层
function CreateDivAndAppendtoPosition(title, authority, position, type) {//参数：标题、填充的实例、安放的位置和供refresh用的类型
    var div = $("<div id='CreateDiv'></div>");
    var firstArea = $("<div id='FirstArea'></div>");
    var secondArea = $("<div id='SecondArea'></div>");
    var titles = $("<div id='title'>" + title + "</div>");
    var idAnddeep, parentIdAndordernum;
    idAnddeep = $("<div id='idAnddeep'></div>").append("<span>编&nbsp;&nbsp;&nbsp;号：" + authority.AuthorityId + "</span><span>层&nbsp;&nbsp;&nbsp;次：<select id='deep'>" + GetOptionfordeep(2, authority.AuthorityDeep) + "</option></select></span>");
    parentIdAndordernum = $("<div id='parentIdAndordernum'></div>").append("<span>父编号：<select id='parentid'>" + GetOptionforparent(authority.AuthorityDeep, authority.AuthorityParentId) + "</option></select></span><span>排&nbsp;&nbsp;&nbsp;序:<select id='ordernum'>" + GetOptionforordernum(authority.AuthorityParentId, authority.AuthorityOrderNum) + "</select></span>");
    var score = $("<div id='score'><label>分&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;值：</lable><select>" + GetOptionforscore(10, authority.AuthorityScore) + "</select></div>");
    var name = $("<div id='name'><label>设置名称：</lable><input type='text' value='" + authority.AuthorityName + "' /></div>");
    if (authority.AuthorityHandlerPage == "请添加处理页面") {
        var handlerpage = $("<div id='handlerpage'>处理页面：<input type='text' value='" + authority.AuthorityHandlerPage + "'/></div>");
    } else {
        var handlerpage = $("<div id='handlerpage'>处理页面：<input type='text' value='" + authority.AuthorityHandlerPage + "' readonly='readonly'/></div>");
    };
    var remarklab = $("<div id='remarklab'>说&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;明：</div>");
    var remark = $("<div id='remark'><textarea>" + authority.AuthorityRemark + "</textarea></div>");
    var savebtn = $("<button id='savebtn' value='保存'>保存</button>");
    var cancelbtu = $("<button id='cancelbtn' value='取消'>取消</button>");
    firstArea.append(titles, idAnddeep, parentIdAndordernum, score, name, handlerpage, remarklab, remark);
    secondArea.append(savebtn, cancelbtu);
    position.after(div.append(firstArea, secondArea));
    //给保存按钮注册点击事件
    savebtn.click(function () {
        if (SaveAuthority(authority, type) && UpdataedAuthority != null) { //修改成功以后，刷新前台数据
            if (!refresh(UpdataedAuthority, type))
                alert("刷新页面失败。注：保存数据成功");
        } else {
            alert("保存失败。");
        }
    });
    //给取消按钮注册点击事件
    cancelbtu.click(function () {
        EmptyFloatDiv($("#FloatDiv"));
    });

    //给标题行添加拖动效果

    ///* 绑定鼠标左键按住事件 */
    //titles.bind("mousedown", function (event) {
    //    /* 获取需要拖动节点的坐标 */
    //    var offset_x = $(this)[0].offsetLeft;//x坐标
    //    var offset_y = $(this)[0].offsetTop;//y坐标
    //    /* 获取当前鼠标的坐标 */
    //    var mouse_x = event.pageX;
    //    var mouse_y = event.pageY;

    //    /* 绑定拖动事件 */
    //    /* 由于拖动时，可能鼠标会移出元素，所以应该使用全局（document）元素 */
    //    this.bind("mousemove", function (ev) {
    //        /* 计算鼠标移动了的位置 */
    //        var _x = ev.pageX - mouse_x;
    //        var _y = ev.pageY - mouse_y;

    //        /* 设置移动后的元素坐标 */
    //        var now_x = (offset_x + _x) + "px";
    //        var now_y = (offset_y + _y) + "px";
    //        /* 改变目标元素的位置 */
    //        this.css({
    //            top: now_y,
    //            left: now_x
    //        });
    //    });
    //});
    ///* 当鼠标左键松开，结束事件绑定 */
    //titles.bind("mouseup", function () {
    //    $(this).unbind("mousemove");
    //});



    var deep = $("#deep");
    var parentid = $("#parentid");
    var ordernum = $("#ordernum");
    //给层次下拉框注册事件
    deep.change(function () {
        var deepval = deep.val();
        parentid.empty();
        ordernum.empty();
        parentid.append(GetOptionforparent(deepval, -1));
        ordernum.append(GetOptionforordernum(parentid.val(), -1));
    });
    //给父编号下拉框注册事件
    parentid.change(function () {
        var parentidval = parentid.val();
        ordernum.empty();
        ordernum.append(GetOptionforordernum(parentidval, -1));
    });
    //给顺序下拉框注册事件
    ordernum.change(function () {
    });
}

//生成的层提供深度的下拉菜单，参数：当前选择项，如果没有则选择-1
function GetOptionfordeep(deep, selecteddeep) {
    var optiongroup = "";
    for (var i = -1; i < deep; i++) {
        if (selecteddeep == i) {
            if (i == -1) {
                optiongroup += "<option value='" + i + "'  selected='selected'>请选择</option>";
            }
            else {
                optiongroup += "<option value='" + i + "' selected='selected'>第" + (i * 1.0 + 1) + "层</option>";
            }
        } else {
            if (i == -1) {
                optiongroup += "<option value='" + i + "'>请选择</option>";
            } else {
                optiongroup += "<option value='" + i + "'>第" + (i * 1.0 + 1) + "层</option>";
            }
        }
    }
    return optiongroup;
}

//生成的层提供父编号的下拉菜单
function GetOptionforparent(deep, selectedparentId) {
    var optiongroup = "";
    switch (parseInt(deep)) {
        case -1: {
            optiongroup += "<option value='-1' selected='selected'>请选择</option>";
            return optiongroup;
            break;
        }
        case 0: {
            optiongroup += "<option value='-1'>请选择</option>";
            optiongroup += "<option value='0' selected='selected'>主目录</option>";
            return optiongroup;
            break;
        }
        case 1: {
            var authorityList = getAuthorityListbyDeep(deep - 1);
            if (authorityList != null && authorityList != false) {
                optiongroup += "<option value='-1'>请选择</option>";
                for (var i = 0; i < authorityList.length; i++) {
                    if (selectedparentId == authorityList[i].AuthorityId) {
                        optiongroup += "<option value='" + authorityList[i].AuthorityId + "' selected='selected'>" + authorityList[i].AuthorityName + "</option>";
                    } else {
                        optiongroup += "<option value='" + authorityList[i].AuthorityId + "'>" + authorityList[i].AuthorityName + "</option>";
                    }
                }
            }
            return optiongroup;
            break;
        }
    }
}

//生成的层提供排序的下拉菜单
function GetOptionforordernum(parentid, selectedordernum) {
    var optiongroup;
    switch (parentid) {
        case "-1": {
            optiongroup += "<option value='-1' selected='selected'>请选择</option>";
            return optiongroup;
            break;
        }
        default: {
            optiongroup += "<option value='-1'>请选择</option>";
            var maxordernum = getCurrentAuthorityMaxOrdernum(parentid);
            for (var i = 1; i < maxordernum * 1.0 + 1 ; i++) {
                if (selectedordernum == i) {
                    optiongroup += "<option value='" + i + "' selected='selected'> " + i + " </option>";
                } else {
                    optiongroup += "<option value='" + i + "'> " + i + " </option>";
                }
            }
            optiongroup += "<option value='" + (maxordernum * 1.0 + 1) + "'> 末  位 </option>";
            return optiongroup;
            break;
        }
    }
}

//生成的层提供分值的下拉菜单//默认为0
function GetOptionforscore(num, selected) {//参数：提供选择的范围如num=10(范围-10_10)，当前选择项
    var optiongroup = null;
    for (var i = 0 - num; i <= num; i++) {
        if (selected >= 0 - num && selected <= num && selected == i) {
            optiongroup += "<option value='" + i + "' selected='selected'>" + i + "</option>";
        } else {
            optiongroup += "<option value='" + i + "'>" + i + "</option>";
        }
    }
    return optiongroup;
}

//新建层中取消按钮执行的动作
function EmptyFloatDiv(floatDiv) {//清空生成的层
    floatDiv.hide();//遮照层隐藏
    $("#CreateDiv").remove();//新建的层消除
}

//新建层中，保存按钮执行的修改Authority的动作
function SaveAuthority(authority, model) {
    var result = false;
    var name = $("#name input").val();
    var deep = $("#deep option:selected").val();
    var parentid = $("#parentid option:selected").val();
    var score = $("#score select option:selected").val();
    var handlerpage = $("#handlerpage input").val();
    var ordernum = $("#ordernum option:selected").val();
    var remark = $("#remark textarea").val();
    authority.AuthorityName = $.trim(name);
    authority.AuthorityDeep = deep;
    authority.AuthorityParentId = parentid;
    authority.AuthorityScore = score;
    authority.AuthorityHandlerPage = $.trim(handlerpage);
    authority.AuthorityOrderNum = ordernum;
    authority.AuthorityRemark = $.trim(remark);
    EmptyFloatDiv($("#FloatDiv"));
    if (authority.AuthorityId > 0 &&
        authority.AuthorityName.trim() != "请添加名称" &&
        authority.AuthorityDeep >= 0 &&
        authority.AuthorityParentId >= 0 &&
        authority.AuthorityHandlerPage.trim() != "请添加处理页面" &&
        authority.AuthorityOrderNum > 0
        ) {
        UpdataedAuthority = ModifyAuthority(authority, model);
        if (UpdataedAuthority != false) {
            result = true;
        }
    } else {
        alert("请提交正确的数据")
    }
    return result;
}

//显示生成的DIV层，参数是遮照层，数据层是其后第一个元素
function ShowFloatDiv(floatDiv) {
    var clientheight = $(window).height();//窗口的高度
    var clienthwidth = $(window).width();//窗口的宽度
    var scrolltop = $(window).scrollTop();//滚动条距上边的高度
    var creatediv = $("#CreateDiv");
    if (creatediv.length > 0) {//判断元素是否已经生成
        floatDiv.show();//遮罩层
        //设置数据层以内样式
        creatediv.css({ "border": "solid 1px #FF9924" });
        $("#FirstArea").css({});
        $("#FirstArea div,#SecondArea").css({ "height": "30px", "margin": "10px", "text-align": "center", "line-height": "30px" }); //每一行的共性部分
        $("#title").css({ "background-color": "#FF9924", "color": "White", "font-size": "larger", "cursor": "pointer" });
        $("#idAnddeep,#parentIdAndordernum").css({});
        $("#idAnddeep span,#parentIdAndordernum span").css({ "display": "inline-block", "width": "50%" });
        $("#score,#name,#handlerpage").css({ "padding-left": "30px" });
        $("#score label,#name label").css({});
        $("#score select,#name input,#handlerpage input").css({ "width": "240px", "height": "30px" });
        $("#remarklab").css({ "text-align": "left", "padding-left": "40px" });
        $("#remark").css({ "min-height": "200px" });
        $("#remark textarea").css({ "width": "90%", "min-height": "200px" });
        $("#SecondArea").css({ "margin": "20px" });
        $("#SecondArea button").css({ "width": "50%", "height": "40px", "background-color": "#FF9924", "cursor": "pointer" });
        //设置遮罩层部分
        floatDiv.css({
            //设置遮罩层样式
            "position": "absolute",
            "background-color": "White",
            "opacity": 0.7,
            "filter": "alpha(opacity=70)",
            "-moz-opacity": 0.7,
            "-khtml-opacity": 0.7,
            "width": "100%",
            "height": "100%",
            "top": scrolltop,
            "left": 0,
            "z-index": 100
        });
        //设置数据层部分
        creatediv.css({
            //设置数据层样式
            "position": "absolute",
            "background-color": "White",
            "border": "solid 1px #FF9924",
            "width": 450,
            "overflow": "auto",
            "z-index": "9999",
            "top": Math.max(scrolltop + (clientheight - creatediv.height()) / 2, scrolltop),
            "left": (clienthwidth - 500) / 2
        });
    } //判断元素生成执行的代码结束
}

//刷新前台页面
function refresh(authority, model) {
    var result = false;
    var divrow = $("#" + authority.AuthorityId + ""); //找到编辑、删除和移动的地方
    var currentrow;
    switch (model) {
        case "add": //新增
            {
                var prevauthority = getprevmodelbyidforjson(authority.AuthorityId, "prevmodel");//如果是第一条或最后一条则返回自身，否则返回前一条
                divrow = $("#" + prevauthority.AuthorityId + "");
                var newrow = modifyrow(authority, divrow.clone(true));
                if (prevauthority != false) {
                    if (!shiftrow(newrow, prevauthority, authority)) {
                        alert("移动数据失败");
                    } else { result = true; }
                } else {
                    alert("获取数据失败。");
                }
                return result;
                break;
            };
        case "update": //编辑
            {
                divrow.hide();
                var newrow = modifyrow(authority, divrow);
                var prevauthority = getprevmodelbyidforjson(authority.AuthorityId, "prevmodel");//有可能是自身（是父元素且只有一条记录）、前面一条记录、后面一条记录（说明是第一条记录），父元素（说明是子元素，且只有一条记录）
                if (prevauthority != false) {
                    if (!shiftrow(newrow, prevauthority, authority)) {
                        alert("移动数据失败");
                    } else { result = true; }
                } else {
                    alert("获取数据失败。");
                }
                return result;
                break;
            };
        case "delete": //删除
            {
                if (!removerow(divrow)) {
                    alert("删除失败。");
                } else { result = true; }
                return result;
                break;
            };

        case "orderup": //排序上移
            {
                var pervrow;
                divrow.children(".ordernum").html(parseInt(authority.AuthorityOrderNum) - 1);
                if (authority.AuthorityDeep == 0) {//第一层，调整的是整个块
                    divrow.children(".id").html((parseInt(authority.AuthorityOrderNum) - 1) + "组");
                    divrow.parent().prev().children().first().children(".ordernum").html(authority.AuthorityOrderNum);
                    divrow.parent().prev().children().first().children(".id").html(authority.AuthorityOrderNum + "组");
                    pervrow = divrow.parent().prev();
                    currentrow = divrow.parent();
                } else {
                    divrow.children(".id").html(parseInt(authority.AuthorityOrderNum) - 1);
                    pervrow = divrow.prev();
                    pervrow.children(".ordernum").html(authority.AuthorityOrderNum);
                    pervrow.children(".id").html(authority.AuthorityOrderNum);
                    currentrow = divrow;
                }
                currentrow.after(pervrow);
                return result = true;
                break;
            }
        case "orderdown": //排序下移
            {
                var next;
                divrow.children(".ordernum").html(parseInt(authority.AuthorityOrderNum) + 1);
                if (authority.AuthorityDeep == 0) {
                    divrow.children(".id").html(parseInt(authority.AuthorityOrderNum) + 1 + "组");
                    divrow.parent().next().children().first().children(".ordernum").html(parseInt(authority.AuthorityOrderNum));
                    divrow.parent().next().children().first().children(".id").html(parseInt(authority.AuthorityOrderNum) + "组");
                    next = divrow.parent().next();
                    currentrow = divrow.parent();
                }
                else {
                    divrow.children(".id").html(parseInt(authority.AuthorityOrderNum) + 1);
                    next = divrow.next();
                    next.children(".ordernum").html(parseInt(authority.AuthorityOrderNum));
                    next.children(".id").html(parseInt(authority.AuthorityOrderNum));
                    currentrow = divrow;
                }
                currentrow.before(next);
                result = true;
                return result;
                break;
            }
    }
}

//修改一行记录
function modifyrow(authority, divrow) {
    //<div class="rpitem">
    //<div id="5" class="parentrow">
    //    <span class="id"> 2 组 </span>
    //    <span class="name"> 测试与练习 </span>
    //    <span class="handlerpage"> TestAndPractice </span>
    //    <span class="score"> 0 </span>
    //    <span class="remark"> 测试与练习-一级目录 </span>
    //    <span class="ordernum"> 2 </span>
    //    <span class="edit"><label title="5">编辑</label></span>
    //    <span class="delete" style="position: inherit; top: 0px; left: 0px; cursor: none;"><label title="5">删除</label></span>
    //</div>
    //</div>
    //<div id="13" class="childrow" style="background-color: inherit;">
    //    <span class="id"> 1 </span>
    //    <span class="name"> 编辑试题 </span>
    //    <span class="handlerpage"> EditQuestion.aspx </span>
    //    <span class="score"> 0 </span>
    //    <span class="remark"> 编辑试题-二级目录，只能编辑本人新增的且没有审核的试题，包括新增 </span>
    //    <span class="ordernum"> 1 </span>
    //    <span class="edit" style="position: inherit; top: 0px; left: 0px; cursor: none;"><label title="13">编辑</label></span>
    //    <span class="delete"><label title="13">删除</label></span>
    //</div>
    var result = divrow;
    if (authority.AuthorityDeep == 0) {
        divrow.appendTo($("<div class='rpitem'></div>"));
        result = divrow.parent();
    }
    divrow.attr({ "id": authority.AuthorityId, "className": authority.AuthorityDeep == 0 ? "parentrow" : "childrow" });
    divrow.children(".id").html(authority.AuthorityDeep == 0 ? authority.AuthorityOrderNum + "组" : authority.AuthorityOrderNum);
    divrow.children(".name").html(authority.AuthorityName);
    divrow.children(".handlerpage").html(authority.AuthorityHandlerPage);
    divrow.children(".score").html(authority.AuthorityScore);
    divrow.children(".remark").html(authority.AuthorityRemark);
    divrow.children(".ordernum").html(authority.AuthorityOrderNum);
    divrow.children(".edit").children("label").attr("title", authority.AuthorityId);
    divrow.children(".delete").children("label").attr("title", authority.AuthorityId);
    return result;
}

//移动一行记录
function shiftrow(row, prevauthority, rowauthority) {
    ///参数要移动的行，要移动位置行对应的数据，移动行应的数据
    ///移动位置行对应数据的可能性 有可能是自身（是父元素且只有一条记录）、前面一条记录、后面一条记录（说明是第一条记录），父元素（说明是子元素，且只有一条记录）
    var result = false;
    var position = $("#" + prevauthority.AuthorityId + "");
    if (prevauthority == rowauthority)//父元素，且只有一条记录
    {
        result = true;
    } else if (prevauthority.AuthorityParentId == rowauthority.AuthorityParentId) {//同级元素

        if (prevauthority.AuthorityOrderNum < rowauthority.AuthorityOrderNum) {//前面的一条记录
            if (prevauthority.AuthorityDeep == 0) {//你元素
                position.parent().after(row);
            } else {
                position.after(row);
            }
            result = true;
        } else {//后面的一条记录
            if (prevauthority.AuthorityDeep == 0) {//你元素
                position.parent().before(row);
            } else {
                position.before(row);
            }
            result = true;
        }
    } else {//父元素，说明是子元素，且只有一条记录
        position.after($("<div class='Child'></div>").append(row));
        result = true;
    }
    row.show("slow");
    row.children().show("slow");
    return result;
}

//删除一行记录
function removerow(removerow) {
    if (removerow.children(".childrow").length != 0) {  //这是一组的父节点
        alert("存在子元素，不允许删除。");
        return false;
    };
    if (removerow.attr("class") == "parentrow") {
        removerow.parent().nextAll().each(function () {
            $(this).children(".parentrow").children(".id").html(parseInt($.trim($(this).children(".parentrow").children(".id").html())) - 1 + "组");
            $(this).children(".parentrow").children(".ordernum").html(parseInt($.trim($(this).children(".parentrow").children(".ordernum").html())) - 1);
        });
        removerow = removerow.parent();
    } else {
        removerow.nextAll().each(function () {
            $(this).children(".id").html(parseInt($.trim($(this).children(".id").html())) - 1);
            $(this).children(".ordernum").html(parseInt($.trim($(this).children(".ordernum").html())) - 1);
        });
    }
    removerow.remove();
    return true;
}

//利用ajax查找一条记录的前一个实例,type值应为“prevmodel”
function getprevmodelbyidforjson(id, type) {
    //如果成功的话，返回值有可能是自身（说明是此父元素且只有这一条记录）、此元素的前面一条记录、此元素的后面一条记录（说明是第一条记录），此元素的父元素（说明此元素是子元素，且只有这一条记录）
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/Authority.ashx",
        data: { "AuthorityId": id, "type": "PREVMODEL", "model": type },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null)
                result = data;
        },
    });
    return result;
}

//利用ajax根据id从后台取得authority数据实例,不成功则返回false
function getmodelbyidforjson(id) {
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/Authority.ashx",
        data: { "AuthorityId": id, "type": "QUERY" },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null)
                result = data;
        },
    });
    return result;
}

//修改Authority的值,model为add,delete,update,orderup,orderdown
function ModifyAuthority(authority, model) {
    //返回的值是经过修正后的authority如果不成功则返回false
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/Authority.ashx",
        data: {
            "type": model,
            "AuthorityId": authority.AuthorityId,
            "AuthorityName": authority.AuthorityName,
            "AuthorityDeep": authority.AuthorityDeep,
            "AuthorityParentId": authority.AuthorityParentId,
            "AuthorityScore": authority.AuthorityScore,
            "AuthorityHandlerPage": authority.AuthorityHandlerPage,
            "AuthorityOrderNum": authority.AuthorityOrderNum,
            "AuthorityRemark": authority.AuthorityRemark
        },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null) {
                result = data;
            }
        },
    });
    return result;
}

//取得tablename表中的最大ID
function getmaxid(tablename) {
    var result = false;
    $.ajax({
        type: "GET",
        url: "../ashx/Authority.ashx",
        data: { "tablename": tablename, "type": "MAXID" },
        dataType: "text",
        async: false,
        success: function (data) {
            result = data;
        },
    });
    return result;
}

//根据深度得到同一深度的所有权限
function getAuthorityListbyDeep(deep) {
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/Authority.ashx",
        data: { "AuthorityDeep": deep, "type": "GETLISTBYDEEP" },
        dataType: "json",
        async: false,
        success: function (data) {
            result = data;
        },
    });
    return result;
}

//提供当前某一组权限的最大序号，如果没有则返回-1
function getCurrentAuthorityMaxOrdernum(parentid) {
    var result = false;
    $.ajax({
        type: "post",
        url: "../ashx/Authority.ashx",
        data: { "AuthorityParentId": parentid, "type": "GETMAXORDERNUMBYCURRENTAUTHORITYPARENTID" },
        dataType: "text",
        async: false,
        success: function (data) {
            result = data;
        },
    });
    return result;
}