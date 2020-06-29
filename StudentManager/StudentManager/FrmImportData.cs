using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace StudentManager
{
    public partial class FrmImportData : Form
    {


        public FrmImportData()
        {
            InitializeComponent();
            
            this.dgvStudentList.AutoGenerateColumns = false;
        }
        private static string strpath="";
        List<Student> students = new List<Student>();
        private void btnChoseExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel文件(*.xls)|*.xls";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strpath = openFileDialog1.FileName;
                loadxls();
            }
        }

        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }
        //保存到数据库
        private void btnSaveToDB_Click(object sender, EventArgs e)
        {
            try
            {
                StudentService studentService = new StudentService();
                foreach (Student student in students)
                {
                    studentService.Insert(student);
                }
                MessageBox.Show("数据全部保存成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }






        private void loadxls()
        {

            if (strpath != "")
            {
                try
                {
                    this.dgvStudentList.DataSource = null;
                    string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + strpath + " ;Extended Properties=Excel 8.0";
                    System.Data.OleDb.OleDbConnection myConn = new System.Data.OleDb.OleDbConnection(strCon);
                    string strCom = "SELECT * FROM [Sheet1$]";
                    System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, myConn);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    myCommand.Fill(dt);
                    students.Clear();


                    students.Add(new Student()
                    {
                        StudentName = dt.Columns[0].ColumnName,
                        Gender = dt.Columns[1].ColumnName,
                        Birthday = Convert.ToDateTime(dt.Columns[2].ColumnName),
                        Age = DateTime.Now.Year - Convert.ToDateTime(dt.Columns[2].ColumnName).Year,
                        StudentIdNo = dt.Columns[3].ColumnName.Trim(),
                        PhoneNumber = dt.Columns[4].ColumnName.Trim(),
                        StudentAddress = dt.Columns[5].ColumnName.Trim() == "" ? "地址不详" : dt.Columns[5].ColumnName.Trim(),
                        ClassId =int.Parse(dt.Columns[6].ToString()),
                        StuImage = "",
                    });;
                 

                    foreach (DataRow item in dt.Rows)
                    {
                        
                       students.Add(new Student() {
                            StudentName = item[0].ToString(),
                            Gender = item[1].ToString(),
                            Birthday = Convert.ToDateTime(item[2].ToString()),
                            Age = DateTime.Now.Year - Convert.ToDateTime(item[2].ToString()).Year,
                            StudentIdNo = item[3].ToString().Trim(),
                            PhoneNumber = item[4].ToString().Trim(),
                            StudentAddress = item[5].ToString().Trim() == "" ? "地址不详" : item[5].ToString().Trim(),
                            ClassId = int.Parse(item[6].ToString()),
                           StuImage = "",
                       });
                        
                    }
                    this.dgvStudentList.DataSource = students;
                    
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("请选择案件导入的EXCEL："+ex.Message);

                }



            }
            else
            {
                MessageBox.Show("请选择Excel文件");
            }

        }


    }
}

