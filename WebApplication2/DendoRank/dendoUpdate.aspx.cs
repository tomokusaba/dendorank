using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess;

namespace WebApplication2.DendoRank
{
    public partial class dendoUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.QueryString.HasKeys())
            {
                String PageSize = Page.Request.QueryString.Get("Pagesize");
                if (PageSize.Length != 0)
                {
                    int i;
                    if (Int32.TryParse(PageSize, out i))
                    {
                        GridView1.PageSize = i;
                    }
                }
            }
            ObjectDataSource1.Select();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //object o = e.Keys.Values
            int i = e.RowIndex;
            GridViewRow selectRow = GridView1.Rows[i];
            ObjectDataSource1.DeleteParameters["ROWID"].DefaultValue = selectRow.Cells[3].Text;
        }

        protected void ObjectDataSource1_DataBinding(object sender, EventArgs e)
        {
            
        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
    }
}