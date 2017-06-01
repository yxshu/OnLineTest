//定义全局变量
var QuestionTitle,//试题题目
    QuestionAnswers,//四个选项
    CorrectAnswer,//参考答案
    ImageAddress,//上传图像
    ImageNewFileName,//图像上传以后的新名称
    DifficultyId,//难度系数
    UserName,//作者标签
    PaperCodeId,//试题类型
    TextBookId,//参考教材
    ChapterId,//参考章节
    PastExamPaperId,//是否真题
    PastExamRow,//是否真题行
    updata;//新增按钮行;

//随页面载入一起加载的方法和动作
$(document).ready(function () {
    //-----------------------初始化变量----------------------
    QuestionTitle = $("#QuestionTitle");//试题题目
    QuestionAnswers = $(".QC");//四个选项
    CorrectAnswer = $("#CorrectAnswer");//参考答案
    ImageAddress = $("#ImageAddress");//上传图像
    ImageNewFileName = null;//图像上传以后产生的新名称
    DifficultyId = $("#DifficultyId");//难度系数
    UserName = $("#UserName");//作者标签
    PaperCodeId = $("#PaperCodeId");//试题类型
    TextBookId = $("#TextBookId");//参考教材
    ChapterId = $("#ChapterId");//参考章节
    PastExamPaperId = $("#PastExamPaperId");//是否真题
    PastExamRow = $("#PastExamRow");//是否真题行
    updata = $("#updata");//新增按钮


    //--------------------试题选项 试题选项改变后，动态改变参考答案选项----------------------
    QuestionAnswers.each(function () {
        $(this).on({
            "change": function () {
                var text;
                if ($(this).val().length > 50) {
                    text = $(this).val().toString().substring(0, 30) + "……";
                } else {
                    text = $(this).val();
                }
                var selectedindex = CorrectAnswer[0].selectedIndex;//保存选定的值
                switch ($(this).context.id) {
                    case "AnswerA":
                        var option = $("<option value='1'>A、" + text + "</option>");
                        $(CorrectAnswer.children()[1]).replaceWith(option);
                        break;
                    case "AnswerB":
                        var option = $("<option value='2'>B、" + text + "</option>");
                        $(CorrectAnswer.children()[2]).replaceWith(option);
                        break;
                    case "AnswerC":
                        var option = $("<option value='3'>C、" + text + "</option>");
                        $(CorrectAnswer.children()[3]).replaceWith(option);
                        break;
                    case "AnswerD":
                        var option = $("<option value='4'>D、" + text + "</option>");
                        $(CorrectAnswer.children()[4]).replaceWith(option);
                        break;
                }
                CorrectAnswer.get(0).selectedIndex = selectedindex;
            }
        });
    });

    //---------------------------上传试题图像-------------------------------------------------
    ImageAddress.on({
        "change": function () {
            //创建FormData对象
            var data = new FormData();
            data.append("Type", "UpdataQuestion");//标记图像的属性
            data.append("QuestionId", $("#QuestionId").text());
            //为FormData对象添加数据
            $.each(ImageAddress[0].files, function (i, file) {
                data.append('upload_file', file);
            });
            var file = ImageAddress[0].files[0];//将上传的文件； 
            var filelength = parseInt(file.size) / 1024;//得到文件的大小  单位：KB
            var filename = file.name;//文件名
            if (new RegExp("^\\w+\\.(jpg|gif|bmp|png|jpeg)$", "i").test(filename)) {
                if (filelength <= 1024) {
                    if (confirm("确定更换图像？")) {
                        $.ajax({
                            url: "../ashx/UpLoadImage.ashx",
                            type: "POST",
                            data: data,
                            cache: false,
                            contentType: false,    //不可缺
                            processData: false,    //不可缺
                            success: UpLoadImage_Success
                        });
                    }
                } else {
                    alert("请确保上传图像不超过1M");
                }
            } else {
                alert("请上传以‘jpg|gif|bmp|png|jpeg’为后缀名的图片文件。");
            }
        }
    });

    //---------------------------------页面载入时给各个Select添加选择项 初始化各选择项------------------------------
    //初始难度系数 text=DifficultyRatio_DifficultyDescrip,value=DifficultyId（一个表Difficulty）
    //作者：利用(Users)Session["User"].UserName
    //试题类型：text=PaperCode_ChineseName_SubjectName() value=papercodeid(两个表 papercodes和subject)
    Uajax({}, "../ashx/initAddQuestionSelect.ashx", initAddQuestionSelect_success, initAddQuestionSelect_error, new Array(UserName, DifficultyId, PaperCodeId));

    //---------------------------------------------第二次加载  根据用户的选择给各个Select动态加载选择项-------------------------------------------------
    //--------------------试题类型改变  添加参考教材选择项----------------------
    //参考教材：根据选择的Papercodeid异步加载textbook,
    PaperCodeId.on({
        "change": function () {
            var papercodeid = $(this).val();//获取选择项的value
            Uajax({ PaperCodeId: papercodeid }, "../ashx/GetTextBookByPaperCodeId.ashx", GetTextBookByPaperCodeId_success, Error, TextBookId);
        }
    });
    //--------------------参考教材改变的动作  添加参考章节选择项----------------------
    //根据选择的textbook 异步加载 chapter
    TextBookId.on({
        "change": function () {
            var textbookid = $(this).val();//获取用户选择的Textbookid
            Uajax({ TextBookId: textbookid }, "../ashx/getLowChapterByTestBookId.ashx", GetChapterByTextBookId_success, Error, ChapterId);
        }
    });
    //--------------------是否真题的改变动作   添加考区、考试期数和试题号码----------------------
    //是否真题：根据选择异步加载 考区 期数 真题题号 显示考试时间
    PastExamPaperId.on({
        "change": pastexampaperchange
    })
    //--------------------绑定新增按钮的动作----------------------
    updata.on({
        "mouseover": function () { $(this).css({ "border": "1px solid red", "background-color": "#FF9924" }) },
        "mouseout": function () { $(this).css({ "border": "none", "background-color": "#DDD" }) },
        "click": UpdataQuestion
    })
});

//---------------------------------自定义函数区----------------------------------------------------------------
//是否真题的选择项改变
function pastexampaperchange() {
    var pastexampaperid = PastExamPaperId.val();
    if (pastexampaperid > 0) {//是真题，则添加三个录入信息：考区、期数和题号           
        var examzone = "<select id='ExamZone' class='newselect'><option selected='selected' value='-1'>请选择考区</option></select>";
        var pastexamPpaperperiodno = "<select class='newselect' id='PastExamPaperPeriodNo'><option selected='selected' value='-1'>请选择考试期数</option></select>";
        var pastexamquestionid = "<input type='text' class='newselect' name='PastExamQuestionId' id='PastExamQuestionId' value='请填写题号'/>";
        PastExamRow.append(examzone, pastexamPpaperperiodno, pastexamquestionid);
        var ExamZone = $("#ExamZone");
        var PastExamPaperPeriodNo = $("#PastExamPaperPeriodNo");
        var PastExamQuestionId = $("#PastExamQuestionId");
        //根据考区和试卷代码添加考试期数
        $(ExamZone).on({
            "change": function () {
                var examzoneid = $(this).val();
                var papercodeid = $(PaperCodeId).val();
                Uajax({ ExamZoneId: examzoneid, PaperCodeId: papercodeid }, "../ashx/GetPastExamPaperPeriodNoByExamZoneId.ashx", GetPastExamPaperPeriodNoByExamZoneId_success, Error, PastExamPaperPeriodNo);
            }
        });
        PastExamQuestionId.on({
            "focus": function () {
                $(this).val("");
            },
            "blur": function () {
                if ($(this).val().toString().trim() == "")
                    $(this).val("请填写题号");
            }
        })
        Uajax({}, "../ashx/GetAllExamZone.ashx", GetAllExamZone_success, Error, ExamZone);
    } else {//不是真题，判断是否有，如果有则应该删除：考区、期数和题号
        if ($("#ExamZone").length > 0)
            $("#ExamZone").remove();
        if ($("#PastExamPaperPeriodNo").length > 0)
            $("#PastExamPaperPeriodNo").remove();
        if ($("#PastExamQuestionId").length > 0)
            $("#PastExamQuestionId").remove();
    }
}

//新增按钮点击动作对应的新增试题方法
function UpdataQuestion() {
    //QuestionId int identity(1,1) primary key,-----试题ID
    //QuestionTitle text not null,-----题目
    //AnswerA text  not null,-----选项A
    //AnswerB text  not null,-----选项B
    //AnswerC text  not null,-----选项C
    //AnswerD text  not null,-----选项D
    //CorrectAnswer int  not null check(CorrectAnswer in(1,2,3,4)),-----参考答案
    //ImageAddress nvarchar(100) null,-----图形名称
    //DifficultyId int not null references Difficulty(DifficultyId),-----难度系数
    //UserId int not null references Users(UserId),-----上传人ID
    //UpLoadTime datetime not null default getdate(),-----上传时间
    //VerifyTimes int not null default 0,-----被审核次数（三次以上有效）
    //IsVerified bit not null default 0,-----是否审核通过0为不通过，1为通过,只有审核通过，才将试题更新到审核后的状态，否则不更新
    //IsDelte bit not null default 0,-----软删除标记
    //IsSupported int not null default 0,-----被赞次数
    //IsDeSupported int not null default 0,-----被踩次数
    //PaperCodeId int not null references PaperCodes(PaperCodeId),-----试题所对应的试卷代码
    //TextBookId int null references TextBook(TextBookId),-----试题对应的教材
    //ChapterId int null references Chapter(ChapterId),-----试题所对应的章节
    //PastExamPaperId int null references PastExamPaper(PastExamPaperId),-----试题是否是历年真题
    //PastExamQuestionId int null check(PastExamQuestionId>0 and PastExamQuestionId<=100) -----如果是真题，则在真题中的题号
    var regexp = new RegExp("\\(\\s*\\)");
    var Question = new Object();
    Question.QuestionId = $("#QuestionId").text();
    Question.QuestionTitle = $("#QuestionTitle").val();
    Question.AnswerA = $("#AnswerA").val();
    Question.AnswerB = $("#AnswerB").val();
    Question.AnswerC = $("#AnswerC").val();
    Question.AnswerD = $("#AnswerD").val();
    Question.CorrectAnswer = CorrectAnswer.val();
    Question.DifficultyId = DifficultyId.val();
    Question.PaperCodeId = PaperCodeId.val();
    Question.TextBookId = TextBookId.val();
    Question.ChapterId = ChapterId.val();
    Question.PastExamPaperId = PastExamPaperId.val();
    if (Question.PastExamPaperId > 0 && $("#ExamZone").length > 0 && $("#PastExamPaperPeriodNo").length > 0 && $("#PastExamQuestionId").length > 0) {
        Question.ExamZone = $("#ExamZone").val();
        Question.PastExamPaperPeriodNo = $("#PastExamPaperPeriodNo").val();
        Question.PastExamQuestionId = $("#PastExamQuestionId").val();
    }

    var data = JSON.stringify(Question);
    if (regexp.test(Question.QuestionTitle) &&
        Question.QuestionId > 0 &&
        Question.AnswerA != "" &&
        Question.AnswerA != null &&
        Question.AnswerB != "" &&
        Question.AnswerB != null &&
        Question.AnswerC != "" &&
        Question.AnswerC != null &&
        Question.AnswerD != "" &&
        Question.AnswerD != null &&
        (Question.CorrectAnswer == 1 || Question.CorrectAnswer == 2 || Question.CorrectAnswer == 3 || Question.CorrectAnswer == 4) &&
        Question.DifficultyId > 0 &&
        Question.PaperCodeId > 0
        ) {
        Uajax({ data: data }, "../ashx/UpdataQuestion.ashx", UpdataQuestion_success, UpdataQuestion_error, $(this));
    } else {
        alert("1、答案区域请用英文输入'()'一对小括号占位;\r\n2、各选项和参考答案不能为空；\r\n3、难度系数和试题类型不能为空；");
    }
};

//上传试题图像成功  返回的data是文件保存时所使用的新名称
function UpLoadImage_Success(data) {
    if (data != null && data != "false" && data != "") {
        ImageNewFileName = data;
        $("#oldImageAddress").attr({ alt: ImageNewFileName, src: "../QuestionImages/" + ImageNewFileName });
    } else {
        alert("图片更新失败。");
    }
}

//获取数据成功后，用于填充页面
//data:是一个包含三个值的数组，1、作者（UserName）string;2、难度系数 List<Difficulty> diffucluty；3、试题类型（包括科目）List<Dictionary<string, object>> papercode
//obj:也是一个包含三个值的数组，分别表示页面上的三个控件，分别是： UserName,DifficultyId, PaperCodeId
function initAddQuestionSelect_success(data, obj) {
    if (data.length == 3) {
        $(obj[0]).html(data[0]);//填充作者
        for (var i = 0; i < data[1].length; i++) {//填充难度系数
            $(obj[1]).append("<option value='" + data[1][i]["DifficultyId"] + "'>" + data[1][i]["DifficultyDescrip"] + "</option>");
        }
        for (var j = 0; j < data[2].length; j++) {//填充试题类型
            $(obj[2]).append("<option value='" + data[2][j]["PaperCodeId"] + "'>" + data[2][j]["ChineseName"] + " >>>>>> " + data[2][j]["SubjectName"] + "</option>");
        }
        //-------------------------------根据QuestionId获取Question值，然后填充各项值-----------------------------------
        Uajax({ QuestionId: $("#QuestionId").text() }, "../ashx/getUploadQuestionByQuestionId.ashx", getUploadQuestionByQuestionId_success, getUploadQuestionByQuestionId_error, "");
    }
}

//获取数据失败以后，用于向用户说明
function initAddQuestionSelect_error(obj) {
    alert("对不起，初始化数据失败");
    $(obj[0]).html("对不起，初始化数据失败，请联系管理员'yxshu@qq.com'");
    var option = "<option value='-2' selected='selected'>对不起，初始化数据失败，请联系管理员'yxshu@qq.com'</option>";
    for (var i = 1; i < obj.length; i++) {
        $(obj[i]).empty().append(option);
    }
}

//获取Question成功以后， 填充这个控件
function getUploadQuestionByQuestionId_success(data, obj) {
    if (data != null && data != false) {
        QuestionTitle.val(data["QuestionTitle"].replace(new RegExp("_{10}"), "()"));//试题题目
        $("#AnswerA").val(data["AnswerA"]).change();//选项A
        $("#AnswerB").val(data["AnswerB"]).change();//选项A
        $("#AnswerC").val(data["AnswerC"]).change();//选项A
        $("#AnswerD").val(data["AnswerD"]).change();//选项A
        if (data["ImageAddress"] == null) {
            data["ImageAddress"] = "Default.jpg";
        }
        $("#oldImageAddress").attr({ alt: data["ImageAddress"], src: "../QuestionImages/" + data["ImageAddress"] });;//试题图像 
        CorrectAnswer.val(data["CorrectAnswer"]);//设置参考答案
        DifficultyId.val(data["DifficultyId"]);
        PaperCodeId.val(data["PaperCodeId"]).change();
        if (data["TextBookId"] != null) {
            TextBookId.val(data["TextBookId"]).change();
            if (data["ChapterId"] != null) {
                ChapterId.val(data["ChapterId"]);
            }
        }
        if (data["PastExamPaperId"] != null && data["PastExamPaperId"] > 0) {
            PastExamPaperId.val(1).change();
            if ($("#ExamZone").length > 0 && data["ExamZone"] != null)
                $("#ExamZone").val(data["ExamZone"]).change();
            if ($("#PastExamPaperPeriodNo").length > 0 && data["PastExamPaperPeriodNo"] != null)
                $("#PastExamPaperPeriodNo").val(data["PastExamPaperPeriodNo"]);
            if ($("#PastExamQuestionId").length > 0 && data["PastExamQuestionId"] != null)
                $("#PastExamQuestionId").val(data["PastExamQuestionId"]);
        }
    }
}

//获取Question失败
function getUploadQuestionByQuestionId_error(obj) {
    console.log(obj);
}

//根据用户选择的PaperCodeId动态为textbook添加选择项 获取数据成功
function GetTextBookByPaperCodeId_success(data, obj) {
    obj.empty();
    obj.append("<option value='-1' selected='selected'>请选择参考教材……</option>");
    for (var i = 0; i < data.length; i++) {
        obj.append("<option value='" + data[i]["TextBookId"] + "'>" + data[i]["TextBookName"] + "</option>");
    }
}

//根据用户选择的TextBookId动态为Chapter添加选择项 获取数据成功
function GetChapterByTextBookId_success(data, obj) {
    obj.empty();
    obj.append("<option value='-1' selected='selected'>请选择参考章节……</option>");
    for (var i = 0; i < data.length; i++) {
        obj.append("<option value='" + data[i]["ChapterId"] + "'>" + data[i]["ChapterName"] + "</option>");
    }
}

//获取所有的考区信息成功
function GetAllExamZone_success(data, obj) {
    obj.empty();
    obj.append("<option value='-1' selected='selected'>请选择考区……</option>");
    for (var i = 0; i < data.length; i++) {
        obj.append("<option value='" + data[i]["ExamZoneId"] + "'>" + data[i]["ExamZoneName"] + "</option>");
    }
}

//根据ExamZoneId从表PastExamPaper获取真题考试期数 获取数据成功
function GetPastExamPaperPeriodNoByExamZoneId_success(data, obj) {
    obj.empty();
    obj.append("<option value='-1' selected='selected'>请选择考试期数……</option>");
    for (var i = 0; i < data.length; i++) {
        obj.append("<option value='" + data[i]["PastExamPaperPeriodNo"] + "'>第 " + data[i]["PastExamPaperPeriodNo"] + " 期</option>");
    }
}

//更新试题成功
function UpdataQuestion_success(data, obj) {
    if (parseInt(data) > 0) {
        alert("编辑试题成功。");
        window.location.href = "ShowQuestion.aspx?questionid=" + parseInt(data);
    } else {
        alert("修改试题失败。\r\n" + data);
    }
}

//更新试题失败
function UpdataQuestion_error(obj) {
    alert("新增试题失败。");
}

//所有select动态添加数据失败
function Error(obj) {
    obj.empty();
    obj.append("<option value='-1' selected='selected'>初始化失败……</option>");
    alert("对不起，初始化数据失败，请联系管理员'yxshu@qq.com");
}