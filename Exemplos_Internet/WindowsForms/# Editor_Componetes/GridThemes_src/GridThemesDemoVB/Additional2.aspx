<%@ Page Language="VB" MasterPageFile="~/SampleGridThemes.master"%>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<script runat="server">

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Dim numColumns As Integer = Convert.ToInt32(ddColumns.SelectedValue)
        Select Case (ddSource.SelectedValue)
            Case "0" ' simple data sample
                GridView1.DataSource = DataProvider.CreateDataSource(numColumns)
                
            Case "1" ' categorized data sample
                GridView1.DataSource = DataProvider.CreateCategoryDataSource(numColumns)
        End Select
        
        GridView1.DataBind()
    End Sub
</script>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    DataSource:  
    <asp:DropDownList ID="ddSource" runat="server" AutoPostBack="true">
        <asp:ListItem Text="Fewer Rows" Value="0" Selected="True" />
        <asp:ListItem Text="More Rows" Value="1" />
    </asp:DropDownList>
    
    &nbsp;&nbsp;&nbsp;
    Number of Value Columns:
    <asp:DropDownList ID="ddColumns" runat="server" AutoPostBack="true">
        <asp:ListItem Text="3" Value="3" />
        <asp:ListItem Text="4" Value="4" />
        <asp:ListItem Text="5" Value="5" Selected="True" />
        <asp:ListItem Text="6" Value="6" />
        <asp:ListItem Text="7" Value="7" />
    </asp:DropDownList>
    <br /><br />

    <asp:GridView ID="GridView1" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" GridLines="None">
    </asp:GridView>
    <br />
    
<pre>  &lt;theme id="Additional2" title="Sample: Alternating Rows and Columns"&gt;
    &lt;Header&gt;
      &lt;Apply BackColor="#999999" /&gt;
      &lt;If test="CellIndex = 0"&gt;
        &lt;Apply BackColor="#999999" /&gt;
        &lt;Else&gt;
          &lt;If test="CellIndex mod 2 = 0"&gt;
            &lt;Apply BackColor="#9999CC" /&gt;
            &lt;ElseApply BackColor="#6666CC" /&gt;
          &lt;/If&gt;
        &lt;/Else&gt;
      &lt;/If&gt;
    &lt;/Header&gt;

    &lt;DataRow&gt;
      &lt;If test="CellIndex = 0"&gt;
        &lt;Apply BackColor="#CCCCCC" Font-Bold="True" /&gt;
        &lt;Else&gt;
          &lt;Apply width="60px" horizontalAlign="right" /&gt;
          &lt;If test="CellIndex mod 2 = 0"&gt;
            &lt;Apply BackColor="#CCCCFF" /&gt;
            &lt;If test="IsNegative"&gt;
              &lt;Apply foreColor="Red" /&gt;
            &lt;/If&gt;

            &lt;Else&gt;
              &lt;Apply BackColor="#9999FF" /&gt;
              &lt;If test="IsNegative"&gt;
                &lt;Apply foreColor="Yellow" /&gt;
              &lt;/If&gt;
            &lt;/Else&gt;
          &lt;/If&gt;
        &lt;/Else&gt;
      &lt;/If&gt;

      &lt;If test="RowIndex mod 2 = 0"&gt;
        &lt;Apply BackColorExpression='ColorUtility.BlendColors(cell.BackColor, "#9C9C9C")' /&gt;
      &lt;/If&gt;

    &lt;/DataRow&gt;
  &lt;/theme&gt;
</pre>
    
    
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample: Alternating Rows and Columns" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
