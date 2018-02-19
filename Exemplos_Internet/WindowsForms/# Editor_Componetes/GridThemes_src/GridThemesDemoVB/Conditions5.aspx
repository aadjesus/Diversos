<%@ Page Language="VB" MasterPageFile="~/SampleGridThemes.master"%>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="CreateDataSource"
        TypeName="DataProvider">
        <SelectParameters>
            <asp:Parameter DefaultValue="5" Name="numValueColumns" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" GridLines="None">
    </asp:GridView>
    <br />
    The <code>Cells</code> collection is available in condition tests to access properties
    of any cell in the given row.  
    In this example, the Cells collection is used to hilite the row titled "East Region".
        
<pre>  &lt;theme id="Conditions5" title="Sample: Cells collection"&gt;
    &lt;DataRow&gt;
      &lt;If test='Cells(0).Text = "East Region" '&gt;
        &lt;Apply font-bold='True' /&gt;
        &lt;If test='CellIndex = 0'&gt;
          &lt;apply backColor='#93D8B2' foreColor='DarkGreen' /&gt;
          &lt;elseApply backColor='#ADFFD2' foreColor='DarkGreen' /&gt;
        &lt;/If&gt;
        &lt;Else&gt;
          &lt;If test='CellIndex = 0'&gt;
            &lt;apply backColor='#EFEFEF' /&gt;
            &lt;elseApply backColor='White' /&gt;
          &lt;/If&gt;
        &lt;/Else&gt;
      &lt;/If&gt;
            
      &lt;If test="IsNumeric"&gt;        
        &lt;Apply horizontalAlign="right" width="60px"/&gt;
        &lt;If test="IsNegative" &gt;
          &lt;Apply foreColor="Red" /&gt;
        &lt;/If&gt;       
      &lt;/If&gt;
    &lt;/DataRow&gt;

    &lt;Header&gt;
      &lt;apply BackColor="#CCCCCC" /&gt;
    &lt;/Header&gt;
  &lt;/theme&gt;
</pre>
    
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample: Cells collection" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
