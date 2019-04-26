using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MISA.WEBFORM.Demo.Controller
{
    public class TestController
    {
        string connectionString = "Server=FRESHER-52\\SQLEXPRESS01;Database=DNN;User Id=dnndev;Password=12345678@Abc;";

        //To View all employees details      
        public List<News> GetAllNews()
        {
            List<News> lstNews = new List<News>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Proc_GetAllNews", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    News news = new News();

                    news.Title = rdr["Title"].ToString();
                    news.Description = rdr["Description"].ToString();
                    news.link = rdr["link"].ToString();
                    lstNews.Add(news);
                }
                con.Close();
            }
            return lstNews;
        }
    }
}