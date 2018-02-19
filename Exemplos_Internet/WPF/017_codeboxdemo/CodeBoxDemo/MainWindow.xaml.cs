using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using CodeBoxControl;
using CodeBoxControl.Decorations ;
using System.Windows.Markup;
namespace CodeBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbSize.ItemsSource = new double[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
       Debug.WriteLine(XamlWriter.Save(txtTest.Template));
        }

        private void btSQL_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch swatch = new Stopwatch();
            swatch.Start();
            this.txtTest.FontSize = 12;
            this.txtTest.Decorations.Clear();

            // Color Built in functions Magenta
            MultiRegexWordDecoration builtInFunctions = new MultiRegexWordDecoration();
            builtInFunctions.Brush = new SolidColorBrush(Colors.Magenta);
            builtInFunctions.Words.AddRange(GetBuiltInFunctions());
            this.txtTest.Decorations.Add(builtInFunctions);

            //Color global variables Magenta
            MultiStringDecoration globals = new MultiStringDecoration();
            globals.Brush = new SolidColorBrush(Colors.Magenta);
            globals.Strings.AddRange(GetGlobalVariables());
            this.txtTest.Decorations.Add(globals);

            //Color most reserved words blue
            MultiRegexWordDecoration bluekeyWords = new MultiRegexWordDecoration();
            bluekeyWords.Brush = new SolidColorBrush(Colors.Blue);
            bluekeyWords.Words.AddRange(GetBlueKeyWords());
            this.txtTest.Decorations.Add(bluekeyWords);

            MultiRegexWordDecoration grayKeyWords = new MultiRegexWordDecoration();
            grayKeyWords.Brush = new SolidColorBrush(Colors.Gray);
            grayKeyWords.Words.AddRange(GetGrayKeyWords());
            this.txtTest.Decorations.Add(grayKeyWords);

            MultiRegexWordDecoration dataTypes = new MultiRegexWordDecoration();
            dataTypes.Brush = new SolidColorBrush(Colors.Blue);
            dataTypes.Words.AddRange(GetDataTypes());
            this.txtTest.Decorations.Add(dataTypes);

            
            MultiRegexWordDecoration systemViews = new MultiRegexWordDecoration();
            systemViews.Brush = new SolidColorBrush(Colors.Green );
            systemViews.Words.AddRange(GetSystemViews());
            this.txtTest.Decorations.Add(systemViews);

            MultiStringDecoration operators = new MultiStringDecoration();
            operators.Brush = new SolidColorBrush(Colors.Gray);
            operators.Strings.AddRange(GetOperators());
            this.txtTest.Decorations.Add(operators);


            RegexDecoration quotedText = new RegexDecoration();
            quotedText.Brush = new SolidColorBrush(Colors.Red);
            quotedText.RegexString = "'.*?'";
            this.txtTest.Decorations.Add(quotedText);

            RegexDecoration nQuote = new RegexDecoration();
            //nQuote.DecorationType = EDecorationType.TextColor;
            nQuote.Brush = new SolidColorBrush(Colors.Red);
            nQuote.RegexString = "N''";
            this.txtTest.Decorations.Add(nQuote);


            //Color single line comments green
            RegexDecoration singleLineComment = new RegexDecoration();
            singleLineComment.DecorationType = EDecorationType.TextColor;
            singleLineComment.Brush = new SolidColorBrush(Colors.Green);
            singleLineComment.RegexString = "--.*";
            this.txtTest.Decorations.Add(singleLineComment);

            //Color multiline comments green
            RegexDecoration multiLineComment = new RegexDecoration();
            multiLineComment.DecorationType = EDecorationType.TextColor ;
            multiLineComment.Brush = new SolidColorBrush(Colors.Green);
            multiLineComment.RegexString = @"(?s:/\*.*?\*/)";
            this.txtTest.Decorations.Add(multiLineComment);

            this.txtTest.Text = StoredProcedureText();
            this.TimeReport.Text = swatch.Elapsed.TotalSeconds.ToString() + " seconds to load";
            swatch.Stop();

            
        }


        #region String data for text coloring
        public string[] GetBuiltInFunctions ()
        {
            string[] funct = { "parsename", "db_name", "object_id", "count", "ColumnProperty", "LEN",
                             "CHARINDEX" ,"isnull" , "SUBSTRING" };
            return funct;

        }

        public string[] GetGlobalVariables()
        {

            string[] globals = { "@@fetch_status" };
            return globals;

        }

        public string[] GetDataTypes()
        {
            string[] dt = { "int", "sysname", "nvarchar", "char" };
            return dt;

        }


        public string[] GetBlueKeyWords() // List from 
        {
            string[] res = {"ADD","EXISTS","PRECISION","ALL","EXIT","PRIMARY","ALTER","EXTERNAL",
                            "PRINT","FETCH","PROC","ANY","FILE","PROCEDURE","AS","FILLFACTOR",
                            "PUBLIC","ASC","FOR","RAISERROR","AUTHORIZATION","FOREIGN","READ","BACKUP",
                            "FREETEXT","READTEXT","BEGIN","FREETEXTTABLE","RECONFIGURE","BETWEEN","FROM",
                            "REFERENCES","BREAK","FULL","REPLICATION","BROWSE","FUNCTION","RESTORE",
                            "BULK","GOTO","RESTRICT","BY","GRANT","RETURN","CASCADE","GROUP","REVERT",
                            "CASE","HAVING","REVOKE","CHECK","HOLDLOCK","RIGHT","CHECKPOINT","IDENTITY",
                            "ROLLBACK","CLOSE","IDENTITY_INSERT","ROWCOUNT","CLUSTERED","IDENTITYCOL",
                            "ROWGUIDCOL","COALESCE","IF","RULE","COLLATE","IN","SAVE","COLUMN","INDEX",
                            "SCHEMA","COMMIT","INNER","SECURITYAUDIT","COMPUTE","INSERT","SELECT",
                            "CONSTRAINT","INTERSECT","SESSION_USER","CONTAINS","INTO","SET","CONTAINSTABLE",
                            "SETUSER","CONTINUE","JOIN","SHUTDOWN","CONVERT","KEY","SOME","CREATE",
                            "KILL","STATISTICS","CROSS","LEFT","SYSTEM_USER","CURRENT","LIKE","TABLE",
                            "CURRENT_DATE","LINENO","TABLESAMPLE","CURRENT_TIME","LOAD","TEXTSIZE",
                            "CURRENT_TIMESTAMP","MERGE","THEN","CURRENT_USER","NATIONAL","TO","CURSOR",
                            "NOCHECK","TOP","DATABASE","NONCLUSTERED","TRAN","DBCC","NOT","TRANSACTION",
                            "DEALLOCATE","NULL","TRIGGER","DECLARE","NULLIF","TRUNCATE","DEFAULT","OF",
                            "TSEQUAL","DELETE","OFF","UNION","DENY","OFFSETS","UNIQUE","DESC", "ON", 
                            "UNPIVOT","DISK","OPEN","UPDATE","DISTINCT","OPENDATASOURCE","UPDATETEXT",
                            "DISTRIBUTED","OPENQUERY","USE","DOUBLE","OPENROWSET","USER","DROP","OPENXML",
                            "VALUES","DUMP","OPTION","VARYING","ELSE","OR","VIEW","END","ORDER","WAITFOR",
                            "ERRLVL","OUTER","WHEN","ESCAPE","OVER","WHERE","EXCEPT","PERCENT","WHILE",
                            "EXEC","PIVOT","WITH","EXECUTE","PLAN","WRITETEXT", "GO", "ANSI_NULLS",
                            "NOCOUNT", "QUOTED_IDENTIFIER", "master"};

            return res;
        }


        public string[] GetGrayKeyWords()
        {
            string[] res = { "AND", "Null", "IS" };

            return res;

        }

        public string[] GetOperators()
        {
            string[] ops = { "=", "+", ".", ",", "-", "(", ")", "*", "<", ">" };

            return ops;

        }

        public string[] GetSystemViews()
        {
            string[] views = { "syscomments", "sysobjects", "sys.syscomments" };
            return views;
        }

#endregion

        public string StoredProcedureText()
        {

            return @"USE [master]
GO
/****** Object:  StoredProcedure [sys].[sp_helptext]    Script Date: 03/04/2009 14:49:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [sys].[sp_helptext]
@objname nvarchar(776)
,@columnname sysname = NULL
as

set nocount on

declare @dbname sysname
,@objid	int
,@BlankSpaceAdded   int
,@BasePos       int
,@CurrentPos    int
,@TextLength    int
,@LineId        int
,@AddOnLen      int
,@LFCR          int --lengths of line feed carriage return
,@DefinedLength int

/* NOTE: Length of @SyscomText is 4000 to replace the length of
** text column in syscomments.
** lengths on @Line, #CommentText Text column and
** value for @DefinedLength are all 255. These need to all have
** the same values. 255 was selected in order for the max length
** display using down level clients
*/
,@SyscomText	nvarchar(4000)
,@Line          nvarchar(255)

select @DefinedLength = 255
select @BlankSpaceAdded = 0 /*Keeps track of blank spaces at end of lines. Note Len function ignores
                             trailing blank spaces*/
CREATE TABLE #CommentText
(LineId	int
 ,Text  nvarchar(255) collate database_default)

/*
**  Make sure the @objname is local to the current database.
*/
select @dbname = parsename(@objname,3)
if @dbname is null
	select @dbname = db_name()
else if @dbname <> db_name()
        begin
                raiserror(15250,-1,-1)
                return (1)
        end

/*
**  See if @objname exists.
*/
select @objid = object_id(@objname)
if (@objid is null)
        begin
		raiserror(15009,-1,-1,@objname,@dbname)
		return (1)
        end

-- If second parameter was given.
if ( @columnname is not null)
    begin
        -- Check if it is a table
        if (select count(*) from sys.objects where object_id = @objid and type in ('S ','U ','TF'))=0
            begin
                raiserror(15218,-1,-1,@objname)
                return(1)
            end
        -- check if it is a correct column name
        if ((select 'count'=count(*) from sys.columns where name = @columnname and object_id = @objid) =0)
            begin
                raiserror(15645,-1,-1,@columnname)
                return(1)
            end
    if (ColumnProperty(@objid, @columnname, 'IsComputed') = 0)
		begin
			raiserror(15646,-1,-1,@columnname)
			return(1)
		end

        declare ms_crs_syscom  CURSOR LOCAL
        FOR select text from syscomments where id = @objid and encrypted = 0 and number =
                        (select column_id from sys.columns where name = @columnname and object_id = @objid)
                        order by number,colid
        FOR READ ONLY

    end
else if @objid < 0	-- Handle system-objects
	begin
		-- Check count of rows with text data
		if (select count(*) from master.sys.syscomments where id = @objid and text is not null) = 0
			begin
				raiserror(15197,-1,-1,@objname)
				return (1)
			end
			
		declare ms_crs_syscom CURSOR LOCAL FOR select text from master.sys.syscomments where id = @objid
			ORDER BY number, colid FOR READ ONLY
	end
else
    begin
        /*
        **  Find out how many lines of text are coming back,
        **  and return if there are none.
        */
        if (select count(*) from syscomments c, sysobjects o where o.xtype not in ('S', 'U')
            and o.id = c.id and o.id = @objid) = 0
                begin
                        raiserror(15197,-1,-1,@objname)
                        return (1)
                end

        if (select count(*) from syscomments where id = @objid and encrypted = 0) = 0
                begin
                        raiserror(15471,-1,-1,@objname)
                        return (0)
                end

		declare ms_crs_syscom  CURSOR LOCAL
		FOR select text from syscomments where id = @objid and encrypted = 0
				ORDER BY number, colid
		FOR READ ONLY

    end

/*
**  else get the text.
*/
select @LFCR = 2
select @LineId = 1


OPEN ms_crs_syscom

FETCH NEXT from ms_crs_syscom into @SyscomText

WHILE @@fetch_status >= 0
begin

    select  @BasePos    = 1
    select  @CurrentPos = 1
    select  @TextLength = LEN(@SyscomText)

    WHILE @CurrentPos  != 0
    begin
        --Looking for end of line followed by carriage return
        select @CurrentPos =   CHARINDEX(char(13)+char(10), @SyscomText, @BasePos)

        --If carriage return found
        IF @CurrentPos != 0
        begin
            /*If new value for @Lines length will be > then the
            **set length then insert current contents of @line
            **and proceed.
            */
            while (isnull(LEN(@Line),0) + @BlankSpaceAdded + @CurrentPos-@BasePos + @LFCR) > @DefinedLength
            begin
                select @AddOnLen = @DefinedLength-(isnull(LEN(@Line),0) + @BlankSpaceAdded)
                INSERT #CommentText VALUES
                ( @LineId,
                  isnull(@Line, N'') + isnull(SUBSTRING(@SyscomText, @BasePos, @AddOnLen), N''))
                select @Line = NULL, @LineId = @LineId + 1,
                       @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
            end
            select @Line    = isnull(@Line, N'') + isnull(SUBSTRING(@SyscomText, @BasePos, @CurrentPos-@BasePos + @LFCR), N'')
            select @BasePos = @CurrentPos+2
            INSERT #CommentText VALUES( @LineId, @Line )
            select @LineId = @LineId + 1
            select @Line = NULL
        end
        else
        --else carriage return not found
        begin
            IF @BasePos <= @TextLength
            begin
                /*If new value for @Lines length will be > then the
                **defined length
                */
                while (isnull(LEN(@Line),0) + @BlankSpaceAdded + @TextLength-@BasePos+1 ) > @DefinedLength
                begin
                    select @AddOnLen = @DefinedLength - (isnull(LEN(@Line),0) + @BlankSpaceAdded)
                    INSERT #CommentText VALUES
                    ( @LineId,
                      isnull(@Line, N'') + isnull(SUBSTRING(@SyscomText, @BasePos, @AddOnLen), N''))
                    select @Line = NULL, @LineId = @LineId + 1,
                        @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
                end
                select @Line = isnull(@Line, N'') + isnull(SUBSTRING(@SyscomText, @BasePos, @TextLength-@BasePos+1 ), N'')
                if LEN(@Line) < @DefinedLength and charindex(' ', @SyscomText, @TextLength+1 ) > 0
                begin
                    select @Line = @Line + ' ', @BlankSpaceAdded = 1
                end
            end
        end
    end

	FETCH NEXT from ms_crs_syscom into @SyscomText
end

IF @Line is NOT NULL
    INSERT #CommentText VALUES( @LineId, @Line )

select Text from #CommentText order by LineId

CLOSE  ms_crs_syscom
DEALLOCATE 	ms_crs_syscom

DROP TABLE 	#CommentText

return (0) -- sp_helptext";
        }


        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch swatch = new Stopwatch();
            swatch.Start();
            this.txtTest.Text = "";
            this.TimeReport.Text = swatch.Elapsed.TotalSeconds.ToString() + " seconds to clear";
            swatch.Stop();

        }

        private void btStyles_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch swatch = new Stopwatch();
            swatch.Start();
            this.txtTest.FontSize = 24;
            this.txtTest.Text = "TextColor,\n\nHilight,\n\nUnderline,\n\nStrikethrough";
            this.txtTest.Decorations.Clear();

          
            StringDecoration textColor = new StringDecoration();
            textColor.DecorationType = EDecorationType.TextColor;
            textColor.Brush = new SolidColorBrush(Colors.DarkOrchid);
            textColor.String = "TextColor";
            textColor.StringComparison = StringComparison.CurrentCulture;
            this.txtTest.Decorations.Add(textColor);

          
            StringDecoration hilight = new StringDecoration();
            hilight.DecorationType = EDecorationType.Hilight;
            hilight.Brush = new SolidColorBrush(Colors.Yellow );
            hilight.String = "Hilight";
            hilight.StringComparison = StringComparison.CurrentCulture;
            this.txtTest.Decorations.Add(hilight);



            StringDecoration underline = new StringDecoration();
            underline.DecorationType = EDecorationType.Underline;
            underline.Brush = new SolidColorBrush(Colors.Green );
            underline.String = "Underline";
            underline.StringComparison = StringComparison.CurrentCulture;
            this.txtTest.Decorations.Add(underline);

            // Color TextColor DarkOrchid
            StringDecoration strikethru = new StringDecoration();
            strikethru.DecorationType = EDecorationType.Strikethrough;
            strikethru.Brush = new SolidColorBrush(Colors.Red );
            strikethru.String = "Strikethrough";
            strikethru.StringComparison = StringComparison.CurrentCulture;
            this.txtTest.Decorations.Add(strikethru);


         this.TimeReport.Text = swatch.Elapsed.TotalSeconds.ToString() + " seconds to load";
            swatch.Stop();

        }

     
        
    }
}
