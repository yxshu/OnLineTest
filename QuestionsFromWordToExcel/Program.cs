using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLineTest.Model;
using System.IO;
using NPOI.XWPF.UserModel;
using System.Threading;
using System.Text.RegularExpressions;
using OnLineTest.BLL;
using log4net;

namespace QuestionsFromWordToExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger(typeof(Question));
            using (FileStream stream = File.OpenRead("d://000.docx"))
            {
                XWPFDocument docx = new XWPFDocument(stream);
                bool star = false;
                bool end = false;
                Question question = null;
                QuestionManager questionmanager = new QuestionManager();
                foreach (var para in docx.Paragraphs)
                {
                    Regex regA = new Regex("^[ABCDabcd]{1}.|、", RegexOptions.IgnoreCase);//以A|B|C|D开头  选项
                    Regex regNO = new Regex("^[0-9]+.|、", RegexOptions.IgnoreCase);//以数字开头  题干
                    Regex regChapter = new Regex("^第[0-9]{1,2}章", RegexOptions.IgnoreCase);//章节标题
                    string text = para.ParagraphText.Trim();
                    if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrEmpty(text))
                    {
                        if (regNO.IsMatch(text))//题干 
                        {
                            question = new Question();
                            star = true;
                            question.QuestionTitle = regNO.Replace(text, "").Trim();
                        }
                        else if (regA.IsMatch(text))//选项
                        {
                            string[] strs = Regex.Split(text, "\\s{4,}");
                            foreach (var str in strs)
                            {
                                string answer = regA.Replace(str, "").Trim();
                                switch (regA.Match(str).ToString().Trim().Substring(0, 1).ToUpper())
                                {
                                    case "A":
                                        question.AnswerA = answer;
                                        break;
                                    case "B":
                                        question.AnswerB = answer;
                                        break;
                                    case "C":
                                        question.AnswerC = answer;
                                        break;
                                    case "D":

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
                                        question.AnswerD = answer;
                                        question.UserId = 1;
                                        question.PaperCodeId = 5;
                                        question.DifficultyId = 1;
                                        question.CorrectAnswer = 1;
                                        end = true;
                                        star = false;                                        
                                        int sn = questionmanager.Add(question);
                                        logger.Info("第 " + sn + " 题插入正常。");
                                        Console.WriteLine("第 " + sn + " 题插入正常。");
                                        break;
                                    default: break;
                                }
                            }
                        }
                        else if (regChapter.IsMatch(text))//章节标题
                        {
                        }
                    }
                    System.Threading.Thread.Sleep(300);
                }
            }

        }
    }
}
