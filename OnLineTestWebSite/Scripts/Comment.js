var NoComment = $("<div class='loadlab'><a href='#'><img src='../Images/TextbookMaintenance.png' alt='没有数据' />你还没有发表过评论咧!</a></div>");
var Error = $("<div class='loadlab'><a href='#'><img src='../Images/false.png' alt='获取数据失败' />获取数据失败!</a></div>");
var LoadingFinish = $("<div id='loadingfinish' class='loadlab'>数据加载完成。</div>");
var LoadingError = $("<div id='loadingfinish' class='loadlab'>数据加载失败。</div>");
var Aloading = $("<div class='loadlab'><a href='#'><img src='../Images/loading.gif' alt='正在载入' />正在载入</a></div>")
var finishedLoading = false;//加载完成的标记
var loading = false;//正在加载的标记
var hidden;
$(document).ready(function () {
    hidden = $("#currentNum");
    InitComment(0);
    //滚动条绑定事件
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
        if (scrollTop + clientHeight == htmlHeight) {
            if (!finishedLoading && !loading) {
                InitComment(hidden.val());
            }
        }
    });
});

//-----------------------自定义函数区------------------------------------
function InitComment(step) {
    var content = $("#content");
    content.append(Aloading);
    loading = true;//改变标记
    Uajax({ step: step }, "../ashx/GetComment.ashx", CommentSuccess, CommentError, content);
}

function CommentSuccess(data, obj) {
    if (Aloading) {
        Aloading.remove();
    }
    if (Error) {
        Error.remove();
    }
    if (data == null) {//数据为空  1、本身没有数据 2、数据已经全部取完
        if ($(".Zone").length == 0) {//表示第一次加载就没有数据
            obj.append(NoComment);
        } else {
            obj.append(LoadingFinish);
        }
        finishedLoading = true;//后续不要再加载了
    } else {
        for (var i = 0; i < data.length; i++) {
            obj.append(CreateCommentZone(data[i]));
        }
        var value = hidden.val();
        hidden.val(parseInt(value) + 1)
    }
    loading = false;
}

function CommentError(obj) {
    if (Aloading) {
        Aloading.remove();
    }
    if (Error) {
        Error.remove();
    }
    obj.append(Error);
    loading = false;
}
//根所获得的评论信息 产生一个显示的jquery对象样式
function CreateCommentZone(comment) {
    var Zone = $("<div class='Zone' id=zone'" + comment.CommentId + "'></div>");
    Zone.append(CreateQuestionStruct(comment), CreateCommentStruct(comment));
    var showallbtn = $("<div class='commentbtn'><label class='showall'>更多评论>></label><label class='hidden'>0</label></div>");
    showallbtn.mouseover(function () {
        $(this).css({ "color": "#FF9924", "cursor": "pointer" });
    }).mouseout(function () {
        $(this).css({ "color": "GrayText", "cursor": "default" });
    }).click(function (data) {//添加双击事件
        ShowAllBtnClick(data);
    });
    Zone.append(showallbtn);
    return Zone;
}
//生成试题的HTML结构
function CreateQuestionStruct(comment) {
    var questionZone = $("<div class='question' id='question" + comment.QuestionId + "'></div>");
    var title = $("<div class='title'> 试题： " + comment.QuestionTitle + "</div>");
    var choose = $("<div class='choose'>A、" + comment.AnswerA + "</div><div class=choose> B、 " + comment.AnswerB + "</div><div class=choose> C、 " + comment.AnswerC + "</div><div class=choose> D、 " + comment.AnswerD + "</div>");
    var answer = $("<div class='answer'>" + InitQuestionAnswer(comment) + "</div>");
    questionZone.mouseover(function () { $(this).css({ "border": "solid 1px #FF9924" }) }).mouseout(function () { $(this).css({ "border": "NONE" }) });
    questionZone.append(title, choose, answer);
    return questionZone;
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
//更多评论按钮的双击事件
function ShowAllBtnClick(data) {
    var QuestionId = $(data.target).parent().siblings().first().attr("id").substring(8);
    var PageNum = $(data.target).siblings().html();
    var handler = $(data.target);
    //handler.replaceWith("<label class='LABloadingcomment'><img src=''>数据正在加载中…</label>");
    Uajax({ QuestionId: QuestionId, PageNum: PageNum }, "../ashx/GetMoreCommentByQuestionId.ashx", GetMoreCommentByQuestionIdSuccess, GetMoreCommentByQuestionIdError, handler);
}
//删除按钮的双击事件
function DelBtnClick(data) {
    var CommentId = $(data.target).parent().parent().attr("id").substring(7);
    var handler = $(data.target);
    Uajax({ CommentId: CommentId }, "../ashx/DeleteComment.ashx", DeleteCommentSuccess, DeleteCommentError, handler);
}
//删除评论成功
function DeleteCommentSuccess(data, handler) {
    // console.log("DeleteCommentSuccess: " + data);
    if (handler.parent().parent().siblings().hasClass("comment")) {
        handler.parent().parent().remove();
    } else {
        handler.parent().parent().parent().fadeOut("slow", function () { $(this).remove(); })
    }
}
//删除评论错误
function DeleteCommentError(data, handler) {
    //console.log("DeleteCommentError: " + data);
    alert("删除评论失败。");
}
//加载更多评论成功
function GetMoreCommentByQuestionIdSuccess(data, handler) {
    //console.log("GetMoreCommentByQuestionIdSuccess: " + data);
    var pagenum = handler.siblings().html();
    handler.siblings().html(parseInt(pagenum) + 1);//设置隐藏的页码值
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var appendpos = handler.parent().prev();
            appendpos.after(CreateCommentStruct(data[i]));
        }
    }
    if (data.length < 10) {//表示最后一次加载,因为服务器每次返回10个长度的值
        handler.parent().replaceWith("<div class='LABallcommentfinish'><label>全部评论加载完成.</label></div>");
    }
}
//加载更多评论错误
function GetMoreCommentByQuestionIdError(data, handler) {
    handler.replaceWith("<label class='LABcommentloadingerror'>数据加载错误.</label>");
}