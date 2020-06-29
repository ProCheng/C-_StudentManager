using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Models;
using StudentManager.Common;

namespace StudentManager
{
    public partial class FrmAddStudent : Form
    {

        private StudentClassService _studentClassService = new StudentClassService();
        private StudentService _studentService = new StudentService();
        private List<Student> _students = new List<Student>();
        public FrmAddStudent()
        {
            InitializeComponent();
            this.cboClassName.DataSource = _studentClassService.GetAllClass();
            this.cboClassName.DisplayMember = "ClassName";
            this.cboClassName.ValueMember = "ClassId";
            if (this.cboClassName.Items.Count == 0)
            {
                this.cboClassName.SelectedIndex = -1;
            }
            
            this.dgvStudentList.AutoGenerateColumns = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //数据验证
            if (!Common.DataValidate.IsIdentityCard(txtStudentIdNo.Text))
            {
                MessageBox.Show("请输入正确格式的身份证");
                return;
            }

            //验证身份证号与生日是否匹配
            string month = string.Empty;
            string day = string.Empty;
            if (Convert.ToDateTime(this.dtpBirthday.Text).Month < 10)
                month = "0" + Convert.ToDateTime(this.dtpBirthday.Text).Month;
            else month = Convert.ToDateTime(this.dtpBirthday.Text).Month.ToString();
            if (Convert.ToDateTime(this.dtpBirthday.Text).Day < 10)
                day = "0" + Convert.ToDateTime(this.dtpBirthday.Text).Day;
            else day = Convert.ToDateTime(this.dtpBirthday.Text).Day.ToString();
            string birth = Convert.ToDateTime(this.dtpBirthday.Text).Year.ToString() + month + day;
            if (!this.txtStudentIdNo.Text.Contains(birth))
            {
                MessageBox.Show("身份证号和出生年月不匹配");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //判断身份证是否在数据库存在
            if (_studentService.IsIdNoExisted(this.txtStudentIdNo.Text))
            {
                MessageBox.Show("该身份证号已经在数据库中存在");
                return;
            }

            //封装学生实体
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
                StuImage = this.pbVideo.Image != null ? new SerializeObjectToString().
                SerializeObject(this.pbVideo.Image) : ""
            };
            int result = _studentService.Insert(student);
            if (result > 1)
            {
                student.StudentId = result;
                _students.Add(student);
                this.dgvStudentList.DataSource = null;
                this.dgvStudentList.DataSource = _students;

                DialogResult dResult = MessageBox.Show("是否继续添加", "系统消息", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    foreach (Control item in this.gbstuinfo.Controls)
                    {
                        if (item is TextBox)
                            item.Text = "";
                    }
                   
                    this.txtStudentName.Focus();
                    this.pbVideo.Image = null;
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.pbVideo.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddStudent_Load(object sender, EventArgs e)
        {

        }

        private void cboClassName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}