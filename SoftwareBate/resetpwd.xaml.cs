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
    /// resetpwd.xaml 的交互逻辑
    /// </summary>
    public partial class resetpwd : Window
    {
        public resetpwd()
        {
            InitializeComponent();
        }

        //获取中验证码
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassSQL.SendEmail(this.mailend.Text.Trim(), "我们已收到您的密码重置请求 您的验证码已在邮件内容中请注意查收", "我们已收到您的密码重置请求,您的验证码已在邮件内容中请注意查收.软件开发还在测试阶段 作者：Boollan bug反馈邮箱：wyzaoz@163.com \n" + Interaction.retpwd(),"重置密码");
            mailer.IsEnabled = true;
            res_yes.IsEnabled = true;
            System.Windows.Forms.MessageBox.Show("重置邮箱发送成功\n请查看邮箱","系统提示");

        }

        //验证验证码是否正确
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Interaction.verification(this.mailer.Text.Trim())==true)
            {
                this.lable_username.Visibility = Visibility.Visible;
                this.lable_password.Visibility = Visibility.Visible;
                this.username.Visibility = Visibility.Visible;
                this.passsword.Visibility = Visibility.Visible;
                this.yes.Visibility = Visibility.Visible;
                this.mailend.IsEnabled = false;
                this.username.Text = ClassSQL.mail_user(this.mailend.Text.Trim());
            }
            
        }

        //界面切换
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Land land = new Land();
            land.Show();
            this.Hide();
        }

        //重置密码
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            if (ClassSQL.Password_reset(this.username.Text.Trim(), this.passsword.Text.Trim())==true)
            {
                System.Windows.Forms.MessageBox.Show("重置密码成功");
                 this.lable_username.Visibility = Visibility.Hidden;
                this.lable_password.Visibility = Visibility.Hidden;
                this.username.Visibility = Visibility.Hidden;
                this.passsword.Visibility = Visibility.Hidden;
                this.yes.Visibility = Visibility.Hidden;
                this.mailend.IsEnabled = true;
                
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("重置密码失败");
            }
            

        }

        //结束线程
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
