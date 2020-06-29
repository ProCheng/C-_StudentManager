using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BLL;
using Models;


namespace StudentManager
{
    public partial class FrmStudentManage : Form
    {

        private StudentClassService _studentClassService = new StudentClassService();
        private StudentService _studentService = new StudentService();
        public FrmStudentManage()
        {
            InitializeComponent();
            this.cboClass.DataSource = _studentClassService.GetAllClass();
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;
            this.dgvStudentList.AutoGenerateColumns = false;

        }
        //按照班级查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if(this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("请先选中班级!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = _studentService.GetStudentsByClassName(this.cboClass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常操作");
            }                            
        }
        //根据学号查询
        private void btnQueryById_Click(object sender, EventArgs e)
        {
            if(txtStudentId.Text.Length == 0)
            {
                MessageBox.Show("请输入学号!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = _studentService.GetStudentById(this.txtStudentId.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常操作");
            }
        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        //双击选中的学员对象并显示详细信息
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentService studentService = new StudentService();
            int stuId = int.Parse(this.dgvStudentList.CurrentRow.Cells[0].Value.ToString());
            Student student = null;

            student = studentService.GetStudentById(stuId);
            FrmStudentInfo info = new FrmStudentInfo(student);
            
            info.Show();
        }
        //修改学员对象
        private void btnEidt_Click(object sender, EventArgs e)
        {
            if (this.dgvStudentList.Rows.Count == 0)
                return;
            int stuId =  int.Parse(this.dgvStudentList.CurrentRow.Cells[0].Value.ToString());
           
            Student student = _studentService.GetStudentById(stuId);
            FrmEditStudent frmEditStudent = new FrmEditStudent(student,dgvStudentList.CurrentRow);
            frmEditStudent.Show();
        }
        //删除学员对象
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.dgvStudentList.Rows.Count == 0)
                return;

            try
            {
                _studentService.delete(int.Parse(this.dgvStudentList.CurrentRow.Cells[0].Value.ToString()));
                //this.dgvStudentList.Rows.Remove(this.dgvStudentList.CurrentRow);

                List<Student> list = (List<Student>)dgvStudentList.DataSource; //'转换数据源
                list.RemoveAt(dgvStudentList.CurrentRow.Index);//’移除
                dgvStudentList.DataSource = null;// '为空
                dgvStudentList.DataSource = list;//’显示数据

               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message);
            }
            

        }
        //姓名降序
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("请先选中班级!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = _studentService.GetStudentsByNameDesc(this.cboClass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常操作");
            }
        }
        //学号降序
        private void btnStuIdDESC_Click(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("请先选中班级!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = 
                    _studentService.GetStudentsByIdDesc(this.cboClass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常操作");
            }
        }
        //添加行号
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        
        }
        //打印当前学员信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //导出到Excel
        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void tsmiModifyStu_Click(object sender, EventArgs e)
        {

        }

        private void FrmStudentManage_Load(object sender, EventArgs e)
        {
            
        }
    }

   
}