using System;

namespace SampleProject
{
	public class CellNew : SourceGrid2.Cells.Real.Cell
	{
		public CellNew()
		{
			System.Reflection.Assembly l_ExecAs = System.Reflection.Assembly.GetExecutingAssembly();

			SourceGrid2.VisualModels.Common l_Model = new SourceGrid2.VisualModels.Common(false);
			l_Model.Image = System.Drawing.Image.FromStream(l_ExecAs.GetManifestResourceStream("SampleProject.Samples.new.bmp"));
			l_Model.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
			l_Model.BackColor = System.Drawing.Color.WhiteSmoke;
			l_Model.Border = SourceGrid2.RectangleBorder.NoBorder;
			VisualModel = l_Model;
		}
	}
}
