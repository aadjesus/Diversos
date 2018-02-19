<%@ Page Language="VB" MasterPageFile="~/SampleGridThemes.master"%>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="CreateCategoryDataSource"
        TypeName="DataProvider">
        <SelectParameters>
            <asp:Parameter DefaultValue="5" Name="numValueColumns" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" GridLines="None">
    </asp:GridView>
    <br />
    
    Use the &lt;Group&gt; tag and required <i>column</i> attribute to specify grouping
    of rows based on repeated values in the given column (0-based index).  The optional
    &lt;suppressRepeating&gt; attribute may be supplied as <code>true</code> to indicate
    that group category text should only display in the first row of the group.  Note:  
    the data source should be pre-sorted based on the grouping column.
            
<pre>
  &lt;theme id="Group1" title="Grouping: Suppress Repeating"&gt;
    &lt;group column="0" suppressRepeating="true" /&gt;
  &lt;/theme&gt;
</pre>            
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Grouping: Suppress Repeating" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
