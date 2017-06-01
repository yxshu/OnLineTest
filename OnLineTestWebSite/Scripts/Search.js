/*--------文档载入的时候执行的方法--------------------*/
var finishedLoading = false;//加载完成标志
var loadedfinishtitle = false;//加载完成标签是否显示标志
var loading = false;//正在加载中标志
var loaderror = false;//加载错误标志
$(document).ready(function () {
    var txtKeyword = $("#keywords");//输入框口
    var arraySearchTitle = $("#SearchTitle span");//获取搜索类别的数组
    var HideSubject = $("#Subject");//这是一个隐藏域，提交搜索的项目
    var currentNum = $("#currentNum");
    var btnSubmit = $("#btnSubmit");//搜索按钮
    var divResult = $("#Result");//显示搜索结果的区域

    //输入框口绑定取得关键词的方法
    txtKeyword.bind({
        "focus keyup": function () { GetKeywordsSuggestion(txtKeyword) },
        "blur": function (event) { RemoveSuggestionKeywordLayer() }
    });

    //搜索标题绑定的方法
    arraySearchTitle.each(function (index) {
        $(this).bind("click", function (index) { ChangeSearchTitleStatus($(this), index, arraySearchTitle, HideSubject) });
    });

    //搜索按钮绑定的动作
    btnSubmit.bind("click", function () {
        if (txtKeyword.val().trim() != "" && txtKeyword.val().trim() != null && HideSubject.val().trim() != "") {
            if (currentNum.val() != 0) {
                currentNum.val(0);
                divResult.empty();
                finishedLoading = false;
                loadedfinishtitle = false;
                loading = false;
                loaderror = false;
            }
            GetSearchResult(txtKeyword.val().trim(), HideSubject.val().trim(), currentNum.val(), 10, $("#Result"), currentNum);
        }
    });

    //滚动条绑定事件
    $(window).bind("scroll", function (event) {
        //下面这句主要是获取网页的总高度
        var htmlHeight = $(document).height();
        //clientHeight是网页在浏览器中的可视高度
        var clientHeight = $(window).height();
        //scrollTop是浏览器滚动条的top位置，        
        //var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;
        var scrollTop = $(window).scrollTop();
        document.title = "html" + htmlHeight + "client" + clientHeight + "scroll" + scrollTop;
        var finished = $("<div id='finished' class='CrossRowPromptTag'>已经全部加载完毕！</div>");
        //通过判断滚动条的top位置与可视网页之和与整个网页的高度是否相等来决定是否加载内容；       
        if (scrollTop + clientHeight == htmlHeight) {
            if (txtKeyword.val().trim() != "" && txtKeyword.val().trim() != null && HideSubject.val().trim() != ""&&!loaderror) {
                if (!finishedLoading && !loading) {
                    GetSearchResult(txtKeyword.val().trim(), HideSubject.val().trim(), currentNum.val(), 10, $("#Result"), currentNum);
                } else if (finishedLoading && !loadedfinishtitle) { //已经全部加载完毕
                    finished.appendTo(divResult);
                    loadedfinishtitle = true;
                }
            }
        }
    });


})
/*--------文档载入以后执行的方法--------------------*/

//利用ajax从服务器获取关键词,参数是输入框对象，方便后面取位置
function GetKeywordsSuggestion(txtKeyword) {
    var txtKeywordoffset = txtKeyword.offset();
    var txtKeywordouterHeight = txtKeyword.outerHeight();
    var txtKeywordinnerWidth = txtKeyword.innerWidth();
    var liHeight = "30px";
    RemoveSuggestionKeywordLayer();
    var keyword = txtKeyword.val().replace(/(^\s*)|(\s*$)/g, "");
    if (keyword != "" && keyword != null) {
        $.ajax({
            type: "post",
            url: "./ashx/SearchSuggestion.ashx",
            data: { "term": keyword },
            dataType: "json",
            async: true,
            success: function (data) {
                if (data.length > 0) {
                    var layer = "<div id=keywordslayer>";
                    for (var i = 0; i < data.length; i++) {
                        if (data[i] != "" && data[i] != null) {
                            layer += "<li>" + data[i] + "</li>";
                        }
                    }
                    layer += "</div>";
                    txtKeyword.after(layer);
                    /*下面设置提示层的CSS*/
                    var layertop, layerleft, layerwidth, layerheight;
                    layerwidth = txtKeywordinnerWidth;
                    layerheight = data.length * parseInt(liHeight);
                    layertop = parseInt(txtKeywordoffset.top) + parseInt(txtKeywordouterHeight);
                    layerleft = txtKeywordoffset.left;
                    $("#keywordslayer").css({
                        "position": "absolute",
                        "width": layerwidth,
                        "height": layerheight,
                        "top": layertop,
                        "left": layerleft,
                        "overflow": "hidden",
                        "background-color": "white",
                        "border": "solid 1px #FF9924"
                    });
                    $("#keywordslayer li").css({
                        "list-style-type": "none",
                        "display": "block",
                        "height": liHeight,
                        "line-height": liHeight,
                        "text-align": "left",
                        "padding-left": "5px"
                    }).each(function (index) {
                        var key;
                        $(this).bind({
                            "mouseenter": function () { $(this).css({ "cursor": "pointer", "background-color": "#FF9924" }); txtKeyword.val($(this).html()); },
                            "mouseleave": function () { $(this).css({ "background-color": "White" }) },
                            "click": function () {
                                RemoveSuggestionKeywordLayer();
                            }
                        });
                    });
                }
            }
        });
    }
}

/*关闭关键词提示层*/
function RemoveSuggestionKeywordLayer() {
    if (($("#keywordslayer")).length > 0) {
        $("#keywordslayer").remove();
    }
}

//改变搜索标题的状态,并把搜索类别写入到隐藏域中
function ChangeSearchTitleStatus(CurrentTitle, CurrentIndex, ArrayTitle, HideSubject) {
    var CurrentStatus = CurrentTitle.attr("id");
    HideSubject.val(CurrentStatus);
    ArrayTitle.each(function (index) { if (index != CurrentIndex) { $(this).css({ "font-weight": "normal", "color": "#ff9924", "text-decoration": "underline", "cursor": "pointer" }); } });
    CurrentTitle.css({ "font-weight": "bolder", "color": "black", "text-decoration": "none", "cursor": "default" });
}

//利用Ajax取得搜索的结果，并格式化到divResult的区域显示出来
function GetSearchResult(Keyword, SearchSubject, StarNum, TotalNum, divResult, currentNum) {
    var Subject = SearchSubject.trim() != "" && SearchSubject.trim() != null ? SearchSubject.trim() : "Question";
    var loadingtitle = $("<div class='CrossRowPromptTag'> <img src='./Images/loading.gif' alt='正在搜索中…' width='14px' height='14px'/>&nbsp;&nbsp; &nbsp;正在搜索中… </div>");
    var error = $("<div class='CrossRowPromptTag'> <img src='./Images/false.png' alt='获取数据失败…' width='14px' height='14px'/>&nbsp;&nbsp; &nbsp;<red>获取数据失败！<red> </div>");
    if (Keyword != null && Keyword != "") {
        $.ajax({
            type: "post",
            url: "./ashx/GetSearchResult.ashx",
            data: { "Keyword": Keyword, "Subject": Subject, "StarNum": StarNum, "TotalNum": TotalNum },  //关键词、类别、开始编号、总共希望取得的条数
            dataType: "json",
            async: true,
            beforeSend: function (XMLHttpRequest) {
                //this;  调用本次AJAX请求时传递的options参数
                if (currentNum.val() == 0) {
                    divResult.empty();
                }
                divResult.append(loadingtitle);
                loading = true;
            },
            success: function (data) {
                /*返回的结果分两个情况
                1、返回null,表明没有查询到结果；
                2、如果有结果的话，则返回一个数组，
                    分别为：
                        1、查询所使用的时间；
                        2、查询到的记录的数量；
                        3、返回的类型，为字符串“Question”或者是“Comment”
                        4、从第四条以后都是相应的结果
                */
                if (loadingtitle) {
                    loadingtitle.remove();
                }
                if (error) {
                    error.remove();
                }
                DataAnalizer(data, divResult, currentNum);
                loading = false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // 通常 textStatus 和 errorThrown 之中
                // 只有一个会包含信息
                //this;  调用本次AJAX请求时传递的options参数
                if (currentNum.val() == 0) {
                    divResult.empty();
                }
                if (loadingtitle) {
                    loadingtitle.remove();
                }
                divResult.append(error);
                loaderror = true;
                loading = false;
            }
        })
    }
}

//分析返回的数据并格式化
function DataAnalizer(data, divResult, currentNum) {
    var getNum = currentNum.val();
    if (data == null) {
        if (getNum == 0) {
            divResult.empty();
            divResult.append("<div id='NoResult' class='CrossRowPromptTag'>没有查询到数据…</div>");
        }
    } else {
        if (data.length >= 3) {
            var time = data[0].TotalMilliseconds;//花费的时间
            var totalnum = data[1];//得到的总条数
            var type = data[2];//类型
            var head = $("<div class='head'>为你找到相关页面" + totalnum + "个，共耗时：" + time + "毫秒。</div>");
            if (getNum == 0) {
                divResult.empty();
                divResult.append(head);
            };
            if (type == "Question") {
                for (var i = 3; i < data.length; i++) {
                    var TitleId = $("<div class='TitleId'>" + data[i].QuestionId + "、</div>");
                    var Title = $("<div class='Title'>" + data[i].QuestionTitle + "</div>");
                    var Choose = $("<div class='Choose'><div class='ChooseA'>A:" + data[i].AnswerA + "</div><div class='ChooseB'>B:" + data[i].AnswerB + "</div><div class='ChooseC'> C:" + data[i].AnswerC + "</div><div class='ChooseD'>D:" + data[i].AnswerD + "</div></div>");
                    var Answer = $("<div class='Answer'> 参考答案 " + getCorrectAnswer(data[i]) + "</div>");
                    var QItem = $("<div class='QItem'></div>");
                    QItem.append(TitleId).append(Title).append(Choose).append(Answer).appendTo(divResult);
                    QItem.bind("mouseenter", function (e) {
                        $(this).css({ "border": "solid 1px #FF9924", "cursor": "pointer" });
                    }).bind("mouseleave", function (e) {
                        $(this).css({"border":"solid 1px #DDD"});
                    });
                }
            } else {//返回的是Comment的内容

            }
            currentNum.val(parseInt(getNum) + parseInt(data.length) - parseInt(3));
            if (currentNum.val() == totalnum)
                finishedLoading = true;
        } else {//返回数据格式错误
            if (getNum == 0) {
                divResult.empty();
            }
            divResult.append("<div id='NoResult' class='CrossRowPromptTag'>数据格式错误…</div>");
            loaderror = true;
        }
    }
    function getCorrectAnswer(answer) {
        switch (answer.CorrectAnswer) {
            case 1: return "A：" + answer.AnswerA;
            case 2: return "B：" + answer.AnswerB;
            case 3: return "C：" + answer.AnswerC;
            case 4: return "D：" + answer.AnswerD;
            default: return "X：" + "上帝也没有正确答案";
        }
    }
}