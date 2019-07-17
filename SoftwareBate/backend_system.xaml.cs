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
    /// backend_system.xaml 的交互逻辑
    /// </summary>
    public partial class backend_system : Window
    {
        public backend_system()
        {
            InitializeComponent();
        }

        //返回主菜单
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        //结束进程
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //锁定权限

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Comb_system1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (this.comb_system1.SelectedIndex > -1)
            {
                if (ClassSQL.values_back_system(this.comb_system1.SelectedIndex) == true)
                {
                    if (this.comb_system1.SelectedIndex == 0)
                    {
                        if (data.bakc_system_0_kami_root == 1)
                        {
                            this.c_kami_sytem.IsChecked = true;
                        }
                        else
                        {
                            c_kami_sytem.IsChecked = false;
                        }
                        if (data.bakc_system_0_back_system == 1)
                        {
                            this.c_back_system.IsChecked = true;
                        }
                        else
                        {
                            this.c_back_system.IsChecked = false;

                        }
                        if (data.bakc_system_0_sql_system == 1)
                        {
                            this.c_sql_system.IsChecked = true;

                        }
                        else
                        {
                            this.c_sql_system.IsChecked = false;

                        }
                    }
                    else
                    {
                        if (this.comb_system1.SelectedIndex == 1)
                        {
                            if (data.bakc_system_1_kami_root == 1)
                            {
                                this.c_kami_sytem.IsChecked = true;
                            }
                            else
                            {
                                c_kami_sytem.IsChecked = false;
                            }
                            if (data.bakc_system_1_back_system == 1)
                            {
                                this.c_back_system.IsChecked = true;
                            }
                            else
                            {
                                this.c_back_system.IsChecked = false;

                            }
                            if (data.bakc_system_1_sql_system == 1)
                            {
                                this.c_sql_system.IsChecked = true;

                            }
                            else
                            {
                                this.c_sql_system.IsChecked = false;

                            }
                        }
                        else
                        {
                            if (this.comb_system1.SelectedIndex == 2)
                            {
                                if (data.bakc_system_2_kami_root == 1)
                                {
                                    this.c_kami_sytem.IsChecked = true;
                                }
                                else
                                {
                                    c_kami_sytem.IsChecked = false;
                                }
                                if (data.bakc_system_2_back_system == 1)
                                {
                                    this.c_back_system.IsChecked = true;
                                }
                                else
                                {
                                    this.c_back_system.IsChecked = false;

                                }
                                if (data.bakc_system_2_sql_system == 1)
                                {
                                    this.c_sql_system.IsChecked = true;

                                }
                                else
                                {
                                    this.c_sql_system.IsChecked = false;

                                }
                            }
                            else
                            {
                                if (this.comb_system1.SelectedIndex == 3)
                                {
                                    if (data.bakc_system_3_kami_root == 1)
                                    {
                                        this.c_kami_sytem.IsChecked = true;
                                    }
                                    else
                                    {
                                        c_kami_sytem.IsChecked = false;
                                    }
                                    if (data.bakc_system_3_back_system == 1)
                                    {
                                        this.c_back_system.IsChecked = true;
                                    }
                                    else
                                    {
                                        this.c_back_system.IsChecked = false;

                                    }
                                    if (data.bakc_system_3_sql_system == 1)
                                    {
                                        this.c_sql_system.IsChecked = true;

                                    }
                                    else
                                    {
                                        this.c_sql_system.IsChecked = false;

                                    }
                                }
                                else
                                {
                                    if (this.comb_system1.SelectedIndex == 4)
                                    {
                                        if (data.bakc_system_4_kami_root == 1)
                                        {
                                            this.c_kami_sytem.IsChecked = true;
                                        }
                                        else
                                        {
                                            c_kami_sytem.IsChecked = false;
                                        }
                                        if (data.bakc_system_4_back_system == 1)
                                        {
                                            this.c_back_system.IsChecked = true;
                                        }
                                        else
                                        {
                                            this.c_back_system.IsChecked = false;

                                        }
                                        if (data.bakc_system_4_sql_system == 1)
                                        {
                                            this.c_sql_system.IsChecked = true;

                                        }
                                        else
                                        {
                                            this.c_sql_system.IsChecked = false;

                                        }
                                    }
                                    else
                                    {
                                        if (this.comb_system1.SelectedIndex == 5)
                                        {
                                            if (data.bakc_system_5_kami_root == 1)
                                            {
                                                this.c_kami_sytem.IsChecked = true;
                                            }
                                            else
                                            {
                                                c_kami_sytem.IsChecked = false;
                                            }
                                            if (data.bakc_system_5_back_system == 1)
                                            {
                                                this.c_back_system.IsChecked = true;
                                            }
                                            else
                                            {
                                                this.c_back_system.IsChecked = false;

                                            }
                                            if (data.bakc_system_5_sql_system == 1)
                                            {
                                                this.c_sql_system.IsChecked = true;

                                            }
                                            else
                                            {
                                                this.c_sql_system.IsChecked = false;

                                            }
                                        }
                                        else
                                        {
                                            if (this.comb_system1.SelectedIndex == 6)
                                            {
                                                if (data.bakc_system_6_kami_root == 1)
                                                {
                                                    this.c_kami_sytem.IsChecked = true;
                                                }
                                                else
                                                {
                                                    c_kami_sytem.IsChecked = false;
                                                }
                                                if (data.bakc_system_6_back_system == 1)
                                                {
                                                    this.c_back_system.IsChecked = true;
                                                }
                                                else
                                                {
                                                    this.c_back_system.IsChecked = false;

                                                }
                                                if (data.bakc_system_6_sql_system == 1)
                                                {
                                                    this.c_sql_system.IsChecked = true;

                                                }
                                                else
                                                {
                                                    this.c_sql_system.IsChecked = false;

                                                }
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }


                }
            }
        }

        //更改用户权限
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        //锁定传参
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (this.comb_system1.SelectedIndex > -1 && this.comb_system1.SelectedIndex < 7)
            {
                if (this.comb_system1.SelectedIndex == 0)
                {
                    //卡密系统
                    if (this.c_kami_sytem.IsChecked == true)
                    {
                        data.bakc_system_0_kami_root = 1;
                    }
                    else
                    {
                        data.bakc_system_0_kami_root = 0;

                    }
                    //后台系统
                    if (this.c_back_system.IsChecked == true)
                    {
                        data.bakc_system_0_back_system = 1;
                    }
                    else
                    {
                        data.bakc_system_0_back_system = 0;
                    }
                    //数据库系统
                    if (this.c_sql_system.IsChecked == true)
                    {
                        data.bakc_system_0_sql_system = 1;
                    }
                    else
                    {
                        data.bakc_system_0_sql_system = 0;
                    }
                }
                else
                {
                    if (this.comb_system1.SelectedIndex == 1)
                    {
                        //卡密系统
                        if (this.c_kami_sytem.IsChecked == true)
                        {
                            data.bakc_system_1_kami_root = 1;
                        }
                        else
                        {
                            data.bakc_system_1_kami_root = 0;

                        }
                        //后台系统
                        if (this.c_back_system.IsChecked == true)
                        {
                            data.bakc_system_1_back_system = 1;
                        }
                        else
                        {
                            data.bakc_system_1_back_system = 0;
                        }
                        //数据库系统
                        if (this.c_sql_system.IsChecked == true)
                        {
                            data.bakc_system_1_sql_system = 1;
                        }
                        else
                        {
                            data.bakc_system_1_sql_system = 0;
                        }
                    }
                    else
                    {
                        if (this.comb_system1.SelectedIndex == 2)
                        {
                            //卡密系统
                            if (this.c_kami_sytem.IsChecked == true)
                            {
                                data.bakc_system_2_kami_root = 1;
                            }
                            else
                            {
                                data.bakc_system_2_kami_root = 0;

                            }
                            //后台系统
                            if (this.c_back_system.IsChecked == true)
                            {
                                data.bakc_system_2_back_system = 1;
                            }
                            else
                            {
                                data.bakc_system_2_back_system = 0;
                            }
                            //数据库系统
                            if (this.c_sql_system.IsChecked == true)
                            {
                                data.bakc_system_2_sql_system = 1;
                            }
                            else
                            {
                                data.bakc_system_2_sql_system = 0;
                            }
                        }
                        else
                        {
                            if (this.comb_system1.SelectedIndex == 3)
                            {
                                //卡密系统
                                if (this.c_kami_sytem.IsChecked == true)
                                {
                                    data.bakc_system_3_kami_root = 1;
                                }
                                else
                                {
                                    data.bakc_system_3_kami_root = 0;

                                }
                                //后台系统
                                if (this.c_back_system.IsChecked == true)
                                {
                                    data.bakc_system_3_back_system = 1;
                                }
                                else
                                {
                                    data.bakc_system_3_back_system = 0;
                                }
                                //数据库系统
                                if (this.c_sql_system.IsChecked == true)
                                {
                                    data.bakc_system_3_sql_system = 1;
                                }
                                else
                                {
                                    data.bakc_system_3_sql_system = 0;
                                }
                            }
                            else
                            {
                                if (this.comb_system1.SelectedIndex == 4)
                                {
                                    //卡密系统
                                    if (this.c_kami_sytem.IsChecked == true)
                                    {
                                        data.bakc_system_4_kami_root = 1;
                                    }
                                    else
                                    {
                                        data.bakc_system_4_kami_root = 0;

                                    }
                                    //后台系统
                                    if (this.c_back_system.IsChecked == true)
                                    {
                                        data.bakc_system_4_back_system = 1;
                                    }
                                    else
                                    {
                                        data.bakc_system_4_back_system = 0;
                                    }
                                    //数据库系统
                                    if (this.c_sql_system.IsChecked == true)
                                    {
                                        data.bakc_system_4_sql_system = 1;
                                    }
                                    else
                                    {
                                        data.bakc_system_4_sql_system = 0;
                                    }
                                }
                                else
                                {
                                    if (this.comb_system1.SelectedIndex == 5)
                                    {
                                        //卡密系统
                                        if (this.c_kami_sytem.IsChecked == true)
                                        {
                                            data.bakc_system_5_kami_root = 1;
                                        }
                                        else
                                        {
                                            data.bakc_system_5_kami_root = 0;

                                        }
                                        //后台系统
                                        if (this.c_back_system.IsChecked == true)
                                        {
                                            data.bakc_system_5_back_system = 1;
                                        }
                                        else
                                        {
                                            data.bakc_system_5_back_system = 0;
                                        }
                                        //数据库系统
                                        if (this.c_sql_system.IsChecked == true)
                                        {
                                            data.bakc_system_5_sql_system = 1;
                                        }
                                        else
                                        {
                                            data.bakc_system_5_sql_system = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (this.comb_system1.SelectedIndex == 6)
                                        {
                                            //卡密系统
                                            if (this.c_kami_sytem.IsChecked == true)
                                            {
                                                data.bakc_system_6_kami_root = 1;
                                            }
                                            else
                                            {
                                                data.bakc_system_6_kami_root = 0;

                                            }
                                            //后台系统
                                            if (this.c_back_system.IsChecked == true)
                                            {
                                                data.bakc_system_6_back_system = 1;
                                            }
                                            else
                                            {
                                                data.bakc_system_6_back_system = 0;
                                            }
                                            //数据库系统
                                            if (this.c_sql_system.IsChecked == true)
                                            {

                                                data.bakc_system_6_sql_system = 1;
                                            }
                                            else
                                            {
                                                data.bakc_system_6_sql_system = 0;
                                            }

                                        }
                                        else
                                        {
                                           
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (ClassSQL.update_back_system(this.comb_system1.SelectedIndex) == true)
                {

                }
                else
                {

                }
            }
        }

        //用户权限
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Authority_allocation.Visibility = Visibility.Visible;

        }
    }
}
