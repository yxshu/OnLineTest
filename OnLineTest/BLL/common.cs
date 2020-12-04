using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using log4net;
using System.Web.UI;
using OnLineTest.Model;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Analysis;
using Lucene.Net.Search;
using System.Diagnostics;
using PanGu;
using System.Reflection;
using Lucene.Net.QueryParsers;
using System.Data;


namespace OnLineTest.BLL
{
    /// <summary>
    /// 通用类
    /// </summary>
    public static class common
    {
        /// <summary>
        /// 系统根目录
        /// </summary>
        public static string RootPath = System.Web.HttpContext.Current.Request.ApplicationPath;

        #region 设置依赖SQL数据库缓存
        /// <summary>
        /// 设置依赖SQL数据库缓存
        /// </summary>
        /// <param name="CacheKey">名称</param>
        /// <param name="objObject">值</param>
        /// <param name="dep">依赖项，可以使用本类中的initialSqlCacheDependency(表名称)</param>
        public static void SetCacheBySql(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep)
        {
            System.Web.Caching.Cache objCache = System.Web.HttpRuntime.Cache;
            objCache.Insert(
                CacheKey,
                objObject,
                dep,
                System.Web.Caching.Cache.NoAbsoluteExpiration,//从不过期
                System.Web.Caching.Cache.NoSlidingExpiration,//禁用可调过期
                System.Web.Caching.CacheItemPriority.Default,
                null);
        }
        #endregion

        #region 从缓存中取数据
        /// <summary>
        /// 从缓存中取数据
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCacheBySql(string CacheKey)
        {

            System.Web.Caching.Cache objCache = System.Web.HttpRuntime.Cache;
            return objCache[CacheKey];
        }
        #endregion

        #region 初始化依赖项
        /// <summary>
        /// 初始化依赖项
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static System.Web.Caching.AggregateCacheDependency initalSqlCacheDependency(List<string> tablenames)
        {
            System.Web.Caching.AggregateCacheDependency SqlCacheDependencys = new System.Web.Caching.AggregateCacheDependency();
            foreach (string tablename in tablenames)
            {
                //依赖数据库codematic中的P_Product表变化 来更新缓存
                SqlCacheDependencys.Add(new System.Web.Caching.SqlCacheDependency("OnLineTest", tablename));
            }
            return SqlCacheDependencys;
        }
        public static System.Web.Caching.SqlCacheDependency initalSqlCacheDependency(string tablename)
        {
            //依赖数据库codematic中的P_Product表变化 来更新缓存
            return new System.Web.Caching.SqlCacheDependency("OnLineTest", tablename);

        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public static ILog logger()
        {
            return LogManager.GetLogger("FromCommon");
        }
        #endregion

        #region 将指定字符串进行MD5加密
        /// <summary>
        /// 将指定字符串进行MD5加密
        /// </summary>
        public static string GetMD5(string oldStr)
        {
            //将输入转换为ASCII 字符编码
            ASCIIEncoding enc = new ASCIIEncoding();
            //将字符串转换为字节数组
            byte[] buffer = enc.GetBytes(oldStr);
            //创建MD5实例
            MD5 md5 = new MD5CryptoServiceProvider();
            //进行MD5加密
            byte[] hash = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            //拼装加密后的字符
            for (int i = 0; i < hash.Length; i++)
            {
                sb.AppendFormat("{0:x2}", hash[i]);
            }
            //输出加密后的字符串
            return sb.ToString();
        }
        #endregion

        #region 生成验证码字符串
        /// <summary> 
        /// 生成验证码字符串 
        /// </summary> 
        /// <param name="codeLen">验证码字符长度</param> 
        /// <returns>返回验证码字符串</returns> 
        private static string MakeCode(int codeLen)
        {
            if (codeLen < 1)
            {
                return string.Empty;
            }

            int number;
            string checkCode = string.Empty;
            Random random = new Random();
            for (int index = 0; index < codeLen; index++)
            {
                number = random.Next();
                if (number % 2 == 0)
                {
                    checkCode += (char)('0' + (char)(number % 10));     //生成数字 
                }
                else
                {
                    checkCode += (char)('A' + (char)(number % 26));     //生成字母 
                }
            }
            checkCode = checkCode.Replace('o', '0');
            return checkCode;
        }
        #endregion

        #region 生成验证码图片流
        ///<summary> 
        /// 生成验证码图片流 
        /// </summary> 
        /// <param name="checkCode">验证码字符串的长度</param> 
        /// <returns>返回验证码图片流</returns> 
        public static MemoryStream CreateValidCode(int codeLen, int heigth, out string checkCode)
        {
            checkCode = MakeCode(codeLen);
            if (string.IsNullOrEmpty(checkCode))
            {
                return null;
            }

            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 13.0)), heigth);
            Graphics graphic = Graphics.FromImage(image);
            try
            {
                Random random = new Random();

                graphic.Clear(Color.White);

                int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
                //画杂七杂八的干拢线条
                for (int index = 0; index < 25; index++)
                {
                    x1 = random.Next(image.Width);
                    x2 = random.Next(image.Width);
                    y1 = random.Next(image.Height);
                    y2 = random.Next(image.Height);
                    graphic.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Red, Color.DarkRed, 1.2f, true);
                graphic.DrawString(checkCode, font, brush, (image.Width - checkCode.Length * 12) / 2, (image.Height - font.Height) / 2);

                int x = 0;
                int y = 0;

                //画图片的前景噪音点 
                for (int i = 0; i < 100; i++)
                {
                    x = random.Next(image.Width);
                    y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线 
                graphic.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //将图片验证码保存为流Stream返回 
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms;
            }
            finally
            {
                graphic.Dispose();
                image.Dispose();
            }
        }
        #endregion

        #region 要跳转的链接
        /// <summary>
        /// 要跳转的链接
        /// </summary>
        /// <param name="TransferPage">跳转的页面，输入相对路径"~\\TransferPage"</param>
        /// <param name="ErrorCode">错误码，可以为NULL</param>
        /// <param name="ex">异常，可以为null</param>
        /// <param name="SecondTransferPage">要求二次跳的页面</param>
        public static void ServerTransfer(string TransferPage, int? ErrorCode, Exception ex, string SecondTransferPage)
        {
            System.Web.HttpContext.Current.Response.Redirect(RootPath + "\\" + TransferPage + "?code=" + ErrorCode + "&exception=" + ex.Message.Replace("\n", "") + "&SecondTransferPage=" + SecondTransferPage);
        }

        public static void ServerTransfer(string TransferPage, int? ErrorCode, string errorMessage, string SecondTransferPage)
        {
            System.Web.HttpContext.Current.Response.Redirect(RootPath + "\\" + TransferPage + "?code=" + ErrorCode + "&exception=" + errorMessage + "&SecondTransferPage=" + SecondTransferPage);
        }

        public static void ServerTransfer(string TransferPage, int? ErrorCode, string errorMessage, Page page, string SecondTransferPage)
        {
            System.Web.HttpContext.Current.Response.Redirect(RootPath + "\\" + TransferPage + "?code=" + ErrorCode + "&exception=" + errorMessage + "&page=" + page.Page.Title + "&SecondTransferPage=" + SecondTransferPage);
        }

        public static void ServerTransfer(string TransferPage, int? ErrorCode, Exception ex, Page page, string SecondTransferPage)
        {
            System.Web.HttpContext.Current.Response.Redirect(RootPath + "\\" + TransferPage + "?code=" + ErrorCode + "&exception=" + ex.Message.Replace("\n", "") + "&page=" + page.Page.Title + "&SecondTransferPage=" + SecondTransferPage);
        }

        public static void ServerTransfer(string TransferPage, int? ErrorCode, Exception ex, string errorMessage, string SecondTransferPage)
        {
            System.Web.HttpContext.Current.Response.Redirect(RootPath + "\\" + TransferPage + "?code=" + ErrorCode + "&exception=" + ex.Message.Replace("\n", "") + "&errormessage=" + errorMessage + "&SecondTransferPage=" + SecondTransferPage);
        }

        public static void ServerTransfer(string TransferPage, int? ErrorCode, Page page, string SecondTransferPage)
        {
            System.Web.HttpContext.Current.Response.Redirect(RootPath + "\\" + TransferPage + "?code=" + ErrorCode + "&page=" + page.Page.Title + "&SecondTransferPage=" + SecondTransferPage);
        }

        #endregion

        #region 以javascript形式在前台弹出提示框
        /// <summary>
        /// 以javascript形式在前台弹出提示框
        /// </summary>
        /// <param name="page">要弹框的页面类</param>
        /// <param name="showmessage">弹出的内容</param>
        public static void ShowMessageBox(Page page, string showmessage)
        {
            ClientScriptManager sm = page.ClientScript;
            sm.RegisterClientScriptBlock(page.GetType(), "messagebox", "window.onload = function() { alert('" + showmessage + "')};", true);
            //sm.RegisterStartupScript(page.GetType(), "messagebox", "window.onload = function() { alert('" + showmessage + "')};", true);
        }
        #endregion


        #region 向前台添加JQuery代码（代码加载在文档的前面）
        /// <summary>
        /// 向前台添加JQuery代码
        /// </summary>
        /// <param name="page">页面类型</param>
        /// <param name="JQueryCode">要注入的代码段</param>
        public static void InsertJQueryCodeByRegisterClientScriptBlock(Page page, string Key, string JQueryCode)
        {

            ClientScriptManager cm = page.ClientScript;
            cm.RegisterClientScriptInclude("QutoJQuery", "./JavaScript/jquery-1.9.1.js");
            cm.RegisterClientScriptBlock(page.GetType(), Key, JQueryCode, true);
        }
        #endregion

        #region 向前台输出JQuery代码（此方法将代码加载在文档的最后）
        /// <summary>
        /// 向前台输出JQuery代码（此方法将代码加载在文档的最后）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Key"></param>
        /// <param name="Script"></param>
        public static void InsertJQueryCodeByRegisterStartupScript(Page page, string Key, string Script)
        {
            ClientScriptManager cm = page.ClientScript;
            cm.RegisterClientScriptInclude("QutoJQuery", "./JavaScript/jquery-1.9.1.js");
            cm.RegisterStartupScript(page.GetType(), Key, Script, true);
        }
        #endregion

        #region 将内容中的某些部分高亮（可以针对所有的字符执行）
        /// <summary>
        /// 将内容中的某些部分高亮（可以针对所有的字符执行）
        /// </summary>
        /// <param name="keyword">要高亮显示的关键词</param>
        /// <param name="content">含有关键词的内容</param>
        /// <returns>如果内容中含有关键词将其高亮返回，如果没有，则返回内容</returns>
        public static String highLight(string keyword, String content)
        {
            PanGu.HighLight.SimpleHTMLFormatter formatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color='red'>", "</font>");//高亮部分的表现方式，即前后要加的标签
            PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(formatter, new Segment());//两个参数的意义1、高亮的表现形式；2、要高亮显示的关键词，并进行分词
            highlighter.FragmentSize = 800;//要显示的长度
            string msg = highlighter.GetBestFragment(keyword, content);//要上面要求返回的字符
            if (string.IsNullOrEmpty(msg))
            {
                return content;
            }
            else
            {
                return msg;
            }
        }
        #endregion

        #region 将一个object类型的对象中string类型的属性进行高亮标识，并返回标识后的object对象
        /// <summary>
        /// 将一个object类型的对象中string类型的属性进行高亮标识，并返回标识后的object对象
        /// </summary>
        /// <param name="obj">要标识的对象</param>
        /// <param name="keyword">要标识的关键字</param>
        /// <returns>经标识后的对象</returns>
        public static object HighLight(object obj, string keyword)
        {
            if (obj != null)
            {
                PropertyInfo[] proinfo = obj.GetType().GetProperties();
                foreach (PropertyInfo pro in proinfo)
                {
                    if (pro.PropertyType.FullName == typeof(string).FullName)
                    {
                        if (pro.CanWrite && pro.CanRead)
                        {
                            pro.SetValue(obj, highLight(keyword, pro.GetValue(obj, null).ToString()), null);
                        }
                    }
                }
                return obj;
            }
            return obj;
        }
        #endregion

        #region 对于特定的网页，用户是否被授权访问
        /// <summary>
        /// 对于特定的网页，用户是否被授权访问
        /// </summary>
        /// <param name="user">用户实例</param>
        /// <param name="requestFileName">要访问的网页，且是存在授权要求的</param>
        /// <returns>要求授权的网页，用户没有得到授权返回false,否则返回true</returns>
        public static bool isAuthorized(OnLineTest.Model.Users user, string requestFileName)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(typeof(common));
            bool isauthorized = true;
            UserGroupManager usergroupmanager = new UserGroupManager();
            if (((UserGroup)usergroupmanager.GetModel(user.UserGroupId)).UserGroupName.Trim() != "超级管理员")
            {
                UserAuthorityManager userauthoritymanager = new UserAuthorityManager();
                AuthorityManager authoritymanager = new AuthorityManager();
                try
                {
                    Authority authority = new Authority();
                    if (HttpRuntime.Cache[requestFileName] != null)
                    {
                        authority = (Authority)HttpRuntime.Cache[requestFileName];
                    }
                    else
                    {
                        authority = authoritymanager.GetModel(requestFileName);
                        if (authority != null)
                        {
                            HttpRuntime.Cache.Insert(requestFileName, authority, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                            logger.Info(requestFileName + "处理网页，对应的权限缓存成功。");
                        }
                    }
                    if (authority != null)
                    {
                        isauthorized = userauthoritymanager.Exists(user, authoritymanager.GetModel(requestFileName));
                        //if (isauthorized)
                        //{
                        //    authority.AuthorityClickNum += 1;
                        //    authoritymanager.Update(authority);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("查询网页授权过程出错。", ex);
                    ServerTransfer("error.aspx", 1005, ex, string.Empty);
                }
            }
            return isauthorized;
        }
        #endregion

        #region 根据路径判断文件夹是否存在，如果存在则返回文件夹信息，如果不存在则新建并返回文件夹信息
        /// <summary>
        /// 根据路径判断文件夹是否存在，如果存在则返回文件夹信息，如果不存在则新建并返回文件夹信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文件夹信息</returns>
        public static DirectoryInfo CreateDirectory(string path)
        {
            DirectoryInfo directoryinfo = new DirectoryInfo(path);
            if (directoryinfo.Exists)
            {
                return directoryinfo;
            }
            else
            {
                directoryinfo.Create();
                return directoryinfo;
            }
        }
        #endregion

        #region 利用盘古分词来分词，返回拆分的词组
        /// <summary>
        /// 利用盘古分词来分词
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static string[] WordSegmentation(string keyword)
        {
            List<string> list = new List<string>();
            Analyzer analyzer = new PanGuAnalyzer();
            //Analyzer analyzer = new StandardAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(keyword));
            Lucene.Net.Analysis.Token token = null;
            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }
            return list.ToArray();
        }
        #endregion

        #region 将一 条记录生成一条索引
        /// <summary>
        ///将一 条记录生成一条索引记录 
        /// </summary>
        /// <param name="CreateIndexDirectionPathInfo">索引记录存放的位置</param>
        /// <param name="field">要添加的字段</param>
        private static void CreateIndex(DirectoryInfo CreateIndexDirectionPathInfo, Field[] field)
        {
            string indexPath = CreateDirectory(CreateIndexDirectionPathInfo.FullName).FullName;
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            bool isExists = IndexReader.IndexExists(directory);
            if (isExists)
            {
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExists, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
            Document document = new Document();
            //只有对需要全文检索的字段才ANALYZED
            Field[] Fields = field;
            foreach (Field fie in Fields)
            {
                document.Add(fie);
            }
            writer.AddDocument(document);
            writer.Optimize();
            writer.Close();
            directory.Close();
        }
        #endregion

        #region 生成一条用于索引的字段
        /// <summary>
        /// 生成一条用于索引的字段
        /// </summary>
        /// <param name="FieldTitle">给字段的命名</param>
        /// <param name="IndexContent">生成索引的内容</param>
        /// <param name="isFieldStroe">内容是否存储</param>
        /// <param name="isANALYZED">内容是否索引</param>
        /// <param name="TermVector">   TermVector表示文档的条目（由一个Document和Field定位）和它们在当前文档中所出现的次数 
        ///0:Field.TermVector.YES:为每个文档（Document）存储该字段的TermVector 
        ///1:Field.TermVector.NO:不存储TermVector 
        ///2:Field.TermVector.WITH_POSITIONS:存储位置 
        ///3:Field.TermVector.WITH_OFFSETS:存储偏移量 
        ///默认值：Field.TermVector.WITH_POSITIONS_OFFSETS:存储位置和偏移量</param>
        /// <returns>生成的索引字段Feild</returns>
        private static Field CreateField(string FieldTitle, string IndexContent, bool isFieldStroe, bool isANALYZED, int TermVector)
        {
            Field.Store store = Field.Store.YES;
            Field.Index ANALYZED = Field.Index.ANALYZED;
            Field.TermVector termvector = Field.TermVector.WITH_POSITIONS_OFFSETS;
            if (isFieldStroe == false)
                store = Field.Store.NO;
            if (isANALYZED == false)
                ANALYZED = Field.Index.NOT_ANALYZED;
            switch (TermVector)
            {
                case 0: termvector = Field.TermVector.YES;
                    break;
                case 1: termvector = Field.TermVector.NO;
                    break;
                case 2: termvector = Field.TermVector.WITH_OFFSETS;
                    break;
                case 3: termvector = Field.TermVector.WITH_POSITIONS;
                    break;
                default: termvector = Field.TermVector.WITH_POSITIONS_OFFSETS;
                    break;
            }
            Field field = new Field(FieldTitle, IndexContent, store, ANALYZED, termvector);
            return field;
        }
        #endregion

        #region 生成一条用于索引的字段(重载)
        /// <summary>
        /// 生成一条用于索引的字段
        /// </summary>
        /// <param name="FieldTitle">给字段的命名</param>
        /// <param name="IndexContent">生成索引的内容</param>
        /// <param name="isFieldStroe">内容是否存储</param>
        /// <param name="isANALYZED">内容是否索引</param>
        /// <param name="TermVector">   TermVector表示文档的条目（由一个Document和Field定位）和它们在当前文档中所出现的次数 
        ///0:Field.TermVector.YES:为每个文档（Document）存储该字段的TermVector 
        ///1:Field.TermVector.NO:不存储TermVector 
        ///2:Field.TermVector.WITH_POSITIONS:存储位置 
        ///3:Field.TermVector.WITH_OFFSETS:存储偏移量 
        ///默认值：Field.TermVector.WITH_POSITIONS_OFFSETS:存储位置和偏移量</param>
        /// <returns>生成的索引字段Feild</returns>
        private static Field CreateField(string FieldTitle, string IndexContent, bool isFieldStroe, bool isANALYZED)
        {
            return CreateField(FieldTitle, IndexContent, isFieldStroe, isANALYZED, -1);
        }
        #endregion

        #region 将一条评论生成一条索引，其中只有Commnet可以检索，通过CommentId取数据
        /// <summary>
        /// 将一条评论生成一条索引，其中只有CommnetContent可以检索，通过CommentId取数据
        /// </summary>
        /// <param name="directoryinfo">索引存放的位置</param>
        /// <param name="comment">要索引的试题，其中只有评论内容可以检索</param>
        public static void CreateIndexofCommnet(DirectoryInfo directoryinfo, Comment comment)
        {
            Field[] fileds = new Field[2];
            Field commId = CreateField("CommentId", comment.CommentId.ToString(), true, false, 1);
            Field commContent = CreateField("CommentContent", comment.CommentContent, false, true);
            fileds[0] = commId;
            fileds[1] = commContent;
            CreateIndex(directoryinfo, fileds);
            logger().Info("第" + comment.CommentId.ToString() + "号评论，创建索引成功。");
        }
        #endregion

        #region 将一条试题生成一条索引，其中有两个字段QuestionId和Question,其中Question包含试题标题和选项，可以检索，通过QuestionId取数据
        /// <summary>
        /// 将一条试题生成一条索引，其中有两个字段QuestionId和Question,其中Question包含试题标题和选项，可以检索，通过QuestionId取数据
        /// </summary>
        /// <param name="directoryinfo"></param>
        /// <param name="question"></param>
        public static void CreateIndexofQuestion(DirectoryInfo directoryinfo, Question question)
        {
            Field[] fileds = new Field[2];
            fileds[0] = CreateField("QuestionId", question.QuestionId.ToString(), true, false, 1);
            fileds[1] = CreateField("QuestionTitle", question.QuestionTitle + question.AnswerA + question.AnswerB + question.AnswerC + question.AnswerD, false, true);
            //fileds[2] = CreateField("AnswerA", question.AnswerA, false, true);
            //fileds[3] = CreateField("AnswerB", question.AnswerB, false, true);
            //fileds[4] = CreateField("AnswerC", question.AnswerC, false, true);
            //fileds[5] = CreateField("AnswerD", question.AnswerD, false, true);
            CreateIndex(directoryinfo, fileds);
            logger().Info("第" + question.QuestionId.ToString() + "题，创建索引成功。");
        }
        #endregion

        #region 根据已经有的索引检索数据
        /// <summary>
        /// 根据已经有的索引检索数据
        /// </summary>
        /// <param name="FieldTitle">检索的字段，注意，这个一定要求是索引中已经存在的</param>
        /// <param name="keyword">关键字、词、句</param>
        /// <param name="directoryinfo">索引所在的位置</param>
        /// <param name="costTime">检索花费的时间</param>
        /// <param name="CountNum">检索到的条数</param>
        /// <param name="CountNum">检索是否成功</param>
        /// <returns></returns>
        public static IEnumerable<Document> Query(string FieldTitle, string keyword, DirectoryInfo directoryinfo, out TimeSpan costTime, out int CountNum, out bool isSuccess)
        {
            isSuccess = false;
            costTime = TimeSpan.Zero;
            CountNum = 0;
            string indexPath = string.Empty;
            if (directoryinfo.Exists)
            {
                indexPath = directoryinfo.FullName;
            }
            else
            {

                ShowMessageBox(new Page(), "索引路径不正确");
                return null;
            }
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            if (reader.GetFieldNames(IndexReader.FieldOption.ALL).Contains(FieldTitle))//判断索引中是否包含此字段
            {
                IndexSearcher searcher = new IndexSearcher(reader);
                //BooleanQuery query = new BooleanQuery();
                PhraseQuery query = new PhraseQuery();
                string[] KeyWords = WordSegmentation(keyword);
                foreach (string word in KeyWords)//先用空格，让用户去分词，空格分隔的就是词“计算机 专业”
                {
                    //query.Add(new TermQuery(new Term(FieldTitle, word)), BooleanClause.Occur.SHOULD);//每个词只要有就查出来，用的“与或搜索”
                    query.Add(new Term(FieldTitle, word));
                }
                query.SetSlop(100);
                TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
                Stopwatch stopwatch = Stopwatch.StartNew();
                stopwatch.Start();
                searcher.Search(query, null, collector);
                stopwatch.Stop();
                costTime = stopwatch.Elapsed;
                CountNum = collector.GetTotalHits();
                ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;
                List<Document> list = new List<Document>();
                for (int i = 0; i < docs.Length; i++)
                {
                    int docId = docs[i].doc;//取到文档的编号（主键，这个是Lucene .net分配的）//检索结果中只有文档的id，如果要取Document，则需要Doc再去取 //降低内容占用
                    Document doc = searcher.Doc(docId);//根据id找Document
                    list.Add(doc);
                    //string url = highLight(KeyWords[0], doc.Get("url"));
                    //string body = highLight(KeyWords[0], doc.Get("body"));
                    //Response.Write(@"<a href='" + url + "'>" + url + "</a> <br/>" + body + "<br /><br />");
                    //Response.Write("<hr/><br />");
                }
                isSuccess = true;
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 根据已经有的索引检索数据
        /// <summary>
        /// 根据已经有的索引检索数据,并返回全部的结果
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <param name="directoryinfo"></param>
        /// <param name="costTime"></param>
        /// <param name="CountNum"></param>
        /// <param name="isSuccess"></param>
        /// <returns></returns>
        public static IEnumerable<Document> Query(Type type, string keyword, DirectoryInfo directoryinfo, out TimeSpan costTime, out int CountNum, out bool isSuccess)
        {
            costTime = TimeSpan.Zero;
            TimeSpan totaltime = TimeSpan.Zero;
            CountNum = 0;
            int totalcout = 0;
            isSuccess = true;
            List<Document> list = new List<Document>();
            PropertyInfo[] info = type.GetProperties();
            for (int i = 0; i < info.Length; i++)
            {
                IEnumerable<Document> list1 = new List<Document>();
                list1 = Query(info[i].Name, keyword, directoryinfo, out costTime, out CountNum, out isSuccess);
                if (list1 != null)
                {
                    list.AddRange(list1);
                }
                totaltime += costTime;
                totalcout += CountNum;
            }
            costTime = totaltime;
            CountNum = totalcout;
            isSuccess = true;
            return list;
        }
        #endregion

        #region 根据已经有的索引检索数据,并返回特定数量的结果
        /// <summary>
        /// 根据已经有的索引检索数据,并返回特定数量的结果
        /// </summary>
        /// <param name="type">检索的数据类型</param>
        /// <param name="keyword">关键词</param>
        /// <param name="directoryinfo">索引目录</param>
        /// <param name="starIndex">返回开始的条数</param>
        /// <param name="retuTotalCountNum">返回的总条数</param>
        /// <param name="costTime">花费的时间</param>
        /// <param name="CountNum">获得的条数（注意，这里并不是能够搜索的所有条数，而是返回的条数+开始的返回条数的索引）</param>
        /// <param name="isSuccess">是否成功标志</param>
        /// <returns></returns>
        public static IEnumerable<Document> Query(Type type, string keyword, DirectoryInfo directoryinfo, int starIndex, int retuTotalCountNum, out TimeSpan costTime, out int CountNum, out bool isSuccess)
        {
            costTime = TimeSpan.Zero;
            TimeSpan totaltime = TimeSpan.Zero;
            CountNum = 0;
            int totalcout = 0;
            isSuccess = true;
            List<Document> list = new List<Document>();
            PropertyInfo[] info = type.GetProperties();
            for (int i = 0; i < info.Length; i++)
            {
                if (totalcout < (starIndex + retuTotalCountNum))
                {
                    IEnumerable<Document> list1 = new List<Document>();
                    list1 = Query(info[i].Name, keyword, directoryinfo, out costTime, out CountNum, out isSuccess);
                    if (list1 != null)
                    {
                        list.AddRange(list1);
                    }
                    totaltime += costTime;
                    totalcout += CountNum;
                }
            }
            costTime = totaltime;
            CountNum = totalcout;
            isSuccess = true;
            return list;
        }
        #endregion

        #region 将字符串型“A、B、C、D”转化为“1、2、3、4”
        /// <summary>
        /// 将字符串型“A、B、C、D”转化为“1、2、3、4”
        /// </summary>
        /// <param name="p">A、B、C、D</param>
        /// <returns>1、2、3、4，如果返回-1，表示转化错误</returns>
        public static int tryparse(string p)
        {
            p = p.Trim().ToLower();
            if (p.Length == 1)
            {
                switch (p)
                {
                    case "a":
                        return 1;

                    case "b":
                        return 2;

                    case "c":
                        return 3;

                    case "d":
                        return 4;

                    default: return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region 将字符串型“1、2、3、4”转化为“A、B、C、D”
        /// <summary>
        /// 将字符串型“A、B、C、D”转化为“1、2、3、4”
        /// </summary>
        /// <param name="p">1、2、3、4</param>
        /// <returns>A、B、C、D，如果返回NULL，表示转化错误</returns>
        public static string tryparse(int i)
        {
            if (i == 1 || i == 2 || i == 3 || i == 4)
            {
                switch (i)
                {
                    case 1:
                        return "A";

                    case 2:
                        return "B";

                    case 3:
                        return "C";

                    case 4:
                        return "D";

                    default: return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 根据整形选择答案
        /// <summary>
        /// 根据整形选择答案
        /// </summary>
        /// <param name="i"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        public static string getCorrectAnswer(int i, string A, string B, string C, string D)
        {
            switch (common.tryparse(i).ToUpper())
            {
                case "A": return "A：" + A;
                case "B": return "B：" + B;
                case "C": return "C：" + C;
                case "D": return "D：" + D;
                default: return "地球人还不会。";
            }
        }
        #endregion

        #region 尝试更新或者新增用户搜索关键词
        /// <summary>
        /// 尝试更新或者新增用户搜索关键词
        /// </summary>
        /// <param name="keywords">用户搜索的关键词</param>
        public static void UpdataOrAddSuggestionKeyword(string keywords)
        {
            SuggestionKeyword sk = new SuggestionKeyword();
            sk.SuggestionKeywords = keywords.Length > 50 ? keywords.Substring(0, 50) : keywords;//varchar 100,因为是汉字占两位，所以这里是50
            SuggestionKeywordManager skm = new SuggestionKeywordManager();
            int SuggestionKeywordsId = -1;
            if (!skm.Exists(sk.SuggestionKeywords, out SuggestionKeywordsId))
            {
                sk.SuggestionKeywordsCreateTime = DateTime.Now;
                sk.SuggestionKeywordsNum = 1;
                skm.Add(sk);
            }
            else
            {
                sk.SuggestionKeywordsId = SuggestionKeywordsId;
                sk = skm.GetModel(sk.SuggestionKeywordsId);
                sk.SuggestionKeywordsNum += 1;
                skm.Update(sk);
            }
        }
        #endregion

        #region 根据时间调整问候语
        /// <summary>
        /// 根据时间调整问候语
        /// </summary>
        /// <returns>不同的问候语</returns>
        public static string SayHelloByTime()
        {
            int hour = DateTime.Now.Hour;
            switch (hour)
            {
                case 0: return "信念是感知阳光的鸟，当黎明还沉浸在黑暗之中的时候，它就歌唱了。愿信念的鸟在你心中筑巢";
                case 1: return "世上最心痛的距离，是你冷漠的说你已不在意。";
                case 2: return "该休息了，身体可是革命的本钱啊！";
                case 3: return "夜深人静，只有你敲击鼠标的声音...";
                case 4: return "爱是一种最极端的状态，生活总能继续下去，它或是毁掉爱，或是被爱毁掉。";
                case 5: return "或许，真正的成功就是按照自己喜欢的方式，去度过人生。";
                case 6: return "你知道吗，此时是国内网络速度最快的时候！";
                case 7: return "与自己最虚荣最美丽的时候告别，不再为容颜禁锢。像苍老一样希望，像青春一样绝望。";
                case 8: return "在我心灵的百花园里，采集金色的鲜花，我把最鲜艳的一朵给你，作为我对你的问候。";
                case 9: return "雄关漫道真如铁，而今迈步从头越";
                case 10: return "上午好！今天你看上去好精神哦！";
                case 11: return "当你年轻时，以为什么都有答案，可是老了的时候，你会明白，其实人生并没有所谓的答案。";
                case 12: return "其实，我不是一定要等你，只是等上了，就等不了别人了。";
                case 13: return "少年的时光就是晃，用大把时间彷徨，只用几个瞬间来成长。";
                case 14: return "壮志与毅力是事业的双翼，愿你张开这双翼，展翅高飞，飞越一个又一个山巅";
                case 15: return "曾经以为，拥有是不容易的；后来才知道，舍弃更难。";
                case 16: return "知音，能有一两个已经很好了，实在不必太多。朋友之乐，贵在那份踏实的信赖。";
                case 17: return "世界也在忙碌，谁不忙碌？愿你笑对忙碌，寻找生命的快乐。";
                case 18: return "如果一个人必须完成一件自己不喜欢的事，最好的办法就是尽快做好，然后结束。";
                case 19: return "茫茫人生，知己难寻，好朋友有缘在一起欢喜就好，祝福你";
                case 20: return "月光洒进窗台，心里也想起了你，是否一切都还如意。 ";
                case 21: return "重要的不是他有多好，而是他对你有多好。";
                case 22: return "无论世界怎样变换，我对你的爱岿然不动。";
                case 23: return "真是越玩越精神，不打算睡了？";
                default: return "好好享受与家人团聚的时刻~";
            }
        }
        #endregion

        #region 根据用户提交的数据得到用户的操作系统版本号
        /// <summary>
        /// 根据用户提交的数据得到用户的操作系统版本号
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetOSNameByUserAgent(HttpContext context)
        {
            string userAgent = context.Request.UserAgent == null ? "无" : context.Request.UserAgent;
            string osVersion = "未知";

            if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion + "版本号:" + userAgent;

        }
        #endregion

        #region 通过n次随机的方式从数据库中选一道试题，如果n次没有成功，则返回null
        /// <summary>
        /// 通过n次(参数)随机的方式从数据库中选一道试题，如果三次没有成功，则返回null
        /// </summary>
        /// <returns>null或者Question实例</returns>
        public static Question CreateQuestionbyRandom(int n)
        {
            Question question = new Question();
            QuestionManager questionmanager = new QuestionManager();
            int i = 0;
            do
            {
                question = questionmanager.GetModel(new Random().Next(1, questionmanager.GetMaxId()));
                i++;
            } while (question == null && i < n);
            return question;
        }
        #endregion

        #region 获取当前用户
        public static Users GetCurrnetUser(HttpContext context)
        {
            Users user = new Users();
            if (context.Session != null && context.Session["User"] != null)
            {
                user = (Users)context.Session["User"]; //得到当前登录用户实例 
            }
            else if (context.Request.Cookies["UserId"] != null)
            {
                user.UserId = Convert.ToInt32(context.Request.Cookies["UserId"].Value);
                user = new OnLineTest.BLL.UsersManager().GetModel(user.UserId);
            }
            else { user = null; }
            return user;
        }
        #endregion

        #region 将一个数据行转换为一个以列号为名称的字典集
        /// <summary>
        /// 将一个数据行转换为一个以列号为名称的字典集
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns>两个情况 没有值时返回null，否则返回一个字典</returns>
        public static Dictionary<string, object> DataRowTODictionary(DataRow dr)
        {
            Dictionary<string, object> dic = null;
            if (dr != null && dr.Table.Columns.Count > 0)
            {
                dic = new Dictionary<string, object>();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    string columnname = dr.Table.Columns[i].ColumnName;
                    object value = dr[columnname];
                    dic.Add(columnname, value);
                }
            }
            return dic;
        }
        #endregion

        #region 将dataset的数据集转换成list
        /// <summary>
        /// 将dataset的数据集转换成list
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>两种情况，一为null,二为填了数据的list<dictionary<string,object>></returns>
        public static List<Dictionary<string, object>> DataSetToList(DataSet ds)
        {
            List<Dictionary<string, object>> list = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = new List<Dictionary<string, object>>();
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic = DataRowTODictionary(ds.Tables[i].Rows[j]);
                        list.Add(dic);
                    }
                }
            }
            return list;
        }
        #endregion


    }
}
