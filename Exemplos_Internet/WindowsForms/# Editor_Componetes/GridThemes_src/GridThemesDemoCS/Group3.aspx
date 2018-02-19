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
    
    Additionally, formatting instructions that appear nested within the &lt;Group&gt;
    tag but outside any &lt;AlternateFormat&gt; tag are applied to all groups.
    In this example, alternate formatting for blue and green rows among groups is applied
    with &lt;AlternateFormat&gt; tags.  However, additional conditions are tested
    regardless of the group, and used to apply row heading and numeric formatting.
            
<pre>  &lt;Theme id="Group3" title="Grouping: Alternating Formats 2"&gt;
    &lt;Group column="0" suppressRepeating="true"&gt;
      &lt;if test="IsNumeric"&gt;
        &lt;apply horizontalAlign="right" /&gt;
      &lt;/if&gt;

      &lt;if test="CellIndex==0"&gt;
        &lt;apply Font-Bold="true" /&gt;
      &lt;/if&gt;
      
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
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Grouping: Alternating Formats 2" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
