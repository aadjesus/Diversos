using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PerformanceNHibernate1
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ASPxGridView1.DataBind

            FGlobus.Comum.ChaveCRUD.Conecta("ORA11G64", "SIITESTE");
            var x1 = new BLL.CadastroBO().Retornar();

            Label1.Text = x1.Count().ToString();


            var list = new BindingList<Globus5.Abastecimento.DTO.ClienteDTO>( x1.ToList());
            GridView1.DataSource = list;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //supposing id is the first cell,change the index according to your grid
            // hides the first column
            e.Row.Cells[0].Visible = false;

            //if (btn1Click)
            {
                //to set header text
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[1].Text = "Cell Text";
                    e.Row.Cells[2].Text = "Cell Text";
                }
            }
            //else if (btn2Click)
            //{
            //    //to set header text
            //    if (e.Row.RowType == DataControlRowType.Header)
            //    {
            //        e.Row.Cells[1].Text = "Cell Text";
            //        e.Row.Cells[2].Text = "Cell Text";
            //    }
            //}

        }
    }
}