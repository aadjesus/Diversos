<%@ Page Language="C#" MasterPageFile="~/SampleGridThemes.master"%>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bChecked = CheckBox1.Checked;
        GridView1.Visible = bChecked;
        GridView2.Visible = !bChecked;

        if (!IsPostBack)
        {
            System.Data.DataTable t = DataProvider.CreateCategoryWithSubGroupsDataSource(5);
            GridView1.DataSource = t;
            GridView2.DataSource = t;

            GridView1.DataBind();
            GridView2.DataBind();
        }         
    }
</script>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text="Display With GridTheme Formatting" Checked="True"/>
    <br /><br />
    <asp:GridView ID="GridView1" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" GridLines="None">
    </asp:GridView>
    <asp:GridView ID="GridView2" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" GridLines="None">
    </asp:GridView>
    <br />    
    
    Some interesting effects are possible with multiple groups.  In this example,
    groups are defined for both column 0 and column 1.  The utility class
    <code>ColorUtility</code> is found in the App_Code folder; its <code>BlendColors</code>
    method is called as an expression within group 1 to adjust the background color 
    established by group 0.
                
<pre>  &lt;theme id="Group7" title="Grouping: Multiple groups"&gt;
    
    &lt;group column="0" suppressRepeating="true"&gt;
      &lt;if test="RowIndexWithinGroup==0"&gt;
        &lt;apply CssClass="topBorder" /&gt;
      &lt;/if&gt;

      &lt;alternateFormat&gt;
        &lt;apply BackColor="MediumSlateBlue"/&gt;
        &lt;if test='CellIndex == 0'&gt;
          &lt;apply Font-Bold='True' horizontalAlign='center'/&gt;
        &lt;/if&gt;
      &lt;/alternateFormat&gt;

      &lt;alternateFormat&gt;
        &lt;apply BackColor="DarkSeaGreen" /&gt;
        &lt;if test='CellIndex == 0'&gt;
          &lt;apply Font-Bold='True' horizontalAlign='center'/&gt;
        &lt;/if&gt;
      &lt;/alternateFormat&gt;

    &lt;/group&gt;
    
    &lt;group column="1" suppressRepeating="true" &gt;
      
      &lt;alternateFormat&gt;
        &lt;!-- use an expression to adjust the color --&gt;
        &lt;if test="CellIndex != 0"&gt;
          &lt;apply backColorExpression='ColorUtility.BlendColors(cell.BackColor, "LightGray")' /&gt;
        &lt;/if&gt;        
      &lt;/alternateFormat&gt;

      &lt;alternateFormat&gt;
        &lt;!-- use an expression to adjust the color --&gt;
        &lt;if test="CellIndex != 0"&gt;
          &lt;apply backColorExpression='ColorUtility.BlendColors(cell.BackColor, "White")' /&gt;
        &lt;/if&gt;
      &lt;/alternateFormat&gt;
    &lt;/group&gt;
        
    
    &lt;if test="IsNumeric"&gt;
      &lt;apply horizontalAlign="right" /&gt;
    &lt;/if&gt;

    &lt;if test="CellIndex==1"&gt;
      &lt;Apply horizontalAlign="center" /&gt;
    &lt;/if&gt;

    &lt;header&gt;
      &lt;apply backColor="#999999" /&gt;
    &lt;/header&gt;

  &lt;/theme&gt;

</pre>            
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Grouping: Multiple groups" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
