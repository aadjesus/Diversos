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
    This example shows a nested &lt;If&gt; tag, refining the formatting of numeric cells by displaying negative numbers in red.<br />
    <pre>  &lt;theme id="Conditions2" title="Sample: Nested If conditions"&gt;
    &lt;DataRow&gt;
      &lt;If test="IsNumeric"&gt;
        &lt;Apply horizontalAlign="right" /&gt;
        &lt;If test="IsNegative" &gt;
          &lt;Apply foreColor="Red" /&gt;
        &lt;/If&gt;
      &lt;/If&gt;
    &lt;/DataRow&gt;
  &lt;/theme&gt;
</pre>
    
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample: Nested If conditions" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
