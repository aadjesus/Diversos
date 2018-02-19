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
    In addition to the names of TableCell properties, the attributes of the &lt;Apply&gt; tag
    may include tokens following the syntax <code><i>property</i>Expression</code>, where <i>property</i>
    is a valid TableCell property name.  These "expression" attributes are treated as 
    literal code expressions in the syntax of the project language.  For example, to supply
    a code expression for the <code>Text</code> property, include a <code>TextExpression</code>
    attribute in the &lt;Apply&gt; tag.
    <br />
    <br />
    In this example, line numbering and numeric formatting 
    is applied using the <code>string.Format()</code>
    method as a <code>TextExpression</code>.  For numbers, a boolean expression 
    (CellValue greater than 8000) is used to set the Font-Bold property.
<pre>
  &lt;theme id="Conditions4" title="Sample:  Expression attributes"&gt;
    &lt;DataRow&gt;
      &lt;If test="CellIndex == 0"&gt;
        &lt;Apply textExpression='string.Format("{0}: {1}", RowIndex + 1, CellText)' 
               backColor='#EFEFEF'
               width='120px'/&gt;
        
        &lt;Else&gt;
          &lt;Apply textExpression='string.Format("{0:#,##0.0}", CellValue)' 
                 font-boldExpression='(CellValue &gt; 8000)'
                 horizontalAlign="right"
                 width="60px"
                 /&gt;
         &lt;/Else&gt;
      &lt;/If&gt;
    &lt;/DataRow&gt;
    
    &lt;Header&gt;
      &lt;apply backColor="#CCCCCC" /&gt;
    &lt;/Header&gt;
  &lt;/theme&gt;
</pre>
    
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample:  Expression attributes" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
