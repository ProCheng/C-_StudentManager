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
        //���հ༶��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if(this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("����ѡ�а༶!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = _studentService.GetStudentsByClassName(this.cboClass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "�쳣����");
            }                            
        }
        //����ѧ�Ų�ѯ
        private void btnQueryById_Click(object sender, EventArgs e)
        {
            if(txtStudentId.Text.Length == 0)
            {
                MessageBox.Show("������ѧ��!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = _studentService.GetStudentById(this.txtStudentId.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "�쳣����");
            }
        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        //˫��ѡ�е�ѧԱ������ʾ��ϸ��Ϣ
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentService studentService = new StudentService();
            int stuId = int.Parse(this.dgvStudentList.CurrentRow.Cells[0].Value.ToString());
            Student student = null;

            student = studentService.GetStudentById(stuId);
            FrmStudentInfo info = new FrmStudentInfo(student);
            
            info.Show();
        }
        //�޸�ѧԱ����
        private void btnEidt_Click(object sender, EventArgs e)
        {
            if (this.dgvStudentList.Rows.Count == 0)
                return;
            int stuId =  int.Parse(this.dgvStudentList.CurrentRow.Cells[0].Value.ToString());
           
            Student student = _studentService.GetStudentById(stuId);
            FrmEditStudent frmEditStudent = new FrmEditStudent(student,dgvStudentList.CurrentRow);
            frmEditStudent.Show();
        }
        //ɾ��ѧԱ����
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.dgvStudentList.Rows.Count == 0)
                return;

            try
            {
                _studentService.delete(int.Parse(this.dgvStudentList.CurrentRow.Cells[0].Value.ToString()));
                //this.dgvStudentList.Rows.Remove(this.dgvStudentList.CurrentRow);

                List<Student> list = (List<Student>)dgvStudentList.DataSource; //'ת������Դ
                list.RemoveAt(dgvStudentList.CurrentRow.Index);//���Ƴ�
                dgvStudentList.DataSource = null;// 'Ϊ��
                dgvStudentList.DataSource = list;//����ʾ����

               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("ɾ��ʧ�ܣ�" + ex.Message);
            }
            

        }
        //��������
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("����ѡ�а༶!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = _studentService.GetStudentsByNameDesc(this.cboClass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "�쳣����");
            }
        }
        //ѧ�Ž���
        private void btnStuIdDESC_Click(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("����ѡ�а༶!");
                return;
            }
            try
            {
                this.dgvStudentList.DataSource = 
                    _studentService.GetStudentsByIdDesc(this.cboClass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "�쳣����");
            }
        }
        //����к�
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        
        }
        //��ӡ��ǰѧԱ��Ϣ
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //������Excel
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