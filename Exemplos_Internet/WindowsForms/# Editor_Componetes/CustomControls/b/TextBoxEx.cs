using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CustomControls
{
	#region Enumerations
	public enum InputMode
	{
		Overwrite=0,
		Insert
	}

	public enum ValidationModes
	{
		None = 0,
		ValidCharacters = 1,
		InvalidCharacters = 2,
		Letters = 3,
		Numbers = 4,
		MaskEdit = 5,
		RegularExpression = 6
	}

	public enum  RegularExpressionModes
	{
		Custom = 0,
		Email = 1,
		Url = 2,
		IP = 3,
		Dates = 4,
		Zip = 5
	}

	#endregion Enumerations

	/// <summary>
	/// Author	:- Moditha Kumara
	/// Date	:- 26/5/2004
	/// Extended TextBox component which is capable of validating text in couple
	/// of ways. This is a modified version of the original by Luis Alberto Ruiz Arauz
	/// ValidText component (in codeproject.com). However it was in VB.NET. So I thought
	/// of converting it to c#. Secondly, it had quite lengthy code which I thought
	/// can be reduced. This is the end result. This address the masking issue
	/// differently to Luis version. Couple of touches have been made here and there.
	/// 
	/// Use this. edit it(if needed) and please tell me if you made any improvements
	/// to my code. I will be adding more controls/improvements under CustomControls
	/// </summary>
	[ToolboxBitmap(typeof(TextBoxEx))] 
	[DefaultProperty("Text")]
	public class TextBoxEx : System.Windows.Forms.TextBox
	{
		#region Instance_Variables
		private string lastText=string.Empty;
		private string validationText=string.Empty;
		private string maskPattern=string.Empty; //ex: (###)-A-a
		private string mask=string.Empty; //maskPpattern converted. ex: to (___)-_-_
		private string keyType=string.Empty;
		private string errorMsg=string.Empty;
		private string previousText=string.Empty; //stores the previous value of TEXT property

		private bool isRequired=false;
		private bool useEnterAsTab = false;
		private bool showErrorIcon=true;

		private ValidationModes validationMode;
		private RegularExpressionModes regexMode;
		private InputMode inputMode=InputMode.Insert;//default insert mode

		private const string	REGEX_EMAIL	= @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
		private const string	REGEX_IP	= @"(?<First>[01]?\d\d?|2[0-4]\d|25[0-5])\.(?<Second>[01]?\d\d?|2[0-4]\d|25[0-5])\.(?<Third>[01]?\d\d?|2[0-4]\d|25[0-5])\.(?<Fourth>[01]?\d\d?|2[0-4]\d|25[0-5])(?x)";
		private const string	REGEX_URL	= @"^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/";
		private const string	REGEX_DATE	= @"(?<Month>\d{1,2})/(?<Day>\d{1,2})/(?<Year>(?:\d{4}|\d{2}))(?x)";
		private const string	REGEX_ZIP	= @"(?<Zip>\d{5})-(?<Sub>\d{4})(?x)";

		private const char		MASK_CHAR_HOLDER	= '_';
		private const char		MASK_ESCAPE			= '\\';
		private const string	MASK_CHARS			="#Aa&$";
		private const string	MASK_NUMBERS		= "0123456789.";
		private const string	MASK_LETTERS		= "AÁBCDEÉFGHIÍJKLMNÑOÓPQRSTUÚÜVWXYZaábcdeéfghiíjklmnñoópqrstuúüvwxyz ,;.'";
		private const string	MASK_TYPE_A			= "AÁBCDEÉFGHIÍJKLMNÑOÓPQRSTUÚÜVWXYZ";

		private const string	DEFAULT_ISREQUIRED_MSG	= "This is a required field.";
		private const string	DEFAULT_INVALID_MSG		= "This field contains invalid data.";

		#endregion Instance_Variables

		#region Class_Variables
		internal static ErrorProvider errProvider=null;
		#endregion Class_Variables

		#region Event_Declarations
		public delegate void OnValidationErrorDelegate(object sender);
		public event OnValidationErrorDelegate ValidationError;
		#endregion Event_Declarations
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextBoxEx()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			if (errProvider==null)
			{
				errProvider = new ErrorProvider();
				errProvider.BlinkStyle=ErrorBlinkStyle.NeverBlink;
			}

			//set up default values for properties
			this.RegExPatternMode=RegularExpressionModes.Custom;
			this.ValidationMode=ValidationModes.None;
			this.TextInputMode=InputMode.Insert;
			this.IsRequired=false;
			this.UseEnterAsTab=false;
			this.ShowErrorIcon=true;

			//disable the default context menu
			this.ContextMenu = new ContextMenu(); 
		}


		#region Properties
		public override string Text
		{
			get{return base.Text;}
			set
			{
				//Handle situations where somebody might input text
				//directly at runtime.
				if (!base.DesignMode)
				{
					#region IMPORTANT_READ_THIS
					//Very IMPORTANT CODE BLOCK
					//This will prevent stack overflow by continous validation
					//because if some crackpot contraption do something like following
					//in his form.(By capturing TextChanged event)
					//private void txtMaskValidator_TextChanged(object sender, System.EventArgs e)
					//{
					//	txtMaskValidator.Text=txtMaskValidator.Text;
					//}
					//Try removing the following if..else and see for yourself.
					//Anyhow doing something like above, will waste the CPU. we can avoid
					//that by overriding the OnTextChanged event and not call base.TextChanged(e)
					//but then its not good and expected by general guidelines.
					//
					//If a real mega-maniac do something like;
					//private void txtMaskValidator_TextChanged(object sender, System.EventArgs e)
					//{
					//	txtMaskValidator.Text=Guid.NewGuid().ToString();
					//}
					//Then anyway your CPU cries and then throw a StackOverFlowException.
					//but this is still the same even if you use a standard .NET TextBox Control.
					//So this is basicaly user error. You are not suppose to be that crazy!
					if (previousText==value) 
						return;
					else
						previousText=value;

					#endregion IMPORTANT_READ_THIS

					TextPropertyChanged(value);
				}
				else
				{
					base.Text=value;
				}

			}
		}

		[Category("Extended")]
		[Description("ValidText/InvalidText/RegularExpression or Mask to be used. [ValidationMode] property will determine how the data in [ValidationText] property will be used.\n" +
		"For Mask.\n#=Numbers\nA=Uppercase\na= lowercase\n&&=Uppercase and lowercase\n$=Uppercase lowercase and Numbers.\nOther characters are fixed in the mask.")]
		[MergableProperty(false)]
		[RefreshProperties(RefreshProperties.All)]
		public string ValidationText
		{
			get{return validationText;}
			set
			{
				validationText=value;
				if (this.ValidationMode == ValidationModes.MaskEdit)
				{
					if (this.ValidationText.Length!=0)
					{
						this.mask=GetMaskString(this.validationText);
						base.Text=this.mask;
						this.maskPattern = this.ValidationText;
					}
					else
					{
						this.mask=string.Empty;
						this.maskPattern = string.Empty;
					}
				}
			}
		}

		[Category("Extended")]
		[Description("Controls whether a value is needed for text property.")]
		[MergableProperty(false)]
		public bool IsRequired
		{
			get{return isRequired;}
			set{isRequired=value;}
		}

		[Category("Extended")]
		[Description("Controls whether error icon should be shown.")]
		[MergableProperty(false)]
		public bool ShowErrorIcon
		{
			get{return showErrorIcon;}
			set{showErrorIcon=value;}
		}

		[Category("Extended")]
		[Description("Regular expression mode to use.")]
		[MergableProperty(false)]
		[RefreshProperties(RefreshProperties.All)]
		public RegularExpressionModes RegExPatternMode
		{
			get{return regexMode;}
			set
			{
				regexMode=value;
				this.ValidationMode=ValidationModes.RegularExpression;

				switch (regexMode)
				{
					case RegularExpressionModes.Dates:this.ValidationText=REGEX_DATE;break;
					case RegularExpressionModes.Email:this.ValidationText=REGEX_EMAIL;break;
					case RegularExpressionModes.IP:this.ValidationText=REGEX_IP;break;
					case RegularExpressionModes.Url:this.ValidationText=REGEX_URL;break;
					case RegularExpressionModes.Zip:this.ValidationText=REGEX_ZIP;break;
				}
			}
		}

		[Category("Extended")]
		[Description("Specifies the type of validation to be used.")]
		[MergableProperty(false)]
		[RefreshProperties(RefreshProperties.All)]
		public ValidationModes ValidationMode
		{
			get{return validationMode;}
			set
			{
				validationMode=value;
				switch(validationMode)
				{
					case ValidationModes.None:
						this.ValidationText= "";
						break;
					case ValidationModes.ValidCharacters:
						this.ValidationText= "";
						break;
					case ValidationModes.InvalidCharacters:
						this.ValidationText= "";
						break;
					case ValidationModes.Letters:
						this.ValidationText= MASK_LETTERS;
						break;
					case ValidationModes.Numbers:
						this.ValidationText= MASK_NUMBERS;
						break;
					case ValidationModes.MaskEdit:
						if (this.ValidationText.Length==0)
						{
							this.ValidationText="Not set";
						}
						else
						{
							this.mask=GetMaskString(this.validationText);
							base.Text=this.mask;
							this.maskPattern = this.ValidationText;
						}
						break;
				}
			}
		}

		[Category("Extended")]
		[Description("Error message to show when validation fails.")]
		[MergableProperty(false)]
		public string ErrorMessage
		{
			get{return errorMsg;}
			set{errorMsg=value;}
		}

		[Category("Extended")]
		[Description("Controls wherther text input mode is insert or overwrite.")]
		[MergableProperty(false)]
		public InputMode TextInputMode
		{
			get{return inputMode;}
			set{inputMode=value;}
		}

		[Category("Extended")]
		[Description("Controls whether the ENTER key acts as a TAB.")]
		[MergableProperty(false)]
		public bool UseEnterAsTab
		{
			get{return useEnterAsTab;}
			set{useEnterAsTab=value;}
		}

		#endregion Properties

		#region Private_Methods
		
		/// <summary>
		/// OnTextChanged event cannot be used here.
		/// We call this method from the TEXT property, SET section
		/// </summary>
		private void TextPropertyChanged(string newText)
		{
			base.SelectionStart=0;//when inserting text from code start from begining

			IEnumerator enumString = newText.GetEnumerator();

			while(enumString.MoveNext())
			{	
				char current = char.Parse(enumString.Current.ToString());
				KeyPressEventArgs kpeArg=new KeyPressEventArgs(current);
				PerformMaskValidation(kpeArg,false);
			}
		}

		private void ClearSelectionInMask()
		{
			int start = this.SelectionStart;
			int length = start + this.SelectionLength;

			if ( (start==0) && (length==mask.Length) ) //clear all dont go to loop
			{
				base.Text=mask;
				base.SelectionStart=0;
				return;
			}

			for(int i=start; i<length ; i++)
			{
				if (mask[i]==MASK_CHAR_HOLDER)
				{
					base.SelectionStart=i;
					base.SelectionLength=1;
					base.SelectedText=MASK_CHAR_HOLDER.ToString();
				}
			}
		}
		private void ShowError(string msg)
		{
			if( (errProvider!=null) && (showErrorIcon) )
			{
				errProvider.SetError(this,msg);
			}
			if( (msg.Length!=0) && (ValidationError!=null) )
			{
				ValidationError(this);
			}
		}

		/// <summary>
		/// Checks each character and adds it to TEXT property.
		/// additionaly tells wether the current character is valid.
		/// Can be used to check wether the passed character got added
		/// </summary>
		/// <param name="e">KeyPressEventArgs</param>
		/// <returns>true- if the character is valid. else false.</returns>
		/// 
		private bool PerformMaskValidation(KeyPressEventArgs e, bool isKeyPress)
		{
			//current cursor position
			int cursorPos = base.SelectionStart;

			if (cursorPos<0)//this happense only when loading the form
			{
				e.Handled=true;
				return true; //send true, else mask will not be set.
			}

			//cursor is at the beginning AND backspace pressed? QUIT
			if ( (cursorPos == 0) && (e.KeyChar == (char)8) )
			{
				e.Handled=true;
				return false;
			}

			//backspace pressed or cursor is at the end of the mask?
			if ( (e.KeyChar == (char)8) || (cursorPos==mask.Length) )
			{
				if ( (cursorPos==mask.Length) && (e.KeyChar != (char)8) )
				{
					e.Handled=true;
					return false;
				}
				else //only possible key is backspace
				{
					//When your going to delete a character, check what should be there
					//in the current place, comparing with the original mask.
					//We can delete anything which is in the place of a '_'
					//Other characters should remain
					--this.SelectionStart;
					if (mask[this.SelectionStart]==MASK_CHAR_HOLDER)
					{
						//System.Diagnostics.Debug.WriteLine("removing characters and adding '_'");
						this.SelectionLength=1;
						this.SelectedText="_";
						--this.SelectionStart;
					}
					e.Handled=true;
					return false;
				}
			}

			//Always check the mask against our private mask variable
			if (mask[cursorPos] != MASK_CHAR_HOLDER)
			{
				//this.SelectionStart = (cursorPos==mask.Length)? this.SelectionStart : ++this.SelectionStart;
				//System.Diagnostics.Debug.WriteLine("cannot insert here, advancing cursor");
				while(cursorPos!=mask.Length)
				{
					if (mask[cursorPos] != MASK_CHAR_HOLDER)
						++cursorPos;
					else
						break;
				}

				if (cursorPos!=mask.Length)
					this.SelectionStart = cursorPos;
				else
				{
					e.Handled=true;
					return false;
				}
			}

			if (IsValidInputForMask(maskPattern[cursorPos],e.KeyChar))
			{
				this.SelectionLength=1;
				if (!isKeyPress)//if not keypress event we have to insert manually
					this.SelectedText=e.KeyChar.ToString();
				return true;
			}
			else
			{
				//System.Diagnostics.Debug.WriteLine("not a valid char");
				e.Handled=true;
				return false;
			}
		}

		private bool PerformControlValidations()
		{
			bool isValid=true;

			if ( (this.IsRequired) && (this.Text.Length==0) )
			{
				ShowError(DEFAULT_ISREQUIRED_MSG);
				return false;
			}

			switch (this.ValidationMode)
			{
				case ValidationModes.RegularExpression:
					if (!isRegExValid(this.Text,this.ValidationText))
					{
						isValid = false;
						ShowError((this.errorMsg.Length==0)? DEFAULT_INVALID_MSG : this.errorMsg);
					}
					else
						isValid = true;
					break;

				case ValidationModes.MaskEdit:
					if ( !isMaskFilled() || (this.IsRequired) )
					{
						ShowError(DEFAULT_ISREQUIRED_MSG);
						isValid = false;
					}
					else
						isValid = true;
					break;

				case ValidationModes.InvalidCharacters:
					if (this.Text.IndexOfAny(this.ValidationText.ToCharArray()) > -1 )
					{
						ShowError((this.errorMsg.Length==0)? DEFAULT_INVALID_MSG : this.errorMsg);
						isValid = false;
					}
					else
						isValid = true;
					break;

				case ValidationModes.Letters:
					if (SourceInTarget(this.Text,MASK_LETTERS))
						isValid = true;
					else
					{
						ShowError((this.errorMsg.Length==0)? DEFAULT_INVALID_MSG : this.errorMsg);
						isValid = false;
					}
					break;
				
				case ValidationModes.Numbers:
					if (SourceInTarget(this.Text,MASK_NUMBERS))
						isValid = true;
					else
					{
						ShowError((this.errorMsg.Length==0)? DEFAULT_INVALID_MSG : this.errorMsg);
						isValid = false;
					}
					break;

				case ValidationModes.ValidCharacters:
					if (SourceInTarget(this.Text,this.ValidationText))
						isValid = true;
					else
					{
						ShowError((this.errorMsg.Length==0)? DEFAULT_INVALID_MSG : this.errorMsg);
						isValid = false;
					}
					break;

				default:isValid = true;break;
			}

			return isValid;
		}

		private bool SourceInTarget(string source,string target)
		{
			IEnumerator enumString = source.GetEnumerator();

			while(enumString.MoveNext())
			{	
				if (target.IndexOf(enumString.Current.ToString()) == -1)
				{
					return false;
				}
			}
			return true;
		}

		private bool isMaskFilled()
		{
			int availablePlaceHolders=0;
			int noOfPlaceHolders=0;

			//count number of placeholders
			IEnumerator enumChar = this.mask.GetEnumerator();
			while(enumChar.MoveNext())
			{	
				if (char.Parse(enumChar.Current.ToString())==MASK_CHAR_HOLDER)
				{
					noOfPlaceHolders++;
				}
			}

			//now count number of placeholders in TEXT property
			enumChar = this.Text.GetEnumerator();

			while(enumChar.MoveNext())
			{	
				if (char.Parse(enumChar.Current.ToString())==MASK_CHAR_HOLDER)
				{
					availablePlaceHolders++;
				}
			}

			//A mask should be either filled completely or
			//not filled at all.
			if (availablePlaceHolders==noOfPlaceHolders) //nothing have been filled, its ok.
				return true;

			if (availablePlaceHolders==0) //all places are filled, still ok
				return true;

			return false; //it comes here only if half is filled.
		}

		private bool isRegExValid(string text, string pattern)
		{
			try
			{
				return (Regex.IsMatch(text,pattern))? true:false;
			}
			catch
			{
				return false;
			}
		}

		private bool IsValidInputForMask(char maskChar,char userChar)
		{
			bool isValid=true;
			switch(maskChar)
			{
				case '#': isValid = (MASK_NUMBERS.IndexOf(userChar) > -1)? true:false;break;
				case 'A': 
					isValid = (MASK_TYPE_A.IndexOf(userChar) > -1)? true:false;break;
				case 'a': isValid = (MASK_TYPE_A.ToLower().IndexOf(userChar) > -1)? true:false;break;
				case '$': 
					isValid = (MASK_NUMBERS.IndexOf(userChar) > -1)? true:false;
					isValid |=(MASK_TYPE_A.IndexOf(userChar) > -1)? true:false;
					isValid |=(MASK_TYPE_A.ToLower().IndexOf(userChar) > -1)? true:false;
					break;
				case '&': 
					isValid =(MASK_TYPE_A.IndexOf(userChar) > -1)? true:false;
					isValid |=(MASK_TYPE_A.ToLower().IndexOf(userChar) > -1)? true:false;
					break;
			}
			return isValid;
		}

		private string GetMaskString(string pattern) //getMask
		{
			string maskString=string.Empty;

			for (int i = 0; i <= pattern.Length - 1; i++)
			{
				if (pattern.IndexOfAny(MASK_CHARS.ToCharArray(),i,1)>-1)
				{
					maskString += MASK_CHAR_HOLDER;
				}
				else
				{
					maskString += pattern[i];
				}
			}

			return maskString;
		}

		/// <summary>
		/// TODO : For future updates. To provide mask character escaping functionality.
		/// </summary>
		/// <param name="pattern"></param>
		/// <returns></returns>
		private string GetMaskPattern(string pattern)
		{
			return string.Empty;
		}
		#endregion Private_Methods

		#region Public_Methods
		public bool Validate()
		{
			ShowError("");//clear any previous errors before we try this run
			return PerformControlValidations();
		}
		#endregion Public_Methods

		#region Event_Handling
		protected override void OnEnter ( System.EventArgs e )//eventoEnter_validText
		{
			if (this.inputMode==InputMode.Overwrite)
				base.SelectAll();

			base.OnEnter(e);
		}

		protected override void OnLeave ( System.EventArgs e )
		{
			if (!PerformControlValidations())
			{
				base.Focus();
				return;
			}else
				ShowError("");

			base.OnLeave(e);
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (useEnterAsTab)
			{
				if (e.KeyCode == Keys.Enter)
				{
					SendKeys.Send("{Tab}");
					e.Handled=true;
				}
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			//Realtime validation is only for maskedit.
			//others gets validated on leave.
			if (this.ValidationMode==ValidationModes.MaskEdit)
			{
				PerformMaskValidation(e,true);
			}
			base.OnKeyPress(e);
		}

//		protected override void OnTextChanged(System.EventArgs e)
//		{
//
//		}

		protected override void OnValidating(CancelEventArgs e)
		{
			if (this.IsRequired)
			{
				if (!PerformControlValidations())
				{
					base.Focus();
					return;
				}else
					ShowError("");
			}
			base.OnValidating(e);
		}

		protected override void OnValidated(System.EventArgs e)
		{
			ShowError("");
			base.OnValidated(e);
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();

				if (errProvider!=null)
					errProvider.Dispose();
			}
			base.Dispose( disposing );
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) 
		{ 
			//If the mode is mask edit watchout for troublesome keys.(like Ctrl-V, Shift-Insert)
			if (this.ValidationMode!=ValidationModes.MaskEdit)
			{
				return base.ProcessCmdKey(ref msg, keyData); 
			}

			//Avoid Shift-Insert
			if (keyData.ToString()=="Insert, Shift")
			{
				return true;
			}

			if(keyData == Keys.Delete)
			{
				//if a selection is there in the mask when pressing delete
				//clear those letters and replace mask
				ClearSelectionInMask();
				return true; 
			}
			else 
				return base.ProcessCmdKey(ref msg, keyData); 
		}  

		#endregion Event_Handling

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TextBoxEx
			// 
			this.Name = "TextBoxEx";
		}
		#endregion
	}
}
