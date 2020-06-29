using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using System.Data.SqlClient;

namespace BLL
{
    public class StudentService
    {
        /// <summary>
        /// 判断身份证号是否已经存在
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public bool IsIdNoExisted(string studentIdNo)
        {
            string sql = "select count(*) from Students where StudentIdNo=" + studentIdNo;
            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
            if (result == 1) return true;
            else return false;
        }


        public int Insert(Student objStudent)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Students(studentName,Gender,Birthday,");
            sqlBuilder.Append("StudentIdNo,Age,PhoneNumber,StudentAddress,ClassId,StuImage)");
            sqlBuilder.Append(" values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}');select @@Identity");
            //【2】解析对象
            string sql = string.Format(sqlBuilder.ToString(), objStudent.StudentName,
                     objStudent.Gender, objStudent.Birthday.ToString("yyyy-MM-dd"),
                    objStudent.StudentIdNo, objStudent.Age,
                    objStudent.PhoneNumber, objStudent.StudentAddress,
                    objStudent.ClassId, objStudent.StuImage);
            try
            {
               object result = SqlHelper.ExecuteScalar(sql);
               return  Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }


        public List<Student> GetStudentsByClassName(string className)
        {
            return GetStudentsByWhere($" and className = '{className}'");
        }

        public List<Student> GetStudentsByNameDesc(string className)
        {
            return GetStudentsByClassName(className)
                .OrderByDescending(p=>p.StudentName).ToList();
        }
        public List<Student> GetStudentsByIdDesc(string className)
        {
            return GetStudentsByClassName(className)
                .OrderByDescending(p => p.StudentId).ToList();
        }
        //通用
        private List<Student> GetStudentsByWhere(string whereStr="")
        {
            List<Student> students = new List<Student>();
            string sql = $"select StudentId,StudentName,Gender,Birthday,StudentIdNo,Age,PhoneNumber,StudentAddress," +
                 $"Students.ClassId,ClassName from Students,StudentClass where Students.ClassId " +
                 $"= StudentClass.ClassId" + whereStr;
            try
            {
                SqlDataReader objReader = SqlHelper.ExecuteReader(sql, null);
                while (objReader.Read())
                {
                    students.Add(new Student()
                    {
                        StudentId = Convert.ToInt32(objReader["StudentId"]),
                        StudentName = objReader["StudentName"].ToString(),
                        Gender = objReader["Gender"].ToString(),
                        PhoneNumber = objReader["PhoneNumber"].ToString(),
                        Birthday = Convert.ToDateTime(objReader["Birthday"]),
                        StudentIdNo = objReader["StudentIdNo"].ToString(),
                        ClassName = objReader["ClassName"].ToString()
                    });
                }
                objReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return students;
        }

        public List<Student> GetStudentById(string stuIdNo)
        {
            return GetStudentsByWhere($" and StudentId = {stuIdNo}" );
        }

        public Student GetStudentById(int stuIdNo)
        {
            Student student = null;
            string sql = $"select StudentId,StudentName,Gender,Birthday,StudentIdNo,Age,PhoneNumber,StudentAddress," +
                 $"Students.ClassId,ClassName,StuImage,StudentAddress from Students,StudentClass where Students.ClassId " +
                 $"= StudentClass.ClassId and StudentId = {stuIdNo}";
            try
            {
                SqlDataReader objReader = SqlHelper.ExecuteReader(sql, null);
                if (objReader.Read())
                {
                    student = new Student()
                    {
                        StudentId = Convert.ToInt32(objReader["StudentId"]),
                        StudentName = objReader["StudentName"].ToString(),
                        Gender = objReader["Gender"].ToString(),
                        PhoneNumber = objReader["PhoneNumber"].ToString(),
                        Birthday = Convert.ToDateTime(objReader["Birthday"]),
                        StudentIdNo = objReader["StudentIdNo"].ToString(),
                        ClassName = objReader["ClassName"].ToString(),
                        StudentAddress = objReader["StudentAddress"].ToString(),
                        StuImage= objReader["StuImage"].ToString()                      
                    };
                }
                objReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return student;
        }

        

        public int Update(Student student)
        {
            string sql = $"update Students set StudentName = '{student.StudentName}'," +
                $" Gender = '{student.Gender}', " +
                $" Birthday = '{student.Birthday}' ," +
                $" StudentIdNO = {student.StudentIdNo} ," +
                $" Age = {student.Age} ," +
                $" PhoneNumber = '{student.PhoneNumber} '," +
                $" StudentAddress = '{student.StudentAddress}'," +
                $" ClassId = '{student.ClassId}'," +
                $" StuImage ='{student.StuImage}' " +
                $"where StudentId = {student.StudentId}";

            try
            {
               return  SqlHelper.ExecuteNonQuery(sql,null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool delete(int id)
        {
            List<string> sql = new List<string>();
            sql.Add($"delete ScoreList where StudentId = {id}");
            sql.Add($"delete Students where StudentId = {id}");
            try
            {
                return SqlHelper.ExecuteSQL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
