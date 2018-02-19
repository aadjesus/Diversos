using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Web.Compilation;
using System.Xml;
using System.Xml.XPath;
using System.Web.UI.WebControls;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Web.UI;
using System.Globalization;

namespace UNLV.IAP.GridThemes
{
    public class GridThemesBuildProvider : BuildProvider
    {
        #region constants and private fields 

        protected const string kNAMESPACE = "UNLV.IAP.GridThemes";
        protected const string kCLASS = "GridThemesMethods";

        private AssemblyBuilder _assemblyBuilder;
        private CodeTypeDeclaration _class = null;

        // special variable declarations
        private bool _useCells = false;
        private bool _useCellValue = false;
        private bool _useCellText = false;
        private bool _useCellIndex = false;
        private bool _useRowIndex = false;

        private bool _useIsNumeric = false;
        private bool _useIsNegative = false;
        private bool _useIsPositive = false;
        private bool _useIsZero = false;

        private bool _useIsNotNumeric = false;
        private bool _useIsNotNegative = false;
        private bool _useIsNotPositive = false;
        private bool _useIsNotZero = false;

        #endregion

        #region BuildProvider support

        // ---------------------------------------------------------------------
        // GenerateCode method
        // ---------------------------------------------------------------------
        // Override BuildProvider.GenerateCode; this provides the CodeCompileUnit
        // that this builder creates
        // ---------------------------------------------------------------------
        public override void GenerateCode(AssemblyBuilder assemblyBuilder)
        {
            _assemblyBuilder = assemblyBuilder;

            // call the base method
            base.GenerateCode(assemblyBuilder);

            // now create the CodeCompileUnit that will include the class
            // definition for GridThemesMethods, a class that contains
            // methods generated from the XML builder file.
            CodeCompileUnit code = new CodeCompileUnit();

            // identify the namespace for this new class
            CodeNamespace ns = new CodeNamespace(kNAMESPACE);
            code.Namespaces.Add(ns);

            // define imports for this class
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));

            // specify the class GridThemesMethods as partial
            CodeTypeDeclaration cls = new CodeTypeDeclaration(kCLASS);
            cls.IsClass = true;
            cls.IsPartial = true;
            cls.Attributes = MemberAttributes.Public;
            //cls.Attributes = MemberAttributes.Static;
            ns.Types.Add(cls);
            _class = cls;

            // load the theme file as an xmlDocument
            XmlDocument doc = new XmlDocument();
            using (Stream gridThemeFile = this.OpenStream())
            {
                doc.Load(gridThemeFile);
            }

            // create methods for each found theme; allow some degree
            // of case-insensitivity throughout
            XmlNodeList nodes = doc.SelectNodes("//Theme | //theme");
            foreach (XmlNode node in nodes)
            {
                CodeMemberMethod method = CreateGridViewEventHandlingMethod(node);
                cls.Members.Add(method);
            }

            // finally, add the partial class to the assembly builder
            assemblyBuilder.AddCodeCompileUnit(this, code);
        }

        #endregion

        #region Support for Special Variables

        // ---------------------------------------------------------------------
        // SpecialVariableDeclarations method
        // ---------------------------------------------------------------------
        // Create special variable declarations, if necessary, such as:
        // IsNumeric, CellIndex, CellValue, IsNegative...
        // ---------------------------------------------------------------------
        private CodeStatementCollection SpecialVariableDeclarations()
        {
            // add variable declarations if they are to be used elsewhere in the script
            CodeStatementCollection coll = new CodeStatementCollection();

            // Cells - the e.Row.Cells collection ************************************
            if (_useCells)
            {
                coll.Add(new CodeVariableDeclarationStatement(typeof(TableCellCollection), "Cells"
                    , new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Row"), "Cells")
                    ));
            }
                
            // CellText - the text value of the cell ************************************
            if (_useCellText)
            {
                coll.Add(new CodeVariableDeclarationStatement(typeof(string), "CellText"
                    , new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("cell"), "Text")
                    ));
            }

            // CellIndex - the index value of the cell ************************************
            if (_useCellIndex)
            {
                coll.Add(new CodeVariableDeclarationStatement(typeof(int), "CellIndex"
                    , new CodeVariableReferenceExpression("iCell")
                    ));
            }

            // RowIndex - the index value of the row ************************************
            if (_useRowIndex)
            {
                coll.Add(new CodeVariableDeclarationStatement(typeof(int), "RowIndex"
                    , new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Row"), "RowIndex")
                    ));
            }


            // IsNumeric - true if the cell value is numeric ************************************
            if (_useCellValue || _useIsNumeric || _useIsNegative || _useIsPositive || _useIsZero
                 || _useIsNotNumeric || _useIsNotNegative || _useIsNotPositive || _useIsNotZero)
            {
                // IsNumeric = Double.TestParse(cell.Text, out cellValue);
                coll.Add(new CodeVariableDeclarationStatement(typeof(Double), "CellValue"));
                CodeMethodInvokeExpression tryParse = new CodeMethodInvokeExpression(
                     new CodeTypeReferenceExpression(typeof(Double))
                    , "TryParse"
                    , new CodeExpression[] {new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("cell"),"Text")
                                           ,new CodeDirectionExpression(FieldDirection.Out, new CodeVariableReferenceExpression("CellValue"))
                                            }
                   );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsNumeric", tryParse));
            }


            // IsNegative - true if the cell value is numeric and less than 0, false if not
            if (_useIsNegative || _useIsNotNegative)
            {
                // IsNegative = (IsNumeric && cellValue < 0)
                CodeVariableReferenceExpression condition1 = new CodeVariableReferenceExpression("IsNumeric");
                CodeBinaryOperatorExpression condition2 = new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("CellValue")
                    , CodeBinaryOperatorType.LessThan
                    , new CodePrimitiveExpression(0)
                    );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsNegative",
                    new CodeBinaryOperatorExpression(condition1, CodeBinaryOperatorType.BooleanAnd, condition2)));
            }

            // IsPositive - true if the cell value is numeric and greater than 0, false if not
            if (_useIsPositive || _useIsNotPositive)
            {
                // IsPositive = (IsNumeric && cellValue > 0)
                CodeVariableReferenceExpression condition1 = new CodeVariableReferenceExpression("IsNumeric");
                CodeBinaryOperatorExpression condition2 = new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("CellValue")
                    , CodeBinaryOperatorType.GreaterThan
                    , new CodePrimitiveExpression(0)
                    );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsPositive",
                    new CodeBinaryOperatorExpression(condition1, CodeBinaryOperatorType.BooleanAnd, condition2)));
            }

            // IsZero - true if the cell value is numeric and equal to 0, false if not
            if (_useIsZero || _useIsNotZero)
            {
                // IsZero = (IsNumeric && cellValue == 0)
                CodeVariableReferenceExpression condition1 = new CodeVariableReferenceExpression("IsNumeric");
                CodeBinaryOperatorExpression condition2 = new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("CellValue")
                    , CodeBinaryOperatorType.ValueEquality
                    , new CodePrimitiveExpression(0)
                    );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsZero",
                    new CodeBinaryOperatorExpression(condition1, CodeBinaryOperatorType.BooleanAnd, condition2)));
            }

            // IsNotNumeric - true if the cell value is not numeric
            if (_useIsNotNumeric)
            {
                // IsNotNumeric = (IsNumeric == False);
                CodeBinaryOperatorExpression isNumericFalse = new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("IsNumeric")
                   , CodeBinaryOperatorType.ValueEquality
                   , new CodePrimitiveExpression(false)
                   );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsNotNumeric"
                           , isNumericFalse));
            }

            // IsNotNegative - true if the cell value is numeric and not negative
            if (_useIsNotNegative)
            {
                // IsNotNegative = (IsNumeric && (IsNegative == False))
                CodeBinaryOperatorExpression condition2 = new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("IsNegative")
                   , CodeBinaryOperatorType.ValueEquality
                   , new CodePrimitiveExpression(false)
                   );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsNotNegative"
                    , new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("IsNumeric")
                        , CodeBinaryOperatorType.BooleanAnd
                        , condition2
                      )
                    ));
            }

            // IsNotPositive - true if the cell value is numeric and not positive
            if (_useIsNotPositive)
            {
                // IsNotPositive = (IsNumeric && (IsPositive == False))
                CodeBinaryOperatorExpression condition2 = new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("IsPositive")
                   , CodeBinaryOperatorType.ValueEquality
                   , new CodePrimitiveExpression(false)
                   );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsNotPositive"
                    , new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("IsNumeric")
                        , CodeBinaryOperatorType.BooleanAnd
                        , condition2
                      )
                    ));
            }

            // IsNotZero - true if the cell value is numeric and not zero
            if (_useIsNotZero)
            {
                // IsNotZero = (IsNumeric && (IsZero == False))
                CodeBinaryOperatorExpression condition2 = new CodeBinaryOperatorExpression(
                    new CodeVariableReferenceExpression("IsZero")
                   , CodeBinaryOperatorType.ValueEquality
                   , new CodePrimitiveExpression(false)
                   );

                coll.Add(new CodeVariableDeclarationStatement(typeof(bool), "IsNotZero"
                    , new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("IsNumeric")
                        , CodeBinaryOperatorType.BooleanAnd
                        , condition2
                      )
                    ));
            }

            return coll;
        }

        // ---------------------------------------------------------------------
        // MarkSpecialVariablesNeeded method
        // ---------------------------------------------------------------------
        // based on the given condition or expression, determine if
        // a special variable is used and set its boolean flag accordingly
        // ---------------------------------------------------------------------
        private void MarkSpecialVariablesNeeded(string test)
        {
            // if special variables are indicated in the given condition, mark that we'll
            // need to define them in the script
            if (test.Contains("Cells")) _useCells = true;
            if (test.Contains("CellValue")) _useCellValue = true;
            if (test.Contains("CellText")) _useCellText = true;
            if (test.Contains("CellIndex")) _useCellIndex = true;
            if (test.Contains("RowIndex")) _useRowIndex = true;

            if (test.Contains("IsNumeric")) _useIsNumeric = true;
            if (test.Contains("IsNegative")) _useIsNegative = true;
            if (test.Contains("IsPositive")) _useIsPositive = true;
            if (test.Contains("IsZero")) _useIsZero = true;

            if (test.Contains("IsNotNumeric")) _useIsNotNumeric = true;
            if (test.Contains("IsNotNegative")) _useIsNotNegative = true;
            if (test.Contains("IsNotPositive")) _useIsNotPositive = true;
            if (test.Contains("IsNotZero")) _useIsNotZero = true;
        }

        #endregion

        #region XML parsing and CodeDOM generation

        // ---------------------------------------------------------------------
        // CreateGridViewEventHandlingMethod method
        // ---------------------------------------------------------------------
        // given the <theme> tag and its contents, generate a method
        // of the GridThemesMethods class that interprets conditions
        // and formatting instructions found in the theme; the new method matches
        // the signature expected by the GridView.RowDataBound event
        // ---------------------------------------------------------------------
        private CodeMemberMethod CreateGridViewEventHandlingMethod(XmlNode node)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            
            // determine the theme id and title (if specified; if not, use the themeID for the title)
            string themeId = (node.Attributes["id"] == null ? "" : node.Attributes["id"].Value);
            if (themeId == "") themeId = (node.Attributes["Id"] == null ? "" : node.Attributes["Id"].Value);
            if (themeId == "") themeId = (node.Attributes["ID"] == null ? "" : node.Attributes["ID"].Value);

            if (themeId == "")
                throw new GridThemesException("GridThemes must be defined with a <Theme> tag and a valid 'id' attribute.  For example:  <Theme id='myTheme'>...</Theme>");

            string title = (node.Attributes["title"] == null ? "" : node.Attributes["title"].Value);
            if (title == "") title = (node.Attributes["Title"] == null ? "" : node.Attributes["Title"].Value);
            if (title == "") title = themeId;

            // define the method signature and attributes for this to be a GridView.RowDataBound event handler
            method.Name = themeId.Replace(" ", "_");
            method.Attributes = MemberAttributes.Public;
            //method.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Object), "source"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(GridViewRowEventArgs), "e"));

            // add a custom GridThemeAttribute to identify this method as an evemt handler, so the
            // GridThemesManager may appropriately interpret its purpose
            method.CustomAttributes.Add(
                new CodeAttributeDeclaration(new CodeTypeReference(typeof(GridThemeAttribute))
                 , new CodeAttributeArgument[] { new CodeAttributeArgument(new CodeSnippetExpression("\"" + title + "\"")) })
                 );
            
            // now for the method body; start by declaring the iCell variable used in for/next loops throughout the code
            method.Statements.Add(new CodeVariableDeclarationStatement(typeof(int), "iCell", new CodePrimitiveExpression(0)));

            // continue by parsing any <group> tags that might be available
            method.Statements.AddRange(ParseForGroups(node, themeId));            

            // parse for non/group instructions now; create an outer for/next loop 
            // to add to the method body
            CodeIterationStatement outerForLoop = CreateCellIterationLoop();
            CodeStatementCollection loopStatements = ParseForCellIteration(node, themeId);
            // if there is only one statement here, it will be the "cell" variable declaration
            // and the loop will not be necessary;
            if (loopStatements.Count > 0)
            {
                outerForLoop.Statements.AddRange(loopStatements);
                method.Statements.Add(outerForLoop);
            }


            return method;
        }

        // ---------------------------------------------------------------------
        // ParseForCellIteration method
        // ---------------------------------------------------------------------
        // create a for/next loop that iterates over every cell in the row
        // and parse for conditions & formatting instructions according to the
        // rules within the given node or list of nodes
        // ---------------------------------------------------------------------
        private CodeStatementCollection ParseForCellIteration(XmlNode node, string themeId)
        {
            CodeStatementCollection coll = new CodeStatementCollection();

            // now parse the XmlNode for conditions and formatting instructions
            // within each potential row type; DataRow, Header, and Footer are supported
            CodeStatementCollection statementsDataRow = ParseForRowType(node, "DataRow", themeId);
            CodeStatementCollection statementsHeader = ParseForRowType(node, "Header", themeId);
            CodeStatementCollection statementsFooter = ParseForRowType(node, "Footer", themeId);

            // parse any If/Apply instructions that are at the root level of this theme
            CodeStatementCollection statementsThemeApply = ParseForCellFormat("Apply", node, themeId);
            CodeStatementCollection statementsThemeIf = ParseForIf(node, themeId);

            // now that parsing has taken place, we know if we need any special variables
            // (like "isNegative").  Add those variable definitions here, at the top of the script            
            coll.AddRange(SpecialVariableDeclarations());

            // add everything else
            coll.AddRange(statementsThemeApply);
            coll.AddRange(statementsThemeIf);
            coll.AddRange(statementsDataRow);
            coll.AddRange(statementsHeader);
            coll.AddRange(statementsFooter);

            return coll;
        }

      
        private CodeIterationStatement CreateCellIterationLoop()
        {
            // now for the method body; start with a for/next loop that iterates through each cell in the row
            // i.e.  for(int iCell=0; iCell<e.Row.Cells.Count; iCell++)
            CodeIterationStatement outerForLoop = new CodeIterationStatement();
            outerForLoop.InitStatement = new CodeAssignStatement(new CodeVariableReferenceExpression("iCell"), new CodePrimitiveExpression(0));
            outerForLoop.TestExpression = new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("iCell")
                         , CodeBinaryOperatorType.LessThan
                         , new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Row"), "Cells"), "Count")
                         );
            outerForLoop.IncrementStatement = new CodeAssignStatement(new CodeVariableReferenceExpression("iCell")
                           , new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("iCell"), CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1))
                           );

            // add local variables in the for loop for the table cell
            // i.e. TableCell cell = e.Row.Cells[i];
            outerForLoop.Statements.Add(
                new CodeVariableDeclarationStatement(typeof(TableCell), "cell"
                , new CodeIndexerExpression(new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Row"), "Cells")
                         , new CodeVariableReferenceExpression[] { new CodeVariableReferenceExpression("iCell") }
                         )
                    )
                 );

            return outerForLoop;
        }

        // ---------------------------------------------------------------------
        // ParseForRowType method
        // ---------------------------------------------------------------------
        // return a collection of code statements that interpret a given row type tag, 
        // such as <DataRow>, <Header>, or <Footer>
        // ---------------------------------------------------------------------
        private CodeStatementCollection ParseForRowType(XmlNode node, string rowType, string themeId)
        {
            CodeStatementCollection coll = new CodeStatementCollection();
            string selector = "";

            // allow for a certain degree of case insensitivity
            switch (rowType.ToLower())
            {
                case "datarow": selector = "DataRow | dataRow | datarow | Datarow"; break;
                case "header": selector = "Header | header"; break;
                case "footer": selector = "Footer | footer"; break;
                default: selector = rowType; break;
            }

            // look for tags of the given row type
            XmlNode nodeRowType = node.SelectSingleNode(selector);
            if (nodeRowType != null)
            {
                // create an outer if statement for this row type
                // e.g. if (e.Row.RowType == DataControlRowType.DataRow)
                CodeConditionStatement outerIf = new CodeConditionStatement();
                CodeExpression leftExp = new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Row"), "RowType");
                CodeExpression rightExp = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(DataControlRowType)), rowType);
                outerIf.Condition = new CodeBinaryOperatorExpression(
                     leftExp, CodeBinaryOperatorType.ValueEquality, rightExp);

                
                // generate cell formatting statements for any <Apply> nodes
                outerIf.TrueStatements.AddRange(
                    ParseForCellFormat("Apply", nodeRowType, themeId)
                    );

                // generate additional conditional statements for any <If> nodes
                outerIf.TrueStatements.AddRange(
                    ParseForIf(nodeRowType, themeId)
                    );


                // add the if block to the main loop
                coll.Add(outerIf);

            }

            return coll;
        }

        // ---------------------------------------------------------------------
        // ParseForGroups method
        // ---------------------------------------------------------------------
        // return a collection of code statements that interpret a given <Group>
        // ---------------------------------------------------------------------
        private CodeStatementCollection ParseForGroups(XmlNode node, string themeId)
        {
            CodeStatementCollection coll = new CodeStatementCollection();
            
            // allow for a certain degree of case insensitivity
            string selector = "Group | group";
            
            // look for group indicator tags
            XmlNodeList nodes = node.SelectNodes(selector);
            foreach (XmlNode nodeGroup in nodes)
            {
                // make sure we have a column="" attribute
                string col = (nodeGroup.Attributes["column"] == null ? "" : nodeGroup.Attributes["column"].Value);
                if (col == "")
                    col = (nodeGroup.Attributes["Column"] == null ? "" : nodeGroup.Attributes["Column"].Value);
                if (col == "")
                    throw new GridThemesException(
                        string.Format("A <Group> tag in the theme '{0}' is missing the required attribute 'column'", themeId)
                        );
                int iCol;
                if (!int.TryParse(col, out iCol))
                    throw new GridThemesException(
                        string.Format("A <Group> tag in the theme '{0}' has an invalid value for the attribute 'column'.  This attribute must be an integer.", themeId)
                        );

                // suppress repeating values for the group?
                string sup = (nodeGroup.Attributes["suppressRepeating"] == null ? "" : nodeGroup.Attributes["suppressRepeating"].Value);
                if (sup == "")
                    sup = (nodeGroup.Attributes["SuppressRepeating"] == null ? "" : nodeGroup.Attributes["SuppressRepeating"].Value);
                if (sup == "")
                    sup = (nodeGroup.Attributes["suppressrepeating"] == null ? "" : nodeGroup.Attributes["suppressrepeating"].Value);
                if (sup == "")
                    sup = "false";

                bool bSup;
                if (!bool.TryParse(sup, out bSup))
                    bSup = false;

                // get nodes for each alternateFormat in this group
                string afSel = "alternateFormat | alternateformat | AlternateFormat";
                XmlNodeList formatNodes = nodeGroup.SelectNodes(afSel);
                

                // add the requisite if statement to ensure we're only impacting datarows
                // if (e.Row.RowType == DataControlRowType.DataRow) {...} statement
                CodeConditionStatement outerIf = new CodeConditionStatement();
                CodeExpression leftExp = new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Row"), "RowType");
                CodeExpression rightExp = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(DataControlRowType)), "DataRow");
                outerIf.Condition = new CodeBinaryOperatorExpression(
                     leftExp, CodeBinaryOperatorType.ValueEquality, rightExp);

                outerIf.TrueStatements.AddRange(
                    CreateGroupStatements(iCol, bSup, formatNodes, themeId)
                    );


                // now apply any additional conditions or formatting instructions
                // within the <group> tag that are outside an alternateFormat tag
                CodeIterationStatement outerForLoop = CreateCellIterationLoop();
                outerForLoop.Statements.AddRange(ParseForCellIteration(nodeGroup, themeId));
                // if there is only one statement here, it will be the "cell" variable declaration
                // and the loop will not be necessary;
                if (outerForLoop.Statements.Count > 1)
                    outerIf.TrueStatements.Add(outerForLoop);


                coll.Add(outerIf);
                
            }

            return coll;
        }


        // ---------------------------------------------------------------------
        // CreateGroupStatements method
        // ---------------------------------------------------------------------
        // return a collection of code statements that create grouping code
        // ---------------------------------------------------------------------
        private CodeStatementCollection CreateGroupStatements(int columnIndex, bool suppressRepeating, XmlNodeList formatNodes, string themeId)
        {
            CodeStatementCollection coll = new CodeStatementCollection();

            // create fields for the grouping variables and assign directly 
            // to the class so they are outside the event handler
            string rthemeId = themeId.Replace(' ', '_');
            string groupTextVar = string.Format("{0}_groupText_{1}", rthemeId, columnIndex);
            string groupIndexVar = string.Format("{0}_groupIndex_{1}", rthemeId, columnIndex);
            string rowIndexWithinGroupVar = string.Format("{0}_rowIndexWithinGroup_{1}", rthemeId, columnIndex);
            string curGroupTextVar = string.Format("{0}_groupTextCur_{1}", rthemeId, columnIndex);

            CodeMemberField varGroupText = new CodeMemberField(typeof(string), groupTextVar);
            CodeMemberField varGroupIndex = new CodeMemberField(typeof(int), groupIndexVar);
            CodeMemberField varRowIndexWithinGroup = new CodeMemberField(typeof(int), rowIndexWithinGroupVar);
            varGroupText.InitExpression = new CodePrimitiveExpression("");
            varGroupIndex.InitExpression = new CodePrimitiveExpression(-1);
            varRowIndexWithinGroup.InitExpression = new CodePrimitiveExpression(0);
            _class.Members.Add(varGroupText);
            _class.Members.Add(varGroupIndex);
            _class.Members.Add(varRowIndexWithinGroup);


            // Now add code statements to handle the group; the statements should
            // create code that resembles the following:
            // --- start of code ---
            /*
            string catTextCurCell0 = e.Row.Cells[0].Text;
            if (catTextCurCell == col0_text)
            {
                // repeated cateogry; suppress repeated value?
                if (bSuppress)
                    e.Row.Cells[0].Text = "";
                
                rowIndexWithinGroup++;
            }
            else
            {
                // category change
                col0_number++;
                col0_text = catTextCurCell0;
                rowIndexWithinGroup = 0;
            }

            // set formatting?
            switch (col0_number % 2)
            {
                case 0:
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                    break;
                case 1:
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    break;
            }
            */
            // --- end of code ---

            // string catTextCurCell = e.Row.Cells[0].Text;
            CodeExpression cellTextExpression = new CodePropertyReferenceExpression(
                new CodeIndexerExpression(new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("e"), "Row"), "Cells")
                         , new CodePrimitiveExpression[] { new CodePrimitiveExpression(columnIndex) }
                         )
                , "Text");
            CodeVariableDeclarationStatement catTextCurCell = new CodeVariableDeclarationStatement(
                typeof(string), curGroupTextVar
                , cellTextExpression
               );
            coll.Add(catTextCurCell);

            //if (col0_curText == col0_text) ...
            CodeConditionStatement categoryIf = new CodeConditionStatement();
            CodeExpression leftExp = new CodeVariableReferenceExpression(groupTextVar);
            CodeExpression rightExp = new CodeVariableReferenceExpression(curGroupTextVar);
            categoryIf.Condition = new CodeBinaryOperatorExpression(
                 leftExp, CodeBinaryOperatorType.ValueEquality, rightExp);

            // true statements; potentially suppress the repeated category value?
            //       if (bSuppress)
            //            e.Row.Cells[0].Text = "";
            if (suppressRepeating)
            {
                CodeAssignStatement blankOutCellText = new CodeAssignStatement(
                    cellTextExpression
                    , new CodePrimitiveExpression("")
                   );
                categoryIf.TrueStatements.Add(blankOutCellText);
            }

            // true statements - increment the row index within group
            CodeAssignStatement incrementRowIndexWithinGroup = new CodeAssignStatement(
                new CodeVariableReferenceExpression(rowIndexWithinGroupVar)
               , new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression(rowIndexWithinGroupVar), CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1))
               );
            categoryIf.TrueStatements.Add(incrementRowIndexWithinGroup);

            // false statements upon the category change
            //      col0_number++;
            //      col0_text = catTextCurCell;
            //      col0_rowIndexWithinGroup = 0;
            CodeAssignStatement incrementGroupNumber = new CodeAssignStatement(
                new CodeVariableReferenceExpression(groupIndexVar)
               , new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression(groupIndexVar), CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1))
               );
            CodeAssignStatement setCategoryText = new CodeAssignStatement(
                new CodeVariableReferenceExpression(groupTextVar)
              , new CodeVariableReferenceExpression(curGroupTextVar)
              );
            CodeAssignStatement resetRowIndexWithinGroup = new CodeAssignStatement(
                new CodeVariableReferenceExpression(rowIndexWithinGroupVar)
                , new CodePrimitiveExpression(0)
               );

            categoryIf.FalseStatements.Add(incrementGroupNumber);
            categoryIf.FalseStatements.Add(setCategoryText);
            categoryIf.FalseStatements.Add(resetRowIndexWithinGroup);

            coll.Add(categoryIf);

            // add a variable definition for GroupIndex
            CodeVariableDeclarationStatement groupIndexAssignment = new CodeVariableDeclarationStatement(
                typeof(int), "GroupIndex"
                , new CodeVariableReferenceExpression(groupIndexVar)
               );
            coll.Add(groupIndexAssignment);

            // add a variable definition for GroupText
            CodeVariableDeclarationStatement groupTextAssignment = new CodeVariableDeclarationStatement(
                typeof(string), "GroupText"
                , new CodeVariableReferenceExpression(groupTextVar)
               );
            coll.Add(groupTextAssignment);

            // add a variable definition for RowIndexWithinGroup
            CodeVariableDeclarationStatement rowIndexWithinGroupAssignment = new CodeVariableDeclarationStatement(
                typeof(int), "RowIndexWithinGroup"
                , new CodeVariableReferenceExpression(rowIndexWithinGroupVar)
               );
            coll.Add(rowIndexWithinGroupAssignment);



            // now add group-based formatting
            // switch (col0_number % 2)
            // NOTE:  as CodeDOM has no equivalent of a switch statement, 
            // generate the equivalent using multiple if's
            CodeConditionStatement prevIf = null;
            for (int i = 0; i < formatNodes.Count; i++)
            {
                XmlNode formatNode = formatNodes[i];
                CodeConditionStatement formatIf = new CodeConditionStatement();
                CodeExpression leftMod = new CodeVariableReferenceExpression(groupIndexVar);
                CodeExpression rightMod = new CodePrimitiveExpression(formatNodes.Count);
                CodeBinaryOperatorExpression mod = new CodeBinaryOperatorExpression(
                     leftMod, CodeBinaryOperatorType.Modulus, rightMod);
                formatIf.Condition = new CodeBinaryOperatorExpression(
                    mod, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(i));

                // add the first if() directly to the collection;
                // each successive if() will be added as a false statement to the 
                // previous one
                if (prevIf == null)
                    coll.Add(formatIf);
                else
                    prevIf.FalseStatements.Add(formatIf);

                prevIf = formatIf;

                // finally, add the formatting instructions themselves
                CodeIterationStatement outerForLoop = CreateCellIterationLoop();
                outerForLoop.Statements.AddRange(ParseForCellIteration(formatNode, themeId));
                if (outerForLoop.Statements.Count > 0)
                    formatIf.TrueStatements.Add(outerForLoop);

            }

            return coll;
        }


        // ---------------------------------------------------------------------
        // ParseForIf method
        // ---------------------------------------------------------------------
        // return a collection of code statements that interpret an <If> tag
        // ---------------------------------------------------------------------
        private CodeStatementCollection ParseForIf(XmlNode outerNode, string themeId)
        {
            CodeStatementCollection coll = new CodeStatementCollection();
            XmlNodeList nodes = outerNode.SelectNodes("If | if");

            foreach (XmlNode node in nodes)
            {
                // is there a valid condition supplied as the "test" attribute?
                string test = (node.Attributes["test"] == null ? "" : node.Attributes["test"].Value);
                if (test == "")
                    test = (node.Attributes["Test"] == null ? "" : node.Attributes["Test"].Value);

                if (test == "")
                {
                    throw new GridThemesException(
                        string.Format("An <If> tag in the theme '{0}' is missing the required attribute 'test'", themeId)
                        );
                }

                // create a CodeDOM if statement to match the <If> tag condition;
                // the condition should be in the same syntax as the compiling language
                CodeConditionStatement ifBlock = new CodeConditionStatement();
                MarkSpecialVariablesNeeded(test);
                ifBlock.Condition = new CodeSnippetExpression(test);

                // parse within the <If> tag for "Apply" cell formatting instructions
                ifBlock.TrueStatements.AddRange(
                    ParseForCellFormat("Apply", node, themeId)
                    );

                // parse within the <If> tag for "ElseApply" instructions
                ifBlock.FalseStatements.AddRange(
                    ParseForCellFormat("ElseApply", node, themeId)
                    );

                // parse within the <If> tag for "Else" instructions
                ifBlock.FalseStatements.AddRange(
                    ParseForElse(node, themeId)
                    );

                // finally, parse for additional nested <If> tags
                ifBlock.TrueStatements.AddRange(
                    ParseForIf(node, themeId)
                    );
                
                coll.Add(ifBlock);

            }

            return coll;
        }

        // ---------------------------------------------------------------------
        // ParseForElse method
        // ---------------------------------------------------------------------
        // return a collection of code statements that interpret an <Else> tag
        // ---------------------------------------------------------------------
        private CodeStatementCollection ParseForElse(XmlNode outerNode, string themeId)
        {
            CodeStatementCollection coll = new CodeStatementCollection();
            XmlNodeList nodes = outerNode.SelectNodes("Else | else");
            foreach (XmlNode node in nodes)
            {
                // generate cell formatting statements for any <Apply> nodes
                coll.AddRange(
                    ParseForCellFormat("Apply", node, themeId)
                    );

                // generate additional conditional statements for any <If> nodes
                coll.AddRange(
                    ParseForIf(node, themeId)
                    );
            }

            return coll;

        }

        // ---------------------------------------------------------------------
        // ParseForCellFormat method
        // ---------------------------------------------------------------------
        // return a collection of code statements that interpret a tag that indicates
        // formatting for the given TableCell.  These could include <Apply> and <ElseApply>
        // tags
        // ---------------------------------------------------------------------
        private CodeStatementCollection ParseForCellFormat(string parseName, XmlNode outerNode, string themeId)
        {
            CodeStatementCollection coll = new CodeStatementCollection();

            // allow some degree of case-insensitivity
            string parseList = parseName + " | " + parseName.ToLower();
            if (parseName == "ElseApply") parseList += " | elseApply";

            XmlNodeList nodes = outerNode.SelectNodes(parseList);
            foreach (XmlNode node in nodes)
            {
                // parse the node attributes for formatting instructions
                ParseAttributesInFormattingNode(coll, node, themeId);

                // are there any child nodes?  if so, treat each as a property of a TableCell
                if (node.HasChildNodes)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                        ParseAttributesInFormattingNode(coll, node.ChildNodes[i], themeId);
                }

            }
            
            return coll;
        }

        // ---------------------------------------------------------------------
        // ParseAttributesInFormattingNode method
        // ---------------------------------------------------------------------
        // treat the attributes in the given node as formatting instructions for the current
        // TableCell.  The attributes may either be TableCell properties (like BackColor)
        // or a TableCell property with the word "Expression" tacked on (like BackColorExpression).
        // The former's values are interpreted as property values to apply.
        // The latter's are interpreted as literal code snippets that affect the property.
        // ---------------------------------------------------------------------
        private void ParseAttributesInFormattingNode(CodeStatementCollection coll, XmlNode node, string themeId)
        {
            Type typeForNode = null;

            // is this attribute's tag a main cell-formatting tag?
            if (node.Name.ToLower() == "apply" || node.Name.ToLower()=="elseapply")
                // then indicate our node type is a TableCell
                typeForNode = typeof(TableCell);
            else
            {
                // if not, assume the tag name is a TableCell object property and determine
                // its type.  <Font> would be a good example.
                PropertyInfo prop = (typeof(TableCell)).GetProperty(node.Name
                    , BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (prop == null)
                    throw new GridThemesException(string.Format(
                        "A formatting tag in theme '{1}' has an invalid child tag <{2}>.  Child tags must be valid TableCell properties."
                        , themeId, node.Name
                        ));

                typeForNode = prop.PropertyType;
            }

            // assume each attribute is an attribute of the determined type (either TableCell
            // or a sub-object).  Throw an exception if not.
            foreach (XmlAttribute att in node.Attributes)
            {
                // make sure the property name is legitimate
                if (!IsLegitimateProperty(typeForNode, att.Name))
                    throw new GridThemesException(string.Format(
                        "A formatting tag in theme '{0}' has an invalid attribute '{1}'.  Attributes must be valid TableCell properties, or subproperties of TableCell."
                        , themeId, att.Name
                        ));

                // if the name is legitimate, output using CodeDOM
                try
                {
                    string name = "";
                    // are we dealing directly with a tableCell property?
                    if (typeForNode == typeof(TableCell))
                        name = att.Name;
                    else
                        // if not, we're dealing with a subobject of the TableCell.
                        // handle it as though it were following the hyphenated syntax
                        name = node.Name + "-" + att.Name;

                    ProcessTableCellProperty(coll, name, att.Value);
                }
                catch (Exception ex)
                {
                    throw new GridThemesException(
                        string.Format("There was an error assigning the value '{0}' to the cell property '{1}' in theme '{2}'.  Inner exception: {3}"
                                     , att.Value, att.Name, themeId, ex.Message)
                        , ex);
                }


            }
        }              
        
        // ---------------------------------------------------------------------
        // ProcessTableCellProperty method
        // ---------------------------------------------------------------------
        // Add a code statement to the given collection that appropriately assignes the given value
        // to a property of the given name
        // ---------------------------------------------------------------------
        private void ProcessTableCellProperty(CodeStatementCollection coll, string name, string value)
        {
            // get a reference to the cell variable
            CodeVariableReferenceExpression cell = new CodeVariableReferenceExpression("cell");
            CodeExpression valueToAssign = null;
            CodePropertyReferenceExpression cellProp = null;
            bool isExpression = false;
            string sProp = name;

            // check for the special "xxxExpression" attributes; these are non-TableCell
            // attributes that allow for code expressions to be used in cell property assignments
            if (sProp.ToLower().EndsWith("expression"))
            {
                // strip "Expression" and keep the rest as the actual property name
                sProp = sProp.Substring(0, sProp.Length - 10); 
                isExpression = true;
            }

            // then check for any other TableCell property/subproperty
            // determine the property reference; 
            //take into account the potential for hyphenated subproperties

            string[] arr = sProp.Split('-');
            PropertyInfo prop = (typeof(TableCell)).GetProperty(arr[0]
                , BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            cellProp = new CodePropertyReferenceExpression(cell, prop.Name);
            for (int i = 1; i < arr.Length; i++)
            {
                prop = prop.PropertyType.GetProperty(arr[i]
                    , BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                cellProp = new CodePropertyReferenceExpression(cellProp, prop.Name);
            }

            // if using an expression, apply the value literally as a code snippet
            if (isExpression)
            {
                // treat the attribute value as a literal chunk of code
                valueToAssign = new CodeSnippetExpression(value);
                // also, check if special variables have been used
                MarkSpecialVariablesNeeded(value);
            }
            else
            {
                // otherwise convert the value to the appropriate property type if possible
                Object objValue = value;

                // Start with: an enum?
                if (prop.PropertyType.IsEnum)
                    objValue = Enum.Parse(prop.PropertyType, value, true);
                // Color object?
                else if (prop.PropertyType == typeof(System.Drawing.Color))
                    objValue = System.Drawing.ColorTranslator.FromHtml(value);
                // FontUnit?
                else if (prop.PropertyType == typeof(FontUnit))
                    objValue = FontUnit.Parse(value);
                // Unit struct?
                else if (prop.PropertyType == typeof(Unit))
                    objValue = Unit.Parse(value);
                // other object types, attempt to convert from the string value
                else
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(prop.PropertyType);
                    if (tc != null)
                    {
                        if (tc.CanConvertFrom(typeof(string)))
                            objValue = tc.ConvertFromString(value);
                    }
                }

                // Now create the CodeDOM expression that represents the 
                // value to assign to the given property
                valueToAssign = GetValueExpressionForAssignment(
                    prop, objValue, objValue.GetType());
            }

            // if we were successful in creating a CodeDOM expression for the
            // property value to assign, then create the assignment statement
            // e.g. cell.BackColor = System.Drawing.Color.Blue;
            if (valueToAssign != null)
            {
                CodeAssignStatement assign = new CodeAssignStatement(
                    cellProp, valueToAssign);

                coll.Add(assign);
            }
        }

        // ---------------------------------------------------------------------
        // GetValueExpressionForAssignment method
        // ---------------------------------------------------------------------
        // Determine the CodeDOM representation of the value we wish to assign to the
        // given property; this code borrows its structure from the .NET framework's 
        // GenerateExpressionForValue() method of the CodeDomUtility class.  This class
        // is used internally by the framework for parsing .aspx pages and was discovered
        // using Lutz Roeder's .NET Reflector application.
        // ---------------------------------------------------------------------
        private CodeExpression GetValueExpressionForAssignment(PropertyInfo prop, object value, Type t)
        {

            // inspect the property type; is it a plain string?
            if (t == typeof(string) && value is string)
                return new CodePrimitiveExpression(value);

            // is the property type another primitive?
            if (t.IsPrimitive)
                return new CodePrimitiveExpression(value);

            if (((prop == null) && (t == typeof(object)))
                && ((value == null) || value.GetType().IsPrimitive))
                return new CodePrimitiveExpression(value);
            
            // is the property an array?
            if (t.IsArray)
            {
                Array arr = (Array) value;
                CodeArrayCreateExpression exp = new CodeArrayCreateExpression();
                exp.CreateType = new CodeTypeReference(t.GetElementType());
                if (arr != null)
                {
                    foreach(object o in arr)
                        exp.Initializers.Add(GetValueExpressionForAssignment(null, o, t.GetElementType()));
                }
            }
            
            // is the property type a Type itself?
            if (t == typeof(Type))
                return new CodeTypeOfExpression((Type) value);


            // if we're still here, try to get a PropertyDescriptor object 
            // for this property
            PropertyDescriptorCollection pdc = null;
            if (prop != null)
                pdc = TypeDescriptor.GetProperties(prop.PropertyType);
            PropertyDescriptor pd = null;
            if (prop != null && pdc != null)
                pd = pdc.Find(prop.Name, true);


            // try to get a TypeConverter for this property type
            TypeConverter conv = null;
            if (pd != null)
                conv = pd.Converter;
            else
                conv = TypeDescriptor.GetConverter(t);

            // do we have a TypeConverter?
            if (conv != null)
            {
                // if so, try to get an InstanceDescriptor
                InstanceDescriptor inst = null;
                if (conv.CanConvertTo(typeof(InstanceDescriptor)))
                    inst = (InstanceDescriptor)conv.ConvertTo(value, typeof(InstanceDescriptor));

                // do we have an InstanceDescriptor?
                if (inst != null)
                {                    
                    // is this instance a Field?
                    if (inst.MemberInfo is FieldInfo)
                    {
                        return new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(inst.MemberInfo.DeclaringType.FullName)
                            , inst.MemberInfo.Name
                            );

                    }
                    // is this instance a Property?
                    else if (inst.MemberInfo is PropertyInfo)
                    {
                        return new CodePropertyReferenceExpression(
                             new CodeTypeReferenceExpression(inst.MemberInfo.DeclaringType.FullName)
                             , inst.MemberInfo.Name
                             );

                    }

                    else
                    {
                        object[] objArr = new object[inst.Arguments.Count];
                        inst.Arguments.CopyTo(objArr, 0);
                        CodeExpression[] expArr = new CodeExpression[objArr.Length];
                        
                        // is this instance a Method?
                        if (inst.MemberInfo is MethodInfo)
                        {
                            ParameterInfo[] paramArr = ((MethodInfo)inst.MemberInfo).GetParameters();
                            for (int i = 0; i < objArr.Length; i++)
                                expArr[i] = GetValueExpressionForAssignment(null, objArr[i], paramArr[i].ParameterType);

                            CodeMethodInvokeExpression exp = new CodeMethodInvokeExpression(
                                new CodeTypeReferenceExpression(inst.MemberInfo.DeclaringType.FullName)
                                , inst.MemberInfo.Name
                                , new CodeExpression[0]
                                );

                            foreach(CodeExpression expParam in expArr)
                                exp.Parameters.Add(expParam);

                            return new CodeCastExpression(t, exp);
                        }

                        // is this instance a type constructor?
                        else if (inst.MemberInfo is ConstructorInfo)
                        {
                            
                            ParameterInfo[] paramArr = ((ConstructorInfo)inst.MemberInfo).GetParameters();
                            for (int i = 0; i < objArr.Length; i++)
                                expArr[i] = GetValueExpressionForAssignment(null, objArr[i], paramArr[i].ParameterType);
                            
                            CodeObjectCreateExpression exp = new CodeObjectCreateExpression(
                                inst.MemberInfo.DeclaringType.FullName
                                , new CodeExpression[0]
                                );

                            foreach (CodeExpression expParam in expArr)
                                exp.Parameters.Add(expParam);

                            return exp;
                        }

                    }
                }
            }

            
            // still here?  try looking for a Parse method
            if (t.GetMethod("Parse", new Type[] { typeof(string), typeof(CultureInfo) }) != null)
            {
                string s = "";
                CodeMethodInvokeExpression exp = new CodeMethodInvokeExpression(
                    new CodeTypeReferenceExpression(t.FullName)
                    , "Parse"
                    , new CodeExpression[0]
                    );

                if (conv != null)
                    s = conv.ConvertToInvariantString(value);
                else
                    s = value.ToString();

                exp.Parameters.Add(new CodePrimitiveExpression(s));
                exp.Parameters.Add(new CodePropertyReferenceExpression(
                    new CodeTypeReferenceExpression(typeof(CultureInfo))
                    , "InvariantCulture")
                    );

                return exp;
            }

            // try for a "Parse" method without the cultureinfo
            if (t.GetMethod("Parse", new Type[] { typeof(string) }) != null)
            {
                CodeMethodInvokeExpression exp = new CodeMethodInvokeExpression(
                    new CodeTypeReferenceExpression(t.FullName)
                    , "Parse"
                    , new CodeExpression[0]
                    );

                exp.Parameters.Add(new CodePrimitiveExpression(value.ToString()));
                return exp;
            }

            // still nothing?  return null
            return null;

        }

        // ---------------------------------------------------------------------
        // IsLegitimateProperty method
        // ---------------------------------------------------------------------
        // Return true if the given name is a legitimate property of the given type;
        // Take into account the potential syntax of hypenated subproperties,
        // and special "___Expression" attributes
        // ---------------------------------------------------------------------
        private bool IsLegitimateProperty(Type t, string name)
        {
            // make sure the name listed is a legitimate property of the given type;
            // allow for hyphenated subproperties, and check them too

            string[] arr = name.Split('-');
            if (arr.Length > 0)
            {
                string sProp = arr[0];

                // allow for the special "xxxExpression" attributes
                if (sProp.ToLower().EndsWith("expression"))
                    sProp = sProp.Substring(0, sProp.Length - 10);

                // is the current string a valid property of type t?
                PropertyInfo prop = t.GetProperty(sProp,
                    BindingFlags.Public | BindingFlags.IgnoreCase
                    | BindingFlags.Instance
                    );

                if (prop == null)
                    return false;

                // subproperties?
                if (arr.Length > 1)
                {
                    string subPropName = String.Join("-", arr, 1, arr.Length - 1);
                    return IsLegitimateProperty(prop.PropertyType, subPropName);
                }

                return true;
            }

            return false;
        }

        #endregion

    }
}
