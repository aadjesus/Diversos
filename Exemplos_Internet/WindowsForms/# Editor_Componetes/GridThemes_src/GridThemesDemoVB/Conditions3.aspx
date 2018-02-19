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
    This example demonstrates the use of &lt;Else&gt; and &lt;ElseApply&gt; within &lt;If&gt;
    tags.&nbsp; &lt;Else&gt; and &lt;ElseApply&gt; are processed if the initial &lt;If&gt;
    condition evaluates to false.&nbsp; In this first &lt;If&gt; test, if the given cell
    is numeric it is right-aligned; otherwise, it is left-aligned and bolded.&nbsp;
    In the second test, if the given cell is a negative number it is displayed in red;
    otherwise, as long as the cell is numeric, it is displayed in blue.<br />
    <pre>  &lt;theme id="Conditions3" title="Sample:  If/Else/ElseApply"&gt;
   &lt;DataRow&gt;
    &lt;If test="IsNumeric"&gt;
      &lt;Apply horizontalAlign="right" /&gt;
      &lt;ElseApply horizontalAlign="left" font-bold="true" /&gt;
    &lt;/If&gt;
    
    &lt;If test="IsNegative" &gt;
      &lt;Apply foreColor="Red" /&gt;
      &lt;Else&gt;
        &lt;If test="IsNumeric"&gt;
          &lt;Apply foreColor="Blue" /&gt;
        &lt;/If&gt;
      &lt;/Else&gt;
    &lt;/If&gt;    
   &lt;/DataRow&gt;
  &lt;/theme&gt;

</pre>
    
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample:  If/Else/ElseApply" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
