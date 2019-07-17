using Mert;
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
    /// Land.xaml 的交互逻辑
    /// </summary>
    public partial class Land : Window
    {
        public Land()
        {
            InitializeComponent();
        }

        //登录
        private void Button_land_Click(object sender, RoutedEventArgs e)
        {
            //逻辑判定
            if (Interaction.Landlogin(textbox_user.Text.Trim(), textbox_passwrod.Password.Trim()))
            {
                if (message.messagetext(ClassSQL.SQLLandlogin(textbox_user.Text.Trim(), textbox_passwrod.Password.Trim()))==100)
                {
                    if (ClassSQL.is_ban(textbox_user.Text.Trim())==false)
                    {
                        if (checkbox_me.IsChecked == true)
                        {

                            SettingsUser.Default.Password = ClassSQL.Encrypt(textbox_passwrod.Password.Trim());
                            SettingsUser.Default.UserName = ClassSQL.Encrypt(textbox_user.Text.Trim());
                            SettingsUser.Default.Land_IsChecked_User = true;
                            SettingsUser.Default.Save();
                        }
                        else
                        {

                            SettingsUser.Default.Password = null;
                            SettingsUser.Default.UserName = null;
                            SettingsUser.Default.Land_IsChecked_User = false;
                            SettingsUser.Default.Save();
                        }

                        ClassSQL.SQLLandpassHome(textbox_user.Text.Trim());

                        Home home = new Home();
                        home.Show();
                        this.Hide();
                    }else
                    {
                        News.NewsText("您的账号已被封禁", "反作弊系统");

                    }
                }
                else
                {
                    News.NewsText("账号或密码错误！", "系统提示");
                }
                
            }
            else
            {
                News.NewsText("账号或密码错误！", "系统提示");

            }
        }

        //关闭线程
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //跳转注册界面
        private void Button_home_registe_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            registered registered = new registered();
            registered.Show();
        }

        //加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //创建所需数据表
            
            ClassSQL.SQLcon();//创建user数据存储表
            ClassSQL.cdk_account();//创建CDK数据存储表
            ClassSQL.SQLpersonatable();//创建资料表
            ClassSQL.user_vip_sql();//创建vip数据表
            //ClassSQL.Value_added_services();//创建增值业务物品数据表
            ClassSQL.SQL_back_system();//创建后台管理

            //验证用户是否记住密码
            if (SettingsUser.Default.Land_IsChecked_User == true && SettingsUser.Default.Password.Length > -1)
            {
                
                this.textbox_passwrod.Password = ClassSQL.Decrypt(SettingsUser.Default.Password);
                this.textbox_user.Text = ClassSQL.Decrypt(SettingsUser.Default.UserName);
                this.checkbox_me.IsChecked = Convert.ToBoolean(ClassSQL.Decrypt(SettingsUser.Default.Land_IsChecked_User.ToString()));
                ClassSQL.SQLLandpassHome(textbox_user.Text.Trim());
                this.Hide();
                Home home = new Home();
                home.Show();

            }


        }


        private void Button_resetpwd_Click(object sender, RoutedEventArgs e)
        {
            resetpwd resetpwd = new resetpwd();
            resetpwd.Show();
            this.Hide();
        }

        private void Label_pwd_Copy_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }


    }
}
