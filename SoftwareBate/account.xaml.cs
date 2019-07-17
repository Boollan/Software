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
using System.Windows.Forms;

namespace cute_debug
{
    /// <summary>
    /// account.xaml 的交互逻辑
    /// </summary>
    public partial class account : Window
    {
        public account()
        {
            InitializeComponent();
        }

        //关闭线程
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //账号or密码
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Play.Visibility = Visibility.Hidden;
            this.Grid_Play.IsEnabled = false;
            this.Grid_Password_change.Visibility = Visibility.Visible;
            this.Grid_Password_change.IsEnabled = true;
            this.Grid_vip.Visibility = Visibility.Hidden;
            
        }

        //密码修改 按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.textbox_account_password.Password.Trim()==this.textbox_account_Confirmpassword.Password.Trim())
            {
                if(ClassSQL.Passowrd_change(data.Username, this.textbox_account_Thepassword.Password.Trim(), this.textbox_account_password.Password.Trim()))
                {
                    System.Windows.Forms.MessageBox.Show("密码更改成功","温馨提示");
                    this.textbox_account_Thepassword.Password="";
                    this.textbox_account_password.Password = "";
                    this.textbox_account_Confirmpassword.Password = "";
                    ClassSQL.SQLLandpassHome(data.Username);

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("密码更改失败","温馨提示");
                    this.textbox_account_Thepassword.Password = "";
                    this.textbox_account_password.Password = "";
                    this.textbox_account_Confirmpassword.Password = "";
                    ClassSQL.SQLLandpassHome(data.Username);

                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("两次新密码不一致","温馨提示");
            }

        }

        //账号充值
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Grid_Password_change.Visibility = Visibility.Hidden;
            this.Grid_Password_change.IsEnabled = false;
            this.Grid_Play.Visibility = Visibility.Visible;
            this.Grid_Play.IsEnabled = true;
            this.Grid_vip.Visibility = Visibility.Hidden;


        }

        //加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_data();
        }

        //加载数据
        private void Load_data()
        {
            //加载用户名到指定菜单
            this.textbox_account_username.Text = data.Username.ToString();//账号密码 的用户名
            this.textbox_paly_username.Text = data.Username.ToString();//账号充值的 用户名
            this.textbox_paly_currency.Text = data.Currency.ToString();//获取账号余额

            //加载到开通VIP菜单
            this.textbox_vip_username.Text = data.Username.ToString();
            this.textbox_vip_level.Text = data.Vip.ToString();
            this.textbox_vip_money.Text = data.Currency.ToString();

            //加载数据
            ClassSQL.SQLLandpassHome(data.Username);
        }

        //卡密充值
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(ClassSQL.cdk_pay(this.textbox_cdk.Text.Trim(), this.textbox_paly_username.Text.Trim())==true)//卡密充值
            {
                System.Windows.Forms.MessageBox.Show("充值成功","系统提示");
                this.textbox_cdk.Text = "";
                Load_data();


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("充值失败","系统提示");
                this.textbox_cdk.Text = "";
                Load_data();

            }
        }

        //返回主菜单
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        //开通会员界面
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Grid_vip.Visibility = Visibility.Visible;
            Grid_Play.Visibility = Visibility.Hidden;
            Grid_Password_change.Visibility = Visibility.Hidden;
        }

        //开通会员 /*开发中*/
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int combobox_index= this.combobox_vip_time.SelectedIndex;
            int money =20;
            int dt = 0;
            
            if (combobox_index==0)
            {
                money = 20;
                dt = 30;
            }
            else
            {
                if (combobox_index == 1)
                {
                    money = 60;
                    dt = 90;

                }
                else
                {
                    if (combobox_index == 2)
                    {
                        money = 150;
                        dt = 180;
                    }
                    else
                    {
                        if (combobox_index == 3)
                        {
                            money = 170;
                            dt = 280;
                        }
                        else
                        {
                            if (combobox_index == 4)
                            {
                                money = 198;
                                dt = 365;
                            }
                            else
                            {
                                if (combobox_index == 5)
                                {
                                    money = 240;
                                    dt = 365;
                                }
                                else
                                {
                                    if (combobox_index >5&&combobox_index<0)
                                    {
                                        money = 99999;
                                        dt = 0;
                                    }
                                    else
                                    {
                                        System.Windows.Forms.MessageBox.Show("错误","致命错误");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Convert.ToInt32(ClassSQL.user_money(data.Username))>=money)
            {
                if(ClassSQL.VIP_open(data.Username,dt,money.ToString())==true)
                {
                    System.Windows.Forms.MessageBox.Show("感谢您开通VIP\n到期时间为："+DateTime.Now.AddDays(dt)+"","系统提示(如果VIP没到期请以查询时间为准)");
                    Load_data();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("开通VIP失败\n不扣除余额","系统提示");
                    Load_data();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("开通VIP失败\n您的余额为："+ClassSQL.user_money(data.Username));
                Load_data();
            }
        }
    }
}
