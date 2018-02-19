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
    
    The GridThemes framework supports the application of formatting to groups of rows
    based on the value of the cells in a given column.  The data source used for
    these examples outputs multiple rows of random data per region.  The above grid
    has no associated GridTheme.
            
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
    </cc1:gridthemesextender>
    
</asp:Content>
