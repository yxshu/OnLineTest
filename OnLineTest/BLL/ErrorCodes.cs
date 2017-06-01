using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{

    public enum ErrorCodes
    {
        用户不存在 = 1000,
        存在多个同名同密码用户 = 1001,
        用户没有提交数据或提交不全或从非法路径跳转 = 1002,
        用户没有登录 = 1003,
        验证码输入错误=1004,
        未定义的错误=1005,
        用户权限不足=1006
    }
}
