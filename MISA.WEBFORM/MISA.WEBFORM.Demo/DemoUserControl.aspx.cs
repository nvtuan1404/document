using MISA.WEBFORM.Demo.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MISA.WEBFORM.Demo
{
    public partial class DemoUserControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindDNN();
            }
        }
        private void DataBindDNN()
        {

            TestController test = new TestController();
            List<News> lstNews = new List<News>();
            lstNews = test.GetAllNews();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Title"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("link"));

            for (int i = 0; i < lstNews.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Title"] = lstNews[i].Title;
                dr["Description"] = lstNews[i].Description;
                dr["link"] = lstNews[i].link;
                dt.Rows.Add(dr);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}