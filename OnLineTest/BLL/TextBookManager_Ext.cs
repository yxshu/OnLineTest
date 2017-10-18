
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 教材实例
    /// </summary>
    public partial class TextBookManager
    {
        /// <summary>
        /// 根据书名获得textbook列表
        /// </summary>
        /// <param name="textbookname"></param>
        /// <returns></returns>
        public TextBook[] GetModel(string textbookname)
        {
            TextBook[] textbooks = null;
            //select * from TextBook where TextBookName='个人求生'
            DataSet ds = dal.GetList("TextBookName='" + textbookname + "'");
            textbooks = DataTableToList(ds.Tables[0]).ToArray();
            return textbooks;
        }
    }
}

