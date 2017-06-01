
using System.Text;
using System;
namespace Model
{
    public class Pager
    {
        /// <summary>
        ///构造函数 
        /// </summary>
        /// <param name="countnum">总条数</param>
        /// <param name="currentpage">当前页码</param>
        /// <param name="countperpage">页容量</param>
        /// <param name="urlformat">URL格式(形式：Search.aspx?subject=Question{&Page})</param>
        public Pager(int countnum, int currentpage, int countperpage, string urlformat)
        {
            CountNum = countnum;
            CurrentPage = currentpage;
            CountPerPage = countperpage;
            UrlFormat = urlformat;
            displayNum = 9;
        }
        public Pager() { displayNum = 9; }
        public int CountNum { get; set; }
        public int CurrentPage { get; set; }
        public int CountPerPage { get; set; }//每页所显示的数据量
        public string UrlFormat { get; set; }
        private int displayNum { get; set; }//要显示的页面标签条数(不包括首页，前页，后页和尾页)
        private string initURL(int i)
        {
            return UrlFormat.Replace("{&Page}", "&Page=" + i);
        }
        public string PageRender()
        {
            int countpage = (int)Math.Ceiling((double)CountNum / CountPerPage);
            string url = UrlFormat.Replace("{&Page}", "&Page=");
            StringBuilder sb = new StringBuilder();
            if (countpage <= displayNum)//总页数小于要显示的标签个数
            {
                for (int i = 1; i <= countpage; i++)
                {
                    if (i == CurrentPage)
                        sb.Append(i);
                    else
                    {
                        sb.Append("<a href=" + initURL(i) + ">" + i + "</a>");
                    }
                }
            }
            else
            {
                sb.Append("<a class='doublePage' href=" + initURL(1) + ">" + "首" + "</a>");
                if (CurrentPage != 1)
                {
                    sb.Append("<a class='doublePage' href=" + initURL(CurrentPage - 1) + ">" + "前" + "</a>");
                }
                for (int i = 1; i <= displayNum; i++)
                {

                    if (CurrentPage < (int)Math.Ceiling((double)displayNum / 2))
                    {
                        if (i == CurrentPage)//当前页的输出不带超键接
                        {
                            sb.Append( CurrentPage);
                        }
                        else
                        {
                            sb.Append("<a href=" + initURL(i) + ">" + i + "</a>");
                        }
                    }
                    else if ((int)(countpage - CurrentPage) < (int)Math.Ceiling((double)displayNum / 2))
                    {
                        if (countpage - displayNum + i == CurrentPage)//当前页的输出不带超键接
                        {
                            sb.Append(CurrentPage);
                        }
                        else
                        {
                            sb.Append("<a href=" + initURL(countpage - displayNum + i) + ">" + (countpage - displayNum + i) + "</a>");
                        }
                    }
                    else
                    {
                        if ((CurrentPage - (int)Math.Ceiling((double)displayNum / 2) + i) == CurrentPage) 
                        {
                            sb.Append(CurrentPage);
                        }
                        else
                        {
                            sb.Append("<a href=" + initURL(CurrentPage - (int)Math.Ceiling((double)displayNum / 2) + i) + ">" + (CurrentPage - (int)Math.Ceiling((double)displayNum / 2) + i) + "</a>");
                        }
                    }

                }
                if (CurrentPage != countpage)
                {

                    sb.Append("<a class='doublePage'  href=" + initURL(CurrentPage + 1) + ">" + "后" + "</a>");
                }
                sb.Append("<a class='doublePage' href=" + initURL(countpage) + ">" + "末" + "</a>");
            }

            return sb.ToString();
        }
    }
}
