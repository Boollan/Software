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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mert;

namespace cute_debug
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            {
            }
        }

        //关闭线程
        private  void HOME_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
            
            
        }

        //加载
        private void HOME_Loaded(object sender, RoutedEventArgs e)
        {
            /*加载用户信息*/
            textbox_user.Text = data.Username;
            textbox_gred.Text = data.Grade.ToString();
            textbox_currency.Text = data.Currency.ToString();
            textbox_GM.Text = data.Administrator.ToString();
            textbox_vip.Text = data.Vip.ToString();
            /**/

            //验证用户是否具有权限访问内容
            int[] is_root = ClassSQL.is_root(data.Username, data.Administrator);

            if(is_root[0]==1)
            {
                this.kami_system.IsEnabled = true;
                if (is_root[1]==1)
                {
                    this.back_system.IsEnabled = true;
                    if (is_root[2]==1)
                    {
                        this.sql_system.IsEnabled = true;
                    }
                    else
                    {
                        sql_system.IsEnabled = false;
                    }
                }
                else
                {
                    this.back_system.IsEnabled = false;
                    if (is_root[2] == 1)
                    {
                        this.sql_system.IsEnabled = true;
                    }
                    else
                    {
                        sql_system.IsEnabled = false;
                    }
                }
            }
            else
            {

                this.kami_system.IsEnabled = false;
                if (is_root[1] == 1)
                {
                    this.back_system.IsEnabled = true;
                    if (is_root[2] == 1)
                    {
                        this.sql_system.IsEnabled = true;
                    }
                    else
                    {
                        this.sql_system.IsEnabled = false;
                    }
                }
                else
                {
                    this.back_system.IsEnabled = false;
                    if (is_root[2] == 1)
                    {
                        this.sql_system.IsEnabled = true;
                    }
                    else
                    {
                        this.sql_system.IsEnabled = false;

                    }
                }
            }


        }

        //个人资料
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PersonalInformation personalInformation = new PersonalInformation();
            personalInformation.Show();
        }

        //账号系统
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            account account = new account();
            account.Show();
        }

        //卡密系统
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            kami_Sytem kami_Sytem = new kami_Sytem();
            kami_Sytem.Show();
            this.Hide();

        }

        //后台管理系统
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            backend_system backend_System = new backend_system();
            backend_System.Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //清除用户信息
            data.Username = null;
            data.Grade = 0;
            data.Currency = 0;
            data.Administrator = 0;
            data.Vip = 0;

            //清楚文本框
            textbox_user.Text = null;
            textbox_gred.Text = null;
            textbox_currency.Text = null;
            textbox_GM.Text = null;
            textbox_vip.Text = null;

            //清除本地保存数据
            
            SettingsUser.Default.Password = null;
            SettingsUser.Default.UserName = null;
            SettingsUser.Default.Land_IsChecked_User = false;
            SettingsUser.Default.Save();
            Land land = new Land();
            this.Hide();
            land.Show();


        }
    }
}
