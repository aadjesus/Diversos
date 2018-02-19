using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using LJ.Controls.Win32;

namespace LJ.Controls
{
	/// <summary>
	/// Summary description for Collate.
	/// </summary>
	public class Collate : System.Windows.Forms.UserControl
	{
		public enum MouseFlag
		{
			None=0,InFirst=1,InSecond=2
		};
		#region Variable
		private int   maxlen=0;
        private float fontwidth=0.0F;
     
		private string first="";
		private string second="";
		private string result="";
        private BitArray diff=null; 
		private Font   font=null;
        private SolidBrush yellowBrush=new SolidBrush(Color.LightYellow);
		private SolidBrush blackBrush=new SolidBrush(Color.Black);
		private SolidBrush grayBrush=new SolidBrush(Color.Gray);
		private Pen hotTrackPen=new Pen(Color.FromName("HotTrack"),1);

		private Rectangle rc1=new Rectangle();
		private Rectangle rc2=new Rectangle();

		private MouseFlag mf=MouseFlag.None;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private LJ.Controls.EditEx ResultBox;
		private System.ComponentModel.Container components = null;
		#endregion

		public Collate()
		{
			font=new Font("Courier New",8);
			fontwidth=font.Size;
			InitializeComponent();
		}
 
		#region Helper
		private void DrawFirstEntry(Graphics g)
		{
			rc1=new Rectangle(label1.Right+5,label1.Top,Width-label1.Width-20,font.Height+4);
			g.DrawRectangle(hotTrackPen,rc1);
			if(mf == MouseFlag.InFirst)
			{
				Rectangle r=rc1;
				r.Inflate(1,1); 
				g.FillRectangle(grayBrush,r);
			}
			
			int i;
			float allwidth=rc1.Left+2;
			for(i=0;i<first.Length && i<diff.Length;i++)
			{
				string t=first[i].ToString();
				//float w=g.MeasureString(t,Font).Width;
				float w=fontwidth;
				if(!diff[i])
				{
					//WindowsAPI.SetBkMode(g.GetHdc(),BackgroundMode.OPAQUE);
					//WindowsAPI.SetTextColor(g.GetHdc(),(uint)Color.Yellow.ToArgb());
					//WindowsAPI.ExtTextOut(g.GetHdc(),allwidth,rc1.Top+2,ETO_CLIPPED, ref rect, str, str.Length, IntPtr.Zero);

					g.FillRectangle(yellowBrush,allwidth+2,rc1.Top+2,w-2,rc1.Height-4); 
				
				} 
				//WindowsAPI.SetBkMode(g.GetHdc(),BackgroundMode.TRANSPARENT);
				g.DrawString(t,font,blackBrush,allwidth,rc1.Top+2);
				allwidth+=w;
			}
			if(i<first.Length)
			{
				string t=first.Substring(i,first.Length-i);
				float w=g.MeasureString(t,Font).Width;
				g.FillRectangle(yellowBrush,allwidth,rc1.Top+2,w,rc1.Height-4);
				g.DrawString(t,font,blackBrush,allwidth,rc1.Top+2);
			}
		}
		private void DrawSecondEntry(Graphics g)
		{
			rc2=new Rectangle(label2.Right+5,label2.Top,Width-label2.Width-20,font.Height+4);
			g.DrawRectangle(hotTrackPen,rc2);
			if(mf==MouseFlag.InSecond)
			{
				Rectangle r=rc2;
				r.Inflate(1,1);
				g.FillRectangle(grayBrush,r);
			}

			int i;
			float allwidth=rc2.Left+2;
			for(i=0;i<second.Length && i<diff.Length;i++)
			{
				string t=second[i].ToString();
				//float w=g.MeasureString(t,Font).Width;
				float w=fontwidth;
				if(!diff[i])
				{
					g.FillRectangle(yellowBrush,allwidth+2,rc2.Top+2,w-2,rc2.Height-4); 
				}
				g.DrawString(t,font,blackBrush,allwidth,rc2.Top+2);
				allwidth+=w;
			}
			if(i<second.Length)
			{
				string t=second.Substring(i,first.Length-i);
				//float w=g.MeasureString(t,Font,t.Length).Width;
				float w=t.Length*fontwidth;
				g.FillRectangle(yellowBrush,allwidth,rc2.Top+2,w,rc2.Height-4);
				g.DrawString(t,font,blackBrush,allwidth,rc2.Top+2);
			}
		}
		#endregion

		#region Override 
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		protected override void OnCreateControl()
		{
			fontwidth = (float)font.Size;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
            Graphics g=e.Graphics; 
			g.DrawRectangle(hotTrackPen,2,2,Width-4,Height-4);
			DrawFirstEntry(g);
			DrawSecondEntry(g);
		}
		protected override void OnResize(EventArgs e)
		{
			ResultBox.Left=label1.Right+5;
			ResultBox.Width=Width-ResultBox.Left-5;
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if(e.X > rc1.Left && e.X < rc1.Right && e.Y >rc1.Top && e.Y < rc1.Bottom)
			{
				if(mf != MouseFlag.InFirst)
				{
					mf=MouseFlag.InFirst;
					Invalidate();
				}
			}
			else if(e.X > rc2.Left && e.X < rc2.Right && e.Y >rc2.Top && e.Y < rc2.Bottom)
			{
				if(mf != MouseFlag.InSecond)
				{
					mf=MouseFlag.InSecond;
					Invalidate();
				}
			}
			else
			{
				if(mf != MouseFlag.None)
				{
					mf=MouseFlag.None;
					Invalidate();
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if(mf == MouseFlag.InFirst)
			{
				ResultBox.Text=first;
				ResultBox.SelectAll(); 
				result=first;
			}
			else if(mf == MouseFlag.InSecond)
			{
				ResultBox.Text=second;
				ResultBox.SelectAll(); 
				result=second;
			}
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			ResultBox.Focus();
		}
		#endregion

		#region Property
        public new Font Font 
		{
			get{ return font;}
			set
			{
				if(font != value)
				{
					font=value;
					ResultBox.Font=value;
					Invalidate();
				}
			}
		}
		#endregion
		
		#region Mothed
        public void SetEntry(string first,string second)
		{
			this.first=first;
			this.second=second;

			string tmp=first.Length > second.Length ? first:second;
			
			//Graphics g=Graphics.FromHwnd(this.Handle);
			//maxlen =(int)g.MeasureString(tmp,font,tmp.Length).Width;
			maxlen= Convert.ToInt32(tmp.Length*fontwidth);

			int width=Width-label1.Width-20;
			if(width < maxlen)
			{
				//Width += (maxlen - width);
				this.SuspendLayout();
				Width = maxlen + label1.Width +30;
				this.ResumeLayout(true);
			}

			int len=Math.Max(first.Length,second.Length);
			diff=new BitArray(len,true);
			
			for(int i=0;i<len;i++)
			{
				if(i < first.Length && i< second.Length)
				{
					if(first[i] != second[i])
						diff[i]=false;
				}
				else
				{
					diff[i]=false;
				}
			}
			Invalidate();
		}
		public void SetFinalEntry(string rst)
		{
			ResultBox.Text=rst;
		}
		public string GetFinalEntry()
		{
			return ResultBox.Text;
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label1.Location = new System.Drawing.Point(8, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "First       Entry";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label2.Location = new System.Drawing.Point(8, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Second Entry";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label3.Location = new System.Drawing.Point(10, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Result";
			// 
			// Collate
			// 
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "Collate";
			this.Size = new System.Drawing.Size(424, 88);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
