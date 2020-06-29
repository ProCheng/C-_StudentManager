using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Models;
using BLL;
using StudentManager.Common;

namespace StudentManager
{
    public partial class FrmEditStudent : Form
    {
        private Student _student;
        private StudentClassService _studentClassService = new StudentClassService();
        private Student student;
        private DataGridViewRow currentRow;


        public FrmEditStudent(Student student, DataGridViewRow currentRow)
        {

            InitializeComponent();
            _student = student;
            this.currentRow = currentRow;

            this.cboClassName.DataSource = _studentClassService.GetAllClass();
            this.cboClassName.DisplayMember = "ClassName";
            this.cboClassName.ValueMember = "ClassId";
            this.cboClassName.SelectedIndex = -1;

            cboClassName.Text = _student.ClassName;
            txtStudentId.Text = _student.StudentId.ToString();
            txtStudentIdNo.Text = _student.StudentIdNo;
            txtStudentName.Text = _student.StudentName;
            txtPhoneNumber.Text = _student.PhoneNumber;
            txtAddress.Text = _student.StudentAddress;
            rdoFemale.Checked = _student.Gender == "女";
            rdoMale.Checked = _student.Gender == "男";
            dtpBirthday.Text = _student.Birthday.ToShortDateString();
            this.pbStu.Image = student.StuImage.Length != 0 ?
              (Image)new Common.SerializeObjectToString().
              DeserializeObject(student.StuImage) 
              : Image.FromFile("default.png"); 
        }

       /* public FrmEditStudent(Student student, DataGridViewRow currentRow)
        {
            this.student = student;
            this.currentRow = currentRow;
        }*/

        //提交修改
        private void btnModify_Click(object sender, EventArgs e)
        {

            //验证数据

            //封装实体

            Student student = new Student()
            {
                StudentName = this.txtStudentName.Text.Trim(),
                Gender = this.rdoMale.Checked ? "男" : "女",
                Birthday = Convert.ToDateTime(this.dtpBirthday.Text),
                StudentIdNo = this.txtStudentIdNo.Text.Trim(),
                PhoneNumber = this.txtPhoneNumber.Text.Trim(),
                StudentAddress = this.txtAddress.Text.Trim() == "" ? "地址不详" : this.txtAddress.Text.Trim(),
                ClassName = this.cboClassName.Text,
                ClassId = Convert.ToInt32(this.cboClassName.SelectedValue),//获取选择班级对应classId
                Age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year,
                StuImage = this.pbStu.Image != null ? new SerializeObjectToString().
                SerializeObject(this.pbStu.Image) : "",
                StudentId =int.Parse(txtStudentId.Text),
            };

            try
            {
                if (new StudentService().Update(student) > 0)
                {
                    MessageBox.Show("成功");
                    this.currentRow.Cells[1].Value = student.StudentName;
                    this.currentRow.Cells[2].Value = student.Gender;
                    this.currentRow.Cells[3].Value = student.StudentIdNo;
                    this.currentRow.Cells[4].Value = student.Birthday;
                    this.currentRow.Cells[5].Value = student.PhoneNumber;
                    this.currentRow.Cells[6].Value = student.ClassName;





                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //选择照片
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog objFileDialog = new OpenFileDialog();
            DialogResult result = objFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                this.pbStu.Image = Image.FromFile(objFileDialog.FileName);
        }

        private void FrmEditStudent_Load(object sender, EventArgs e)
        {

        }
    }
}