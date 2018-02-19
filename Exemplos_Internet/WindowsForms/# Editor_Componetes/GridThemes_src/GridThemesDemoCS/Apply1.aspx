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
    Individual GridThemes are defined using the &lt;Theme&gt; tag.
    Formatting instructions are issued using an &lt;Apply&gt; tag.
    Attributes of the &lt;Apply&gt; tag are interpreted as properties of a TableCell object.
    Without specifying conditions or a row type, the instruction is applied to all table cells.<br />
    <br />
    <pre>&lt;theme id="Apply1" title="Sample: Apply tag"&gt;
    &lt;apply backColor="#CCCCCC" foreColor="blue"&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;horizontalAlign="center"
           /&gt;<br />&lt;/theme&gt;
    </pre>
    
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample: Apply tag" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
