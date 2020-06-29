using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
    public class AdminServeice
    {
        public Admin Login(Admin admin)
        {
            Admin user = null ;
            string sql = "select * from admins where LoginId = @loginId and LoginPwd = @pwd";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@loginId",admin.LoginId),
                new SqlParameter("@pwd",admin.LoginPwd)
            };
            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(sql,parameters);
                if (reader.Read())
                {
                    user = new Admin()
                    {
                        LoginId = (int)reader["LoginId"],
                        LoginPwd = reader["LoginPwd"].ToString(),
                        AdminName = reader["AdminName"].ToString(),
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
    }
}
