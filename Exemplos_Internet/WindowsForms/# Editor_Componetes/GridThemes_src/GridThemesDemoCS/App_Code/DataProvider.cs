using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DataProvider
/// </summary>
public class DataProvider
{

    // Create the sample datasource for these exercises
    // this simulates a stored procedure that returns variable value columns
    public static DataTable CreateDataSource(int numValueColumns)
    {
        DataTable t = new DataTable();

        // create the table structure
        DataColumn c = new DataColumn();

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.String");
        c.ColumnName = "Category";
        t.Columns.Add(c);

        for (int i = 1; i <= numValueColumns; i++)
        {
            c = new DataColumn();
            c.DataType = System.Type.GetType("System.Int32");
            c.ColumnName = "Value" + i.ToString();
            t.Columns.Add(c);
        }

        // populate the table with some sample rows of data
        Random rnd = new Random();
        DataRow r = t.NewRow();
        r["Category"] = "North Region";
        for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10, 10);
        t.Rows.Add(r);

        r = t.NewRow();
        r["Category"] = "South Region";
        for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10000, 10000);
        t.Rows.Add(r);

        r = t.NewRow();
        r["Category"] = "East Region";
        for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10000, 10000);
        t.Rows.Add(r);

        r = t.NewRow();
        r["Category"] = "West Region";
        for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10000, 10000);
        t.Rows.Add(r);

        return t;
    }


    public static DataTable CreateCategoryDataSource(int numValueColumns)
    {
        DataTable t = new DataTable();

        // create the table structure
        DataColumn c = new DataColumn();

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.String");
        c.ColumnName = "Category";
        t.Columns.Add(c);

        for (int i = 1; i <= numValueColumns; i++)
        {
            c = new DataColumn();
            c.DataType = System.Type.GetType("System.Int32");
            c.ColumnName = "Value" + i.ToString();
            t.Columns.Add(c);
        }

        // populate the table with some sample rows of data
        Random rnd = new Random();
        DataRow r;
        int top = rnd.Next(2, 8);
        for (int j = 0; j <= top; j++)
        {
            r = t.NewRow();
            r["Category"] = "North Region";
            for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10, 10);
            t.Rows.Add(r);
        }

        top = rnd.Next(2, 8);
        for (int j = 0; j <= top; j++)
        {
            r = t.NewRow();
            r["Category"] = "South Region";
            for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10000, 10000);
            t.Rows.Add(r);
        }


        top = rnd.Next(2, 8);
        for (int j = 0; j <= top; j++)
        {
            r = t.NewRow();
            r["Category"] = "East Region";
            for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10000, 10000);
            t.Rows.Add(r);
        }

        top = rnd.Next(2, 8);
        for (int j = 0; j <= top; j++)
        {
            r = t.NewRow();
            r["Category"] = "West Region";
            for (int i = 1; i <= numValueColumns; i++) r["Value" + i.ToString()] = rnd.Next(-10000, 10000);
            t.Rows.Add(r);
        }


        return t;
    }

    public static DataTable CreateCategoryWithSubGroupsDataSource(int numValueColumns)
    {
        DataTable t = new DataTable();

        // create the table structure
        DataColumn c = new DataColumn();

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.String");
        c.ColumnName = "Category";
        t.Columns.Add(c);

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.String");
        c.ColumnName = "Subcategory";
        t.Columns.Add(c);

        for (int i = 1; i <= numValueColumns; i++)
        {
            c = new DataColumn();
            c.DataType = System.Type.GetType("System.Int32");
            c.ColumnName = "Value" + i.ToString();
            t.Columns.Add(c);
        }

        // populate the table with some sample rows of data
        CreateRegionRows(t, "North Region", new string[]{"Minnesota","North Dakota"});
        CreateRegionRows(t, "South Region", new string[] { "Louisiana", "Alabama", "Mississippi" });
        CreateRegionRows(t, "East Region", new string[] { "New York", "Maine" });
        CreateRegionRows(t, "West Region", new string[] { "California", "Nevada", "Oregon" });


        return t;
    }

    private static void CreateRegionRows(DataTable t, string category, string[] subCategories)
    {
        Random rnd = new Random();
        DataRow r;

        foreach (string s in subCategories)
        {
            int top = rnd.Next(1, 4);
            for (int j = 0; j <= top; j++)
            {
                r = t.NewRow();
                r["Category"] = category;
                r["SubCategory"] = s;
                for (int i = 2; i < t.Columns.Count; i++) r["Value" + (i-1).ToString()] = rnd.Next(-100, 100);
                t.Rows.Add(r);
            }
        }
    }

}
