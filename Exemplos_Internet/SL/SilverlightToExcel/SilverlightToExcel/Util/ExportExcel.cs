using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;


namespace SilverlightToExcel
{

    /**
     * 
     * @author Robson
     * 
     */
	public class ExportExcel
	{
		public ExportExcel()
		{
		}

        /**
         * 
         * @param dataGrid
         * @return 
         * 
         */
        public string DgToHTML<T>(DataGrid dataGrid)
		{
				string html = "";
                System.Reflection.PropertyInfo propInfo;
                System.Windows.Data.Binding binding;
                string colPath;

            
            
                html+= "<table width='"+dataGrid.Width+"'><thead><tr width='"+dataGrid.Width+"' >";
                
                for(int i = 0;i<dataGrid.Columns.Count; i++)
                    if(dataGrid.Columns.ElementAt(i).Header != null && dataGrid.Columns.ElementAt(i).Visibility == Visibility.Visible) 
                        html+="<th >"+dataGrid.Columns.ElementAt(i).Header.ToString()+"</th>";
                
                html += "</tr></thead><tbody>";

                foreach (Object data in new List<T>((IEnumerable<T>)dataGrid.ItemsSource))
                {
                    html+="<tr width=\""+Math.Ceiling(dataGrid.Width)+"\">";

                    foreach (DataGridColumn col in dataGrid.Columns)
                    {
                        if (col is DataGridBoundColumn)
                        {
                            binding = (col as DataGridBoundColumn).Binding;
                            colPath = binding.Path.Path;
                            propInfo = data.GetType().GetProperty(colPath);
                            if (propInfo != null)
                            {
                                html += "<td nowrap=\"nowrap\"  width=\"" + Math.Ceiling(dataGrid.Width) + "\" >" + propInfo.GetValue(data, null).ToString()+"</td>";
                            }
                        }

                    }
                    html += "</tr>";
                }

                html+="</tbody></table>";
				
				return html;
		}
	}
}