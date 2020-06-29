using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace StudentManager
{
    public partial class FrmScoreQuery : Form
    {
             private DataSet ds = null;//保存全部查询结果的数据集
        public FrmScoreQuery()
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //根据班级名称动态筛选
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtScore.Text != "" && this.cboClass.Text != "Models.StudentClass" && this.cboClass.Text != "")
            {
                this.dgvScoreList.DataSource = null;
                this.dgvScoreList.DataSource = scoreservice.showScorce(cboClass.Text,int.Parse(txtScore.Text));

            }
        }
        //显示全部成绩
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            this.dgvScoreList.DataSource = null;
            this.dgvScoreList.DataSource = scoreservice.showScorce();
           

           
        }
        //根据C#成绩动态筛选
        private void txtScore_TextChanged(object sender, EventArgs e)
        {
            if (txtScore.Text != "" && this.cboClass.Text != "")
            {
                this.dgvScoreList.DataSource = null;
                this.dgvScoreList.DataSource = scoreservice.showScorce(cboClass.Text, int.Parse(txtScore.Text));

            }
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           // Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

        //打印当前的成绩信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        private void FrmScoreQuery_Load(object sender, EventArgs e)
        {

        }
    }
}
