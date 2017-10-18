using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Configuration;
using System.Diagnostics;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NPOI.XWPF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Threading;

public partial class CreateQuestionData : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(CreateQuestionData));
    //索引地址
    string IndexPath = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        IndexPath = ConfigurationManager.AppSettings["CreateIndexDirectionPath"].ToString();
        TextBox2.Text = IndexPath;
        TextBox1.Text = Server.MapPath("~\\1.txt");
    }

    ///<summary>
    ///按钮事件，将一个文件中的数据导入到数据库并创建索引
    ///<param name="sender"></param>
    ///<param name="e"></param>
    ///</summary>
    protected void Button2_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string txtpath = TextBox1.Text;
        string[] s = sb.Append(File.ReadAllText(txtpath, Encoding.Default)).ToString().Split(new string[] { "<题目>" }, StringSplitOptions.RemoveEmptyEntries);
        int useridmax = new OnLineTest.BLL.UsersManager().GetMaxId();
        for (int i = 0; i < s.Length; i++)
        {
            s[i] = Regex.Replace(s[i].Replace("\\r", "").Replace("\n", "").Replace("?", "").Trim(), @"\s+", "").Replace("()", "__________");
            string[] s2 = s[i].Split(new string[] { "<参考答案>" }, StringSplitOptions.RemoveEmptyEntries);
            if (s2.Length == 2)
            {
                string[] s3 = s2[0].Split(new string[] { "A．", "B．", "C．", "D．" }, StringSplitOptions.RemoveEmptyEntries);
                if (s3.Length == 5)
                {

                    Question q = StringsTOQuestion(new string[] { s3[0], s3[1], s3[2], s3[3], s3[4] }, s2[1]);
                    QuestionManager qm = new QuestionManager();
                    common.CreateIndexofQuestion(new DirectoryInfo(IndexPath), qm.GetModel(qm.Add(q)));
                }
            }
        }
        common.ShowMessageBox(this.Page, "导入数据和创建索引成功。");
    }


    protected void createQuestion(object sender, EventArgs e)
    {
        ILog logger = LogManager.GetLogger(typeof(Question));
        using (FileStream stream = File.OpenRead("d://000/hhyq.docx"))
        {
            XWPFDocument docx = new XWPFDocument(stream);
            string[] strs = new string[7];
            Regex regA = new Regex("^[ABCDabcd]{1}.|、", RegexOptions.IgnoreCase);//以A|B|C|D开头  选项
            Regex regNO = new Regex("^[0-9]+.|、", RegexOptions.IgnoreCase);//以数字开头  题干
            Regex regChapter = new Regex("^第[一二三四五六七八九十]{1,3}章", RegexOptions.IgnoreCase);//章节标题
            Regex regnode = new Regex("^第[一二三四五六七八九十]{1,3}节", RegexOptions.IgnoreCase);
            Chapter chapter = null, node = null;
            ChapterManager chaptermanager = new ChapterManager();
            Question question = null;
            QuestionManager questonmanager = new QuestionManager();
            foreach (var para in docx.Paragraphs)
            {
                string text = para.ParagraphText.Trim();
                if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrEmpty(text))//非空
                {
                    if (regChapter.IsMatch(text))
                    {
                        chapter = new Chapter();
                        chapter.TextBookId = 1;
                        chapter.IsVerified = true;
                        chapter.ChapterDeep = 0;
                        chapter.ChapterName = regChapter.Replace(text, "").Trim();
                        chapter.ChapterParentNo = 0;
                        chapter.ChapterRemark = chapter.ChapterName;
                        chaptermanager = new ChapterManager();
                        chapter.ChapterId = chaptermanager.Add(chapter);
                    }
                    else if (regnode.IsMatch(text))
                    {
                        node = new Chapter();
                        node.ChapterName = regnode.Replace(text, "").Trim();
                        node.ChapterParentNo = chapter.ChapterId;
                        node.ChapterDeep = 1;
                        node.ChapterRemark = node.ChapterName;
                        node.TextBookId = 1;
                        node.IsVerified = true;
                        node.ChapterId = chaptermanager.Add(node);
                    }
                    else
                    {
                        string[] paratext = text.Split(new string[] { "A.", "B.", "C.", "D." }, StringSplitOptions.RemoveEmptyEntries);
                        question = StringsTOQuestion(paratext, "D");
                        question.QuestionId = questonmanager.Add(question);
                        Console.WriteLine(question.QuestionId + "  添加成功。");
                    }
                }
                System.Threading.Thread.Sleep(300);
            }
        }
    }

    /// <summary>
    /// 根据字符串生成试题
    /// </summary>
    /// <param name="str">试题字符串，如果一个值则为判断题，如果是四个值，则为三个选项的选择题，如果是五个值，则为正常的选项题</param>
    /// <param name="answer">答案</param>
    /// <returns>生成的试题</returns>
    public Question StringsTOQuestion(string[] str, string answer)
    {
        Question question = new Question();
        question.QuestionTitle = new Regex("^[0-9]+.|、", RegexOptions.IgnoreCase).Replace(str[0], "").Trim();
        if (str.Length == 5)
        {
            question.AnswerA = str[1];
            question.AnswerB = str[2];
            question.AnswerC = str[3];
            question.AnswerD = str[4];
        }
        else if (str.Length == 4)
        {
            question.AnswerA = str[1];
            question.AnswerB = str[2];
            question.AnswerC = str[3];
            question.AnswerD = "";
        }
        else if (str.Length == 1)
        {
            question.AnswerA = "";
            question.AnswerB = "";
            question.AnswerC = "";
            question.AnswerD = "";
        }
        else
        {
            logger.Info("异常");
        }

        question.CorrectAnswer = common.tryparse(answer);
        question.DifficultyId = new Random().Next(1, new DifficultyManager().GetMaxId());
        int userid;
        do
        {
            userid = new Random().Next(new UsersManager().GetMaxId());
        } while (!new OnLineTest.BLL.UsersManager().Exists(userid));
        question.UserId = userid;
        question.PaperCodeId = 14;//new Random().Next(1, new PaperCodesManager().GetMaxId());
        question.IsVerified = true;
        question.VerifyTimes = 3;
        question.IsDelte = false;
        question.IsSupported = 0;
        question.IsDeSupported = 0;
        string tihao = new Regex("^[0-9]+.|、", RegexOptions.IgnoreCase).Match(str[0]).ToString();
        question.ImageAddress = tihao.Substring(0, tihao.Length - 1);
        return question;
    }

    /// <summary>
    /// 将根目录下的Question Libraries下的每个EXCEL文件中的试题导入数据库并创建索引
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();//主要试题中的科目与数据库中textbookname的名称不一致，这里建立一个对应关系，key是试题中的名称，value是数据库中的名称
        string[] subjectResourse = {
                                       "航海学(航海仪器)", 
                                       "航海学(航海气象与海洋学)",
                                       "航海学(航海地文、天文)",
                                       "船舶管理", 
                                       "船舶结构与货运",
                                       "航海英语",
                                       "船舶操纵与避碰（船舶操纵）",
                                       "船舶操纵与避碰（船舶避碰）",
                                       "基本安全-个人安全与社会责任",
                                       "基本安全-个人求生",
                                       "基本安全-防火与灭火",
                                       "基本安全-基本急救",
                                       "精通救生艇筏和救助艇培训",
                                       "高级消防培训",
                                       "精通急救培训" 
                                   };
        string[] subjectData = {
                                   "航海学（航海地文、天文和仪器）", 
                                   "航海学（航海气象与海洋学）", 
                                   "航海学（航海地文、天文和仪器）",
                                   "船舶管理（驾驶员）", 
                                   "船舶结构与货运", 
                                   "航海英语（二、三副）", 
                                   "船舶操纵与避碰（船舶操纵）",
                                    "船舶操纵与避碰（船舶避碰）",
                                   "个人安全与社会责任", 
                                   "个人求生", 
                                   "防火与灭火", 
                                   "基本急救" ,
                                   "救生艇筏和救助艇操作及管理",
                                   "高级消防",
                                   "精通医疗急救"
                               };
        if (subjectData.Length != subjectResourse.Length) throw new ArgumentException("两个科目的长度不一致");
        for (int i = 0; i < subjectData.Length; i++)
        {
            dic.Add(subjectResourse[i], subjectData[i]);
        }
        Difficulty difficulty = new DifficultyManager().GetModel(1);//difficultyid为1  是难度不确定
        Users user = (Users)Session["User"];//得到当前登录的用户
        TextBook textbook = null;
        Chapter chapter = new Chapter();
        Chapter node = new Chapter();
        QuestionManager questionmanager = new QuestionManager();
        //获得获取根目录下Question Libraries文件中的试题文件
        string[] QuestionLibraries = Directory.GetFiles(Server.MapPath("~\\Question Libraries"));
        foreach (String path in QuestionLibraries)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = null;//获得EXCEL文档
            if (path.IndexOf(".xlsx") > 0) // 2007版本
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (path.IndexOf(".xls") > 0) // 2003版本
            {
                workbook = new HSSFWorkbook(fs);
            }
            ISheet sheet = workbook.GetSheetAt(0);//获得文档中的第一个工作表
            IRow firstrow = sheet.GetRow(0);//获得工作表中的第一行
            int column = firstrow.LastCellNum;//获得第一行中的列数
            int rownum = sheet.LastRowNum;
            for (int i = 1; i < rownum; i++)
            {
                Question question = new Question();
                IRow row = sheet.GetRow(i);
                //表格中的数据是从0列开始，分别是0AllID/1Id/2SN/3SNID/4Subject/5Chapter/6Node/7Title/8Choosea/9Chooseb/10Choosec/11Choosed/12Answer/13Explain/14ImageAddress/15Remark
                question.QuestionTitle = row.GetCell(7).StringCellValue.Trim();
                question.AnswerA = row.GetCell(8).StringCellValue.Trim() + "";
                question.AnswerB = row.GetCell(9).StringCellValue.Trim() + "";
                question.AnswerC = row.GetCell(10).StringCellValue.Trim() + "";
                question.AnswerD = row.GetCell(11).StringCellValue.Trim() + "";
                question.CorrectAnswer = common.tryparse(row.GetCell(12).StringCellValue.Trim());
                question.Explain = row.GetCell(13).StringCellValue.Trim();
                question.ImageAddress = row.GetCell(14).StringCellValue.Trim();
                question.DifficultyId = difficulty.DifficultyId;
                question.UserId = user.UserId;
                string subjectFromExcel = row.GetCell(4).StringCellValue.Trim();
                string subjectFromDb = dic[subjectFromExcel].Trim();
                if (textbook == null || textbook.TextBookName != subjectFromDb)
                {
                    textbook = new TextBookManager().GetModel(subjectFromDb)[0];
                }
                question.TextBookId = textbook.TextBookId;
                question.PaperCodeId = textbook.PaperCodeId;
                //后面开始处理章节标题，先检测章标题，后检测节标题
                //如果章标题不存在，进添加进去，如果节标题不存在，也添加进去
                string chapterFromExcel = row.GetCell(5).StringCellValue.Trim();
                string nodeFromExcel = row.GetCell(6).StringCellValue.Trim();
                //章标题为空，或者没有，则要新建
                if (chapter.TextBookId != textbook.TextBookId || chapter.ChapterName != chapterFromExcel)
                {
                    ChapterManager chaptermanager = new ChapterManager();
                    List<Chapter> list = chaptermanager.GetModel(textbook.TextBookId, chapterFromExcel);
                    if (list.Count > 0)
                    {
                        chapter = list[0];
                    }
                    else
                    {
                        chapter = new Chapter();
                        chapter.TextBookId = textbook.TextBookId;
                        chapter.ChapterName = chapterFromExcel;
                        chapter.ChapterParentNo = 0;
                        chapter.ChapterDeep = 0;
                        chapter.ChapterRemark = chapterFromExcel;
                        int resu = new ChapterManager().Add(chapter);
                        if (resu > 0)
                        {
                            chapter.ChapterId = resu;
                        }
                        else
                        {
                            throw new Exception("插入章标题失败。");
                        }
                    }
                }
                if (node.TextBookId != textbook.TextBookId || node.ChapterName != nodeFromExcel)
                {
                    ChapterManager chaptermanager = new ChapterManager();
                    List<Chapter> list = chaptermanager.GetModel(textbook.TextBookId, nodeFromExcel);
                    if (list.Count > 0)
                    {
                        node = list[0];
                    }
                    else
                    {
                        node = new Chapter();
                        node.TextBookId = textbook.TextBookId;
                        node.ChapterName = nodeFromExcel;
                        node.ChapterParentNo = chapter.ChapterId;
                        node.ChapterDeep = 1;
                        node.ChapterRemark = nodeFromExcel;
                        int resu = new ChapterManager().Add(node);
                        if (resu > 0)
                        {
                            node.ChapterId = resu;
                        }
                        else
                        {
                            throw new Exception("插入节标题失败。");
                        }
                    }
                }
                question.ChapterId = node.ChapterId;
                question.Remark = row.GetCell(15).StringCellValue.Trim();
                int qr = questionmanager.Add(question);
                Thread.Sleep(500);
                Debug.WriteLine("科目：" + path + ",  第 " + i + " 题 :" + qr);
            };//一本试题结束

        };//全部结束
        common.ShowMessageBox(this.Page, "全部完成");
    }
}
