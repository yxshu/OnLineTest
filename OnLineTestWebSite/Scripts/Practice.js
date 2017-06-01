var Main;//整个内容区
var loading;

//页面载入时，执行的动作
$(document).ready(function () {
    Main = $("#content");
    loading = $("#loading");
    init();

});

//---------------------------自定义函数区---------------------------------
///初始化页面，
///1、设置载入中图像的样式;
///2、请求科目数据
function init() {
    loading.css({ "width": Main.width() + "px", "line-height": Main.height() + "px", "text-align": "center" });
    Uajax({}, "../ashx/getSubject.ashx", getSubject_success, Error, Main);
}

///获取科目数据成功
///1、去除载入中动画；
///2、加载科目和类型选择项
///3、初始化科目选择的option
///4、给科目的select添加change函数,请求类型的数据
function getSubject_success(data, obj) {
    if (data.length > 0) {
        loading.remove();
        var title = $("<div>请选择练习科目：</div>");
        var content = $("<div></div>");
        var lsubject = $("<label>科目：</label>");
        var ssubecjt = $("<select id='subject' class='subject'></select>");
        var option_subject = $("<option selected='selected' value='-1'>请选择科目</option>");
        ssubecjt.append(option_subject);
        for (var i = 0; i < data.length; i++) {
            var new_option = $("<option value='" + data[i]['SubjectId'] + "'>" + data[i]['SubjectName'] + "</option>");
            ssubecjt.append(new_option);
        }
        var ltype = $("<label>类别：</label>");
        var stype = $("<select id='type' class='type'></select>");
        var option_type = $("<option selected='selected' value='-1'>请选择类别</option>");
        stype.append(option_type);
        content.append(lsubject, ssubecjt, ltype, stype);
        Main.append(title, content);
        title.css({ "line-height": "40px", "position": "relative", "left": "-150px", "margin-top": Main.height() / 2 - 180 + "px", "font-size": "larger" });
        content.css({ "line-height": "40px" });
        ltype.css({ "display": "inline-block", "margin-left": "20px" });
        ssubecjt.on("change", Subject_Change);
    }
}

///根据用户选择的科目获取类别数据
function Subject_Change() {
    var subjectid = $(this).val();
    if (subjectid > 0) {
        Uajax({ "SubjectId": subjectid }, "../ashx/getPaperCodeBySubjectId.ashx", getPaperCodeBySubjectId_success, Error, $("#type"));
    }
}

///获取类别数据成功以后，添加到类别select中
function getPaperCodeBySubjectId_success(data, obj) {
    obj.empty();
    if (data.length <= 0) return;
    for (var i = 0; i < data.length; i++) {
        var option = $("<option value='" + data[i]["PaperCodeId"] + "'>" + data[i]["ChineseName"] + "</option>");
        obj.append(option);
    }
    obj.on("change", type_change_getQuestionCount);
}

//根据用户选择的科目 获取当前科目下的试题数量
function type_change_getQuestionCount() {
    $("#sub").remove();//$("#sub"是确定按钮)
    Uajax({ "PapercodeId": $(this).val() }, "../ashx/getQuestionCountByPapercodeId.ashx", getQuestionCountByPapercodeId_success, Error, $("#type"));
}

///根据提交的papercodeid查询此类别下试题的数量  成功
function getQuestionCountByPapercodeId_success(data, obj) {
    $(".zu").remove();
    var bzu = $("<div class='zu'>该类别下的试题共：" + data + " 题，不足 3 题无法练习。</div>");
    bzu.css({ "color": "red" });
    var czu = $("<div class='zu'>该类别下共有试题：" + data + " 题。</div>");
    czu.css({ "color": "GrayText" });
    if (data > 3) {
        obj.after(czu);
        setTimeout(function () { czu.hide("slow") }, 5000);
        if ($("#question").length > 0) {
            if ($("#rightnarrow").length > 0) {
                $("#rightnarrow").off("click").on({ "click": rightnarrow_click }).css({ "background-color": "#FF9924", "cursor": "pointer" });
            }
            return;
        }
        Type_Change_addSubBTN(obj);//是否添加确定按钮，如果已经是试题页面则不需要添加
    } else {
        obj.after(bzu);
        setTimeout(function () { bzu.hide("slow") }, 5000);
        if ($("#rightnarrow").length > 0) {
            $("#rightnarrow").off("click").css({ "background-color": "GrayText", "cursor": "default" })
        };
    }
}

///确定按钮的出现和隐藏
function Type_Change_addSubBTN(obj) {
    if ($(obj).val() > 0) {
        if ($("#sub").length <= 0) {
            var sub = $("<div id='sub'>确定</div>");
            sub.on({
                "mouseenter": function () { $(this).css({ "border": "solid 1px red" }) },
                "mouseout": function () { $(this).css({ "border": "none" }) },
                "click": Submit
            });
            Main.append(sub.fadeIn("slow"));
            sub.css({ "width": "100px", "line-height": "40px", "position": "relative", "left": "200px", "font-size": "larger", "margin-top": "50px", "background-color": "#FF9924", "cursor": "pointer" });
        }
    } else {
        if ($("#sub").length > 0)
            $("#sub").remove();
    }
}

///确定按钮的点击事件
function Submit() {
    var subjectid = $("#subject").val();
    var papercodeid = $("#type").val();
    Uajax({ "papercodeid": papercodeid }, "../ashx/getQuestionByPapercodeidANDRand.ashx", getQuestion_success, getQuestion_Error, Main);
}

///获取试题数据成功以后
function getQuestion_success(data, obj) {
    if (data[0] == null) {
        alert("没有通过审核的可用试题。");
        return;
    }
    var subject = $("#subject");
    var type = $("#type");
    var top = $("<div id='top'></div>");//顶行用于随时选择科目
    var status = $("<div id='status'><lable>科目：" + type.find("option:selected").text() + "——" + subject.find("option:selected").text() + "</label> </div>");//最底下的状态栏，表示此试题所属的科目
    top.css({ "height": "40px", "text-align": "left", "line-height": "40px", "padding-left": "20px" });
    status.css({ "height": "50px", "text-align": "right", "padding-right": "20px" });
    top.append($("<label>科目：</label>"), subject, $("<label>类别：</label>"), type);
    Main.empty();
    var question = $("<div id='question'><div>");
    question.css({ "position": "relative", "min-height": Main.height() - top.height() - status.height() - 50 });
    var leftnarrow = $("<div id='leftnarrow'><</div>");
    var rightnarrow = $("<div id='rightnarrow'>></div>");
    leftnarrow.css({ "display": "none", "cursor": "pointer", "color": "white", "font-weight": "bolder", "height": "100px", "line-height": "100px", "width": "50px", "z-index": "999", "position": "absolute", "left": 0, "top": (Main.height() - top.height() - status.height()) / 2 - 50, "background-color": "#ff9924" });
    rightnarrow.css({ "display": "none", "cursor": "pointer", "color": "white", "font-weight": "bolder", "height": leftnarrow.height(), "line-height": "100px", "width": "50px", "z-index": "999", "position": "absolute", "left": Main.width() - leftnarrow.width(), "top": (Main.height() - top.height() - status.height()) / 2 - 50, "background-color": "#FF9924" });
    question.append(CreateQuestionUL(data), leftnarrow, rightnarrow);
    Main.append(top, question, status);
    //绑定左边按钮事件
    leftnarrow.on({
        "click": leftnarrow_click
    });
    //绑定右边按钮事件
    rightnarrow.on({
        "click": rightnarrow_click
    });
    //绑定问题区域事件  主要是左右箭头的显隐问题
    question.on({
        "mouseenter": function () { leftnarrow.fadeIn(); rightnarrow.fadeIn() },
        "mouseleave": function () { leftnarrow.fadeOut(), rightnarrow.fadeOut() }
    });

    //给radio绑定事件，因为用的选择器，所以要在加载到页面以后才可以找到
    $("input:radio[name='answer']").each(
        function () {
            $(this).on("click", { "obj": $(this) }, radio_click);
        });
}

//radio点击事件执行的动作
function radio_click(obj) {
    var YLoganswer = $("#YLoganswer");//记录中的答案 如果是第一次则为-1
    var value = obj.target["value"];//选择的答案(完整的包含字符)
    var curranswer = str_answerTONUM(value);//选择的答案转化为数字（转化为数字以后的）
    //下面是第一次填答案，将其保存在数据库
    if (YLoganswer.text() < 0) {
        YLoganswer.text(curranswer);
        var currentlogpracticeid = $("#logparcticeid").text();
        Uajax({ "LogPracticeId": currentlogpracticeid, "curranswer": curranswer }, "../ashx/insertpracticeanswer.ashx", insertpracticeanswer_success, insertpracticeanswer_Error, Main);
    }
    var Yanswer = $("#Yanswer");//填写测试者选择的答案的区域（表示出来的）
    var correctanswer = $("#correctanswer");//正确答案的区域，因为是练习，所以没有每次从服务器上取数据
    Yanswer.text(value);//设定测试者选择答案的区域
    if (curranswer == correctanswer.text()) {//答案选择正确，用绿色字体表示
        $("#Qanswer").css("color", "green");
    } else {//答案选择错误，用红色字体表示
        $("#Qanswer").css("color", "red");
    }
}

///写入用户的答案成功
function insertpracticeanswer_success(data, obj) {
    if (data == true)
        console.log("写入答案成功：" + data);
    else if (data == false)
        console.log("写入答案错误：" + data);
}

///写入用户的答案错误
function insertpracticeanswer_Error(data) {
    console.log("写入答案错误：" + data);
}

//将答案转化为数字形式表示
function str_answerTONUM(str_answer) {
    var firstA = str_answer.slice(0, 1);
    switch (firstA.toLowerCase()) {
        case "a": return 1;
        case "b": return 2;
        case "c": return 3;
        case "d": return 4;
        default: return -1;
    }
}

///产生试题列表
function CreateQuestionUL(question) {
    var zone = $("<div id='QuestionUL'></div>");
    var title = $("<div id='Qtitle' class='left'><label id='Labtitle'>题号 " + question[0]["QuestionId"] + "： </label><label id='Labquestion'>" + question[0]["QuestionTitle"] + "</label></div>");
    var AnswerA = $("<div class='answer left' ><input  type='radio' value='A：" + question[0]["AnswerA"] + "' name='answer' />&nbsp&nbsp&nbsp&nbspA：" + question[0]["AnswerA"] + "</div>");
    var AnswerB = $("<div class='answer left'> <input type='radio'  value='B：" + question[0]["AnswerB"] + "'  name='answer' />&nbsp&nbsp&nbsp&nbspB：" + question[0]["AnswerB"] + "</div>");
    var AnswerC = $("<div class='answer left'><input  type='radio'  value='C：" + question[0]["AnswerC"] + "'  name='answer' />&nbsp&nbsp&nbsp&nbspC：" + question[0]["AnswerC"] + "</div>");
    var AnswerD = $("<div class='answer left'> <input   type='radio' value='D：" + question[0]["AnswerD"] + "'  name='answer' />&nbsp&nbsp&nbsp&nbspD：" + question[0]["AnswerD"] + "</div>");
    //if (question[1]["LogPracticeAnswer"] != null) {
    //    switch (question[1]["LogPracticeAnswer"]) {
    //        case 1: AnswerA.attr("checked", "checked"); return;
    //        case 2: AnswerB.attr("checked", "checked"); return;
    //        case 3: AnswerC.attr("checked", "checked"); return;
    //        case 4: AnswerD.attr("checked", "checked"); return;
    //        default: return;
    //    }
    //}
    var answer = $("<div id='Qanswer' class='right'><label>您选择的答案：</label><label id='Yanswer'></label></div>");
    //下面几个是隐藏区域
    var correctanswer = $("<label id='correctanswer'>" + question[0]["CorrectAnswer"] + "</label>");
    correctanswer.hide();
    var loganswer = question[1]["LogPracticeAnswer"] == null ? "-1" : question[1]["LogPracticeAnswer"];
    var YLoganswer = $("<label id='YLoganswer'>" + loganswer + "</label>");
    YLoganswer.hide();
    var LogPracticeId = $("<div id='logparcticeid'>" + question[1]["LogPracticeId"] + "</div>");
    LogPracticeId.hide();
    var questionid = $("<div id='questionid'>" + question[0]["QuestionId"] + "</div>");
    questionid.hide();
    var commentstep = $("<div id='commentstep'>0</div>");
    commentstep.hide();
    var ImageAddress = $("<div id='Qimage'><label>参考图：</label><img id='ImageAddress' alt='图表' src='/OnLineTestWebSite/QuestionImages/" + question[0]["ImageAddress"] + "'/></div>");
    var Supported = $("<div id='Qsupport' class='right'></div>");
    var ding = $("<label id='up'> 顶： </label><label id='Labup'>" + question[0]["IsSupported"] + "</label>");
    var cai = $("<label id='down'> &nbsp&nbsp&nbsp&nbsp踩： </label><label id='Labdown'>" + question[0]["IsDeSupported"] + "</label>");
    Supported.append(ding, cai);
    var DifficultyDescrip = $("<div id='Qdifficult' class='right'><label>难度：</lable><lable>" + question[0]["DifficultyDescrip"] + "</label></div>");
    var TextBookANDChapter = $("<div id='Qtextbook' class='right'><label>教材：</label><label>" + question[0]["TextBookName"] + "</label><label>    章节：</label><label>" + question[0]["ChapterName"] + "</label>" + + "</div>");

    var subcomment = $("<div id='Qcomment'><textarea id='comment'></textarea></div>");
    var BTNsubcomment = $("<div id='Qsubcomment'></div>");
    var labsubcomment = $("<label id='Labsubcomment'>提交评论</label>");
    BTNsubcomment.append(labsubcomment);
    var querypracticelog = $("<div id='chktestlog' class='right'>查看测试记录</div>");
    var querycoment = $("<div id='chkcomment' class='right'>查看评论</div>");
    zone.append(title, AnswerA, AnswerB, AnswerC, AnswerD, answer, correctanswer, YLoganswer, questionid, commentstep, ImageAddress, Supported, DifficultyDescrip, TextBookANDChapter, LogPracticeId, subcomment, BTNsubcomment, querypracticelog, querycoment);
    //试题图像是否显示
    if (question[0]["ImageAddress"] == "Default.jpg") {
        ImageAddress.remove();
    }
    //试题所属的教材和科目是否显示
    if (question[0]["TextBookName"] == null && question[0]["ChapterName"] == null) {
        TextBookANDChapter.remove();
    }

    //顶 按钮所对应的动作
    ding.on({
        "mouseenter": function () { $(this).css({ "cursor": "pointer" }) },
        "mouseout": function () { $(this).css({ "cursor": "default" }) },
        "click": function () { upORdown_func(1) }
    });
    //踩按钮所对应的动作
    cai.on({
        "mouseenter": function () { $(this).css({ "cursor": "pointer" }) },
        "mouseout": function () { $(this).css({ "cursor": "default" }) },
        "click": function () { upORdown_func(-1) }
    });
    //提交评论按钮的动作
    labsubcomment.on({
        "mouseenter": function () { $(this).css({ "background-color": "#FF9924" }) },
        "mouseout": function () { $(this).css({ "background-color": "#eee" }) },
        "click": subcomment_click
    });
    //查看测试记录按钮的动作
    querypracticelog.on({
        "click": gettestlog
    });
    //查看评论按钮的动作
    querycoment.on({
        "click": getcomment
    });
    return zone;
}

//返回上一道试题
function leftnarrow_click() {
    var currentlogpracticeid = $("#logparcticeid").text();
    Uajax({ "LogPracticeId": currentlogpracticeid }, "../ashx/getLastLogpracticeAndQuestion.ashx", getQuestion_success, Error, Main);
}

//继续下一道试题
function rightnarrow_click() {
    Submit();
}

//顶或踩的动作,顶UOD=1,踩UOD=-1
function upORdown_func(UOD) {
    var questionid = $("#questionid").text();
    var obj = $("#Labup");
    if (UOD < 0)
        obj = $("#Labdown");
    Uajax({ "questionid": questionid, "upORdown": UOD }, "../ashx/handlerupORdown.ashx", handlerupORdown_success, handlerupORdown_Error, obj);
}
//处理顶或踩成功
function handlerupORdown_success(data, obj) {
    if (data == true)
        obj.text(obj.text() * 1 + 1);
    else
        alert("亲，每次间隔10分钟才可以顶踩一次哦。");
}
//处理顶或踩失败
function handlerupORdown_Error(obj) {
    alert("亲，每次间隔10分钟才可以顶踩一次哦。");
}
//查看测试记录按钮的动作
function gettestlog() {
    alert("textlog");
}

//查看评论按钮的动作
function getcomment() {
    Uajax({ "step": $("#commentstep").text(), "QuestionId": $("#questionid").text() }, "../ashx/GetComment.ashx", CommentSuccess, Error, $("chkcomment"));
}
//获取评论成功
function CommentSuccess(data, obj) {
    $(".comment").remove();
    if (data != null) {
        for (var i = 0; i < data.length; i++) {
            $("#chkcomment").append(CreateCommentStruct(data[i]));
        }
    }
}
//提交评论
function subcomment_click() {
    
    var comment_textarea = $("#comment");
    if (comment_textarea.length > 0) {
        var comment_val = comment_textarea.val().trim();
        if (comment_val.length > 0) {
            Uajax({ "Comment": comment_val, "QuestionId": $("#questionid").text(), "QuoteCommentId": "-1" }, "../ashx/InsertComment.ashx", InsertComment_success, Error, comment_textarea);
        } else {
            alert("内容不能为空");
        }
    } else {
        alert("提交评论错误。");
    }
}
//提交评论成功
function InsertComment_success(data, obj) {
    $(obj).val("");
    console.log(data);
    alert("提交评论成功。");
}
//获取试题失败
function getQuestion_Error(obj) {
    alert("error");
}

//错误
function Error(obj) {
    loading.empty();
    var loaderror = $("<img src='../Images/false.png' alt='载入错误'/>载入错误！");
    loading.append(loaderror);
}


//生成评论的HTML结构
function CreateCommentStruct(comment) {
    var commentZone = $("<div class='comment' id='comment" + comment.CommentId + "'></div>");//最外围的一个圈
    var commentZoneTitle;
    if (comment.AddDelBTN) {//只有自身的评论才能够删除，判断是否添加删除按钮  放在第一行
        var delbtn = $("<label class='delete'>删除</label>");
        delbtn.css({ "display": "none" });
        delbtn.mouseover(function () {
            $(this).css({ "color": "#FF9924", "cursor": "pointer" });
        }).mouseout(function () {
            $(this).css({ "color": "GrayText", "cursor": "default" });
        }).click(function (data) {
            DelBtnClick(data);
        });
        commentZone.mouseenter(function () { delbtn.fadeIn("fast") }).mouseleave(function () { delbtn.fadeOut("fast") });
        commentZoneTitle = $("<div class='comment-title'>我的评论 :</div>");//第一行
        commentZoneTitle.append(delbtn);

    } else {
        commentZoneTitle = $("<div class='comment-title'> </div>");//第一行
    }
    commentZone.append(commentZoneTitle);
    var leftcommentauthormessage = $("<div class='leftcommentauthormessage'><img class='userimage' alt='用户图像' src='../UserImages/" + comment.UserImageName + "' /></div>");
    var rightcomment = $("<div class='rightcomment'></div>");
    var commenttitle = $("<div class='commenttitle'><label class='username'>" + comment.UserName + "</label><label class='commenttime'>" + JsonToDate(comment.CommentTime).Format("yyyy-M-d h:m:s") + "</label></div>");
    var commentcontent = $("<div class='commentcontent'>" + comment.CommentContent + "</div>");
    commentZone.mouseover(function () { $(this).css({ "border": "solid 1px #FF9924" }) }).mouseout(function () { $(this).css({ "border": "NONE", "border-top": "1px dotted #DDD" }) });
    rightcomment.append(commenttitle, commentcontent);
    commentZone.append(leftcommentauthormessage, rightcomment);
    return commentZone;
}
