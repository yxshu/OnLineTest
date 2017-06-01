<%@ WebHandler Language="C#" Class="UpdataQuestion" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Web.SessionState;

//更新试题，如果成功则返回QuestionId，否则返回不成功的原因
public class UpdataQuestion : IHttpHandler, IReadOnlySessionState
{

    public void ProcessRequest(HttpContext context)
    {
        StringBuilder result = new StringBuilder();//最终返回给用户的信息
        JavaScriptSerializer serializer = new JavaScriptSerializer(); //序列化
        Question question = new Question();//下面主要的工作就是实例化这个对象
        bool DataFormatSuccess = true;//数据格式是否正确的标志，只要有一个地方不对就会被标成false，则不会执行添加
        QuestionManager manager = new QuestionManager();
        Dictionary<string, object> dic = serializer.Deserialize<Dictionary<string, object>>(context.Request.Form["data"]);//将客户端送来的数据保存在一个Dictionary中，为什么不直接存在一个Question中呢？因为其中有两个数据不是对象的属性，包括：ExamZone，PastExamPaperPeriodNo，但又要求根据这两个值和另外的一个试卷类型查询PastExamPaperId

        //这是Question对象对于数据格式的要求
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

        Regex regex_title = new Regex("\\(\\s*\\)", RegexOptions.IgnoreCase);//试题标题的正则表达式，要求包含一对小括号，用于标志答案位置
        Regex regex_imageaddress = new Regex("^\\w+\\.(jpg|gif|bmp|png)$", RegexOptions.IgnoreCase);//试题图像的文件的正则表达式，要求是图片文件格式

        try
        {
            int QuestionId = -1;
            if (dic.ContainsKey("QuestionId") && int.TryParse(dic["QuestionId"].ToString(), out QuestionId) && manager.Exists(QuestionId))
            {
                question.QuestionId = QuestionId;
                question = manager.GetModel(question.QuestionId);
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("试题编号错误。");
            }
        }
        catch (Exception ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("试题编号错误" + ex.Message);
        }
        //处理试题题目
        try
        {
            if (dic.ContainsKey("QuestionTitle") && !string.IsNullOrEmpty(dic["QuestionTitle"].ToString()) && regex_title.IsMatch(dic["QuestionTitle"].ToString()))
            {
                //question.QuestionTitle = Regex.Replace(dic["QuestionTitle"].ToString(), "(\\(\\s*\\))", "__________", RegexOptions.IgnoreCase);
                question.QuestionTitle = regex_title.Replace(dic["QuestionTitle"].ToString(), "__________");
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("试题题目数据格式错误。");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的数据中不包含试题题目" + ex.Message);
        }

        //处理试题选项A
        try
        {
            if (dic.ContainsKey("AnswerA") && !string.IsNullOrEmpty(dic["AnswerA"].ToString()))
            {
                question.AnswerA = dic["AnswerA"].ToString();
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("选项A数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含选择A" + ex.Message);
        }

        //处理试题选项B
        try
        {
            if (dic.ContainsKey("AnswerB") && !string.IsNullOrEmpty(dic["AnswerB"].ToString()))
            {
                question.AnswerB = dic["AnswerB"].ToString();
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("选项B数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含选择B" + ex.Message);
        }

        //处理试题选项C
        try
        {
            if (dic.ContainsKey("AnswerC") && !string.IsNullOrEmpty(dic["AnswerC"].ToString()))
            {
                question.AnswerC = dic["AnswerC"].ToString();
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("选项C数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含选择C" + ex.Message);
        }

        //处理试题选项D
        try
        {
            if (dic.ContainsKey("AnswerD") && !string.IsNullOrEmpty(dic["AnswerD"].ToString()))
            {
                question.AnswerD = dic["AnswerD"].ToString();
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("选项D数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含选择D" + ex.Message);
        }

        //处理试题参考答案
        try
        {
            Int32 answer = -1;
            if (dic.ContainsKey("CorrectAnswer") && Int32.TryParse(dic["CorrectAnswer"].ToString(), out answer) && (answer == 1 || answer == 2 || answer == 3 || answer == 4))
            {
                question.CorrectAnswer = answer;
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("参考答案数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含参考答案" + ex.Message);
        }

        //试题图像不在此处更新，在页面采用异步更新
        ////处理试题图像,试题图像部分仅仅保存文件名称，不保存路径
        //try
        //{

        //    if (dic.ContainsKey("ImageAddress"))
        //    {
        //        if (!string.IsNullOrEmpty(dic["ImageAddress"].ToString()))//没有图像也是合法的
        //        {
        //            if (regex_imageaddress.IsMatch(Path.GetFileName(dic["ImageAddress"].ToString())) && dic["ImageAddress"].ToString().Length < 100)//文件长度不超过100
        //            {
        //                question.ImageAddress = Path.GetFileName(dic["ImageAddress"].ToString());
        //            }
        //            else
        //            {
        //                DataFormatSuccess = false;
        //                result.AppendLine("试题图像数据格式错误");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        DataFormatSuccess = false;
        //        result.AppendLine("上传的试题中不包含试题图像数据");
        //    }
        //}
        //catch (KeyNotFoundException ex)
        //{
        //    DataFormatSuccess = false;
        //    result.AppendLine("上传的试题中不包含试题图像数据");
        //}

        //处理试题难度系数
        try
        {
            Int32 DifficultyId = -1;
            if (dic.ContainsKey("DifficultyId") && Int32.TryParse(dic["DifficultyId"].ToString(), out DifficultyId))
            {
                if (new DifficultyManager().Exists(DifficultyId))
                {
                    question.DifficultyId = DifficultyId;
                }
                else
                {
                    DataFormatSuccess = false;
                    result.AppendLine("系统中不存在你选择的难度系数");
                }
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("难度系数数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含难度系数数据" + ex.Message);
        }

        //处理试题类型papercode
        Int32 PaperCodeId = -1;
        try
        {
            if (dic.ContainsKey("PaperCodeId") && Int32.TryParse(dic["PaperCodeId"].ToString(), out PaperCodeId))
            {
                if (new PaperCodesManager().Exists(PaperCodeId))
                {
                    question.PaperCodeId = PaperCodeId;
                }
                else
                {
                    DataFormatSuccess = false;
                    result.AppendLine("系统中不存在你选择的试题类型");
                }
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("试题类型数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含试题类型数据" + ex.Message);
        }

        //处理参考教材TextBookId,可以为null
        Int32 TextBookId = -1;
        try
        {
            if (dic.ContainsKey("TextBookId") && Int32.TryParse(dic["TextBookId"].ToString(), out TextBookId))
            {
                if (TextBookId > 0)//默认值为-1
                {
                    if (new TextBookManager().Exists(TextBookId))
                    {
                        question.TextBookId = TextBookId;
                    }
                    else
                    {
                        DataFormatSuccess = false;
                        result.AppendLine("系统中不存在你选择的参考教材");
                    }
                }
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("参考教材数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含参考教材数据" + ex.Message);
        }

        //处理参考章节ChapterId
        if (TextBookId > 0 && TextBookId != null)
        {
            try
            {
                Int32 ChapterId = -1;
                if (dic.ContainsKey("ChapterId") && Int32.TryParse(dic["ChapterId"].ToString(), out ChapterId))
                {
                    if (ChapterId > 0)
                    {
                        if (new ChapterManager().Exists(ChapterId) && ((Chapter)new ChapterManager().GetModel(ChapterId)).TextBookId == TextBookId)
                        {
                            question.ChapterId = ChapterId;
                        }
                        else
                        {
                            DataFormatSuccess = false;
                            result.AppendLine("系统中不存在你选择的参考章节");
                        }
                    }
                }
                else
                {
                    DataFormatSuccess = false;
                    result.AppendLine("参考章节数据格式错误");
                }
            }
            catch (KeyNotFoundException ex)
            {
                DataFormatSuccess = false;
                result.AppendLine("上传的试题中不包含参考章节数据" + ex.Message);
            }
        }

        //处理是否是真题的标志PastExamPaperId（0：不是真题， 1：是真题）
        try
        {
            Int32 PastExamPaperId = -1;
            if (dic.ContainsKey("PastExamPaperId") && Int32.TryParse(dic["PastExamPaperId"].ToString(), out PastExamPaperId))
            {
                if (PastExamPaperId == 0)//不是真题
                {
                }
                else if (PastExamPaperId == 1)//真题
                {
                    try
                    {
                        int ExamZone = -1, PastExamPaperPeriodNo = -1;
                        if (
                            dic.ContainsKey("ExamZone") &&
                            dic.ContainsKey("PastExamPaperPeriodNo") &&
                            Int32.TryParse(dic["ExamZone"].ToString(), out ExamZone) &&
                            Int32.TryParse(dic["PastExamPaperPeriodNo"].ToString(), out PastExamPaperPeriodNo) &&
                            new ExamZoneManager().Exists(ExamZone) &&
                            new PastExamPaperManager().Exists(PaperCodeId, ExamZone, PastExamPaperPeriodNo)
                            )
                        {
                            question.PastExamPaperId = new PastExamPaperManager().GetModel(PaperCodeId, ExamZone, PastExamPaperPeriodNo).PastExamPaperId;
                            //后面开始处理真题ID的问题
                            int PastExamQuestionId = -1;
                            if (
                                dic.ContainsKey("PastExamQuestionId") &&
                                Int32.TryParse(dic["PastExamQuestionId"].ToString(), out PastExamQuestionId) &&
                                PastExamQuestionId > 0 &&
                                PastExamQuestionId < 160
                                )
                            {
                                question.PastExamQuestionId = PastExamQuestionId;
                            }
                            else
                            {
                                DataFormatSuccess = false;
                                result.AppendLine("真题题号没有填写或者是格式错误（1-160的数字）");
                            }
                        }
                        else
                        {
                            DataFormatSuccess = false;
                            result.AppendLine("上传的试题中不包含考区和真题期数数据或者格式错误");
                        }
                    }
                    catch (KeyNotFoundException e)
                    {
                        DataFormatSuccess = false;
                        result.AppendLine("上传的试题中不包含考区和真题期数数据");
                    }
                }
                else
                { //格式错误
                    DataFormatSuccess = false;
                    result.AppendLine("真题数据格式错误");
                }
            }
            else
            {
                DataFormatSuccess = false;
                result.AppendLine("真题数据格式错误");
            }
        }
        catch (KeyNotFoundException ex)
        {
            DataFormatSuccess = false;
            result.AppendLine("上传的试题中不包含真题数据");
        }

        //自能修改自身提交的试题
        if (context.Session["User"] != null && question.UserId != ((Users)context.Session["User"]).UserId)
        {
            DataFormatSuccess = false;
            result.AppendLine("不具有处理此试题的权限");
        }

        if (DataFormatSuccess)
        {
            try
            {
                if (manager.Update(question))
                    result.AppendLine(question.QuestionId.ToString());
            }
            catch (Exception ex)
            {
                result.AppendLine("数据格式正确，但新增试题错误。" + ex.Message);
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(result.ToString()));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}