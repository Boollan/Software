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
using System.Data.SqlClient;

namespace cute_debug
{
    /// <summary>
    /// kami_Sytem.xaml 的交互逻辑
    /// </summary>
    public partial class kami_Sytem : Window
    {
        public kami_Sytem()
        {
            InitializeComponent();
        }
        //返回菜单
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();

        }
        //结束进程
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //生成卡密
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textbox_money.Text!=""&&Convert.ToInt32(textbox_money.Text.Trim())>0)//逻辑判断
            {
                string cdk = ClassSQL.kami_generate();//储存字符串
                this.textbox_cdktext.Text = cdk;//添加字符串
                if(ClassSQL.kami_submit(cdk, this.textbox_money.Text.Trim())==true)
                {
                    System.Windows.Forms.MessageBox.Show("生成CDK成功！\n卡密："+cdk+"","系统提示");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("生成CDK失败！\n请重试","系统提示");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("请填写正确金额","系统提示");
            }
            
            
        }
    }
}
