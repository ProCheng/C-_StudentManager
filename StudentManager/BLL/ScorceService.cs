using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ScorceService
    {
             List<ScoreList> scorelist = new List<ScoreList>();

        public List<ScoreList> showScorce()
        {
            scorelist.Clear();
            string sql = "select a.studentid,a.studentname,a.gender,a.classid,b.classname,c.CSharp,c.SQLServerDB,a.PhoneNumber  from students as a,StudentClass as b,scorelist as c where a.StudentId=c.studentid and a.classid=b.classid";

            SqlDataReader read = SqlHelper.ExecuteReader(sql,null);
            while (read.Read())
            {
                scorelist.Add(new ScoreList() {
                    StudentId= (int)read[0],
                    StudentName=read[1].ToString(),
                    Gender = read[2].ToString(),
                    ClassId = (int)read[3],
                    ClassName = read[4].ToString(),
                    CSharp = (int)read[5],
                    SQLServerDB  = (int)read[6],
                    PhoneNumber = read[7].ToString(),

                });;
            }
            read.Close();
            SqlHelper.connection.Close();
            return scorelist;
        }
        public List<ScoreList> showScorce(string className)
        {
            scorelist.Clear();
            string sql = $"select a.studentid,a.studentname,a.gender,a.classid,b.classname,c.CSharp,c.SQLServerDB  from students as a,StudentClass as b,scorelist as c where a.StudentId=c.studentid and a.classid=b.classid and b.ClassName='{className}'";

            SqlDataReader read = SqlHelper.ExecuteReader(sql, null);
            while (read.Read())
            {
                scorelist.Add(new ScoreList()
                {
                    StudentId = (int)read[0],
                    StudentName = read[1].ToString(),
                    Gender = read[2].ToString(),
                    ClassId = (int)read[3],
                    ClassName = read[4].ToString(),
                    CSharp = (int)read[5],
                    SQLServerDB = (int)read[6],

                });
            }
            read.Close();
            SqlHelper.connection.Close();
            return scorelist;
        }

        public List<ScoreList> showScorce(string className,int score)
        {
            scorelist.Clear();
            string sql = $"select a.studentid,a.studentname,a.gender,a.classid,b.classname,c.CSharp,c.SQLServerDB  from students as a,StudentClass as b,scorelist as c where a.StudentId=c.studentid and a.classid=b.classid and b.ClassName='{className}' and c.csharp>{score}";

            SqlDataReader read = SqlHelper.ExecuteReader(sql, null);
            while (read.Read())
            {
                scorelist.Add(new ScoreList()
                {
                    StudentId = (int)read[0],
                    StudentName = read[1].ToString(),
                    Gender = read[2].ToString(),
                    ClassId = (int)read[3],
                    ClassName = read[4].ToString(),
                    CSharp = (int)read[5],
                    SQLServerDB = (int)read[6],

                });
            }
            read.Close();
            SqlHelper.connection.Close();
            return scorelist;
        }
        public List<string> showlack()
        {
            string sql = "select StudentName from Students where Students.StudentId in (select Students.StudentId from Students EXCEPT select ScoreList.StudentId from ScoreList)";
            SqlDataReader read = SqlHelper.ExecuteReader(sql, null);
            List<string> arrName = new List<string>();
            while (read.Read())
            {
                arrName.Add(read[0].ToString());
            }
            read.Close();
            SqlHelper.connection.Close();
            return arrName;
        }
        public List<string> showlack(string className)
        {
            string sql = $"select StudentName from Students where Students.StudentId in (select Students.StudentId from Students EXCEPT select ScoreList.StudentId from ScoreList) and Students.ClassId in (select ClassId from StudentClass where ClassName='{className}')";

            SqlDataReader read = SqlHelper.ExecuteReader(sql, null);
            List<string> arrName = new List<string>();
            while (read.Read())
            {
                arrName.Add(read[0].ToString());
            }
            read.Close();
            SqlHelper.connection.Close();
            return arrName;
        }
    }
}
