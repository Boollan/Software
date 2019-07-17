using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cute_debug
{
     public class message
    {

        //消息提示
        public static int messagetext(int num)
        {
            if (num==100)
            {
                return 100;
            }else
                if (num==404)
            {
                
                //System.Windows.Forms.MessageBox.Show("账号或密码错误","系统提示");
                return 404;
            }
            else
                if (num==502)
            {
                //System.Windows.Forms.MessageBox.Show("账号不存在", "系统提示");
                return 502;

            }
            else 
                if (num == 503)
            {
                //System.Windows.Forms.MessageBox.Show("邮箱已被注册 或 用户名已存在", "系统提示");
                return 503;

            }
            {
                //System.Windows.Forms.MessageBox.Show("错误");
                return 502;
                
            }

        }












    }
}
