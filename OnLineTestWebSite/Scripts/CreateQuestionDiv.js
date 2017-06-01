//根据Question生成一个展示Question的Div
//使用此方法，请引入Scripts/common.js
//使用此方法，请添加CSS/CreateQuestionDiv.CSS样式
//一个被实例化的完整的question   json对象
//返回值：DIV
function CreateQuestionDiv(data) {
    var QuestionDiv = $("<div id='QuestionDiv'></div>");//生成一个存放QUESTION的DIV
    if (data != null && data != undefined) {
        var question = { "AnswerA": data["AnswerA"], "AnswerB": data["AnswerB"], "AnswerC": data["AnswerC"], "AnswerD": data["AnswerD"], "CorrectAnswer": data["CorrectAnswer"] };
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
        //PastExamQuestionId int null check(PastExamQuestionId>0 and PastExamQuestionId<=160) -----如果是真题，则在真题中的题号

        //每一行形如：
        //<div id="QuestionId" class="row">
        //<label class="title">试题编号:</label>
        //<label class="content">第220题</label>
        //</div>

        //试题编号行
        var QuestionId = $("<div id='QuestionId' class='row'></div>");//116
        var QuestionId_title = $("<label class='title'>试题编号:</label>");
        var QuestionId_content = $("<label class='content'>第 <label id='id'>" + data["QuestionId"] + "</label> 题</label>");
        QuestionId.append(QuestionId_title, QuestionId_content);

        //试题题目行
        var QuestionTitle = $("<div id='QuestionTitle' class='row'></div>");//在磁罗经罗经柜内左右两边水平纵向放置的磁棒为__________校正器，用于校正罗经的__________。
        var QuestionTitle_title = $("<label class='title'>试题题干:</label>");
        var QuestionTitle_content = $("<label class='content'>" + data["QuestionTitle"] + "</label>");
        QuestionTitle.append(QuestionTitle_title, QuestionTitle_content);

        //选项A
        var AnswerA = $("<div id='AnswerA' class='row'></div>");//硬铁，半圆自差力
        var AnswerA_title = $("<label class='title'>选项A:</label>");
        var AnswerA_content = $("<label class='content'>A、" + data["AnswerA"] + "</label>");
        AnswerA.append(AnswerA_title, AnswerA_content);

        //选项B
        var AnswerB = $("<div id='AnswerB' class='row'></div>");//硬铁，象限自差力
        var AnswerB_title = $("<label class='title'>选项B:</label>");
        var AnswerB_content = $("<label class='content'>B、" + data["AnswerB"] + "</label>");
        AnswerB.append(AnswerB_title, AnswerB_content);

        //选项C
        var AnswerC = $("<div id='AnswerC' class='row'></div>");//软铁，半圆自差力
        var AnswerC_title = $("<label class='title'>选项C:</label>");
        var AnswerC_content = $("<label class='content'>C、" + data["AnswerC"] + "</label>");
        AnswerC.append(AnswerC_title, AnswerC_content);

        //选项D
        var AnswerD = $("<div id='AnswerD' class='row'></div>");//软铁，象限自差力
        var AnswerD_title = $("<label class='title'>选项D:</label>");
        var AnswerD_content = $("<label class='content'>D、" + data["AnswerD"] + "</label>");
        AnswerD.append(AnswerD_title, AnswerD_content);

        //参考答案
        var CorrectAnswer = $("<div id='CorrectAnswer' class='row'></div>");//1
        var CorrectAnswer_title = $("<label class='title'>参考答案:</label>");
        var CorrectAnswer_content = $("<label class='content'>" + InitQuestionAnswer(question) + "</label>");
        CorrectAnswer.append(CorrectAnswer_title, CorrectAnswer_content);

        //将前半部分添加到DIV中
        QuestionDiv.append( QuestionId, QuestionTitle, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer);

        //试题图像
        if (data["ImageAddress"] != null && data["ImageAddress"] != undefined) {
            var ImageAddress = $("<div id='ImageAddress' class='row'></div>");//null
            var ImageAddress_title = $("<label class='title'>试题参考图:</label>");
            var ImageAddress_content = $("<label class='content'><img id='oldimageaddress' alt='试题图像' src='../QuestionImages/" + data["ImageAddress"] + "' /></label>");
            ImageAddress.append(ImageAddress_title, ImageAddress_content);
            QuestionDiv.append(ImageAddress);
        }


        //出题时间
        var UpLoadTime = $("<div id='UpLoadTime' class='row'></div>");///Date(1436698984863)/
        var UpLoadTime_title = $("<label class='title'>出题时间:</label>");
        var UpLoadTime_content = $("<label class='content'>" + JsonToDate(data["UpLoadTime"]).Format("yyyy-MM-dd hh:mm:ss.S") + "</label>");
        UpLoadTime.append(UpLoadTime_title, UpLoadTime_content);

        //审核状态
        var IsVerified = $("<div id='IsVerified' class='row'></div>");//true
        var IsVerified_title = $("<label class='title'>审核状态:</label>");
        var IsVerified_content;
        if (data["IsVerified"] == 1) {
            IsVerified_content = $("<label class='content'> 审核通过</label>");
        } else {
            IsVerified_content = $("<label class='content'> 审核中…… </label>");
        }
        IsVerified.append(IsVerified_title, IsVerified_content);

        //审核次数
        var VerifyTimes = $("<div id='VerifyTimes' class='row'></div>");//0
        var VerifyTimes_title = $("<label class='title'>审核次数:</label>");
        var VerifyTimes_content = $("<label class='content'>" + data["VerifyTimes"] + "</label>");
        VerifyTimes.append(VerifyTimes_title, VerifyTimes_content);

        //点赞次数
        var IsSupported = $("<div id='IsSupported' class='row'></div>");//515
        var IsSupported_title = $("<label class='title'>被点赞次数:</label>");
        var IsSupported_content = $("<label class='content'>" + data["IsSupported"] + "</label>");
        IsSupported.append(IsSupported_title, IsSupported_content);

        //被踩次数
        var IsDeSupported = $("<div id='IsDeSupported' class='row'></div>");//515
        var IsDeSupported_title = $("<label class='title'>被吐槽次数:</label>");
        var IsDeSupported_content = $("<label class='content'>" + data["IsDeSupported"] + "</label>");
        IsDeSupported.append(IsDeSupported_title, IsDeSupported_content);


        //作者
        var UserName = $("<div id='UserName' class='row'></div>");//yxshu
        var UserName_title = $("<label class='title'>作者:</label>");
        var UserName_content = $("<label class='content'>" + data["UserName"] + "</label>");
        UserName.append(UserName_title, UserName_content);

        //难度系数
        var DifficultyDescrip = $("<div id='DifficultyDescrip' class='row'></div>");//容易
        var DifficultyDescrip_title = $("<label class='title'>难易程度:</label>");
        var DifficultyDescrip_content = $("<label class='content'>" + data["DifficultyDescrip"] + "</label>");
        DifficultyDescrip.append(DifficultyDescrip_title, DifficultyDescrip_content);

        //试题类型
        var ChineseName = $("<div id='ChineseName' class='row'></div>");//3000总吨及以上船舶二/三副
        var ChineseName_title = $("<label class='title'>试题类型:</label>");
        var ChineseName_content = $("<label class='content'>" + data["ChineseName"] + "</label>");
        ChineseName.append(ChineseName_title, ChineseName_content);

        //科目
        var SubjectName = $("<div id='SubjectName' class='row'></div>");//船舶结构与货运
        var SubjectName_title = $("<label class='title'>试题所属科目:</label>");
        var SubjectName_content = $("<label class='content'>" + data["SubjectName"] + "</label>");
        SubjectName.append(SubjectName_title, SubjectName_content);

        //参考教材
        var TextBookName = $("<div id='TextBookName' class='row'></div>");//null
        var TextBookName_title = $("<label class='title'>试题所属教材:</label>");
        var TextBookName_content = $("<label class='content'>" + data["TextBookName"] + "</label>");
        TextBookName.append(TextBookName_title, TextBookName_content);

        //参考章节
        var ChapterName = $("<div id='ChapterName' class='row'></div>");//null
        var ChapterName_title = $("<label class='title'>试题所属章节:</label>");
        var ChapterName_content = $("<label class='content'>" + data["ChapterName"] + "</label>");
        ChapterName.append(ChapterName_title, ChapterName_content);

        //是否真题
        var PastExamPaperId = $("<div id='PastExamPaperId' class='row'></div>");//null
        var PastExamPaperId_title = $("<label class='title'>是否真题:</label>");
        var PastExamPaperId_content;
        if (data["PastExamPaperId"] != null && data["PastExamPaperId"] > 0) {
            PastExamPaperId_content = $("<label class='content'>是：考区："+data["ExamZoneName"]+"；第 "+data["PastExamPaperPeriodNo"]+" 期；第 "+ data["PastExamQuestionId"] +" 题</label>");
            //PastExamPaperId_content = $("<label class='content'>是</label>");
            //var ExamZoneName = $("<label class='title'>" + data["ExamZoneName"] + " 考区 </label>");
            //var PastExamPaperPeriodNo = $("<label class='title'> 第 " + data["PastExamPaperPeriodNo"] + " 期 </label>");
            //var PastExamQuestionId = $("<label class='title'> 第 " + data["PastExamQuestionId"] + " 题 </label>");
            //PastExamPaperId_content.after(ExamZoneName, PastExamPaperId, PastExamQuestionId);
        } else {
            PastExamPaperId_content = $("<label class='content'>否</label>");
        }
        PastExamPaperId.append(PastExamPaperId_title, PastExamPaperId_content);

        QuestionDiv.append(UpLoadTime, IsVerified, VerifyTimes, IsSupported, IsDeSupported, UserName, DifficultyDescrip, ChineseName, SubjectName, TextBookName, ChapterName, PastExamPaperId);



    } else {//没有取得数据
        QuestionDiv.append("<div class='row'>没有试题数据</div>");
    }
    return QuestionDiv;
}
