using System;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WellFormed
{

	/// <summary>
	/// The range of styles that can be used to justify text
	/// </summary>
	public enum JustificationStyles
	{
		/// <summary>
		/// Text is left justified
		/// </summary>
		Left,
		/// <summary>
		/// Text is right justified
		/// </summary>
		Right,
		/// <summary>
		/// Test is centered between the margins
		/// </summary>
		Centered,
		/// <summary>
		/// Text is fully justified between the margins
		/// </summary>
		Justified
	}

	/// <summary>
	/// The types of whitespace that the text justifier recognizes
	/// </summary>
	public enum WhiteSpace
	{
		/// <summary>
		/// Not a whitespace type
		/// </summary>
		None,
		/// <summary>
		/// a space
		/// </summary>
		Space,
		/// <summary>
		/// a tab character
		/// </summary>
		Tab,
		/// <summary>
		/// a new line cr/lf combination
		/// </summary>
		NewLine,
		/// <summary>
		/// special type used to indent the first line of a paragraph
		/// </summary>
		Indent
	}

	/// <summary>
	/// A specialized collection class that manages WordPos objects
	/// </summary>
	public class WordPosCollection : CollectionBase
	{
		bool _containsNewline=false;
		bool _newParagraph=false;

		/// <summary>
		/// 
		/// </summary>
		public bool NewParagraph
		{
			get{return _newParagraph;}
			set{_newParagraph=value;}
		}

		/// <summary>
		/// Readonly property. when true, the formatted text contains a newline whitespace character
		/// </summary>
		public  bool ContainsNewline
		{
			get{return _containsNewline;}
		}

		/// <summary>
		/// Strongly typed method to add WordPos objects to the cllection.
		/// </summary>
		/// <param name="wp">The WordPos object to add</param>
		public void Add(WordPos wp)
		{
			if(wp.WhiteSpace==WhiteSpace.NewLine)
				_containsNewline=true;
			List.Add(wp);
		}

		/// <summary>
		/// Indexer that gets and sets WordPos objecs at a specified index within the collection
		/// </summary>
		public WordPos this[int index]
		{
			get{return (WordPos)List[index];}
			set
			{
				if(value.WhiteSpace==WhiteSpace.NewLine)
					this._containsNewline=true;
				List[index]=value;
			}
		}

		/// <summary>
		/// Removes the last WordPos object from the collection.
		/// </summary>
		public void RemoveLast()
		{
			List.RemoveAt(List.Count-1);
		}

		/// <summary>
		/// Returns the last WordPos object in the collection.
		/// </summary>
		/// <returns>The last WordPos object in the collection</returns>
		public WordPos Last()
		{
			if(List.Count==0)
				return null;
			return (WordPos)List[List.Count-1];
		}
	}

	/// <summary>
	/// WordPos is a description for a word and  poition.
	/// </summary>
	/// <remarks>
	/// The WordPos object maintains data about the linear position within the margins of a page, the physical width of a word, the font and its whitespace characteristics.
	/// </remarks>
	public class WordPos  : ICloneable
	{
		public float PagePos;
		public string Word;
		public float WordWidth;
		public WhiteSpace WhiteSpace;
		public Font Font;

		/// <summary>
		/// Constructor
		/// </summary>
		public WordPos()
		{
		}

		/// <summary>
		/// Constructor for a whitespace word position
		/// </summary>
		/// <param name="ws">The WhiteSpace type to use</param>
		public WordPos(WhiteSpace ws)
		{
			WhiteSpace = ws;
			PagePos=0;
			Word = "";
			WordWidth = 0;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ws">The WhiteSpace type</param>
		/// <param name="pp">Page position</param>
		/// <param name="word">The string definition of the word</param>
		/// <param name="ww">Word Width</param>
		/// <param name="font">the Font used to draw the word</param>
		protected WordPos(WhiteSpace ws, float pp, string word, float ww, Font font)
		{
			WhiteSpace = ws;
			PagePos=pp;
			Word = word;
			WordWidth = ww;
			Font=font;
		}
		
		/// <summary>
		/// Creates a string definition of this object for diagnostic purposes
		/// </summary>
		/// <returns>a string describing the contents of the WordPos object</returns>
		public override string ToString()
		{
			return string.Format("WordPos:\r\n"+
				"\tPagePos = {0}\r\n"+
				"\tWord = {1}\r\n"+
				"\tWordWidth = {2}\r\n"+
				"\tWhiteSpace = {3}\r\n",
				PagePos,
				Word,
				WordWidth,
				WhiteSpace);
		}

		/// <summary>
		/// Creates a copy of the object
		/// </summary>
		/// <returns>a copy of the current object</returns>
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// returns a copy of the object
		/// </summary>
		/// <returns>An exact copy of the object</returns>
		public WordPos Clone()
		{
			return new WordPos(WhiteSpace,	
				PagePos,
				Word,		
				WordWidth,	
				Font);	
		}

	}


	
	/// <summary>
	/// The Formatter takes a string of text and arranges the words so that they are justified within a specified set of boundaries
	/// </summary>
	public class Formatter
	{

		float _leftmargin;
		float _columnwidth;
		float _Pos;
		float _indent;
		float[] _tabs;
		float _tabstops;
		JustificationStyles _justify;
		string _text;
		WordPosCollection _words=new WordPosCollection();

		protected int WordIndex;

		/// <summary>
		/// The value by which to indent the first word in the paragraph
		/// </summary>
		public float Indent
		{
			get{return _indent;}
			set{_indent=value;}
		}

		/// <summary>
		/// The text to be formatted
		/// </summary>
		public string Text
		{
			get{return _text;}
			set{_text=value;}
		}

		/// <summary>
		/// The collection of word position objects that define the current text
		/// </summary>
		[Browsable(false)]
		public WordPosCollection Words
		{
			get{return _words;}
			set{_words=value;}
		}

		/// <summary>
		/// The style of justification used
		/// </summary>
		public JustificationStyles Justify
		{
			get{return _justify;}
			set{_justify=value;}
		}

		/// <summary>
		/// the value of the left margin. Normally 0  but may be used to add margins within a text area
		/// </summary>
		public float LeftMargin
		{
			get{return _leftmargin;}
			set{_leftmargin=value;}
		}

		/// <summary>
		/// The width of the column used to format text into
		/// </summary>
		public float ColumnWidth
		{
			get{return _columnwidth;}
			set{_columnwidth=value;}
		}

		/// <summary>
		/// 
		/// </summary>
        [Browsable(true)]
		public float Pos
		{
			get{return _Pos;}
			set{_Pos=value;}
		}
		
		
		/// <summary>
		/// An array of tab stop values
		/// </summary>
		public float[] Tabs
		{
			get{return _tabs;}
			set{_tabs=value;}
		}

		/// <summary>
		/// gets or sets the width of a tab-stop for this formatter.
		/// </summary>
		/// <remarks>
		/// Setting the TabStops will reset the tab array
		/// </remarks>
		public float TabStops
		{
			get{return _tabstops;}
			set
			{
				_tabstops=value;
				ArrayList temptabs=new ArrayList();
				for(float f = LeftMargin;f<LeftMargin+ColumnWidth;f+=_tabstops)
					temptabs.Add(f);
				float[] tabs=new float[temptabs.Count];
				temptabs.CopyTo(tabs);
				Tabs=tabs;
			}
		}

		/// <summary>
		/// Returns a word-width for a specific word, font and graphics object
		/// </summary>
		/// <param name="s">The word to measure</param>
		/// <param name="font">The font used to display the word</param>
		/// <param name="g">The graphics device upon which formatted text will be displayed</param>
		/// <returns></returns>
		public float GetWordWidth(string s, Font font, Graphics g)
		{
			StringFormat sf=StringFormat.GenericTypographic;
			sf.FormatFlags|=StringFormatFlags.MeasureTrailingSpaces;
			SizeF sz = g.MeasureString(s,font,4096,sf);
			return sz.Width;
		}

		/// <summary>
		/// Removes all words from the word list
		/// </summary>
		void ClearWords()
		{
			this._words.Clear();
		}

		/// <summary>
		/// Adds a string of text to the wordlist
		/// </summary>
		/// <remarks>
		/// The text is analysed to extract a full word position description for the supplied paragraph
		/// </remarks>
		/// <param name="text">The text to add</param>
		/// <param name="font">The font used to display the text</param>
		/// <param name="g">The graphics device upon which the text is to be displayed</param>
		public void AddWords(string text, Font font, Graphics g)
		{
			WordPosCollection wpl=GetWords(text,font,g);
			foreach(WordPos wp in wpl)
				_words.Add(wp);
		}

		/// <summary>
		/// returns the word list
		/// </summary>
		/// <returns>The collection of words contained in the formatter</returns>
		public WordPosCollection GetWords()
		{
			return _words;
		}

		/// <summary>
		/// Returns a word collection for a specified paragraph.
		/// </summary>
		/// <param name="text">The text to format</param>
		/// <param name="font">The font used to display the word</param>
		/// <param name="g">The graphics device upon which formatted text will be displayed</param>
		/// <returns>the collection of words after analysis</returns>
		public WordPosCollection GetWords(string text, Font font, Graphics g)
		{
			if(text==null || font==null)
			  return new WordPosCollection();
			WordPosCollection PossArray = new WordPosCollection();
			WordPos wp;
			//remove carriage returns..

			string[] subsnocr = text.Split(new Char[]{'\r'});
			StringBuilder sb = new StringBuilder();
			foreach(string ss in subsnocr)
				sb.Append(ss);
			string s=sb.ToString();

			//we test for this whitespace...
			char[] testarray = new char[]{' ','\t','\n'};

			do
			{
				bool done=false;
				do
				{
					if(s.IndexOfAny(testarray)==0)
					{
						switch(s[0])
						{
							case ' ':
								wp=new WordPos(WhiteSpace.Space);
								wp.Word=" ";
								break;
							case '\t':
								wp=new WordPos(WhiteSpace.Tab);
								wp.Word="\t";
								break;
							case '\n':
								wp=new WordPos(WhiteSpace.NewLine);
								break;
							default:
								wp=new WordPos();
								break;
						}

						wp.Font=font;
						wp.WordWidth=this.GetWordWidth(wp.Word,font,g);

						PossArray.Add(wp);

						s=s.Substring(1,s.Length-1);
					}
					else
						done=true;
				}
				while(!done);

				if(s.Length==0)
					continue;

				int wsindex = s.IndexOfAny(testarray);
				
				wp = new WordPos();
				
				if(wsindex>-1)
				{
					wp.Word=s.Substring(0,wsindex);
					wp.WordWidth=this.GetWordWidth(wp.Word,font,g);
					s=s.Substring(wsindex,s.Length-wsindex);
				}
				else
				{
					wp.Word=s;
					wp.WordWidth=GetWordWidth(s,font,g);
					s="";
				}

				wp.Font=font;

				PossArray.Add(wp);
			}
			while(s.Length>0);
		
			return PossArray; 

		}

		/// <summary>
		/// Removes the whitespace from the front of a word list
		/// </summary>
		/// <remarks>Tab characters are not removed because they perform sensible formatting at the beginning of a line.</remarks>
		/// <param name="words">A reference to the word list that should be trimmed</param>
		public void TrimStart(ref WordPosCollection words)
		{
			int firstindex=0;
			if(words.Count==0)
				return;
			if(words[0].WhiteSpace==WhiteSpace.Indent)
				firstindex=1;
			float pos=words[0].PagePos;
			bool needsRePos = false;
			while(words[firstindex].WhiteSpace==WhiteSpace.Space || words[firstindex].WhiteSpace==WhiteSpace.NewLine)
			{
				words.RemoveAt(firstindex);
				needsRePos=true;
				if(words.Count>0)
					words[0].PagePos=pos;
			}
			if(needsRePos)
				RePos(ref words);
		}

		/// <summary>
		/// Trims the whitespace from the end of a word list
		/// </summary>
		/// <param name="words">A reference to the word list to be trimmed</param>
		public void TrimEnd(ref WordPosCollection words)
		{
			if(words.Count==0)
				return;
			while(words.Count!=0 && (words.Last().WhiteSpace!=WhiteSpace.None))				
			{
				words.RemoveLast();
			}
		}


		/// <summary>
		/// returns then position of the next tabstop after the current position
		/// </summary>
		/// <param name="currPos">The current position</param>
		/// <returns>The linear value of the calculated tab-stop</returns>
		public float GetNextTab(float currPos)
		{
			if(Tabs==null)
				return currPos;
			foreach(float f in Tabs)
			{
				if(f>currPos)
					return f;
			}
			return currPos;
		}

		/// <summary>
		/// Re positions words according to their calculated word widths 
		/// </summary>
		/// <param name="words">A reference to the line being repaginated</param>
		public void RePos(ref WordPosCollection words)
		{
			if(words.Count==0)
				return;
			float currPos = words[0].PagePos;
			for(int i=0;i<words.Count;i++)
			{
				words[i].PagePos=currPos;
				currPos+=words[i].WordWidth;
			}
		}

		/// <summary>
		/// Calculates word positions for a centred line
		/// </summary>
		/// <param name="words">A reference to the line being repaginated</param>
		public void CenterLine(ref WordPosCollection words)
		{
			if(words.Count==0)
				return;
			TrimEnd(ref words);
			if(words.Count==0)
				return;
			TrimStart(ref words);
			if(words.Count==0)
				return;
			words[0].PagePos=(ColumnWidth/2) - (GetLineLength(ref words)/2) + LeftMargin;
			RePos(ref words);
		}

		/// <summary>
		/// Fully Justifies a line
		/// </summary>
		/// <param name="words">A reference to the collection of word positions that constitute the current line</param>
		public void JustifyLine(ref WordPosCollection words)
		{
			if(words.Count==0)
				return;
			if(words.ContainsNewline)
			{
				LeftJustifyLine(ref words);
				return;
			}
			TrimEnd(ref words);
			if(words.Count==0)
				return;
			TrimStart(ref words);
			if(words.Count==0)
				return;
			int lastTab = 0;
			int n=0;
			int spacecount=0;
			foreach(WordPos wp in words)
			{
				if(wp.WhiteSpace==WhiteSpace.Tab)
				{
					lastTab=n;
					spacecount=0;
				}
				if(wp.WhiteSpace==WhiteSpace.Space)
					spacecount++;
				n++;
			}

			WordPos lastWord = words.Last();

			float difference = ColumnWidth - GetLineLength(ref words);

			if(spacecount>0)
			{
				for(int i=lastTab;i<words.Count;i++)
				{
					if(words[i].WhiteSpace==WhiteSpace.Space)
						words[i].WordWidth+=difference/spacecount;
				}
			}

			RePos(ref words);
		}

		/// <summary>
		/// Calculates word positions for a right justified line
		/// </summary>
		/// <param name="words">A reference to the line being repaginated</param>
		public virtual void RightJustifyLine(ref WordPosCollection words)
		{
			if(words.Count==0)
				return;
			TrimEnd(ref words);
			if(words.Count==0)
				return;
			TrimStart(ref words);
			if(words.Count==0)
				return;
			words[0].PagePos=LeftMargin;
			RePos(ref words);
			words[0].PagePos=ColumnWidth-GetLineLength(ref words)+LeftMargin;
			RePos(ref words);
		}

		/// <summary>
		/// Calculates word positions for a left justified line
		/// </summary>
		/// <param name="words">A reference to the line being repaginated</param>
		public void LeftJustifyLine(ref WordPosCollection words)
		{
			if(words.Count==0)
				return;
			TrimEnd(ref words);
			if(words.Count==0)
				return;
			TrimStart(ref words);
			if(words.Count==0)
				return;
			RePos(ref words);
		}

		/// <summary>
		/// measures a total length of a line
		/// </summary>
		/// <param name="words">A reference to the line being measured</param>
		/// <returns>The total length of the line</returns>
		public float GetLineLength(ref WordPosCollection words)
		{
			float f=0;
			foreach(WordPos wp in words)
				f+=wp.WordWidth;
			return f;			
		}

		/// <summary>
		/// Justifies the specified line
		/// </summary>
		/// <param name="line">A reference to the line being justified</param>
		public void DoJustify(ref WordPosCollection line)
		{
			DoJustify(ref line,this.Justify);
		}	

		/// <summary>
		/// Justifies the specified line
		/// </summary>
		/// <param name="line">A reference to the line being justified</param>
		/// <param name="justify">The style of justifiation to perform</param>
		public void DoJustify(ref WordPosCollection line, JustificationStyles justify)
		{
			switch(justify)
			{
				case JustificationStyles.Centered:
					CenterLine(ref line);
					break;
				case JustificationStyles.Justified:
					JustifyLine(ref line);
					break;
				case JustificationStyles.Right:
					RightJustifyLine(ref line);
					break;
				case JustificationStyles.Left:
					LeftJustifyLine(ref line);
					break;
			}
		}

		/// <summary>																 
		/// Calculates an array of lines that are correctly justified for a paragraph of a certain column width.
		/// </summary>
		/// <param name="words">The raw list of words in the paragraph</param>
		/// <returns>An array of lines</returns>
		public WordPosCollection[] GetLines(WordPosCollection words)
		{
			ArrayList lines=new ArrayList();
			bool firstLine=true;
			bool paragraph=true;
			int WordIndex=0;
			WordPosCollection line;
			do
			{
				line = new WordPosCollection();
				line.NewParagraph=true;
				float lineTotal=LeftMargin;
				//Does this line need to be indented?
				if(firstLine && Indent!=0)
				{
					WordPos iwp=new WordPos();
					iwp.PagePos=LeftMargin;
					iwp.WhiteSpace=WhiteSpace.Space;
					iwp.WordWidth=Indent;
					iwp.Font=words[0].Font;
					line.Add(iwp);
				}

				firstLine=false;


				for(;WordIndex<words.Count;WordIndex++)
				{
					//calculate the word Pos
					WordPos wp=words[WordIndex].Clone();
					wp.PagePos=lineTotal;

					//holds the additional size to be added to lineTotal
					//This takes care of odd values added due to tab-stops etc.
					float extra=0;

					bool newline = false;

					//calculate the lineTotal shift
					switch(wp.WhiteSpace)
					{
						case WhiteSpace.Space:
						case WhiteSpace.None:
							extra+=wp.WordWidth;
							break;
						case WhiteSpace.Tab:
							extra+=this.GetNextTab(lineTotal)-lineTotal;
							wp.WordWidth=extra;
							break;
						case WhiteSpace.NewLine:
							extra=0;
							line.Add(wp);
							newline=true;
							paragraph=true;
							break;
					}

					if(lineTotal+extra > LeftMargin+ColumnWidth)
						newline=true;

					//if the line is long enough, justify whats in this line and move on
					if(newline)
					{
						DoJustify(ref line);
						lines.Add(line);
						line=new WordPosCollection();
						line.NewParagraph=paragraph;
						paragraph=false;
						lineTotal=LeftMargin;
						wp.PagePos=lineTotal;
						if(extra!=0)
						{
							line.Add(wp);
							lineTotal+=extra;
						}
					}
					else //just add the current entity to the line
					{
						line.Add(wp);
						lineTotal+=extra;
					}
				}			
			}
			while(WordIndex<words.Count-1); //until there are no more entities left in the word list

			if(line.Count!=0)
			{
				//Takes care of the last line in a justified paragraph. 
				//which must always be left justified.
				if(this.Justify==JustificationStyles.Justified)
					DoJustify(ref line,JustificationStyles.Left);
				else
					DoJustify(ref line,this.Justify);
				lines.Add(line);
			}

			WordPosCollection[] linearray=new WordPosCollection[lines.Count];
			lines.CopyTo(linearray);

			return linearray;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Formatter()
		{
		}

	}

	[ToolboxBitmap(typeof(TextPanel),"TextFormatter.TextPanel.bmp")]
	public class TextPanel : Panel
	{

		Formatter fmtr=new Formatter();

		bool _showWhiteSpace;

		public bool ShowWhiteSpace
		{
			get{return _showWhiteSpace;}
			set{_showWhiteSpace=value;}
		}


		[Browsable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		public float[] Tabs
		{
			get{return fmtr.Tabs;}
			set{fmtr.Tabs=value;}
		}

		public float StandardTabs
		{
			get{return fmtr.TabStops;}
			set{fmtr.TabStops=value;}
		}

		
		public JustificationStyles JustificationStyle
		{
			get{return fmtr.Justify;}
			set{
				fmtr.Justify=value;
				Invalidate();
			}
		}


		public TextPanel() : base()
		{
			Graphics g=this.CreateGraphics();
			fmtr.TabStops=g.DpiX/2; //standard tab-stops every 1/2 inch
			g.Dispose();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if(fmtr.Words.Count==0)
				return;
			float yStep=Font.GetHeight();
			float ty=0;
			fmtr.LeftMargin=0;
			fmtr.ColumnWidth=this.ClientSize.Width;
			SolidBrush sb=new SolidBrush(this.ForeColor);
			foreach(WordPosCollection wpc in fmtr.GetLines(fmtr.Words))
			{
				foreach(WordPos wp in wpc)
				{
					if(wp.WhiteSpace==WhiteSpace.None)
					{
						e.Graphics.DrawString(wp.Word,Font,sb,wp.PagePos,ty,StringFormat.GenericTypographic);
					}
					else
					{
						if(_showWhiteSpace)
						{
							e.Graphics.DrawRectangle(Pens.Red,wp.PagePos,ty,wp.WordWidth,yStep);
						}
					}
				}
				ty+=yStep;
			}
			sb.Dispose();

		}

		protected virtual void RefreshText()
		{
			fmtr.Words.Clear();
			Graphics g=this.CreateGraphics();
			fmtr.AddWords(this.Text,Font,g);
			g.Dispose();
			Invalidate();
		}

		protected override void OnTextChanged(EventArgs e)
		{
			RefreshText();
			base.OnTextChanged (e);
		}

		protected override void OnFontChanged(EventArgs e)
		{
			RefreshText();
			base.OnFontChanged (e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			fmtr.LeftMargin=0;
			fmtr.ColumnWidth=this.ClientSize.Width;
			Graphics g=CreateGraphics();
			fmtr.TabStops=g.DpiX/2;
			g.Dispose();
			Invalidate();
			base.OnSizeChanged (e);
		}




	}

}
