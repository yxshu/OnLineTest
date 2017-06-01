
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
using System.Collections.Generic;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 试卷代码实例
    /// </summary>
    public partial class PaperCodesManager
    {
        /// <summary>
        /// 按strWhere的要求查询papercodes,并将其中的外链进行实例化
        /// </summary>
        /// <param name="strWhere">查询条件或者排序方式，如：order by papercodeid</param>
        /// <returns>list</returns>
        public List<Dictionary<string, object>> GetPaperCodeAndInstantiation(string strWhere)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataSet ds = dal.GetPaperCodeAndInstantiation(strWhere);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(common.DataRowTODictionary(dr));
            }
            return list;
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PaperCodeId)
        {
            return dal.Exists(PaperCodeId);
        }
        /// <summary>
        /// 根据subjectid查询papercodes,其中按papercodeid排序
        /// </summary>
        /// <param name="subjectid">subjectid</param>
        /// <returns>list<papercodes></returns>
        public List<PaperCodes> GetModelListBySubjectId(int subjectid)
        {
            return DataTableToList(dal.GetModelListBySubjectId(subjectid).Tables[0]);
        }
    }
}

