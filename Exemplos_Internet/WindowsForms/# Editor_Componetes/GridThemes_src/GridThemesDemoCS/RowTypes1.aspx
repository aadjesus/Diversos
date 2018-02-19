<%@ Page Language="C#" MasterPageFile="~/SampleGridThemes.master"%>

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
    The tags &lt;DataRow&gt;, &lt;Header&gt;, and &lt;Footer&gt; are used to specify
    that formatting should be applied based on a specific row type.&nbsp; In this example,
    header cells and datarow cells are formatted differently.<br />
    <pre>
&lt;theme id="RowTypes1" title="Sample: RowTypes"&gt;
  &lt;Header&gt;
    &lt;apply backColor="DarkBlue" foreColor="White" 
           horizontalAlign="center" 
           font-bold="true" font-italic="true" 
           /&gt;
  &lt;/Header&gt;

  &lt;DataRow&gt;
    &lt;apply backColor="#EFEFEF" foreColor="DarkBlue" /&gt;
  &lt;/DataRow&gt;
&lt;/theme&gt;     </pre>
    
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample: RowTypes" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
