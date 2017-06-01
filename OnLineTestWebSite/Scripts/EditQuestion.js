/// <reference path="../TestAndPractice/AddQuestion.aspx" />
/// <reference path="../TestAndPractice/AddQuestion.aspx" />
//-----------------------------------------------------随页面一起载入运行----------------------------------------------------------------
var AddNewQuestion;//定义新增按钮
var myQuestion;//定义已经上传试题区域
var IsAsyn;//是否正在进行异步操作
var GetAll;//所有试题已经全部获取完成
var PageNum;//当前获取试题的页码值
var arrayQuestionPosition;//用户存放最后一行试题左下角的坐标位置

$(document).ready(function () {
    AddNewQuestion = $("#AddNewQuestion");
    myQuestion = $("#myQuestion");
    IsAsyn = false;//异步操作是否正在执行的标志
    GetAll = false;//初始化成功获取所有试题的标记
    PageNum = $("#PageNum");//初始化当前页码的标记
    PageNum.val(0);
    arrayQuestionPosition = initPositionArray();//初始化存放试题位置的数组，这是一个有三个值的二维数组，形如：0,0;0,282;0,563
    getUploadedQuestion();
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
        if (scrollTop + clientHeight == htmlHeight && IsAsyn == false && GetAll == false) {
            getUploadedQuestion();
        }
    });
    AddNewQuestion.on("click", function () { window.location.href = "AddQuestion.aspx?questionid=-1" });
});

//----------------------------------------------------自定义函数区--------------------------------------------------------------------
//初始化位置数组 
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
    positionArr[2][1] = 561;
    return positionArr;
}
//根据当前登录的用户每次取得10个用户上传的试题
function getUploadedQuestion() {
    //同时只进行一个取数据的操作
    if (IsAsyn == false && GetAll == false) {//正常情况
        IsAsyn = true;
        Uajax({ pagenum: PageNum.val() }, "../ashx/getUploadedQuestion.ashx", getQuestionSuccess, getQuestionError, myQuestion);
    } else if (IsAsyn == true && GetAll == false) {//正在取值
        alert("服务器正在回地球的路上……");
    } else {//值已经取完
        alert("没有更多的数据……");
    }
}
//获取用户上传试题成功后的操作
function getQuestionSuccess(data, obj) {
    if (data != null && data.length > 0) {//正常取得非空的数据
        if (data.length == 10) {//正常取得10条记录，说明后面有可能没有数据有可能还有数据,仅展示数据
            PageNum.val(parseInt(PageNum.val()) + 1); //将页码将自增1;
        } else {//取得的数据少于10条，说明后面已经没有数据，展示数据并添加没有更多数据标签
            GetAll = true;
        }
        setQuestionPosition(data, obj);//设置试题的位置
    } else {//取得空值
        if (PageNum.val() == 0) {//第一次就取得空值，说明本身没有数据，仅添加没有数据标签
            obj.append("<div class='lab' id='nodata'>你还没有上传过试题哦……</div>");
        } else {//已经取得了值，本次取得空值，说明本身存在数据，已经全部取完，仅添加没有更多数据标签
            var finishdata = $("<div class='lab' id='finishdata'>没有更多的试题了……</div>");
            finishdata.css({ "position": "absolute", "top": arrayQuestionPosition[getMaxposi(new Array(arrayQuestionPosition[0][0], arrayQuestionPosition[1][0], arrayQuestionPosition[2][0]))][0] + "px" });
            obj.append(finishdata);
        }
        GetAll = true;
    }
    IsAsyn = false;
}
//获取用户上传试题失败后的操作
function getQuestionError(data, obj) {
    alert("获取试题失败……");
    IsAsyn = false;
}
//设置试题的位置
function setQuestionPosition(questionarray, addposition) {
    //设置每个试题的位置
    for (var i = 0; i < questionarray.length; i++) {
        var questionzone = initQuestionZone(questionarray[i]);//将每个试题生成一个DIV
        addposition.append(questionzone); //将试题添加到展示前台
        var questionheight = questionzone.height() + 33;//试题DIV的高度
        var minSub = getMinposi(new Array(arrayQuestionPosition[0][0], arrayQuestionPosition[1][0], arrayQuestionPosition[2][0]));//获得当前高度最小的下标 从0开始
        var top = arrayQuestionPosition[minSub][0];//试题区左上角的位置-纵坐标
        var left = arrayQuestionPosition[minSub][1];//试题区左上角的位置-横坐标
        questionzone.css({ "position": "absolute", "top": top + "px", "left": left + "px" }).fadeIn("slow");//设置试题的位置并显示
        arrayQuestionPosition[minSub][0] = top + questionheight;
    }
    //确定是否要添加已经全部加载的标签
    if (GetAll) {
        var loadfinish = $("<div class='lab' id='loadfinish'>试题已经全部加载完成。</div>");
        loadfinish.css({ "position": "absolute", "top": arrayQuestionPosition[minSub][0] + "px" });
        addposition.append(loadfinish);
    }
    var maxSub = getMaxposi(new Array(arrayQuestionPosition[0][0], arrayQuestionPosition[1][0], arrayQuestionPosition[2][0]));
    myQuestion.height(arrayQuestionPosition[maxSub][0] + 100);
}
//初始化试题区域
function initQuestionZone(question) {
    var QuestionZone = $("<div class='QuestionZone' id='QuestionZone" + question.QuestionId + "'></div>");//最外面的包围圈

    var QuestionZoneTitle = $("<div class='QuestionZoneTitle'></div>");//标题行，主要包括作者信息
    var titlecontent = $("<ul class='QuestionZoneTitleContent'><li class='UserImageName'><img class='UserImage' alt='用户图像'  src='/OnLineTestWebSite/UserImages/" + question.UserImageName + "' /></li><li class='UserName'>" + question.UserName + "</li><li class='UpLoadTime'>" + JsonToDate(question.UpLoadTime).Format("yyyy-MM-dd") + "</li></ul>");
    //添加状态标志
    var status;
    if (question.IsDelte) {
        status = $("<li class='status'>已删除</li>");
        status.css({ "color": "red" });
    } else {
        if (question.IsVerified) {
            status = $("<li class='status'>已审核</li>");
            status.css({ "color": "green" });
        } else {
            if (question.VerifyTimes > 0) {
                status = $("<li class='status'>审核中</li>");
                status.css({ "color": "#FF9924" });
            } else {
                status = $("<li class='status'>未审核</li>");
                status.css({ "color": "black" });
            }

        }
    }
    titlecontent.append(status);
    QuestionZoneTitle.append(titlecontent);
    var QuestionContent = $("<div  class='QuestionContent'>" + question.QuestionTitle + "</div>");//试题内容
    QuestionZone.append(QuestionZoneTitle, QuestionContent);//
    if (question.ImageAddress != null) {
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
    QuestionZone.mouseover(function () { $(this).css({ "border": "1px solid #FF9924", "background-color": "#EFEFEF" }) }).mouseout(function () { $(this).css({ "border": "1px solid #DDD", "background-color": "white" }) });
    QuestionZone.css({ "display": "none" });
    if (question.IsDelte) {
        QuestionZone.css({ "color": "GrayText", "cursor": "text" })
    } else {
        if (question.IsVerified) {
            QuestionZone.css({ "color": "green", "cursor": "pointer" });
            QuestionZone.on("click", function () { window.location.href = "ShowQuestion.aspx?questionid=" + question.QuestionId });
        } else {
            if (question.VerifyTimes > 0) {
                QuestionZone.css({ "color": "#FF9924", "cursor": "pointer" });
                QuestionZone.on("click", function () { window.location.href = "ShowQuestion.aspx?questionid=" + question.QuestionId });
            } else {
                QuestionZone.css({ "color": "black", "cursor": "pointer" });
                QuestionZone.on("click", function () { window.location.href = "UpdataQuestion.aspx?questionid=" + question.QuestionId });
            }
        }

    }
    return QuestionZone;
}
//得到数组中最小值的下标
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
//得到数组中最大值的下标
function getMaxposi(array) {
    var max = array[0];
    var j = 0;
    for (var i = 0; i < array.length; i++) {
        if (array[i] > max) {
            max = array[i];
            j = i;
        }
    }
    return j;
}

