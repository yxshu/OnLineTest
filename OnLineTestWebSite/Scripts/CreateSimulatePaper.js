var Form;//定义显示区域
var RatioValid = true;//定义一个章节比例判断标签
$(document).ready(function () {
    Form = $("#form");//显示区域
    initPage(Form);//初始化页面
});

//=================================================================================================================================================
//初始化页面
function initPage(form) {
    var title = $("<div id='title'>+ 生成试卷 +</div>");//添加标题行
    //科目选择行
    var subject = $("<div class='DIVselect'><lable for='subject'>科&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp目：</lable><select id='subject' name='subject'><option select='selected' value='-1'>请选择</option></select><span class='star'>*</span></div>");//学科
    subject.on({
        "change": subjectchange
    });
    //试题代码选择行
    var papercode = $("<div class='DIVselect'><lable for='papercode'>适用对象：</lable><select id='papercode' name='papercode'><option select='selected' value='-1'>请选择</option></select><span class='star'>*</span></div>");//适用对象
    papercode.on({
        "change": papercodechange
    });
    //试题难度选择行
    var difficulty = $("<div class='DIVselect'><lable for='difficulty'>难度系数：</lable><select id='difficulty' name='difficulty'><option select='selected' value='-1'>请选择</option></select><span class='star'>*</span></div>");//难度系数
    difficulty.on({
        "change": difficultychange
    })
    var textbook = $("<div class='DIVselect' id='DIVtextbook'><lable for='textbook'>适用教材：</lable><select id='textbook' name='textbook'><option select='selected' value='-1'>请选择</option></select><span class='star'>*</span></div>");//教材
    textbook.on({
        "change": textbookchange
    });
    //var chapter = $("<select id='chapter'><option select='selected' value='-1'>请选择</option></select>");//章节
    var sub = $("<div id='sub'>生成试卷</div>");
    sub.on({
        "click": submit,
        "mouseenter": function () { $(this).css({ "cursor": "pointer", "border": "solid 1px #FF9924" }) },
        "mouseout": function () { $(this).css({ "border": "none" }) }
    });
    form.append(title, subject, papercode, difficulty, textbook, sub);
    initOption();//填充数据选项
}
//给学科和难度系数添加数据
function initOption() {
    Uajax({}, "../ashx/getSubject.ashx", getSubject_success, error, $("#subject"));//获取学科数据
    Uajax({}, "../ashx/getDifficulty.ashx", getDifficulty_success, error, $("#difficulty"));//获取难度系数数据
}
//获取学科数据成功
function getSubject_success(data, obj) {
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var option = "<option value='" + data[i]["SubjectId"] + "'>" + data[i]["SubjectName"] + "</option>";
            obj.append(option);
        }
    }
}
//获取难度系数成功
function getDifficulty_success(data, obj) {
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var option = "<option value='" + data[i]["DifficultyId"] + "'>" + data[i]["DifficultyDescrip"] + "</option>";
            obj.append(option);
        }
    }
}
//subject改变的事件
function subjectchange() {
    $("#subject").next().css({ "color": "GrayText" });
    $(".tips").remove();
    var subjectId = $("#subject").val();
    if (subjectId < 0) return;
    Uajax({ SubjectId: subjectId }, "../ashx/getPaperCodeBySubjectId.ashx", subjectchange_success, error, $("#papercode"))
}
//papercode获取数据成功
function subjectchange_success(data, obj) {
    obj.empty();
    obj.append("<option select='selected' value='-1'>请选择</option>");
    if (data != null && data.length > 0) {

        for (var i = 0; i < data.length; i++) {
            obj.append("<option value='" + data[i]["PaperCodeId"] + "'>" + data[i]["PaperCode"] + " " + data[i]["ChineseName"] + "</option>");
        }
    }
}

//papaercode改变事件
function papercodechange() {
    $("#papercode").next().css({ "color": "GrayText" });
    $(".tips").remove();
    var papercodeid = $("#papercode").val();
    if (papercodeid < 0) return;
    Uajax({ PaperCodeId: papercodeid }, "../ashx/GetTextBookByPaperCodeId.ashx", papercodechange_success, error, $("#textbook"));
}
//textbook获取教材数据成功
function papercodechange_success(data, obj) {
    obj.empty();
    obj.append("<option select='selected' value='-1'>请选择</option>");
    if (data != null && data.length > 0) {

        for (var i = 0; i < data.length; i++) {
            obj.append("<option  value='" + data[i]["TextBookId"] + "'>" + data[i]["TextBookName"] + "  ISBN:  " + data[i]["ISBN"] + "</option>");
        }
    }
}
//难度系数改变事件
function difficultychange() {
    $("#difficulty").next().css({ "color": "GrayText" });
    $(".tips").remove();
}
//textbook教材改变事件
function textbookchange() {
    $("#textbook").next().css({ "color": "GrayText" });
    $(".tips").remove();
    var textbookid = $("#textbook").val();
    if (textbookid < 0) return;
    Uajax({ TextBookId: textbookid }, "../ashx/GetChapterByTextBookId.ashx", textbookchange_success, error, $("#DIVtextbook"));
}
//教材改变以后，获取章节数据成功，生成章节信息
function textbookchange_success(data, obj) {
    var DIVchapter = $("#DIVchapter");
    if (DIVchapter.length > 0) DIVchapter.remove();//清除以前的数据 
    if (data != null && data != "" && data.length > 0) {
        RatioValid = true;
        DIVchapter = $("<div id='DIVchapter' class='chap'><div id='title_chapter'>请选择生成的试卷中各章节的比例：<label id='allRatio'>100</label></div></div>")
        CreateChapterStruction(data, DIVchapter);
    } else {
        DIVchapter = $("<div id='DIVchapter' class='chap'><div id='title_chapter'>没有章节信息，将按默认方式生成试卷。</div></div>");
        DIVchapter.children().css({"color":"red"});
    }
    obj.after(DIVchapter);
}
//生成章节层次结构
function CreateChapterStruction(data, div) {
    var currentAllRatio = 0, averageRatio, ParentChapterNO = 0, CurrentParentChapterNO = 0;
    for (var j = 0; j < data.length; j++) {
        if (data[j]["ChapterDeep"] == 0)
            ParentChapterNO++;
    }
    averageRatio = Math.floor(100 / ParentChapterNO);
    for (var i = 0; i < data.length; i++) {
        //ChapterId":1,"TextBookId":1,"ChapterName":"航海基础知识","ChapterParentNo":0,"ChapterDeep":0,"ChapterRemark":"航海基础知识","IsVerified":true
        var chapter = $("<div class='chap'></div>");//生成一个大框框，圈住后面的一切
        var border = $("<span></span>");//用于产生前面的虚线
        border.css({ "display": "inline-block", "width": (data[i]["ChapterDeep"] + 1) * 25 + "px", "height": "14px", "border-bottom": "dashed 1px #FF9924", "vertical-align": "top", "margin-left": "5px" });
        var chaptername = $("<span>" + data[i]["ChapterName"] + "</span>");//我们看到的章节名称
        chaptername.css({ "display": "inline-block", "margin-left": "8px" });
        var chapterratio = $("<span></span>");//用于存放比例的框框
        if (data[i]["ChapterDeep"] == 0) {
            CurrentParentChapterNO++;
            var bord = $("<span></span>");//用于产生后面的虚线
            var left = $("<span class='L'>-</span>");//减少的按钮
            var ratio;
            if (CurrentParentChapterNO != ParentChapterNO) {
                ratio = $("<input class='ratio' type='text' readonly='readonly' value='" + averageRatio + "' name='" + data[i]["ChapterId"] + "' />");//比例值
                currentAllRatio += averageRatio;
            } else {
                ratio = $("<input class='ratio' type='text' readonly='readonly' value='" + parseInt(100 - currentAllRatio) + "' name='" + data[i]["ChapterId"] + "' />");//比例值
            }
            var right = $("<span class='R'>+</span>");//增加的按钮
            var bordwidth = 500 - (25 + 15 + data[i]["ChapterName"].length * 16 + 100);//计算后面虚线的长度，也即框框的宽度
            bord.css({ "display": "inline-block", "height": "14px", "border-bottom": "dashed 1px #FF9924", "vertical-align": "top", "margin": "0px 5px", "width": bordwidth + "px" });
            left.css({
                "display": "inline-block", "width": "20px", "text-align": "center"
            }).on({
                "mouseover mouseout click": ChangeRatio
            });
            ratio.css({ "display": "inline-block", "width": "30px", "text-align": "center", "height": "25px" });
            right.css({
                "display": "inline-block", "width": "20px", "margin-left": "10px", "text-align": "center"
            }).on({
                "mouseover mouseout click": ChangeRatio
            })
            chapterratio.append(bord, left, ratio, right);//比例部分的汇总
            chapter.addClass("index");//大章节的特殊样式  加粗和黑体
        }
        chapterratio.css({ "display": "inline-block", "float": "right" });
        chapter.append(border, chaptername, chapterratio);//最终的汇总
        chapter.css({ "border-left": "dashed 1px #FF9924" });
        div.append(chapter);
    }
    return div;
}
//改变比例事件
function ChangeRatio(event) {
    if (event.type == "mouseover") {
        $(this).css({ "cursor": "pointer", "background-color": "#FF9924" });
    } else if (event.type == "mouseout") {
        $(this).css({ "background-color": "#EEE" });
    } else if (event.type == "click") {
        $(".tips").remove();
        var target = $(event.target);
        var ratio;
        RatioValid = false;
        //减少比例
        if (target.attr("class") == "L") {
            ratio = target.next();
            if (ratio.val() > 0) {
                ratio.val(parseInt(ratio.val()) - 1);
            }
        } else {
            //增加比例
            ratio = target.prev();
            if (ratio.val() < 100) {
                ratio.val(parseInt(ratio.val()) + 1)
            }
        }
        //计算总值
        var allratioVal = 0;
        $(".ratio").each(function () {
            allratioVal += parseInt($(this).val());
        })
        $("#allRatio").html(allratioVal);
        if (allratioVal != 100) {
            $("#title_chapter").css({ "color": "red" });
        } else {
            $("#title_chapter").css({ "color": "GrayText" });
            RatioValid = true;
        }
    }
}
//生成试卷按钮事件
function submit() {
    $(".tips").remove();
    var subject = $("#subject");
    var papercode = $("#papercode");
    var difficulty = $("#difficulty");
    var textbook = $("#textbook");
    var sub = $("#sub");
    if (subject.val() < 0) { subject.next().css({ "color": "red" }); sub.before("<p class='tips'>科目不能为空</p>"); return; }
    if (papercode.val() < 0) { papercode.next().css({ "color": "red" }); sub.before("<p class='tips'>适用对象不能为空</p>"); return; }
    if (difficulty.val() < 0) { difficulty.next().css({ "color": "red" }); sub.before("<p class='tips'>难度系数不能为空</p>"); return; }
    if (textbook.val() < 0) { textbook.next().css({ "color": "red" }); sub.before("<p class='tips'>适用教材不能为空</p>"); return; }
    if (!RatioValid) { $("#title_chapter").css({ "color": "red" }); sub.before("<p class='tips'>各章节比例不为100%</p>"); return; }
    Form.submit();
    //Uajax({ data: "data" }, "../ashx/CreateSimulatePaper.ashx", CreateSimulatePaper_success, CreateSimulatePaper_error, $(this))
}
//获取数据失败，共同的获取数据失败
function error(obj) {
    alert("对不起，获取数据失败。");
}
//function CreateSimulatePaper_success(data,obj) {
//    console.log(data);
//}

//function CreateSimulatePaper_error(obj) {
//    console.log(obj);
//}