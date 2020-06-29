using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


using System.Diagnostics;
using Models;

namespace StudentManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //创建登录窗体
            FrmUserLogin frmUserLogin = new FrmUserLogin();
            DialogResult result =  frmUserLogin.ShowDialog();
            if(result == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Exit();
            }           
        }
        public static Admin CurrentAdmin = null;
    }
}
