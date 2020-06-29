using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Models;

namespace StudentManager
{
    public partial class FrmScoreManage : Form
    {     
        //侧边栏显示数据
        public void sidebar()
        {
            if (dgvScoreList.Rows.Count != 0)
            {
                this.lblAttendCount.Text = dgvScoreList.Rows.Count.ToString();
                this.lblCount.Text = lblList.Items.Count.ToString();
                int c = 0;
                int db = 0;
                foreach (DataGridViewRow tr in dgvScoreList.Rows)
                {
                    c += (int)(tr.Cells[4].Value);
                    db += (int)(tr.Cells[5].Value);
                }
                this.lblCSharpAvg.Text = (c / dgvScoreList.Rows.Count).ToString();
                this.lblDBAvg.Text = (db / dgvScoreList.Rows.Count).ToString();
            }
            else
            {
                this.lblAttendCount.Text = "无";
                this.lblCount.Text = "无";
                this.lblCSharpAvg.Text = "无";
                this.lblDBAvg.Text = "无";
            }
        } 


        public FrmScoreManage()
        {
            InitializeComponent();
            this.dgvScoreList.AutoGenerateColumns = false;
            StudentClassService _studentClassService = new StudentClassService();
            this.cboClass.DataSource = _studentClassService.GetAllClass();
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;
            


        }
        ScorceService scoreservice = new ScorceService();
        //根据班级查询      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboClass.Text != "Models.StudentClass" && this.cboClass.Text != "")
            {
                this.dgvScoreList.DataSource = null;
                this.lblList.DataSource = null;
                this.dgvScoreList.DataSource = scoreservice.showScorce(this.cboClass.Text);
                this.lblList.DataSource = scoreservice.showlack(this.cboClass.Text);

                sidebar();


            }
          
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //统计全校考试成绩
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.dgvScoreList.DataSource = null;
            this.lblList.DataSource = null;
            this.dgvScoreList.DataSource = scoreservice.showScorce();
            this.lblList.DataSource= scoreservice.showlack();

            sidebar();
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

    
     
        //选择框选择改变处理
        private void dgvScoreList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
         
        }

        private void FrmScoreManage_Load(object sender, EventArgs e)
        {
            
        }

        private void cboClass_TextChanged(object sender, EventArgs e)
        {
              
        }
    }
}