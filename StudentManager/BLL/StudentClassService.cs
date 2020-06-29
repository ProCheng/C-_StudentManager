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
    public class StudentClassService
    {
        public List<StudentClass> GetAllClass()
        {
            List<StudentClass> list = new List<StudentClass>();
            string sql = "select * from StudentClass";
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, null);
            while (reader.Read())
            {
                list.Add(new StudentClass()
                {
                    ClassId = (int)reader["ClassId"],
                    ClassName = reader["ClassName"].ToString()
                });
            }
            reader.Close();
            return list;
        }
    }
}
