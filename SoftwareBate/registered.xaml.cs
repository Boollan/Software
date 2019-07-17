using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cute_debug
{
    /// <summary>
    /// registered.xaml 的交互逻辑
    /// </summary>
    public partial class registered : Window
    {
        public registered()
        {
            InitializeComponent();
        }
        //提交注册
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           //判断用户输入的内容是否非法
            if (Interaction.Registrationlogic(textbox_username.Text.Trim(),textbox_pwd.Password.Trim(), textbox_yespwd.Password.Trim(), textbox_mail.Text.Trim(),textbox_Verification.Text.Trim()) ==true)
            {
                //判断数据是否有这个用户
                if (message.messagetext(ClassSQL.SQLregiste(textbox_username.Text, textbox_pwd.Password, textbox_mail.Text))==100)
                {
                    //提示信息
                    
                    News.NewsText("注册成功","系统提示");
                    /*注册信息文本框清空*/
                    textbox_username.Text=null;
                    textbox_pwd.Password = null;
                    textbox_yespwd.Password = null;
                    textbox_mail.Text = null;
                    textbox_Verification.Text = null;
                    /*注册信息文本框清空 END*/




                }
                else
                {
                    /*注册信息文本框清空*/
                    textbox_username.Text = null;
                    textbox_pwd.Password = null;
                    textbox_yespwd.Password = null;
                    textbox_mail.Text = null;
                    textbox_Verification.Text = null;
                    /*注册信息文本框清空 END*/

                }
            }
            else
            { /*注册信息文本框清空*/
                textbox_username.Text = null;
                textbox_pwd.Password = null;
                textbox_yespwd.Password = null;
                textbox_mail.Text = null;
                textbox_Verification.Text = null;
                /*注册信息文本框清空 END*/
                //message.messagetext(404);
                //提示信息
                System.Windows.Forms.MessageBox.Show("您输入的注册信息 格式错误 ");
            }
        }

        //关闭线程
        private void Button_login_home_Click(object sender, RoutedEventArgs e)
        {
            //隐藏当前窗口
            this.Hide();
            //实例化窗口
            Land land = new Land();
            //显示实例化的窗口
            land.Show();
        }

        //注册邮箱获取验证码
        private void Mail_verification_Click(object sender, RoutedEventArgs e)
        {
            //调用邮箱验证码发送类 进行邮箱验证
            ClassSQL.SendEmail(this.textbox_mail.Text.Trim(), "我们已收到您的注册请求 您的验证码已在邮箱内","我们已收到您的注册请求，感谢您成为我们的一员，软件开发还在测试阶段 作者：Boollan bug反馈邮箱：wyzaoz@163.com \n"+Interaction.retpwd(),"感谢注册成为会员");
            //ClassSQL.server_key(this.textbox_mail.Text.Trim());
            //提示信息
            System.Windows.Forms.MessageBox.Show("验证码发送成功","系统提示");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
