using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using StudentManager.Common;

namespace StudentManager
{
    public partial class FrmMain : Form
    {
       
        public FrmMain()
        {
            InitializeComponent();
            lblCurrentUser.Text = Program.CurrentAdmin.AdminName + "】";
            spContainer.Panel2.BackgroundImage = Image.FromFile("mainbg.png");
            this.spContainer.Panel2.BackgroundImageLayout = ImageLayout.Stretch;

        }

        #region 嵌入窗体显示
        private void CloseFrm()
        {
            //将嵌入在面板中的窗体关闭
            foreach (var item in spContainer.Panel2.Controls)
            {
                if(item is Form)
                {
                    Form objFrm = (Form)item;
                    objFrm.Close();
                }
            }
        }

        private void OpenForm(Form objFrm)
        {
            CloseFrm();
            objFrm.TopLevel = false;//将子窗体设为非顶层对象
            objFrm.FormBorderStyle = FormBorderStyle.None;
            objFrm.Parent = this.spContainer.Panel2;
            objFrm.Dock = DockStyle.Fill;
            objFrm.Show();
        }

    
        //显示添加新学员窗体       
        private void tsmiAddStudent_Click(object sender, EventArgs e)
        {
            FrmAddStudent addStudent = new FrmAddStudent();
            OpenForm(addStudent);
        }
        private void btnAddStu_Click(object sender, EventArgs e)
        {
            tsmiAddStudent_Click(null, null);
        }
        //批量导入学员信息
        private void tsmi_Import_Click(object sender, EventArgs e)
        {
            FrmImportData objForm = new FrmImportData();
            OpenForm(objForm);
        }
        private void btnImportStu_Click(object sender, EventArgs e)
        {
            tsmi_Import_Click(null, null);
        }
        //考勤打卡      
        private void tsmi_Card_Click(object sender, EventArgs e)
        {
          
          //  OpenForm(objForm);
        }
        private void btnCard_Click(object sender, EventArgs e)
        {
            tsmi_Card_Click(null, null);
        }
        //成绩快速查询【嵌入显示】
        private void tsmiQuery_Click(object sender, EventArgs e)
        {
            FrmScoreQuery objForm = new FrmScoreQuery();
            OpenForm(objForm);
        }
        private void btnScoreQuery_Click(object sender, EventArgs e)
        {
            tsmiQuery_Click(null, null);
        }
        //学员管理【嵌入显示】
        private void tsmiManageStudent_Click(object sender, EventArgs e)
        {
            FrmStudentManage objForm = new FrmStudentManage();
            OpenForm(objForm);
        }
        private void btnStuManage_Click(object sender, EventArgs e)
        {
            tsmiManageStudent_Click(null, null);
        }
        //显示成绩查询与分析窗口    
        private void tsmiQueryAndAnalysis_Click(object sender, EventArgs e)
        {
            FrmScoreManage objForm = new FrmScoreManage();
            OpenForm(objForm);
        }
        private void btnScoreAnalasys_Click(object sender, EventArgs e)
        {
            tsmiQueryAndAnalysis_Click(null, null);
        }
        //考勤查询
        private void tsmi_AQuery_Click(object sender, EventArgs e)
        {
           
           // OpenForm(objForm);
        }
        private void btnAttendanceQuery_Click(object sender, EventArgs e)
        {
            tsmi_AQuery_Click(null, null);
        }

        #endregion

        #region 退出系统确认

        //退出系统
        private void tmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        }

        #endregion

        #region 其他

        //密码修改
        private void tmiModifyPwd_Click(object sender, EventArgs e)
        {
            FrmModifyPwd objPwd = new FrmModifyPwd();
            objPwd.ShowDialog();
        }
        private void btnModifyPwd_Click(object sender, EventArgs e)
        {
            tmiModifyPwd_Click(null, null);
        }
        //账号切换
        private void btnChangeAccount_Click(object sender, EventArgs e)
        {

        }
        private void tsbAddStudent_Click(object sender, EventArgs e)
        {
            tsmiAddStudent_Click(null, null);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tsmiManageStudent_Click(null, null);
        }
        private void tsbScoreAnalysis_Click(object sender, EventArgs e)
        {
            tsmiQueryAndAnalysis_Click(null, null);
        }
        private void tsbModifyPwd_Click(object sender, EventArgs e)
        {
            tmiModifyPwd_Click(null, null);
        }
        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsbQuery_Click(object sender, EventArgs e)
        {
            tsmiQuery_Click(null, null);
        }

        //访问官网
        private void tsmi_linkxkt_Click(object sender, EventArgs e)
        {
         
        }
        private void btnGoXiketang_Click(object sender, EventArgs e)
        {
            tsmi_linkxkt_Click(null, null);
        }
        //系统升级
        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }


        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //autoSizeFormClass.controllInitializeSize(this);
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            //autoSizeFormClass.controlAutoSize(this);
        }

        
    }
}