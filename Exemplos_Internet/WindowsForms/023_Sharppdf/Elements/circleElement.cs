//*****************************************************************
//******************Bezier Fix by scarlip (2004)*******************
//*****************************************************************
using System;
using System.Text;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;

namespace sharpPDF.Elements
{
	/// <summary>
	/// A Class that implements a PDF circle.
	/// </summary>
	public sealed class circleElement : pdfElement
	{
		
		private const float kappa = 0.5522847498f;

		private int _ray;
		private pdfLineStyle _lineStyle;

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="X">X position in the PDF document</param>
		/// <param name="Y">Y position in the PDF document</param>
		/// <param name="Ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of circle's border</param>
		/// <param name="fillColor">Color of the circle</param>
		public circleElement(int X, int Y, int Ray, pdfColor strokeColor, pdfColor fillColor):this(X, Y, Ray, strokeColor, fillColor, 1, predefinedLineStyle.csNormal)
		{
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="X">X position in the PDF document</param>
		/// <param name="Y">Y position in the PDF document</param>
		/// <param name="Ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of circle's border</param>
		/// <param name="fillColor">Color of the circle</param>
		/// <param name="newWidth">Border's size</param>
		public circleElement(int X, int Y, int Ray, pdfColor strokeColor, pdfColor fillColor, int newWidth):this(X, Y, Ray, strokeColor, fillColor, newWidth, predefinedLineStyle.csNormal)
		{		
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="X">X position in the PDF document</param>
		/// <param name="Y">Y position in the PDF document</param>
		/// <param name="Ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of circle's border</param>
		/// <param name="fillColor">Color of the circle</param>
		/// <param name="newStyle">Border's style</param>
		public circleElement(int X, int Y, int Ray, pdfColor strokeColor, pdfColor fillColor, predefinedLineStyle newStyle):this(X, Y, Ray, strokeColor, fillColor, 1, newStyle)
		{		
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="X">X position in the PDF document</param>
		/// <param name="Y">Y position in the PDF document</param>
		/// <param name="Ray">Ray of the circle</param>
		/// <param name="strokeColor">Color of circle's border</param>
		/// <param name="fillColor">Color of the circle</param>
		/// <param name="newWidth">Border's size</param>
		/// <param name="newStyle">Border's style</param>
		public circleElement(int X, int Y, int Ray, pdfColor strokeColor, pdfColor fillColor, int newWidth, predefinedLineStyle newStyle)
		{
			_coordX = X;
			_coordY = Y;
			_ray = Ray;
			_strokeColor = strokeColor;
			_fillColor = fillColor;
			_lineStyle = new pdfLineStyle(newWidth, newStyle);
			_height = (Ray * 2) + Convert.ToInt32(Math.Round((double)(newWidth / 2)));
			_width = _height;
		}

		/// <summary>
		/// Method that returns the PDF codes to write the circle in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public override string getText()
		{
			StringBuilder resultCircle = new StringBuilder();
            StringBuilder circleContent = new StringBuilder();
            circleContent.Append("q"+ Convert.ToChar(13) + Convert.ToChar(10));
			if (_strokeColor.isColor())
			{
				circleContent.Append(_strokeColor.rColor + " " + _strokeColor.gColor + " " + _strokeColor.bColor + " RG"+ Convert.ToChar(13) + Convert.ToChar(10));
			}
			if (_fillColor.isColor()) 
			{
				circleContent.Append(_fillColor.rColor + " " + _fillColor.gColor + " " + _fillColor.bColor + " rg" + Convert.ToChar(13) + Convert.ToChar(10));
			}
            circleContent.Append(_lineStyle.getText() + Convert.ToChar(13) + Convert.ToChar(10));

			// http://www.whizkidtech.redprince.net/bezier/circle/            
            float kappa_ray = _ray * kappa;
            // move to 12 (x,y+r) m
            circleContent.Append(_coordX.ToString() + " " + Math.Round((float)(_coordY + _ray),0).ToString() + " m" + Convert.ToChar(13) + Convert.ToChar(10));
            // arc to 3 (x+k,y+r) (x+r,y+k) (x+r,y) c
            circleContent.Append(Math.Round((_coordX + kappa_ray),0).ToString() + " " + Math.Round((float)(_coordY + _ray),0).ToString() + " " +
                Math.Round((float)(_coordX + _ray),0).ToString() + " " + Math.Round((float)(_coordY + kappa_ray),0).ToString() + " " +
                Math.Round((float)(_coordX + _ray),0).ToString() + " " + _coordY.ToString() + " c" + Convert.ToChar(13) + Convert.ToChar(10));
            // arc to 6 (x+r,y-k) (x+k,y-r) (x,y-r) c
            circleContent.Append(Math.Round((float)(_coordX + _ray),0).ToString() + " " + Math.Round((float)(_coordY - kappa_ray),0).ToString() + " " +
                Math.Round((float)(_coordX + kappa_ray),0).ToString() + " " + Math.Round((float)(_coordY - _ray),0).ToString() + " " +
                _coordX.ToString() + " " + Math.Round((float)(_coordY - _ray),0).ToString() + " c" + Convert.ToChar(13) + Convert.ToChar(10));
            // arc to 9 (x-k,y-r) (x-r,y-k) (x-r,y) c
            circleContent.Append(Math.Round((float)(_coordX - kappa_ray),0).ToString() + " " + Math.Round((float)(_coordY - _ray),0).ToString() + " " +
                Math.Round((float)(_coordX - _ray),0).ToString() + " " + Math.Round((float)(_coordY - kappa_ray),0).ToString() + " " +
                Math.Round((float)(_coordX - _ray),0).ToString() + " " + _coordY.ToString() + " c" + Convert.ToChar(13) + Convert.ToChar(10));
            // arc to 12 (x-r,y+k) (x-k,y+r) (x,y+r) c
            circleContent.Append(Math.Round((float)(_coordX - _ray),0).ToString() + " " + Math.Round((float)(_coordY + kappa_ray),0).ToString() + " " +
                Math.Round((float)(_coordX - kappa_ray),0).ToString() + " " + Math.Round((float)(_coordY + _ray),0).ToString() + " " +
                _coordX.ToString() + " " + Math.Round((float)(_coordY + _ray),0).ToString() + " c" + Convert.ToChar(13) + Convert.ToChar(10));
			circleContent.Append("B" + Convert.ToChar(13) + Convert.ToChar(10));
            circleContent.Append("Q" + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append(_objectID.ToString() + " 0 obj" + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append("<<" + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append("/Length " + circleContent.Length.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append(">>" + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append("stream" + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append(circleContent.ToString() + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append("endstream" + Convert.ToChar(13) + Convert.ToChar(10));
            resultCircle.Append("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
			circleContent = null;
			return resultCircle.ToString();            
		}

		/// <summary>
		/// Method that clones the element
		/// </summary>
		/// <returns>Cloned object</returns>
		public override object Clone()
		{
			return new circleElement(this._coordX, this._coordY, this._ray, (pdfColor)this._strokeColor.Clone(), (pdfColor)this._fillColor.Clone(), this._lineStyle.width, this._lineStyle.lineStyle);
		}
	}
}

