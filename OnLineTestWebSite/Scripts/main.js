//-------------------------------随页面一起载入运行-----------------------------------------
var LabNum; var isTrue; var positionArr;
$(document).ready(function () {
    LabNum = $("#LabNum");
    LabNum.val(0);
    isTrue = true;
    //定义一个二维数组用于存放瀑布流的位置，第一维存放top值，第二维存在left值
    initPositionArray();
    //初始化用户信息部分
    GetCurrentUser();
    //初始化用户测试记录部分
    Uajax({ num: 20 }, "./ashx/GetLogTest.ashx", initLogtest, GetLogtestError, this);
    //初始化载入热题
    initPopularQuestion()
    //滚动条的滚动到页面最底部事件
    //参数 isTrue： 是一个隐藏的jquery控件 其val为true 或 false  不于控制方法是否执行的条件
    //参数 ScrollEvent： 方法 执行的方法
    $(window).bind("scroll", function (event) {
        //下面这句主要是获取网页的总高度
        var htmlHeight = $(document).height();
        //clientHeight是网页在浏览器中的可视高度
        var clientHeight = $(window).height();
        //scrollTop是浏览器滚动条的top位置，        
        //var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;
        var scrollTop = $(window).scrollTop();
        //document.title = "html" + htmlHeight + "client" + clientHeight + "scroll" + scrollTop;
        //通过判断滚动条的top位置与可视网页之和与整个网页的高度是否相等来决定是否加载内容；       
        if (scrollTop + clientHeight == htmlHeight && isTrue) {
            initPopularQuestion();
        }
    });

})



//---------------------------------自定义函数区---------------------------------------------
//初始化位置数组 
//将x值全部置为0  y值分别置为 0，282和563
function initPositionArray() {
    positionArr = new Array(3);
    for (var i = 0; i < positionArr.length; i++) {
        positionArr[i] = new Array(2);
        for (var j = 0; j < positionArr[i].length; j++) {
            positionArr[i][j] = 0;
        }
    }
    positionArr[0][1] = 0;
    positionArr[1][1] = 282;
    positionArr[2][1] = 563;
}
//初始化热题部分
//此部分的试题要求
//1、当前用户上传的
//2、已经审核通过的
//3、没有被软删除的
//试题的展示按照上传时间的倒序
function initPopularQuestion() {
    isTrue = false;
    Uajax({ pagenum: LabNum.val() }, "./ashx/getQuestion.ashx", getQuestionSuccess, getQuestionError, $("#PopularQuestion"));
}
//异步获取试题成功
function getQuestionSuccess(data, handler) {
    var LabLoading = $("#load_PopularQuestion");
    if (data != null && data.length > 0) {//确认数据存在
        LabLoading.remove();
        handler.css({ "border": "none" });
        for (var i = 0; i < data.length; i++) {
            var z = CreateZoneByQuestion(data[i]);
            handler.append(z);
            setQuestionZonePosition(z, positionArr);
        }
        var maxTOP = Math.max(positionArr[0][0], positionArr[1][0], positionArr[2][0]);
        if (data.length == 10) {//因为服务器端已经固定每次取10个值
            LabNum.val(parseInt(LabNum.val()) + 1);
            isTrue = true;
        } else {
            var finish = $("<div id='loadingfinish'>数据全部加载完成。</div>");
            finish.css({ "border": "1px solid #FF9924", "width": "100%", "height": "40px", "position": "absolute", "top": maxTOP, "line-height": "40px" });
            handler.append(finish);
        }
        handler.height((maxTOP * 1 + 60) + "px");
    } else {
        if (LabLoading.length > 0) {
            LabLoading.replaceWith("<div id='loadingfinish'>你没有更多上传的试题通过审核哦，去  <a href='TestAndPractice/EditQuestion.aspx' id='edit_link'>编辑试题</a>  看看吧。</div>");
        }
    }
}
//异步获取数据失败
function getQuestionError(data, handler) {
    var LabLoading = $("#load_PopularQuestion");
    if (LabLoading.length > 0) {
        LabLoading.replaceWith("<div id='loadingfinish'>数据载入错误。</div>");
    } else {
        handler.append("<div id='loadingfinish'>数据载入错误。</div>");
    }
    isTrue = true;
}
//设置位置
function setQuestionZonePosition(questionzone) {
    var foot = $("#footer");
    var QuestionZone = questionzone;
    var TOPSubscript = getMinposi(new Array(positionArr[0][0], positionArr[1][0], positionArr[2][0]));
    var height = questionzone.height() + 33;//区域块的高度
    var top = positionArr[TOPSubscript][0];
    var left = positionArr[TOPSubscript][1];
    questionzone.css({ "position": "absolute", "top": top + "px", "left": left + "px" }).fadeIn("slow");
    //questionzone.css({ "position": "absolute" }).animate({top:top,left:left, opacity: "show" },500);
    positionArr[TOPSubscript][0] = positionArr[TOPSubscript][0] + height;
}
//得到最小值的下标
function getMinposi(array) {
    var min = array[0];
    var j = 0;
    for (var i = 0; i < array.length; i++) {
        if (array[i] < min) {
            min = array[i];
            j = i;
        }
    }
    return j;
}
//利用Question生成一个Div
function CreateZoneByQuestion(question) {
    var QuestionZone = $("<div class='QuestionZone' id='QuestionZone" + question.QuestionId + "'></div>");//最外面的包围圈
    var QuestionZoneTitle = $("<div class='QuestionZoneTitle'></div>");//标题行，主要包括作者信息
    var titlecontent = $("<ul class='QuestionZoneTitleContent'><li class='UserImageName'><img class='UserImage' alt='用户图像'  src='/OnLineTestWebSite/UserImages/" + question.UserImageName + "' /></li><li class='UserName'>" + question.UserName + "</li><li class='UpLoadTime'>" + JsonToDate(question.UpLoadTime).Format("yyyy-MM-dd") + "</li></ul>");
    QuestionZoneTitle.append(titlecontent);
    var QuestionContent = $("<div  class='QuestionContent'>" + question.QuestionTitle + "</div>");//试题内容
    QuestionZone.append(QuestionZoneTitle, QuestionContent);//
    if (question.ImageAddress != null && question.ImageAddress != undefined) {
        var QuestionImage = $("<div class='QuestionImage'><img class='ImageAddress' alt='图表' src='/OnLineTestWebSite/QuestionImages/" + question.ImageAddress + "'/></div>");//试题图表
        QuestionZone.append(QuestionImage);
    }
    var AnswerA = $("<div  class='Answer'>A、 " + question.AnswerA + "</div>");//选项A
    var AnswerB = $("<div  class='Answer'>B、 " + question.AnswerB + "</div>");//选项B
    var AnswerC = $("<div  class='Answer'>C、 " + question.AnswerC + "</div>");//选项C
    var AnswerD = $("<div  class='Answer'>D、 " + question.AnswerD + "</div>");//选项D
    var CorrectAnswer = $("<div  class='CorrectAnswer'><label class='LabCorrectAnswer'>参考答案 </label>" + InitQuestionAnswer(question) + "</div>");//参考答案
    var Tail = $("<div  class='Tail'></div>");//尾巴，主要包括顶、踩，难度说明
    var tailcontent = $("<ul class='tailcontent'><li class='IsSupported'>顶[" + question.IsSupported + "]</li><li class='IsDeSupported'>踩[" + question.IsDeSupported + "]</li><li class='DifficultyDescrip'>难度[" + question.DifficultyDescrip + "]</li></ul>");
    Tail.append(tailcontent);
    QuestionZone.append(AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer, Tail);
    QuestionZone.on({
        "mouseover": function () { $(this).css({ "border": "1px solid #FF9924", "cursor": "pointer" }) },
        "mouseout": function () { $(this).css({ "border": "1px solid #DDD" }) },
        "click": function () { window.location.href = "TestAndPractice/ShowQuestion.aspx?questionid=" + question.QuestionId; }
    });
    QuestionZone.css({ "display": "none" });
    return QuestionZone;
}
//获取当前用户信息
function GetCurrentUser() {
    var result;
    $.ajax({
        type: "post",
        url: "./ashx/GetCurrentUser.ashx",
        data: {},
        dataType: "json",
        async: true,
        beforeSend: function () {
            result = null;
        },
        success: function (data, loading) {
            if (data != null && data.length == 4) {
                initUserInfo(data);
            }
        },
        error: function () {
            var error = $("<a href='#'><img src='Images/false.png' alt='载入错误' />载入错误</a>");
            var loading = $("#loading_user");
            loading.empty();
            loading.append(error);
        }
    });
}

//初始化第二行，用户测试记录
function initLogtest(logtests, obj) {
    var loading = $("#LastOperation");
    loading.empty();
    var title = $("<div class='title logtestitem'></div>");
    var tr = $("<tr><td class='logteststarttime'>练习时间</td><td class='papercode'>试卷代码</td><td class='chinesename'>试卷类型</td><td class='subjectname'>科目</td><td class='logtestscore'>得分</td><td class='papercodetotalscore'>总分</td><td class='difficultyratio'>难度系数</td><td class='difficultydescrip'>难度描述</td><td class='usedteim'>使用时间</td><td class='timerange'>总时间</td></tr>");
    title.append(tr);
    loading.append(title);
    if (logtests.length > 0) {
        for (var i = 0; i < logtests.length; i++) {
            var item = $("<a href='#'></a>").append(CreateLogtestItem(logtests[i], i));
            item.mouseenter(function () {
                $(this).css({ "color": "#FF9924" })
            }).mouseleave(function () {
                $(this).css({ "color": "black" });
            }).hover(function () { $(this).css({ "text-decoration": "none" }) });
            item.fadeIn("slow").appendTo(loading)
        }
    } else {
        var NoLogtest = $("<div id='nolog'>无测试记录</div>");
        NoLogtest.fadeOut("slow");
        loading.append(NoLogtest);
    }
}

//生成一条logtestitem记录
function CreateLogtestItem(logtest, i) {
    var div = $("<div  class='logtestitem'></div>");
    var tr = $("<tr></tr>");
    var chinesename = $("<td class='chinesename'>" + logtest.chinesename + "</td>");
    var difficultydescrip = $("<td class='difficultydescrip'>" + logtest.difficultydescrip + "</td>");
    var difficultyratio = $("<td class='difficultyratio'>" + logtest.difficultyratio + "</td>");
    var difficultyremark = $("<td class='difficultyremark'>" + logtest.difficultyremark + "</td>");
    var logtestscore = $("<td class='logtestscore'>" + logtest.logtestscore + "</td>");
    var logteststarttime = $("<td class='logteststarttime'>" + JsonToDate(logtest.logteststarttime).Format("yyyy-MM-dd") + "</td>");
    var papercode = $("<td class='papercode'>" + logtest.papercode + "</td>");
    var papercodepassscore = $("<td class='papercodepassscore'>" + logtest.papercodepassscore + "</td>");
    var papercoderemark = $("<td class='papercoderemark'>" + logtest.papercoderemark + "</td>");
    var papercodetotalscore = $("<td class='papercodetotalscore'>" + logtest.papercodetotalscore + "</td>");
    var subjectname = $("<td class='subjectname'>" + logtest.subjectname + "</td>");
    var subjectremark = $("<td class='subjectremark'>" + logtest.subjectremark + "</td>");
    var timerange = $("<td class='timerange'>" + logtest.timerange + "</td>");
    var usetime = $("<td class='usedteim'>" + usedtime(logtest.logtestendtime, logtest.logteststarttime) + "</td>");
    tr.append(logteststarttime, papercode, chinesename, subjectname, logtestscore, papercodetotalscore, difficultyratio, difficultydescrip, usetime, timerange);
    return div.append(tr);
}

//获到logtest任务错误时使用的函数
function GetLogtestError() {
    var error = $("<a href='#'><img src='Images/false.png' alt='载入错误' />载入错误</a>");
    var loading = $("#LastOperation");
    loading.empty();
    loading.append(error);
}

//计算用时
function usedtime(endtime, startime) {
    var usedtime = null;
    if (endtime != null && startime != null) {
        usedtime = (new Date(parseInt(JsonToDate(endtime).getTime() - JsonToDate(startime).getTime())).Format("hh:mm:ss"));
    };
    return usedtime;
};

///初始化第一行，用户的信息
function initUserInfo(data) {
    //第一行的标签
    var firstRow = $("<div id='UserInfo' class='MainContent'></div>");

    //第一列的内容
    var firstCol = $("<div id='UserScore' class='UserInfo_Row1'></div>");
    var more = $("<div class='more'><a href='#'>更多></a></div>");
    var caifuzhi = $("<div id='caifuzhi'> 财富值: <label id='Score'>" + data[0].UserScore + "</label>分 </div>");
    var detail = $("<div id='detail'></div>");
    var shouru = $("<label id='shouru'>收入:<label id='shourufeng'><a href='#'>" + data[1].shouru + "分</a></label></label>");
    var zhichu = $("<label id='zhichu'>支出:<label id='zhichufeng'><a href='#'>" + data[1].zhichu + "分</a></label></label>");
    detail.append(shouru, zhichu);
    var other = $("<div id='other'></div>");
    var email = $("<a href='#'>Email:<label id='email'>" + data[0].UserEmail.substr(0, 12) + "</label></a>");
    var phone = $("<a href='#'>电话:<label id='phone'>" + data[0].Tel + "</label></a>");
    var regtime = $("<a href='#'>注册日期:<label id='regdate'>" + JsonToDate(data[0].UserRegisterDatetime).Format("yyyy-MM-dd") + "</label></a>");
    other.append(email, phone, regtime);
    firstCol.append(more, caifuzhi, detail, other);

    //第二列的内容
    var secondCol = $("<div id='NextRank' class='UserInfo_Row1'></div>");
    var more2 = $("<div class='more'><a href='#'>更多></a></div>");
    var ranktitle = $("<div class='sec_col_title'>下一等级</div>");
    var rankcontent = $("<div class='sec_col_content' id='Div1'><a href='#'>" + data[2].UserRankName + "</a></div>");
    var nextcfzt = $("<div class='sec_col_title'>需要财富值:</div>");
    var nextcfzc = $("<div class='sec_col_content' id='needscore'><a href='#'>" + (data[2].MinScore - data[0].UserScore) + "分</a></div>");
    var paimingt = $("<div class='sec_col_title'>目前财富排名:</div>");
    var paimingc = $("<div class='sec_col_content' id='mingci'><a href='#'>" + data[1].paiming + "名</a></div>");
    secondCol.append(more2, ranktitle, rankcontent, nextcfzt, nextcfzc, paimingt, paimingc);

    //第三列的内容
    var thirdCol = $("<div id='Message' class='UserInfo_Row1'></div>");
    var messagetitle = $("<div>站内信:</div>");
    var nmnumb = $("<div id='newmessage_number'><a href='#'>" + data[1].message_weidu + "条</a></div>");
    var lweidu = $("<div id='lab_weidu'>未读</div>");
    var yidu = $("<div class='third_col'>已读:<a href='#'>" + (data[1].message_recerve - data[1].message_weidu) + "条</a></div>");
    var sended = $("<div class='third_col'>发送:<a href='#'>" + data[1].message_sended + "条</a></div>");
    var newmessage = $("<div id='newmessage'><a href='#'>新建</a></div>");
    thirdCol.append(messagetitle, nmnumb, lweidu, yidu, sended, newmessage);

    //第四列的内容
    var forthCol = $("<div id='LogLogin' class='UserInfo_Row1'></div>");
    var log = $("<div>登录信息:</div>");
    var ip = $("<div class='logininfo'>IP：<a href='#'>" + data[3].LogLoginIp + "</a></div>");
    var logtimetitle = $("<div class='logininfo'>登录时间：</div>");
    var logtime = $("<div class='logininfo' id='logintime'><a href='#'>" + JsonToDate(data[3].LogLoginTime).Format("yyyy-MM-dd hh:mm") + "</a></div>");
    logtimetitle.append(logtime);
    var stattime = $("<div class='logininfo' id='stay_time'>停留：<a href='#'>" + new Date(parseInt((new Date() - JsonToDate(data[3].LogLoginTime) - 8 * 3600 * 1000))).Format("hh:mm:ss") + "</a></div>");
    var sys = $("<div class='logininfo'>系统：<a href='#'>" + (data[3].LogLoginOperatiionSystem).split("版本号")[0] + "</a></div>");
    var web = $("<div class='logininfo'>工具：<a href='#'>" + data[3].LogLoginWebServerClient + "</a></div>");
    forthCol.append(log, ip, logtimetitle, stattime, sys, web);

    //全部添加到第一行中
    firstRow.append(firstCol, secondCol, thirdCol, forthCol);
    firstRow.fadeIn("slow");
    $("#loading_user").replaceWith(firstRow);

    //更新停留的时间
    setInterval(function () { FreshStaytime(JsonToDate(data[3].LogLoginTime), $("#stay_time a")) }, 1000);
}

//将序列化后的时间格式重新转为时间对象
function JsonToDate(json) {
    var re = /-?\d+/;
    var m = re.exec(json);
    return new Date(parseInt(m[0]));
}

// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//更新停留时间
function FreshStaytime(logtime, obj) {
    var now = new Date();
    var staytime = new Date(now - logtime - 8 * 3600 * 1000).Format("hh:mm:ss");
    obj.html(staytime);
}

//将试题格式化以后，添加到position内部的前面
function prependQuestion(position, question) {
    if (question != null && question != false) {
        var questionFrame = $("<div class='questionFrame' id='题号：" + question.QuestionId + "'></div>");
        var QuestionTitle = $("<div class='QuestionTitle'>题目：" + question.QuestionTitle + "</div> ");
        var AnswerA = $("<div class='AnswerA'>A、" + question.AnswerA + "</div>");
        var AnswerB = $("<div class='AnswerB'>B、" + question.AnswerB + "</div>");
        var AnswerC = $("<div class='AnswerC'>C、" + question.AnswerC + "</div>");
        var AnswerD = $("<div class='AnswerD'>D、" + question.AnswerD + "</div>");
        var CorrectAnswer = $("<div class='CorrectAnswer'>" + getcorrectanswer(question.CorrectAnswer, question) + "</div>");
        questionFrame.append(QuestionTitle, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer);
        questionFrame.hide();
        children = position.children();
        if (children.length > 10) {
            children.slice(10, children.length).remove();
        }
        position.prepend(position, questionFrame);
        questionFrame.show("2000");
    }
}

//正确的格式显示试题答案
function getcorrectanswer(CorrectAnswer, question) {
    var result = "正确答案：";;
    switch (CorrectAnswer) {
        case 1: {
            result += "A、" + question.AnswerA;
            return result;

        }
        case 2: {
            result += "B、" + question.AnswerB;
            return result;

        }
        case 3: {
            result += "C、" + question.AnswerC;
            return result;

        }
        case 4: {
            result += "D、" + question.AnswerD;
            return result;

        }
        default: {
            result += "没有正确答案";
            return result;

        }
    }
}

//从服务器随机获取试题
function getQuestion() {
    var result;
    $.ajax({
        type: "post",
        url: "./ashx/getQuestion.ashx",
        data: {},
        dataType: "json",
        async: false,
        beforeSend: function () {
            result = null;
        },
        success: function (data) {
            result = data;
        },
        error: function () {
            result = false;
        }
    });
    return result;
}