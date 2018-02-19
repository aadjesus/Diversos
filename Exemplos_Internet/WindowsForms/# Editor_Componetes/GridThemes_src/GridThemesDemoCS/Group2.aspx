<%@ Page Language="C#" MasterPageFile="~/SampleGridThemes.master"%>

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
    
    One or more &lt;AlternateFormat&gt; tags may be supplied as children of the &lt;Group&gt;
    tag, each defining a block of conditions and formatting instructions.  Group 
    formatting rotates among all defined blocks.
            
<pre>  &lt;Theme id="Group2" title="Grouping: Alternating Formats"&gt;
    &lt;Group column="0" suppressRepeating="true"&gt;      
      &lt;AlternateFormat&gt;
        &lt;Apply backColor="LightBlue" /&gt;
      &lt;/AlternateFormat&gt;

      &lt;AlternateFormat&gt;
        &lt;Apply backColor="PaleGreen" /&gt;
      &lt;/AlternateFormat&gt;
    &lt;/Group&gt;

    &lt;Header&gt;
      &lt;Apply backColor="#CCCCCC" /&gt;
    &lt;/Header&gt;
  &lt;/Theme&gt;
</pre>            
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Grouping: Alternating Formats" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
