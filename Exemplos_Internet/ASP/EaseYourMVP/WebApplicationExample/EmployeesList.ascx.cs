using System;
using System.Web.UI;
using Research.MVP.Web;
using Research.MVP.ViewsImpl;
using Research.MVP.PresentersImpl;
using System.Web.UI.WebControls;

namespace Research.MVP.WebApplicationExample
{

    public partial class EmployeesList : MVPUserControl<EmployeesListPresenter, IEmployeesListView>, IEmployeesListView
    {

        #region IEmployeesListView Members

        public object Source
        {
            set 
            {
                this.GridView1.DataSource = value;
                this.GridView1.DataBind();
            }
        }

        public string DateTimeFormat
        {
            set 
            {
                foreach (DataControlField field in this.GridView1.Columns)
                {
                    BoundField boundField = field as BoundField;
                    if ((null != boundField) && (("BirthDate" == boundField.DataField) || ("HiredDate" == boundField.DataField)))
                    {
                        boundField.DataFormatString = value ?? string.Empty;
                    }
                }
            }
        }

        #endregion

    }

}
