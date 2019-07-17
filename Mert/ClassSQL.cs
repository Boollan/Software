using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.IO;

//using System.Windows.Forms;

namespace cute_debug
{
    public class ClassSQL
    {

        /*连接数据库参数*/
        private static readonly string SQL_Server = ".";
        private static readonly string SQL_User = "sa";
        private static readonly string SQL_Pwd = "3838538+.";
        private static readonly string SQL_Database = "Boollan";
        //连接字符串
        private static readonly string sqlconstr = "Server=" + SQL_Server + ";user=" + SQL_User + ";pwd=" + SQL_Pwd + ";database=" + SQL_Database + "";


        // 设置发送方的邮件信息,例如使用网易的smtp
        private static readonly string smtpServer = "smtp.163.com"; //SMTP服务器
        private static readonly string mailFrom = "wyzaoz@163.com"; //登陆用户名
        private static readonly string userPassword = "xiaowei123";//登陆密码


        //创建用户信息表
        public static void SQLcon()
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                try
                {
                    sqlcon.Open();
                    // 判断是否存在数据表
                    SqlCommand cmd = new SqlCommand("select * from information_schema.TABLES where Table_NAME ='usertable'", sqlcon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == false)//返回值为true，存在，false不存在（SqlDataReader 的HasRows ，判断是否有数据）
                    {
                        //创建用户表
                        string sql_user_table = "create table usertable(id int identity(1,1) primary key,username varchar(40) not null,pwd varchar(40) not null,mail varchar(40) not null,administrator varchar(40) not null,currency varchar(40) not null,Paypassowrd varchar(10),grade varchar(10),vip varchar(10),is_ban int)";
                        SqlCommand sqlcom = new SqlCommand(sql_user_table, sqlcon);
                        reader.Close();
                        sqlcom.ExecuteNonQuery();
                        sqlcom.Clone();
                        //该表不存在
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
            }
        }

        /// <summary>
        /// 提交注册信息
        /// </summary>
        /// <param name="username">要注册的用户名</param>
        /// <param name="password">要注册的密码</param>
        /// <param name="mail">要注册的邮箱</param>
        /// <returns></returns>
        //提交注册信息
        public static int SQLregiste(string username, string password, string mail)
        {

            password=MD5Encrypt64(password);

            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();

                SqlCommand sqlcomselect = new SqlCommand("select * from usertable where username='" + username + "'", sqlcon);
                SqlDataReader sqlData = sqlcomselect.ExecuteReader();
                if (sqlData.Read() == false)
                {
                    sqlData.Close();
                    SqlCommand sqlcomselect1 = new SqlCommand("select * from usertable where mail='" + mail + "'", sqlcon);
                    SqlDataReader sqlData1 = sqlcomselect1.ExecuteReader();
                    if (sqlData1.Read() == false)
                    {

                        sqlData1.Close();

                        //插入基本信息数据
                        SqlCommand com = new SqlCommand("insert usertable(username,pwd,mail,administrator,currency,grade,vip,is_ban)values('" + username + "','" + password + "','" + mail + "','0','0','0','0',0)", sqlcon);
                        com.ExecuteNonQuery();
                        //添加个人默认资料表
                        SqlCommand sqlCommand = new SqlCommand("insert information(username,name,mail,birthday,profession,country,phone)values('" + username + "',' ',' ',' ',' ',' ','')", sqlcon);
                        sqlCommand.ExecuteNonQuery();
                        //添加VIP默认数据表
                        DateTime dateTime_vip = new DateTime(1999, 1, 1, 00, 00, 00);
                        SqlCommand sqlcom_vip = new SqlCommand("insert user_vip(username,vip_level,vip_Expiration_time)values('" + username + "','0','" + dateTime_vip.ToLongTimeString() + "')", sqlcon);
                        sqlcom_vip.ExecuteNonQuery();
                        return 100;
                    }
                    else
                    {
                        return 503;
                    }
                }
                else
                {
                    return 503;

                }
            }
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户的密码</param>
        /// <returns></returns>
        //登录验证
        public static int SQLLandlogin(string username, string password)
        {
            //MD5加密
            password = MD5Encrypt64(password);


            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + username + "'", sqlcon);
                SqlDataReader sqlData = sqlcom.ExecuteReader();
                if (sqlData.Read() == true)
                {
                    string nameuser = sqlData.GetString(sqlData.GetOrdinal("username"));
                    string pwd = sqlData.GetString(sqlData.GetOrdinal("pwd"));
                    if (username == nameuser)
                    {
                        if (password == pwd)
                        {
                            return 100;
                        }
                        else
                        {
                            return 404;
                        }
                    }
                    else
                    {
                        return 502;
                    }
                }
                else
                {
                    return 502;
                }
            }
        }

        /// <summary>
        /// 获取用户名下的数据同步
        /// </summary>
        /// <param name="username"></param>
        //数据库信息信息传递到HOME
        public static void SQLLandpassHome(string username)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + username + "' ", sqlcon);
                SqlDataReader sqlData = sqlcom.ExecuteReader();
                if (sqlData.Read() == true)
                {
                    string sqlusername = sqlData.GetString(sqlData.GetOrdinal("username"));
                    string sqlgrade = sqlData.GetString(sqlData.GetOrdinal("grade"));
                    string sqlcurrency = sqlData.GetString(sqlData.GetOrdinal("currency"));
                    int sqladministrator =Convert.ToInt32(sqlData.GetString(sqlData.GetOrdinal("administrator")));
                    string sqlvip = sqlData.GetString(sqlData.GetOrdinal("vip"));
                    sqlcon.Close();
                    data.Username = sqlusername;
                    data.Grade = Convert.ToInt32(sqlgrade);
                    data.Currency = Convert.ToInt32(sqlcurrency);
                    data.Administrator = sqladministrator;
                    data.Vip = Convert.ToInt32(sqlvip);



                }
            }
        }

        //个人资料表创建
        public static void SQLpersonatable()
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                // 判断是否存在数据表
                SqlCommand cmd = new SqlCommand("select * from information_schema.TABLES where Table_NAME ='information'", sqlcon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == false)//返回值为true，存在，false不存在（SqlDataReader 的HasRows ，判断是否有数据）
                {
                    //创建用户表
                    string sql_user_information = "create table information(id int identity(1,1) primary key,username varchar(40) not null,name varchar(40),mail varchar(40),birthday varchar(40),profession varchar(40),country varchar(50),phone varchar(20))";
                    SqlCommand sqlcom = new SqlCommand(sql_user_information, sqlcon);
                    reader.Close();
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Clone();
                    //该表不存在
                }
            }
        }

        /// <summary>
        /// 查询用户名的个人资料
        /// </summary>
        /// <param name="username">要获取的用户名</param>
        //个人资料查询
        public static void SQLpersonadata(string username)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("select * from information where username='" + username.ToString() + "' ", sqlcon);
                SqlDataReader sqlData = sqlcom.ExecuteReader();
                if (sqlData.Read() == true)
                {
                    string sqlname = sqlData.GetString(sqlData.GetOrdinal("name"));
                    string sqlmail = sqlData.GetString(sqlData.GetOrdinal("mail"));
                    string sqlbirthday = sqlData.GetString(sqlData.GetOrdinal("birthday"));
                    string sqlprofession = sqlData.GetString(sqlData.GetOrdinal("profession"));
                    string sqlcountry = sqlData.GetString(sqlData.GetOrdinal("country"));
                    string sqlphone = sqlData.GetString(sqlData.GetOrdinal("phone"));
                    sqlcon.Close();

                    data.Name = sqlname;
                    data.Mail = sqlmail;
                    data.Birthday = sqlbirthday;
                    data.Profession = sqlprofession;
                    data.Country = sqlcountry;
                    data.Phone = sqlphone;
                }






            }
        }

        //个人资料编辑
        public static bool SQLpersonaDataediting()
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcomname = new SqlCommand("update information set name='" + data.Name.ToString() + "' where username='" + data.Username.ToString() + "'", sqlcon);
                SqlCommand sqlcommail = new SqlCommand("update information set mail='" + data.Mail.ToString() + "' where username='" + data.Username.ToString() + "'", sqlcon);
                SqlCommand sqlcombirthday = new SqlCommand("update information set birthday='" + data.Birthday.ToString() + "' where username='" + data.Username.ToString() + "'", sqlcon);
                SqlCommand sqlcomprofession = new SqlCommand("update information set profession='" + data.Profession.ToString() + "' where username='" + data.Username.ToString() + "'", sqlcon);
                SqlCommand sqlcomcountry = new SqlCommand("update information set country='" + data.Country.ToString() + "' where username='" + data.Username.ToString() + "'", sqlcon);
                SqlCommand sqlcomphone = new SqlCommand("update information set phone='" + data.Phone.ToString() + "' where username='" + data.Username.ToString() + "'", sqlcon);
                sqlcomname.ExecuteNonQuery();
                sqlcommail.ExecuteNonQuery();
                sqlcombirthday.ExecuteNonQuery();
                sqlcomprofession.ExecuteNonQuery();
                sqlcomcountry.ExecuteNonQuery();
                sqlcomphone.ExecuteNonQuery();
                return true;


            }
        }

        //邮箱发送
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">要发送的邮箱</param>
        /// <param name="mailSubject">邮箱主题</param>
        /// <param name="mailContent">邮箱内容</param>
        /// <param name="mailname">邮件名称</param>
        /// <returns>返回发送邮箱的结果</returns>
        public static bool SendEmail(string mailTo, string mailSubject, string mailContent, string mailname)
        {
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置       
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人
            mailMessage.Subject = mailSubject;//主题
            mailMessage.Body = mailContent;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级
            mailMessage.From = new MailAddress(mailname + "<wyzaoz@163.com>");

            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                return true;
            }
            catch (SmtpException e)
            {
                 
                System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取邮箱下的用户名
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        //获取邮箱的当前用户名
        public static string mail_user(string mail)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("select * from usertable where mail='" + mail + "'", sqlcon);
                SqlDataReader sqlData = sqlcom.ExecuteReader();
                if (sqlData.Read() == true)
                {
                    string username_reg = sqlData.GetString(sqlData.GetOrdinal("username"));

                    return username_reg;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="username">重置的用户名</param>
        /// <param name="pwd">新的密码</param>
        /// <returns></returns>
        //重置密码
        public static bool Password_reset(string username, string pwd)
        {

            pwd = MD5Encrypt64(pwd);

            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + username + "'", sqlcon);
                SqlDataReader sqlData = sqlcom.ExecuteReader();
                if (sqlData.Read() == true)
                {
                    sqlcom.Clone();
                    sqlData.Close();

                    SqlCommand sqlCommand = new SqlCommand("update usertable set pwd='" + pwd + "' where username='" + username + "'", sqlcon);
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //创建CDK卡密存储数据库
        public static void cdk_account()
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                // 判断是否存在数据表
                SqlCommand cmd = new SqlCommand("select * from information_schema.TABLES where Table_NAME ='cdktable'", sqlcon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == false)//返回值为true，存在，false不存在（SqlDataReader 的HasRows ，判断是否有数据）
                {
                    //创建cdk表
                    string sql_cdk_table = "create table cdktable(id int identity(1,1) primary key,cdk varchar(40) not null,effective varchar(40) not null,money varchar(40) not null)";
                    SqlCommand sqlcom = new SqlCommand(sql_cdk_table, sqlcon);
                    reader.Close();
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Clone();
                    //该表不存在
                }
            }
        }
        /// <summary>
        /// 验证cdk是否有效
        /// </summary>
        /// <param name="cdk">CDK</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        //验证CDK 
        public static bool cdk_pay(string cdk, string username)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("select * from cdktable where cdk='" + cdk + "'", sqlcon);
                SqlDataReader sqlData = sqlcom.ExecuteReader();
                if (sqlData.Read())
                {
                    string payl_cdk = sqlData.GetString(sqlData.GetOrdinal("cdk"));//获取卡密
                    string payl_effective = sqlData.GetString(sqlData.GetOrdinal("effective"));//获取是否有效
                    string payl_money = sqlData.GetString(sqlData.GetOrdinal("money"));//获取卡密金额

                    if (payl_effective == "1")//判断卡密是否有效
                    {
                        SqlCommand sqlcom_effective = new SqlCommand("update cdktable set effective='0' where cdk='" + payl_cdk + "'", sqlcon);//更改卡密为已使用
                        sqlData.Close();
                        sqlcom_effective.ExecuteNonQuery();
                        if (currency_Play(username, payl_money) == true)
                        {
                            SQLLandpassHome(data.Username.ToString());
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 充值账号余额
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="currency">余额</param>
        public static bool currency_Play(string username, string currency)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();//建立连接
                SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + username + "'", sqlcon);//查询指定用户名
                SqlDataReader sqlData = sqlcom.ExecuteReader();//存储数据
                if (sqlData.Read())//判断是否有这个用户
                {
                    string currency_temp = sqlData.GetString(sqlData.GetOrdinal("currency"));//获取用户当前余额
                    string curency_play = Convert.ToString(Convert.ToInt32(currency_temp) + Convert.ToInt32(currency));//余额合并
                    SqlCommand sqlcom_currency = new SqlCommand("update usertable set currency ='" + curency_play + "' where username='" + username + "'", sqlcon);
                    sqlData.Close();
                    sqlcom_currency.ExecuteNonQuery();//执行
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        //修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="passowrd_low">旧密码</param>
        /// <param name="passowrd">新密码</param>
        /// <returns></returns>
        public static bool Passowrd_change(string username, string passowrd_low, string passowrd)
        {
            passowrd_low=MD5Encrypt64(passowrd_low);
            passowrd = MD5Encrypt64(passowrd);


            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))//连接数据库
            {
                sqlcon.Open();//建立连接
                SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + username + "'", sqlcon);//查询用户
                SqlDataReader sqlData = sqlcom.ExecuteReader();//存储数据
                if (sqlData.Read() == true)
                {
                    string pwd = sqlData.GetString(sqlData.GetOrdinal("pwd"));//获取密码
                    if (pwd == passowrd_low)//验证旧密码
                    {
                        sqlcom.Clone();
                        sqlData.Close();
                        SqlCommand sqlcom_passowd = new SqlCommand("update usertable set pwd ='" + passowrd + "' where username='" + username + "'", sqlcon);
                        sqlcom_passowd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //卡密提交
        /// <summary>
        /// 卡密提交
        /// </summary>
        /// <param name="cdk">卡密序列号</param>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public static bool kami_submit(string cdk, string money)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))//连接字符串
            {
                sqlcon.Open();//建立连接
                SqlCommand sqlcom = new SqlCommand("select * from cdktable where cdk='" + cdk + "'", sqlcon);//查询指定内容
                SqlDataReader sqlData = sqlcom.ExecuteReader();//存储数据
                if (sqlData.Read() == false)//判断是否存在这张卡密
                {
                    sqlData.Close();
                    //插入cdk指定内容
                    SqlCommand sqlcom_insert = new SqlCommand("insert cdktable(cdk,effective,money)values('" + cdk + "','1','" + money + "')", sqlcon);
                    sqlcom_insert.ExecuteNonQuery();//建立连接并执行
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        static string result;
        /// <summary>
        /// 卡密生成
        /// </summary>
        /// <returns></returns>
        public static string kami_generate()
        {
            //35个字符
            string str = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random r = new Random();
            result = string.Empty;
            //生成一个8位长的随机字符，具体长度可以自己更改            
            for (int i = 0; i < 32; i++)
            {
                //这里下界是0，随机数可以取到，上界应该是35，因为随机数取不到上界，也就是最大74，符合我们的题意
                int m = r.Next(0, 36);
                string s = str.Substring(m, 1);
                result += s;
            }
            return result;//返回字符串
        }

        /// <summary>
        /// 余额开通VIP
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="time">开通的天数</param>
        public static bool VIP_open(string username, int day_vip, string money)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();//建立连接
                SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + username + "'", sqlcon);//查询指定用户名
                SqlDataReader sqlData = sqlcom.ExecuteReader();//存储数据
                if (sqlData.Read())//判断是否有这个用户
                {
                    string currency_temp = sqlData.GetString(sqlData.GetOrdinal("currency"));//获取用户当前余额
                    sqlData.Close();//关闭上一个

                    SqlCommand sqlcomd = new SqlCommand("select * from user_vip where username = '"+username+"'", sqlcon);
                    SqlDataReader sqldata_ = sqlcomd.ExecuteReader();
                    if (sqldata_.Read())//查询VIP表
                    {
                        
                        DateTime vip_date = sqldata_.GetDateTime(sqldata_.GetOrdinal("vip_Expiration_time"));//获取日期
                        sqldata_.Close();
                        if (DateTime.Now < vip_date)//判断VIP是否过期 如果没过期就追加VIP时间 如果过期就获取当前日期
                        {


                            if (Convert.ToInt32(currency_temp) >= Convert.ToInt32(money))
                            {
                                string curency_play = Convert.ToString(Convert.ToInt32(currency_temp) - Convert.ToInt32(money));//余额合并
                                SqlCommand sqlcom_currency = new SqlCommand("update usertable set currency ='" + curency_play + "' where username='" + username + "'", sqlcon);
                                sqlData.Close();
                                sqlcom_currency.ExecuteNonQuery();//执行

                                string vip = "1";
                                //更改VIP
                                SqlCommand sqlcom_vip = new SqlCommand("update user_vip set vip_level='" + vip + "' where username='" + username + "'", sqlcon);
                                SqlCommand sqlcom_vip_time = new SqlCommand("update user_vip set vip_Expiration_time='" + vip_date.AddDays(day_vip) + "' where username='" + username + "'", sqlcon);
                                SqlCommand sqlcom_usertable_vip = new SqlCommand("update usertable set vip='" + vip + "' where username='" + username + "'", sqlcon);

                                sqlcom_vip_time.ExecuteNonQuery();//执行SQL语句
                                sqlcom_vip.ExecuteNonQuery();//执行SQL语句
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(currency_temp) >= Convert.ToInt32(money))
                            {
                                string curency_play = Convert.ToString(Convert.ToInt32(currency_temp) - Convert.ToInt32(money));//余额合并
                                SqlCommand sqlcom_currency = new SqlCommand("update usertable set currency ='" + curency_play + "' where username='" + username + "'", sqlcon);
                                sqlData.Close();
                                sqlcom_currency.ExecuteNonQuery();//执行

                                string vip = "1";
                                //更改VIP
                                SqlCommand sqlcom_vip = new SqlCommand("update user_vip set vip_level='" + vip + "' where username='" + username + "'", sqlcon);
                                SqlCommand sqlcom_vip_time = new SqlCommand("update user_vip set vip_Expiration_time='" + DateTime.Now.AddDays(day_vip) + "' where username='" + username + "'", sqlcon);
                                SqlCommand sqlcom_usertable_vip = new SqlCommand("update usertable set vip='" + vip + "' where username='" + username + "'", sqlcon);

                                sqlcom_vip_time.ExecuteNonQuery();//执行SQL语句
                                sqlcom_vip.ExecuteNonQuery();//执行SQL语句
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                    
                   
                }
                else
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// 创建vip数据库
        /// </summary>
        public static void user_vip_sql()
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                // 判断是否存在数据表
                SqlCommand cmd = new SqlCommand("select * from information_schema.TABLES where Table_NAME ='user_vip'", sqlcon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == false)//返回值为true，存在，false不存在（SqlDataReader 的HasRows ，判断是否有数据）
                {
                    //创建用户表
                    string sql_user_information = "create table user_vip(id int identity(1,1) primary key,username varchar(40) not null,vip_level varchar(10) not null,vip_Expiration_time datetime not null)";
                    SqlCommand sqlcom = new SqlCommand(sql_user_information, sqlcon);
                    reader.Close();
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Clone();
                    //该表不存在
                }
            }
        }

        //获取数据库时间 
        public static DateTime GetDateTimeFromSQL()
        {
            SqlConnection conn = new SqlConnection(sqlconstr);
            conn.Open();
            string sql = "select getdate()";
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader reader = comm.ExecuteReader();
            DateTime dt;
            if (reader.Read())
            {
                dt = (DateTime)reader[0];
                conn.Close();
                return dt;
            }
            conn.Close();
            return DateTime.MinValue;
        }

        /// <summary>
        /// 创建增值业务表
        /// </summary>
        public static void Value_added_services()
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                // 判断是否存在数据表
                SqlCommand cmd = new SqlCommand("select * from information_schema.TABLES where Table_NAME ='Value_added_services'", sqlcon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == false)//返回值为true，存在，false不存在（SqlDataReader 的HasRows ，判断是否有数据）
                {
                    //创建用户表
                    string sql_user_information = "create table Value_added_services(id int identity(1,1) primary key,goodsname varchar(40) not null,money varchar(10) not null,good_Expiration_time varchar(10) not null)";
                    SqlCommand sqlcom = new SqlCommand(sql_user_information, sqlcon);
                    reader.Close();
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Clone();
                    //该表不存在
                }
            }
        }

        /// <summary>
        /// 增值业务
        /// </summary>
        /// <param name="goodsname">商品名称</param>
        /// <param name="money">商品价格</param>
        /// <param name="good_time">商品的有效时间</param>
        public static bool insert_Value_added_services(string goodsname, string money, string good_time)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("insert Value_added_services(goodsname,money,good_Expiration_time)values('" + goodsname + "','" + money + "','" + good_time + "')", sqlcon);
                sqlcom.ExecuteNonQuery();
                return true;
            }

        }

        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static string user_money(string username)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();//建立连接
                SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + username + "'", sqlcon);//查询指定用户名
                SqlDataReader sqlData = sqlcom.ExecuteReader();//存储数据
                if (sqlData.Read())//判断是否有这个用户
                {
                    string currency_temp = sqlData.GetString(sqlData.GetOrdinal("currency"));//获取用户当前余额
                    //关闭连接
                    sqlData.Close();
                    //返回查询到的值
                    return currency_temp;
                }
                else
                {
                    //返回值
                    return "0";
                }

            }
        }

        /// <summary>
        /// 创建系统管理表
        /// </summary>
        public static void SQL_back_system()
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                // 判断是否存在数据表
                SqlCommand cmd = new SqlCommand("select * from information_schema.TABLES where Table_NAME ='Table_back_system'", sqlcon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == false)//返回值为true，存在，false不存在（SqlDataReader 的HasRows ，判断是否有数据）
                {
                    //创建用户表
                    string Table_back_system = "create table Table_back_system(id int identity(1,1) primary key,root int not null,kami_root int not null,back_system int not null,sql_system int not null)";
                    SqlCommand sqlcom = new SqlCommand(Table_back_system, sqlcon);
                    reader.Close();
                    sqlcom.ExecuteNonQuery();

                    //插入数据表
                    //0表示有无权限 1表示有权限
                    SqlCommand sqlcoM_insert0 = new SqlCommand("insert Table_back_system(root,kami_root,back_system,sql_system)values(0,0,0,0)", sqlcon);
                    SqlCommand sqlcoM_insert1 = new SqlCommand("insert Table_back_system(root,kami_root,back_system,sql_system)values(1,1,0,0)", sqlcon);
                    SqlCommand sqlcoM_insert2 = new SqlCommand("insert Table_back_system(root,kami_root,back_system,sql_system)values(2,1,0,0)", sqlcon);
                    SqlCommand sqlcoM_insert3 = new SqlCommand("insert Table_back_system(root,kami_root,back_system,sql_system)values(3,1,1,0)", sqlcon);
                    SqlCommand sqlcoM_insert4 = new SqlCommand("insert Table_back_system(root,kami_root,back_system,sql_system)values(4,1,1,0)", sqlcon);
                    SqlCommand sqlcoM_insert5 = new SqlCommand("insert Table_back_system(root,kami_root,back_system,sql_system)values(5,1,1,0)", sqlcon);
                    SqlCommand sqlcoM_root6 = new SqlCommand("insert Table_back_system(root,kami_root,back_system,sql_system)values(6,1,1,1)", sqlcon);
                    sqlcoM_insert0.ExecuteNonQuery();
                    sqlcoM_insert1.ExecuteNonQuery();
                    sqlcoM_insert2.ExecuteNonQuery();
                    sqlcoM_insert3.ExecuteNonQuery();
                    sqlcoM_insert4.ExecuteNonQuery();
                    sqlcoM_insert5.ExecuteNonQuery();
                    sqlcoM_root6.ExecuteNonQuery();


                    sqlcom.Clone();
                    //该表不存在
                }
            }
        }

        /// <summary>
        /// 查询后台管理权限
        /// </summary>
        /// <param name="itemindex">权限级别</param>
        /// <returns>如果为true那么成功</returns>
        public static bool values_back_system(int itemindex)
        {
            using (SqlConnection sqlcon =new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                SqlCommand sqlcom_rootinex = new SqlCommand("select * from Table_back_system where root='"+itemindex+"'", sqlcon);
                SqlDataReader sqlData = sqlcom_rootinex.ExecuteReader();
                if (sqlData.Read() == true)
                {
                    if (itemindex==0)
                    {
                        data.bakc_system_0_kami_root = sqlData.GetInt32(sqlData.GetOrdinal("kami_root"));
                        data.bakc_system_0_back_system = sqlData.GetInt32(sqlData.GetOrdinal("back_system"));
                        data.bakc_system_0_sql_system = sqlData.GetInt32(sqlData.GetOrdinal("sql_system"));
                        return true;
                    }
                    else
                    {
                        if (itemindex == 1)
                        {
                            data.bakc_system_1_kami_root = sqlData.GetInt32(sqlData.GetOrdinal("kami_root"));
                            data.bakc_system_1_back_system = sqlData.GetInt32(sqlData.GetOrdinal("back_system"));
                            data.bakc_system_1_sql_system = sqlData.GetInt32(sqlData.GetOrdinal("sql_system"));
                            return true;
                        }
                        else
                        {
                            if (itemindex == 2)
                            {
                                data.bakc_system_2_kami_root = sqlData.GetInt32(sqlData.GetOrdinal("kami_root"));
                                data.bakc_system_2_back_system = sqlData.GetInt32(sqlData.GetOrdinal("back_system"));
                                data.bakc_system_2_sql_system = sqlData.GetInt32(sqlData.GetOrdinal("sql_system"));
                                return true;
                            }
                            else
                            {
                                if (itemindex == 3)
                                {
                                    data.bakc_system_3_kami_root = sqlData.GetInt32(sqlData.GetOrdinal("kami_root"));
                                    data.bakc_system_3_back_system = sqlData.GetInt32(sqlData.GetOrdinal("back_system"));
                                    data.bakc_system_3_sql_system = sqlData.GetInt32(sqlData.GetOrdinal("sql_system"));
                                    return true;
                                }
                                else
                                {
                                    if (itemindex == 4)
                                    {
                                        data.bakc_system_4_kami_root = sqlData.GetInt32(sqlData.GetOrdinal("kami_root"));
                                        data.bakc_system_4_back_system = sqlData.GetInt32(sqlData.GetOrdinal("back_system"));
                                        data.bakc_system_4_sql_system = sqlData.GetInt32(sqlData.GetOrdinal("sql_system"));
                                        return true;
                                    }
                                    else
                                    {
                                        if (itemindex == 5)
                                        {
                                            data.bakc_system_5_kami_root = sqlData.GetInt32(sqlData.GetOrdinal("kami_root"));
                                            data.bakc_system_5_back_system = sqlData.GetInt32(sqlData.GetOrdinal("back_system"));
                                            data.bakc_system_5_sql_system = sqlData.GetInt32(sqlData.GetOrdinal("sql_system"));
                                            return true;
                                        }
                                        else
                                        {
                                            if (itemindex == 6)
                                            {
                                                data.bakc_system_6_kami_root = sqlData.GetInt32(sqlData.GetOrdinal("kami_root"));
                                                data.bakc_system_6_back_system = sqlData.GetInt32(sqlData.GetOrdinal("back_system"));
                                                data.bakc_system_6_sql_system = sqlData.GetInt32(sqlData.GetOrdinal("sql_system"));
                                                return true;
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 更改信息
        /// </summary>
        /// <param name="itemindex">用户选择的索引值</param>
        /// <returns></returns>
        public static bool update_back_system(int itemindex)
        {
            using (SqlConnection sqlcon = new SqlConnection(sqlconstr))
            {
                sqlcon.Open();
                if (itemindex==0)
                {
                    SqlCommand sqlcom_update_0_kami_root = new SqlCommand("update Table_back_system set kami_root =" + data.bakc_system_0_kami_root + " where root=" + itemindex + "", sqlcon);
                    SqlCommand sqlcom_update_0_back_system = new SqlCommand("update Table_back_system set back_system =" + data.bakc_system_0_back_system + " where root=" + itemindex + "", sqlcon);
                    SqlCommand sqlcom_update_0_sql_system = new SqlCommand("update Table_back_system set sql_system =" + data.bakc_system_0_sql_system + " where root=" + itemindex + "", sqlcon);
                    /*end_0*/
                    sqlcom_update_0_kami_root.ExecuteNonQuery();
                    sqlcom_update_0_back_system.ExecuteNonQuery();
                    sqlcom_update_0_sql_system.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    if (itemindex == 1)
                    {
                        SqlCommand sqlcom_update_1_kami_root = new SqlCommand("update Table_back_system set kami_root =" + data.bakc_system_1_kami_root + " where root=" + itemindex + "", sqlcon);
                        SqlCommand sqlcom_update_1_back_system = new SqlCommand("update Table_back_system set back_system =" + data.bakc_system_1_back_system + " where root=" + itemindex + "", sqlcon);
                        SqlCommand sqlcom_update_1_sql_system = new SqlCommand("update Table_back_system set sql_system =" + data.bakc_system_1_sql_system + " where root=" + itemindex + "", sqlcon);
                        /*end_1*/
                        sqlcom_update_1_kami_root.ExecuteNonQuery();
                        sqlcom_update_1_back_system.ExecuteNonQuery();
                        sqlcom_update_1_sql_system.ExecuteNonQuery();
                        return true;

                    }
                    else
                    {
                        if (itemindex == 2)
                        {
                            SqlCommand sqlcom_update_2_kami_root = new SqlCommand("update Table_back_system set kami_root =" + data.bakc_system_2_kami_root + " where root=" + itemindex + "", sqlcon);
                            SqlCommand sqlcom_update_2_back_system = new SqlCommand("update Table_back_system set back_system =" + data.bakc_system_2_back_system + " where root=" + itemindex + "", sqlcon);
                            SqlCommand sqlcom_update_2_sql_system = new SqlCommand("update Table_back_system set sql_system =" + data.bakc_system_2_sql_system + " where root=" + itemindex + "", sqlcon);
                            /*end_2*/
                            sqlcom_update_2_kami_root.ExecuteNonQuery();
                            sqlcom_update_2_back_system.ExecuteNonQuery();
                            sqlcom_update_2_sql_system.ExecuteNonQuery();
                            return true;

                        }
                        else
                        {
                            if (itemindex == 3)
                            {
                                SqlCommand sqlcom_update_3_kami_root = new SqlCommand("update Table_back_system set kami_root =" + data.bakc_system_3_kami_root + " where root=" + itemindex + "", sqlcon);
                                SqlCommand sqlcom_update_3_back_system = new SqlCommand("update Table_back_system set back_system =" + data.bakc_system_3_back_system + " where root=" + itemindex + "", sqlcon);
                                SqlCommand sqlcom_update_3_sql_system = new SqlCommand("update Table_back_system set sql_system =" + data.bakc_system_3_sql_system + " where root=" + itemindex + "", sqlcon);
                                /*end_3*/
                                sqlcom_update_3_kami_root.ExecuteNonQuery();
                                sqlcom_update_3_back_system.ExecuteNonQuery();
                                sqlcom_update_3_sql_system.ExecuteNonQuery();
                                return true;

                            }
                            else
                            {
                                if (itemindex == 4)
                                {
                                    SqlCommand sqlcom_update_4_kami_root = new SqlCommand("update Table_back_system set kami_root =" + data.bakc_system_4_kami_root + " where root=" + itemindex + "", sqlcon);
                                    SqlCommand sqlcom_update_4_back_system = new SqlCommand("update Table_back_system set back_system =" + data.bakc_system_4_back_system + " where root=" + itemindex + "", sqlcon);
                                    SqlCommand sqlcom_update_4_sql_system = new SqlCommand("update Table_back_system set sql_system =" + data.bakc_system_4_sql_system + " where root=" + itemindex + "", sqlcon);
                                    /*end_4*/
                                    sqlcom_update_4_kami_root.ExecuteNonQuery();
                                    sqlcom_update_4_back_system.ExecuteNonQuery();
                                    sqlcom_update_4_sql_system.ExecuteNonQuery();
                                    return true;

                                }
                                else
                                {
                                    if (itemindex == 5)
                                    {
                                        SqlCommand sqlcom_update_5_kami_root = new SqlCommand("update Table_back_system set kami_root =" + data.bakc_system_5_kami_root + " where root=" + itemindex + "", sqlcon);
                                        SqlCommand sqlcom_update_5_back_system = new SqlCommand("update Table_back_system set back_system =" + data.bakc_system_5_back_system + " where root=" + itemindex + "", sqlcon);
                                        SqlCommand sqlcom_update_5_sql_system = new SqlCommand("update Table_back_system set sql_system =" + data.bakc_system_5_sql_system + " where root=" + itemindex + "", sqlcon);
                                        /*end_5*/
                                        sqlcom_update_5_kami_root.ExecuteNonQuery();
                                        sqlcom_update_5_back_system.ExecuteNonQuery();
                                        sqlcom_update_5_sql_system.ExecuteNonQuery();
                                        return true;

                                    }
                                    else
                                    {
                                        if (itemindex == 6)
                                        {
                                            SqlCommand sqlcom_update_6_kami_root = new SqlCommand("update Table_back_system set kami_root =" + data.bakc_system_6_kami_root + " where root=" + itemindex + "", sqlcon);
                                            SqlCommand sqlcom_update_6_back_system = new SqlCommand("update Table_back_system set back_system =" + data.bakc_system_6_back_system + " where root=" + itemindex + "", sqlcon);
                                            SqlCommand sqlcom_update_6_sql_system = new SqlCommand("update Table_back_system set sql_system =" + data.bakc_system_6_sql_system + " where root=" + itemindex + "", sqlcon);
                                            /*end_6*/
                                            sqlcom_update_6_kami_root.ExecuteNonQuery();
                                            sqlcom_update_6_back_system.ExecuteNonQuery();
                                            sqlcom_update_6_sql_system.ExecuteNonQuery();
                                            return true;

                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 密码加密MD5
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>64位md5加密后字符串</returns>
        public static string MD5Encrypt64(string password)
        {
            //string cl = password;
            ////string pwd = "";
            //MD5 md5 = MD5.Create(); //实例化一个md5对像
            //                        // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            //byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            //return Convert.ToBase64String(s);

            byte[] textBytes = System.Text.Encoding.Default.GetBytes(password);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        private static string encryptKey = "Booa";//字符串加密密钥(注意：密钥只能是4位)

        public static string Encrypt(string str)
        {//加密字符串

            try
            {
                byte[] key = Encoding.Unicode.GetBytes(encryptKey);//密钥
                byte[] data = Encoding.Unicode.GetBytes(str);//待加密字符串

                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//加密、解密对象
                MemoryStream MStream = new MemoryStream();//内存流对象

                //用内存流实例化加密流对象
                CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);
                CStream.Write(data, 0, data.Length);//向加密流中写入数据
                CStream.FlushFinalBlock();//将数据压入基础流
                byte[] temp = MStream.ToArray();//从内存流中获取字节序列
                CStream.Close();//关闭加密流
                MStream.Close();//关闭内存流

                return Convert.ToBase64String(temp);//返回加密后的字符串
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str">加密后的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {//解密字符串

            try
            {
                byte[] key = Encoding.Unicode.GetBytes(encryptKey);//密钥
                byte[] data = Convert.FromBase64String(str);//待解密字符串

                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//加密、解密对象
                MemoryStream MStream = new MemoryStream();//内存流对象

                //用内存流实例化解密流对象
                CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
                CStream.Write(data, 0, data.Length);//向加密流中写入数据
                CStream.FlushFinalBlock();//将数据压入基础流
                byte[] temp = MStream.ToArray();//从内存流中获取字节序列
                CStream.Close();//关闭加密流
                MStream.Close();//关闭内存流

                return Encoding.Unicode.GetString(temp);//返回解密后的字符串
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// 查询是否具备模块访问权限
        /// </summary>
        /// <param name="User">用户名</param>
        ///<param name="Gerde">权限等级</param>
        /// <returns>true 具备权限 </returns>
        public static int[] is_root(string User,int administrator)
        {
            SqlConnection sqlcon = new SqlConnection(sqlconstr);
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("Select * from Table_back_system where root="+ administrator + " ", sqlcon);
            SqlDataReader sqldata = sqlcom.ExecuteReader();
            if (sqldata.Read())
            {
               int kami_root =  sqldata.GetInt32(sqldata.GetOrdinal("kami_root"));
               int back_System = sqldata.GetInt32(sqldata.GetOrdinal("back_system"));
               int sql_System = sqldata.GetInt32(sqldata.GetOrdinal("sql_system"));
                int[] ret = new int[] { kami_root, back_System, sql_System };
                return ret;
            }
            return null;
        }

        public static bool is_ban(string User)
        {
            SqlConnection sqlcon = new SqlConnection(sqlconstr);
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("select * from usertable where username='" + User+"'", sqlcon);
            SqlDataReader sqldata = sqlcom.ExecuteReader();
            if (sqldata.Read()==true)
            {
               int is_ban = sqldata.GetInt32(sqldata.GetOrdinal("is_ban"));
                if (is_ban==0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            return true;
        }





    }
}
