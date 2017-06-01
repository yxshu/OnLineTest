
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 历年真题信息实例(仅
    /// </summary>
    public partial class PastExamPaperManager
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="PaperCodeId"></param>
        /// <param name="ExamZoneId"></param>
        /// <param name="PastExamPaperPeriodNo"></param>
        /// <returns></returns>
        public bool Exists(int PaperCodeId, int ExamZoneId, int PastExamPaperPeriodNo)
        {
            return dal.Exists(PaperCodeId, ExamZoneId, PastExamPaperPeriodNo);
        }
        public PastExamPaper GetModel(int PaperCodeId, int ExamZoneId, int PastExamPaperPeriodNo)
        {
            return dal.GetModel(PaperCodeId, ExamZoneId, PastExamPaperPeriodNo);
        }
    }

}

