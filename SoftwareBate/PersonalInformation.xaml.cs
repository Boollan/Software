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
    /// PersonalInformation.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInformation : Window
    {
        public PersonalInformation()
        {
            InitializeComponent();
        }

        //关闭线程
        private void Window_Closed(object sender, EventArgs e)
        {
            //结束当前应用程序
            Environment.Exit(0);
        }

        //返回
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //实例化窗口
            Home home = new Home();
            //显示实例化窗口
            home.Show();
            //隐藏当前类窗口
            this.Hide();

        }

        //加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //同步数据
            ClassSQL.SQLpersonadata(data.Username);

            //个人资料的加载
            //同步"昵称"到文本框
            this.lable_inf_name.Content = data.Name.ToString();
            //同步"邮箱"到文本框
            this.lable_inf_mail.Content = data.Mail.ToString();
            //同步"出生日期"到文本框
            this.lable_inf_birthday.Content = data.Birthday.ToString();
            //同步"技术专业学习"到文本框
            this.lable_inf_pro.Content = data.Profession.ToString();
            //同步"学校名称"到文本框
            this.lable_inf_countries.Content = data.Country.ToString();
            //同步"手机号"到文本框
            this.lable_inf_phone.Content = data.Phone.ToString();




            //
            this.name.Text = data.Name.ToString();
            this.birthday.Text = data.Birthday.ToString();
            this.mail.Text = data.Mail.ToString();
            this.por.Text = data.Profession.ToString();
            this.Country.Text = data.Country.ToString();
            this.phone.Text = data.Phone.ToString();
        }

        //个人资料编辑 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Interaction.persoadata(); 函数废弃

            if (this.edit.Content.ToString() == "编辑")
            {
                //界面美化不予注释
                this.name.IsEnabled = true;
                this.mail.IsEnabled = true;
                this.birthday.IsEnabled = true;
                this.por.IsEnabled = true;
                this.Country.IsEnabled = true;
                this.phone.IsEnabled = true;
                //界面美化不予注释

                this.lable_inf_name.Visibility = Visibility.Hidden;
                this.lable_inf_mail.Visibility = Visibility.Hidden;
                this.lable_inf_birthday.Visibility = Visibility.Hidden;
                this.lable_inf_countries.Visibility = Visibility.Hidden;
                this.lable_inf_pro.Visibility = Visibility.Hidden;
                this.lable_inf_phone.Visibility = Visibility.Hidden;
                this.edit.Content = "保存";


            }
            else
            {


                //界面美化不予注释
                this.lable_inf_name.Content = this.name.Text.ToString();
                this.lable_inf_mail.Content = this.mail.Text.ToString();
                this.lable_inf_birthday.Content = this.birthday.Text.ToString();
                this.lable_inf_pro.Content = this.por.Text.ToString();
                this.lable_inf_countries.Content = this.Country.Text.ToString();
                this.lable_inf_phone.Content = this.phone.Text.ToString();
                //界面美化不予注释

                this.lable_inf_name.Visibility = Visibility.Visible;
                this.lable_inf_mail.Visibility = Visibility.Visible;
                this.lable_inf_birthday.Visibility = Visibility.Visible;
                this.lable_inf_countries.Visibility = Visibility.Visible;
                this.lable_inf_pro.Visibility = Visibility.Visible;
                this.lable_inf_phone.Visibility = Visibility.Visible;
                //界面美化不予注释

                this.name.IsEnabled = false;
                this.mail.IsEnabled = false;
                this.birthday.IsEnabled = false;
                this.por.IsEnabled = false;
                this.Country.IsEnabled = false;
                this.phone.IsEnabled = false;
                this.edit.Content = "编辑";
                

                Interaction.personapass(this.name.Text.Trim(), this.mail.Text.Trim(), this.birthday.Text.Trim(), this.por.Text.Trim(), this.Country.Text.Trim(), this.phone.Text.Trim());//传值


                if (ClassSQL.SQLpersonaDataediting() == true)
                {
                    System.Windows.Forms.MessageBox.Show("更新成功","系统提示");
                }

            }



        }
    }
}
