using System;
namespace OnLineTest.Model
{
    /// <summary>
    /// 试题实例
    /// </summary>
    [Serializable]
    public partial class Question
    {
        public Question()
        { }
        #region Model
        private int _questionid;
        private string _questiontitle;
        private string _answera;
        private string _answerb;
        private string _answerc;
        private string _answerd;
        private int _correctanswer;
        private string _explain;
        private string _imageaddress = "Default.jpg";
        private int _difficultyid;
        private int _userid;
        private DateTime _uploadtime = DateTime.Now;
        private int _verifytimes = 0;
        private bool _isverified = false;
        private bool _isdelte = false;
        private int _issupported = 0;
        private int _isdesupported = 0;
        private int _papercodeid;
        private int? _textbookid;
        private int? _chapterid;
        private int? _pastexampaperid;
        private int? _pastexamquestionid;
        private string _remark;
        /// <summary>
        /// 试题ID int identity(1,1) primary key
        /// </summary>
        public int QuestionId
        {
            set { _questionid = value; }
            get { return _questionid; }
        }
        /// <summary>
        /// 题目 text not null
        /// </summary>
        public string QuestionTitle
        {
            set { _questiontitle = value; }
            get { return _questiontitle; }
        }
        /// <summary>
        /// 选项A text  not null
        /// </summary>
        public string AnswerA
        {
            set { _answera = value; }
            get { return _answera; }
        }
        /// <summary>
        /// 选项B text  not null
        /// </summary>
        public string AnswerB
        {
            set { _answerb = value; }
            get { return _answerb; }
        }
        /// <summary>
        /// 选项C text  not null
        /// </summary>
        public string AnswerC
        {
            set { _answerc = value; }
            get { return _answerc; }
        }
        /// <summary>
        /// 选项D text  not null
        /// </summary>
        public string AnswerD
        {
            set { _answerd = value; }
            get { return _answerd; }
        }
        /// <summary>
        /// 参考答案 int  not null check(CorrectAnswer in(1,2,3,4))
        /// </summary>
        public int CorrectAnswer
        {
            set { _correctanswer = value; }
            get { return _correctanswer; }
        }
        /// <summary>
        /// 解析 Explain text null,-----解析
        /// </summary>
        public string Explain
        {
            set { _explain = value; }
            get { return _explain; }
        }
        /// <summary>
        /// 题目对应的图形(如果有) nvarchar(100) null
        /// </summary>
        public string ImageAddress
        {
            set { _imageaddress = value; }
            get { return _imageaddress; }
        }
        /// <summary>
        /// 外键,难度系数 int not null references Difficulty(DifficultyId)
        /// </summary>
        public int DifficultyId
        {
            set { _difficultyid = value; }
            get { return _difficultyid; }
        }
        /// <summary>
        /// 外键,上传此试题的用户 int not null references Users(UserId)
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 上传的时间 datetime not null default getdate()
        /// </summary>
        public DateTime UpLoadTime
        {
            set { _uploadtime = value; }
            get { return _uploadtime; }
        }
        /// <summary>
        /// 被审核的次数(三次以后进入终审) int not null default 0
        /// </summary>
        public int VerifyTimes
        {
            set { _verifytimes = value; }
            get { return _verifytimes; }
        }
        /// <summary>
        /// 是否审核通过标记,0为不通过,1为通过,只有通过审核以后,才将试题更新到审核后的状态,否则不更新 bit not null default 0
        /// </summary>
        public bool IsVerified
        {
            set { _isverified = value; }
            get { return _isverified; }
        }
        /// <summary>
        /// 软删除标记 bit not null default 0
        /// </summary>
        public bool IsDelte
        {
            set { _isdelte = value; }
            get { return _isdelte; }
        }
        /// <summary>
        /// 被赞次数 int not null default 0
        /// </summary>
        public int IsSupported
        {
            set { _issupported = value; }
            get { return _issupported; }
        }
        /// <summary>
        /// 被踩次数 int not null default 0
        /// </summary>
        public int IsDeSupported
        {
            set { _isdesupported = value; }
            get { return _isdesupported; }
        }
        /// <summary>
        /// 外键,题目所属的试卷代码ID int not null references PaperCodes(PaperCodeId)
        /// </summary>
        public int PaperCodeId
        {
            set { _papercodeid = value; }
            get { return _papercodeid; }
        }
        /// <summary>
        /// 外键,题目对应的教材(因为一个试卷代码可以有多本教材) int null references TextBook(TextBookId)
        /// </summary>
        public int? TextBookId
        {
            set { _textbookid = value; }
            get { return _textbookid; }
        }
        /// <summary>
        /// 试题所对应的章节 int null references Chapter(ChapterId)
        /// </summary>
        public int? ChapterId
        {
            set { _chapterid = value; }
            get { return _chapterid; }
        }
        /// <summary>
        /// 试题是否是历年真题,null表示不是真题 int null references PastExamPaper(PastExamPaperId)
        /// </summary>
        public int? PastExamPaperId
        {
            set { _pastexampaperid = value; }
            get { return _pastexampaperid; }
        }
        /// <summary>
        ///真题对应的编号   如果是真题，则在真题中的题号
        /// </summary>
        public int? PastExamQuestionId
        {
            set { _pastexamquestionid = value; }
            get { return _pastexamquestionid; }
        }
        /// <summary>
        /// 备注  Remark text null---------备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

