using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace StudentManager
{
    public partial class FrmStudentInfo : Form
    {
        public FrmStudentInfo(Student _student)
        {
            InitializeComponent();
           

            lblStudentName.Text = _student.ClassName;
            lblGender.Text = _student.Gender.ToString();
            lblBirthday.Text = _student.Birthday.ToString();
            lblClass.Text = _student.ClassName;
            lblStudentIdNo.Text = _student.StudentIdNo;
            lblPhoneNumber.Text = _student.PhoneNumber;
            lblAddress.Text = _student.StudentAddress;
            
            this.pbStu.Image = _student.StuImage.Length != 0 ?
              (Image)new Common.SerializeObjectToString().
              DeserializeObject(_student.StuImage)
              : Image.FromFile("default.png");
        }
      
        //¹Ø±Õ
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentInfo_Load(object sender, EventArgs e)
        {

        }
    }
}