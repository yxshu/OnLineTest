
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
using System.Collections.Generic;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 教材章节表
    /// </summary>
    public partial class ChapterManager
    {
        /// <summary>
        /// 根据textbookid得到结构化的目录
        /// 其中的结构化体现在 list排序按目录顺序进行
        /// </summary>
        /// <param name="textbookid"></param>
        /// <returns> 返回按目录顺序排列的list<chapter>集合 </returns>
        public List<Chapter> getStructionChapterModel_by_TextBookId(int textbookid)
        {
            List<Chapter> result = new List<Chapter>();
            List<Chapter> temp = GetModelList("TextBookId=" + textbookid + " order by ChapterDeep");
            int MaxDeep = -1;
            //获取最大深度
            foreach (Chapter c in temp)
            {
                if (c.ChapterDeep > MaxDeep)
                    MaxDeep = c.ChapterDeep;
            }
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].ChapterDeep == 0)
                {
                    result.AddRange(getChapterChildren(temp[i], temp, MaxDeep));
                }
            }
            return result;
        }
        /// <summary>
        /// 利用递归查询子节点
        /// </summary>
        /// <param name="chapter"></param>
        /// <param name="temp"></param>
        /// <param name="MaxDeep"></param>
        /// <returns></returns>
        private IEnumerable<Chapter> getChapterChildren(Chapter chapter, List<Chapter> temp, int MaxDeep)
        {
            List<Chapter> list = new List<Chapter>();
            list.Add(chapter);
            if (chapter.ChapterDeep < MaxDeep)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].ChapterParentNo == chapter.ChapterId && temp[i].ChapterDeep == chapter.ChapterDeep + 1)
                    {
                        list.AddRange(getChapterChildren(temp[i], temp, MaxDeep));
                    }
                }
            }
            return list;
        }
        #region 在一个目录集合中查到某个目录的所有底层子目录
        /// <summary>
        /// 在一个目录集合中查到某个目录的所有底层子目录
        /// </summary>
        /// <param name="c">目录</param>
        /// <param name="list">目录的集合</param>
        /// <returns>所有底层目录的集合</returns>
        public List<Chapter> getLowChapter(Chapter c, List<Chapter> list)
        {
            List<Chapter> result = new List<Chapter>();
            bool isTrue = true;//标记自身是不是底层目录
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ChapterParentNo == c.ChapterId)
                {
                    isTrue = false;
                    result.AddRange(getLowChapter(list[i], list));
                }
            }
            if (isTrue) result.Add(c);
            return result;
        }
        #endregion

        #region 求出一个目录集合的所有底层目录并根据试题的总量附上每章节的默认试题量
        /// <summary>
        /// 求出一个目录集合的所有底层目录并根据试题的总量附上每章节的默认试题量
        /// </summary>
        /// <param name="list">目录集合</param>
        /// <param name="QuestionCount">试题试题总量</param>
        /// <returns>返回一个Dictionary,字典中的键值对是 章节对象，此章节对应的试题量</returns>
        public Dictionary<Chapter, int> getAllLowChapterAndDefaultCount(List<Chapter> list, int QuestionCount)
        {
            Dictionary<Chapter, int> result = new Dictionary<Chapter, int>();
            List<Chapter> LowChapter = getAllLowChapter(list);//临时存放底层目录
            int averageCount = (int)Math.Floor((double)QuestionCount / LowChapter.Count);//计算一个平均值
            for (int i = 0; i < LowChapter.Count - 1; i++)//将各底层目录的比例填充到字典中
            {
                result.Add(LowChapter[i], averageCount);
            }
            result.Add(LowChapter[LowChapter.Count], QuestionCount - averageCount * (LowChapter.Count - 1));//为了保证每一个章节的比例都是整数，所以，最后一个需要另外计算
            return result;
        }
        #endregion

        #region 求出一个目录集合的所有底层目录
        /// <summary>
        /// 求出一个目录集合的所有底层目录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Chapter> getAllLowChapter(List<Chapter> list)
        {
            List<Chapter> LowChapter = new List<Chapter>();//临时存放底层目录
            foreach (Chapter c in list)//找出此目录的底层目录
            {
                bool isLowChapter = true;
                for (int i = 0; i < list.Count; i++)
                {
                    if (c.ChapterId == list[i].ChapterParentNo)
                        isLowChapter = false;
                }
                if (isLowChapter)
                    LowChapter.Add(c);
            }
            return LowChapter;
        }
        #endregion

        #region 求出某个textbookid下的所有底层目录
        /// <summary>
        /// 求出某个textbookid下的所有底层目录
        /// </summary>
        /// <param name="textbookid"></param>
        /// <returns></returns>
        public List<Chapter> getAllLowChapterByTestbookId(int textbookid)
        {
            return getAllLowChapter(new ChapterManager().GetModelList("TextBookId=" + textbookid));
        }
        #endregion




        /// <summary>
        /// 根据书本编号和章节标题查询
        /// </summary>
        /// <param name="textbookid">书的编号</param>
        /// <param name="chaptername">章节标题</param>
        /// <returns>返回查询到的集合</returns>
        public List<Chapter> GetModel(int textbookid, string chaptername)
        {
            List<Chapter> list = null;

            //select * from Chapter where TextBookId=1 and ChapterName='识图'
            DataSet ds = dal.GetList("TextBookId=" + textbookid + " and ChapterName='" + chaptername + "'");
            list = this.DataTableToList(ds.Tables[0]);
            return list;


        }








    }


    /// <summary>
    /// 自定义比较器
    /// </summary>
    public class myComparer : IComparer<Chapter>
    {

        public int Compare(Chapter x, Chapter y)
        {
            return x.ChapterId.CompareTo(y.ChapterId);
        }
    }
}

