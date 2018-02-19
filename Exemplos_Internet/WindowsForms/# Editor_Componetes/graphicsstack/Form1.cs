using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;

namespace graphicsstack
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			GraphicObject o=new GraphicObject();
			o.Shape=Shapes.Square;
			o.Size=new SizeF(100,100);
			o.Translation=new PointF(200,200);

			this.treeView1.Nodes.Add(o);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(160, 398);
			this.treeView1.TabIndex = 0;
			this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter1.Location = new System.Drawing.Point(160, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 398);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(163, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(341, 266);
			this.panel1.TabIndex = 2;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// splitter2
			// 
			this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(163, 266);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(341, 3);
			this.splitter2.TabIndex = 3;
			this.splitter2.TabStop = false;
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Left;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(163, 269);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(181, 129);
			this.propertyGrid1.TabIndex = 4;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Add Node";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Delete Node";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// splitter3
			// 
			this.splitter3.Location = new System.Drawing.Point(344, 269);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(3, 129);
			this.splitter3.TabIndex = 5;
			this.splitter3.TabStop = false;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(347, 269);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(157, 129);
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 398);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.splitter3);
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeView1);
			this.Name = "Form1";
			this.Text = "Graphics Explorer";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (e.Node!=null)
				this.propertyGrid1.SelectedObject=e.Node;
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			StringBuilder sb=new StringBuilder();
			foreach(GraphicObject o in this.treeView1.Nodes )
				o.Paint(e.Graphics, sb);
			this.textBox1.Text=sb.ToString();
		}

		private void propertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			this.panel1.Invalidate();
		}

		private void treeView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.contextMenu1.Show(this.treeView1,this.treeView1.PointToClient(Control.MousePosition));
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			if (this.treeView1.SelectedNode!=null)
				this.treeView1.SelectedNode.Nodes.Add(new GraphicObject());
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if (this.treeView1.SelectedNode!=null)
			{
				this.propertyGrid1.SelectedObject=null;
				if (this.treeView1.SelectedNode.Parent!=null)
				{
					this.treeView1.SelectedNode.Parent.Nodes.Remove(this.treeView1.SelectedNode);
				}
				else
				{
					MessageBox.Show("Cannot remove root node","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				}
			}
			this.panel1.Invalidate();

		}
	}


	public enum Shapes
	{
		None,
		Square,
		Triangle,
		Ellipse
	}

	public class GraphicObject : TreeNode, ICustomTypeDescriptor
	{
		private SizeF _size;
  
		[TypeConverter(typeof(SizeFConverter))]
		public SizeF Size
		{
			get
			{
				return _size;
			}
			set
			{
				_size = value;
			}
		}
		private System.Single _rotation;
  
		public System.Single Rotation
		{
			get
			{
				return _rotation;
			}
			set 
			{
				_rotation=value;
				RecalculateTransform();
			}
		}
		private PointF _translation;
  
		[TypeConverter(typeof(PointFConverter))]
		public PointF Translation
		{
			get
			{
				return _translation;
			}
			set 
			{
				_translation=value;
				RecalculateTransform();
			}
		}
		private SizeF _scale=new SizeF(1,1);
  
		[TypeConverter(typeof(SizeFConverter))]
		public SizeF Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				_scale = value;
				RecalculateTransform();
			}
		}		
		private Shapes _shape=Shapes.None;
  
		public Shapes Shape
		{
			get
			{
				return _shape;
			}
			set
			{
				_shape = value;
				switch (value) 
				{
					case  Shapes.None:
						this.Text="Graphics Node";
						break;
					case  Shapes.Square:
						this.Text="Graphics Node (Square)";
							break;
					case  Shapes.Triangle:
						this.Text="Graphics Node (Triangle)";
							break;
					case  Shapes.Ellipse:
						this.Text="Graphics Node (Ellipse)";
							break;
					default :
						break;
				}
			}
		}		
		private Matrix _transform=new Matrix();
  
		[Browsable(false)]
		public Matrix Transform
		{
			get
			{
				return _transform;
			}
		}
		public GraphicObject()
		{
			Text="Graphic Node";
		}

		//Sets the transform of an object from the Scale, Rotation and Translation properties
		private void RecalculateTransform()
		{
			Matrix mx=new Matrix();
			mx.Scale(this.Scale.Width,this.Scale.Height,MatrixOrder.Append);
			mx.Rotate(this.Rotation,MatrixOrder.Append);
			mx.Translate(this.Translation.X,this.Translation.Y,MatrixOrder.Append);
			this._transform=mx;
		}


		//Draws the shape according to it's shape setting.
		private void DoPaint(Graphics g)
		{
			switch (this.Shape) 
			{
				case  Shapes.Square:
					g.FillRectangle(Brushes.Red,-0.5f*this.Size.Width,-0.5f*this.Size.Height,this.Size.Width,this.Size.Height);
					break;
				case Shapes.Ellipse:
					g.FillEllipse(Brushes.Blue,-0.5f*this.Size.Width,-0.5f*this.Size.Height,this.Size.Width,this.Size.Height);
					break;
				case Shapes.Triangle :
					g.FillPolygon(
						Brushes.Green,
						new PointF[]{
										new PointF(0,-0.5f*this.Size.Height), 
										new PointF(-0.5f*this.Size.Width,0.5f*this.Size.Height), 
										new PointF(0.5f*this.Size.Width,0.5f*this.Size.Height), 
										new PointF(0,-0.5f*this.Size.Height)},
						FillMode.Winding);
					break;
				case Shapes.None :
					break;
			}
		}

		static int indent=0;
		//helper function to put the right number of tabs in a line to indent it nicely
		private string calcindent()
		{
			string s="";
			for(int l_index = 0; l_index < indent; l_index++)
				s+="\t";
			return s;
		}

		//The paint method also populates a StringBuilder with al the operations taking place
		public void Paint(Graphics g, StringBuilder sb)
		{
			sb.Append(string.Format("{0}Painting Object {1}\r\n",calcindent(),this.Text));
			indent++;
			GraphicsState gs=g.Save();
			sb.Append(string.Format("{0}Saving graphics state\r\n",calcindent(),this.Text));
			indent++;
			sb.Append(string.Format("{0}Scale Object {1}\r\n",calcindent(),this.Scale.ToString()));
			sb.Append(string.Format("{0}Rotate Object {1}\r\n",calcindent(),this.Rotation));
			sb.Append(string.Format("{0}Translate Object {1}\r\n",calcindent(),this.Translation.ToString()));
			indent--;
			Matrix mx=new Matrix();
			mx.Multiply(g.Transform);
			mx.Multiply(this.Transform);
			g.Transform=mx;
			DoPaint(g);
			foreach(GraphicObject o in this.Nodes)
			{
				indent++;
				sb.Append(string.Format("{0}Painting child object {1}\r\n",calcindent(),o.Text));
				o.Paint(g,sb);
				indent--;
			}
			sb.Append(string.Format("{0}Restoring graphics state\r\n",calcindent(),this.Translation.ToString()));
			g.Restore(gs);
			indent--;
		}
		#region ICustomTypeDescriptor Members

		public TypeConverter GetConverter()
		{
			// TODO:  Add GraphicObject.GetConverter implementation
			return null;
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			// TODO:  Add GraphicObject.GetEvents implementation
			return null;
		}

		EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
		{
			// TODO:  Add GraphicObject.System.ComponentModel.ICustomTypeDescriptor.GetEvents implementation
			return null;
		}

		public string GetComponentName()
		{
			return "Graphic Node";
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		public AttributeCollection GetAttributes()
		{
			// TODO:  Add GraphicObject.GetAttributes implementation
			return new AttributeCollection(new Attribute[]{});
		}

		private PropertyDescriptorCollection Props()
		{
			PropertyDescriptorCollection opdc=TypeDescriptor.GetProperties(this,true);
			PropertyDescriptorCollection pdc=new PropertyDescriptorCollection(
				new PropertyDescriptor[]{opdc["Translation"],opdc["Scale"],opdc["Rotation"],opdc["Shape"],opdc["Size"]});
			return pdc;
		}
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return Props();
		}

		PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
		{
			return Props();
		}

		public object GetEditor(Type editorBaseType)
		{
			return null;
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		public EventDescriptor GetDefaultEvent()
		{
			// TODO:  Add GraphicObject.GetDefaultEvent implementation
			return null;
		}

		public string GetClassName()
		{
			return "Graphic node";
		}

		#endregion
	}

	//Enables you to edit PointF like Point in the property grid
	public class PointFConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return false;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;
			return false;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string[] s=((string)value).Split(new char[]{','});
				return new PointF(float.Parse(s[0]),float.Parse(s[1]));
			}
			return null;
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				PointF p=(PointF)value;
				return String.Format("{0},{1}",p.X,p.Y);
			}
			return null;
		}
	}


	//Enables you to edit SizeF like Size in the property grid.
	public class SizeFConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return false;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;
			return false;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string[] s=((string)value).Split(new char[]{','});
				return new SizeF(float.Parse(s[0]),float.Parse(s[1]));
			}
			return null;
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				SizeF p=(SizeF)value;
				return String.Format("{0},{1}",p.Width,p.Height);
			}
			return null;
		}
	}
}
