<%@ WebHandler Language="C#" Class="CreateSimulatePaper" %>

using System;
using System.Web;
using NPOI.SS.UserModel;
using OnLineTest.Model;
using OnLineTest.BLL;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Model;
using NPOI.HPSF;
using NPOI.SS.Util;
using System.Collections.Generic;
public class CreateSimulatePaper : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int SubjectId, PapercodeId, DifficultyId, TextbookId, QuestionCount;//定义接收数据的变量
        Dictionary<Chapter, int> result = new Dictionary<Chapter, int>();//保存最终比例分配结果
        if (!int.TryParse(context.Request.Form["subject"], out SubjectId)) return;//开始取值
        if (!int.TryParse(context.Request.Form["papercode"], out PapercodeId)) return;
        if (!int.TryParse(context.Request.Form["difficulty"], out DifficultyId)) return;
        if (!int.TryParse(context.Request.Form["textbook"], out TextbookId)) return;
        if (!int.TryParse(context.Request.Form["questioncount"], out QuestionCount)) QuestionCount = 100;
        TextBookManager textbookmanager = new TextBookManager();//定义
        PaperCodesManager papercodesmanager = new PaperCodesManager();
        SubjectManager subjectmanager = new SubjectManager();
        ChapterManager chaptermanager = new ChapterManager();
        if (!textbookmanager.Exists(TextbookId) || textbookmanager.GetModel(TextbookId).PaperCodeId != PapercodeId || papercodesmanager.GetModel(PapercodeId).SubjectId != SubjectId || !subjectmanager.Exists(SubjectId))//判断接收到的数据完整性
            return;
        List<Chapter> list = chaptermanager.GetModelList("TextBookId=" + TextbookId);//得到此教材对应的所有章节
        Dictionary<Chapter, int> dic = new Dictionary<Chapter, int>();//新建一个字典，用于保存每一大章节占试题问题的比例，字典内的结构是  chapter,ratio
        bool isTrue = true;//一个标记，标记取比例是否成功
        //后面部分开始取比例,即填充dic
        if (list.Count > 0)
        {
            foreach (Chapter c in list)
            {
                if (c.ChapterDeep == 0)
                {
                    int ratio;
                    if (int.TryParse(context.Request.Form[c.ChapterId.ToString()], out ratio))
                    {
                        dic.Add(c, ratio);
                    }
                    else
                    {
                        isTrue = false;
                        break;//其中只要有一个取值不成功，则放弃取值，采用默认比例
                    }

                }
            }
        }
        if (!isTrue)//取用户的比例不成功，则开始分配默认比例，将所有比例分配到底层目录，即该目录没有子目录，同时添加试题时，也应该将试题添加到底层目录
        {
            result = chaptermanager.getAllLowChapterAndDefaultCount(list, QuestionCount);
        }
        else//取用户的比例成功，但是取得的是每一大章节的比例，应该将其平均到此大章节的每一个底层目录
        {
            foreach (KeyValuePair<Chapter, int> kv in dic)
            {
                int value = kv.Value;
                List<Chapter> temp = new List<Chapter>();//存放底层目录的
                temp.AddRange(chaptermanager.getLowChapter(kv.Key, list));
                int averageCount = (int)Math.Floor((double)value / 100 * QuestionCount / temp.Count);//将前台的比例值，转化为试题的数量
                for (int i = 0; i < temp.Count; i++)
                {
                    result.Add(temp[i], averageCount);
                }
                // result.Add(temp[temp.Count - 1], QuestionCount - averageCount * (temp.Count - 1));
            }
        }
        QuestionManager manager = new QuestionManager();
        List<Question> QuestionCollection = manager.CreateSimulatePaper(TextbookId, result, DifficultyId);//提取试题，其中传递的参数包括：TextbookId(试卷适用的教材), dic（每章对应的试题数量）, DifficultyId（难度系数，会在+/-3之间）


        //以下部分的内容是将生成的试题填充到excel中并返回给用户
        //其中返回的试题包含 QuestionId,QuestionTitle,AnswerA,AnswerB,AnswerC,AnswerD,CorrectAnswer,ImageAddress,ChapterId,PastExamPaperId

        NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();//新建一个工作簿

        //下面为文档添加描述性内容
        DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        dsi.Company = "武汉交通职业学院";
        dsi.Manager = "航海学院航海技术教研室";
        SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        si.Subject = "船员在线考试系统";
        si.Author = "武汉交通职业学院航海学院余项树老师";
        si.Comments = "这是利用武汉交通职业学院航海学院航海教研室开发的船员在线考试系统生成的试卷";
        si.Title = "船员在线考试系统";
        book.DocumentSummaryInformation = dsi;
        book.SummaryInformation = si;

        NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("test_01");//添加一个表格
        sheet.Header.Center = "武汉交通职业学院";
        sheet.Footer.Right = "武汉交通职业学院航海学院";

        ICellStyle CellStyleTitle = book.CreateCellStyle();//内容的样式-居中
        ICellStyle CellStyleContentLeft = book.CreateCellStyle();//内容的样-左对齐
        ICellStyle CellStylecContentCenter = book.CreateCellStyle();//标题样式
        CellStyleTitle.Alignment = HorizontalAlignment.Center;//设置水平居中
        CellStyleContentLeft.Alignment = HorizontalAlignment.Left;
        CellStylecContentCenter.Alignment = HorizontalAlignment.Center;
        CellStyleTitle.VerticalAlignment = VerticalAlignment.Center;//设置垂直居中
        CellStyleContentLeft.VerticalAlignment = VerticalAlignment.Center;
        CellStylecContentCenter.VerticalAlignment = VerticalAlignment.Center;
        CellStyleTitle.WrapText = true;//自动换行
        CellStyleContentLeft.WrapText = true;
        CellStylecContentCenter.WrapText = true;
        //CellStyleTitle设置边框
        CellStyleTitle.BorderTop = BorderStyle.Thick;//设置边框样式
        CellStyleTitle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;//边框颜色
        CellStyleTitle.BorderLeft = BorderStyle.Thin;
        CellStyleTitle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        CellStyleTitle.BorderRight = BorderStyle.Thin;
        CellStyleTitle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        //CellStyleTitle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Black.Index;//设置背景色
        IFont WhiteFont = book.CreateFont();//设置字体颜色
        WhiteFont.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
        WhiteFont.Boldweight = 20;
        CellStyleTitle.SetFont(WhiteFont);
        //CellStyleContentCenter设置边框
        CellStylecContentCenter.BorderTop = BorderStyle.Thin;
        CellStylecContentCenter.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        CellStylecContentCenter.BorderRight = BorderStyle.Thin;
        CellStylecContentCenter.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        CellStylecContentCenter.BorderLeft = BorderStyle.Thin;
        CellStylecContentCenter.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        CellStylecContentCenter.BorderBottom = BorderStyle.Thin;
        CellStylecContentCenter.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        //CellStyleContentLeft设置边框
        CellStyleContentLeft.BorderTop = BorderStyle.Thin;
        CellStyleContentLeft.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        CellStyleContentLeft.BorderRight = BorderStyle.Thin;
        CellStyleContentLeft.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        CellStyleContentLeft.BorderLeft = BorderStyle.Thin;
        CellStyleContentLeft.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        CellStyleContentLeft.BorderBottom = BorderStyle.Thin;
        CellStyleContentLeft.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        //设置表格每列的宽度
        sheet.SetColumnWidth(0, 8 * 256);
        sheet.SetColumnWidth(1, 10 * 256);
        sheet.SetColumnWidth(2, 100 * 256);
        sheet.SetColumnWidth(3, 20 * 256);
        sheet.SetColumnWidth(4, 20 * 256);
        sheet.SetColumnWidth(5, 20 * 256);
        sheet.SetColumnWidth(6, 20 * 256);
        sheet.SetColumnWidth(7, 10 * 256);
        sheet.SetColumnWidth(8, 25 * 256);
        sheet.SetColumnWidth(9, 10 * 256);
        sheet.SetColumnWidth(10, 10 * 256);


        //设置标题行
        string[] title = new string[] { "序号", "试题编号", "题目", "选项A", "选项B", "选项C", "选项D", "参考答案", "图片", "章节", "真题编号" };
        IRow TitleRow = sheet.CreateRow(0);
        TitleRow.HeightInPoints = 40;
        for (int i = 0; i < title.Length; i++)
        {
            ICell cell = TitleRow.CreateCell(i);
            cell.SetCellValue(title[i]);
            cell.CellStyle = CellStyleTitle;
        }

        //设置内容行
        int j = 1;
        foreach (Question q in QuestionCollection)
        {
            IRow QuestionRow = sheet.CreateRow(j);
            ICell cell0 = QuestionRow.CreateCell(0);
            cell0.SetCellValue(j);
            cell0.CellStyle = CellStylecContentCenter;
            ICell cell1 = QuestionRow.CreateCell(1);
            cell1.SetCellValue(q.QuestionId);
            cell1.CellStyle = CellStylecContentCenter;
            ICell cell2 = QuestionRow.CreateCell(2);
            cell2.CellStyle = CellStyleContentLeft;
            cell2.SetCellValue(q.QuestionTitle);
            ICell cell3 = QuestionRow.CreateCell(3);
            cell3.SetCellValue(q.AnswerA);
            cell3.CellStyle = CellStyleContentLeft;
            ICell cell4 = QuestionRow.CreateCell(4);
            cell4.SetCellValue(q.AnswerB);
            cell4.CellStyle = CellStyleContentLeft;
            ICell cell5 = QuestionRow.CreateCell(5);
            cell5.SetCellValue(q.AnswerC);
            cell5.CellStyle = CellStyleContentLeft;
            ICell cell6 = QuestionRow.CreateCell(6);
            cell6.SetCellValue(q.AnswerD);
            cell6.CellStyle = CellStyleContentLeft;
            ICell cell7 = QuestionRow.CreateCell(7);
            cell7.SetCellValue(q.CorrectAnswer);
            cell7.CellStyle = CellStylecContentCenter;
            ICell cell8 = QuestionRow.CreateCell(8);
            cell8.SetCellValue(q.ImageAddress);
            cell8.CellStyle = CellStylecContentCenter;
            ICell cell9 = QuestionRow.CreateCell(9);
            cell9.SetCellValue(q.ChapterId.ToString());
            cell9.CellStyle = CellStylecContentCenter;
            ICell cell10 = QuestionRow.CreateCell(10);
            cell10.SetCellValue(q.PastExamPaperId.ToString());
            cell10.CellStyle = CellStylecContentCenter;
            j++;
        }



        // 写入到客户端  
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        book.Write(ms);
        context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
        context.Response.BinaryWrite(ms.ToArray());
        book = null;
        ms.Close();
        ms.Dispose();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}