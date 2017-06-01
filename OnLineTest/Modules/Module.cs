using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Modules
{
    public class Module : IHttpModule
    {
        HttpApplication HttpApp;
        #region IHttpModule 成员
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }
        /// <summary>
        /// 用户请求进入管道时发生的暴露事件，注意要在WEB.CONFIG中注册才有用
        /// 注册方式：add name="myHttpModule" type="Modules.Module,Modules"
        /// 参数的意义  name:自定义一个名称  type:有两个参数，第一个参数是处理的类；第二个参数是程度集名称-->
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApp = (HttpApplication)sender;
            string RequestPath = HttpApp.Context.Request.Path;
            string newRequestPath = RequestPath;
            if (Regex.IsMatch(RequestPath, @"\.html"))
            {
                newRequestPath = Regex.Replace(RequestPath, @"\.html", ".aspx");
            }
            else if (Regex.IsMatch(RequestPath, @"\.xhtml"))
            {
                newRequestPath = Regex.Replace(RequestPath, @"\.xhtml", ".ashx");
            }
            HttpApp.Context.RewritePath(newRequestPath);
            
        }

        public void Dispose()
        {
            HttpApp.Dispose();
        }



        #endregion
    }
}
